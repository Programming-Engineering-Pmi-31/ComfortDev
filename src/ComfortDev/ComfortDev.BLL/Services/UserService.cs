using System;
using System.Collections.Generic;
using System.Text;
using ComfortDev.BLL.Interfaces;
using ComfortDev.Common.Entities;
using ComfortDev.DAL.UnitsOfWork;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;
using ComfortDev.Common;

namespace ComfortDev.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly ComfortDevUnitOfWork database;
        public UserService(ComfortDevUnitOfWork uow)
        {
            database = uow;
        }
        public UserService(): this(new ComfortDevUnitOfWork()) { }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = database.Users.Find(user => user.Name == username).SingleOrDefault();

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyHash(password, user.Hash))
                return null;

            // authentication successful
            return user;
        }

        public void Create(string username, string password)
        {
            ValidateUsername(username);
            ValidatePassword(password);

            var user = new User { Name = username };
            user.Hash = HashConverter.ToString(CreateHash(password));

            database.Users.Create(user);
            database.Save();
        }

        public IEnumerable<User> GetAll()
        {
            return database.Users.GetAll();
        }

        public User GetById(int id)
        {
            return database.Users.Get(id);
        }

        private static byte[] CreateHash(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            }

            string key = Secrets.MD5Key;
            byte[] keyByte = HashConverter.PlainTextToBytes(key);

            byte[] hash;
            using (var hmac = new System.Security.Cryptography.HMACMD5(keyByte))
            {
                hash = hmac.ComputeHash(HashConverter.PlainTextToBytes(password));
            }
            return hash;
        }

        private static bool VerifyHash(string password, string storedHash)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            }

            byte[] storedHashByte = HashConverter.ToBytes(storedHash);
            if (storedHashByte.Length != 16)
            {
                throw new ArgumentException("Invalid length of password hash (16 bytes expected).", "storedHashByte");
            }

            string key = Secrets.MD5Key;
            byte[] keyByte = HashConverter.PlainTextToBytes(key);

            using (var hmac = new System.Security.Cryptography.HMACMD5(keyByte))
            {
                var computedHash = hmac.ComputeHash(HashConverter.PlainTextToBytes(password));
                for (int i = 0; i < computedHash.Length; ++i)
                {
                    if (computedHash[i] != storedHashByte[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password is required.");
            }
            else if (password.Length < 8)
            {
                throw new Exception("Password must be a minimum of 8 characters in length.");
            }
            else if (password.Length > 20)
            {
                throw new Exception("Password can be a maximum of 20 characters in length.");
            }

            if (!(password.Count(x => char.IsLetter(x)) > 0))
            {
                throw new Exception("Password must have at least one letter.");
            }

            if (!(password.Count(x => char.IsDigit(x)) > 0))
            {
                throw new Exception("Password must have at least one digit.");
            }
        }

        private void ValidateUsername(string username)
        {
            if (database.Users.Find(user => user.Name == username).Count() > 0)
            {
                throw new Exception("Username \"" + username + "\" is already taken.");
            }
            else if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("Username is required.");
            }
            else if (username.Length < 3)
            {
                throw new Exception("Username must be a minimum of 3 characters in length.");
            }
            else if (username.Length > 15)
            {
                throw new Exception("Username can be a maximum of 15 characters in length.");
            }
            else if (username.Count(x => char.IsDigit(x) || char.IsLetter(x)) < username.Length)
            {
                throw new Exception("Username must consist only of letters and numbers.");
            }
            else if (username.Count(x => char.IsDigit(x)) == username.Length)
            {
                throw new Exception("Username cannot consist of digits only.");
            }
        }

        public void Delete(int id)
        {
            var user = database.Users.Get(id);
            if (user != null)
            {
                database.Users.Delete(id);
                database.Save();
            }
        }
    }
}
