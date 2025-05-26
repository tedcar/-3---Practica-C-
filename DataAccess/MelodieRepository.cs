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
    /// și a logicii specifice pentru entitatea Melodie.
    /// </summary>
    public class MelodieRepository
    {
        /// <summary>
        /// Adaugă o melodie nouă în baza de date.
        /// </summary>
        /// <param name="melodie">Obiectul Melodie de adăugat.</param>
        /// <returns>True dacă adăugarea a reușit, altfel false.</returns>
        public bool AdaugaMelodie(Melodie melodie)
        {
            if (melodie == null) return false;

            string query = @"
                INSERT INTO Melodii (Titlu, Artist, GenMuzical, AnLansare, PunctajTotal, DataAdaugare)
                VALUES (@Titlu, @Artist, @GenMuzical, @AnLansare, @PunctajTotal, GETDATE());
                SELECT SCOPE_IDENTITY();"; // Returnează ID-ul nou inserat.

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Titlu", SqlDbType.NVarChar, 200) { Value = melodie.Titlu },
                new SqlParameter("@Artist", SqlDbType.NVarChar, 150) { Value = melodie.Artist },
                new SqlParameter("@GenMuzical", SqlDbType.NVarChar, 100) { Value = (object)melodie.GenMuzical ?? DBNull.Value },
                new SqlParameter("@AnLansare", SqlDbType.Int) { Value = (object)melodie.AnLansare ?? DBNull.Value },
                new SqlParameter("@PunctajTotal", SqlDbType.Int) { Value = melodie.PunctajTotal } // Punctajul inițial, implicit 0 dacă nu este setat.
            };

            try
            {
                object newIdObj = DatabaseHelper.ExecuteScalar(query, CommandType.Text, parameters);
                if (newIdObj != null && newIdObj != DBNull.Value)
                {
                    melodie.MelodieID = Convert.ToInt32(newIdObj);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la adăugarea melodiei: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Actualizează datele unei melodii existente în baza de date.
        /// Punctajul total este gestionat separat prin logica de votare/calcul scor.
        /// </summary>
        /// <param name="melodieToUpdate">Obiectul Melodie cu datele actualizate.</param>
        /// <returns>True dacă actualizarea a reușit, altfel false.</returns>
        public bool UpdateMelodie(Melodie melodieToUpdate)
        {
            if (melodieToUpdate == null) return false;

            string query = @"
                UPDATE Melodii 
                SET Titlu = @Titlu, Artist = @Artist, GenMuzical = @GenMuzical, AnLansare = @AnLansare
                WHERE MelodieID = @MelodieID;"; 

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MelodieID", SqlDbType.Int) { Value = melodieToUpdate.MelodieID },
                new SqlParameter("@Titlu", SqlDbType.NVarChar, 200) { Value = melodieToUpdate.Titlu },
                new SqlParameter("@Artist", SqlDbType.NVarChar, 150) { Value = melodieToUpdate.Artist },
                new SqlParameter("@GenMuzical", SqlDbType.NVarChar, 100) { Value = (object)melodieToUpdate.GenMuzical ?? DBNull.Value },
                new SqlParameter("@AnLansare", SqlDbType.Int) { Value = (object)melodieToUpdate.AnLansare ?? DBNull.Value }
            };

            try
            {
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la actualizarea melodiei: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Preia toate melodiile din baza de date, ordonate după titlu și artist.
        /// </summary>
        /// <returns>O listă de obiecte Melodie.</returns>
        public List<Melodie> GetAllMelodii()
        {
            List<Melodie> melodii = new List<Melodie>();
            string query = "SELECT MelodieID, Titlu, Artist, GenMuzical, AnLansare, PunctajTotal, DataAdaugare FROM Melodii ORDER BY Titlu, Artist";
            try
            {
                DataTable dt = DatabaseHelper.GetDataTable(query, CommandType.Text);
                foreach (DataRow row in dt.Rows)
                {
                    melodii.Add(MapDataRowToMelodie(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la preluarea tuturor melodiilor: {ex.Message}");
            }
            return melodii;
        }

        /// <summary>
        /// Preia o melodie specifică din baza de date pe baza ID-ului.
        /// </summary>
        /// <param name="id">ID-ul melodiei.</param>
        /// <returns>Obiectul Melodie dacă este găsit, altfel null.</returns>
        public Melodie GetMelodieById(int id)
        {
            string query = "SELECT MelodieID, Titlu, Artist, GenMuzical, AnLansare, PunctajTotal, DataAdaugare FROM Melodii WHERE MelodieID = @MelodieID";
            SqlParameter paramId = new SqlParameter("@MelodieID", SqlDbType.Int) { Value = id };
            try
            {
                DataTable dt = DatabaseHelper.GetDataTable(query, CommandType.Text, paramId);
                if (dt.Rows.Count > 0)
                {
                    return MapDataRowToMelodie(dt.Rows[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la preluarea melodiei cu ID {id}: {ex.Message}");
            }
            return null;
        }

        /// <summary>
        /// Șterge o melodie din baza de date.
        /// Important: Această metodă șterge și voturile asociate și anulează (NULL) referințele din predicții
        /// pentru a menține integritatea datelor, dacă nu este configurată ștergerea în cascadă în BD.
        /// </summary>
        /// <param name="id">ID-ul melodiei de șters.</param>
        /// <returns>True dacă ștergerea a reușit, altfel false.</returns>
        public bool DeleteMelodie(int id)
        {
            string query = "DELETE FROM Melodii WHERE MelodieID = @MelodieID";
            SqlParameter paramId = new SqlParameter("@MelodieID", SqlDbType.Int) { Value = id };
            try
            {
                // Înainte de a șterge o melodie, șterge voturile asociate (dacă nu e cascadă în BD).
                VotRepository votRepo = new VotRepository();
                votRepo.StergeVoturiPentruMelodie(id);
                
                // Anulează (setează la NULL) referințele către această melodie în predicții.
                PredictieRepository predRepo = new PredictieRepository();
                predRepo.AnuleazaPredictiiPentruMelodie(id); 

                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, CommandType.Text, paramId);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la ștergerea melodiei {id}: {ex.Message}");
                return false;
            }
        }
        
        /// <summary>
        /// Calculează și actualizează punctajele totale pentru toate melodiile,
        /// pe baza sumei punctelor alocate în tabela Voturi.
        /// </summary>
        public void CalculeazaSiActualizeazaPunctajMelodii()
        {
            string query = @"
                UPDATE m
                SET m.PunctajTotal = ISNULL(v_sum.TotalPuncte, 0)
                FROM Melodii m
                LEFT JOIN (
                    SELECT MelodieID, SUM(PuncteAlocate) AS TotalPuncte
                    FROM Voturi
                    GROUP BY MelodieID
                ) AS v_sum ON m.MelodieID = v_sum.MelodieID;";
            try
            {
                DatabaseHelper.ExecuteNonQuery(query, CommandType.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la calcularea și actualizarea punctajelor melodiilor: {ex.Message}");
            }
        }

        /// <summary>
        /// Preia primele N melodii din clasament, ordonate după punctaj total (descrescător) și apoi titlu (crescător).
        /// Punctajele ar trebui să fie actualizate înainte de apelarea acestei metode (de ex., prin `MainForm`).
        /// </summary>
        /// <param name="n">Numărul de melodii de preluat.</param>
        /// <returns>O listă cu primele N melodii.</returns>
        public List<Melodie> GetTopNMelodii(int n)
        {
            List<Melodie> melodii = new List<Melodie>();
            string query = $"SELECT TOP {n} MelodieID, Titlu, Artist, GenMuzical, AnLansare, PunctajTotal, DataAdaugare FROM Melodii ORDER BY PunctajTotal DESC, Titlu ASC";
            try
            {
                DataTable dt = DatabaseHelper.GetDataTable(query, CommandType.Text);
                foreach (DataRow row in dt.Rows)
                {
                    melodii.Add(MapDataRowToMelodie(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la preluarea top {n} melodii: {ex.Message}");
            }
            return melodii;
        }

        /// <summary>
        /// Mapează un DataRow la un obiect Melodie.
        /// </summary>
        /// <param name="row">DataRow-ul de mapat.</param>
        /// <returns>Un obiect Melodie populat.</returns>
        private Melodie MapDataRowToMelodie(DataRow row)
        {
            return new Melodie
            {
                MelodieID = Convert.ToInt32(row["MelodieID"]),
                Titlu = row["Titlu"].ToString(),
                Artist = row["Artist"].ToString(),
                GenMuzical = row["GenMuzical"] != DBNull.Value ? row["GenMuzical"].ToString() : null,
                AnLansare = row["AnLansare"] != DBNull.Value ? Convert.ToInt32(row["AnLansare"]) : (int?)null,
                PunctajTotal = Convert.ToInt32(row["PunctajTotal"]),
                DataAdaugare = Convert.ToDateTime(row["DataAdaugare"])
            };
        }
    }
} 