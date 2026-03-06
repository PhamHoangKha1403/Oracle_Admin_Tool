-- ============================================================
-- ATBM Hospital Management - Demo Data
-- CSC12001 - An toàn và Bảo mật Dữ liệu trong HTTT
-- ============================================================
-- Chạy script này SAU schema.sql, bằng tài khoản SYS AS SYSDBA.
-- Script sẽ INSERT data + tạo Oracle user + gán role.
-- ============================================================

ALTER SESSION SET CONTAINER = XEPDB1;

-- ============================================================
-- 1. INSERT DỮ LIỆU NHÂN VIÊN
-- ============================================================

-- Điều phối viên
INSERT INTO NHANVIEN VALUES ('NV001', N'Nguyễn Văn An',    N'Nam',  TO_DATE('1985-03-15','YYYY-MM-DD'), '079085001001', N'Hồ Chí Minh', '0901000001', N'Dieu phoi vien', N'Khoa Tiêu hóa');
INSERT INTO NHANVIEN VALUES ('NV002', N'Trần Thị Bích',    N'Nữ',   TO_DATE('1990-07-22','YYYY-MM-DD'), '079090002002', N'Hà Nội',      '0901000002', N'Dieu phoi vien', N'Khoa Tim mạch');

-- Bác sĩ
INSERT INTO NHANVIEN VALUES ('NV003', N'Lê Hoàng Cường',   N'Nam',  TO_DATE('1980-01-10','YYYY-MM-DD'), '079080003003', N'Đà Nẵng',     '0901000003', N'Bac si',          N'Khoa Tiêu hóa');
INSERT INTO NHANVIEN VALUES ('NV004', N'Phạm Minh Dũng',   N'Nam',  TO_DATE('1982-11-05','YYYY-MM-DD'), '079082004004', N'Hải Phòng',   '0901000004', N'Bac si',          N'Khoa Thần kinh');

-- Kỹ thuật viên
INSERT INTO NHANVIEN VALUES ('NV005', N'Võ Thị Hoa',       N'Nữ',   TO_DATE('1993-06-18','YYYY-MM-DD'), '079093005005', N'Cần Thơ',     '0901000005', N'Ky thuat vien',   N'Khoa Tiêu hóa');

COMMIT;

-- ============================================================
-- 2. INSERT DỮ LIỆU BỆNH NHÂN
-- ============================================================

INSERT INTO BENHNHAN VALUES ('BN001', N'Hoàng Anh Tuấn',  N'Nam',  TO_DATE('1975-04-20','YYYY-MM-DD'), '079075006006', N'12',  N'Nguyễn Trãi',    N'Quận 5',    N'Hồ Chí Minh', N'Viêm dạ dày',      N'Cao huyết áp',   NULL);
INSERT INTO BENHNHAN VALUES ('BN002', N'Ngô Bảo Ngọc',    N'Nữ',   TO_DATE('1988-09-12','YYYY-MM-DD'), '079088007007', N'45',  N'Lê Lợi',         N'Quận 1',    N'Hồ Chí Minh', NULL,                NULL,              N'Penicillin');
INSERT INTO BENHNHAN VALUES ('BN003', N'Đặng Văn Khoa',   N'Nam',  TO_DATE('1995-12-01','YYYY-MM-DD'), '079095008008', N'78',  N'Trần Hưng Đạo',  N'Hoàn Kiếm', N'Hà Nội',      N'Hen suyễn',        NULL,              NULL);
INSERT INTO BENHNHAN VALUES ('BN004', N'Lý Thị Mai',      N'Nữ',   TO_DATE('2000-02-28','YYYY-MM-DD'), '079000009009', N'100', N'Hai Bà Trưng',   N'Quận 3',    N'Hồ Chí Minh', NULL,                N'Tiểu đường',     NULL);
INSERT INTO BENHNHAN VALUES ('BN005', N'Bùi Quốc Hùng',   N'Nam',  TO_DATE('1970-08-08','YYYY-MM-DD'), '079070010010', N'5',   N'Pasteur',        N'Quận 1',    N'Hồ Chí Minh', N'Thoái hóa cột sống', N'Tim mạch',     N'Aspirin');

COMMIT;

-- ============================================================
-- 3. INSERT HỒ SƠ BỆNH ÁN
-- ============================================================

INSERT INTO HSBA VALUES ('HSBA001', 'BN001', TO_DATE('2026-01-10','YYYY-MM-DD'), N'Viêm dạ dày cấp',    N'Dùng thuốc ức chế axit',   'NV003', N'Khoa Tiêu hóa',  N'Theo dõi thêm');
INSERT INTO HSBA VALUES ('HSBA002', 'BN002', TO_DATE('2026-01-15','YYYY-MM-DD'), N'Đau đầu kéo dài',    N'Chụp MRI',                  'NV004', N'Khoa Thần kinh', NULL);
INSERT INTO HSBA VALUES ('HSBA003', 'BN003', TO_DATE('2026-02-01','YYYY-MM-DD'), N'Cơn hen cấp',        N'Khí dung + thuốc giãn phế quản', 'NV003', N'Khoa Tiêu hóa', N'Xuất viện');

