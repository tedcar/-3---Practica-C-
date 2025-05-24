using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks; // For potential delays in status messages
using MelodiiApp.UserInterface.Forms;
using MelodiiApp.UserInterface.Helpers; // Added for ThemeHelper

namespace MelodiiApp.UserInterface.Controls
{
    /// <summary>
    /// Control pentru gestionarea intervievaților (vizualizare, adăugare, modificare, ștergere).
    /// </summary>
    public partial class GestioneazaIntervievatiControl : UserControl
    {
        private readonly IntervievatRepository _intervievatRepository;
        
        /// <summary>
        /// Eveniment declanșat când se solicită afișarea panoului de adăugare intervievat.
        /// </summary>
        public event EventHandler RequestShowAdaugaIntervievatPanel;
        // public event EventHandler<int> RequestShowModificaIntervievatPanel; // For future use

        /// <summary>
        /// Initializează o nouă instanță a clasei <see cref="GestioneazaIntervievatiControl"/>.
        /// </summary>
        public GestioneazaIntervievatiControl()
        {
            InitializeComponent();
            _intervievatRepository = new IntervievatRepository();
            
            SetupDataGridView(); // Setup columns before applying theme to DGV
            ThemeHelper.ApplyUserControlTheme(this); // Apply theme to all controls including DGV
            
            this.Load += GestioneazaIntervievatiControl_Load;
            btnAdaugaIntervievat.Click += BtnAdaugaIntervievat_Click;
            btnModificaIntervievat.Click += BtnModificaIntervievat_Click;
            btnStergeIntervievat.Click += BtnStergeIntervievat_Click;
            btnRefreshIntervievati.Click += BtnRefreshIntervievati_Click;
            dgvIntervievati.CellDoubleClick += DgvIntervievati_CellDoubleClick;
        }

        private void SetupDataGridView()
        {
            dgvIntervievati.AutoGenerateColumns = false;
            dgvIntervievati.Columns.Clear(); // Clear existing columns if any, before adding new ones
            dgvIntervievati.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdCol", DataPropertyName = "IntervievatID", HeaderText = "ID", Width = 50 });
            dgvIntervievati.Columns.Add(new DataGridViewTextBoxColumn { Name = "NumeCol", DataPropertyName = "NumeComplet", HeaderText = "Nume Complet", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvIntervievati.Columns.Add(new DataGridViewTextBoxColumn { Name = "VarstaCol", DataPropertyName = "Varsta", HeaderText = "Vârstă", Width = 70 });
            dgvIntervievati.Columns.Add(new DataGridViewTextBoxColumn { Name = "LocalitateCol", DataPropertyName = "Localitate", HeaderText = "Localitate", Width = 150 });
            dgvIntervievati.Columns.Add(new DataGridViewTextBoxColumn { Name = "ScorCol", DataPropertyName = "ScorTotalConcurs", HeaderText = "Scor Concurs", Width = 100 });
        }

        private void GestioneazaIntervievatiControl_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        /// <summary>
        /// Reîmprospătează datele afișate în DataGridView.
        /// </summary>
        public void RefreshData()
        {
            var intervievati = _intervievatRepository.GetAllIntervievati();
            dgvIntervievati.DataSource = null; // Clear previous data source
            dgvIntervievati.DataSource = intervievati.ToList(); // Use ToList() to ensure it's a concrete list for binding
            if (this.Visible) // Only show status if the control is active
            {
                 ShowStatus("Lista de intervievați a fost actualizată.", ThemeHelper.MidBlue, 2000); // Use theme color
            }
        }

        private void BtnAdaugaIntervievat_Click(object sender, EventArgs e)
        {
            RequestShowAdaugaIntervievatPanel?.Invoke(this, EventArgs.Empty);
        }

        private void BtnModificaIntervievat_Click(object sender, EventArgs e)
        {
            if (dgvIntervievati.SelectedRows.Count > 0)
            {
                EditSelectedIntervievat();
            }
            else
            {
                ShowStatus("Vă rugăm selectați un intervievat pentru a-l modifica.", ThemeHelper.RedAccent, 3000);
            }
        }

        private void DgvIntervievati_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                EditSelectedIntervievat();
            }
        }

        private void EditSelectedIntervievat()
        {
            var row = dgvIntervievati.SelectedRows.Count > 0 ? dgvIntervievati.SelectedRows[0] : null;
            if (row == null) return;

            int id = (int)row.Cells["IdCol"].Value;
            var interv = _intervievatRepository.GetIntervievatById(id);
            if (interv == null) { ShowStatus("Nu s-a găsit intervievatul.", ThemeHelper.RedAccent, 3000); return; }

            using (var frm = new EditIntervievatForm(interv))
            {
                ThemeHelper.ApplyFormTheme(frm); // Style the dialog form as well
                if (frm.ShowDialog() == DialogResult.OK && frm.Saved)
                {
                    RefreshData();
                    ShowStatus("Intervievat actualizat.", Color.Green, 2500); // Or ThemeHelper.DarkBlue for success
                }
            }
        }

        private void BtnStergeIntervievat_Click(object sender, EventArgs e)
        {
            if (dgvIntervievati.SelectedRows.Count > 0)
            {
                var selectedRow = dgvIntervievati.SelectedRows[0];
                int intervievatId = (int)selectedRow.Cells["IdCol"].Value;
                string numeIntervievat = selectedRow.Cells["NumeCol"].Value.ToString();

                DialogResult confirmation = MessageBox.Show($"Sunteți sigur că doriți să ștergeți intervievatul '{numeIntervievat}' (ID: {intervievatId})?\nAceasta va șterge și toate voturile asociate.", 
                                                           "Confirmare Ștergere", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmation == DialogResult.Yes)
                {
                    bool success = _intervievatRepository.StergeIntervievat(intervievatId);
                    if (success)
                    {
                        ShowStatus($"Intervievatul '{numeIntervievat}' a fost șters cu succes.", Color.Green, 3000);
                        RefreshData(); // Refresh list
                    }
                    else
                    {
                        ShowStatus($"Eroare la ștergerea intervievatului '{numeIntervievat}'.", ThemeHelper.RedAccent, 3000);
                    }
                }
                else
                {
                    ShowStatus("Operațiunea de ștergere a fost anulată.", ThemeHelper.MidBlue, 2000);
                }
            }
            else
            {
                ShowStatus("Vă rugăm selectați un intervievat pentru a-l șterge.", ThemeHelper.RedAccent, 3000);
            }
        }

        private void BtnRefreshIntervievati_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private async void ShowStatus(string message, Color color, int delay = 0)
        {
            lblStatusGestIntervievati.Text = message;
            ThemeHelper.StyleSpecificLabel(lblStatusGestIntervievati, color);
            lblStatusGestIntervievati.Visible = true;
            if (delay > 0)
            {
                await Task.Delay(delay);
                lblStatusGestIntervievati.Visible = false;
            }
        }
        /// <summary>
        /// Șterge mesajul din bara de stare.
        /// </summary>
        public void ClearStatus() // Public method to allow MainForm to clear status if needed
        {
            lblStatusGestIntervievati.Visible = false;
        }
    }
} 