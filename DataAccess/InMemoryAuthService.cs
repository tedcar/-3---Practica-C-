using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess.DatabaseConnection;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography; // Pentru hashing
using System.Text; // Pentru hashing

namespace MelodiiApp.DataAccess
{
    /// <summary>
    /// Serviciu pentru autentificarea și gestionarea utilizatorilor.
    /// Denumit anterior InMemoryAuthService, dar interacționează cu baza de date.
    /// </summary>
    public static class AuthService 
    {
        /// <summary>
        /// Utilizatorul autentificat curent în aplicație.
        /// </summary>
        public static AppUser CurrentLoggedInUser { get; set; }

        /// <summary>
        /// Generează un hash pentru parolă.
        /// ATENȚIE: Aceasta este o implementare simplificată SHA256 și NU ESTE SIGURĂ pentru parole reale
        /// fără utilizarea de salt și un număr adecvat de iterații. 
        /// Într-o aplicație de producție, utilizați o bibliotecă robustă precum BCrypt.Net sau ASP.NET Core Identity PasswordHasher.
        /// </summary>
        /// <param name="password">Parola în clar.</param>
        /// <returns>Hash-ul parolei.</returns>
        private static string HashPassword(string password)
        {
            // Exemplu comentat pentru o abordare mai robustă (necesită .NET 6+ sau RNG separat pentru salt):
            // byte[] salt = new byte[16];
            // RandomNumberGenerator.Fill(salt); 
            // var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            // byte[] hash = pbkdf2.GetBytes(20);
            // byte[] hashBytes = new byte[36];
            // Array.Copy(salt, 0, hashBytes, 0, 16);
            // Array.Copy(hash, 0, hashBytes, 16, 20);
            // return Convert.ToBase64String(hashBytes);

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Verifică dacă parola furnizată corespunde hash-ului stocat.
        /// Trebuie să corespundă metodei `HashPassword`.
        /// </summary>
        /// <param name="password">Parola în clar.</param>
        /// <param name="storedHash">Hash-ul stocat.</param>
        /// <returns>True dacă parola este validă, altfel false.</returns>
        private static bool VerifyPassword(string password, string storedHash)
        { 
            string hashOfInput = HashPassword(password);
            return StringComparer.OrdinalIgnoreCase.Compare(hashOfInput, storedHash) == 0;
        }

        /// <summary>
        /// Autentifică un utilizator pe baza numelui de utilizator și a parolei.
        /// Setează `CurrentLoggedInUser` la succes.
        /// </summary>
        /// <param name="username">Numele de utilizator.</param>
        /// <param name="password">Parola.</param>
        /// <returns>Obiectul `AppUser` dacă autentificarea reușește, altfel null.</returns>
        public static AppUser AuthenticateUser(string username, string password)
        {
            string query = "SELECT Id, Username, PasswordHash, Email, Role, IsTemporary FROM AppUsers WHERE Username = @Username";
            SqlParameter paramUsername = new SqlParameter("@Username", SqlDbType.NVarChar, 100) { Value = username };

            try
            {
                DataTable dt = DatabaseHelper.GetDataTable(query, CommandType.Text, paramUsername);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    string storedHash = row["PasswordHash"].ToString();
                    
                    if (VerifyPassword(password, storedHash))
                    {
                        CurrentLoggedInUser = new AppUser
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            Username = row["Username"].ToString(),
                            Password = null, // Nu stocați parola în clar în obiectul de sesiune
                            Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                            Role = row["Role"].ToString()
                            // Câmpul IsTemporary nu face parte direct din modelul AppUser pentru sesiune, dar este util în BD
                        };
                        return CurrentLoggedInUser;
                    }
                }
            }
            catch (Exception ex)
            {
                // Într-o aplicație reală, aici s-ar loga eroarea folosind un framework de logging.
                Console.WriteLine($"Eroare de autentificare: {ex.Message}");
            }
            return null;
        }

