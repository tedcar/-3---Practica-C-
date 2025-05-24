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
                var currentUser = InMemoryAuthService.CurrentLoggedInUser;
                bool isUserRole = currentUser != null && currentUser.Role.Equals("User", StringComparison.OrdinalIgnoreCase);
                bool isAdminRole = currentUser != null && currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase);

                if (isUserRole)
                {
                    lblSelecteazaIntervievat.Visible = false;
                    cmbIntervievati.Visible = false;
                    cmbIntervievati.Enabled = false;
                    PopulateIntervievati(); // Call to clear/reset, but it won't be used for selection by User

                    if (currentUser?.Id != null) // User votes are tied to their AppUser.Id
                    {
                        // CheckIfUserAlreadyVoted will use CurrentLoggedInUser.Id implicitly or explicitly
                        CheckIfUserAlreadyVoted(currentUser.Id); 
                    }
                    else
                    {
                        ShowStatus("Utilizator invalid. Votarea nu este posibilă.", ThemeHelper.RedAccent);
                        EnableVotingFields(false);
                    }
                }
                else if (isAdminRole) // Admin should not be voting here
                {
                    lblSelecteazaIntervievat.Visible = true; // Or false, if admin truly has no interaction
                    cmbIntervievati.Visible = true;       // Or false
                    cmbIntervievati.Enabled = false;      // Disable admin interaction
                    PopulateIntervievati();               // Populate for display if needed, but not for voting
                    EnableVotingFields(false);            // Disable voting fields for Admin
                    ShowStatus("Administratorii nu pot vota prin acest panou.", Color.Orange, 0); // Persistent message
                }
                else // No valid role or user not logged in
                {
                    EnableVotingFields(false);
                    ShowStatus("Rol neautorizat sau utilizator nelogat. Votarea nu este disponibilă.", ThemeHelper.RedAccent, 0);
                }
                SwitchToAlocareMode();
            }
            else // Control is being hidden
            {
                // Detach event handlers if they were attached for admin (though admin shouldn't use this)
                if (cmbIntervievati.DataSource != null) // Check if it was populated
                {
                    cmbIntervievati.SelectedIndexChanged -= CmbIntervievati_SelectedIndexChanged;
                }
            }
        }
        
        private void CmbIntervievati_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This method is primarily for Admin interaction if they were to select an interviewee.
            // Since Admins are now blocked from voting here, this might be less critical,
            // but we'll keep the logic in case of future changes or for other roles.
            var currentUser = InMemoryAuthService.CurrentLoggedInUser;
            if (currentUser != null && currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                // If an admin selects an interviewee (though voting is disabled)
                if (cmbIntervievati.SelectedValue is int intervievatId)
                {
                     // Potentially show info about the selected interviewee, but not for voting.
                     // For now, just reset and state that admin cannot vote.
                    ResetPuncteAllocation();
                    EnableVotingFields(false);
                    ShowStatus($"Administratorii nu pot vota. (Intervievat selectat: {cmbIntervievati.Text})", Color.Orange);
                }
            }
        }

        private void CheckIfUserAlreadyVoted(int userIdOrIntervievatId) // Parameter name updated for clarity
        {
            // This method now checks votes based on the ID passed.
            // For Users, it's their AppUser.Id.
            // The VotRepository would need to know how to query based on this (e.g. if Vot.IntervievatID stores AppUser.Id for voters)
            
            // Example: bool hasVoted = _votRepository.AreVoturiInregistratePentruUtilizator(userIdOrIntervievatId);
            // For this task, we assume the existing logic in VotRepository using IntervievatID will be used,
            // and IntervievatID in the Vot table will store AppUser.Id for voters.
            // The original implementation of CheckIfUserAlreadyVoted did not actually check anything,
            // it just reset points and enabled fields. We'll keep that part.

            ResetPuncteAllocation(); // Resets points and enables fields
            EnableVotingFields(true); 
            
            var currentUser = InMemoryAuthService.CurrentLoggedInUser;
            if (currentUser != null && currentUser.Role.Equals("User", StringComparison.OrdinalIgnoreCase) && currentUser.Id == userIdOrIntervievatId)
            {
                 ShowStatus($"Pregătit pentru votare. Asigurați-vă că alocați toți cele {MAX_POINTS_PER_VOTER} puncte.", Color.DarkGreen, 3000);
            }
            // If it was an admin context (which is now blocked), a different message might show.
            // The original message "Pregătit pentru votare pentru intervievatul selectat." is less relevant for users.
        }


        private void ResetFormState()
        {
            txtCautaMelodie.Clear();
            _listaMelodiiCautate.Clear();
            _melodiiPentruVotBindingList.Clear();
            ResetPuncteAllocation();
            // EnableVotingFields(true); // This should be managed by role check in VisibleChanged
            lblStatusVoteaza.Visible = false;
            pnlConfirmareVot.Visible = false;
            SetMainVotingControlsVisibility(true); // Show voting area by default, VisibleChanged will disable if needed
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
            if (dgvMelodiiPentruVot.DataSource != null) dgvMelodiiPentruVot.Refresh(); 
        }
        
        private void SetMainVotingControlsVisibility(bool visible)
        {
            // These are the core controls for allocating points
            lblCautaMelodie.Visible = visible;
            txtCautaMelodie.Visible = visible;
            btnCautaMelodie.Visible = visible;
            dgvMelodiiPentruVot.Visible = visible;
            lblPuncteDisponibileInfo.Visible = visible;
            lblPuncteDisponibile.Visible = visible;
            btnFinalizeazaAlocarea.Visible = visible;
            // cmbIntervievati and lblSelecteazaIntervievat visibility is handled by VoteazaControl_VisibleChanged
        }


        private void PopulateIntervievati()
        {
            // This method populates the combobox. It's always called on VisibleChanged.
            // Visibility of the combobox itself is determined by the user role.
            var intervievati = _intervievatRepository.GetAllIntervievati().OrderBy(i => i.NumeComplet).ToList();
            cmbIntervievati.DataSource = null; 
            cmbIntervievati.DisplayMember = "NumeComplet";
            cmbIntervievati.ValueMember = "IntervievatID";
            cmbIntervievati.DataSource = intervievati;
            cmbIntervievati.SelectedIndex = -1; // Default to no selection
            
            // Detach first to prevent multiple subscriptions if called multiple times for admin
            cmbIntervievati.SelectedIndexChanged -= CmbIntervievati_SelectedIndexChanged;
            var currentUser = InMemoryAuthService.CurrentLoggedInUser;
            if (currentUser != null && currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase) && cmbIntervievati.Enabled)
            {
                // Only attach if admin and combo is enabled (though current logic disables it for voting)
                cmbIntervievati.SelectedIndexChanged += CmbIntervievati_SelectedIndexChanged;
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
            if (!txtCautaMelodie.Enabled) return; // Don't search if disabled

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

            var previouslyAllocatedPoints = new Dictionary<int, int>(_puncteAlocateTemporar);
            _melodiiPentruVotBindingList.Clear(); // Clear before re-populating
            _puncteAlocateTemporar.Clear(); // Clear temporary allocations, will be rebuilt from DGV

            foreach (var melodie in _listaMelodiiCautate)
            {
                _melodiiPentruVotBindingList.Add(new MelodieVotDisplay
                {
                    MelodieID = melodie.MelodieID,
                    Titlu = melodie.Titlu,
                    Artist = melodie.Artist,
                    // Restore points if this song was previously in the list and had points
                    Puncte = previouslyAllocatedPoints.ContainsKey(melodie.MelodieID) ? previouslyAllocatedPoints[melodie.MelodieID] : 0
                });
            }
            // After repopulating the binding list, update _puncteAlocateTemporar based on what's now in the DGV
            foreach(var item in _melodiiPentruVotBindingList.Where(mvd => mvd.Puncte > 0))
            {
                _puncteAlocateTemporar[item.MelodieID] = item.Puncte;
            }

            RecalculateAndUpdatePuncteRamase(); // Update points display based on current DGV items

             if (!_listaMelodiiCautate.Any())
            {
                ShowStatus("Nicio melodie găsită pentru termenul căutat.", Color.Orange);
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
            if (currentUser == null)
            {
                ShowStatus("Utilizator nelogat. Acțiune anulată.", ThemeHelper.RedAccent);
                SwitchToAlocareMode(); // Allow retry or exit
                return;
            }

            bool isUserRole = currentUser.Role.Equals("User", StringComparison.OrdinalIgnoreCase);

            if (!isUserRole) // Only Users (Voters) should be able to finalize. Admins are blocked by VisibleChanged logic.
            {
                ShowStatus("Doar utilizatorii pot finaliza alocarea voturilor. Acțiune blocată.", Color.Orange);
                EnableVotingFields(false); // Defensive: ensure fields are disabled
                SwitchToAlocareMode(); // Go back
                return;
            }

            // Proceed for User role
            if (currentUser.Id == 0) // Or some other invalid ID check like < 0
            {
                ShowStatus("ID utilizator invalid. Nu se poate finaliza votul.", ThemeHelper.RedAccent);
                SwitchToAlocareMode();
                return;
            }

            if (_puncteRamase != 0)
            {
                ShowStatus($"Trebuie să alocați exact {MAX_POINTS_PER_VOTER} puncte. Mai aveți de alocat/șters: {_puncteRamase} puncte.", ThemeHelper.RedAccent);
                return; // Stay in allocation mode for correction
            }
            if (!_puncteAlocateTemporar.Any(kvp => kvp.Value > 0))
            {
                ShowStatus("Nu ați alocat niciun punct. Vă rugăm alocați puncte melodiilor.", ThemeHelper.RedAccent);
                return; // Stay in allocation mode
            }
            
            lblConfirmareIntervievat.Text = $"Confirmare vot pentru: {currentUser.Username}";
            
            var rezumatVoturi = _melodiiPentruVotBindingList
                .Where(m => m.Puncte > 0)
                .Select(m => $"- {m.Titlu} ({m.Artist}): {m.Puncte} puncte");
            txtRezumatVoturi.Text = string.Join(Environment.NewLine, rezumatVoturi);
            
            SwitchToConfirmareMode();
        }

        private void SwitchToAlocareMode()
        {
            pnlConfirmareVot.Visible = false; // Hide confirmation panel
            SetMainVotingControlsVisibility(true); // Show main voting controls

            var currentUser = InMemoryAuthService.CurrentLoggedInUser;
            // Role check primarily happens in VoteazaControl_VisibleChanged.
            // This ensures that if SwitchToAlocareMode is called, the UI reflects the correct state.
            if (currentUser != null && currentUser.Role.Equals("User", StringComparison.OrdinalIgnoreCase))
            {
                lblSelecteazaIntervievat.Visible = false;
                cmbIntervievati.Visible = false;
                cmbIntervievati.Enabled = false;
                EnableVotingFields(true); // User should be able to vote
            }
            else // Admin or other roles (or null user)
            {
                // Admins should not be able to interact with voting.
                // This is defensive, as VisibleChanged should handle most of this.
                lblSelecteazaIntervievat.Visible = true; // Or false, depending on desired Admin view if this panel was visible
                cmbIntervievati.Visible = true;       // Or false
                cmbIntervievati.Enabled = false;      // Definitely disabled
                EnableVotingFields(false);            // Voting fields off for Admin
                if (currentUser != null && currentUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    ShowStatus("Administratorii nu pot aloca voturi.", Color.Orange, 0);
                }
            }
        }

        private void SwitchToConfirmareMode()
        {
            pnlConfirmareVot.Visible = true;
            SetMainVotingControlsVisibility(false); // Hide main voting section
            lblSelecteazaIntervievat.Visible = false; // Not relevant in confirmation for User
            cmbIntervievati.Visible = false;          // Not relevant
            lblStatusVoteaza.Visible = false;         // Clear any previous status messages
        }

        private async void BtnConfirmaVotFinal_Click(object sender, EventArgs e)
        {
            var currentUser = InMemoryAuthService.CurrentLoggedInUser;
            if (currentUser == null)
            {
                ShowStatus("Utilizator nelogat. Imposibil de confirmat votul.", ThemeHelper.RedAccent);
                SwitchToAlocareMode(); // Allow re-login or exit
                return;
            }

            bool isUserRole = currentUser.Role.Equals("User", StringComparison.OrdinalIgnoreCase);
            
            if (!isUserRole) // Only Users (Voters) should confirm.
            {
                ShowStatus("Doar utilizatorii (Voter) pot confirma voturi. Acțiune blocată.", Color.Orange);
                SwitchToAlocareMode(); 
                EnableVotingFields(false); // Ensure fields are disabled
                return;
            }

            // Proceed for User (Voter)
            if (currentUser.Id == 0) // Or some other invalid ID check
            {
                ShowStatus("ID utilizator invalid. Imposibil de confirmat votul.", ThemeHelper.RedAccent);
                SwitchToAlocareMode();
                return;
            }
            int voterId = currentUser.Id; // Use AppUser.Id as the identifier for the vote

            // Clear any previous votes for this voter (AppUser.Id)
            // This assumes Vot.IntervievatID is used to store AppUser.Id for voters.
            _votRepository.StergeVoturiPentruIntervievat(voterId); 

            var voturiDeSalvat = new List<Vot>();
            foreach (var item in _melodiiPentruVotBindingList.Where(m => m.Puncte > 0))
            {
                // Create Vot object using voterId (which is AppUser.Id)
                voturiDeSalvat.Add(new Vot(voterId, item.MelodieID, item.Puncte));
            }

            if (!voturiDeSalvat.Any())
            {
                 ShowStatus("Nu au fost alocate puncte pentru a fi salvate. Revizuiți alocarea.", Color.Orange);
                 SwitchToAlocareMode(); // Go back to allow allocation
                 return;
            }

            if (_votRepository.InregistreazaVoturi(voturiDeSalvat))
            {
                ShowStatus("Voturile au fost înregistrate cu succes!", Color.Green, 3000);
                EnableVotingFields(false); // Prevent re-submission
                await Task.Delay(3000); // Give user time to see success message
                RequestClose?.Invoke(this, EventArgs.Empty); // Close the control
            }
            else
            {
                ShowStatus("Eroare la salvarea finală a voturilor. Încercați din nou sau contactați suportul.", ThemeHelper.RedAccent);
                // Stay on confirmation screen or switch to allocation? 
                // Staying on confirmation allows user to try "Confirma" again if it was a transient issue.
                // Switching back to allocation might be safer if the error is persistent.
                // For now, let's let them retry or go back manually.
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