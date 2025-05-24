using System;

namespace MelodiiApp.Core.DomainModels
{
    public class Vot
    {
        private static int _nextVotId = 1;

        public int VotID { get; private set; }
        public int IntervievatID { get; set; }
        public int MelodieID { get; set; }
        public int PuncteAlocate { get; set; }
        public DateTime DataVot { get; set; }

        public Vot()
        {
            this.VotID = _nextVotId++;
            this.DataVot = DateTime.UtcNow;
        }

        public Vot(int intervievatId, int melodieId, int puncteAlocate) : this()
        {
            IntervievatID = intervievatId;
            MelodieID = melodieId;
            PuncteAlocate = puncteAlocate;
        }
    }
} 