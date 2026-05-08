--Mã nhóm: DBSec05
--22127172 - Phạm Hoàng Kha
--23127145 - Khưu Ngọc Ý Vy
--23127254 - Nguyễn Thị Như Quỳnh
--23127428 - Nguyễn Thị Trà My
--23127524 - Hình Diễm Xuân

--Chạy bằng sys
CONNECT SYS/123 AS SYSDBA;
ALTER SESSION SET CONTAINER = XEPDB1;
EXEC LBACSYS.CONFIGURE_OLS;
EXEC LBACSYS.OLS_ENFORCEMENT.ENABLE_OLS;
SELECT * FROM DBA_OLS_STATUS;

-- Tắt policy trước (nếu có)
BEGIN
  SA_SYSDBA.DROP_POLICY(
    policy_name => 'OLS_THONGBAO',
    drop_column => TRUE
  );
END;
/

--GRANT
GRANT SELECT ON ADMIN_PH2.THONG_BAO TO RL_BACSI;
GRANT SELECT ON ADMIN_PH2.THONG_BAO TO RL_KYTHUATVIEN;
GRANT SELECT ON ADMIN_PH2.THONG_BAO TO RL_DIEUPHOIVIEN;


--Tạo chính sách bảo mật
BEGIN
    SA_SYSDBA.CREATE_POLICY(
        policy_name => 'OLS_THONGBAO',
        column_name => 'SECURITY_LABEL'
    );
END;
/

GRANT OLS_THONGBAO_DBA TO ADMIN_PH2;
SET ROLE OLS_THONGBAO_DBA;

--Cấp quyền FULL cho ADMIN_PH2
BEGIN
  LBACSYS.SA_USER_ADMIN.SET_USER_PRIVS (
    policy_name => 'OLS_THONGBAO',
    user_name   => 'ADMIN_PH2',
    privileges  => 'FULL'
  );
END;
/

--LEVEL
BEGIN
    SA_COMPONENTS.CREATE_LEVEL('OLS_THONGBAO', 10, 'NV', 'Nhân viên');
    SA_COMPONENTS.CREATE_LEVEL('OLS_THONGBAO', 20, 'LD', 'Lãnh đạo');
    SA_COMPONENTS.CREATE_LEVEL('OLS_THONGBAO', 30, 'GD', 'Ban giám đốc');
END;
/

--COMPARTMENT
BEGIN
    SA_COMPONENTS.CREATE_COMPARTMENT('OLS_THONGBAO', 1, 'TIMMACH', 'Tim mạch');
    SA_COMPONENTS.CREATE_COMPARTMENT('OLS_THONGBAO', 2, 'THANKINH', 'Thần kinh');
    SA_COMPONENTS.CREATE_COMPARTMENT('OLS_THONGBAO', 3, 'TIEUHOA', 'Tiêu hóa');
END;
/

--GROUP
BEGIN
  SA_COMPONENTS.CREATE_GROUP('OLS_THONGBAO', 1, 'HCM', 'Hồ Chí Minh');
  SA_COMPONENTS.CREATE_GROUP('OLS_THONGBAO', 2, 'HN', 'Hà Nội');
  SA_COMPONENTS.CREATE_GROUP('OLS_THONGBAO', 3, 'HP', 'Hải Phòng');
END;
/

--LABEL (DATA)
BEGIN
  LBACSYS.SA_LABEL_ADMIN.CREATE_LABEL('OLS_THONGBAO', 1000, 'NV'); --T1
  LBACSYS.SA_LABEL_ADMIN.CREATE_LABEL('OLS_THONGBAO', 1100, 'GD'); --T2
  LBACSYS.SA_LABEL_ADMIN.CREATE_LABEL('OLS_THONGBAO', 1200, 'LD'); --T3
  LBACSYS.SA_LABEL_ADMIN.CREATE_LABEL('OLS_THONGBAO', 1300, 'LD:TIEUHOA'); --T4
  LBACSYS.SA_LABEL_ADMIN.CREATE_LABEL('OLS_THONGBAO', 1400, 'NV:TIEUHOA:HCM'); --T5
  LBACSYS.SA_LABEL_ADMIN.CREATE_LABEL('OLS_THONGBAO', 1500, 'NV:TIEUHOA:HN'); --T6
  LBACSYS.SA_LABEL_ADMIN.CREATE_LABEL('OLS_THONGBAO', 1600, 'LD:TIEUHOA,THANKINH:HP'); --T7
