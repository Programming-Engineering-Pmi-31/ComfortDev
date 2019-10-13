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

        public int Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new Exception("Username is empty.");
            }
            else if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Password is empty.");
            }

            var user = database.Users.Find(user => user.Name == username).SingleOrDefault();
            if (user == null)
            {
                throw new Exception("User with current name does not exist.");
            }
            else if (!VerifyHash(password, user.Hash))
            {
                throw new Exception("Invalid password.");
            }

            return user.Id;
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

        private byte[] CreateHash(string password)
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

        private bool VerifyHash(string password, string storedHash)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            }

            byte[] key = HashConverter.PlainTextToBytes(Secrets.MD5Key);
            using (var hmac = new System.Security.Cryptography.HMACMD5(key))
            {
                var computedHash = HashConverter.ToString(hmac.ComputeHash(HashConverter.PlainTextToBytes(password)));
                return storedHash == computedHash;
            }
        }

        private void ValidatePassword(string password)
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
