using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MelodiiApp.DataAccess; // Potential future use, ensure namespace is updated
using MaterialSkin;
using MaterialSkin.Controls;
using MelodiiApp.UserInterface.Controls; // Added for UserControls
using MelodiiApp.UserInterface.Forms; // Required for PreziceTopMelodiiForm
using MelodiiApp.Core.Models; // For UserRole enum
using MelodiiApp.Core.DomainModels; // Added for Intervievat and other domain models
using System.IO; // Added for File operations

namespace MelodiiApp.UserInterface.Forms
{
    public partial class MainForm : MaterialSkin.Controls.MaterialForm
    {
        private AdaugaMelodieControl _adaugaMelodieControl;
        private AdaugaIntervievatControl _adaugaIntervievatControl;
        private GestioneazaIntervievatiControl _gestioneazaIntervievatiControl;
        private GestioneazaMelodiiControl _gestioneazaMelodiiControl;
        private VoteazaControl _voteazaControl;
        private TopNMelodiiControl _topNMelodiiControl;
        private TopNIntervievatiControl _topNIntervievatiControl;
        private PreziceTopMelodiiControl _preziceTopMelodiiControl;
        // private ListaParticipantiControl _listaParticipantiControl; // Old control, to be replaced
        private ListaVotantiControl _listaVotantiControl; // New control for voters list
        // private System.Windows.Forms.TabPage tabDespre; // This will be handled by the designer

        private UserRole _currentUserRole; // Removed default assignment
        private readonly MelodieRepository _melodieRepository;         // Added repository instances
        private readonly IntervievatRepository _intervievatRepository; // Added repository instances

        public MainForm(UserRole userRole) // Added UserRole parameter
        {
            InitializeComponent();
            _currentUserRole = userRole; // Assign passed role
            _melodieRepository = new MelodieRepository();         // Initialize
            _intervievatRepository = new IntervievatRepository(); // Initialize
            InitializeMaterialSkinManager();
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5"); // Cream background for the form
            InitializeUserControls();
            AttachButtonEventHandlers();
            ApplyCustomStylingToTabButtons();
            ConfigureUIForRole(_currentUserRole);
        }