        /// <summary>
        /// Obține un utilizator din baza de date pe baza numelui de utilizator.
        /// </summary>
        /// <param name="username">Numele de utilizator.</param>
        /// <returns>Obiectul `AppUser` dacă este găsit, altfel null.</returns>
        public static AppUser GetUserByUsername(string username)
        {
            string query = "SELECT Id, Username, Email, Role FROM AppUsers WHERE Username = @Username";
            SqlParameter paramUsername = new SqlParameter("@Username", SqlDbType.NVarChar, 100) { Value = username };

            try
            {
                DataTable dt = DatabaseHelper.GetDataTable(query, CommandType.Text, paramUsername);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new AppUser
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Username = row["Username"].ToString(),
                        Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                        Role = row["Role"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la obținerea utilizatorului după username: {ex.Message}");
            }
            return null;
        }
        
        /// <summary>
        /// Obține un utilizator din baza de date pe baza ID-ului.
        /// </summary>
        /// <param name="id">ID-ul utilizatorului.</param>
        /// <returns>Obiectul `AppUser` dacă este găsit, altfel null.</returns>
        public static AppUser GetUserById(int id)
        {
            string query = "SELECT Id, Username, Email, Role FROM AppUsers WHERE Id = @Id";
            SqlParameter paramId = new SqlParameter("@Id", SqlDbType.Int) { Value = id };

            try
            {
                DataTable dt = DatabaseHelper.GetDataTable(query, CommandType.Text, paramId);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new AppUser
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Username = row["Username"].ToString(),
                        Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                        Role = row["Role"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la obținerea utilizatorului după ID: {ex.Message}");
            }
            return null;
        }

        /// <summary>
        /// Înregistrează un utilizator nou în sistem.
        /// </summary>
        /// <param name="username">Numele de utilizator.</param>
        /// <param name="email">Adresa de email.</param>
        /// <param name="password">Parola.</param>
        /// <param name="role">Rolul utilizatorului (implicit "User").</param>
        /// <param name="saveCredentials">Indicä dacă datele de autentificare sunt permanente (true) sau temporare (false).</param>
        /// <returns>True dacă înregistrarea reușește, altfel false.</returns>
        public static bool RegisterUser(string username, string email, string password, string role = "User", bool saveCredentials = true)
        {
            // Verifică dacă numele de utilizator există deja.
            if (GetUserByUsername(username) != null)
            {
                return false; 
            }

            string hashedPassword = HashPassword(password);
            string query = @"
                INSERT INTO AppUsers (Username, PasswordHash, Email, Role, IsTemporary, DataInregistrare)
                VALUES (@Username, @PasswordHash, @Email, @Role, @IsTemporary, GETDATE());
                SELECT SCOPE_IDENTITY();"; // Pentru a obține ID-ul noului utilizator

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", SqlDbType.NVarChar, 100) { Value = username },
                new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 255) { Value = hashedPassword },
                new SqlParameter("@Email", SqlDbType.NVarChar, 150) { Value = (object)email ?? DBNull.Value },
                new SqlParameter("@Role", SqlDbType.NVarChar, 50) { Value = role },
                new SqlParameter("@IsTemporary", SqlDbType.Bit) { Value = !saveCredentials }
            };

            try
            {
                object newIdObj = DatabaseHelper.ExecuteScalar(query, CommandType.Text, parameters);
                if (newIdObj != null && newIdObj != DBNull.Value)
                {
                    // Opțional, dacă este necesar ID-ul noului utilizator imediat:
                    // int newUserId = Convert.ToInt32(newIdObj);
                    return true;
                }
                return false;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) // Încălcare constrângere unică (cheie primară/unică)
            {
                // Specific pentru SQL Server: 2627 (unique constraint), 2601 (unique index)
                Console.WriteLine($"Eroare de înregistrare: Numele de utilizator sau emailul există deja. {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare de înregistrare: {ex.Message}");
                return false;
            }
        }
    }
} 