using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using MelodiiApp.UserInterface.Helpers; // Added for ThemeHelper

namespace MelodiiApp.UserInterface.Controls
{
    /// <summary>
    /// Control pentru înregistrarea voturilor unui intervievat pentru melodii.
    /// </summary>
    public partial class VoteazaControl : UserControl
    {
        private readonly IntervievatRepository _intervievatRepository;
        private readonly MelodieRepository _melodieRepository;
        private readonly VotRepository _votRepository;

        /// <summary>
        /// Eveniment declanșat când se solicită închiderea acestui control.
        /// </summary>
        public event EventHandler RequestClose; // Event to notify MainForm to close this panel

        private const int MAX_POINTS_PER_VOTER = 6;
        private List<Melodie> _listaMelodiiCautate; // Full list from search
        private BindingList<MelodieVotDisplay> _melodiiPentruVotBindingList; // For DataGridView display and point editing
        private Dictionary<int, int> _puncteAlocateTemporar; // MelodieID -> Puncte
        private int _puncteRamase;

        // Helper class for DataGridView display
        private class MelodieVotDisplay
        {
            public int MelodieID { get; set; }
            public string Titlu { get; set; }
            public string Artist { get; set; }
            private int _puncte;
            public int Puncte // This will be bound to the DataGridView column for points
            {
                get => _puncte;
                set => _puncte = Math.Max(0, value); // Ensure points are not negative
            }
            // Constructor can be added if needed
        }

        /// <summary>
        /// Initializează o nouă instanță a clasei <see cref="VoteazaControl"/>.
        /// </summary>
        public VoteazaControl()
        {
            InitializeComponent();
            _intervievatRepository = new IntervievatRepository();
            _melodieRepository = new MelodieRepository();
            _votRepository = new VotRepository();
            _listaMelodiiCautate = new List<Melodie>();
            _melodiiPentruVotBindingList = new BindingList<MelodieVotDisplay>();
            _puncteAlocateTemporar = new Dictionary<int, int>();

            SetupDataGridView(); // Columns must be defined before ThemeHelper styles DGV
            ThemeHelper.ApplyUserControlTheme(this); // Apply theme to the whole control

            // Specific styling for labels that might have their text updated frequently or need emphasis
            if (lblPuncteDisponibile != null) ThemeHelper.ApplyLabelStyle(lblPuncteDisponibile); // Ensure it's styled
            if (lblPuncteDisponibileInfo != null) ThemeHelper.ApplyLabelStyle(lblPuncteDisponibileInfo);
            if (lblStatusVoteaza != null) ThemeHelper.ApplyLabelStyle(lblStatusVoteaza); // Initial style
            if (lblSelecteazaIntervievat != null) ThemeHelper.ApplyLabelStyle(lblSelecteazaIntervievat);
            // For confirmation panel, ThemeHelper.ApplyUserControlTheme should handle controls within pnlConfirmareVot
            // including lblConfirmareIntervievatInfo and lblRezumatVoturiInfo if they are standard labels.
            // txtRezumatVoturi will be styled as a TextBox.

            this.VisibleChanged += VoteazaControl_VisibleChanged;
            this.btnCautaMelodie.Click += BtnCautaMelodie_Click;
            this.txtCautaMelodie.KeyDown += TxtCautaMelodie_KeyDown;
            this.dgvMelodiiPentruVot.CellValueChanged += DgvMelodiiPentruVot_CellValueChanged;
            this.dgvMelodiiPentruVot.CellValidating += DgvMelodiiPentruVot_CellValidating;
            this.dgvMelodiiPentruVot.DataError += DgvMelodiiPentruVot_DataError; // Handle data entry errors

            this.btnFinalizeazaAlocarea.Click += BtnFinalizeazaAlocarea_Click;
            this.btnAnuleaza.Click += BtnAnuleaza_Click;
            this.btnConfirmaVotFinal.Click += BtnConfirmaVotFinal_Click;
            this.btnRevinoLaAlocare.Click += BtnRevinoLaAlocare_Click;
        }

        private void SetupDataGridView()
        {
            dgvMelodiiPentruVot.AutoGenerateColumns = false;
            // Assuming colMelodieId, colTitlu, colArtist, colPuncteAlocate are defined in Designer
            // We just set DataPropertyName and DataSource
            if (colMelodieId != null) colMelodieId.DataPropertyName = "MelodieID";
            if (colTitlu != null) colTitlu.DataPropertyName = "Titlu";
            if (colArtist != null) colArtist.DataPropertyName = "Artist";
            if (colPuncteAlocate != null) colPuncteAlocate.DataPropertyName = "Puncte";
            
            dgvMelodiiPentruVot.DataSource = _melodiiPentruVotBindingList;
            // ThemeHelper.ApplyDataGridViewStyle will be called as part of ApplyUserControlTheme
        }