END;
/

--Áp dụng chính sách vào bảng
BEGIN
  SA_POLICY_ADMIN.APPLY_TABLE_POLICY (
    policy_name     => 'OLS_THONGBAO',
    schema_name     => 'ADMIN_PH2', 
    table_name      => 'THONG_BAO',
    table_options   => 'LABEL_DEFAULT, READ_CONTROL, WRITE_CONTROL'
  );
END;
/

--bật policy
BEGIN
  SA_POLICY_ADMIN.ENABLE_POLICY('OLS_THONGBAO');
END;
/

--áp dụng policy và đồng bộ
UPDATE THONG_BAO 
SET SECURITY_LABEL = CHAR_TO_LABEL('OLS_THONGBAO', SECURITY_LABEL_CHUOI);
COMMIT;

-- CLEARANCE
BEGIN
  FOR r IN (
    SELECT nv.MA_NV,
           nv.CHUYEN_KHOA,
           ct.CHUCVU,
           ct.COSO
    FROM ADMIN_PH2.NHAN_VIEN nv
    JOIN ADMIN_PH2.NHANVIEN_CHITIET ct
      ON nv.MA_NV = ct.MA_NV
    WHERE nv.VAI_TRO <> 'Bệnh nhân'
  ) LOOP

    DECLARE
      v_level VARCHAR2(10);
      v_comp  VARCHAR2(50);
      v_group VARCHAR2(10);
      v_label VARCHAR2(100);
      v_min   VARCHAR2(100);
    BEGIN

      -- LEVEL
      IF r.CHUCVU = 'Giám đốc' THEN
        v_level := 'GD';
      ELSIF r.CHUCVU = 'Lãnh đạo' THEN
        v_level := 'LD';
      ELSE
        v_level := 'NV';
      END IF;

      -- COMPARTMENT
      IF r.CHUYEN_KHOA = 'TH' THEN
        v_comp := 'TIEUHOA';
      ELSIF r.CHUYEN_KHOA = 'TK' THEN
        v_comp := 'THANKINH';
      ELSIF r.CHUYEN_KHOA = 'TM' THEN
        v_comp := 'TIMMACH';
      ELSE
        CONTINUE;
      END IF;

      -- GROUP
      IF r.COSO = 'Hồ Chí Minh' THEN
        v_group := 'HCM';
      ELSIF r.COSO = 'Hà Nội' THEN
        v_group := 'HN';
      ELSIF r.COSO = 'Hải Phòng' THEN
        v_group := 'HP';
      ELSE
        CONTINUE;
      END IF;

      -- BUILD LABEL
    IF v_level = 'GD' THEN
        v_label := 'GD';
    
    ELSIF r.CHUCVU = 'Lãnh đạo' THEN
        -- u7
        v_label := 'LD';
    
    ELSIF v_level = 'LD' THEN
        -- u2, u3, u6
        v_label := 'LD:' || v_comp || ':' || v_group;
    
    ELSE
        -- u4, u5, u8
        v_label := 'NV:' || v_comp || ':' || v_group;
    END IF;
    
    -- min_write_label luôn đúng chuẩn
    v_min := 'NV';

      -- GÁN (có chống lỗi)
      BEGIN
        SA_USER_ADMIN.SET_USER_LABELS(
            policy_name => 'OLS_THONGBAO',
            user_name => r.MA_NV,
            max_read_label => v_label,
            max_write_label => v_label,
            min_write_label => v_min,
            def_label => v_label,
            row_label => v_label
        );
      EXCEPTION
        WHEN OTHERS THEN
          DBMS_OUTPUT.PUT_LINE('Error user ' || r.MA_NV || ': ' || SQLERRM);
      END;

    END;

  END LOOP;
END;
/

COMMIT;
