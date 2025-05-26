using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
// using System.Data; // Unused
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using MelodiiApp.UserInterface.Helpers;

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

        /// <summary>
        /// Eveniment declanșat după ce voturile au fost trimise cu succes.
        /// </summary>
        public event EventHandler VotesSubmitted;

        private const int MAX_POINTS_PER_VOTER = 6;
        private List<Melodie> _listaMelodiiInMemory; // Full list of all melodies, loaded once
        private BindingList<MelodieVotDisplay> _melodiiPentruVotBindingList; // For DataGridView display and point editing
        private Dictionary<int, int> _puncteAlocateTemporar; // MelodieID -> Puncte
        private int _puncteRamase;

        // State management fields
        private string _currentVotingUsername = null;
        private bool _isInitialLoadForUser = true;

        // Helper class for DataGridView display
        private class MelodieVotDisplay : INotifyPropertyChanged
        {
            public int MelodieID { get; set; }
            public string Titlu { get; set; }
            public string Artist { get; set; }
            private int _puncte;
            public int Puncte
            {
                get => _puncte;
                set
                {
                    // Ensure points are not negative (though button logic should prevent this)
                    int newPuncte = Math.Max(0, value); 
                    if (_puncte != newPuncte)
                    {
                        _puncte = newPuncte;
                        OnPropertyChanged(nameof(Puncte));
                    }
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            _listaMelodiiInMemory = new List<Melodie>(); // Initialize
            _melodiiPentruVotBindingList = new BindingList<MelodieVotDisplay>();
            _puncteAlocateTemporar = new Dictionary<int, int>();

            SetupDataGridView();
            ThemeHelper.ApplyUserControlTheme(this);

            if (lblPuncteDisponibile != null) ThemeHelper.ApplyLabelStyle(lblPuncteDisponibile);
            if (lblPuncteDisponibileInfo != null) ThemeHelper.ApplyLabelStyle(lblPuncteDisponibileInfo);
            if (lblStatusVoteaza != null) ThemeHelper.ApplyLabelStyle(lblStatusVoteaza);
            if (lblSelecteazaIntervievat != null) lblSelecteazaIntervievat.Visible = false;
            if (cmbIntervievati != null) cmbIntervievati.Visible = false;

            this.VisibleChanged += VoteazaControl_VisibleChanged;
            this.btnCautaMelodie.Click += BtnCautaMelodie_Click;
            this.txtCautaMelodie.KeyDown += TxtCautaMelodie_KeyDown;
            this.dgvMelodiiPentruVot.CellContentClick += DgvMelodiiPentruVot_CellContentClick;
            this.dgvMelodiiPentruVot.DataError += DgvMelodiiPentruVot_DataError;

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
            if (colPuncteAlocate != null) 
            {
                colPuncteAlocate.DataPropertyName = "Puncte";
                colPuncteAlocate.ReadOnly = true; // Ensure this column is NOT editable directly
            }
            
            dgvMelodiiPentruVot.DataSource = _melodiiPentruVotBindingList;
            // ThemeHelper.ApplyDataGridViewStyle will be called as part of ApplyUserControlTheme
        }

        private void FullResetForNewSession()
        {
            ResetFormState(); 
            _currentVotingUsername = null;
            _isInitialLoadForUser = true; 
        }

        private void VoteazaControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                var currentUser = AuthService.CurrentLoggedInUser;

                if (currentUser == null || currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    if (_currentVotingUsername != null || !_isInitialLoadForUser) 
                    {
                        FullResetForNewSession();
                    }
                    ShowStatus("Doar utilizatorii standard autentificați pot aloca puncte.", ThemeHelper.RedAccent, 0);
                    EnableVotingFields(false);
                    SetMainVotingControlsVisibility(false); 
                    lblTitluPanou.Text = "Votare indisponibilă";
                    if (pnlConfirmareVot != null) pnlConfirmareVot.Visible = false;
                    return;
                }

                // Check if the user has already voted
                if (_votRepository.UserAFostVotat(currentUser.Id))
                {
                    FullResetForNewSession(); // Clear any lingering state
                    lblTitluPanou.Text = "Vot Înregistrat";
                    ShowStatus($"Utilizatorul {currentUser.Username} a votat deja.", ThemeHelper.DarkBlue, 0);
                    EnableVotingFields(false); // Disable all voting inputs
                    SetMainVotingControlsVisibility(false); // Hide search, grid, etc.
                    btnFinalizeazaAlocarea.Visible = false; // Explicitly hide finalize button
                    if (pnlConfirmareVot != null) pnlConfirmareVot.Visible = false; // Ensure confirmation panel is hidden
                    _isInitialLoadForUser = false; // Mark as processed for this visibility change
                    _currentVotingUsername = currentUser.Username; // Set current user to prevent re-entry into voting logic
                    return;
                }

                if (_currentVotingUsername != currentUser.Username || _isInitialLoadForUser)
                {
                    FullResetForNewSession(); 
                    _currentVotingUsername = currentUser.Username;
                    _isInitialLoadForUser = false; 

                    lblTitluPanou.Text = "Alocare Puncte Vot";
                    SetMainVotingControlsVisibility(true);
                    EnableVotingFields(true);
                    LoadAllMelodii(); // Load all songs initially
                    ShowStatus($"Pregătit pentru votare, {currentUser.Username}. Puncte disponibile: {MAX_POINTS_PER_VOTER}. Căutați sau selectați din listă.", Color.DarkGreen, 4500);
                    if (pnlConfirmareVot != null) pnlConfirmareVot.Visible = false;
                    txtCautaMelodie.Focus();
                }
                else
                {
                    // Same user returning to an active session
                    if(!_melodiiPentruVotBindingList.Any() && _listaMelodiiInMemory.Any())
                    {
                        PopulateDataGridView(_listaMelodiiInMemory); // Repopulate if filtered list was empty
                    }
                    lblTitluPanou.Text = "Alocare Puncte Vot";
                    EnableVotingFields(true); 
                    UpdatePuncteRamaseDisplay(); 

                    if (pnlConfirmareVot != null && pnlConfirmareVot.Visible)
                    {
                        SetMainVotingControlsVisibility(false);
                    }
                    else
                    {
                        SetMainVotingControlsVisibility(true);
                        txtCautaMelodie.Focus();
                    }
                }
            }
        }
        
        private void ResetFormState()
        {
            txtCautaMelodie.Clear();
            // _listaMelodiiInMemory is not cleared here, it's the master list
            _melodiiPentruVotBindingList.Clear();
            ResetPuncteAllocation();
            EnableVotingFields(true);
            lblStatusVoteaza.Visible = false;
            if (pnlConfirmareVot != null) pnlConfirmareVot.Visible = false;
            SetMainVotingControlsVisibility(true);
        }

        private void ResetPuncteAllocation()
        {
            _puncteAlocateTemporar.Clear();
            _puncteRamase = MAX_POINTS_PER_VOTER;
            UpdatePuncteRamaseDisplay();
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
            string searchTerm = txtCautaMelodie.Text.Trim().ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                PopulateDataGridView(_listaMelodiiInMemory); // Show all if search is cleared
                ShowStatus("Afișare toate melodiile.", ThemeHelper.TextColorOnDark, 2000);
            }
            else
            {
                var melodiiFiltrate = _listaMelodiiInMemory
                    .Where(m => (m.Titlu?.ToLowerInvariant().Contains(searchTerm) ?? false) || 
                                (m.Artist?.ToLowerInvariant().Contains(searchTerm) ?? false))
                    .ToList();

                PopulateDataGridView(melodiiFiltrate);
                if (!melodiiFiltrate.Any())
                {
                    ShowStatus("Nicio melodie găsită conform criteriilor.", ThemeHelper.RedAccent, 3000);
                }
                else
                {
                    ShowStatus($"{melodiiFiltrate.Count} melodii găsite.", ThemeHelper.TextColorOnDark, 2000);
                }
            }
        }
        
        private void PopulateDataGridView(List<Melodie> melodiiSursa)
        {
            _melodiiPentruVotBindingList.Clear();
            foreach (var melodie in melodiiSursa)
            {
                var displayItem = new MelodieVotDisplay
                {
                    MelodieID = melodie.MelodieID,
                    Titlu = melodie.Titlu,
                    Artist = melodie.Artist,
                    Puncte = _puncteAlocateTemporar.ContainsKey(melodie.MelodieID) ? _puncteAlocateTemporar[melodie.MelodieID] : 0
                };
                _melodiiPentruVotBindingList.Add(displayItem);
            }
            dgvMelodiiPentruVot.Refresh(); // Ensure UI updates.
            if (!melodiiSursa.Any() && _listaMelodiiInMemory.Any()) // Check if filter resulted in empty but master list has songs
            {
                // This case is handled by BtnCautaMelodie_Click status message
            }
            else if (!_listaMelodiiInMemory.Any())
            {
                 ShowStatus("Nu există melodii în sistem pentru a vota.", ThemeHelper.RedAccent, 0);
                 EnableVotingFields(false);
            }
        }

        private void DgvMelodiiPentruVot_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Console.WriteLine($"DataGridView DataError: {e.Exception.Message} at row {e.RowIndex}, col {e.ColumnIndex}");
            e.ThrowException = false;
        }

        private void DgvMelodiiPentruVot_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("[DEBUG] DgvMelodiiPentruVot_CellContentClick Fired.");
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvMelodiiPentruVot.Columns["colAdaugaPunct"].Index)
            {
                Console.WriteLine("[DEBUG] '+' button clicked.");
                if (_puncteRamase <= 0)
                {
                    ShowStatus($"Ați alocat deja toate cele {MAX_POINTS_PER_VOTER} puncte.", ThemeHelper.RedAccent, 2000);
                    Console.WriteLine("[DEBUG] No points remaining.");
                    return;
                }
                var melodieVotDisplay = _melodiiPentruVotBindingList[e.RowIndex];
                if (melodieVotDisplay.Puncte >= MAX_POINTS_PER_VOTER)
                {
                     ShowStatus($"Melodia '{melodieVotDisplay.Titlu}' are deja maximul de {MAX_POINTS_PER_VOTER} puncte.", ThemeHelper.RedAccent, 2500);
                     Console.WriteLine($"[DEBUG] Melodia '{melodieVotDisplay.Titlu}' already has max points.");
                    return;
                }
                melodieVotDisplay.Puncte++;
                Console.WriteLine($"[DEBUG] Melodia '{melodieVotDisplay.Titlu}' Puncte incremented to: {melodieVotDisplay.Puncte}");
                
                if (melodieVotDisplay.Puncte > 0)
                {
                    _puncteAlocateTemporar[melodieVotDisplay.MelodieID] = melodieVotDisplay.Puncte;
                    Console.WriteLine($"[DEBUG] _puncteAlocateTemporar updated for MelodieID {melodieVotDisplay.MelodieID} to {melodieVotDisplay.Puncte}");
                }
                else 
                {
                    _puncteAlocateTemporar.Remove(melodieVotDisplay.MelodieID);
                     Console.WriteLine($"[DEBUG] _puncteAlocateTemporar removed for MelodieID {melodieVotDisplay.MelodieID}");
                }
                RecalculateAndUpdatePuncteRamase();
                Console.WriteLine($"[DEBUG] RecalculateAndUpdatePuncteRamase called. _puncteRamase: {_puncteRamase}");
                
                if (e.RowIndex >= 0 && e.RowIndex < _melodiiPentruVotBindingList.Count)
                {
                    Console.WriteLine($"[DEBUG] Attempting _melodiiPentruVotBindingList.ResetItem({e.RowIndex})");
                    _melodiiPentruVotBindingList.ResetItem(e.RowIndex);
                    Console.WriteLine("[DEBUG] _melodiiPentruVotBindingList.ResetItem called.");
                }
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
            else lblPuncteDisponibile.ForeColor = ThemeHelper.TextColorOnDark; 
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
            lblStatusVoteaza.Text = message;
            lblStatusVoteaza.ForeColor = color;
            lblStatusVoteaza.Visible = true;

            // Check if the user is an admin - they shouldn't see voting related messages as primary status.
            var currentUserForStatus = AuthService.CurrentLoggedInUser;
            bool isAdmin = currentUserForStatus != null && currentUserForStatus.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase);

            if (delay > 0 && !isAdmin) // Only auto-hide for non-admins or if delay is positive
            {
                await Task.Delay(delay);
                if (lblStatusVoteaza.Text == message) 
                {
                    lblStatusVoteaza.Visible = false;
                }
            }
        }

        private void BtnFinalizeazaAlocarea_Click(object sender, EventArgs e)
        {
            Console.WriteLine("[DEBUG] BtnFinalizeazaAlocarea_Click Fired.");
            var currentUser = AuthService.CurrentLoggedInUser;
            if (currentUser == null || currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                 ShowStatus("Operațiune nepermisă. Doar utilizatorii standard autentificați pot aloca puncte.", ThemeHelper.RedAccent, 0);
                 Console.WriteLine("[DEBUG] Finalize denied: Invalid user or Admin.");
                return;
            }
            Console.WriteLine($"[DEBUG] Before points check. _puncteRamase: {_puncteRamase}");
            if (_puncteRamase != 0)
            {
                ShowStatus($"Trebuie să alocați exact {MAX_POINTS_PER_VOTER} puncte. Mai aveți {_puncteRamase} puncte de alocat/ajustat.", ThemeHelper.RedAccent, 0);
                Console.WriteLine("[DEBUG] Finalize denied: _puncteRamase is not 0.");
                return;
            }
            bool hasAnyPointsAllocated = _melodiiPentruVotBindingList.Any(m => m.Puncte > 0);
            Console.WriteLine($"[DEBUG] Before any points allocated check. hasAnyPointsAllocated: {hasAnyPointsAllocated}");
            if (!hasAnyPointsAllocated)
            {
                ShowStatus("Nu ați alocat niciun punct. Vă rugăm alocați puncte melodiilor.", ThemeHelper.RedAccent, 0);
                Console.WriteLine("[DEBUG] Finalize denied: No points allocated to any song.");
                return;
            }
            string voterNameToConfirm = currentUser.Username;
            if (lblConfirmareIntervievat != null) lblConfirmareIntervievat.Text = voterNameToConfirm;
            if (lblConfirmareIntervievatInfo != null) lblConfirmareIntervievatInfo.Text = "Votant:";
            var rezumatVoturi = _melodiiPentruVotBindingList
                .Where(m => m.Puncte > 0)
                .Select(m => $"- {m.Titlu} ({m.Artist}): {m.Puncte} puncte");
            if (txtRezumatVoturi != null) txtRezumatVoturi.Text = string.Join(Environment.NewLine, rezumatVoturi);
            
            Console.WriteLine("[DEBUG] About to call SwitchToConfirmareMode().");
            SwitchToConfirmareMode();
        }

        private void SwitchToAlocareMode()
        {
            if (pnlConfirmareVot != null) pnlConfirmareVot.Visible = false;
            SetMainVotingControlsVisibility(true); // This will show voting controls
        }

        private void SwitchToConfirmareMode()
        {
            if (pnlConfirmareVot != null) pnlConfirmareVot.Visible = true;
            SetMainVotingControlsVisibility(false); // Hide main voting area
            if (lblStatusVoteaza != null) lblStatusVoteaza.Visible = false; 
        }

        private async void BtnConfirmaVotFinal_Click(object sender, EventArgs e)
        {
            var currentUser = AuthService.CurrentLoggedInUser;

            if (currentUser == null || currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                ShowStatus("Eroare internă: Utilizatorul curent nu este valid pentru confirmarea votului.", ThemeHelper.RedAccent);
                SwitchToAlocareMode(); 
                return;
            }
            
            int userId = currentUser.Id; // Changed from UserID to Id, to match AppUser.cs

            _votRepository.StergeVoturiPentruUser(userId); // Changed from StergeVoturiPentruIntervievat

            var voturiDeSalvat = new List<Vot>();
            foreach (var item in _melodiiPentruVotBindingList.Where(m => m.Puncte > 0))
            {
                voturiDeSalvat.Add(new Vot(userId, item.MelodieID, item.Puncte)); // Pass userId
            }

            if (!voturiDeSalvat.Any())
            {
                 ShowStatus("Nu au fost alocate puncte pentru a fi salvate.", ThemeHelper.RedAccent, 0); // Changed Color.Orange to RedAccent
                 SwitchToAlocareMode();
                 return;
            }

            if (_votRepository.InregistreazaVoturi(voturiDeSalvat))
            {
                ShowStatus("Voturile au fost înregistrate cu succes!", Color.Green, 2500); // Or a ThemeHelper success color
                VotesSubmitted?.Invoke(this, EventArgs.Empty); // Trigger event
                
                FullResetForNewSession(); // Reset state for the next voting session by this or another user

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
            FullResetForNewSession(); // User explicitly cancels, so reset session state
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void LoadAllMelodii()
        {
            _listaMelodiiInMemory = _melodieRepository.GetAllMelodii().OrderBy(m => m.Titlu).ToList();
            PopulateDataGridView(_listaMelodiiInMemory);
            // No need to show status here, VisibleChanged handles it.
        }
    }
} 