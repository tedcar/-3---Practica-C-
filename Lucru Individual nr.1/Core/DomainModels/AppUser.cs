using System;

namespace MelodiiApp.Core.DomainModels // Updated namespace
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // In a real app, this would be hashed
        public string Role { get; set; } // e.g., "User", "Admin"
        public int? IntervievatID { get; set; } // Legătură cu entitatea Intervievat; null pentru admin sau neasociat
        public string Email { get; set; }

        private static int nextId = 1;

        public AppUser()
        {
            Id = nextId++;
        }
    }
} 