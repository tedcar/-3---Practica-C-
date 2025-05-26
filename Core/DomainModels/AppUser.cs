using System;

namespace MelodiiApp.Core.DomainModels
{
    /// <summary>
    /// Reprezintă un utilizator al aplicației.
    /// </summary>
    public class AppUser
    {
        /// <summary>
        /// Identificatorul unic al utilizatorului (generat de baza de date).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Numele de utilizator.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Parola utilizatorului. 
        /// ATENȚIE: Într-o aplicație reală, aici s-ar stoca hash-ul parolei, nu parola în clar.
        /// Logica de hashing este gestionată în AuthService.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Rolul utilizatorului în aplicație (de ex., "User", "Admin").
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Adresa de email a utilizatorului.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Constructor implicit.
        /// </summary>
        public AppUser()
        {
        }
    }
} 