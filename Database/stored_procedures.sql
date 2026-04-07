-- ============================================================
-- ATBM Hospital Management - Stored Procedures
-- Tạo tài khoản Oracle cho nhân viên/bệnh nhân
-- Chạy bằng tài khoản DBA (có quyền CREATE USER, GRANT)
-- Naming: sp_<ROLE>_<ACTION>_<TABLE>
-- ============================================================

-- ============================================================
-- sp_dba_create_user: Tạo tài khoản Oracle cho một nhân viên/bệnh nhân
-- Password mặc định: ID || '23127@' (ví dụ: BS001 → BS00123127@)
-- Grant role tương ứng với VAI_TRO
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_dba_create_user(p_id IN VARCHAR2)
AS
    v_vai_tro   VARCHAR2(50);
    v_count     NUMBER;
    v_sql       VARCHAR2(500);
BEGIN
    -- Validate: NULL hoặc rỗng
    IF p_id IS NULL OR TRIM(p_id) = '' THEN
        RAISE_APPLICATION_ERROR(-20001, 'ID khong duoc de trong');
    END IF;

    -- Kiểm tra tồn tại trong NHANVIEN
    SELECT COUNT(*) INTO v_count
    FROM NHANVIEN
    WHERE MANV = p_id;

    IF v_count > 0 THEN
        SELECT VAITRO INTO v_vai_tro
        FROM NHANVIEN
        WHERE MANV = p_id;
    ELSE
        -- Kiểm tra tồn tại trong BENHNHAN
        SELECT COUNT(*) INTO v_count
        FROM BENHNHAN
        WHERE MABN = p_id;

        IF v_count > 0 THEN
            v_vai_tro := 'Bệnh nhân';
        ELSE
            RAISE_APPLICATION_ERROR(-20002, 'ID khong ton tai trong NHANVIEN hay BENHNHAN');
        END IF;
    END IF;

    -- Tạo Oracle User (bỏ qua nếu đã tồn tại - ORA-01920)
    BEGIN
        v_sql := 'CREATE USER ' || p_id
              || ' IDENTIFIED BY "' || p_id || '23127@"'
              || ' DEFAULT TABLESPACE USERS'
              || ' QUOTA UNLIMITED ON USERS';
        EXECUTE IMMEDIATE v_sql;

        -- Grant CREATE SESSION
        EXECUTE IMMEDIATE 'GRANT CREATE SESSION TO ' || p_id;

        -- Grant role theo VAI_TRO
        IF v_vai_tro = 'Bac si' THEN
            EXECUTE IMMEDIATE 'GRANT RL_BACSI TO ' || p_id;
        ELSIF v_vai_tro = 'Ky thuat vien' THEN
            EXECUTE IMMEDIATE 'GRANT RL_KYTHUATVIEN TO ' || p_id;
        ELSIF v_vai_tro = 'Dieu phoi vien' THEN
            EXECUTE IMMEDIATE 'GRANT RL_DIEUPHOIVIEN TO ' || p_id;
        ELSIF v_vai_tro = 'Bệnh nhân' THEN
            EXECUTE IMMEDIATE 'GRANT RL_BENHNHAN TO ' || p_id;
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
-- sp_dba_createall_user: Tạo tài khoản Oracle cho toàn bộ NHANVIEN và BENHNHAN
-- Trả về số lượng tài khoản được tạo mới thành công
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_dba_createall_user(p_so_luong_tao OUT NUMBER)
AS
    CURSOR cur_users IS
        SELECT MANV AS ID FROM NHANVIEN
        UNION ALL
        SELECT MABN AS ID FROM BENHNHAN;
    v_count     NUMBER;
BEGIN
    p_so_luong_tao := 0;

    FOR rec IN cur_users LOOP
        BEGIN
            -- Kiểm tra user đã tồn tại chưa
            SELECT COUNT(*) INTO v_count
            FROM DBA_USERS
            WHERE USERNAME = UPPER(rec.ID);

            IF v_count = 0 THEN
                sp_dba_create_user(rec.ID);
                p_so_luong_tao := p_so_luong_tao + 1;
            END IF;

        EXCEPTION
            WHEN OTHERS THEN
                -- Nếu có lỗi, throw exception ra ngoài để C# bắt được
                RAISE;
        END;
    END LOOP;
END sp_dba_createall_user;
/

-- ============================================================
-- sp_NV_Select_NHANVIEN
-- Cho phép nhân viên xem QUEQUAN, SODT của chính mình
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_NV_Select_NHANVIEN(p_cursor OUT SYS_REFCURSOR)
AS
    v_user VARCHAR2(100);
BEGIN
    -- Lấy session user hiện tại
    v_user := SYS_CONTEXT('USERENV', 'SESSION_USER');
    
    OPEN p_cursor FOR
        SELECT QUEQUAN, SODT
        FROM NHANVIEN
        WHERE MANV = v_user;
END sp_NV_Select_NHANVIEN;
/

-- ============================================================
-- sp_NV_Update_NHANVIEN
-- Cho phép nhân viên cập nhật QUEQUAN, SODT của chính mình
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_NV_Update_NHANVIEN(
    p_QUEQUAN IN NVARCHAR2,
    p_SODT IN VARCHAR2
)
AS
    v_user VARCHAR2(100);
BEGIN
    v_user := SYS_CONTEXT('USERENV', 'SESSION_USER');
    
    UPDATE NHANVIEN
    SET QUEQUAN = p_QUEQUAN,
        SODT = p_SODT
    WHERE MANV = v_user;
    
    COMMIT;
END sp_NV_Update_NHANVIEN;
/

-- ============================================================
-- sp_BN_Select_BENHNHAN
-- Cho phép bệnh nhân xem các trường quy định
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_BN_Select_BENHNHAN(p_cursor OUT SYS_REFCURSOR)
AS
    v_user VARCHAR2(100);
BEGIN
    v_user := SYS_CONTEXT('USERENV', 'SESSION_USER');
    
    OPEN p_cursor FOR
        SELECT SONHA, TENDUONG, QUANHUYEN, TINHTP, TIENSUBENH, TIENSUBENHGD, DIUNGTUOC
        FROM BENHNHAN
        WHERE MABN = v_user;
END sp_BN_Select_BENHNHAN;
/

-- ============================================================
-- sp_BN_Update_BENHNHAN
-- Cho phép bệnh nhân tự update
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_BN_Update_BENHNHAN(
    p_SONHA IN NVARCHAR2,
    p_TENDUONG IN NVARCHAR2,
    p_QUANHUYEN IN NVARCHAR2,
    p_TINHTP IN NVARCHAR2,
    p_TIENSUBENH IN NVARCHAR2,
    p_TIENSUBENHGD IN NVARCHAR2,
    p_DIUNGTUOC IN NVARCHAR2
)
AS
    v_user VARCHAR2(100);
BEGIN
    v_user := SYS_CONTEXT('USERENV', 'SESSION_USER');
    
    UPDATE BENHNHAN
    SET SONHA = p_SONHA,
        TENDUONG = p_TENDUONG,
        QUANHUYEN = p_QUANHUYEN,
        TINHTP = p_TINHTP,
        TIENSUBENH = p_TIENSUBENH,
        TIENSUBENHGD = p_TIENSUBENHGD,
        DIUNGTUOC = p_DIUNGTUOC
    WHERE MABN = v_user;
    
    COMMIT;
END sp_BN_Update_BENHNHAN;
/
