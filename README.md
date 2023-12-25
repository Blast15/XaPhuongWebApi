# Xã Phường Web Api

Đồ án thực tập xây dựng website api sử dụng Restful Api Asp .Net Core 6

## Topic

Xây dựng một API quản lý thông tin về Xã phường. API này sẽ cho phép quản trị viên và người dùng thực hiện các thao tác khác nhau dựa trên vai trò của họ. Các chức năng cụ thể bao gồm:

- Các thông tin cần quản lý bao gồm tên xã phường, dân số, diện tích.
- Quản trị viên (role Admin) có quyền thêm, sửa, xóa và xem thông tin về Xã phường, thêm người dùng mới.
- Người dùng (role User) chỉ được phép gọi api lấy thông tin Xã phường.
- Sử dụng JWT Token để xác thực người dùng và cấp quyền truy cập.

Yêu cầu cụ thể:

- Xây dựng API với các endpoint cần thiết để thực hiện các chức năng quản lý thông tin Xã phường theo yêu cầu trên.
- Cài đặt hệ thống xác thực JWT Token để cấp quyền truy cập và phân quyền dựa trên vai trò của người dùng.
- Thực hiện các phương thức HTTP tương ứng (GET, POST, PUT, DELETE) để thực hiện thêm, sửa, xóa và xem thông tin Xã phường, tuân thủ nguyên tắc RESTful API.
- Tích hợp Swagger để hiển thị thông tin API, cách sử dụng,...

## Package

- Microsoft.AspNetCore.Authentication.JwBearer
- Microsoft.EntityFrameworkCore.SqlServer
- Swashbuckle.AspNetCore
- System.IdentityModel.Tokens.Jwt

## Database
XaPhuongs
```
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Population { get; set; }
        public int Area { get; set; }
```
Users
```
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Role { get; set; }
        [Required]
        public byte[]? PasswordSalt { get; set; }
        [Required]
        public byte[]? PasswordHash { get; set; }
```
## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.
