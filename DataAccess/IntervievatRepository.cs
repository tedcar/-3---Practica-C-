using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MelodiiApp.DataAccess
{
    /// <summary>
    /// Repository pentru gestionarea operațiunilor CRUD (Creare, Citire, Actualizare, Ștergere)
    /// și a logicii specifice pentru entitatea Intervievat.
    /// </summary>
    public class IntervievatRepository
    {
        /// <summary>
        /// Adaugă un intervievat nou în baza de date.
        /// </summary>
        /// <param name="intervievat">Obiectul Intervievat de adăugat.</param>
        /// <returns>True dacă adăugarea a reușit, altfel false.</returns>
        public bool AdaugaIntervievat(Intervievat intervievat)
        {
            if (intervievat == null || string.IsNullOrWhiteSpace(intervievat.NumeComplet) || intervievat.Varsta <= 0)
            {
                // Validare de bază pentru datele esențiale.
                return false;
            }

            string query = @"
                INSERT INTO Intervievati (NumeComplet, Varsta, Localitate, ScorTotalConcurs, DataInscriere)
                VALUES (@NumeComplet, @Varsta, @Localitate, @ScorTotalConcurs, GETDATE());
                SELECT SCOPE_IDENTITY();"; // Returnează ID-ul nou inserat.

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@NumeComplet", SqlDbType.NVarChar, 200) { Value = intervievat.NumeComplet },
                new SqlParameter("@Varsta", SqlDbType.Int) { Value = (object)intervievat.Varsta ?? DBNull.Value },
                new SqlParameter("@Localitate", SqlDbType.NVarChar, 100) { Value = (object)intervievat.Localitate ?? DBNull.Value },
                new SqlParameter("@ScorTotalConcurs", SqlDbType.Int) { Value = intervievat.ScorTotalConcurs } // Scorul inițial.
            };

            try
            {
                object newIdObj = DatabaseHelper.ExecuteScalar(query, CommandType.Text, parameters);
                if (newIdObj != null && newIdObj != DBNull.Value)
                {
                    intervievat.IntervievatID = Convert.ToInt32(newIdObj);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la adăugarea intervievatului: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Preia toți intervievații din baza de date, ordonați după nume.
        /// </summary>
        /// <returns>O listă de obiecte Intervievat.</returns>
        public List<Intervievat> GetAllIntervievati()
        {
            List<Intervievat> intervievati = new List<Intervievat>();
            string query = "SELECT IntervievatID, NumeComplet, Varsta, Localitate, ScorTotalConcurs, DataInscriere FROM Intervievati ORDER BY NumeComplet";
            try
            {
                DataTable dt = DatabaseHelper.GetDataTable(query, CommandType.Text);
                foreach (DataRow row in dt.Rows)
                {
                    intervievati.Add(MapDataRowToIntervievat(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la preluarea tuturor intervievaților: {ex.Message}");
            }
            return intervievati;
        }

        /// <summary>
        /// Preia un intervievat specific din baza de date pe baza ID-ului.
        /// </summary>
        /// <param name="id">ID-ul intervievatului.</param>
        /// <returns>Obiectul Intervievat dacă este găsit, altfel null.</returns>
        public Intervievat GetIntervievatById(int id)
        {
            string query = "SELECT IntervievatID, NumeComplet, Varsta, Localitate, ScorTotalConcurs, DataInscriere FROM Intervievati WHERE IntervievatID = @IntervievatID";
            SqlParameter paramId = new SqlParameter("@IntervievatID", SqlDbType.Int) { Value = id };
            try
            {
                DataTable dt = DatabaseHelper.GetDataTable(query, CommandType.Text, paramId);
                if (dt.Rows.Count > 0)
                {
                    return MapDataRowToIntervievat(dt.Rows[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la preluarea intervievatului cu ID {id}: {ex.Message}");
            }
            return null;
        }

        /// <summary>
        /// Șterge un intervievat din baza de date.
        /// De asemenea, șterge predicțiile asociate acestuia pentru a menține integritatea datelor.
        /// </summary>
        /// <param name="id">ID-ul intervievatului de șters.</param>
        /// <returns>True dacă ștergerea a reușit, altfel false.</returns>
        public bool StergeIntervievat(int id)
        {
            string query = "DELETE FROM Intervievati WHERE IntervievatID = @IntervievatID";
            SqlParameter paramId = new SqlParameter("@IntervievatID", SqlDbType.Int) { Value = id };
            try
            {
                // Înainte de a șterge un intervievat, șterge predicțiile acestuia.
                PredictieRepository predictieRepo = new PredictieRepository();
                predictieRepo.StergePredictiiPentruIntervievat(id); 

                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, CommandType.Text, paramId);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la ștergerea intervievatului {id}: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Preia o listă de intervievați care au vârsta sub o anumită limită maximă.
        /// </summary>
        /// <param name="varstaMaxima">Vârsta maximă (exclusiv).</param>
        /// <returns>O listă de intervievați care îndeplinesc criteriul de vârstă.</returns>
        public List<Intervievat> GetIntervievatiSubVarsta(int varstaMaxima)
        {
            List<Intervievat> intervievati = new List<Intervievat>();
            string query = "SELECT IntervievatID, NumeComplet, Varsta, Localitate, ScorTotalConcurs, DataInscriere FROM Intervievati WHERE Varsta < @VarstaMaxima ORDER BY NumeComplet";
            SqlParameter paramVarsta = new SqlParameter("@VarstaMaxima", SqlDbType.Int) { Value = varstaMaxima };
            try
            {
                DataTable dt = DatabaseHelper.GetDataTable(query, CommandType.Text, paramVarsta);
                foreach (DataRow row in dt.Rows)
                {
                    intervievati.Add(MapDataRowToIntervievat(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la preluarea intervievaților cu vârsta sub {varstaMaxima}: {ex.Message}");
            }
            return intervievati;
        }

        /// <summary>
        /// Calculează și actualizează scorurile totale de concurs pentru toți intervievații.
        /// Scorul se bazează pe corectitudinea predicțiilor lor față de top 3 melodii reale.
        /// Punctaje: Locul 1 corect = 10p, Locul 2 corect = 5p, Locul 3 corect = 1p.
        /// </summary>
        public void CalculeazaSiActualizeazaScorIntervievati()
        {
            MelodieRepository melodieRepository = new MelodieRepository();
            List<Melodie> top3MelodiiReale = melodieRepository.GetTopNMelodii(3);
            PredictieRepository predictieRepository = new PredictieRepository();
            List<Intervievat> totiIntervievatii = GetAllIntervievati(); 

            if (top3MelodiiReale == null) top3MelodiiReale = new List<Melodie>(); // Asigură că lista nu este nulă.

            foreach (var intervievat in totiIntervievatii)
            {
                int scorCalculat = 0;
                Predictie predictiaIntervievatului = predictieRepository.GetPredictieByIntervievatId(intervievat.IntervievatID);

                if (predictiaIntervievatului != null)
                {
                    // Verifică predicția pentru Locul 1
                    if (top3MelodiiReale.Count >= 1 && predictiaIntervievatului.MelodieID_Loc1 == top3MelodiiReale[0].MelodieID)
                    {
                        scorCalculat += 10;
                    }
                    // Verifică predicția pentru Locul 2
                    if (top3MelodiiReale.Count >= 2 && predictiaIntervievatului.MelodieID_Loc2 == top3MelodiiReale[1].MelodieID)
                    {
                        scorCalculat += 5;
                    }
                    // Verifică predicția pentru Locul 3
                    if (top3MelodiiReale.Count >= 3 && predictiaIntervievatului.MelodieID_Loc3 == top3MelodiiReale[2].MelodieID)
                    {
                        scorCalculat += 1;
                    }
                }
                
                // Actualizează scorul în baza de date pentru acest intervievat.
                string updateQuery = "UPDATE Intervievati SET ScorTotalConcurs = @Scor WHERE IntervievatID = @IntervievatID";
                SqlParameter[] updateParams = new SqlParameter[]
                {
                    new SqlParameter("@Scor", SqlDbType.Int) { Value = scorCalculat },
                    new SqlParameter("@IntervievatID", SqlDbType.Int) { Value = intervievat.IntervievatID }
                };
                try
                {
                    DatabaseHelper.ExecuteNonQuery(updateQuery, CommandType.Text, updateParams);
                }
                catch (Exception ex)
                {
                     Console.WriteLine($"Eroare la actualizarea scorului pentru intervievatul {intervievat.IntervievatID}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Preia primii N intervievați din clasament, ordonați după scor total (descrescător) și apoi nume (crescător).
        /// Scorurile ar trebui să fie actualizate înainte de apelarea acestei metode (de ex., prin `MainForm`).
        /// </summary>
        /// <param name="n">Numărul de intervievați de preluat.</param>
        /// <returns>O listă cu primii N intervievați.</returns>
        public List<Intervievat> GetTopNIntervievati(int n)
        {
            List<Intervievat> intervievati = new List<Intervievat>();
            string query = $"SELECT TOP {n} IntervievatID, NumeComplet, Varsta, Localitate, ScorTotalConcurs, DataInscriere FROM Intervievati ORDER BY ScorTotalConcurs DESC, NumeComplet ASC";
            try
            {
                DataTable dt = DatabaseHelper.GetDataTable(query, CommandType.Text);
                foreach (DataRow row in dt.Rows)
                {
                    intervievati.Add(MapDataRowToIntervievat(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la preluarea top {n} intervievați: {ex.Message}");
            }
            return intervievati;
        }

        /// <summary>
        /// Actualizează datele unui intervievat existent în baza de date.
        /// Scorul total de concurs este gestionat separat prin `CalculeazaSiActualizeazaScorIntervievati`.
        /// </summary>
        /// <param name="intervievat">Obiectul Intervievat cu datele actualizate.</param>
        /// <returns>True dacă actualizarea a reușit, altfel false.</returns>
        public bool UpdateIntervievat(Intervievat intervievat)
        {
            if (intervievat == null) return false;

            string query = @"
                UPDATE Intervievati 
                SET NumeComplet = @NumeComplet, Varsta = @Varsta, Localitate = @Localitate
                WHERE IntervievatID = @IntervievatID;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@IntervievatID", SqlDbType.Int) { Value = intervievat.IntervievatID },
                new SqlParameter("@NumeComplet", SqlDbType.NVarChar, 200) { Value = intervievat.NumeComplet },
                new SqlParameter("@Varsta", SqlDbType.Int) { Value = (object)intervievat.Varsta ?? DBNull.Value },
                new SqlParameter("@Localitate", SqlDbType.NVarChar, 100) { Value = (object)intervievat.Localitate ?? DBNull.Value }
            };

            try
            {
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la actualizarea intervievatului: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Mapează un DataRow la un obiect Intervievat.
        /// </summary>
        /// <param name="row">DataRow-ul de mapat.</param>
        /// <returns>Un obiect Intervievat populat.</returns>
        private Intervievat MapDataRowToIntervievat(DataRow row)
        {
            return new Intervievat
            {
                IntervievatID = Convert.ToInt32(row["IntervievatID"]),
                NumeComplet = row["NumeComplet"].ToString(),
                Varsta = row["Varsta"] != DBNull.Value ? Convert.ToInt32(row["Varsta"]) : (int?)null,
                Localitate = row["Localitate"] != DBNull.Value ? row["Localitate"].ToString() : null,
                ScorTotalConcurs = Convert.ToInt32(row["ScorTotalConcurs"]),
                DataInscriere = Convert.ToDateTime(row["DataInscriere"])
            };
        }
    }
}