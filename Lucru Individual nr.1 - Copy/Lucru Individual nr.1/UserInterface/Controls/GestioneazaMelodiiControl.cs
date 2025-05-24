using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using MelodiiApp.UserInterface.Helpers;

namespace MelodiiApp.UserInterface.Controls
{
    /// <summary>
    /// Control pentru gestionarea melodiilor (vizualizare, adăugare, modificare, ștergere).
    /// </summary>
    public partial class GestioneazaMelodiiControl : UserControl
    {
        private readonly MelodieRepository _melodieRepository;
        
        /// <summary>
        /// Eveniment declanșat când se solicită afișarea panoului de adăugare melodie.
        /// </summary>
        public event EventHandler RequestShowAdaugaMelodiePanel;
        // public event EventHandler<int> RequestShowModificaMelodiePanel; // For future use if passing ID

        /// <summary>
        /// Initializează o nouă instanță a clasei <see cref="GestioneazaMelodiiControl"/>.
        /// </summary>
        public GestioneazaMelodiiControl()
        {
            InitializeComponent();
            _melodieRepository = new MelodieRepository();
            
            SetupDataGridView(); // Setup columns before applying theme to DGV
            ThemeHelper.ApplyUserControlTheme(this); // Apply theme
            
            // Custom styling for StatusStrip and its label if not covered by general ApplyUserControlTheme
            if (statusStrip != null) 
            {
                statusStrip.BackColor = ThemeHelper.Cream; 
                statusStrip.ForeColor = ThemeHelper.TextOnLight;
                if(lblStatusGestMelodii != null) ThemeHelper.ApplyLabelStyle(lblStatusGestMelodii); // Ensure label on strip is styled
            }

            this.Load += GestioneazaMelodiiControl_Load;
            btnAdaugaMelodie.Click += BtnAdaugaMelodie_Click;
            btnModificaMelodie.Click += BtnModificaMelodie_Click;
            btnStergeMelodie.Click += BtnStergeMelodie_Click;
            btnRefreshMelodii.Click += BtnRefreshMelodii_Click;
        }

