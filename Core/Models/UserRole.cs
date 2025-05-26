namespace MelodiiApp.Core.Models
{
    /// <summary>
    /// Definește rolurile posibile pentru un utilizator în aplicație.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Rol de Administrator, cu permisiuni extinse.
        /// </summary>
        Admin,

        /// <summary>
        /// Rol de Utilizator standard, cu permisiuni limitate.
        /// </summary>
        User
        // Ulterior, s-ar putea adăuga roluri precum Guest (Invitat) sau altele, dacă este necesar.
    }
} 