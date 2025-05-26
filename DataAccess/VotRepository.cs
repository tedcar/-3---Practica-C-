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
    /// Repository pentru gestionarea operațiunilor legate de voturi.
    /// </summary>
    public class VotRepository
    {
        /// <summary>
        /// Adaugă un vot nou în baza de date.
        /// Gestionează cazul în care UserID este null (de ex., pentru utilizatori nepersistați sau dacă schema permite).
        /// </summary>
        /// <param name="vot">Obiectul Vot de adăugat.</param>
        /// <returns>True dacă adăugarea a reușit, altfel false.</returns>
        public bool AdaugaVot(Vot vot)
        {
            string query = @"
                INSERT INTO dbo.Voturi (UserID, MelodieID, PuncteAlocate, DataVot) 
                VALUES (@UserID, @MelodieID, @PuncteAlocate, GETDATE());
                SELECT SCOPE_IDENTITY();"; // Returnează ID-ul nou inserat.

            SqlParameter[] parameters = new SqlParameter[]
            {
                // Gestionează cazul în care UserID poate fi null (ex: utilizator temporar, sau dacă UserID din Voturi permite null).
                new SqlParameter("@UserID", SqlDbType.Int) { Value = (vot.UserID > 0) ? (object)vot.UserID : DBNull.Value },
                new SqlParameter("@MelodieID", SqlDbType.Int) { Value = vot.MelodieID },
                new SqlParameter("@PuncteAlocate", SqlDbType.Int) { Value = vot.PuncteAlocate }
            };

            try
            {
                object newIdObj = DatabaseHelper.ExecuteScalar(query, CommandType.Text, parameters);
                if (newIdObj != null && newIdObj != DBNull.Value)
                {
                    vot.VotID = Convert.ToInt32(newIdObj);
                    return true;
                }
                return false; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la adăugarea votului: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Preia toate voturile din baza de date.
        /// </summary>
        /// <returns>O listă de obiecte Vot.</returns>
        public List<Vot> GetAllVoturi()
        {
            List<Vot> voturi = new List<Vot>();
            string query = "SELECT VotID, UserID, MelodieID, PuncteAlocate, DataVot FROM dbo.Voturi";
            try
            {
                DataTable dt = DatabaseHelper.GetDataTable(query, CommandType.Text);

                foreach (DataRow row in dt.Rows)
                {
                    voturi.Add(MapDataRowToVot(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la preluarea tuturor voturilor: {ex.Message}");
                // Returnează o listă goală în caz de eroare.
            }
            return voturi;
        }

        /// <summary>
        /// Preia toate voturile pentru un utilizator specific.
        /// </summary>
        /// <param name="userId">ID-ul utilizatorului.</param>
        /// <returns>O listă de voturi pentru utilizatorul specificat.</returns>
        public List<Vot> GetVoturiByUserId(int userId)
        {
            List<Vot> voturi = new List<Vot>();
            string query = "SELECT VotID, UserID, MelodieID, PuncteAlocate, DataVot FROM dbo.Voturi WHERE UserID = @UserID";
            SqlParameter paramUserId = new SqlParameter("@UserID", SqlDbType.Int) { Value = userId };
            try
            {
                DataTable dt = DatabaseHelper.GetDataTable(query, CommandType.Text, paramUserId);

                foreach (DataRow row in dt.Rows)
                {
                    voturi.Add(MapDataRowToVot(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la preluarea voturilor pentru utilizatorul ID {userId}: {ex.Message}");
            }
            return voturi;
        }

        /// <summary>
        /// Verifică dacă un utilizator (cu ID valid > 0) a înregistrat deja voturi.
        /// </summary>
        /// <param name="userId">ID-ul utilizatorului.</param>
        /// <returns>True dacă utilizatorul a votat, altfel false.</returns>
        public bool UserAFostVotat(int userId)
        {
            // Verifică dacă un utilizator persistant (AppUserID > 0) are voturi.
            // Utilizatorii temporari (ID <= 0) nu sunt verificați în acest mod.
            if (userId <= 0) return false; 

            string query = "SELECT COUNT(1) FROM dbo.Voturi WHERE UserID = @UserID";
            SqlParameter paramUserId = new SqlParameter("@UserID", SqlDbType.Int) { Value = userId };
            try
            {
                object result = DatabaseHelper.ExecuteScalar(query, CommandType.Text, paramUserId);
                return result != null && Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la verificarea dacă utilizatorul {userId} a votat: {ex.Message}");
                return false; // Presupune că utilizatorul nu a votat sau a apărut o eroare.
            }
        }

        /// <summary>
        /// Șterge toate voturile asociate unei melodii specifice.
        /// Utilă dacă ștergerea în cascadă nu este definită în baza de date.
        /// </summary>
        /// <param name="melodieId">ID-ul melodiei.</param>
        public void StergeVoturiPentruMelodie(int melodieId) 
        {
            string query = "DELETE FROM dbo.Voturi WHERE MelodieID = @MelodieID";
            SqlParameter paramId = new SqlParameter("@MelodieID", SqlDbType.Int) { Value = melodieId };
            try
            {
                DatabaseHelper.ExecuteNonQuery(query, CommandType.Text, paramId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la ștergerea voturilor pentru melodia {melodieId}: {ex.Message}");
            }
        }

        /// <summary>
        /// Șterge toate voturile asociate unui utilizator specific.
        /// </summary>
        /// <param name="userId">ID-ul utilizatorului.</param>
        public void StergeVoturiPentruUser(int userId)
        {
            string query = "DELETE FROM dbo.Voturi WHERE UserID = @UserID";
            SqlParameter paramId = new SqlParameter("@UserID", SqlDbType.Int) { Value = userId };
            try
            {
                DatabaseHelper.ExecuteNonQuery(query, CommandType.Text, paramId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la ștergerea voturilor pentru utilizatorul {userId}: {ex.Message}");
            }
        }

        /// <summary>
        /// Înregistrează o listă de voturi pentru un utilizator.
        /// Această metodă adaugă fiecare vot individual.
        /// </summary>
        /// <param name="voturiUtilizator">Lista de voturi de înregistrat.</param>
        /// <returns>True dacă toate voturile au fost adăugate cu succes, altfel false.</returns>
        public bool InregistreazaVoturi(List<Vot> voturiUtilizator)
        {
            if (voturiUtilizator == null || !voturiUtilizator.Any()) return true; // Sau false, în funcție de comportamentul dorit.

            bool allSuccessful = true;
            foreach (var vot in voturiUtilizator)
            {
                if (!AdaugaVot(vot)) // Refolosește metoda AdaugaVot.
                {
                    allSuccessful = false;
                    Console.WriteLine($"Eroare la salvarea votului pentru UserID: {vot.UserID}, MelodieID: {vot.MelodieID}");
                    // Opțional, se poate opri procesul sau arunca o excepție aici.
                }
            }
            return allSuccessful;
        }

        /// <summary>
        /// Mapează un DataRow la un obiect Vot.
        /// </summary>
        /// <param name="row">DataRow-ul de mapat.</param>
        /// <returns>Un obiect Vot populat.</returns>
        private Vot MapDataRowToVot(DataRow row)
        {
            return new Vot
            {
                VotID = Convert.ToInt32(row["VotID"]),
                UserID = DatabaseHelper.GetNullableValueOrDefault<int>(row["UserID"]),
                MelodieID = Convert.ToInt32(row["MelodieID"]),
                PuncteAlocate = Convert.ToInt32(row["PuncteAlocate"]),
                DataVot = Convert.ToDateTime(row["DataVot"])
            };
        }
    }
} 