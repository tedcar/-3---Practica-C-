using MelodiiApp.DataAccess;
using MelodiiApp.UserInterface.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MelodiiApp.UserInterface.Controls
{
    /// <summary>
    /// Control utilizator pentru afișarea și exportul listei complete de participanți (intervievați).
    /// </summary>
    public partial class ListaParticipantiControl : UserControl
    {
        private readonly IntervievatRepository _intervievatRepository;
        // Componentele UI (dgvParticipanti, btnRefreshLista, etc.) sunt acum definite în fișierul Designer.cs

        /// <summary>
        /// Eveniment declanșat când utilizatorul dorește să navigheze înapoi.
        /// </summary>
        public event EventHandler RequestGoBack;

        /// <summary>
        /// Constructorul controlului ListaParticipantiControl.
        /// Inițializează componentele, repository-ul și aplică tema.
        /// </summary>
        public ListaParticipantiControl()
        {
            InitializeComponent(); // Inițializează componentele definite în Designer.cs
            _intervievatRepository = new IntervievatRepository();
            // InitializeCustomComponents(); // Eliminat - componentele sunt acum în Designer.cs
            SetupDataGridView(); // Configurează coloanele DataGridView.
            ThemeHelper.ApplyUserControlTheme(this); // Aplică tema generală controlului.

            // Atașează manual evenimentele pentru butoanele definite în Designer
            if (this.btnRefreshLista != null) this.btnRefreshLista.Click += (s, e) => RefreshData();
            if (this.btnExportPlaceholder != null) this.btnExportPlaceholder.Click += BtnExportPlaceholder_Click;
            if (this.btnGoBack != null) this.btnGoBack.Click += BtnGoBack_Click;

            this.VisibleChanged += (s, e) => { if (this.Visible) RefreshData(); };
        }

        // Metoda InitializeCustomComponents a fost eliminată.
        // Toată inițializarea componentelor vizuale se face acum în ListaParticipantiControl.Designer.cs

        /// <summary>
        /// Configurează manual coloanele și proprietățile pentru DataGridView-ul participanților.
        /// </summary>
        private void SetupDataGridView()
        {
            if (dgvParticipanti == null) return;
            dgvParticipanti.AutoGenerateColumns = false;
            dgvParticipanti.Columns.Clear();
            dgvParticipanti.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdCol", DataPropertyName = "IntervievatID", HeaderText = "ID", Width = 50 });
            dgvParticipanti.Columns.Add(new DataGridViewTextBoxColumn { Name = "NumeCol", DataPropertyName = "NumeComplet", HeaderText = "Nume Complet", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvParticipanti.Columns.Add(new DataGridViewTextBoxColumn { Name = "VarstaCol", DataPropertyName = "Varsta", HeaderText = "Vârstă", Width = 70 });
            dgvParticipanti.Columns.Add(new DataGridViewTextBoxColumn { Name = "LocalitateCol", DataPropertyName = "Localitate", HeaderText = "Localitate", Width = 150 });
            dgvParticipanti.Columns.Add(new DataGridViewTextBoxColumn { Name = "ScorCol", DataPropertyName = "ScorTotalConcurs", HeaderText = "Scor Concurs", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
        }

        /// <summary>
        /// Reîmprospătează datele afișate în lista de participanți.
        /// Preia toți intervievații din repository, îi ordonează și îi afișează în DataGridView.
        /// </summary>
        public void RefreshData()
        {
            if (dgvParticipanti == null) return;
            try
            {
                var participanti = _intervievatRepository.GetAllIntervievati()
                                     .OrderByDescending(i => i.ScorTotalConcurs)
                                     .ThenBy(i => i.NumeComplet)
                                     .ToList();
                dgvParticipanti.DataSource = null;
                dgvParticipanti.DataSource = participanti;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea listei de participanți: {ex.Message}", "Eroare Listă", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul de revenire.
        /// Declanșează evenimentul RequestGoBack.
        /// </summary>
        private void BtnGoBack_Click(object sender, EventArgs e)
        {
            RequestGoBack?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul de export.
        /// Extrage lista de participanți și o salvează într-un fișier CSV.
        /// </summary>
        private void BtnExportPlaceholder_Click(object sender, EventArgs e)
        {
            try
            {
                var participanti = _intervievatRepository.GetAllIntervievati()
                                     .OrderByDescending(i => i.ScorTotalConcurs)
                                     .ThenBy(i => i.NumeComplet)
                                     .ToList();

                if (participanti == null || !participanti.Any())
                {
                    MessageBox.Show("Nu există participanți pentru export.", "Export Anulat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    sfd.FileName = "Lista_Completa_Participanti.csv";
                    sfd.Title = "Salvare Export Listă Completă Participanți";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        StringBuilder csvContent = new StringBuilder();
                        // Adaugă antetul CSV.
                        csvContent.AppendLine("ID Intervievat,Nume Complet,Vârstă,Localitate,Scor Concurs");

                        foreach (var p in participanti)
                        {
                            csvContent.AppendLine($"{p.IntervievatID},{EscapeCsvField(p.NumeComplet)},{p.Varsta},{EscapeCsvField(p.Localitate)},{p.ScorTotalConcurs}");
                        }

                        File.WriteAllText(sfd.FileName, csvContent.ToString(), Encoding.UTF8);
                        MessageBox.Show($"Datele au fost exportate cu succes în fișierul:\n{sfd.FileName}", "Export Finalizat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la exportul datelor: {ex.Message}", "Eroare Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Formatează un șir de caractere pentru a fi utilizat în siguranță într-un fișier CSV.
        /// Încadrează șirul în ghilimele dacă conține virgulă, ghilimele sau salt la linie nouă,
        /// și dublează ghilimelele existente în interiorul șirului.
        /// </summary>
        /// <param name="field">Șirul de formatat.</param>
        /// <returns>Șirul formatat pentru CSV.</returns>
        private string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field)) return string.Empty;
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                return $"\"{field.Replace("\"", "\"\"")}\"";
            }
            return field;
        }
    }
} 