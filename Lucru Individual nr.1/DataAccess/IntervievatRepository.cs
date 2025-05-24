using MelodiiApp.Core.DomainModels;
// using MelodiiApp.DataAccess.DatabaseConnection; // No longer needed for in-memory
using System;
// using System.Data.SqlClient; // No longer needed for in-memory
using System.Collections.Generic; // Required for List<T>
using System.Linq; // Required for LINQ operations

namespace MelodiiApp.DataAccess
{
    /// <summary>
    /// Repository pentru gestionarea operațiunilor CRUD pentru entitatea Intervievat (In-Memory).
    /// </summary>
    public class IntervievatRepository
    {
        private static List<Intervievat> _intervievati = new List<Intervievat>();
        private static int _nextIntervievatId = 1;

        /// <summary>
        /// Adaugă un nou intervievat în colecția in-memory.
        /// </summary>
        /// <param name="intervievat">Obiectul Intervievat care urmează să fie adăugat.</param>
        /// <returns>True dacă adăugarea a reușit, false în caz contrar.</returns>
        public bool AdaugaIntervievat(Intervievat intervievat)
        {
            if (intervievat == null)
            {
                // Console.WriteLine("EROARE: Obiectul Intervievat este null în AdaugaIntervievat (In-Memory).");
                return false;
            }
            if (string.IsNullOrWhiteSpace(intervievat.NumeComplet))
            {
                 // Console.WriteLine("EROARE: Numele intervievatului nu poate fi gol (In-Memory).");
                 return false;
            }
            if (intervievat.Varsta <= 0)
            {
                // Console.WriteLine("EROARE: Vârsta intervievatului trebuie să fie un număr pozitiv (In-Memory).");
                return false;
            }
            // Optional: Check for duplicate NumeComplet if it should be unique
            // if (_intervievati.Any(i => i.NumeComplet.Equals(intervievat.NumeComplet, StringComparison.OrdinalIgnoreCase)))
            // {
            //     Console.WriteLine("EROARE: Un intervievat cu același nume complet există deja (In-Memory).");
            //     return false;
            // }

            intervievat.IntervievatID = _nextIntervievatId++;
            // ScorTotalConcurs is already initialized to 0 in the Intervievat model constructor
            _intervievati.Add(intervievat);
            // Console.WriteLine($"INFO: Intervievat adăugat in-memory: ID {intervievat.IntervievatID}, Nume: {intervievat.NumeComplet}");
            return true;
        }

        /// <summary>
        /// Returnează toți intervievații din colecția in-memory.
        /// </summary>
        public List<Intervievat> GetAllIntervievati()
        {
            return new List<Intervievat>(_intervievati); 
        }

        /// <summary>
        /// Găsește un intervievat după ID.
        /// </summary>
        public Intervievat GetIntervievatById(int id)
        {
            return _intervievati.FirstOrDefault(i => i.IntervievatID == id);
        }

