# JWT Authentication - ASP.NET Core Razor Pages

Đồ án môn học - Xác thực và phân quyền sử dụng JWT (JSON Web Token) với ASP.NET Core Razor Pages.

## Thành viên nhóm

| STT | Họ tên | Mã SV |
|-----|--------|-------|
| 1 | Hà Việt Anh | BCS240005 |
| 2 | Nguyễn Quang Minh | BCS240030 |
| 3 | Đặng Minh Quân | BCS240038 |
| 4 | Ngô Tuấn Tú | BCS240043 |
| 5 | Trịnh Đắc Vũ | BCS240047 |

## Luồng xác thực JWT (3 bước)

| Bước | Mô tả | Code minh họa |
|------|-------|---------------|
| **1. Login** | Client gửi username/password → Server xác thực → trả JWT | `Controllers/AuthController.cs` (API) + `Pages/Account/Login.cshtml.cs` (form) |
| **2. Gửi JWT** | Client gửi token qua `Authorization: Bearer <token>` header hoặc cookie `jwt` | `Program.cs` - `OnMessageReceived` đọc token từ cookie |
| **3. Xác thực** | Server kiểm tra token → 200 (authorized) / 401 (chưa đăng nhập) / 403 (sai role) | `JwtService.VerifyToken()` + `[Authorize(Roles = "admin")]` |

## Những điểm cần chú ý

### 1. JWT_SECRET phải ≥ 256 bits (32 ký tự)
- HS256 yêu cầu key tối thiểu 256 bits
- Key ngắn hơn → lỗi `IDX10720: Unable to create KeyedHashAlgorithm`

### 2. Bảo mật Secret
- Development: lưu trong `appsettings.json` hoặc `appsettings.Development.json`
- Production: dùng **User Secrets** (`dotnet user-secrets`) hoặc **Environment Variables**
- Không commit secret lên GitHub

### 3. JWT_EXPIRES_IN
- Nên để 60 phút (ngắn hạn), không để quá dài
- Token sống càng lâu càng nguy hiểm nếu bị lộ

### 4. Cookie HttpOnly
- Form login set cookie `jwt` với `HttpOnly=true`, `Secure=true`, `SameSite=Strict`
- Chống tấn công XSS đánh cắp token

### 5. Ba trạng thái truy cập Admin
- **Chưa đăng nhập** → ấn Admin → redirect về `/Account/Login` (401)
- **User login** → ấn Admin → **403 Forbidden**
- **Admin login** → ấn Admin → vào được trang dashboard

### 6. Hai cách gửi JWT
- Web form: tự động qua cookie `jwt`
- API: header `Authorization: Bearer <token>`

## Tài khoản test

| Username | Password | Role |
|----------|----------|------|
| `admin` | `admin123` | admin |
| `user` | `user123` | user |

## API Endpoint

```
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "admin123"
}
```

Response thành công (200):
```json
{
  "token": "<JWT>",
  "role": "admin",
  "username": "admin"
}
```

Response thất bại (401):
```json
{
  "message": "Invalid username or password"
}
```

## Công nghệ sử dụng

- **ASP.NET Core 10** (Razor Pages + MVC Controllers)
- **JWT Bearer Authentication**
- **Bootstrap 5** (giao diện)
- **.NET 10.0**
