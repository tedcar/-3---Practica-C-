using System;

namespace MelodiiApp.Core.DomainModels
{
    /// <summary>
    /// Reprezintă o melodie participantă la concurs.
    /// </summary>
    public class Melodie : ICloneable
    {
        /// <summary>
        /// ID-ul unic al melodiei.
        /// </summary>
        public int MelodieID { get; set; }

        /// <summary>
        /// Titlul melodiei.
        /// </summary>
        public string Titlu { get; set; }

        /// <summary>
        /// Artistul sau formația care interpretează melodia.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Genul muzical al melodiei.
        /// </summary>
        public string GenMuzical { get; set; }

        /// <summary>
        /// Anul lansării melodiei.
        /// </summary>
        public int? AnLansare { get; set; }

        /// <summary>
        /// Punctajul total acumulat de melodie în concurs.
        /// Inițializat la 0.
        /// </summary>
        public int PunctajTotal { get; set; }

        /// <summary>
        /// Data adaugarii melodiei.
        /// </summary>
        public DateTime DataAdaugare { get; set; }

        public string TitluArtist => $"{Titlu} - {Artist}"; // Calculated property for display

        /// <summary>
        /// Constructor implicit.
        /// </summary>
        public Melodie()
        {
            PunctajTotal = 0; // Inițializăm punctajul
        }

        /// <summary>
        /// Constructor pentru crearea unei melodii cu toate detaliile.
        /// </summary>
        /// <param name="melodieID">ID-ul melodiei.</param>
        /// <param name="titlu">Titlul melodiei.</param>
        /// <param name="artist">Artistul melodiei.</param>
        /// <param name="genMuzical">Genul muzical.</param>
        /// <param name="anLansare">Anul lansării.</param>
        public Melodie(int melodieID, string titlu, string artist, string genMuzical, int? anLansare)
        {
            MelodieID = melodieID;
            Titlu = titlu;
            Artist = artist;
            GenMuzical = genMuzical;
            AnLansare = anLansare;
            PunctajTotal = 0;
        }

        // Strongly-typed public Clone method
        public Melodie Clone()
        {
            return (Melodie)this.MemberwiseClone();
        }

        // Explicit interface implementation for ICloneable
        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
} 