        /// <summary>
        /// Șterge un intervievat după ID din colecția in-memory.
        /// De asemenea, va trebui să gestioneze ștergerea voturilor asociate acestui intervievat.
        /// </summary>
        /// <param name="id">ID-ul intervievatului de șters.</param>
        /// <returns>True dacă ștergerea a reușit, false altfel.</returns>
        public bool StergeIntervievat(int id) 
        {
            var intervievatDeSters = _intervievati.FirstOrDefault(i => i.IntervievatID == id);
            if (intervievatDeSters != null)
            {
                _intervievati.Remove(intervievatDeSters);
                
                // Utilizează VotRepository pentru a șterge voturile asociate
                VotRepository votRepository = new VotRepository(); 
                votRepository.StergeVoturiPentruIntervievat(id);

                // Utilizează PredictieRepository pentru a șterge predicțiile asociate
                PredictieRepository predictieRepository = new PredictieRepository();
                predictieRepository.StergePredictiiPentruIntervievat(id);
                
                // Console.WriteLine($"INFO: Intervievat șters in-memory: ID {id}. Voturile și predicțiile asociate au fost de asemenea șterse.");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returnează intervievații sub o anumită vârstă.
        /// </summary>
        public List<Intervievat> GetIntervievatiSubVarsta(int varstaMaxima)
        {
            return _intervievati.Where(i => i.Varsta < varstaMaxima).ToList(); // Sarcina specifică <= 18, deci <18 ani.
        }

        /// <summary>
        /// Calculează și actualizează scorurile intervievaților (in-memory).
        /// Placeholder: Actualizează scorul pe baza numărului de voturi (necesită VotRepository).
        /// Logica detaliată a calculului scorului va fi implementată ulterior.
        /// </summary>
        public void CalculeazaSiActualizeazaScorIntervievati()
        {
            // Obține top 3 melodii REALE (din MelodieRepository, bazat pe voturile cu 6 puncte)
            MelodieRepository melodieRepository = new MelodieRepository(); 
            List<Melodie> top3MelodiiReale = melodieRepository.GetTopNMelodii(3); 

            if (top3MelodiiReale == null || top3MelodiiReale.Count == 0) // Poate fi mai puțin de 3 dacă nu sunt suficiente melodii/voturi
            {
                // Console.WriteLine("WARN: Nu se pot calcula scorurile intervievaților. Clasamentul real al melodiilor nu este disponibil sau este incomplet.");
                // Opțional, resetați scorurile tuturor intervievaților la 0
                foreach (var intervievat in _intervievati)
                {
                    intervievat.ScorTotalConcurs = 0;
                }
                return;
            }

            PredictieRepository predictieRepository = new PredictieRepository();

            foreach (var intervievat in _intervievati)
            {
                intervievat.ScorTotalConcurs = 0; // Reset score
                Predictie predictiaIntervievatului = predictieRepository.GetPredictieByIntervievatId(intervievat.IntervievatID);

                if (predictiaIntervievatului == null) continue; // Nu are predicție, scor 0

                // Array cu ID-urile melodiilor prezise de intervievat
                int[] melodiiPrezise = {
                    predictiaIntervievatului.MelodieID_Loc1,
                    predictiaIntervievatului.MelodieID_Loc2,
                    predictiaIntervievatului.MelodieID_Loc3
                };

                // Logica de calcul scor conform specificatiilor (10pct ghicit exact, 5pct +/-1 poz, 3pct +/-2 poz)
                for (int i = 0; i < top3MelodiiReale.Count; i++)
                {
                    int melodieIdTopReal = top3MelodiiReale[i].MelodieID;
                    int pozitieTopReal = i + 1; // 1, 2, sau 3

                    // Verifică dacă melodia din topul real a fost prezisă de intervievat și pe ce poziție
                    int pozitiePrezisa = -1;
                    if (melodiiPrezise[0] == melodieIdTopReal) pozitiePrezisa = 1;
                    else if (melodiiPrezise.Length > 1 && melodiiPrezise[1] == melodieIdTopReal) pozitiePrezisa = 2;
                    else if (melodiiPrezise.Length > 2 && melodiiPrezise[2] == melodieIdTopReal) pozitiePrezisa = 3;

                    if (pozitiePrezisa != -1) // Melodia a fost găsită în predicții
                    {
                        if (pozitiePrezisa == pozitieTopReal)
                        {
                            intervievat.ScorTotalConcurs += 10; // Ghicit exact (melodie + poziție)
                        }
                        else
                        {
                            int diferentaPozitie = Math.Abs(pozitiePrezisa - pozitieTopReal);
                            if (diferentaPozitie == 1) intervievat.ScorTotalConcurs += 5; // Poziție adiacentă (+/-1)
                            else if (diferentaPozitie == 2) intervievat.ScorTotalConcurs += 3; // Diferență de 2 poziții (+/-2)
                        }
                    }
                }
            }
            // Console.WriteLine("INFO: Scorurile intervievaților au fost recalculate pe baza predicțiilor explicite (in-memory).");
        }

        /// <summary>
        /// Returnează primii N intervievați ordonați după scor (in-memory).
        /// </summary>
        public List<Intervievat> GetTopNIntervievati(int n)
        {
            return _intervievati.OrderByDescending(i => i.ScorTotalConcurs)
                                .ThenBy(i => i.NumeComplet)
                                .Take(n)
                                .ToList();
        }

        /// <summary>
        /// Actualizează un intervievat existent (in-memory).
        /// </summary>
        /// <param name="intervievat">Obiectul cu valori noi (IntervievatID trebuie să existe).</param>
        /// <returns>True dacă actualizarea a reușit.</returns>
        public bool UpdateIntervievat(Intervievat intervievat)
        {
            if (intervievat == null) return false;
            var existent = _intervievati.FirstOrDefault(i => i.IntervievatID == intervievat.IntervievatID);
            if (existent == null) return false;

            existent.NumeComplet = intervievat.NumeComplet;
            existent.Varsta = intervievat.Varsta;
            existent.Localitate = intervievat.Localitate;
            // Scorul se păstrează
            return true;
        }
    }
}