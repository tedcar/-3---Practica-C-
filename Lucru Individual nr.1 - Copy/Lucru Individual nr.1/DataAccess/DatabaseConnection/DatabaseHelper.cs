using System;
using System.Configuration;

namespace MelodiiApp.DataAccess.DatabaseConnection
{
    /// <summary>
    /// Furnizează utilitare pentru conexiunea la baza de date,
    /// inclusiv gestionarea stringului de conexiune.
    /// </summary>
    public static class DatabaseHelper
    {
        private static string _connectionString;

        /// <summary>
        /// Stringul de conexiune la baza de date SQL Server.
        /// Este citit din fișierul App.config.
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    try
                    {
                        // Numele stringului de conexiune din App.config
                        // Acesta trebuie adăugat manual în App.config de către utilizator.
                        _connectionString = ConfigurationManager.ConnectionStrings["MelodiiAppConnection"]?.ConnectionString;

                        if (string.IsNullOrEmpty(_connectionString))
                        {
                            // Acest mesaj este mai mult pentru debugging în timpul dezvoltării.
                            // Într-o aplicație reală, gestionarea ar putea fi diferită.
                            Console.WriteLine("EROARE: Stringul de conexiune 'MelodiiAppConnection' nu este configurat în App.config sau este gol.");
                            // Opțional, aruncă o excepție sau returnează un string de conexiune default/invalid
                            // throw new ConfigurationErrorsException("Stringul de conexiune 'MelodiiAppConnection' nu este configurat în App.config.");
                        }
                    }
                    catch (ConfigurationErrorsException ex)
                    {
                        // Loghează eroarea sau gestioneaz-o corespunzător
                        Console.WriteLine($"EROARE la citirea configuratiei: {ex.Message}");
                        // Poți decide să arunci excepția mai departe sau să gestionezi altfel
                        throw; 
                    }
                }
                return _connectionString;
            }
            // Permite setarea stringului de conexiune programatic, util pentru teste sau configurări alternative.
            // Totuși, citirea din App.config este metoda principală.
            set => _connectionString = value; 
        }

        /// <summary>
        /// Metodă helper pentru a ghida utilizatorul să configureze App.config.
        /// Aceasta nu este menită să fie apelată direct în fluxul normal al aplicației,
        /// ci servește ca un memento în cod.
        /// </summary>
        public static string GetAppConfigExample()
        {
            return @"
            Adăugați următoarea secțiune în fișierul App.config, în interiorul tag-ului <configuration>:
            <connectionStrings>
              <add name=""MelodiiAppConnection"" 
                   connectionString=""Server=NUME_SERVER_SQL;Database=MelodiiConcurs;Trusted_Connection=True;"" 
                   providerName=""System.Data.SqlClient""/>
            </connectionStrings>

            Instrucțiuni:
            1. Deschideți fișierul App.config din proiectul dumneavoastră.
            2. Dacă nu există secțiunea <connectionStrings>, adăugați-o direct sub <configuration>.
            3. Adăugați elementul <add ... /> ca mai sus.
            4. Înlocuiți 'NUME_SERVER_SQL' cu numele instanței dumneavoastră SQL Server (ex: (localdb)\MSSQLLocalDB, SQLEXPRESS, sau numele complet al serverului).
            5. 'Database=MelodiiConcurs' specifică numele bazei de date. Asigurați-vă că se potrivește cu cea creată.
            6. 'Trusted_Connection=True;' este pentru autentificarea Windows. Dacă folosiți autentificare SQL Server, va trebui să modificați stringul pentru a include User ID și Password.
               Exemplu pentru autentificare SQL: connectionString=""Server=NUME_SERVER_SQL;Database=MelodiiConcurs;User ID=your_user;Password=your_password;""
            7. Asigurați-vă că proiectul are o referință la System.Configuration (click dreapta pe References -> Add Reference -> Assemblies -> Framework -> bifați System.Configuration).
            ";
        }
    }
} 