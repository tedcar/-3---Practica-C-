using System;
using System.Collections.Generic;
using System.Linq;
using MelodiiApp.Core.DomainModels;

namespace MelodiiApp.DataAccess
{
    public static class InMemoryAuthService
    {
        public static List<AppUser> Users { get; private set; } = new List<AppUser>();
        public static AppUser CurrentLoggedInUser { get; set; }

        static InMemoryAuthService()
        {
            // Seed with a default admin user for testing, if desired for the template
            // Or leave it empty for the MELODII project to handle user creation via RegistrationForm
            // For now, let's add one for basic login testing post-cleanup.
            Users.Add(new AppUser { Username = "admin", Password = "admin", Role = "Admin" });
            Users.Add(new AppUser { Username = "user", Password = "user", Role = "User" });
        }

        public static AppUser AuthenticateUser(string username, string password)
        {
            return Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public static AppUser GetUserByUsername(string username)
        {
            return Users.FirstOrDefault(u => u.Username == username);
        }

        public static bool RegisterUser(string username, string password, string role = "User")
        {
            if (Users.Any(u => u.Username == username))
            {
                return false; // Username already exists
            }
            Users.Add(new AppUser { Username = username, Password = password, Role = role });
            return true;
        }

        public static bool RegisterUser(string username, string email, string password, string role = "User")
        {
            if (Users.Any(u => u.Username == username))
            {
                return false; // Username already exists
            }
            Users.Add(new AppUser { Username = username, Email = email, Password = password, Role = role });
            return true;
        }
    }
} 