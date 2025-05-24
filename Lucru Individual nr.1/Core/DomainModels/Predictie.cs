using System;

namespace MelodiiApp.Core.DomainModels
{
    /// <summary>
    /// Reprezintă predicția unui intervievat pentru Top 3 melodii.
    /// </summary>
    public class Predictie
    {
        private static int _nextId = 1;
        public int PredictieID { get; private set; }
        public int IntervievatID { get; set; }
        public int MelodieID_Loc1 { get; set; } // ID-ul melodiei prezise pentru locul 1
        public int MelodieID_Loc2 { get; set; } // ID-ul melodiei prezise pentru locul 2
        public int MelodieID_Loc3 { get; set; } // ID-ul melodiei prezise pentru locul 3
        public DateTime DataPredictie { get; set; }

        public Predictie()
        {
            PredictieID = _nextId++;
            DataPredictie = DateTime.Now;
        }

        public Predictie(int intervievatId, int melodieIdLoc1, int melodieIdLoc2, int melodieIdLoc3)
            : this()
        {
            IntervievatID = intervievatId;
            MelodieID_Loc1 = melodieIdLoc1;
            MelodieID_Loc2 = melodieIdLoc2;
            MelodieID_Loc3 = melodieIdLoc3;
        }
    }
} 