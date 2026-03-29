import pandas as pd
import random
from faker import Faker
from datetime import datetime, timedelta

fake = Faker("vi_VN")

# ======================
# RANDOM DATETIME CHUẨN
# ======================
def random_datetime(start='-2y', end='now'):
    base = fake.date_time_between(start_date=start, end_date=end)
    return base.replace(
        hour=random.randint(0, 23),
        minute=random.randint(0, 59),
        second=random.randint(0, 59)
    )

def random_datetime_from(base, days_range=5):
    return base + timedelta(
        days=random.randint(0, days_range),
        hours=random.randint(0, 23),
        minutes=random.randint(0, 59),
        seconds=random.randint(0, 59)
    )

def random_birth_datetime(min_age, max_age):
    base = fake.date_of_birth(minimum_age=min_age, maximum_age=max_age)
    return datetime(
        base.year, base.month, base.day,
        random.randint(0, 23),
        random.randint(0, 59),
        random.randint(0, 59)
    )

# ======================
# 1. KHOA
# ======================
khoa = pd.DataFrame([
    {"MA_KHOA": "TH", "TEN_KHOA": "Khoa tiêu hóa"},
    {"MA_KHOA": "TK", "TEN_KHOA": "Khoa thần kinh"},
    {"MA_KHOA": "TM", "TEN_KHOA": "Khoa tim mạch"}
])

# ======================
# 2. NHAN_VIEN
# ======================
nhan_vien = []

# DPV
for i in range(1, 21):
    nhan_vien.append({
        "MA_NV": f"DP{i:03}",
        "HO_TEN": fake.name(),
        "PHAI": random.choice(["Nam", "Nữ"]),
        "NGAY_SINH": random_birth_datetime(25, 40),
        "CCCD": str(100000000000 + i),
        "QUE_QUAN": fake.city(),
        "SDT": fake.phone_number(),
        "VAI_TRO": "Điều phối viên",
        "CHUYEN_KHOA": None
    })

# Bác sĩ
for i in range(1, 101):
    nhan_vien.append({
        "MA_NV": f"BS{i:03}",
        "HO_TEN": fake.name(),
        "PHAI": random.choice(["Nam", "Nữ"]),
        "NGAY_SINH": random_birth_datetime(30, 60),
        "CCCD": str(200000000000 + i),
        "QUE_QUAN": fake.city(),
        "SDT": fake.phone_number(),
        "VAI_TRO": "Bác sĩ",
        "CHUYEN_KHOA": random.choice(["TH", "TK", "TM"])
    })

# KTV
for i in range(1, 51):
    nhan_vien.append({
        "MA_NV": f"KTV{i:03}",
        "HO_TEN": fake.name(),
        "PHAI": random.choice(["Nam", "Nữ"]),
        "NGAY_SINH": random_birth_datetime(25, 50),
        "CCCD": str(300000000000 + i),
        "QUE_QUAN": fake.city(),
        "SDT": fake.phone_number(),
        "VAI_TRO": "Kỹ thuật viên",
        "CHUYEN_KHOA": random.choice(["TH", "TK", "TM"])
    })

# Bệnh nhân
for i in range(1, 100001):
    nhan_vien.append({
        "MA_NV": f"BN{i:06}",
        "HO_TEN": fake.name(),
        "PHAI": random.choice(["Nam", "Nữ"]),
        "NGAY_SINH": random_birth_datetime(1, 90),
        "CCCD": str(400000000000 + i),
        "QUE_QUAN": fake.city(),
        "SDT": fake.phone_number(),
        "VAI_TRO": "Bệnh nhân",
        "CHUYEN_KHOA": random.choice(["TH", "TK", "TM"])
    })

nhan_vien_df = pd.DataFrame(nhan_vien)

# ======================
# 3. BENH_NHAN
# ======================
bn_df = nhan_vien_df[nhan_vien_df["VAI_TRO"] == "Bệnh nhân"].copy()

benh_nhan_df = pd.DataFrame({
    "MA_BN": bn_df["MA_NV"],
    "SO_NHA": [fake.building_number() for _ in range(len(bn_df))],
    "TEN_DUONG": [fake.street_name() for _ in range(len(bn_df))],
    "QUAN_HUYEN": [fake.city() for _ in range(len(bn_df))],
    "TINH_TP": ["TPHCM"] * len(bn_df),
    "TIEN_SU_BENH": ["Không có"] * len(bn_df),
    "TIEN_SU_BENH_GD": ["Không có"] * len(bn_df),
    "DI_UNG_THUOC": ["Không dị ứng"] * len(bn_df)
})

# ======================
# 4. HSBA
# ======================
hsba_list = []
hsba_id = 1

bs_df = nhan_vien_df[nhan_vien_df["VAI_TRO"] == "Bác sĩ"]

for bn in benh_nhan_df["MA_BN"]:
    for _ in range(random.randint(1, 3)):
        bs = bs_df.sample(1).iloc[0]
        ngay = random_datetime()

        hsba_list.append({
            "MA_HSBA": f"HS{hsba_id:07}",
            "MA_BN": bn,
            "NGAY": ngay,
            "CHAN_DOAN": "Khám tổng quát",
            "DIEU_TRI": "Theo dõi",
            "MA_BS": bs["MA_NV"],
            "MA_KHOA": bs["CHUYEN_KHOA"],
            "KET_LUAN": "Ổn định"
        })

        hsba_id += 1

hsba_df = pd.DataFrame(hsba_list)

# ======================
# 5. DON_THUOC
# ======================
thuoc_list = ["Paracetamol", "Amoxicillin", "Aspirin"]

don_thuoc_list = []
used_dt_keys = set()

