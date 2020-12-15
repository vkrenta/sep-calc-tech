using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using lab2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab2.Controllers {
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase {
        [HttpPost("register")]
        async public Task<object> Register([FromBody] User user) {
            try {
                Console.WriteLine("AAA");
                await AuthModel.createUser(user.Email, user.Password);
                return Created("/", new { message = "User created!" });
            } catch (System.Exception e) {
                if (e.Message == "User exists")
                    return Unauthorized(new { error = $"User with email {user.Email} already exists" });
                throw e;
            }

        }

        public class LoginBody {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("login")]
        async public Task<object> Login([FromBody] LoginBody body) {
            try {
                var user = await AuthModel.getUser(body.Email);
                if (user.Password != body.Password) return Unauthorized(new { error = "Invalid password" });
                return Ok(user);
            } catch (System.Exception e) {
                if (e.Message == "User doesn't exist") return Unauthorized(new { error = "Invalid email" });
                throw e;
            }

        }

        public class UpdateBody {
            public string OldEmail { get; set; }
            public string NewEmail { get; set; }
        }

        [HttpPatch]
        [Route("update")]
        async public Task<object> updateUser([FromBody] UpdateBody body) {
            try {
                Console.WriteLine(JsonSerializer.Serialize(body));
                await AuthModel.updateUser(body.OldEmail, body.NewEmail);
                return Ok(new { message = "User email changed" });
            } catch (System.Exception) {
                return NotFound(new { error = "User not found" });
            }

        }

        [HttpDelete]
        [Route("delete/email={email}")]
        async public Task<object> deleteUser([FromRoute] string email) {
            try {
                await AuthModel.removeUser(email);
                return Ok(new { message = "User removed" });
            } catch (System.Exception) {
                return NotFound(new { error = "User not found" });
            }

        }

        [HttpGet]
        [Route("all")]
        async public Task<object> allUsers() {
            var users = await AuthModel.allUsers();
            return users;
        }

        [HttpGet]
        [Route("redirectable")]
        public object redirect() {
            return Redirect("https://www.google.com");
        }

    }
}