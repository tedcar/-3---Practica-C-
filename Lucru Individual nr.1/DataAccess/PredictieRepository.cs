using MelodiiApp.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MelodiiApp.DataAccess
{
    /// <summary>
    /// Repository pentru gestionarea predicțiilor făcute de intervievați (In-Memory).
    /// </summary>
    public class PredictieRepository
    {
        private static List<Predictie> _predictii = new List<Predictie>();

        /// <summary>
        /// Salvează predicția unui intervievat.
        /// Dacă există deja o predicție pentru acest intervievat, aceasta va fi suprascrisă.
        /// </summary>
        /// <param name="predictie">Predicția de salvat.</param>
        /// <returns>True dacă salvarea a reușit.</returns>
        public bool SalveazaPredictie(Predictie predictieNoua)
        {
            if (predictieNoua == null)
            {
                // Console.WriteLine("EROARE: Obiectul Predictie este null în SalveazaPredictie (In-Memory).");
                return false;
            }

            // Elimină predicția veche, dacă există, pentru acest intervievat
            _predictii.RemoveAll(p => p.IntervievatID == predictieNoua.IntervievatID);

            // Adaugă noua predicție
            _predictii.Add(predictieNoua);
            // Console.WriteLine($"INFO: Predicție salvată/actualizată in-memory pentru IntervievatID {predictieNoua.IntervievatID}.");
            return true;
        }

        /// <summary>
        /// Returnează predicția pentru un intervievat specific.
        /// </summary>
        /// <param name="intervievatId">ID-ul intervievatului.</param>
        /// <returns>Predicția sau null dacă nu există.</returns>
        public Predictie GetPredictieByIntervievatId(int intervievatId)
        {
            return _predictii.FirstOrDefault(p => p.IntervievatID == intervievatId);
        }

        /// <summary>
        /// Returnează toate predicțiile.
        /// </summary>
        public List<Predictie> GetAllPredictii()
        {
            return new List<Predictie>(_predictii); // Returnează o copie
        }
        
        /// <summary>
        /// Șterge toate predicțiile asociate unui intervievat.
        /// Util pentru când un intervievat este șters din sistem.
        /// </summary>
        /// <param name="intervievatId">ID-ul intervievatului.</param>
        public void StergePredictiiPentruIntervievat(int intervievatId)
        {
            int countRemoved = _predictii.RemoveAll(p => p.IntervievatID == intervievatId);
            if (countRemoved > 0)
            {
                // Console.WriteLine($"INFO: {countRemoved} predicții șterse (in-memory) pentru IntervievatID {intervievatId}.");
            }
        }

        // Helper method for testing or reset if needed
        public static void ResetPredictii()
        {
            _predictii.Clear();
            // Console.WriteLine("INFO: Lista de predicții (in-memory) a fost resetată.");
        }
    }
} 