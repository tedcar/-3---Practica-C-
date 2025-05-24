using MelodiiApp.Core.DomainModels;
// using MelodiiApp.DataAccess.DatabaseConnection; // No longer needed for in-memory
using System;
// using System.Data.SqlClient; // No longer needed for in-memory
using System.Collections.Generic; // Required for List<T>
using System.Linq; // Required for LINQ operations like Any() and FirstOrDefault()

namespace MelodiiApp.DataAccess
{
    /// <summary>
    /// Repository pentru gestionarea operațiunilor CRUD pentru entitatea Melodie (In-Memory).
    /// </summary>
    public class MelodieRepository
    {
        // Static list to hold data in memory for the lifetime of the application
        // In a real multi-user or persistent scenario, this would be a database.
        private static List<Melodie> _melodii = new List<Melodie>();
        private static int _nextMelodieId = 1; // Simple ID generator for in-memory

        /// <summary>
        /// Adaugă o nouă melodie în colecția in-memory.
        /// </summary>
        /// <param name="melodie">Obiectul Melodie care urmează să fie adăugat.</param>
        /// <returns>True dacă adăugarea a reușit, false în caz contrar.</returns>
        public bool AdaugaMelodie(Melodie melodie)
        {
            if (melodie == null)
            {
                // Console.WriteLine("EROARE: Obiectul Melodie este null în AdaugaMelodie (In-Memory).");
                return false;
            }
            if (string.IsNullOrWhiteSpace(melodie.Titlu) || string.IsNullOrWhiteSpace(melodie.Artist))
            {
                 // Console.WriteLine("EROARE: Titlul și Artistul melodiei nu pot fi goale (In-Memory).");
                 return false;
            }
            // Check for duplicates (optional, based on requirements - e.g., unique Title and Artist)
            // if (_melodii.Any(m => m.Titlu.Equals(melodie.Titlu, StringComparison.OrdinalIgnoreCase) && 
            //                      m.Artist.Equals(melodie.Artist, StringComparison.OrdinalIgnoreCase)))
            // {
            //     Console.WriteLine("EROARE: O melodie cu același titlu și artist există deja (In-Memory).");
            //     return false;
            // }

            melodie.MelodieID = _nextMelodieId++; // Assign a new ID
            // PunctajTotal is already initialized to 0 in the Melodie model constructor
            _melodii.Add(melodie);
            // Console.WriteLine($"INFO: Melodie adăugată in-memory: ID {melodie.MelodieID}, Titlu: {melodie.Titlu}");
            return true;
        }

        /// <summary>
        /// Returnează toate melodiile din colecția in-memory.
        /// </summary>
        /// <returns>O listă de melodii.</returns>
        public List<Melodie> GetAllMelodii()
        {
            // Return a copy to prevent external modification of the internal list
            return new List<Melodie>(_melodii);
        }

        /// <summary>
        /// Găsește o melodie după ID în colecția in-memory.
        /// </summary>
        /// <param name="id">ID-ul melodiei căutate.</param>
        /// <returns>Obiectul Melodie dacă este găsit, altfel null.</returns>
        public Melodie GetMelodieById(int id)
        {
            return _melodii.FirstOrDefault(m => m.MelodieID == id);
        }

        /// <summary>
        /// Actualizează o melodie existentă în colecția in-memory.
        /// </summary>
        /// <param name="melodieActualizata">Obiectul Melodie cu datele actualizate.</param>
        /// <returns>True dacă actualizarea a reușit, false dacă melodia nu a fost găsită.</returns>
        public bool UpdateMelodie(Melodie melodieActualizata)
        {
            if (melodieActualizata == null) return false;
            var melodieExistenta = _melodii.FirstOrDefault(m => m.MelodieID == melodieActualizata.MelodieID);
            if (melodieExistenta != null)
            {
                melodieExistenta.Titlu = melodieActualizata.Titlu;
                melodieExistenta.Artist = melodieActualizata.Artist;
                melodieExistenta.GenMuzical = melodieActualizata.GenMuzical;
                melodieExistenta.AnLansare = melodieActualizata.AnLansare;
                // PunctajTotal is usually calculated, not directly updated here unless specified
                // melodieExistenta.PunctajTotal = melodieActualizata.PunctajTotal; 
                return true;
            }
            return false;
        }

        /// <summary>
        /// Șterge o melodie din colecția in-memory după ID.
        /// </summary>
        /// <param name="id">ID-ul melodiei de șters.</param>
        /// <returns>True dacă ștergerea a reușit, false dacă melodia nu a fost găsită.</returns>
        public bool DeleteMelodie(int id)
        {
            var melodieDeSters = _melodii.FirstOrDefault(m => m.MelodieID == id);
            if (melodieDeSters != null)
            {
                _melodii.Remove(melodieDeSters);

                // Utilizează VotRepository pentru a șterge voturile asociate
                VotRepository votRepository = new VotRepository(); // Ideal ar fi injectat
                votRepository.StergeVoturiPentruMelodie(id);
                
                // Console.WriteLine($"INFO: Melodie ștearsă in-memory: ID {id}. Voturile asociate au fost de asemenea șterse.");
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Calculează și actualizează punctajele melodiilor pe baza voturilor (in-memory).
        /// Această metodă va necesita acces la VotRepository pentru a obține `toateVoturile`.
        /// Modelul Vot conține acum `MelodieID` și `PuncteAlocate`.
        /// Logica de calcul: Punctajul total al melodiei este suma `PuncteAlocate` din toate voturile.
        /// </summary>
        public void CalculeazaSiActualizeazaPunctajMelodii()
        {
            VotRepository votRepository = new VotRepository(); // Ideal ar fi injectat
            List<Vot> toateVoturile = votRepository.GetAllVoturi();

            foreach (var melodie in _melodii)
            {
                melodie.PunctajTotal = 0; // Reset scores
            }

            if (toateVoturile == null || !toateVoturile.Any()) 
            {
                // Console.WriteLine("INFO: Nu există voturi pentru a calcula punctajele melodiilor.");
                return;
            }

            foreach (var vot in toateVoturile)
            {
                var melodieVotata = _melodii.FirstOrDefault(m => m.MelodieID == vot.MelodieID);
                if (melodieVotata != null)
                {
                    melodieVotata.PunctajTotal += vot.PuncteAlocate; // Sum direct allocated points
                }
            }
            // Console.WriteLine("INFO: Punctajele melodiilor au fost recalculate pe baza punctelor alocate direct (in-memory).");
        }

        /// <summary>
        /// Returnează primele N melodii ordonate după punctaj (in-memory).
        /// </summary>
        public List<Melodie> GetTopNMelodii(int n)
        {
            return _melodii.OrderByDescending(m => m.PunctajTotal)
                           .ThenBy(m => m.Titlu)
                           .Take(n)
                           .ToList();
        }

        // Alte metode necesare conform sarcinii (ex: pentru populare ComboBox-uri, etc.)
    }
} 