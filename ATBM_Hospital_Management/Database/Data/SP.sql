--Mã nhóm: DBSec05
--22127172 - Phạm Hoàng Kha
--23127145 - Khưu Ngọc Ý Vy
--23127254 - Nguyễn Thị Như Quỳnh
--23127428 - Nguyễn Thị Trà My
--23127524 - Hình Diễm Xuân

-- ATBM Hospital Management - Stored Procedures
-- Chạy bằng tài khoản DBA (có quyền CREATE USER, GRANT)
-- Naming: sp_<ROLE>_<ACTION>_<TABLE>

-- sp_dba_create_user: Tạo tài khoản Oracle cho một nhân viên/bệnh nhân
-- Password mặc định: ID || '23127@' (ví dụ: BS001 → BS00123127@)
-- Grant role tương ứng với VAI_TRO
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
    FROM ADMIN_PH2.NHAN_VIEN
    WHERE MA_NV = p_id;

    IF v_count > 0 THEN
        SELECT VAI_TRO INTO v_vai_tro
        FROM ADMIN_PH2.NHAN_VIEN
        WHERE MA_NV = p_id;
    ELSE
        -- Kiểm tra tồn tại trong BENH_NHAN
        SELECT COUNT(*) INTO v_count
        FROM ADMIN_PH2.BENH_NHAN
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

-- sp_dba_createall_user: Tạo tài khoản Oracle cho toàn bộ NHANVIEN và BENHNHAN
-- Trả về số lượng tài khoản được tạo mới thành công
CREATE OR REPLACE PROCEDURE sp_dba_createall_user(p_so_luong_tao OUT NUMBER)
AS
    CURSOR cur_users IS
        SELECT MA_NV AS ID, VAI_TRO FROM ADMIN_PH2.NHAN_VIEN
        UNION ALL
        SELECT MA_BN AS ID, 'Bệnh nhân' FROM ADMIN_PH2.BENH_NHAN;

BEGIN
    p_so_luong_tao := 0;

    FOR rec IN cur_users LOOP
        BEGIN
            -- tạo user (bỏ check tồn tại)
            BEGIN
                sp_dba_create_user(rec.ID);
                p_so_luong_tao := p_so_luong_tao + 1;
            EXCEPTION
                WHEN OTHERS THEN
                    IF SQLCODE != -01920 THEN
                        RAISE;
                    END IF;
            END;

            -- grant role
            IF rec.VAI_TRO = 'Điều phối viên' THEN
                EXECUTE IMMEDIATE 'GRANT RL_DIEUPHOIVIEN TO ' || rec.ID;

            ELSIF rec.VAI_TRO = 'Bác sĩ' THEN
                EXECUTE IMMEDIATE 'GRANT RL_BACSI TO ' || rec.ID;

            ELSIF rec.VAI_TRO = 'Kỹ thuật viên' THEN
                EXECUTE IMMEDIATE 'GRANT RL_KYTHUATVIEN TO ' || rec.ID;

            ELSIF rec.VAI_TRO = 'Bệnh nhân' THEN
                EXECUTE IMMEDIATE 'GRANT RL_BENHNHAN TO ' || rec.ID;
            END IF;

        END;
    END LOOP;
END;
/

-- sp_NV_Select_NHANVIEN
CREATE OR REPLACE PROCEDURE sp_NV_Select_NHANVIEN(p_cursor OUT SYS_REFCURSOR)
AS
    v_user VARCHAR2(100);
BEGIN
    -- Lấy session user hiện tại
    v_user := SYS_CONTEXT('USERENV', 'SESSION_USER');
    
    OPEN p_cursor FOR
    SELECT 
        MA_NV,
        HO_TEN,
        PHAI,
        NGAY_SINH,
        CCCD,
        QUE_QUAN,
        SDT
    FROM NHAN_VIEN
    WHERE MA_NV = v_user;
    
END sp_NV_Select_NHANVIEN;
/

-- sp_NV_Update_NHANVIEN
-- Cho phép nhân viên cập nhật QUEQUAN, SODT của chính mình
CREATE OR REPLACE PROCEDURE sp_NV_Update_NHANVIEN(
    p_QUEQUAN IN NVARCHAR2,
    p_SODT IN VARCHAR2
)
AS
    v_user VARCHAR2(100);
BEGIN
    v_user := SYS_CONTEXT('USERENV', 'SESSION_USER');
    
    UPDATE ADMIN_PH2.NHAN_VIEN
    SET QUE_QUAN = p_QUEQUAN,
        SDT = p_SODT
    WHERE MA_NV = v_user;
    
    COMMIT;