        private void VoteazaControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                ResetFormState();
                PopulateIntervievati(); // For admin
                var currentUser = InMemoryAuthService.CurrentLoggedInUser;
                bool isAdmin = currentUser != null && currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
                lblSelecteazaIntervievat.Visible = isAdmin;
                cmbIntervievati.Visible = isAdmin;

                if (!isAdmin && currentUser?.IntervievatID != null)
                {
                    cmbIntervievati.SelectedValue = currentUser.IntervievatID.Value;
                    cmbIntervievati.Enabled = false; // User cannot change self
                    CheckIfUserAlreadyVoted(currentUser.IntervievatID.Value);
                }
                else if (isAdmin)
                {
                     cmbIntervievati.Enabled = true;
                     cmbIntervievati.SelectedIndexChanged += CmbIntervievati_SelectedIndexChanged; // Attach only if admin can change
                }
                SwitchToAlocareMode(); 
            }
            else
            {
                if (cmbIntervievati.Visible && cmbIntervievati.Enabled) 
                {
                     cmbIntervievati.SelectedIndexChanged -= CmbIntervievati_SelectedIndexChanged;
                }
            }
        }
        
        private void CmbIntervievati_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIntervievati.SelectedValue is int intervievatId)
            {
                CheckIfUserAlreadyVoted(intervievatId);
            }
            else
            {
                ResetPuncteAllocation();
                EnableVotingFields(true);
            }
        }

        private void CheckIfUserAlreadyVoted(int intervievatId)
        {
            ResetPuncteAllocation();
            EnableVotingFields(true); 
            ShowStatus($"Pregătit pentru votare pentru intervievatul selectat.", Color.DarkGreen, 2000);
        }


        private void ResetFormState()
        {
            txtCautaMelodie.Clear();
            _listaMelodiiCautate.Clear();
            _melodiiPentruVotBindingList.Clear();
            ResetPuncteAllocation();
            EnableVotingFields(true);
            lblStatusVoteaza.Visible = false;
            pnlConfirmareVot.Visible = false;
            SetMainVotingControlsVisibility(true);
        }

        private void ResetPuncteAllocation()
        {
            _puncteAlocateTemporar.Clear();
            _puncteRamase = MAX_POINTS_PER_VOTER;
            UpdatePuncteRamaseDisplay();
            foreach (var item in _melodiiPentruVotBindingList)
            {
                item.Puncte = 0;
            }
            dgvMelodiiPentruVot.Refresh(); 
        }
        
        private void SetMainVotingControlsVisibility(bool visible)
        {
            lblCautaMelodie.Visible = visible;
            txtCautaMelodie.Visible = visible;
            btnCautaMelodie.Visible = visible;
            dgvMelodiiPentruVot.Visible = visible;
            lblPuncteDisponibileInfo.Visible = visible;
            lblPuncteDisponibile.Visible = visible;
            btnFinalizeazaAlocarea.Visible = visible;
        }


        private void PopulateIntervievati()
        {
            if (lblSelecteazaIntervievat.Visible) 
            {
                var intervievati = _intervievatRepository.GetAllIntervievati().OrderBy(i => i.NumeComplet).ToList();
                cmbIntervievati.DataSource = null; 
                cmbIntervievati.DisplayMember = "NumeComplet";
                cmbIntervievati.ValueMember = "IntervievatID";
                cmbIntervievati.DataSource = intervievati;
                cmbIntervievati.SelectedIndex = -1;
            }
        }
        
        private void TxtCautaMelodie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnCautaMelodie_Click(sender, e);
                e.SuppressKeyPress = true; 
            }
        }

        private void BtnCautaMelodie_Click(object sender, EventArgs e)
        {
            string searchTerm = txtCautaMelodie.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                _listaMelodiiCautate = _melodieRepository.GetAllMelodii().OrderBy(m => m.Titlu).ToList();
            }
            else
            {
                _listaMelodiiCautate = _melodieRepository.GetAllMelodii()
                    .Where(m => m.Titlu.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                m.Artist.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                    .OrderBy(m => m.Titlu)
                    .ToList();
            }

            _melodiiPentruVotBindingList.Clear();
            foreach (var melodie in _listaMelodiiCautate)
            {
                _melodiiPentruVotBindingList.Add(new MelodieVotDisplay
                {
                    MelodieID = melodie.MelodieID,
                    Titlu = melodie.Titlu,
                    Artist = melodie.Artist,
                    Puncte = _puncteAlocateTemporar.ContainsKey(melodie.MelodieID) ? _puncteAlocateTemporar[melodie.MelodieID] : 0
                });
            }
             if (!_listaMelodiiCautate.Any())
            {
                ShowStatus("Nicio melodie găsită pentru termenul căutat.", Color.Orange); // Could use ThemeHelper.RedAccent or a custom orange
            }
        }
        
        private void DgvMelodiiPentruVot_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            ShowStatus($"Eroare de format în coloana de puncte. Vă rugăm introduceți un număr.", ThemeHelper.RedAccent);
            e.Cancel = true; 
        }


        private void DgvMelodiiPentruVot_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvMelodiiPentruVot.Columns[e.ColumnIndex].Name == colPuncteAlocate.Name)
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out int puncteIntrodus) || puncteIntrodus < 0)
                {
                    dgvMelodiiPentruVot.Rows[e.RowIndex].ErrorText = "Introduceți un număr valid (0 sau mai mare).";
                    e.Cancel = true;
                    return;
                }
                 dgvMelodiiPentruVot.Rows[e.RowIndex].ErrorText = String.Empty; 
            }
        }
        
        private void DgvMelodiiPentruVot_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvMelodiiPentruVot.Columns[e.ColumnIndex].Name == colPuncteAlocate.Name)
            {
                var displayItem = _melodiiPentruVotBindingList[e.RowIndex];
                int melodieId = displayItem.MelodieID;
                int puncteNoiPentruMelodie = displayItem.Puncte; 

                int totalPuncteAlocateActual = 0;
                foreach(var item in _melodiiPentruVotBindingList)
                {
                    if(item.MelodieID != melodieId) 
                    {
                         totalPuncteAlocateActual += item.Puncte;
                    }
                }
                
                int potentialTotal = totalPuncteAlocateActual + puncteNoiPentruMelodie;

                if (potentialTotal > MAX_POINTS_PER_VOTER)
                {
                    int puncteExces = potentialTotal - MAX_POINTS_PER_VOTER;
                    puncteNoiPentruMelodie = Math.Max(0, puncteNoiPentruMelodie - puncteExces);
                    displayItem.Puncte = puncteNoiPentruMelodie; 
                    ShowStatus($"Totalul de puncte nu poate depăși {MAX_POINTS_PER_VOTER}. Puncte ajustate.", Color.Orange); // Or ThemeHelper.RedAccent
                }
                
                if (puncteNoiPentruMelodie > 0)
                {
                    _puncteAlocateTemporar[melodieId] = puncteNoiPentruMelodie;
                }
                else
                {
                    if (_puncteAlocateTemporar.ContainsKey(melodieId))
                    {
                        _puncteAlocateTemporar.Remove(melodieId);
                    }
                }
                RecalculateAndUpdatePuncteRamase();
            }
        }

        private void RecalculateAndUpdatePuncteRamase()
        {
            int totalAlocat = _melodiiPentruVotBindingList.Sum(item => item.Puncte);
            _puncteRamase = MAX_POINTS_PER_VOTER - totalAlocat;
            UpdatePuncteRamaseDisplay();
        }

        private void UpdatePuncteRamaseDisplay()
        {
            lblPuncteDisponibile.Text = $"{_puncteRamase} puncte rămase din {MAX_POINTS_PER_VOTER}.";
            if (_puncteRamase == 0) lblPuncteDisponibile.ForeColor = Color.Green; // Or a success color from ThemeHelper
            else if (_puncteRamase < 0) lblPuncteDisponibile.ForeColor = ThemeHelper.RedAccent;
            else lblPuncteDisponibile.ForeColor = ThemeHelper.TextOnLight; 
        }

        private void EnableVotingFields(bool enable)
        {
            txtCautaMelodie.Enabled = enable;
            btnCautaMelodie.Enabled = enable;
            dgvMelodiiPentruVot.Enabled = enable;
            btnFinalizeazaAlocarea.Enabled = enable;
        }

        private async void ShowStatus(string message, Color color, int delay = 4000)
        {
            if (lblStatusVoteaza != null)
            {
                ThemeHelper.StyleSpecificLabel(lblStatusVoteaza, color);
                lblStatusVoteaza.Text = message;
                lblStatusVoteaza.Visible = true;
                if (delay > 0)
                {
                    await Task.Delay(delay);
                    if (lblStatusVoteaza.Text == message) 
                    {
                        lblStatusVoteaza.Visible = false;
                    }
                }
            }
        }

        private void BtnFinalizeazaAlocarea_Click(object sender, EventArgs e)
        {
            var currentUser = InMemoryAuthService.CurrentLoggedInUser;
            bool isAdmin = currentUser != null && currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
            
            if (isAdmin && cmbIntervievati.SelectedValue == null)
            {
                ShowStatus("Vă rugăm selectați un intervievat pentru a înregistra voturile.", ThemeHelper.RedAccent);
                return;
            }
             if (!isAdmin && (currentUser?.IntervievatID == null))
            {
                 ShowStatus("Utilizatorul curent nu este asociat unui intervievat valid.", ThemeHelper.RedAccent);
                return;
            }

            if (_puncteRamase != 0)
            {
                ShowStatus($"Trebuie să alocați exact {MAX_POINTS_PER_VOTER} puncte. Mai aveți de alocat/șters: {_puncteRamase} puncte.", ThemeHelper.RedAccent);
                return;
            }
            if (!_puncteAlocateTemporar.Any(kvp => kvp.Value > 0))
            {
                ShowStatus("Nu ați alocat niciun punct. Vă rugăm alocați puncte melodiilor.", ThemeHelper.RedAccent);
                return;
            }

            int intervievatIdToConfirm;
            string intervievatNumeToConfirm;

            if(isAdmin)
            {
                intervievatIdToConfirm = (int)cmbIntervievati.SelectedValue;
                Intervievat selectedIntervievatAdmin = _intervievatRepository.GetIntervievatById(intervievatIdToConfirm);
                intervievatNumeToConfirm = selectedIntervievatAdmin?.NumeComplet ?? "N/A";
            }
            else
            {
                intervievatIdToConfirm = currentUser.IntervievatID.Value;
                 Intervievat currentIntervievat = _intervievatRepository.GetIntervievatById(intervievatIdToConfirm);
                intervievatNumeToConfirm = currentIntervievat?.NumeComplet ?? InMemoryAuthService.CurrentLoggedInUser.Username;
            }
            
            lblConfirmareIntervievat.Text = intervievatNumeToConfirm;
            
            var rezumatVoturi = _melodiiPentruVotBindingList
                .Where(m => m.Puncte > 0)
                .Select(m => $"- {m.Titlu} ({m.Artist}): {m.Puncte} puncte");
            txtRezumatVoturi.Text = string.Join(Environment.NewLine, rezumatVoturi);
            
            SwitchToConfirmareMode();
        }

        private void SwitchToAlocareMode()
        {
            pnlConfirmareVot.Visible = false;
            SetMainVotingControlsVisibility(true);
             var currentUser = InMemoryAuthService.CurrentLoggedInUser;
             bool isAdmin = currentUser != null && currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
             lblSelecteazaIntervievat.Visible = isAdmin;
             cmbIntervievati.Visible = isAdmin;
             cmbIntervievati.Enabled = isAdmin; 
        }

        private void SwitchToConfirmareMode()
        {
            pnlConfirmareVot.Visible = true;
            SetMainVotingControlsVisibility(false);
            lblSelecteazaIntervievat.Visible = false;
            cmbIntervievati.Visible = false;
            lblStatusVoteaza.Visible = false; 
        }

        private async void BtnConfirmaVotFinal_Click(object sender, EventArgs e)
        {
            int intervievatId;
            var currentUser = InMemoryAuthService.CurrentLoggedInUser;
            bool isAdmin = currentUser != null && currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase);

            if (isAdmin)
            {
                if (cmbIntervievati.SelectedValue == null)
                {
                    ShowStatus("Eroare internă: Intervievatul nu a fost selectat pentru confirmare.", ThemeHelper.RedAccent);
                    SwitchToAlocareMode(); 
                    return;
                }
                intervievatId = (int)cmbIntervievati.SelectedValue;
            }
            else
            {
                intervievatId = currentUser?.IntervievatID ?? 0;
                if (intervievatId == 0)
                {
                    ShowStatus("Eroare internă: Utilizatorul curent nu este valid pentru confirmare.", ThemeHelper.RedAccent);
                    SwitchToAlocareMode(); 
                    return;
                }
            }

            _votRepository.StergeVoturiPentruIntervievat(intervievatId); 

            var voturiDeSalvat = new List<Vot>();
            foreach (var item in _melodiiPentruVotBindingList.Where(m => m.Puncte > 0))
            {
                voturiDeSalvat.Add(new Vot(intervievatId, item.MelodieID, item.Puncte));
            }

            if (!voturiDeSalvat.Any())
            {
                 ShowStatus("Nu au fost alocate puncte pentru a fi salvate.", Color.Orange); // Or ThemeHelper.RedAccent
                 SwitchToAlocareMode();
                 return;
            }

            if (_votRepository.InregistreazaVoturi(voturiDeSalvat))
            {
                ShowStatus("Voturile au fost înregistrate cu succes!", Color.Green, 2500); // Or a ThemeHelper success color
                await Task.Delay(2500);
                RequestClose?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                ShowStatus("Eroare la salvarea finală a voturilor. Încercați din nou.", ThemeHelper.RedAccent);
                SwitchToAlocareMode(); 
            }
        }

        private void BtnRevinoLaAlocare_Click(object sender, EventArgs e)
        {
            SwitchToAlocareMode();
        }

        private void BtnAnuleaza_Click(object sender, EventArgs e)
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
} 