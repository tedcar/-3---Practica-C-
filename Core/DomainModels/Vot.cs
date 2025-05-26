using System;

namespace MelodiiApp.Core.DomainModels
{
    /// <summary>
    /// Reprezintă un vot acordat de un utilizator unei melodii.
    /// </summary>
    public class Vot
    {
        /// <summary>
        /// Identificatorul unic al votului (generat de baza de date).
        /// </summary>
        public int VotID { get; set; }

        /// <summary>
        /// ID-ul utilizatorului care a acordat votul.
        /// Poate fi 0 sau null în BD dacă votul nu este asociat unui utilizator înregistrat (de ex. vot temporar).
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// ID-ul melodiei pentru care s-a acordat votul.
        /// </summary>
        public int MelodieID { get; set; }

        /// <summary>
        /// Numărul de puncte alocate melodiei prin acest vot.
        /// </summary>
        public int PuncteAlocate { get; set; }

        /// <summary>
        /// Data și ora la care a fost înregistrat votul.
        /// </summary>
        public DateTime DataVot { get; set; }

        /// <summary>
        /// Constructor implicit.
        /// Setează data votului la momentul curent.
        /// </summary>
        public Vot()
        {
            this.DataVot = DateTime.Now; // Consistent cu Predictie.cs
        }

        /// <summary>
        /// Constructor pentru crearea unui vot cu detalii specifice.
        /// </summary>
        /// <param name="userId">ID-ul utilizatorului.</param>
        /// <param name="melodieId">ID-ul melodiei.</param>
        /// <param name="puncteAlocate">Punctele alocate.</param>
        public Vot(int userId, int melodieId, int puncteAlocate) : this() // Apelează constructorul implicit
        {
            UserID = userId;
            MelodieID = melodieId;
            PuncteAlocate = puncteAlocate;
        }
    }
} 