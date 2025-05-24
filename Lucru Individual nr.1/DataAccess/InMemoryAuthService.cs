using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography; // Added for SHA256
using System.Text; // Added for Encoding
using MelodiiApp.Core.DomainModels;

namespace MelodiiApp.DataAccess
{
    public static class InMemoryAuthService
    {
        public static List<AppUser> Users { get; private set; } = new List<AppUser>();
        public static AppUser CurrentLoggedInUser { get; set; }
        private static readonly string Salt = "YOUR_FIXED_SALT_FOR_MELODII_APP"; // Fixed salt

        static InMemoryAuthService()
        {
            // Seed users with hashed passwords
            Users.Add(new AppUser { Username = "admin", Password = HashPassword("Admin123!"), Role = "Admin", Email = "admin@example.com" });
            Users.Add(new AppUser { Username = "user", Password = HashPassword("User123!"), Role = "User", Email = "user@example.com" });
        }

        private static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return null; // Or throw an ArgumentNullException

            using (SHA256 sha256 = SHA256.Create())
            {
                string saltedPassword = password + Salt; 
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static AppUser AuthenticateUser(string username, string password)
        {
            string hashedPassword = HashPassword(password);
            return Users.FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);
        }

        public static AppUser GetUserByUsername(string username)
        {
            return Users.FirstOrDefault(u => u.Username == username);
        }

        public static bool RegisterUser(string username, string password, string role = "User") // This overload might be deprecated or used internally
        {
            if (Users.Any(u => u.Username == username))
            {
                return false; // Username already exists
            }
            // Assuming this version might not have email, but still needs password hashing.
            // If it's truly deprecated, it could be removed. For now, hash the password.
            string hashedPassword = HashPassword(password);
            Users.Add(new AppUser { Username = username, Password = hashedPassword, Role = role });
            return true;
        }

        public static bool RegisterUser(string username, string email, string password, string role = "User")
        {
            if (Users.Any(u => u.Username == username))
            {
                return false; // Username already exists
            }
            string hashedPassword = HashPassword(password); // Hash the password
            Users.Add(new AppUser { Username = username, Email = email, Password = hashedPassword, Role = role });
            return true;
        }
    }
}