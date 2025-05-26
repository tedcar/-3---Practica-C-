using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess.DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MelodiiApp.DataAccess
{
    /// <summary>
    /// Repository pentru gestionarea predicțiilor făcute de intervievați.
    /// </summary>
    public class PredictieRepository
    {
        /// <summary>
        /// Salvează predicția unui intervievat.
        /// Dacă există deja o predicție pentru acest intervievat, aceasta va fi suprascrisă.
        /// </summary>
        /// <param name="predictie">Predicția de salvat.</param>
        /// <returns>True dacă salvarea a reușit.</returns>
        public bool SalveazaPredictie(Predictie predictie)
        {
            if (predictie == null) return false;

            // Check if a prediction already exists for this IntervievatID using the DB method.
            Predictie existingPrediction = GetPredictieByIntervievatId(predictie.IntervievatID);

            string query;
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@IntervievatID", SqlDbType.Int) { Value = predictie.IntervievatID },
                // Handle nullable MelodieIDs
                new SqlParameter("@MelodieID_Loc1", SqlDbType.Int) { Value = predictie.MelodieID_Loc1 == 0 ? DBNull.Value : (object)predictie.MelodieID_Loc1 },
                new SqlParameter("@MelodieID_Loc2", SqlDbType.Int) { Value = predictie.MelodieID_Loc2 == 0 ? DBNull.Value : (object)predictie.MelodieID_Loc2 },
                new SqlParameter("@MelodieID_Loc3", SqlDbType.Int) { Value = predictie.MelodieID_Loc3 == 0 ? DBNull.Value : (object)predictie.MelodieID_Loc3 }
            };

            if (existingPrediction != null)
            {
                query = @"
                    UPDATE Predictii 
                    SET MelodieID_Loc1 = @MelodieID_Loc1, MelodieID_Loc2 = @MelodieID_Loc2, MelodieID_Loc3 = @MelodieID_Loc3, DataPredictie = GETDATE()
                    WHERE IntervievatID = @IntervievatID";
                // No need to add PredictieID to parameters for update by IntervievatID
            }
            else
            {
                query = @"
                    INSERT INTO Predictii (IntervievatID, MelodieID_Loc1, MelodieID_Loc2, MelodieID_Loc3, DataPredictie) 
                    VALUES (@IntervievatID, @MelodieID_Loc1, @MelodieID_Loc2, @MelodieID_Loc3, GETDATE());
                    SELECT SCOPE_IDENTITY();";
            }

            try
            {
                if (existingPrediction != null)
                {
                    int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, CommandType.Text, parameters.ToArray());
                    return rowsAffected > 0;
                }
                else
                {
                    object newIdObj = DatabaseHelper.ExecuteScalar(query, CommandType.Text, parameters.ToArray());
                    if (newIdObj != null && newIdObj != DBNull.Value)
                    {
                        predictie.PredictieID = Convert.ToInt32(newIdObj);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving prediction: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Returnează predicția pentru un intervievat specific.
        /// </summary>
        /// <param name="intervievatId">ID-ul intervievatului.</param>
        /// <returns>Predicția sau null dacă nu există.</returns>
        public Predictie GetPredictieByIntervievatId(int intervievatId)
        {
            string query = "SELECT PredictieID, IntervievatID, MelodieID_Loc1, MelodieID_Loc2, MelodieID_Loc3, DataPredictie FROM Predictii WHERE IntervievatID = @IntervievatID";
            SqlParameter paramId = new SqlParameter("@IntervievatID", SqlDbType.Int) { Value = intervievatId };
            try
            {
                DataTable dt = DatabaseHelper.GetDataTable(query, CommandType.Text, paramId);
                if (dt.Rows.Count > 0)
                {
                    return MapDataRowToPredictie(dt.Rows[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting prediction by intervievat ID {intervievatId}: {ex.Message}");
            }
            return null;
        }

        /// <summary>
        /// Returnează toate predicțiile.
        /// </summary>
        public List<Predictie> GetAllPredictii() // Generally less used if accessed by IntervievatID
        {
            List<Predictie> predictii = new List<Predictie>();
            string query = "SELECT PredictieID, IntervievatID, MelodieID_Loc1, MelodieID_Loc2, MelodieID_Loc3, DataPredictie FROM Predictii";
            try
            {
                DataTable dt = DatabaseHelper.GetDataTable(query, CommandType.Text);
                foreach (DataRow row in dt.Rows)
                {
                    predictii.Add(MapDataRowToPredictie(row));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all predictii: {ex.Message}");
            }
            return predictii;
        }
        
        /// <summary>
        /// Șterge toate predicțiile asociate unui intervievat.
        /// Util pentru când un intervievat este șters din sistem.
        /// </summary>
        /// <param name="intervievatId">ID-ul intervievatului.</param>
        public bool StergePredictiiPentruIntervievat(int intervievatId)
        {
            // This will be handled by ON DELETE CASCADE if Intervievat is deleted.
            // However, if we want to clear predictions without deleting the interviewee:
            string query = "DELETE FROM Predictii WHERE IntervievatID = @IntervievatID";
            SqlParameter paramId = new SqlParameter("@IntervievatID", SqlDbType.Int) { Value = intervievatId };
            try
            {
                int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, CommandType.Text, paramId);
                return rowsAffected > 0; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting predictions for intervievat {intervievatId}: {ex.Message}");
                return false;
            }
        }

        // New method to handle orphaned predictions if a song is deleted and DB doesn't cascade NULL
        public void AnuleazaPredictiiPentruMelodie(int melodieId)
        {
            string query = @"
                UPDATE Predictii SET MelodieID_Loc1 = NULL WHERE MelodieID_Loc1 = @MelodieID;
                UPDATE Predictii SET MelodieID_Loc2 = NULL WHERE MelodieID_Loc2 = @MelodieID;
                UPDATE Predictii SET MelodieID_Loc3 = NULL WHERE MelodieID_Loc3 = @MelodieID;";
            SqlParameter paramId = new SqlParameter("@MelodieID", SqlDbType.Int) { Value = melodieId };
            try
            {
                DatabaseHelper.ExecuteNonQuery(query, CommandType.Text, paramId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error nullifying predictions for melodie {melodieId}: {ex.Message}");
            }
        }

        private Predictie MapDataRowToPredictie(DataRow row)
        {
            return new Predictie
            {
                PredictieID = Convert.ToInt32(row["PredictieID"]),
                IntervievatID = Convert.ToInt32(row["IntervievatID"]),
                MelodieID_Loc1 = DatabaseHelper.GetNullableValueOrDefault<int>(row["MelodieID_Loc1"]),
                MelodieID_Loc2 = DatabaseHelper.GetNullableValueOrDefault<int>(row["MelodieID_Loc2"]),
                MelodieID_Loc3 = DatabaseHelper.GetNullableValueOrDefault<int>(row["MelodieID_Loc3"]),
                DataPredictie = Convert.ToDateTime(row["DataPredictie"])
            };
        }
    }
} 