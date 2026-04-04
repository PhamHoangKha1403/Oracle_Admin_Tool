-- ============================================================
-- ATBM Hospital Management - Stored Procedures
-- Tạo tài khoản Oracle cho nhân viên/bệnh nhân
-- Chạy bằng tài khoản DBA (có quyền CREATE USER, GRANT)
-- Naming: sp_<ROLE>_<ACTION>_<TABLE>
-- ============================================================

-- ============================================================
-- sp_dba_create_user: Tạo tài khoản Oracle cho một nhân viên
-- Password mặc định: MA_NV || '23127@' (ví dụ: BS001 → BS00123127@)
-- Grant role tương ứng với VAI_TRO trong NHAN_VIEN
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_dba_create_user(p_ma_nv IN VARCHAR2)
AS
    v_vai_tro   NHAN_VIEN.VAI_TRO%TYPE;
    v_count     NUMBER;
    v_sql       VARCHAR2(500);
BEGIN
    -- Validate: NULL hoặc rỗng
    IF p_ma_nv IS NULL OR TRIM(p_ma_nv) = '' THEN
        RAISE_APPLICATION_ERROR(-20001, 'MA_NV khong duoc de trong');
    END IF;

    -- Validate: tồn tại trong NHAN_VIEN
    SELECT COUNT(*) INTO v_count
    FROM NHAN_VIEN
    WHERE MA_NV = p_ma_nv;

    IF v_count = 0 THEN
        RAISE_APPLICATION_ERROR(-20002, 'MA_NV khong ton tai trong NHAN_VIEN');
    END IF;

    -- Lấy VAI_TRO tương ứng với MA_NV
    SELECT VAI_TRO INTO v_vai_tro
    FROM NHAN_VIEN
    WHERE MA_NV = p_ma_nv;

    -- Tạo Oracle User (bỏ qua nếu đã tồn tại - ORA-01920)
    BEGIN
        v_sql := 'CREATE USER ' || p_ma_nv
              || ' IDENTIFIED BY "' || p_ma_nv || '23127@"'
              || ' DEFAULT TABLESPACE USERS'
              || ' QUOTA UNLIMITED ON USERS';
        EXECUTE IMMEDIATE v_sql;

        -- Grant CREATE SESSION
        EXECUTE IMMEDIATE 'GRANT CREATE SESSION TO ' || p_ma_nv;

        -- Grant role theo VAI_TRO của nhân viên
        IF v_vai_tro = 'Bác sĩ' THEN
            EXECUTE IMMEDIATE 'GRANT RL_BACSI TO ' || p_ma_nv;
        ELSIF v_vai_tro = 'Kỹ thuật viên' THEN
            EXECUTE IMMEDIATE 'GRANT RL_KYTHUATVIEN TO ' || p_ma_nv;
        ELSIF v_vai_tro = 'Điều phối viên' THEN
            EXECUTE IMMEDIATE 'GRANT RL_DIEUPHOIVIEN TO ' || p_ma_nv;
        ELSIF v_vai_tro = 'Bệnh nhân' THEN
            EXECUTE IMMEDIATE 'GRANT RL_BENHNHAN TO ' || p_ma_nv;
        END IF;

    EXCEPTION
        WHEN OTHERS THEN
            -- Bỏ qua ORA-01920: user đã tồn tại
            IF SQLCODE = -1920 THEN
                NULL;
            ELSE
                RAISE;
            END IF;
    END;
END sp_dba_create_user;
/

-- ============================================================
-- sp_dba_createall_user: Tạo tài khoản Oracle cho toàn bộ NHAN_VIEN
-- Trả về số lượng tài khoản được tạo mới thành công
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_dba_createall_user(p_so_luong_tao OUT NUMBER)
AS
    CURSOR cur_nv IS
        SELECT MA_NV FROM NHAN_VIEN;
    v_count     NUMBER;
BEGIN
    p_so_luong_tao := 0;

    FOR rec IN cur_nv LOOP
        BEGIN
            -- Kiểm tra user đã tồn tại chưa
            SELECT COUNT(*) INTO v_count
            FROM DBA_USERS
            WHERE USERNAME = UPPER(rec.MA_NV);

            IF v_count = 0 THEN
                sp_dba_create_user(rec.MA_NV);
                p_so_luong_tao := p_so_luong_tao + 1;
            END IF;

        EXCEPTION
            WHEN OTHERS THEN
                -- Ghi nhận lỗi, tiếp tục với bản ghi tiếp theo
                NULL;
        END;
    END LOOP;
END sp_dba_createall_user;
/
