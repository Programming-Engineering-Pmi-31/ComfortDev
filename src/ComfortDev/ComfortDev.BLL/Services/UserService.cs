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
        public UserService()
        {
            database = new ComfortDevUnitOfWork();
        }

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

        public User Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
            {
                // use custom exceptions
                throw new Exception("Password is required");
            }
            if (database.Users.Find(u => u.Name == user.Name).Count() > 0)
            {
                throw new Exception("Username " + user.Name + " is already taken");
            }

            byte[] hash = CreateHash(password);
            user.Hash = HashConverter.ToString(hash);

            database.Users.Create(user);
            database.Save();

            return user;
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
    }
}
