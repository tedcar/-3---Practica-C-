using System;

namespace MelodiiApp.Core.DomainModels
{
    /// <summary>
    /// Reprezintă predicția unui intervievat pentru Top 3 melodii.
    /// </summary>
    public class Predictie
    {
        /// <summary>
        /// Identificatorul unic al predicției (generat de baza de date).
        /// </summary>
        public int PredictieID { get; set; }

        /// <summary>
        /// ID-ul intervievatului care a făcut predicția.
        /// </summary>
        public int IntervievatID { get; set; }

        /// <summary>
        /// ID-ul melodiei prezise pentru locul 1.
        /// Valoarea 0 indică nicio selecție.
        /// </summary>
        public int MelodieID_Loc1 { get; set; } 

        /// <summary>
        /// ID-ul melodiei prezise pentru locul 2.
        /// Valoarea 0 indică nicio selecție.
        /// </summary>
        public int MelodieID_Loc2 { get; set; } 

        /// <summary>
        /// ID-ul melodiei prezise pentru locul 3.
        /// Valoarea 0 indică nicio selecție.
        /// </summary>
        public int MelodieID_Loc3 { get; set; } 

        /// <summary>
        /// Data la care a fost înregistrată predicția.
        /// </summary>
        public DateTime DataPredictie { get; set; }

        /// <summary>
        /// Constructor implicit.
        /// Setează data predicției la momentul curent.
        /// </summary>
        public Predictie()
        {
            DataPredictie = DateTime.Now;
        }

        /// <summary>
        /// Constructor pentru crearea unei predicții cu detalii specifice.
        /// </summary>
        /// <param name="intervievatId">ID-ul intervievatului.</param>
        /// <param name="melodieIdLoc1">ID-ul melodiei pentru locul 1.</param>
        /// <param name="melodieIdLoc2">ID-ul melodiei pentru locul 2.</param>
        /// <param name="melodieIdLoc3">ID-ul melodiei pentru locul 3.</param>
        public Predictie(int intervievatId, int melodieIdLoc1, int melodieIdLoc2, int melodieIdLoc3)
            : this() // Apelează constructorul implicit pentru a seta DataPredictie
        {
            IntervievatID = intervievatId;
            MelodieID_Loc1 = melodieIdLoc1;
            MelodieID_Loc2 = melodieIdLoc2;
            MelodieID_Loc3 = melodieIdLoc3;
        }
    }
} 