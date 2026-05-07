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

    -- Kiểm tra tồn tại trong NHAN_VIEN
    SELECT COUNT(*) INTO v_count
    FROM NHAN_VIEN
    WHERE MA_NV = p_id;

    IF v_count > 0 THEN
        SELECT VAI_TRO INTO v_vai_tro
        FROM NHAN_VIEN
        WHERE MA_NV = p_id;
    ELSE
        -- Kiểm tra tồn tại trong BENH_NHAN
        SELECT COUNT(*) INTO v_count
        FROM BENH_NHAN
        WHERE MA_BN = p_id;

        IF v_count > 0 THEN
            v_vai_tro := 'Bệnh nhân';
        ELSE
            RAISE_APPLICATION_ERROR(-20002, 'ID khong ton tai trong NHAN_VIEN hay BENH_NHAN');
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
        IF v_vai_tro = 'Bác sĩ' THEN
            EXECUTE IMMEDIATE 'GRANT RL_BACSI TO ' || p_id;
        ELSIF v_vai_tro = 'Kỹ thuật viên' THEN
            EXECUTE IMMEDIATE 'GRANT RL_KYTHUATVIEN TO ' || p_id;
        ELSIF v_vai_tro = 'Điều phối viên' THEN
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
        SELECT MA_NV AS ID FROM NHAN_VIEN
        UNION ALL
        SELECT MA_BN AS ID FROM BENH_NHAN;
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
        SELECT MA_NV, HO_TEN, PHAI, NGAY_SINH, CCCD, QUE_QUAN, SDT, VAI_TRO, CHUYEN_KHOA
        FROM NHAN_VIEN
        WHERE MA_NV = v_user;
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
    
    UPDATE NHAN_VIEN
    SET QUE_QUAN = p_QUEQUAN,
        SDT = p_SODT
    WHERE MA_NV = v_user;
    
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
        SELECT n.HO_TEN, n.PHAI, n.NGAY_SINH, n.CCCD, n.SDT, b.SO_NHA, b.TEN_DUONG, b.QUAN_HUYEN, b.TINH_TP, b.TIEN_SU_BENH, b.TIEN_SU_BENH_GD, b.DI_UNG_THUOC
        FROM BENH_NHAN b
        JOIN NHAN_VIEN n ON b.MA_BN = n.MA_NV
        WHERE b.MA_BN = v_user;
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
    p_DIUNGTHUOC IN NVARCHAR2
)
AS
    v_user VARCHAR2(100);
BEGIN
    v_user := SYS_CONTEXT('USERENV', 'SESSION_USER');
    
    UPDATE BENH_NHAN
    SET SO_NHA = p_SONHA,
        TEN_DUONG = p_TENDUONG,
        QUAN_HUYEN = p_QUANHUYEN,
        TINH_TP = p_TINHTP,
        TIEN_SU_BENH = p_TIENSUBENH,
        TIEN_SU_BENH_GD = p_TIENSUBENHGD,
        DI_UNG_THUOC = p_DIUNGTHUOC
    WHERE MA_BN = v_user;
    
    COMMIT;
END sp_BN_Update_BENHNHAN;
/

