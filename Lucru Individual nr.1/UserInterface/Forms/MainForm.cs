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
        private ListaParticipantiControl _listaParticipantiControl;
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
            _listaParticipantiControl = new ListaParticipantiControl { Dock = DockStyle.Fill, Visible = false };

            this.tabMelodii.Controls.AddRange(new Control[] { _adaugaMelodieControl, _gestioneazaMelodiiControl, _topNMelodiiControl });
            this.tabIntervievati.Controls.AddRange(new Control[] { _adaugaIntervievatControl, _gestioneazaIntervievatiControl, _topNIntervievatiControl });
            this.tabVoteazaMelodii.Controls.Add(_voteazaControl); // Only voting control here
            this.tabManagementPredictii.Controls.Add(_preziceTopMelodiiControl); // Prediction control here
            this.tabRapoarte.Controls.Add(_listaParticipantiControl);
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
            btnListaParticipanti.Click += BtnListaParticipanti_Click;
            btnExportParticipantiSub18.Click += BtnExportParticipantiSub18_Click;
        }

        private void ConfigureUIForRole(UserRole role)
        {
            _currentUserRole = role; 
            bool isAdmin = (role == UserRole.Admin);

            // Tab Visibility
            // Admin Tabs
            if (tabMelodii != null) tabMelodii.Visible = isAdmin; // Text: "Melodii (Admin)"
            if (tabIntervievati != null) tabIntervievati.Visible = isAdmin; // Text: "Intervievați (Admin)"
            if (tabManagementPredictii != null) tabManagementPredictii.Visible = isAdmin; // Text: "Management Predicții"
            if (tabAdministrare != null) tabAdministrare.Visible = isAdmin; // Text: "Administrare Clasamente"
            if (tabRapoarte != null) tabRapoarte.Visible = isAdmin; // Text: "Rapoarte"

            // User Tabs
            if (tabVoteazaMelodii != null) tabVoteazaMelodii.Visible = !isAdmin; // Text: "Votează Melodii"
            
            // Shared Tab
            if (tabDespre != null) tabDespre.Visible = true; // Always visible for both roles

            // Set default tab based on role
            if (isAdmin)
            {
                if (mainTabControl.TabPages.Contains(tabMelodii) && tabMelodii.Visible)
                    mainTabControl.SelectedTab = tabMelodii;
                else
                {
                    var firstVisibleAdminTab = mainTabControl.TabPages.Cast<TabPage>()
                        .FirstOrDefault(tp => tp.Visible && tp != tabDespre && tp != tabVoteazaMelodii); // Prioritize admin functional tabs
                    if (firstVisibleAdminTab != null) mainTabControl.SelectedTab = firstVisibleAdminTab;
                    else if (mainTabControl.TabPages.Contains(tabDespre)) mainTabControl.SelectedTab = tabDespre; // Fallback to Despre for Admin
                }
            }
            else // User
            {
                if (mainTabControl.TabPages.Contains(tabVoteazaMelodii) && tabVoteazaMelodii.Visible)
                    mainTabControl.SelectedTab = tabVoteazaMelodii;
                else if (mainTabControl.TabPages.Contains(tabDespre)) // Fallback to Despre for User
                    mainTabControl.SelectedTab = tabDespre;
                else 
                {
                     var firstVisibleUserTab = mainTabControl.TabPages.Cast<TabPage>().FirstOrDefault(tp => tp.Visible);
                    if (firstVisibleUserTab != null) mainTabControl.SelectedTab = firstVisibleUserTab;
                }
            }
            
            // Button visibility:
            // Buttons are generally on role-specific tabs. If a tab is visible, its main navigation buttons should be visible.
            // The UserControls themselves handle their internal logic.
            // btnInregistreazaPredictii is on tabManagementPredictii (Admin only) -> visibility determined by tab's visibility.
            // btnInregistreazaVoturi is on tabVoteazaMelodii (User only) -> visibility determined by tab's visibility.
            // No explicit button hiding/showing needed here if tabs are strictly role-based.
        }

        private void ShowControlInTabPage(TabPage tabPage, Control controlToShow)
        {
            if (tabPage == null || !tabPage.Visible) // Do not attempt to show on a hidden tab
            {
                 // Optionally, find the first visible tab and select it if current target is hidden
                if (mainTabControl.TabPages.Count > 0 && !mainTabControl.TabPages.Cast<TabPage>().Any(tp => tp.Visible))
                {
                    // No tabs are visible, this is an issue with role configuration or logic.
                    // This case should ideally not happen if ConfigureUIForRole sets a default visible tab.
                    MessageBox.Show("Eroare: Niciun tab nu este vizibil. Verificați configurația rolurilor.", "Eroare Configurare UI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // If the target tabPage is not null but is currently not visible (e.g., due to role restrictions)
                // and the controlToShow is not null, it means there's a logic error trying to show a control on a hidden tab.
                else if (tabPage != null && !tabPage.Visible && controlToShow != null)
                {
                    // Log this or show an error, but don't switch to a random tab.
                    // The button click should not have happened if the tab was invisible.
                    // This implies an issue with button visibility or event handling logic.
                    Console.WriteLine($"Attempted to show control '{controlToShow.Name}' on a hidden tab '{tabPage.Name}'. Review UI logic.");
                    return; 
                }
                // If tabPage is null (should not happen from button clicks) or not visible (and no specific control to show),
                // try to select the first available visible tab. This is more of a fallback.
                else if (tabPage == null || !tabPage.Visible) 
                {
                    var firstVisible = mainTabControl.TabPages.Cast<TabPage>().FirstOrDefault(tp => tp.Visible);
                    if (firstVisible != null) mainTabControl.SelectedTab = firstVisible;
                    else  MessageBox.Show("Eroare: Niciun tab selectabil nu este vizibil.", "Eroare Navigare Tab", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
            if (mainTabControl.SelectedTab != tabPage) // Only change if not already selected
            {
                mainTabControl.SelectedTab = tabPage; 
            }

            foreach (Control ctrl in tabPage.Controls)
            {
                if (ctrl is UserControl && ctrl != controlToShow) 
                {
                    ctrl.Visible = false;
                }
                 // Make sure buttons on the TabPage itself (not within UserControls) remain visible
                else if (ctrl is Button && ctrl.Parent == tabPage)
                {
                    ctrl.Visible = true;
                }
            }

            if (controlToShow != null)
            {
                if (controlToShow == _gestioneazaMelodiiControl) _gestioneazaMelodiiControl.RefreshData(); 
                else if (controlToShow == _gestioneazaIntervievatiControl) _gestioneazaIntervievatiControl.RefreshData(); 
                else if (controlToShow == _topNMelodiiControl) _topNMelodiiControl.RefreshData(); 
                else if (controlToShow == _topNIntervievatiControl) _topNIntervievatiControl.RefreshData(); 
                else if (controlToShow == _listaParticipantiControl) _listaParticipantiControl.RefreshData();
                
                controlToShow.Visible = true;
                controlToShow.BringToFront(); 
            }
        }

        private void UserControl_RequestClose(object sender, EventArgs e)
        {
            if (sender is Control control) control.Visible = false; 
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
                if (_listaParticipantiControl != null && tabRapoarte.Controls.Contains(_listaParticipantiControl) && _listaParticipantiControl.Visible)
                {
                    _listaParticipantiControl.RefreshData();
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
            ShowControlInTabPage(this.tabRapoarte, _listaParticipantiControl);
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