        private void InitializeMaterialSkinManager()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            // To achieve a dark header (#003049) with light text, and a light main window background (#fdf0d5)
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK; // Makes app bar text white
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.BlueGrey800, // Closest to #003049 for app bar
                Primary.BlueGrey900, // Darker variant
                Primary.Cyan700,     // Lighter variant (less critical if overriding manually)
                Accent.Red700,       // Accent #c1121f 
                TextShade.WHITE
            );
        }

        private void InitializeUserControls()
        {
            _adaugaMelodieControl = new AdaugaMelodieControl { Dock = DockStyle.Fill, Visible = false };
            _adaugaIntervievatControl = new AdaugaIntervievatControl { Dock = DockStyle.Fill, Visible = false };
            _gestioneazaIntervievatiControl = new GestioneazaIntervievatiControl { Dock = DockStyle.Fill, Visible = false };
            _gestioneazaMelodiiControl = new GestioneazaMelodiiControl { Dock = DockStyle.Fill, Visible = false };
            _voteazaControl = new VoteazaControl { Dock = DockStyle.Fill, Visible = false };
            _preziceTopMelodiiControl = new PreziceTopMelodiiControl { Dock = DockStyle.Fill, Visible = false };
            _topNMelodiiControl = new TopNMelodiiControl { Dock = DockStyle.Fill, Visible = false };
            _topNIntervievatiControl = new TopNIntervievatiControl { Dock = DockStyle.Fill, Visible = false };
            // _listaParticipantiControl = new ListaParticipantiControl { Dock = DockStyle.Fill, Visible = false }; // Old control initialization
            _listaVotantiControl = new ListaVotantiControl { Dock = DockStyle.Fill, Visible = false }; // Initialize new control

            this.tabMelodii.Controls.AddRange(new Control[] { _adaugaMelodieControl, _gestioneazaMelodiiControl, _topNMelodiiControl });
            this.tabIntervievati.Controls.AddRange(new Control[] { _adaugaIntervievatControl, _gestioneazaIntervievatiControl, _topNIntervievatiControl });
            this.tabVoteazaMelodii.Controls.Add(_voteazaControl); // Only voting control here
            this.tabManagementPredictii.Controls.Add(_preziceTopMelodiiControl); // Prediction control here
            
            // Remove the old control from the tab and add the new one
            // this.tabRapoarte.Controls.Remove(_listaParticipantiControl); // Ensure old one is removed if it was added by designer
            this.tabRapoarte.Controls.Clear(); // Clear all controls first to be safe
            this.tabRapoarte.Controls.Add(this.btnListaParticipanti); // Re-add button if it was on the tab directly
            this.tabRapoarte.Controls.Add(this.btnExportParticipantiSub18); // Re-add button
            this.tabRapoarte.Controls.Add(_listaVotantiControl); // Add the new control
            // tabDespre does not have user controls, so no additions here.

            _adaugaMelodieControl.RequestClose += UserControl_RequestClose;
            _adaugaIntervievatControl.RequestClose += UserControl_RequestClose;
            _gestioneazaIntervievatiControl.RequestShowAdaugaIntervievatPanel += (s, e) => ShowControlInTabPage(this.tabIntervievati, _adaugaIntervievatControl);
            _gestioneazaMelodiiControl.RequestShowAdaugaMelodiePanel += (s, e) => ShowControlInTabPage(this.tabMelodii, _adaugaMelodieControl);
            _voteazaControl.RequestClose += UserControl_RequestClose;
            _preziceTopMelodiiControl.RequestClose += UserControl_RequestClose;
        }
        
        private void ApplyCustomStylingToTabButtons()
        {
            Color btnBackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc"); // Mid Blue
            Color btnForeColor = Color.White; // White text on Mid Blue
            Color hoverColor = System.Drawing.ColorTranslator.FromHtml("#003049"); // Dark Blue on hover

            foreach (TabPage tabPage in mainTabControl.TabPages)
            {
                // Style Buttons that are direct children of TabPage (used for navigation within tab, not UserControls)
                var buttons = tabPage.Controls.OfType<Button>();
                foreach (var btn in buttons)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = btnBackColor;
                    btn.ForeColor = btnForeColor;
                    btn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.Height = 40;
                    btn.MinimumSize = new Size(180, 40);
                    
                    // Store original color for MouseLeave
                    Color originalColor = btn.BackColor;
                    btn.MouseEnter += (sender, e) => ((Button)sender).BackColor = hoverColor;
                    btn.MouseLeave += (sender, e) => ((Button)sender).BackColor = originalColor;
                }
            }
            mainTabControl.Font = new Font("Segoe UI", 10F); 
            // TabPage BackColor is set in Designer to #fdf0d5
            // Ensure text color on tab pages (for labels, etc.) is dark
            mainTabControl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049"); // Dark blue text for tab headers
        }

        private void AttachButtonEventHandlers()
        {
            // TabMelodii
            btnAdaugaMelodie.Click += (s, e) => ShowControlInTabPage(this.tabMelodii, _adaugaMelodieControl);
            btnGestioneazaMelodii.Click += (s, e) => ShowControlInTabPage(this.tabMelodii, _gestioneazaMelodiiControl);
            btnVeziClasamentMelodii.Click += (s, e) => ShowControlInTabPage(this.tabMelodii, _topNMelodiiControl);

            // TabIntervievati
            btnAdaugaIntervievat.Click += (s, e) => ShowControlInTabPage(this.tabIntervievati, _adaugaIntervievatControl);
            btnGestioneazaIntervievati.Click += (s, e) => ShowControlInTabPage(this.tabIntervievati, _gestioneazaIntervievatiControl);
            btnVeziClasamentIntervievati.Click += (s, e) => ShowControlInTabPage(this.tabIntervievati, _topNIntervievatiControl);

            // TabVoteazaMelodii (User)
            btnInregistreazaVoturi.Click += (s, e) => ShowControlInTabPage(this.tabVoteazaMelodii, _voteazaControl);

            // TabManagementPredictii (Admin)
            btnInregistreazaPredictii.Click += (s, e) => ShowControlInTabPage(this.tabManagementPredictii, _preziceTopMelodiiControl); 

            // TabDespre
            // btnDespre.Click += BtnDespre_Click; // Removed
            // TabAdministrare
            btnActualizeazaClasamente.Click += BtnActualizeazaClasamente_Click; 
            // TabRapoarte
            btnListaParticipanti.Click += BtnListaParticipanti_Click; // This will now show ListaVotantiControl
            btnExportParticipantiSub18.Click += BtnExportParticipantiSub18_Click;

            // Logout Button
            if (this.Controls.OfType<MaterialSkin.Controls.MaterialButton>().Any(btn => btn.Name == "btnLogout"))
            {
                var logoutButton = this.Controls.OfType<MaterialSkin.Controls.MaterialButton>().First(btn => btn.Name == "btnLogout");
                logoutButton.Click += btnLogout_Click;
            }
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear the currently logged-in user session
            InMemoryAuthService.CurrentLoggedInUser = null;

            // Create a new instance of LoginForm
            LoginForm loginForm = new LoginForm();
            loginForm.Show();

            // Close the current MainForm
            this.Close();
        }

        private void ConfigureUIForRole(UserRole role)
        {
            _currentUserRole = role;
            bool isAdmin = (role == UserRole.Admin);

            // Admin View Logic
            if (isAdmin)
            {
                if (tabMelodii != null) { tabMelodii.Text = "Melodii (Admin)"; tabMelodii.Visible = true; }
                if (tabIntervievati != null) { tabIntervievati.Text = "Intervievați (Admin)"; tabIntervievati.Visible = true; }
                if (tabManagementPredictii != null) { tabManagementPredictii.Text = "Management Predicții"; tabManagementPredictii.Visible = true; }
                if (tabAdministrare != null) { tabAdministrare.Text = "Administrare Clasamente"; tabAdministrare.Visible = true; }
                if (tabRapoarte != null) { tabRapoarte.Text = "Rapoarte"; tabRapoarte.Visible = true; }
                if (tabDespre != null) { tabDespre.Text = "Despre"; tabDespre.Visible = true; }

                if (tabVoteazaMelodii != null) { tabVoteazaMelodii.Visible = false; }

                // Default selected tab for Admin
                if (mainTabControl.TabPages.Contains(tabMelodii))
                    mainTabControl.SelectedTab = tabMelodii;
                else if (mainTabControl.TabPages.Contains(tabDespre)) // Fallback
                    mainTabControl.SelectedTab = tabDespre;
                else 
                {
                    var firstVisibleAdminTab = mainTabControl.TabPages.Cast<TabPage>().FirstOrDefault(tp => tp.Visible);
                    if (firstVisibleAdminTab != null) mainTabControl.SelectedTab = firstVisibleAdminTab;
                }


                // Button visibility within Admin tabs (ensure they exist before accessing)
                // btnInregistreazaPredictii is on tabManagementPredictii
                // This button is part of the tab page directly, not a UserControl.
                // Its visibility is controlled by the tab's visibility.
                // If btnInregistreazaPredictii were a specific control variable in MainForm, you'd do:
                // if (btnInregistreazaPredictii != null) btnInregistreazaPredictii.Visible = true;
            }
            // User (Voter) View Logic
            else // UserRole.User
            {
                if (tabVoteazaMelodii != null) { tabVoteazaMelodii.Text = "Votează Melodii"; tabVoteazaMelodii.Visible = true; }
                if (tabDespre != null) { tabDespre.Text = "Despre"; tabDespre.Visible = true; }

                if (tabMelodii != null) { tabMelodii.Visible = false; }
                if (tabIntervievati != null) { tabIntervievati.Visible = false; }
                if (tabManagementPredictii != null) { tabManagementPredictii.Visible = false; }
                if (tabAdministrare != null) { tabAdministrare.Visible = false; }
                if (tabRapoarte != null) { tabRapoarte.Visible = false; }

                // Default selected tab for User
                if (mainTabControl.TabPages.Contains(tabVoteazaMelodii))
                    mainTabControl.SelectedTab = tabVoteazaMelodii;
                else if (mainTabControl.TabPages.Contains(tabDespre)) // Fallback
                    mainTabControl.SelectedTab = tabDespre;
                else
                {
                    var firstVisibleUserTab = mainTabControl.TabPages.Cast<TabPage>().FirstOrDefault(tp => tp.Visible);
                    if (firstVisibleUserTab != null) mainTabControl.SelectedTab = firstVisibleUserTab;
                }


                // Button visibility within User tabs
                // btnInregistreazaVoturi is on tabVoteazaMelodii.
                // Similar to admin, this button's visibility is controlled by the tab's visibility.
                // If btnInregistreazaVoturi were a specific control variable in MainForm, you'd do:
                // if (btnInregistreazaVoturi != null) btnInregistreazaVoturi.Visible = true;
            }
            
            // Ensure all UserControls on now-hidden tabs are also hidden.
            // This is more of a safeguard, as ShowControlInTabPage should handle it.
            foreach (TabPage tabPage in mainTabControl.TabPages)
            {
                if (!tabPage.Visible)
                {
                    foreach (Control ctrl in tabPage.Controls)
                    {
                        if (ctrl is UserControl)
                        {
                            ctrl.Visible = false;
                        }
                    }
                }
            }
        }

        private void ShowControlInTabPage(TabPage tabPage, Control controlToShow)
        {
            // Ensure the target tab is actually visible before trying to select it or show controls on it.
            // This check is crucial after ConfigureUIForRole might have hidden the tab.
            if (tabPage == null || !mainTabControl.TabPages.Contains(tabPage) || !tabPage.Visible)
            {
                // Attempt to find and select the first visible tab as a fallback.
                var firstVisible = mainTabControl.TabPages.Cast<TabPage>().FirstOrDefault(tp => tp.Visible);
                if (firstVisible != null)
                {
                    mainTabControl.SelectedTab = firstVisible;
                    // If we are falling back to a different tab, we should not show the original 'controlToShow'
                    // unless it's explicitly meant for this fallback tab (which is unlikely).
                    // So, we typically would hide all UserControls on this fallback tab or show its default state.
                    foreach (Control ctrl in firstVisible.Controls)
                    {
                        if (ctrl is UserControl) ctrl.Visible = false;
                        else if (ctrl is Button && ctrl.Parent == firstVisible) ctrl.Visible = true; // Ensure buttons on the tab page are visible
                    }
                }
                else
                {
                    // This case means no tabs are visible at all, which indicates a deeper issue.
                    MessageBox.Show("Eroare critică: Niciun tab nu este configurat pentru a fi vizibil.", "Eroare Configurare UI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return; // Exit if the target tab is not suitable
            }
            
            // Proceed if the tabPage is valid and visible
            if (mainTabControl.SelectedTab != tabPage)
            {
                mainTabControl.SelectedTab = tabPage;
            }

            // Hide all other UserControls on this tab page, show the requested one
            foreach (Control ctrl in tabPage.Controls)
            {
                if (ctrl is UserControl && ctrl != controlToShow)
                {
                    ctrl.Visible = false;
                }
                // Ensure main navigation buttons on the TabPage itself remain visible
                else if (ctrl is Button && ctrl.Parent == tabPage) 
                {
                    ctrl.Visible = true;
                }
            }

            if (controlToShow != null)
            {
                // Refresh data if the control implements a RefreshData method or similar
                if (controlToShow == _gestioneazaMelodiiControl) _gestioneazaMelodiiControl.RefreshData();
                else if (controlToShow == _gestioneazaIntervievatiControl) _gestioneazaIntervievatiControl.RefreshData();
                else if (controlToShow == _topNMelodiiControl) _topNMelodiiControl.RefreshData();
                else if (controlToShow == _topNIntervievatiControl) _topNIntervievatiControl.RefreshData();
                // else if (controlToShow == _listaParticipantiControl) _listaParticipantiControl.RefreshData(); // Old control
                else if (controlToShow == _listaVotantiControl) _listaVotantiControl.RefreshData(); // New control
                // Note: _voteazaControl and _preziceTopMelodiiControl might have their own load/refresh logic 
                // in VisibleChanged or a dedicated method if needed.

                controlToShow.Visible = true;
                controlToShow.BringToFront();
            }
            else // No specific UserControl to show, ensure all UserControls are hidden (buttons remain visible)
            {
                foreach (Control ctrl in tabPage.Controls)
                {
                    if (ctrl is UserControl)
                    {
                        ctrl.Visible = false;
                    }
                }
            }
        }

        private void UserControl_RequestClose(object sender, EventArgs e)
        {
            if (sender is Control control)
            {
                control.Visible = false;
                // Optional: when a user control closes, show the default buttons on its parent tab.
                // This assumes user controls are direct children of a TabPage.
                if (control.Parent is TabPage parentTabPage)
                {
                    foreach (Control parentCtrl in parentTabPage.Controls)
                    {
                        if (parentCtrl is Button)
                        {
                            parentCtrl.Visible = true;
                        }
                    }
                }
            }
        }

        private void BtnActualizeazaClasamente_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 1: Recalculate song scores based on all votes
                _melodieRepository.CalculeazaSiActualizeazaPunctajMelodii();
                MessageBox.Show("Punctajele melodiilor au fost actualizate pe baza voturilor.", "Actualizare Melodii OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Step 2: Recalculate interviewee scores based on their predictions vs actual top songs
                _intervievatRepository.CalculeazaSiActualizeazaScorIntervievati();
                MessageBox.Show("Scorurile intervievaților au fost actualizate pe baza predicțiilor și a clasamentului real.", "Actualizare Intervievați OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Step 3: Refresh relevant views
                if (_topNMelodiiControl != null && tabMelodii.Controls.Contains(_topNMelodiiControl) && _topNMelodiiControl.Visible)
                {
                    _topNMelodiiControl.RefreshData();
                }
                if (_topNIntervievatiControl != null && tabIntervievati.Controls.Contains(_topNIntervievatiControl) && _topNIntervievatiControl.Visible)
                {
                    _topNIntervievatiControl.RefreshData();
                }
                if (_gestioneazaMelodiiControl != null && tabMelodii.Controls.Contains(_gestioneazaMelodiiControl) && _gestioneazaMelodiiControl.Visible)
                {
                    _gestioneazaMelodiiControl.RefreshData();
                }
                if (_gestioneazaIntervievatiControl != null && tabIntervievati.Controls.Contains(_gestioneazaIntervievatiControl) && _gestioneazaIntervievatiControl.Visible)
                {
                    _gestioneazaIntervievatiControl.RefreshData();
                }
                // if (_listaParticipantiControl != null && tabRapoarte.Controls.Contains(_listaParticipantiControl) && _listaParticipantiControl.Visible)
                // {
                //    _listaParticipantiControl.RefreshData(); // Old control
                // }
                if (_listaVotantiControl != null && tabRapoarte.Controls.Contains(_listaVotantiControl) && _listaVotantiControl.Visible)
                {
                    _listaVotantiControl.RefreshData(); // New control
                }

                MessageBox.Show("Toate clasamentele și listele relevante au fost actualizate.", "Actualizare Completă", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la actualizarea clasamentelor: {ex.Message}", "Eroare Actualizare Clasamente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnListaParticipanti_Click(object sender, EventArgs e)
        {
            // This button should now show the list of voters (AppUsers with Role "User")
            ShowControlInTabPage(this.tabRapoarte, _listaVotantiControl); 
        }

        private void BtnExportParticipantiSub18_Click(object sender, EventArgs e)
        {
            try
            {
                IntervievatRepository repo = new IntervievatRepository();
                List<Intervievat> participantiSub18 = repo.GetIntervievatiSubVarsta(18);
                if (participantiSub18 == null || !participantiSub18.Any())
                {
                    MessageBox.Show("Nu există participanți sub 18 ani pentru export.", "Export Anulat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    sfd.FileName = "Participanti_Sub_18_Ani.csv";
                    sfd.Title = "Salvare Export Participanți Sub 18 Ani";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        StringBuilder csvContent = new StringBuilder();
                        csvContent.AppendLine("ID Intervievat,Nume Complet,Vârstă,Localitate,Scor Concurs");
                        foreach (var p in participantiSub18)                        
                            csvContent.AppendLine($"{p.IntervievatID},\"{EscapeCsvField(p.NumeComplet)}\",{p.Varsta},\"{EscapeCsvField(p.Localitate)}\",{p.ScorTotalConcurs}");                        
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
        
        private string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field)) return string.Empty;
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))            
                return $"\"{field.Replace("\"", "\"\"")}\""; 
            return field;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigureUIForRole(_currentUserRole); // Ensure UI is configured before showing default content
            // Default content loading is now handled within ConfigureUIForRole by selecting the tab
            // Further logic to show a specific UserControl on that selected tab can be added if needed.
        }
    }
} 