END sp_NV_Update_NHANVIEN;
/

-- sp_BN_Select_BENHNHAN
-- Cho phép bệnh nhân xem các trường quy định
CREATE OR REPLACE PROCEDURE sp_BN_Select_BENHNHAN(p_cursor OUT SYS_REFCURSOR)
AS
    v_user VARCHAR2(100);
BEGIN
    v_user := SYS_CONTEXT('USERENV', 'SESSION_USER');
    
    OPEN p_cursor FOR
        SELECT MA_NV, HO_TEN, PHAI, NGAY_SINH, CCCD, QUE_QUAN, SDT, VAI_TRO, CHUYEN_KHOA, SO_NHA, TEN_DUONG, QUAN_HUYEN, TINH_TP, TIEN_SU_BENH, TIEN_SU_BENH_GD, DI_UNG_THUOC
        FROM ADMIN_PH2.VW_BN_BENH_NHAN bn
        JOIN ADMIN_PH2.NHAN_VIEN nv ON bn.MA_BN = nv.MA_NV
        WHERE bn.MA_BN = v_user;
END sp_BN_Select_BENHNHAN;
/

-- sp_BN_Update_BENHNHAN
-- Cho phép bệnh nhân tự update
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
    
    UPDATE ADMIN_PH2.BENH_NHAN
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

