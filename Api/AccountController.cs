using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using apiforapp.Models;
using apiforapp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiforapp.Api
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly Dictionary<string, string> _passwordResetTokens = new Dictionary<string, string>();
        private readonly ApplicationDbcontext _db;
        private readonly ILogger<AccountController> _logger;
        //private ErrorLogger errorLogger = new ErrorLogger(logFilePath);
        //private static string logFilePath = "..\\wwwroot\\Content\\jsonAlth.txt";

        public AccountController(ILogger<AccountController> logger, ApplicationDbcontext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet("GetAllUser")]
        public IActionResult GetAllUsers()
        {
            var users = _db.Users.ToList();
            if (users.Count == 0)
            {
                return NotFound("Không có user nào trong hệ thống");
            }
            return Ok(users);
        }

        [HttpPost("Login")]
        public IActionResult Login(string username, string password)
        {
            try
            {
                var user = _db.Users.SingleOrDefault(p => p.emailAddress == username.Trim());

                if (user == null || user.password != password.Trim())
                {
                    return Unauthorized("Tên đăng nhập hoặc mật khẩu không chính xác");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                //errorLogger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _user = _db.Users.SingleOrDefault(p=>p.emailAddress == user.emailAddress);
            if (_user == null)
            {
                try
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return Ok("Đăng ký người dùng thành công.");

                }
                catch (Exception ex)
                {
                    //errorLogger.LogError(ex.Message);
                    return StatusCode(500, ex.Message);
                }
            }
            else
            {
                return Conflict("Tên đăng nhập đã tồn tại");
            }
        }
        //[HttpPost("forgot")]
        //public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        //{
        //    // Lấy địa chỉ email từ yêu cầu
        //    var email = request.Email;
        //    var user = _db.Users.SingleOrDefault(p=>p.emailAddress == email);
        //    if (user == null) { return BadRequest(); }

        //    var resetToken = Guid.NewGuid().ToString();

        //    // Lưu mã xác nhận với email (thường lưu vào cơ sở dữ liệu)
        //    _passwordResetTokens[email] = resetToken;

        //    // TODO: Gửi mã xác nhận tới email
        //    await SendResetTokenToEmail(email, resetToken);

        //    return Ok("Mã xác nhận đã được gửi tới email.");
        //}

        //[HttpPost("reset")]
        //public IActionResult ResetPassword([FromBody] ResetPasswordRequest request)
        //{
        //    var email = request.Email;
        //    var token = request.Token;
        //    var newPassword = request.NewPassword;

        //    // Kiểm tra xem mã xác nhận có chính xác không
        //    if (_passwordResetTokens.TryGetValue(email, out var savedToken) && savedToken == token)
        //    {
        //        // TODO: Đặt lại mật khẩu cho email đã xác nhận
        //        // Ví dụ: Lưu mật khẩu mới vào cơ sở dữ liệu

        //        // Xóa mã xác nhận sau khi đã sử dụng
        //        _passwordResetTokens.Remove(email);

        //        return Ok("Mật khẩu đã được đặt lại.");
        //    }

        //    return BadRequest("Mã xác nhận không hợp lệ.");
        //}

        //private async Task SendResetTokenToEmail(string email, string token)
        //{
        //    string address = "phuchieu1213@gmail.com"; //Địa chỉ email của bạn
        //    string password = "nxpthpviosnyutmh"; //Mật khẩu ứng dụng
        //    using (SmtpClient smtpClient = new SmtpClient())
        //    {
        //        smtpClient.Host = "smtp.gmail.com";
        //        smtpClient.Port = 587;
        //        smtpClient.EnableSsl = true;
        //        smtpClient.Credentials = new NetworkCredential(address, password);
        //        smtpClient.Send(new MailMessage(address, email, "FOOTURE MÃ OTP", $"Mã xác nhận của bạn là: {token}"));
        //    };
        //}

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(int idUser, [FromBody] User updatedUser)
        {
            var _user = _db.Users.Find(idUser);

            if (_user == null)
            {
                return NotFound("Người dùng không tồn tại trên hệ thống");
            }

            try
            {
                _user.name = updatedUser.name;
                //_user.emailAddress = updatedUser.emailAddress;
                _user.password = updatedUser.password;
                _user.age = updatedUser.age;
                _user.gender = updatedUser.gender;
                _user.weigh = updatedUser.weigh;
                _user.heigh = updatedUser.heigh;
                _user.avatar = updatedUser.avatar;

                _db.SaveChanges();
                return Ok("Cập nhật người dùng thành công");

            }
            catch (Exception ex)
            {
                //errorLogger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int iduser)
        {
            try
            {
                var _user = _db.Users.Find(iduser);
                if (_user == null)
                {
                    return NotFound("Người dùng không tồn tại");
                }
                _db.Users.Remove(_user);
                _db.SaveChanges();
                return Ok("Xóa người dùng thành công.");
            }
            catch (Exception ex)
            {
                //errorLogger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword(string username,string oldPassword ,string newPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _db.Users.SingleOrDefault(p=>p.emailAddress ==username);
                if (user == null)
                {
                    return NotFound("Người dùng không tồn tại trong hệ thống");
                }
                if (user.password != oldPassword)
                {
                    return BadRequest("Mật khẩu khẩu cũ không chính xác.");
                   
                }
                else
                {
                    user.password = newPassword;
                    _db.SaveChanges();
                    return Ok("Mật khẩu đã được cập nhật thành công.");
                }
                    
            }
            catch (Exception ex)
            {
                //errorLogger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllRole")]
        public IActionResult GetAllRole()
        {
            var roles =_db.Roles.ToList();
            if(roles.Count == 0)
            {
                return BadRequest();
            }
            return Ok(roles);
        }

        [HttpPost("AddRole")]
        public IActionResult AddRole([FromBody] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _db.Roles.Add(role);
                _db.SaveChanges();
                return Ok("Thêm quyền thành công.");
            }
            catch (Exception ex)
            {
                //errorLogger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPut("UpdateRole")]
        public IActionResult UpdateRole([FromBody] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _role = _db.Roles.Find(role.idRole);
            if (_role == null)
            {
                return NotFound("Không tồn tại mã quyền trong hệ thống");
            }
            try
            {
                _role.name = role.name;
                _role.description = role.description;
                _db.SaveChanges();
                return Ok("cập nhật quyền thành công.");

            }
            catch(Exception ex)
            {
                //errorLogger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("DeleteRole")]
        public IActionResult DeleteRole(int idRole) {
            try
            {
                var _role = _db.Roles.Find(idRole);
                if (_role == null)
                {
                    return NotFound("Quyền người dùng không tồn tại");
                }
                _db.Roles.Remove(_role);
                _db.SaveChanges();
                return Ok("Xóa quyền thành công.");
            }
            catch (Exception ex)
            {
                //errorLogger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

    }
}