COMMIT;

-- ============================================================
-- 4. INSERT DỊCH VỤ HỖ TRỢ CHẨN ĐOÁN
-- ============================================================

INSERT INTO HSBA_DV VALUES ('HSBA001', N'Nội soi dạ dày',    TO_DATE('2026-01-11','YYYY-MM-DD'), 'NV005', N'Viêm niêm mạc dạ dày');
INSERT INTO HSBA_DV VALUES ('HSBA002', N'Chụp MRI não',      TO_DATE('2026-01-16','YYYY-MM-DD'), 'NV005', NULL);
INSERT INTO HSBA_DV VALUES ('HSBA003', N'Xét nghiệm máu',   TO_DATE('2026-02-02','YYYY-MM-DD'), 'NV005', N'Chỉ số bình thường');

COMMIT;

-- ============================================================
-- 5. INSERT ĐƠN THUỐC
-- ============================================================

INSERT INTO DONTHUOC VALUES ('HSBA001', TO_DATE('2026-01-10','YYYY-MM-DD'), N'Omeprazole',     N'20mg x 2 lần/ngày');
INSERT INTO DONTHUOC VALUES ('HSBA001', TO_DATE('2026-01-10','YYYY-MM-DD'), N'Domperidone',    N'10mg x 3 lần/ngày');
INSERT INTO DONTHUOC VALUES ('HSBA003', TO_DATE('2026-02-01','YYYY-MM-DD'), N'Salbutamol',     N'Khí dung 2.5mg khi cần');

COMMIT;

-- ============================================================
-- 6. TẠO ORACLE USER CHO TỪNG NHÂN VIÊN & BỆNH NHÂN
-- ============================================================
-- Mỗi user có password mặc định = username (đổi sau khi đăng nhập lần đầu)
-- Lưu ý: Chạy phần này bằng SYS AS SYSDBA

-- Nhân viên
CREATE USER NV001 IDENTIFIED BY NV001 DEFAULT TABLESPACE USERS QUOTA UNLIMITED ON USERS;
CREATE USER NV002 IDENTIFIED BY NV002 DEFAULT TABLESPACE USERS QUOTA UNLIMITED ON USERS;
CREATE USER NV003 IDENTIFIED BY NV003 DEFAULT TABLESPACE USERS QUOTA UNLIMITED ON USERS;
CREATE USER NV004 IDENTIFIED BY NV004 DEFAULT TABLESPACE USERS QUOTA UNLIMITED ON USERS;
CREATE USER NV005 IDENTIFIED BY NV005 DEFAULT TABLESPACE USERS QUOTA UNLIMITED ON USERS;

-- Bệnh nhân
CREATE USER BN001 IDENTIFIED BY BN001 DEFAULT TABLESPACE USERS QUOTA UNLIMITED ON USERS;
CREATE USER BN002 IDENTIFIED BY BN002 DEFAULT TABLESPACE USERS QUOTA UNLIMITED ON USERS;
CREATE USER BN003 IDENTIFIED BY BN003 DEFAULT TABLESPACE USERS QUOTA UNLIMITED ON USERS;
CREATE USER BN004 IDENTIFIED BY BN004 DEFAULT TABLESPACE USERS QUOTA UNLIMITED ON USERS;
CREATE USER BN005 IDENTIFIED BY BN005 DEFAULT TABLESPACE USERS QUOTA UNLIMITED ON USERS;

-- Cấp quyền CREATE SESSION cho tất cả
GRANT CREATE SESSION TO NV001, NV002, NV003, NV004, NV005;
GRANT CREATE SESSION TO BN001, BN002, BN003, BN004, BN005;

-- ============================================================
-- 7. GÁN ROLE CHO USER
-- ============================================================

-- Điều phối viên
GRANT RL_DIEUPHOIVIEN TO NV001;
GRANT RL_DIEUPHOIVIEN TO NV002;

-- Bác sĩ
GRANT RL_BACSI TO NV003;
GRANT RL_BACSI TO NV004;

-- Kỹ thuật viên
GRANT RL_KYTHUATVIEN TO NV005;

-- Bệnh nhân
GRANT RL_BENHNHAN TO BN001;
GRANT RL_BENHNHAN TO BN002;
GRANT RL_BENHNHAN TO BN003;
GRANT RL_BENHNHAN TO BN004;
GRANT RL_BENHNHAN TO BN005;

-- ============================================================
-- Xong! Dữ liệu demo đã sẵn sàng.
-- ============================================================