-- BACSI SELECT BENHNHAN 
CREATE OR REPLACE PROCEDURE sp_BS_Select_BENHNHAN(
    p_cursor OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN p_cursor FOR
        SELECT DISTINCT
            bn.MA_BN,
            nv.HO_TEN,       
            nv.PHAI,        
            nv.NGAY_SINH,    
            nv.CCCD,        
            bn.SO_NHA,
            bn.TEN_DUONG,
            bn.QUAN_HUYEN,
            bn.TINH_TP,
            bn.TIEN_SU_BENH,
            bn.DI_UNG_THUOC
        FROM ADMIN_PH2.BENH_NHAN bn
        JOIN ADMIN_PH2.NHAN_VIEN nv ON bn.MA_BN = nv.MA_NV; 
END sp_BS_Select_BENHNHAN;
/

-- BACSI UPDATE BENHNHAN(TIENSUBENH, TIENSUBENHGD, DIUNGTHUOC)
CREATE OR REPLACE PROCEDURE sp_BS_Update_BENHNHAN(
	p_MA_BN IN VARCHAR2,
	p_TIENSUBENH IN NVARCHAR2,
	p_TIENSUBENHGD IN NVARCHAR2,
	p_DIUNGTHUOC IN NVARCHAR2
)
AS
BEGIN
	UPDATE BENH_NHAN
	SET TIEN_SU_BENH = p_TIENSUBENH,
		TIEN_SU_BENH_GD = p_TIENSUBENHGD,
		DI_UNG_THUOC = p_DIUNGTHUOC
	WHERE MA_BN = p_MA_BN;

	IF SQL%ROWCOUNT = 0 THEN
		RETURN;
	END IF;

	COMMIT;
END sp_BS_Update_BENHNHAN;
/

-- BACSI INSERT DONTHUOC(TENTHUOC, LIEUDUNG)
CREATE OR REPLACE PROCEDURE sp_BS_Insert_DONTHUOC(
	p_MA_HSBA IN VARCHAR2,
	p_TENTHUOC IN NVARCHAR2,
	p_LIEUDUNG IN NVARCHAR2
)
AS
BEGIN
	INSERT INTO DON_THUOC (MA_HSBA, NGAY_DT, TEN_THUOC, LIEU_DUNG)
	VALUES (p_MA_HSBA, TRUNC(SYSDATE), p_TENTHUOC, p_LIEUDUNG);

	COMMIT;
END sp_BS_Insert_DONTHUOC;
/

-- BACSI UPDATE DONTHUOC(TENTHUOC, LIEUDUNG) + tu dong cap nhat NGAY_DT theo luc sua
CREATE OR REPLACE PROCEDURE sp_BS_Update_DONTHUOC(
	p_MA_HSBA IN VARCHAR2,
	p_NGAY_DT_CU IN DATE,
	p_TENTHUOC_CU IN NVARCHAR2,
	p_TENTHUOC_MOI IN NVARCHAR2,
	p_LIEUDUNG IN NVARCHAR2
)
AS
BEGIN
	UPDATE DON_THUOC
	SET NGAY_DT = TRUNC(SYSDATE),
		TEN_THUOC = p_TENTHUOC_MOI,
		LIEU_DUNG = p_LIEUDUNG
	WHERE MA_HSBA = p_MA_HSBA
	  AND NGAY_DT = p_NGAY_DT_CU
	  AND TEN_THUOC = p_TENTHUOC_CU;

	IF SQL%ROWCOUNT = 0 THEN
		RETURN;
	END IF;

	COMMIT;
END sp_BS_Update_DONTHUOC;
/

-- BACSI DELETE DONTHUOC(TENTHUOC, LIEUDUNG)
CREATE OR REPLACE PROCEDURE sp_BS_Delete_DONTHUOC(
	p_MA_HSBA IN VARCHAR2,
	p_NGAY_DT IN DATE,
	p_TENTHUOC IN NVARCHAR2,
	p_LIEUDUNG IN NVARCHAR2
)
AS
BEGIN
	DELETE FROM DON_THUOC
	WHERE MA_HSBA = p_MA_HSBA
	  AND NGAY_DT = p_NGAY_DT
	  AND TEN_THUOC = p_TENTHUOC
	  AND NVL(LIEU_DUNG, 'NULL') = NVL(p_LIEUDUNG, 'NULL');

	IF SQL%ROWCOUNT = 0 THEN
		RETURN;
	END IF;

	COMMIT;
END sp_BS_Delete_DONTHUOC;
/

--BÁC SĨ CHỈ XEM CÁC HSBA MÀ MÌNH ĐIỀU TRỊ
CREATE OR REPLACE PROCEDURE sp_BS_Select_HSBA (
    p_cursor OUT SYS_REFCURSOR
) AUTHID CURRENT_USER
AS
BEGIN
    OPEN p_cursor FOR
    SELECT *
    FROM ADMIN_PH2.HSBA;
END;
/

--BÁC SĨ CẬP NHẬT CÁC TRƯỜNG (CHẨNĐOÁN, ĐIỀUTRỊ, KẾTLUẬN) TRÊN HSBA MÀ MÌNH ĐIỀU TRỊ
CREATE OR REPLACE PROCEDURE sp_BS_Update_HSBA (
    p_ma_hsba   IN VARCHAR2,
    p_chan_doan IN VARCHAR2,
    p_dieu_tri  IN VARCHAR2,
    p_ket_luan  IN VARCHAR2
)
AS
BEGIN
    UPDATE ADMIN_PH2.HSBA
    SET CHAN_DOAN = p_chan_doan,
        DIEU_TRI  = p_dieu_tri,
        KET_LUAN  = p_ket_luan
    WHERE MA_HSBA = p_ma_hsba;
    
    COMMIT;
END;
/

--BÁC SĨ THÊM DÒNG TRÊN HSBA_DV
CREATE OR REPLACE PROCEDURE sp_BS_Insert_HSBADV (
    p_ma_hsba IN VARCHAR2,
    p_loai_dv IN VARCHAR2,
    p_ngay_dv IN DATE,
    p_ma_ktv  IN VARCHAR2
) AUTHID CURRENT_USER
AS
BEGIN
    INSERT INTO ADMIN_PH2.HSBA_DV (MA_HSBA, LOAI_DV, NGAY_DV, MA_KTV, KET_QUA)
    VALUES (p_ma_hsba, p_loai_dv, p_ngay_dv, p_ma_ktv, NULL);
    
    COMMIT;
END;
/

--BÁC SĨ XÓA DÒNG TRÊN HSBA_DV
CREATE OR REPLACE PROCEDURE sp_BS_Delete_HSBADV (
    p_ma_hsba IN VARCHAR2,
    p_loai_dv IN VARCHAR2,
    p_ngay_dv IN DATE
) AUTHID CURRENT_USER
AS
BEGIN
    DELETE FROM ADMIN_PH2.HSBA_DV
    WHERE MA_HSBA = p_ma_hsba
      AND LOAI_DV = p_loai_dv
      AND TRUNC(NGAY_DV) = TRUNC(p_ngay_dv); 
      
    COMMIT;
END;
/

--BÁC SĨ XEM HSBA THEO BỆNH NHÂN MÀ MÌNH ĐIỀU TRỊ
CREATE OR REPLACE PROCEDURE sp_BS_Select_HSBA_Theo_BN (
    p_ma_bn   IN VARCHAR2,
    p_cursor  OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
AS
BEGIN
    OPEN p_cursor FOR
        SELECT *
        FROM ADMIN_PH2.HSBA
        WHERE MA_BN = p_ma_bn;
END;
/

--DPV SELECT HSBA
CREATE OR REPLACE PROCEDURE sp_DPV_Select_HSBA
(
    CUR OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
AS
BEGIN
    OPEN CUR FOR
        SELECT * FROM ADMIN_PH2.HSBA;
END;
/

--DPV INSERT HSBA
CREATE OR REPLACE PROCEDURE sp_DPV_Insert_HSBA(
    P_MAHSBA VARCHAR2,
    p_MABN VARCHAR2,
    p_NGAY DATE,
    p_CHANDOAN VARCHAR2,
    p_DIEUTRI VARCHAR2,
    p_MABS VARCHAR2,
    p_MAKHOA VARCHAR2,
    p_KETLUAN VARCHAR2
)
AUTHID CURRENT_USER
AS
BEGIN
    INSERT INTO ADMIN_PH2.HSBA 
    VALUES (p_MAHSBA, p_MABN, p_NGAY, p_CHANDOAN, p_DIEUTRI, p_MABS, p_MAKHOA, p_KETLUAN);
    COMMIT;
END;
/

--DPV UPDATE HSBA (MABS, MAKHOA) 
CREATE OR REPLACE PROCEDURE sp_DPV_Update_HSBA(
    p_MAHSBA VARCHAR2,
    p_MAKHOA VARCHAR2,
    p_MABS VARCHAR2
)
AS
BEGIN
    UPDATE ADMIN_PH2.HSBA
    SET MA_KHOA = p_MAKHOA,
        MA_BS = p_MABS
    WHERE MA_HSBA = p_MAHSBA;
    COMMIT;
END;
/

--DPV SELECT HSBA_DV
CREATE OR REPLACE PROCEDURE sp_DPV_Select_HSBADV(
    CUR OUT SYS_REFCURSOR
)
AUTHID CURRENT_USER
AS
BEGIN
    OPEN CUR FOR
        SELECT * FROM ADMIN_PH2.HSBA_DV;
END;
/

--DPV UPDATE MAKTV TRONG HSBA_DV
CREATE OR REPLACE PROCEDURE sp_DPV_Update_HSBADV(
    p_MAHSBA VARCHAR2,
    p_LOAIDV VARCHAR2,
    p_NGAYDV DATE,
    p_MAKTV VARCHAR2
)
AUTHID CURRENT_USER
AS
BEGIN
    UPDATE ADMIN_PH2.HSBA_DV
    SET MA_KTV = p_MAKTV
    WHERE MA_HSBA = p_MAHSBA
    AND LOAI_DV = p_LOAIDV
    AND TRUNC(NGAY_DV) = TRUNC(p_NGAYDV);
    COMMIT;
END;
/

CREATE OR REPLACE PROCEDURE sp_DPV_Select_BENHNHAN (
    p_cursor OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN p_cursor FOR SELECT * FROM ADMIN_PH2.BENH_NHAN;
END;
/

CREATE OR REPLACE PROCEDURE sp_DPV_Insert_BENHNHAN (
    p_ma_bn           IN VARCHAR2,
    p_ho_ten          IN VARCHAR2,
    p_phai            IN VARCHAR2 DEFAULT NULL,
    p_ngay_sinh       IN DATE DEFAULT NULL,
    p_cccd            IN VARCHAR2 DEFAULT NULL,
    p_que_quan        IN VARCHAR2 DEFAULT NULL,
    p_sdt             IN VARCHAR2 DEFAULT NULL,
    p_chuyen_khoa     IN VARCHAR2 DEFAULT NULL,
    p_so_nha          IN VARCHAR2 DEFAULT NULL,
    p_ten_duong       IN VARCHAR2 DEFAULT NULL,
    p_quan_huyen      IN VARCHAR2 DEFAULT NULL,
    p_tinh_tp         IN VARCHAR2 DEFAULT NULL,
    p_tien_su_benh    IN VARCHAR2 DEFAULT NULL,
    p_tien_su_benh_gd IN VARCHAR2 DEFAULT NULL,
    p_di_ung_thuoc    IN VARCHAR2 DEFAULT NULL
)
IS
    v_count NUMBER;
BEGIN
    IF p_ma_bn IS NULL OR TRIM(p_ma_bn) = '' THEN
        RAISE_APPLICATION_ERROR(-20001, 'MA_BN khong duoc de trong.');
    END IF;

    IF p_ho_ten IS NULL OR TRIM(p_ho_ten) = '' THEN
        RAISE_APPLICATION_ERROR(-20002, 'HO_TEN khong duoc de trong.');
    END IF;

    SELECT COUNT(*) INTO v_count
    FROM ADMIN_PH2.NHAN_VIEN
    WHERE MA_NV = p_ma_bn;

    IF v_count > 0 THEN
        RAISE_APPLICATION_ERROR(-20003, 'MA_BN da ton tai trong NHAN_VIEN.');
    END IF;

    SELECT COUNT(*) INTO v_count
    FROM ADMIN_PH2.BENH_NHAN
    WHERE MA_BN = p_ma_bn;

    IF v_count > 0 THEN
        RAISE_APPLICATION_ERROR(-20004, 'Bệnh nhân đã tồn tại trong hệ thống.');
    END IF;

    INSERT INTO ADMIN_PH2.NHAN_VIEN (
        MA_NV, HO_TEN, PHAI, NGAY_SINH, CCCD, QUE_QUAN, SDT, VAI_TRO, CHUYEN_KHOA
    )
    VALUES (
        p_ma_bn,
        p_ho_ten,
        p_phai,
        p_ngay_sinh,
        p_cccd,
        p_que_quan,
        p_sdt,
        'Bệnh nhân',
        p_chuyen_khoa
    );

    INSERT INTO ADMIN_PH2.BENH_NHAN (
        MA_BN, SO_NHA, TEN_DUONG, QUAN_HUYEN, TINH_TP,
        TIEN_SU_BENH, TIEN_SU_BENH_GD, DI_UNG_THUOC
    )
    VALUES (
        p_ma_bn,
        p_so_nha,
        p_ten_duong,
        p_quan_huyen,
        p_tinh_tp,
        p_tien_su_benh,
        p_tien_su_benh_gd,
        p_di_ung_thuoc
    );

    COMMIT;
END;
/

CREATE OR REPLACE PROCEDURE sp_DPV_Update_BENHNHAN (
    p_ma_bn           IN VARCHAR2,
    p_so_nha          IN VARCHAR2,
    p_ten_duong       IN VARCHAR2,
    p_quan_huyen      IN VARCHAR2,
    p_tinh_tp         IN VARCHAR2,
    p_tien_su_benh    IN VARCHAR2,
    p_tien_su_benh_gd IN VARCHAR2,
    p_di_ung_thuoc    IN VARCHAR2,
    p_ho_ten          IN VARCHAR2 DEFAULT NULL,
    p_phai            IN VARCHAR2 DEFAULT NULL,
    p_ngay_sinh       IN DATE DEFAULT NULL,
    p_cccd            IN VARCHAR2 DEFAULT NULL,
    p_que_quan        IN VARCHAR2 DEFAULT NULL,
    p_sdt             IN VARCHAR2 DEFAULT NULL,
    p_chuyen_khoa     IN VARCHAR2 DEFAULT NULL
)
IS
    v_nv_rows NUMBER := 0;
    v_bn_rows NUMBER := 0;
BEGIN
    UPDATE ADMIN_PH2.NHAN_VIEN
    SET HO_TEN = NVL(p_ho_ten, HO_TEN),
        PHAI = NVL(p_phai, PHAI),
        NGAY_SINH = NVL(p_ngay_sinh, NGAY_SINH),
        CCCD = NVL(p_cccd, CCCD),
        QUE_QUAN = NVL(p_que_quan, QUE_QUAN),
        SDT = NVL(p_sdt, SDT),
        CHUYEN_KHOA = NVL(p_chuyen_khoa, CHUYEN_KHOA)
    WHERE MA_NV = p_ma_bn;

    v_nv_rows := SQL%ROWCOUNT;

    UPDATE ADMIN_PH2.BENH_NHAN
    SET SO_NHA = p_so_nha,
        TEN_DUONG = p_ten_duong,
        QUAN_HUYEN = p_quan_huyen,
        TINH_TP = p_tinh_tp,
        TIEN_SU_BENH = p_tien_su_benh,
        TIEN_SU_BENH_GD = p_tien_su_benh_gd,
        DI_UNG_THUOC = p_di_ung_thuoc
    WHERE MA_BN = p_ma_bn;
    
    v_bn_rows := SQL%ROWCOUNT;
    
    IF v_nv_rows = 0 OR v_bn_rows = 0 THEN
        RAISE_APPLICATION_ERROR(-20002, 'Không tìm thấy bệnh nhân hoặc bạn không có quyền cập nhật bệnh nhân này.');
    ELSE
        COMMIT;
    END IF;
END;
/

-- KTV chỉ xem được các dịch vụ mà mình thực hiện trên HSBA_DV
CREATE OR REPLACE PROCEDURE sp_KTV_Select_HSBADV(
    p_cursor OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN p_cursor FOR SELECT * FROM ADMIN_PH2.VW_KTV_HSBA_DV;
END;
/

-- KTV chỉ được cập nhật KET_QUA trên các dịch vụ mà mình thực hiện 
CREATE OR REPLACE PROCEDURE sp_KTV_Update_HSBADV(
     p_MA_HSBA IN VARCHAR2,
     p_LOAI_DV IN VARCHAR2,
     p_NGAY_DV IN DATE,
     p_KET_QUA IN VARCHAR2
) 
IS
BEGIN
    UPDATE ADMIN_PH2.VW_KTV_HSBA_DV 
    SET KET_QUA = p_KET_QUA
    WHERE MA_HSBA = p_MA_HSBA AND LOAI_DV = p_LOAI_DV AND TRUNC(NGAY_DV) = TRUNC(p_NGAY_DV);
    
    IF SQL%ROWCOUNT = 0 THEN
        RAISE_APPLICATION_ERROR(-20002, 'Không tìm thấy dịch vụ hoặc bạn không được phân công thực hiện dịch vụ này.');
    ELSE
        COMMIT;
    END IF;
END;
/

--XEM THÔNG BÁO
CREATE OR REPLACE PROCEDURE SP_GET_THONGBAO (
    p_cursor OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN p_cursor FOR
        SELECT MA_TB,
               NOI_DUNG,
               NGAY_GIO,
               DIA_DIEM,
               MA_NV
        FROM ADMIN_PH2.THONG_BAO
        ORDER BY NGAY_GIO DESC;
END;
/


-- sp_KTV_Select_AuditLog
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

--=====================
-- sp đưa bảng audit lên UI
CREATE OR REPLACE PROCEDURE SP_GET_AUDIT_FGA (
    p_cursor OUT SYS_REFCURSOR
)
AS
BEGIN
    OPEN p_cursor FOR

        SELECT *
        FROM (

            -- FGA Audit
            SELECT 
                DB_USER AS USERNAME,
                OBJECT_NAME AS OBJECT_NAME,
                POLICY_NAME AS POLICY_NAME,
                TO_CHAR(TIMESTAMP, 'DD/MM/YYYY HH24:MI:SS') AS TIME_FULL,
                STATEMENT_TYPE AS ACTION_TYPE,
                NULL AS OBJECT_TYPE,
                NULL AS RETURN_CODE
            FROM DBA_FGA_AUDIT_TRAIL

            UNION ALL

            -- Standard Audit
            SELECT
                USERNAME AS USERNAME,
                OBJ_NAME AS OBJECT_NAME,
                NULL AS POLICY_NAME,
                TO_CHAR(TIMESTAMP, 'DD/MM/YYYY HH24:MI:SS') AS TIME_FULL,
                ACTION_NAME AS ACTION_TYPE,
                NULL AS OBJECT_TYPE,

                -- ✔ FIX RETURN CODE
                CASE 
                    WHEN RETURNCODE = 0 THEN 'Thành công'
                    ELSE 'Thất bại'
                END AS RETURN_CODE

            FROM DBA_AUDIT_TRAIL
            WHERE OBJ_NAME IN (
                'HSBA', 'HSBA_DV', 'BENH_NHAN', 'NHAN_VIEN',
                'DON_THUOC', 'KHOA', 'THONG_BAO', 'NHAN_VIEN_CHITIET'
            )
        )
        ORDER BY TO_DATE(TIME_FULL, 'DD/MM/YYYY HH24:MI:SS') DESC;

END;
/
-- VARIABLE rc REFCURSOR;
-- BEGIN
--     SP_GET_ALL_AUDIT_POLICIES(:rc);
-- END;
-- /
-- PRINT rc;

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

-- SYNONYM
-- NV
CREATE OR REPLACE PUBLIC SYNONYM sp_NV_Select_NHANVIEN FOR ADMIN_PH2.sp_NV_Select_NHANVIEN;
CREATE OR REPLACE PUBLIC SYNONYM sp_NV_Update_NHANVIEN FOR ADMIN_PH2.sp_NV_Update_NHANVIEN;
-- BN
CREATE OR REPLACE PUBLIC SYNONYM sp_BN_Select_BENHNHAN FOR ADMIN_PH2.sp_BN_Select_BENHNHAN;
CREATE OR REPLACE PUBLIC SYNONYM sp_BN_Update_BENHNHAN FOR ADMIN_PH2.sp_BN_Update_BENHNHAN;

-- DPV
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Select_BENHNHAN FOR ADMIN_PH2.sp_DPV_Select_BENHNHAN;
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Insert_BENHNHAN FOR ADMIN_PH2.sp_DPV_Insert_BENHNHAN;
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Update_BENHNHAN FOR ADMIN_PH2.sp_DPV_Update_BENHNHAN;
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Select_HSBA FOR ADMIN_PH2.sp_DPV_Select_HSBA;
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Insert_HSBA FOR ADMIN_PH2.sp_DPV_Insert_HSBA;
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Update_HSBA FOR ADMIN_PH2.sp_DPV_Update_HSBA;
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Select_HSBADV FOR ADMIN_PH2.sp_DPV_Select_HSBADV;
CREATE OR REPLACE PUBLIC SYNONYM sp_DPV_Update_HSBADV FOR ADMIN_PH2.sp_DPV_Update_HSBADV;

-- BÁC SĨ
CREATE OR REPLACE PUBLIC SYNONYM sp_BS_Select_BENHNHAN FOR ADMIN_PH2.sp_BS_Select_BENHNHAN;
CREATE OR REPLACE PUBLIC SYNONYM sp_BS_Update_BENHNHAN FOR ADMIN_PH2.sp_BS_Update_BENHNHAN;
CREATE OR REPLACE PUBLIC SYNONYM sp_BS_Insert_DONTHUOC FOR ADMIN_PH2.sp_BS_Insert_DONTHUOC;
CREATE OR REPLACE PUBLIC SYNONYM sp_BS_Update_DONTHUOC FOR ADMIN_PH2.sp_BS_Update_DONTHUOC;
CREATE OR REPLACE PUBLIC SYNONYM sp_BS_Delete_DONTHUOC FOR ADMIN_PH2.sp_BS_Delete_DONTHUOC;
CREATE OR REPLACE PUBLIC SYNONYM sp_BS_Select_HSBA FOR ADMIN_PH2.sp_BS_Select_HSBA;
CREATE OR REPLACE PUBLIC SYNONYM sp_BS_Update_HSBA FOR ADMIN_PH2.sp_BS_Update_HSBA;
CREATE OR REPLACE PUBLIC SYNONYM sp_BS_Insert_HSBADV FOR ADMIN_PH2.sp_BS_Insert_HSBADV;
CREATE OR REPLACE PUBLIC SYNONYM sp_BS_Delete_HSBADV FOR ADMIN_PH2.sp_BS_Delete_HSBADV;

-- KTV
CREATE OR REPLACE PUBLIC SYNONYM sp_KTV_Select_HSBADV FOR ADMIN_PH2.sp_KTV_Select_HSBADV;
CREATE OR REPLACE PUBLIC SYNONYM sp_KTV_Update_HSBADV FOR ADMIN_PH2.sp_KTV_Update_HSBADV;
CREATE OR REPLACE PUBLIC SYNONYM sp_KTV_Update_KETQUA FOR ADMIN_PH2.sp_KTV_Update_KETQUA;
CREATE OR REPLACE PUBLIC SYNONYM sp_KTV_Select_AuditLog FOR ADMIN_PH2.sp_KTV_Select_AuditLog;

-- DBA (nếu demo)
CREATE OR REPLACE PUBLIC SYNONYM sp_dba_create_user FOR ADMIN_PH2.sp_dba_create_user;
CREATE OR REPLACE PUBLIC SYNONYM sp_dba_createall_user FOR ADMIN_PH2.sp_dba_createall_user;
CREATE OR REPLACE PUBLIC SYNONYM SP_GET_AUDIT_FG FOR ADMIN_PH2.SP_GET_AUDIT_FG;
CREATE OR REPLACE PUBLIC SYNONYM SP_DISABLE_ALL_AUDIT FOR ADMIN_PH2.SP_DISABLE_ALL_AUDIT;
CREATE OR REPLACE PUBLIC SYNONYM SP_ENABLE_ALL_AUDIT FOR ADMIN_PH2.SP_ENABLE_ALL_AUDIT;

--THÔNG BÁO
CREATE OR REPLACE PUBLIC SYNONYM SP_GET_THONGBAO FOR ADMIN_PH2.SP_GET_THONGBAO;

-- GRANT EXECUTE PROCEDURE

-- NHÂN VIÊN (BS + DPV + KTV)
GRANT EXECUTE ON ADMIN_PH2.sp_NV_Select_NHANVIEN TO RL_BACSI, RL_DIEUPHOIVIEN, RL_KYTHUATVIEN;
GRANT EXECUTE ON ADMIN_PH2.sp_NV_Update_NHANVIEN TO RL_BACSI, RL_DIEUPHOIVIEN, RL_KYTHUATVIEN;

-- BỆNH NHÂN
GRANT EXECUTE ON ADMIN_PH2.sp_BN_Select_BENHNHAN TO RL_BENHNHAN;
GRANT EXECUTE ON ADMIN_PH2.sp_BN_Update_BENHNHAN TO RL_BENHNHAN;

-- ĐIỀU PHỐI VIÊN
GRANT EXECUTE ON ADMIN_PH2.sp_DPV_Select_BENHNHAN TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON ADMIN_PH2.sp_DPV_Insert_BENHNHAN TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON ADMIN_PH2.sp_DPV_Update_BENHNHAN TO RL_DIEUPHOIVIEN;

GRANT EXECUTE ON ADMIN_PH2.sp_DPV_Select_HSBA TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON ADMIN_PH2.sp_DPV_Insert_HSBA TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON ADMIN_PH2.sp_DPV_Update_HSBA TO RL_DIEUPHOIVIEN;

GRANT EXECUTE ON ADMIN_PH2.sp_DPV_Select_HSBADV TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON ADMIN_PH2.sp_DPV_Update_HSBADV TO RL_DIEUPHOIVIEN;
GRANT INSERT ON ADMIN_PH2.HSBA TO RL_DIEUPHOIVIEN;

-- BÁC SĨ
GRANT EXECUTE ON ADMIN_PH2.sp_BS_Select_BENHNHAN TO RL_BACSI;
GRANT EXECUTE ON ADMIN_PH2.sp_BS_Update_BENHNHAN TO RL_BACSI;
GRANT EXECUTE ON ADMIN_PH2.sp_BS_Insert_DONTHUOC TO RL_BACSI;
GRANT EXECUTE ON ADMIN_PH2.sp_BS_Update_DONTHUOC TO RL_BACSI;
GRANT EXECUTE ON ADMIN_PH2.sp_BS_Delete_DONTHUOC TO RL_BACSI;
GRANT EXECUTE ON ADMIN_PH2.sp_BS_Select_HSBA TO RL_BACSI;
GRANT EXECUTE ON ADMIN_PH2.sp_BS_Update_HSBA TO RL_BACSI;
GRANT EXECUTE ON ADMIN_PH2.sp_BS_Insert_HSBADV TO RL_BACSI;
GRANT EXECUTE ON ADMIN_PH2.sp_BS_Delete_HSBADV TO RL_BACSI;
GRANT EXECUTE ON ADMIN_PH2.sp_BS_Select_HSBA_Theo_BN to RL_BACSI;

-- KỸ THUẬT VIÊN
GRANT EXECUTE ON ADMIN_PH2.sp_KTV_Select_HSBADV TO RL_KYTHUATVIEN;
GRANT EXECUTE ON ADMIN_PH2.sp_KTV_Update_HSBADV TO RL_KYTHUATVIEN;
GRANT EXECUTE ON ADMIN_PH2.sp_KTV_Select_AuditLog TO RL_KYTHUATVIEN;
GRANT SELECT, INSERT ON AUDIT_HSBADV_LOG TO RL_KYTHUATVIEN;
GRANT SELECT, UPDATE ON ADMIN_PH2.HSBA_DV TO RL_KYTHUATVIEN;

-- DBA
GRANT EXECUTE ON ADMIN_PH2.sp_dba_create_user TO RL_DIEUPHOIVIEN;
GRANT EXECUTE ON ADMIN_PH2.sp_dba_createall_user TO RL_DIEUPHOIVIEN;

--THÔNG BÁO
GRANT EXECUTE ON ADMIN_PH2.SP_GET_THONGBAO TO RL_BACSI, RL_KYTHUATVIEN, RL_DIEUPHOIVIEN;

