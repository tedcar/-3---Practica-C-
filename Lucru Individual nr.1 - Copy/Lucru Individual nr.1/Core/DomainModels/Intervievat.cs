using System;

namespace MelodiiApp.Core.DomainModels
{
    /// <summary>
    /// Reprezintă o persoană intervievată în cadrul concursului.
    /// </summary>
    public class Intervievat
    {
        /// <summary>
        /// ID-ul unic al persoanei intervievate.
        /// </summary>
        public int IntervievatID { get; set; }

        /// <summary>
        /// Numele complet al persoanei intervievate.
        /// </summary>
        public string NumeComplet { get; set; }

        /// <summary>
        /// Vârsta persoanei intervievate.
        /// </summary>
        public int Varsta { get; set; }

        /// <summary>
        /// Localitatea de proveniență a persoanei intervievate.
        /// </summary>
        public string Localitate { get; set; }

        /// <summary>
        /// Scorul total acumulat de persoana intervievată în concurs.
        /// Inițializat la 0.
        /// </summary>
        public int ScorTotalConcurs { get; set; }

        /// <summary>
        /// Constructor implicit.
        /// </summary>
        public Intervievat()
        {
            ScorTotalConcurs = 0;
        }

        /// <summary>
        /// Constructor pentru crearea unui intervievat cu toate detaliile.
        /// </summary>
        public Intervievat(int intervievatID, string numeComplet, int varsta, string localitate)
        {
            IntervievatID = intervievatID;
            NumeComplet = numeComplet;
            Varsta = varsta;
            Localitate = localitate;
            ScorTotalConcurs = 0;
        }
    }
} 