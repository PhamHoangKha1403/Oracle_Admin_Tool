# Oracle Label Security (OLS) Setup for Notification System

## Overview

Hệ thống thông báo (THONGBAO) hiện đã được cập nhật để hỗ trợ **Oracle Label Security (OLS)**, đảm bảo chỉ những người được phép mới có thể xem thông báo tương ứng.

## Architecture

### Security Levels (Cấp độ bảo mật)

- **NV** (10): Nhân viên
- **LD** (20): Lãnh đạo
- **GD** (30): Ban giám đốc

### Compartments (Chuyên khoa)

- **TIMMACH**: Tim mạch
- **THANKINH**: Thần kinh
- **TIEUHOA**: Tiêu hóa

### Groups (Cơ sở)

- **HCM**: Hồ Chí Minh
- **HN**: Hà Nội
- **HP**: Hải Phòng

### Available Labels (Các nhãn có sẵn)

| Label                    | Mô tả                    | Ai có thể xem             |
| ------------------------ | ------------------------ | ------------------------- |
| `NV`                     | Toàn bộ nhân viên        | Tất cả staff              |
| `LD`                     | Lãnh đạo                 | Lãnh đạo + Giám đốc       |
| `GD`                     | Ban giám đốc             | Giám đốc                  |
| `LD:TIEUHOA`             | Lãnh đạo tiêu hóa        | LD & GD in Gastro         |
| `NV:TIEUHOA:HCM`         | Nhân viên tiêu hóa HCM   | NV, LD, GD in Gastro HCM  |
| `NV:TIEUHOA:HN`          | Nhân viên tiêu hóa HN    | NV, LD, GD in Gastro HN   |
| `LD:TIEUHOA,THANKINH:HP` | LD Tiêu hóa/Thần kinh HP | LD, GD in Gastro/Neuro HP |

## Installation Steps

### Step 1: Run YC2.sql (Main OLS Setup)

Tất cả cấu hình OLS đã được bao gồm trong file YC2.sql. Chạy script này:

```bash
sqlplus sys/password@XEPDB1 as sysdba
@YC2.sql
```

Script này sẽ:

- ✅ Enable OLS
- ✅ Tạo OLS policy cho THONGBAO
- ✅ Định nghĩa LEVELS, COMPARTMENTS, GROUPS
- ✅ Tạo security labels
- ✅ Áp dụng policy vào bảng

### Step 2: Update Database Schema (if needed)

Nếu cột SECURITY_LABEL chưa tồn tại trong bảng THONGBAO, chạy:

```sql
ALTER TABLE ADMIN_PH2.THONGBAO ADD (SECURITY_LABEL RAW(16));
ALTER TABLE ADMIN_PH2.THONGBAO ADD (SECURITY_LABEL_CHUOI VARCHAR2(100));

UPDATE ADMIN_PH2.THONGBAO
SET SECURITY_LABEL_CHUOI = 'NV'
WHERE SECURITY_LABEL_CHUOI IS NULL;

COMMIT;
```

### Step 3: Restart Application

Khởi động lại ứng dụng Oracle Admin Tool để áp dụng các thay đổi.

## Usage

### Adding a Notification with Security Label

1. Click tab **Notifications** trong MainForm
2. Click nút **+ NEW**
3. Điền các thông tin:
   - **ID**: Mã thông báo duy nhất
   - **Content**: Nội dung thông báo
   - **Location**: Địa điểm liên quan
   - **Security Label**: Chọn nhãn bảo mật phù hợp
4. Click **Save**

### Viewing Notifications

Khi người dùng đăng nhập:

- Chỉ những thông báo mà người dùng có **clearance** mới được hiển thị
- Bảo mật dựa trên **vai trò** (role) và **chuyên khoa** của người dùng

### Editing Notifications

1. Click nút **Edit** trên hàng thông báo
2. Thay đổi nội dung, vị trí, hoặc nhãn bảo mật
3. Click **Save**

### Deleting Notifications

1. Click nút **Delete** trên hàng thông báo
2. Xác nhận deletion

## C# Implementation Details