        private void SetupDataGridView()
        {
            dgvMelodii.AutoGenerateColumns = false;
            dgvMelodii.Columns.Clear(); // Clear any existing columns if SetupDataGridView is called multiple times
            dgvMelodii.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdCol", DataPropertyName = "MelodieID", HeaderText = "ID", Width = 50, ReadOnly = true });
            dgvMelodii.Columns.Add(new DataGridViewTextBoxColumn { Name = "TitluCol", DataPropertyName = "Titlu", HeaderText = "Titlu", Width = 200, ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvMelodii.Columns.Add(new DataGridViewTextBoxColumn { Name = "ArtistCol", DataPropertyName = "Artist", HeaderText = "Artist", Width = 150, ReadOnly = true });
            dgvMelodii.Columns.Add(new DataGridViewTextBoxColumn { Name = "GenCol", DataPropertyName = "GenMuzical", HeaderText = "Gen Muzical", Width = 120, ReadOnly = true });
            dgvMelodii.Columns.Add(new DataGridViewTextBoxColumn { Name = "AnCol", DataPropertyName = "AnLansare", HeaderText = "An Lansare", Width = 80, ReadOnly = true });
            dgvMelodii.Columns.Add(new DataGridViewTextBoxColumn { Name = "PunctajCol", DataPropertyName = "PunctajTotal", HeaderText = "Punctaj", Width = 70, ReadOnly = true });
            
            dgvMelodii.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMelodii.MultiSelect = false;
            dgvMelodii.AllowUserToAddRows = false;
            dgvMelodii.AllowUserToDeleteRows = false;
            dgvMelodii.AllowUserToResizeRows = false;
            dgvMelodii.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // To respect individual widths
            dgvMelodii.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
        }

        private void GestioneazaMelodiiControl_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// Reîmprospătează datele afișate în DataGridView.
        /// </summary>
        public void RefreshData()
        {
            try
            {
                var melodii = _melodieRepository.GetAllMelodii();
                dgvMelodii.DataSource = null; 
                dgvMelodii.DataSource = melodii.ToList(); 
                if (this.Visible)
                {
                    ShowStatus("Lista de melodii a fost actualizată.", ThemeHelper.MidBlue, 2000);
                }
            }
            catch (Exception ex)
            {
                ShowStatus($"Eroare la încărcarea melodiilor: {ex.Message}", ThemeHelper.RedAccent, 5000);
            }
        }

        private void BtnAdaugaMelodie_Click(object sender, EventArgs e)
        {
            RequestShowAdaugaMelodiePanel?.Invoke(this, EventArgs.Empty);
        }

        private void BtnModificaMelodie_Click(object sender, EventArgs e)
        {
            if (dgvMelodii.SelectedRows.Count > 0)
            {
                // Melodie selectedMelodie = dgvMelodii.SelectedRows[0].DataBoundItem as Melodie;
                // if (selectedMelodie != null) {
                //     RequestShowModificaMelodiePanel?.Invoke(this, selectedMelodie.MelodieID);
                // }
                ShowStatus("Funcționalitatea de modificare melodie nu este încă implementată.", ThemeHelper.RedAccent, 3000);
            }
            else
            {
                ShowStatus("Vă rugăm selectați o melodie pentru a o modifica.", ThemeHelper.MidBlue, 3000);
            }
        }

        private void BtnStergeMelodie_Click(object sender, EventArgs e)
        {
            if (dgvMelodii.SelectedRows.Count > 0)
            {
                var selectedRow = dgvMelodii.SelectedRows[0];
                int melodieId = (int)selectedRow.Cells["IdCol"].Value;
                string titluMelodie = selectedRow.Cells["TitluCol"].Value.ToString();

                DialogResult confirmation = MessageBox.Show($"Sunteți sigur că doriți să ștergeți melodia '{titluMelodie}' (ID: {melodieId})?\nAceastă acțiune este ireversibilă.",
                                                           "Confirmare Ștergere Melodie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmation == DialogResult.Yes)
                {
                    try
                    {
                        bool success = _melodieRepository.DeleteMelodie(melodieId);
                        if (success)
                        {
                            ShowStatus($"Melodia '{titluMelodie}' a fost ștearsă cu succes.", Color.Green, 3000);
                            RefreshData(); 
                        }
                        else
                        {
                            ShowStatus($"Eroare la ștergerea melodiei '{titluMelodie}'. Melodia nu a fost găsită.", ThemeHelper.RedAccent, 3000);
                        }
                    }
                    catch(Exception ex)
                    {
                         ShowStatus($"Eroare critică la ștergerea melodiei: {ex.Message}", ThemeHelper.RedAccent, 5000);
                    }
                }
                else
                {
                    ShowStatus("Operațiunea de ștergere a fost anulată.", ThemeHelper.MidBlue, 2000);
                }
            }
            else
            {
                ShowStatus("Vă rugăm selectați o melodie pentru a o șterge.", ThemeHelper.MidBlue, 3000);
            }
        }

        private void BtnRefreshMelodii_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private async void ShowStatus(string message, Color foreColor, int delayMilliseconds = 0)
        {
            if (statusStrip != null && lblStatusGestMelodii != null)
            {
                ThemeHelper.StyleSpecificLabel(lblStatusGestMelodii, foreColor);
                lblStatusGestMelodii.Text = message;
                statusStrip.Visible = true;
                if (delayMilliseconds > 0)
                {
                    await Task.Delay(delayMilliseconds);
                    if (lblStatusGestMelodii.Text == message)
                    {
                         lblStatusGestMelodii.Text = string.Empty;
                    }
                }
            }
        }
        
        /// <summary>
        /// Șterge mesajul din bara de stare.
        /// </summary>
        public void ClearStatus()
        {
            if (lblStatusGestMelodii != null) 
            {
                lblStatusGestMelodii.Text = string.Empty;
            }
        }
    }
} 