-- ============================================================
-- sp_DPV_Select_BENHNHAN
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_DPV_Select_BENHNHAN(p_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN p_cursor FOR
        SELECT b.MA_BN, n.HO_TEN, n.PHAI, n.NGAY_SINH, n.CCCD, n.SDT, n.CHUYEN_KHOA, b.SO_NHA, b.TEN_DUONG, b.QUAN_HUYEN, b.TINH_TP, b.TIEN_SU_BENH, b.TIEN_SU_BENH_GD, b.DI_UNG_THUOC
        FROM BENH_NHAN b
        JOIN NHAN_VIEN n ON b.MA_BN = n.MA_NV;
END sp_DPV_Select_BENHNHAN;
/

-- ============================================================
-- sp_DPV_Insert_BENHNHAN
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_DPV_Insert_BENHNHAN(
    p_MABN IN VARCHAR2,
    p_HOTEN IN NVARCHAR2,
    p_PHAI IN NVARCHAR2,
    p_NGAYSINH IN DATE,
    p_CCCD IN VARCHAR2,
    p_SDT IN VARCHAR2,
    p_SONHA IN NVARCHAR2,
    p_TENDUONG IN NVARCHAR2,
    p_QUANHUYEN IN NVARCHAR2,
    p_TINHTP IN NVARCHAR2,
    p_TIENSUBENH IN NVARCHAR2,
    p_TIENSUBENHGD IN NVARCHAR2,
    p_DIUNGTHUOC IN NVARCHAR2,
    p_CHUYENKHOA IN VARCHAR2
)
AS
BEGIN
    INSERT INTO NHAN_VIEN (MA_NV, HO_TEN, PHAI, NGAY_SINH, CCCD, SDT, VAI_TRO, CHUYEN_KHOA)
    VALUES (p_MABN, p_HOTEN, p_PHAI, p_NGAYSINH, p_CCCD, p_SDT, 'Bệnh nhân', p_CHUYENKHOA);

    INSERT INTO BENH_NHAN (MA_BN, SO_NHA, TEN_DUONG, QUAN_HUYEN, TINH_TP, TIEN_SU_BENH, TIEN_SU_BENH_GD, DI_UNG_THUOC)
    VALUES (p_MABN, p_SONHA, p_TENDUONG, p_QUANHUYEN, p_TINHTP, p_TIENSUBENH, p_TIENSUBENHGD, p_DIUNGTHUOC);
    COMMIT;
END sp_DPV_Insert_BENHNHAN;
/

-- ============================================================
-- sp_DPV_Update_BENHNHAN
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_DPV_Update_BENHNHAN(
    p_MABN IN VARCHAR2,
    p_HOTEN IN NVARCHAR2,
    p_PHAI IN NVARCHAR2,
    p_NGAYSINH IN DATE,
    p_CCCD IN VARCHAR2,
    p_SDT IN VARCHAR2,
    p_SONHA IN NVARCHAR2,
    p_TENDUONG IN NVARCHAR2,
    p_QUANHUYEN IN NVARCHAR2,
    p_TINHTP IN NVARCHAR2,
    p_TIENSUBENH IN NVARCHAR2,
    p_TIENSUBENHGD IN NVARCHAR2,
    p_DIUNGTHUOC IN NVARCHAR2,
    p_CHUYENKHOA IN VARCHAR2
)
AS
BEGIN
    UPDATE NHAN_VIEN
    SET HO_TEN = p_HOTEN,
        PHAI = p_PHAI,
        NGAY_SINH = p_NGAYSINH,
        CCCD = p_CCCD,
        SDT = p_SDT,
        CHUYEN_KHOA = p_CHUYENKHOA
    WHERE MA_NV = p_MABN;

    UPDATE BENH_NHAN
    SET SO_NHA = p_SONHA,
        TEN_DUONG = p_TENDUONG,
        QUAN_HUYEN = p_QUANHUYEN,
        TINH_TP = p_TINHTP,
        TIEN_SU_BENH = p_TIENSUBENH,
        TIEN_SU_BENH_GD = p_TIENSUBENHGD,
        DI_UNG_THUOC = p_DIUNGTHUOC
    WHERE MA_BN = p_MABN;
    COMMIT;
END sp_DPV_Update_BENHNHAN;
/

-- ============================================================
-- sp_DPV_Select_HSBA
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_DPV_Select_HSBA(p_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN p_cursor FOR
        SELECT MA_HSBA, MA_BN, NGAY, CHAN_DOAN, DIEU_TRI, MA_BS, MA_KHOA, KET_LUAN
        FROM HSBA;
END sp_DPV_Select_HSBA;
/

-- ============================================================
-- sp_DPV_Select_HSBADV
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_DPV_Select_HSBADV(p_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN p_cursor FOR
        SELECT MA_HSBA, LOAI_DV, NGAY_DV, MA_KTV, KET_QUA
        FROM HSBA_DV;
END sp_DPV_Select_HSBADV;
/

-- ============================================================
-- sp_DPV_Insert_HSBA
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_DPV_Insert_HSBA(
    p_MAHSBA IN VARCHAR2,
    p_MABN IN VARCHAR2,
    p_NGAY IN DATE,
    p_CHANDOAN IN NVARCHAR2,
    p_DIEUTRI IN NVARCHAR2,
    p_MABS IN VARCHAR2,
    p_MAKHOA IN VARCHAR2,
    p_KETLUAN IN NVARCHAR2
)
AS
BEGIN
    INSERT INTO HSBA (MA_HSBA, MA_BN, NGAY, CHAN_DOAN, DIEU_TRI, MA_BS, MA_KHOA, KET_LUAN)
    VALUES (p_MAHSBA, p_MABN, p_NGAY, p_CHANDOAN, p_DIEUTRI, p_MABS, p_MAKHOA, p_KETLUAN);
    COMMIT;
END sp_DPV_Insert_HSBA;
/

-- ============================================================
-- sp_DPV_Update_HSBA
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_DPV_Update_HSBA(
    p_MAHSBA IN VARCHAR2,
    p_MAKHOA IN VARCHAR2,
    p_MABS IN VARCHAR2
)
AS
BEGIN
    UPDATE HSBA
    SET MA_KHOA = p_MAKHOA,
        MA_BS = p_MABS
    WHERE MA_HSBA = p_MAHSBA;
    COMMIT;
END sp_DPV_Update_HSBA;
/

-- ============================================================
-- sp_DPV_Insert_HSBADV
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_DPV_Insert_HSBADV(
    p_MAHSBA IN VARCHAR2,
    p_LOAIDV IN VARCHAR2,
    p_NGAYDV IN DATE,
    p_MAKTV IN VARCHAR2,
    p_KETQUA IN VARCHAR2
)
AS
BEGIN
    INSERT INTO HSBA_DV (MA_HSBA, LOAI_DV, NGAY_DV, MA_KTV, KET_QUA)
    VALUES (p_MAHSBA, p_LOAIDV, p_NGAYDV, p_MAKTV, p_KETQUA);
    COMMIT;
END sp_DPV_Insert_HSBADV;
/

-- ============================================================
-- sp_DPV_Update_HSBADV
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_DPV_Update_HSBADV(
    p_MAHSBA IN VARCHAR2,
    p_LOAIDV IN VARCHAR2,
    p_NGAYDV IN DATE,
    p_MAKTV IN VARCHAR2
)
AS
BEGIN
    UPDATE HSBA_DV
    SET MA_KTV = p_MAKTV
    WHERE MA_HSBA = p_MAHSBA
      AND LOAI_DV = p_LOAIDV
      AND NGAY_DV = p_NGAYDV;
    COMMIT;
END sp_DPV_Update_HSBADV;
/

-- sp đưa bảng audit lên UI
CREATE OR REPLACE PROCEDURE SP_GET_AUDIT_FGA (
    p_cursor OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN p_cursor FOR
        SELECT 
            DB_USER,
            OBJECT_NAME,
            POLICY_NAME,
            STATEMENT_TYPE,
            DBMS_LOB.SUBSTR(SQL_TEXT, 1000, 1) AS SQL_TEXT,
            TO_CHAR(TIMESTAMP, 'DD/MM/YYYY HH24:MI:SS') AS TIME_FULL
        FROM DBA_FGA_AUDIT_TRAIL
        ORDER BY TIMESTAMP DESC;
END;
/
--VARIABLE rc REFCURSOR;
--
--EXEC SP_GET_AUDIT_FGA(:rc);
--
--PRINT rc;

-- TẮT TẤT CẢ AUDIT
CREATE OR REPLACE PROCEDURE SP_DISABLE_ALL_AUDIT AS
BEGIN
    FOR r IN (
        SELECT OBJECT_SCHEMA, OBJECT_NAME, POLICY_NAME
        FROM ALL_AUDIT_POLICIES
        WHERE OBJECT_SCHEMA = 'ADMIN_PH2'
    ) LOOP
        BEGIN
            DBMS_FGA.DISABLE_POLICY(
                object_schema => r.OBJECT_SCHEMA,
                object_name   => r.OBJECT_NAME,
                policy_name   => r.POLICY_NAME
            );
        EXCEPTION WHEN OTHERS THEN NULL;
        END;
    END LOOP;
END;
/
-- BẬT LẠI TẤT CẢ AUDIT 
CREATE OR REPLACE PROCEDURE SP_ENABLE_ALL_AUDIT AS
BEGIN
    FOR r IN (
        SELECT OBJECT_SCHEMA, OBJECT_NAME, POLICY_NAME
        FROM ALL_AUDIT_POLICIES
        WHERE OBJECT_SCHEMA = 'ADMIN_PH2'
    ) LOOP
        BEGIN
            DBMS_FGA.ENABLE_POLICY(
                object_schema => r.OBJECT_SCHEMA,
                object_name   => r.OBJECT_NAME,
                policy_name   => r.POLICY_NAME
            );
        EXCEPTION WHEN OTHERS THEN NULL;
        END;
    END LOOP;
END;
/

-- ============================================================
-- 8. CẤP QUYỀN VÀ TẠO SYNONYM CHO STORED PROCEDURES
-- ============================================================

-- Cho toàn bộ nhân viên
CREATE OR REPLACE PUBLIC SYNONYM sp_NV_Select_NHANVIEN FOR sp_NV_Select_NHANVIEN;
CREATE OR REPLACE PUBLIC SYNONYM sp_NV_Update_NHANVIEN FOR sp_NV_Update_NHANVIEN;
GRANT EXECUTE ON sp_NV_Select_NHANVIEN TO RL_BACSI;
GRANT EXECUTE ON sp_NV_Update_NHANVIEN TO RL_BACSI;
GRANT EXECUTE ON sp_NV_Select_NHANVIEN TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON sp_NV_Update_NHANVIEN TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON sp_NV_Select_NHANVIEN TO RL_KYTHUATVIEN;
GRANT EXECUTE ON sp_NV_Update_NHANVIEN TO RL_KYTHUATVIEN;
GRANT EXECUTE ON sp_NV_Select_NHANVIEN TO RL_BENHNHAN;

-- Cho bệnh nhân
CREATE OR REPLACE PUBLIC SYNONYM sp_BN_Select_BENHNHAN FOR sp_BN_Select_BENHNHAN;
CREATE OR REPLACE PUBLIC SYNONYM sp_BN_Update_BENHNHAN FOR sp_BN_Update_BENHNHAN;
GRANT EXECUTE ON sp_BN_Select_BENHNHAN TO RL_BENHNHAN;
GRANT EXECUTE ON sp_BN_Update_BENHNHAN TO RL_BENHNHAN;

-- Cho Điều phối viên
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Select_BENHNHAN FOR sp_DPV_Select_BENHNHAN;
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Insert_BENHNHAN FOR sp_DPV_Insert_BENHNHAN;
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Update_BENHNHAN FOR sp_DPV_Update_BENHNHAN;
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Select_HSBA FOR sp_DPV_Select_HSBA;
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Select_HSBADV FOR sp_DPV_Select_HSBADV;
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Insert_HSBA FOR sp_DPV_Insert_HSBA;
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Update_HSBA FOR sp_DPV_Update_HSBA;
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Insert_HSBADV FOR sp_DPV_Insert_HSBADV;
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Update_HSBADV FOR sp_DPV_Update_HSBADV;

GRANT EXECUTE ON sp_DPV_Select_BENHNHAN TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON sp_DPV_Insert_BENHNHAN TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON sp_DPV_Update_BENHNHAN TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON sp_DPV_Select_HSBA TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON sp_DPV_Select_HSBADV TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON sp_DPV_Insert_HSBA TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON sp_DPV_Update_HSBA TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON sp_DPV_Insert_HSBADV TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON sp_DPV_Update_HSBADV TO RL_DIEUPHOIVIEN;

-- ============================================================
-- Cho Bác sĩ (HSBA, HSBADV)
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_BS_Update_HSBA(
    p_ma_hsba IN VARCHAR2,
    p_chan_doan IN NVARCHAR2,
    p_dieu_tri IN NVARCHAR2,
    p_ket_luan IN NVARCHAR2
) AUTHID CURRENT_USER
AS
    v_user VARCHAR2(100);
BEGIN
    v_user := SYS_CONTEXT('USERENV', 'SESSION_USER');
    UPDATE HSBA
    SET CHAN_DOAN = p_chan_doan,
        DIEU_TRI = p_dieu_tri,
        KET_LUAN = p_ket_luan
    WHERE MA_HSBA = p_ma_hsba
      AND MA_BS = v_user;
    COMMIT;
END sp_BS_Update_HSBA;
/

CREATE OR REPLACE PROCEDURE sp_BS_Insert_HSBADV(
    p_ma_hsba IN VARCHAR2,
    p_loai_dv IN VARCHAR2,
    p_ngay_dv IN DATE,
    p_ma_ktv IN VARCHAR2,
    p_ket_qua IN NVARCHAR2
) AUTHID CURRENT_USER
AS
BEGIN
    INSERT INTO HSBA_DV (MA_HSBA, LOAI_DV, NGAY_DV, MA_KTV, KET_QUA)
    VALUES (p_ma_hsba, p_loai_dv, p_ngay_dv, p_ma_ktv, p_ket_qua);
    COMMIT;
END sp_BS_Insert_HSBADV;
/

CREATE OR REPLACE PUBLIC SYNONYM sp_BS_Update_HSBA FOR sp_BS_Update_HSBA;
CREATE OR REPLACE PUBLIC SYNONYM sp_BS_Insert_HSBADV FOR sp_BS_Insert_HSBADV;
GRANT EXECUTE ON sp_BS_Update_HSBA TO RL_BACSI;
GRANT EXECUTE ON sp_BS_Insert_HSBADV TO RL_BACSI;

-- ============================================================

-- sp_KTV_Select_HSBADV
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_KTV_Select_HSBADV(p_cursor OUT SYS_REFCURSOR)
AS
BEGIN
    OPEN p_cursor FOR
        SELECT MA_HSBA, LOAI_DV, NGAY_DV, MA_KTV, KET_QUA
        FROM HSBA_DV;
END sp_KTV_Select_HSBADV;
/

-- ============================================================
-- sp_KTV_Update_KETQUA
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_KTV_Update_KETQUA(
    p_MAHSBA IN VARCHAR2,
    p_LOAIDV IN VARCHAR2,
    p_NGAYDV IN DATE,
    p_KETQUA IN NVARCHAR2
)
AS
    v_user VARCHAR2(100);
    v_old_ketqua VARCHAR2(500);
BEGIN
    v_user := SYS_CONTEXT('USERENV', 'SESSION_USER');
    
    BEGIN
        -- Lấy kết quả cũ để ghi audit log
        SELECT KET_QUA INTO v_old_ketqua
        FROM HSBA_DV
        WHERE MA_HSBA = p_MAHSBA
          AND LOAI_DV = p_LOAIDV
          AND NGAY_DV = p_NGAYDV
          AND MA_KTV = v_user;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RAISE_APPLICATION_ERROR(-20005, 'Khong tim thay dich vu hoac ban khong co quyen cap nhat dich vu nay.');
    END;
      
    -- Cập nhật kết quả mới
    UPDATE HSBA_DV
    SET KET_QUA = p_KETQUA
    WHERE MA_HSBA = p_MAHSBA
      AND LOAI_DV = p_LOAIDV
      AND NGAY_DV = p_NGAYDV
      AND MA_KTV = v_user;
      
    -- Ghi Audit Log trực tiếp trong SP
    INSERT INTO AUDIT_HSBADV_LOG (MAHSBA, LOAIDV, NGAYDV, MAKTV, OLD_KETQUA, NEW_KETQUA)
    VALUES (p_MAHSBA, p_LOAIDV, p_NGAYDV, v_user, v_old_ketqua, p_KETQUA);
      
    COMMIT;
END sp_KTV_Update_KETQUA;
/

-- ============================================================
-- sp_KTV_Select_AuditLog
-- ============================================================
CREATE OR REPLACE PROCEDURE sp_KTV_Select_AuditLog(p_cursor OUT SYS_REFCURSOR)
AS
    v_user VARCHAR2(100);
BEGIN
    v_user := SYS_CONTEXT('USERENV', 'SESSION_USER');
    OPEN p_cursor FOR
        SELECT MAHSBA, LOAIDV, NGAYDV, OLD_KETQUA, NEW_KETQUA, NGAY_GHI
        FROM AUDIT_HSBADV_LOG
        WHERE MAKTV = v_user
        ORDER BY NGAY_GHI DESC;
END sp_KTV_Select_AuditLog;
/

-- Cho Kỹ thuật viên
CREATE OR REPLACE PUBLIC SYNONYM sp_KTV_Select_HSBADV FOR sp_KTV_Select_HSBADV;
CREATE OR REPLACE PUBLIC SYNONYM sp_KTV_Update_KETQUA FOR sp_KTV_Update_KETQUA;
CREATE OR REPLACE PUBLIC SYNONYM sp_KTV_Select_AuditLog FOR sp_KTV_Select_AuditLog;

GRANT EXECUTE ON sp_KTV_Select_HSBADV TO RL_KYTHUATVIEN;
GRANT EXECUTE ON sp_KTV_Update_KETQUA TO RL_KYTHUATVIEN;
GRANT EXECUTE ON sp_KTV_Select_AuditLog TO RL_KYTHUATVIEN;
CREATE OR REPLACE PUBLIC SYNONYM AUDIT_HSBADV_LOG FOR AUDIT_HSBADV_LOG;
GRANT SELECT, INSERT ON AUDIT_HSBADV_LOG TO RL_KYTHUATVIEN;