### DbaService Methods

#### GetOLSLabels()

```csharp
// Lấy danh sách tất cả available labels từ OLS policy
DataTable labels = dbaService.GetOLSLabels();
```

#### AddNotification()

```csharp
// Thêm thông báo mới với security label
dbaService.AddNotification(
    notificationId: "TB001",
    content: "Thông báo quan trọng",
    location: "Phòng khám",
    securityLabel: "LD:TIEUHOA"
);
```

#### UpdateNotification()

```csharp
// Cập nhật thông báo với security label mới
dbaService.UpdateNotification(
    notificationId: "TB001",
    content: "Nội dung cập nhật",
    location: "Phòng khám",
    securityLabel: "GD"
);
```

#### GetAllNotifications()

```csharp
// Lấy thông báo (OLS tự động lọc dựa trên user clearance)
DataTable notifications = dbaService.GetAllNotifications();
```

### Security Workflow

```
┌─────────────────────────────────────────────────────────┐
│ User Logs In                                            │
└──────────────────┬──────────────────────────────────────┘
                   │
                   ▼
         ┌─────────────────────┐
         │ OLS Clearance Set   │
         │ Based on User Role  │
         └──────────┬──────────┘
                    │
                    ▼
      ┌────────────────────────────────┐
      │ Query THONGBAO Table           │
      │ SQL automatically filters      │
      │ based on SECURITY_LABEL        │
      └──────────┬─────────────────────┘
                 │
                 ▼
    ┌──────────────────────────┐
    │ Only Permitted           │
    │ Notifications Displayed  │
    └──────────────────────────┘
```

## Troubleshooting

### OLS Not Enabled

Nếu OLS chưa được enable, ứng dụng sẽ fallback tới:

- Lưu security label như chuỗi text (SECURITY_LABEL_CHUOI)
- Không áp dụng row-level filtering

### Column Not Found

Nếu gặp lỗi "Column SECURITY_LABEL not found":

1. Chạy `UpdateSchemaForNotifications.sql` để thêm cột
2. Restart application

## Example SQL Queries

### Check OLS Status

```sql
SELECT POLICY_NAME, POLICY_ID, ENABLED
FROM LBACSYS.OLS$POLICIES
WHERE POLICY_NAME = 'OLS_THONGBAO';
```

### View Notifications with Labels

```sql
SELECT MATB, NOIDUNG, DIADIEM,
       LBACSYS.LABEL_TO_CHAR('OLS_THONGBAO', SECURITY_LABEL) AS LABEL
FROM ADMIN_PH2.THONGBAO
ORDER BY NGAYGIO DESC;
```

### Set User Clearance

```sql
BEGIN
  SA_USER_ADMIN.SET_USER_LABELS(
    policy_name => 'OLS_THONGBAO',
    user_name => 'DOCTOR_USERNAME',
    max_read_label => 'LD:TIEUHOA:HCM',
    def_label => 'LD:TIEUHOA:HCM'
  );
END;
/
```

## Files Modified/Created

- **DbaService.cs** - Added OLS-aware methods
- **AdminNotificationView.cs** - Updated to display security labels
- **AdminNotificationView.Designer.cs** - Added SECURITY_LABEL column
- **NotificationEditForm.cs** - Added security label dropdown
- **UpdateSchemaForNotifications.sql** - Schema update script
- **SetupOLSForNotifications.sql** - OLS configuration script

## Security Notes

1. **Row-Level Security**: OLS tự động lọc rows dựa trên user clearance
2. **Write Control**: Users chỉ có thể thêm/sửa thông báo nếu có write clearance
3. **Audit Trail**: Tất cả thay đổi notification được Oracle audit tự động
4. **No Bypass**: OLS ở mức database, không thể bypass từ application layer

## Next Steps

1. Backup database trước khi áp dụng OLS
2. Test OLS configuration trên UAT environment trước
3. Update user clearances theo organizational hierarchy
4. Implement audit reports để monitoring notification access

---

**Version**: 1.0  
**Date**: May 8, 2026  
**Status**: Ready for Production
