-- ============================================================
-- ATBM Hospital Management - Schema
-- CSC12001 - An toàn và Bảo mật Dữ liệu trong HTTT
-- ============================================================
-- Chạy script này bằng tài khoản SYS AS SYSDBA trên Oracle.
-- ============================================================

-- Chuyển sang PDB trước khi tạo bảng/role/user
ALTER SESSION SET CONTAINER = XEPDB1;

-- ============================================================
-- 1. TẠO BẢNG
-- ============================================================

-- Bảng NHANVIEN
CREATE TABLE NHANVIEN (
    MANV          VARCHAR2(20)  PRIMARY KEY,
    HOTEN         NVARCHAR2(100) NOT NULL,
    PHAI          NVARCHAR2(5),
    NGAYSINH      DATE,
    CMND          VARCHAR2(20),
    QUEQUAN       NVARCHAR2(200),
    SODT          VARCHAR2(15),
    VAITRO        NVARCHAR2(30),   -- 'Dieu phoi vien', 'Bac si', 'Ky thuat vien'
    CHUYENKHOA    NVARCHAR2(50)
);

-- Bảng BENHNHAN
CREATE TABLE BENHNHAN (
    MABN          VARCHAR2(20)  PRIMARY KEY,
    TENBN         NVARCHAR2(100) NOT NULL,
    PHAI          NVARCHAR2(5),
    NGAYSINH      DATE,
    CCCD          VARCHAR2(20),
    SONHA         NVARCHAR2(50),
    TENDUONG      NVARCHAR2(100),
    QUANHUYEN     NVARCHAR2(100),
    TINHTP        NVARCHAR2(100),
    TIENSUBENH    NVARCHAR2(500),
    TIENSUBENHGD  NVARCHAR2(500),
    DIUNGTUOC     NVARCHAR2(500)
);

-- Bảng HSBA (Hồ sơ bệnh án)
CREATE TABLE HSBA (
    MAHSBA        VARCHAR2(20)  PRIMARY KEY,
    MABN          VARCHAR2(20)  REFERENCES BENHNHAN(MABN),
    NGAY          DATE,
    CHANDOAN      NVARCHAR2(500),
    DIEUTRI       NVARCHAR2(500),
    MABS          VARCHAR2(20)  REFERENCES NHANVIEN(MANV),
    MAKHOA        NVARCHAR2(50),
    KETLUAN       NVARCHAR2(500)
);

-- Bảng HSBA_DV (Dịch vụ hỗ trợ chẩn đoán)
CREATE TABLE HSBA_DV (
    MAHSBA        VARCHAR2(20)  REFERENCES HSBA(MAHSBA),
    LOAIDV        NVARCHAR2(100),
    NGAYDV        DATE,
    MAKTV         VARCHAR2(20)  REFERENCES NHANVIEN(MANV),
    KETQUA        NVARCHAR2(500),
    PRIMARY KEY (MAHSBA, LOAIDV, NGAYDV)
);

-- Bảng DONTHUOC
CREATE TABLE DONTHUOC (
    MAHSBA        VARCHAR2(20)  REFERENCES HSBA(MAHSBA),
    NGAYDT        DATE,
    TENTHUOC      NVARCHAR2(100),
    LIEUDUNG      NVARCHAR2(200),
    PRIMARY KEY (MAHSBA, NGAYDT, TENTHUOC)
);

-- Bảng THONGBAO (dùng cho OLS ở yêu cầu 2)
CREATE TABLE THONGBAO (
    MATB          VARCHAR2(20)  PRIMARY KEY,
    NOIDUNG       NVARCHAR2(1000),
    NGAYGIO       TIMESTAMP,
    DIADIEM       NVARCHAR2(200)
);

-- ============================================================
-- 2. TẠO ROLE
-- ============================================================

CREATE ROLE RL_BACSI;
CREATE ROLE RL_DIEUPHOIVIEN;
CREATE ROLE RL_KYTHUATVIEN;
CREATE ROLE RL_BENHNHAN;

-- Cấp quyền cơ bản cho mỗi role (sẽ bổ sung chi tiết ở Phân hệ 2)

-- Điều phối viên: xem, thêm, sửa BENHNHAN; tạo HSBA; cập nhật MAKHOA, MABS trên HSBA
GRANT SELECT, INSERT, UPDATE ON BENHNHAN TO RL_DIEUPHOIVIEN;
GRANT SELECT, INSERT ON HSBA TO RL_DIEUPHOIVIEN;
GRANT UPDATE (MAKHOA, MABS) ON HSBA TO RL_DIEUPHOIVIEN;
GRANT UPDATE (MAKTV) ON HSBA_DV TO RL_DIEUPHOIVIEN;
GRANT SELECT ON HSBA_DV TO RL_DIEUPHOIVIEN;

-- Bác sĩ: xem HSBA mình điều trị, cập nhật CHANDOAN/DIEUTRI/KETLUAN,
--          thêm/xóa HSBA_DV, thêm/xóa/sửa DONTHUOC
GRANT SELECT ON HSBA TO RL_BACSI;
GRANT UPDATE (CHANDOAN, DIEUTRI, KETLUAN) ON HSBA TO RL_BACSI;
GRANT SELECT, INSERT, DELETE ON HSBA_DV TO RL_BACSI;
GRANT SELECT ON BENHNHAN TO RL_BACSI;
GRANT UPDATE (TIENSUBENH, TIENSUBENHGD, DIUNGTUOC) ON BENHNHAN TO RL_BACSI;
GRANT SELECT, INSERT, UPDATE, DELETE ON DONTHUOC TO RL_BACSI;

-- Kỹ thuật viên: xem HSBA_DV mình thực hiện, cập nhật KETQUA
GRANT SELECT ON HSBA_DV TO RL_KYTHUATVIEN;
GRANT UPDATE (KETQUA) ON HSBA_DV TO RL_KYTHUATVIEN;

-- Bệnh nhân: xem thông tin chính mình
GRANT SELECT ON BENHNHAN TO RL_BENHNHAN;
GRANT UPDATE (SONHA, TENDUONG, QUANHUYEN, TINHTP) ON BENHNHAN TO RL_BENHNHAN;

-- ============================================================
-- Xong schema. Chạy data.sql tiếp để insert dữ liệu demo.
-- ============================================================