for _, row in hsba_df.iterrows():
    so_thuoc = random.randint(1, len(thuoc_list))

    ds_thuoc = random.sample(thuoc_list, k=so_thuoc)

    for thuoc in ds_thuoc:
        while True:
            ngay_dt = random_datetime_from(row["NGAY"], 0)

            key = (row["MA_HSBA"], ngay_dt, thuoc)

            if key not in used_dt_keys:
                used_dt_keys.add(key)
                break

        don_thuoc_list.append({
            "MA_HSBA": row["MA_HSBA"],
            "NGAY_DT": ngay_dt,
            "TEN_THUOC": thuoc,
            "LIEU_DUNG": f"{random.randint(1,2)} viên/ngày"
        })

don_thuoc_df = pd.DataFrame(don_thuoc_list)

# ======================
# 6. HSBA_DV (NO DUPLICATE)
# ======================
# Lọc danh sách kỹ thuật viên
ktv_list = nhan_vien_df[nhan_vien_df["VAI_TRO"] == "Kỹ thuật viên"]["MA_NV"].tolist()

dv_list = ["Xét nghiệm máu", "Chụp X-quang", "MRI"]

used_keys = set()
hsba_dv_list = []

for _, row in hsba_df.iterrows():
    for _ in range(random.randint(0, 3)):
        while True:
            loai = random.choice(dv_list)
            ngay = random_datetime_from(row["NGAY"], 5)
            key = (row["MA_HSBA"], loai, ngay)

            if key not in used_keys:
                used_keys.add(key)
                break

        hsba_dv_list.append({
            "MA_HSBA": row["MA_HSBA"],
            "LOAI_DV": loai,
            "NGAY_DV": ngay,
            "MA_KTV": random.choice(ktv_list),  
            "KET_QUA": "Bình thường"
        })

hsba_dv_df = pd.DataFrame(hsba_dv_list)

# ======================
# 7. EXPORT SQL
# ======================
def export_to_sql(df, table_name, file_name):
    with open(file_name, "w", encoding="utf-8") as f:
        for _, row in df.iterrows():
            values = []
            for val in row:
                if pd.isna(val):
                    values.append("NULL")
                elif isinstance(val, datetime):
                    values.append(
                        f"TO_DATE('{val.strftime('%Y-%m-%d %H:%M:%S')}', 'YYYY-MM-DD HH24:MI:SS')"
                    )
                else:
                    values.append("'{}'".format(str(val).replace("'", "''")))

            f.write(f"INSERT INTO {table_name} VALUES ({', '.join(values)});\n")

# ======================
# 7. THONG_BAO
# ======================
# ======================
# LEVEL MAP
# ======================
level_map = {
    "NV": 1,
    "LD": 2,
    "GD": 3
}

khoa_list = ["TIEUHOA", "THANKINH", "TIMMACH"]
coso_list = ["HCM", "HN", "HP"]

dia_diem_list = [
    "Phòng họp A",
    "Phòng họp B",
    "Hội trường",
    "Phòng 101",
    "Phòng 202"
]

# ======================
# FAKE LABEL -> NUMBER
# ======================
def generate_label(level, khoa=None, coso=None):
    base = level * 1000
    k = khoa_list.index(khoa)*100 if khoa else 0
    c = coso_list.index(coso)*10 if coso else 0
    return base + k + c

# ======================
# NV IDS (FIX LỖI)
# ======================
nv_ids = nhan_vien_df[
    nhan_vien_df["VAI_TRO"].isin(["Bác sĩ", "Kỹ thuật viên", "Điều phối viên"])
]["MA_NV"].tolist()

khoa_map = {
    "TIEUHOA": "Khoa tiêu hóa",
    "THANKINH": "Khoa thần kinh",
    "TIMMACH": "Khoa tim mạch"
}

thong_bao_list = []

for i in range(1, 1001):
    type_tb = random.choice(["ALL", "GD", "KHOA", "NV"])

    if type_tb == "ALL":
        label = generate_label(1)
        noi_dung = "Thông báo toàn viện"

    elif type_tb == "GD":
        label = generate_label(3)
        noi_dung = "Họp ban giám đốc"

    elif type_tb == "KHOA":
        khoa = random.choice(khoa_list)
        label = generate_label(2, khoa)
        noi_dung = f"Họp lãnh đạo {khoa_map[khoa]}"

    else:
        khoa = random.choice(khoa_list)
        coso = random.choice(coso_list)
        label = generate_label(1, khoa, coso)
        noi_dung = f"Lịch trực {khoa_map[khoa]} tại {coso}"

    thong_bao_list.append({
        "MA_TB": f"TB{i:04}",
        "NOI_DUNG": noi_dung,
        "NGAY_GIO": random_datetime(),
        "DIA_DIEM": random.choice(dia_diem_list),
        "MA_NV": random.choice(nv_ids),
        "SECURITY_LABEL": label
    })

thong_bao_df = pd.DataFrame(thong_bao_list)

# ======================
# EXPORT
# ======================
export_to_sql(nhan_vien_df, "NHAN_VIEN", "NHAN_VIEN.sql")
export_to_sql(benh_nhan_df, "BENH_NHAN", "BENH_NHAN.sql")
export_to_sql(hsba_df, "HSBA", "hsba.sql")
export_to_sql(don_thuoc_df, "DON_THUOC", "DON_THUOC.sql")
export_to_sql(hsba_dv_df, "HSBA_DV", "HSBA_DV.sql")
export_to_sql(thong_bao_df, "THONG_BAO", "THONG_BAO.sql")

print("DONE - ALL DATETIME RANDOMIZED")