using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace lab2.Models {
    public class User {
        public string Email { set; get; }
        public string Password { set; get; }
    }
    public static class AuthModel {
        static List<User> users = new List<User>();
        async public static Task createUser(string email, string password) {
            await Task.Run(() => {
                var exists = users.Exists(user => user.Email == email);
                if (exists) throw new System.Exception("User exists");

                users.Add(new User { Email = email, Password = password });
            });
        }

        async public static Task<User> getUser(string email) {
            var result = await Task.Run(() => {
                var user = users.Find(user => user.Email == email);
                if (user == null) throw new System.Exception("User doesn't exist");

                return user;
            });
            return result;
        }

        async public static Task updateUser(string oldEmail, string newEmail) {
            await Task.Run(() => {
                var user = users.Find(user => user.Email == oldEmail);
                if (user == null) throw new System.Exception("User doesn't exist");

                // users.Remove(user);
                user.Email = newEmail;

                // users.Add(user);
            });
        }

        async public static Task removeUser(string email) {
            await Task.Run(() => {
                var user = users.Find(user => user.Email == email);
                if (user == null) throw new System.Exception("User doesn't exist");

                users.Remove(user);
            });
        }

        async public static Task<List<User>> allUsers() {
            var list = await Task.Run(() => {
                return users;
            });

            return list;
        }
    }
}