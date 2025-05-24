using MelodiiApp.Core.DomainModels;
// using MelodiiApp.DataAccess.DatabaseConnection; // Removed
using System;
using System.Collections.Generic;
// using System.Data.SqlClient; // Removed
using System.Linq;

namespace MelodiiApp.DataAccess
{
    public class VotRepository
    {
        private static List<Vot> _voturi = new List<Vot>();
        // private static int _nextVotId = 1; // REMOVED: VotID is now generated in Vot.cs constructor

        /// <summary>
        /// Verifică dacă un intervievat a votat deja.
        /// </summary>
        /// <param name="intervievatId">ID-ul intervievatului.</param>
        /// <returns>True dacă intervievatul a votat, false altfel.</returns>
        public bool IntervievatAFostVotat(int intervievatId)
        {
            // lock (_voturi) // Consider thread safety if multiple threads can access this
            // {
                return _voturi.Any(v => v.IntervievatID == intervievatId);
            // }
        }

        /// <summary>
        /// Înregistrează o listă de voturi pentru un intervievat.
        /// </summary>
        /// <param name="voturiDeAdaugat">Lista de voturi de adăugat.</param>
        /// <returns>True dacă voturile au fost adăugate cu succes, false altfel.</returns>
        public bool InregistreazaVoturi(List<Vot> voturiDeAdaugat)
        {
            if (voturiDeAdaugat == null || !voturiDeAdaugat.Any())
            {
                // Console.WriteLine("WARN: Lista de voturi pentru înregistrare este goală sau null (in-memory).");
                return false; // Sau true dacă se consideră o operațiune validă (fără efect)
            }

            // lock (_voturi) // Consider thread safety
            // {
                // Basic check: has this interviewee already voted? This should ideally be caught by UI logic first.
                if (IntervievatAFostVotat(voturiDeAdaugat.First().IntervievatID)) {
                    // Console.WriteLine($"INFO: Intervievatul ID {voturiDeAdaugat.First().IntervievatID} a votat deja (verificare suplimentară în repository).");
                    return false; // Or handle as an update if rules allow, though current logic implies one vote.
                }

                foreach (var vot in voturiDeAdaugat)
                {
                    // vot.VotID = _nextVotId++; // REMOVED: VotID is set in Vot constructor
                    _voturi.Add(vot);
                    // Console.WriteLine($"INFO: Vot adăugat (in-memory): IntervievatID {vot.IntervievatID}, MelodieID {vot.MelodieID}, PuncteAlocate {vot.PuncteAlocate}, VotID {vot.VotID}");
                }
                return true;
            // }
        }

        /// <summary>
        /// Returnează toate voturile.
        /// </summary>
        public List<Vot> GetAllVoturi()
        {
            // lock (_voturi) // Consider thread safety
            // {
                return new List<Vot>(_voturi); // Return a copy
            // }
        }

        /// <summary>
        /// Returnează voturile pentru un intervievat specific.
        /// </summary>
        public List<Vot> GetVoturiByIntervievatId(int intervievatId)
        {
            // lock (_voturi) // Consider thread safety
            // {
                return _voturi.Where(v => v.IntervievatID == intervievatId).ToList();
            // }
        }

        /// <summary>
        /// Șterge toate voturile asociate unui intervievat.
        /// </summary>
        /// <param name="intervievatId">ID-ul intervievatului ale cărui voturi vor fi șterse.</param>
        /// <returns>True dacă operațiunea a reușit.</returns>
        public bool StergeVoturiPentruIntervievat(int intervievatId)
        {
            // lock (_voturi) // Consider thread safety
            // {
                int countRemoved = _voturi.RemoveAll(v => v.IntervievatID == intervievatId);
                if (countRemoved > 0)
                {
                    // Console.WriteLine($"INFO: {countRemoved} voturi șterse (in-memory) pentru IntervievatID {intervievatId}.");
                }
                // După ștergerea voturilor, este bine să actualizăm scorurile melodiilor afectate
                var melodieRepository = new MelodieRepository();
                melodieRepository.CalculeazaSiActualizeazaPunctajMelodii();
                return true; 
            // }
        }

        /// <summary>
        /// Șterge toate voturile asociate unei melodii.
        /// </summary>
        /// <param name="melodieId">ID-ul melodiei ale cărei voturi vor fi șterse.</param>
        /// <returns>True dacă operațiunea a reușit.</returns>
        public bool StergeVoturiPentruMelodie(int melodieId)
        {
            // lock (_voturi) // Consider thread safety
            // {
                int countRemoved = _voturi.RemoveAll(v => v.MelodieID == melodieId);
                if (countRemoved > 0)
                {
                    // Console.WriteLine($"INFO: {countRemoved} voturi șterse (in-memory) pentru MelodieID {melodieId}.");
                }
                // Nu este nevoie să recalculăm aici explicit, deoarece StergeMelodie din MelodieRepository va apela CalculeazaSiActualizeazaPunctajMelodii
                return true;
            // }
        }
        
        // Helper method for testing or reset if needed
        public static void ResetVoturi()
        {
            // lock(_voturi)
            // {
                _voturi.Clear();
                // _nextVotId = 1; // REMOVED: No longer needed here
                // Console.WriteLine("INFO: Lista de voturi (in-memory) a fost resetată.");
                // După resetarea tuturor voturilor, actualizăm scorurile melodiilor
                var melodieRepository = new MelodieRepository();
                melodieRepository.CalculeazaSiActualizeazaPunctajMelodii();
            // }
        }
    }
} 