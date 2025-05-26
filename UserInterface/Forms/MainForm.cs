using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text; // Added for StringBuilder and Encoding
using System.Windows.Forms;
using MelodiiApp.DataAccess;
using MaterialSkin;
using MaterialSkin.Controls;
using MelodiiApp.UserInterface.Controls; // ENSURE THIS IS PRESENT
using MelodiiApp.UserInterface.Forms; // Required for various forms
using MelodiiApp.Core.Models; // For UserRole enum
using MelodiiApp.Core.DomainModels; // Added for Intervievat and other domain models
using System.IO; // Added for File operations

namespace MelodiiApp.UserInterface.Forms
{
    /// <summary>
    /// Formularul principal al aplicației.
    /// Gestionează navigarea între diferitele module (controale utilizator) în funcție de rolul utilizatorului.
    /// </summary>
    public partial class MainForm : MaterialSkin.Controls.MaterialForm
    {
        // Controale utilizator pentru diverse funcționalități
        private AdaugaMelodieControl _adaugaMelodieControl;
        private AdaugaIntervievatControl _adaugaIntervievatControl;
        private GestioneazaIntervievatiControl _gestioneazaIntervievatiControl;
        private GestioneazaMelodiiControl _gestioneazaMelodiiControl;
        private VoteazaControl _voteazaControl;
        private TopNMelodiiControl _topNMelodiiControl;
        private TopNIntervievatiControl _topNIntervievatiControl;
        private PreziceTopMelodiiControl _preziceTopMelodiiControl;
        private ListaParticipantiControl _listaParticipantiControl;

        private UserRole _currentUserRole; // Rolul utilizatorului curent autentificat
        private readonly MelodieRepository _melodieRepository; // Repository pentru melodii
        private readonly IntervievatRepository _intervievatRepository; // Repository pentru intervievați

        /// <summary>
        /// Constructorul formularului principal.
        /// </summary>
        /// <param name="userRole">Rolul utilizatorului autentificat, determină UI-ul și funcționalitățile disponibile.</param>
        public MainForm(UserRole userRole)
        {
            InitializeComponent();
            _currentUserRole = userRole;
            _melodieRepository = new MelodieRepository();
            _intervievatRepository = new IntervievatRepository();
            // InitializeMaterialSkinManager(); // Removed
            // this.BackColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5"); // Removed

            // Ensure this form is managed by the global MaterialSkinManager instance
            MaterialSkinManager.Instance.AddFormToManage(this);

            InitializeUserControls();
            AttachButtonEventHandlers();
            ApplyCustomStylingToTabButtons();
            ConfigureUIForRole(_currentUserRole);
            TriggerRankingsUpdateAndRefreshControls();
        }

        /// <summary>
        /// Inițializează toate controalele utilizator și le adaugă în paginile de tab corespunzătoare.
        /// Setează proprietatea Dock și vizibilitatea inițială, și abonează la evenimentele necesare.
        /// </summary>
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

            if (this.tabMelodii != null) this.tabMelodii.Controls.AddRange(new Control[] { _adaugaMelodieControl, _gestioneazaMelodiiControl });
            if (this.tabIntervievati != null) this.tabIntervievati.Controls.AddRange(new Control[] { _adaugaIntervievatControl, _gestioneazaIntervievatiControl });
            if (this.tabManagementPredictii != null) this.tabManagementPredictii.Controls.Add(_preziceTopMelodiiControl);
            if (this.tabRapoarte != null) this.tabRapoarte.Controls.Add(_listaParticipantiControl);
            
            if (this.tabVoteazaMelodii != null) this.tabVoteazaMelodii.Controls.Add(_voteazaControl);

            if (this.tabClasamenteGenerale != null) 
            {
                this.tabClasamenteGenerale.Controls.AddRange(new Control[] { _topNMelodiiControl, _topNIntervievatiControl });
                _topNMelodiiControl.RequestGoBack += TopNControl_RequestGoBack;
                _topNIntervievatiControl.RequestGoBack += TopNControl_RequestGoBack;
            }

            _adaugaMelodieControl.RequestClose += UserControl_RequestClose;
            _adaugaIntervievatControl.RequestClose += UserControl_RequestClose;
            _gestioneazaIntervievatiControl.RequestShowAdaugaIntervievatPanel += (s, e) => ShowControlInTabPage(this.tabIntervievati, _adaugaIntervievatControl);
            _gestioneazaMelodiiControl.RequestShowAdaugaMelodiePanel += (s, e) => ShowControlInTabPage(this.tabMelodii, _adaugaMelodieControl);
            _voteazaControl.RequestClose += UserControl_RequestClose;
            _voteazaControl.VotesSubmitted += OnVotesSubmitted;
            _preziceTopMelodiiControl.RequestClose += UserControl_RequestClose;

            // Subscribe to save events for automatic ranking updates
            _adaugaMelodieControl.MelodieSaved += OnAdminDataChanged;
            _gestioneazaMelodiiControl.MelodieDataChanged += OnAdminDataChanged;
            _adaugaIntervievatControl.IntervievatSaved += OnAdminDataChanged;
            _gestioneazaIntervievatiControl.IntervievatDataChanged += OnAdminDataChanged;
            _preziceTopMelodiiControl.PredictionSaved += OnAdminDataChanged;
            _listaParticipantiControl.RequestGoBack += ListaParticipantiControl_RequestGoBack;
        }
        
        /// <summary>
        /// Aplică stiluri personalizate butoanelor de navigație din cadrul fiecărei pagini de tab.
        /// Modifică aspectul vizual (culoare, font, etc.) pentru o interfață consistentă.
        /// Acestă metodă se concentrează acum pe efectele de hover, deoarece stilurile statice au fost mutate în Designer.
        /// </summary>
        private void ApplyCustomStylingToTabButtons()
        {
            // Culorile pentru stilizare sunt definite aici pentru claritate.
            // Color btnBackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc"); // Mutat în Designer
            // Color btnForeColor = Color.White; // Mutat în Designer
            Color hoverColor = System.Drawing.ColorTranslator.FromHtml("#003049"); // Culoare pentru hover

            foreach (TabPage tabPage in mainTabControl.TabPages)
            {
                var buttons = tabPage.Controls.OfType<Button>();
                foreach (var btn in buttons)
                {
                    // Stilurile statice precum FlatStyle, BorderSize, BackColor, ForeColor, Font, TextAlign, Height, MinimumSize
                    // au fost mutate în fișierul Designer.cs pentru o mai bună separare a responsabilităților.
                    // btn.FlatStyle = FlatStyle.Flat; // Mutat
                    // btn.FlatAppearance.BorderSize = 0; // Mutat
                    // btn.BackColor = btnBackColor; // Mutat
                    // btn.ForeColor = btnForeColor; // Mutat
                    // btn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold); // Mutat
                    // btn.TextAlign = ContentAlignment.MiddleCenter; // Mutat
                    // btn.Height = 40; // Mutat (sau controlat prin Size)
                    // btn.MinimumSize = new Size(180, 40); // Mutat
                    
                    // Păstrăm doar logica pentru efectul de hover.
                    Color originalColor = btn.BackColor; // Preluăm BackColor setat în Designer.
                    btn.MouseEnter += (sender, e) => ((Button)sender).BackColor = hoverColor;
                    btn.MouseLeave += (sender, e) => ((Button)sender).BackColor = originalColor;
                }
            }
            // mainTabControl.Font = new Font("Segoe UI", 10F); // Mutat în Designer
            // mainTabControl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049"); // Mutat în Designer
        }

        /// <summary>
        /// Atașează metodele de gestionare a evenimentelor la evenimentele Click ale butoanelor principale de navigație.
        /// Aceste butoane controlează afișarea diferitelor controale utilizator în tab-urile corespunzătoare.
        /// </summary>
        private void AttachButtonEventHandlers()
        {
            if (btnAdaugaMelodie != null) btnAdaugaMelodie.Click += (s, e) => ShowControlInTabPage(this.tabMelodii, _adaugaMelodieControl);
            if (btnGestioneazaMelodii != null) btnGestioneazaMelodii.Click += (s, e) => ShowControlInTabPage(this.tabMelodii, _gestioneazaMelodiiControl);

            if (btnAdaugaIntervievat != null) btnAdaugaIntervievat.Click += (s, e) => ShowControlInTabPage(this.tabIntervievati, _adaugaIntervievatControl);
            if (btnGestioneazaIntervievati != null) btnGestioneazaIntervievati.Click += (s, e) => ShowControlInTabPage(this.tabIntervievati, _gestioneazaIntervievatiControl);

            if (btnInregistreazaVoturi != null) btnInregistreazaVoturi.Click += (s, e) => ShowControlInTabPage(this.tabVoteazaMelodii, _voteazaControl);

            if (btnInregistreazaPredictii != null) btnInregistreazaPredictii.Click += (s, e) => ShowControlInTabPage(this.tabManagementPredictii, _preziceTopMelodiiControl); 

            if (btnListaParticipanti != null) btnListaParticipanti.Click += (s, e) => ShowControlInTabPage(this.tabRapoarte, _listaParticipantiControl);
            if (btnExportParticipantiSub18 != null) btnExportParticipantiSub18.Click += BtnExportParticipantiSub18_Click;

            if (btnShowMelodyRankingsCG != null) btnShowMelodyRankingsCG.Click += (s, e) => ShowControlInTabPage(this.tabClasamenteGenerale, _topNMelodiiControl);
            if (btnShowIntervieweeRankingsCG != null) btnShowIntervieweeRankingsCG.Click += (s, e) => ShowControlInTabPage(this.tabClasamenteGenerale, _topNIntervievatiControl);
        }

        /// <summary>
        /// Configurează interfața utilizator (vizibilitatea tab-urilor și a butoanelor) în funcție de rolul utilizatorului (Admin sau User).
        /// </summary>
        /// <param name="role">Rolul utilizatorului curent.</param>
        private void ConfigureUIForRole(UserRole role)
        {
            _currentUserRole = role; 
            bool isAdmin = (role == UserRole.Admin);

            mainTabControl.TabPages.Clear();

            if (isAdmin)
            {
                // Adaugă tab-urile specifice rolului de Admin.
                if (this.tabMelodii != null) mainTabControl.TabPages.Add(this.tabMelodii);
                if (this.tabIntervievati != null) mainTabControl.TabPages.Add(this.tabIntervievati);
                // Tab-ul de votare melodii nu este pentru Admin.
                if (this.tabManagementPredictii != null) mainTabControl.TabPages.Add(this.tabManagementPredictii);
                if (this.tabRapoarte != null) mainTabControl.TabPages.Add(this.tabRapoarte);
                if (this.tabClasamenteGenerale != null) mainTabControl.TabPages.Add(this.tabClasamenteGenerale);

                // Ensure buttons inside specific tabs are only visible/active if that tab is intended for the role
                // This logic might be better placed inside the UserControls themselves based on a role passed to them,
                // or by ShowControlInTabPage handling visibility of sub-buttons.
                // For now, this addresses the direct request related to btnInregistreazaVoturi and btnInregistreazaPredictii
                // which seem to be top-level buttons or whose visibility is tied to these specific admin/user flows.
                if (btnInregistreazaVoturi != null) btnInregistreazaVoturi.Visible = false; 
                if (btnInregistreazaPredictii != null) btnInregistreazaPredictii.Visible = true; 
                
                if (mainTabControl.TabPages.Count > 0) 
                {
                    if (this.tabMelodii != null && mainTabControl.TabPages.Contains(this.tabMelodii)) mainTabControl.SelectedTab = this.tabMelodii;
                    else if (this.tabClasamenteGenerale != null && mainTabControl.TabPages.Contains(this.tabClasamenteGenerale)) 
                    {
                        mainTabControl.SelectedTab = this.tabClasamenteGenerale;
                        ShowControlInTabPage(this.tabClasamenteGenerale, _topNMelodiiControl); // Show default view
                    }
                    else mainTabControl.SelectedIndex = 0; // Default to first available tab
                }
            }
            else // Standard User
            {
                if (this.tabVoteazaMelodii != null) mainTabControl.TabPages.Add(this.tabVoteazaMelodii);
                if (this.tabClasamenteGenerale != null) mainTabControl.TabPages.Add(this.tabClasamenteGenerale);
                
                if (btnInregistreazaVoturi != null) btnInregistreazaVoturi.Visible = true;
                if (btnInregistreazaPredictii != null) btnInregistreazaPredictii.Visible = false;

                if (mainTabControl.TabPages.Count > 0) 
                {
                     if (this.tabVoteazaMelodii != null && mainTabControl.TabPages.Contains(this.tabVoteazaMelodii)) mainTabControl.SelectedTab = this.tabVoteazaMelodii;
                    else if (this.tabClasamenteGenerale != null && mainTabControl.TabPages.Contains(this.tabClasamenteGenerale)) 
                    {
                        mainTabControl.SelectedTab = this.tabClasamenteGenerale;
                        ShowControlInTabPage(this.tabClasamenteGenerale, _topNMelodiiControl); // Show default view
                    }
                    else mainTabControl.SelectedIndex = 0; // Default to first available tab
                }
            }
        }

        /// <summary>
        /// Afișează un control utilizator specific într-o pagină de tab specificată.
        /// Ascunde celelalte controale utilizator din acea pagină și afișează butoanele de navigație dacă controlToShow este null.
        /// </summary>
        /// <param name="tabPage">Pagina de tab în care se va afișa controlul.</param>
        /// <param name="controlToShow">Controlul utilizator care trebuie afișat. Dacă este null, se afișează butoanele de navigație ale tab-ului.</param>
        private void ShowControlInTabPage(TabPage tabPage, Control controlToShow)
        {
            if (tabPage == null || !mainTabControl.TabPages.Contains(tabPage) /* Removed !tabPage.Visible check as ConfigureUIForRole handles presence */)
            {
                // Încearcă selectarea primului tab disponibil dacă tab-ul țintă este invalid.
                if (mainTabControl.TabPages.Count > 0) mainTabControl.SelectedIndex = 0;
                return; 
            }

            mainTabControl.SelectedTab = tabPage; 

            // Make all UserControls in the tabPage invisible, except the one to show
            // Also, ensure all Buttons in the tabPage are visible (these are navigation buttons for the tab page)
            foreach (Control ctrl in tabPage.Controls)
            {
                if (ctrl is UserControl userCtrl)
                {
                    userCtrl.Visible = (userCtrl == controlToShow);
                }
                else if (ctrl is Button buttonInTab)
                {
                    buttonInTab.Visible = (controlToShow == null); 
                }
            }

            if (controlToShow != null)
            {
                // If the controlToShow is not already in the tabPage's controls (should be, by InitializeUserControls)
                // then it might need to be added. However, current design adds all UCs at init.
                // if (!tabPage.Controls.Contains(controlToShow)) { tabPage.Controls.Add(controlToShow); controlToShow.Dock = DockStyle.Fill; }

                // Refresh data if the control supports it (methods like RefreshData() are specific to these UCs)
                if (controlToShow == _gestioneazaMelodiiControl) _gestioneazaMelodiiControl.RefreshData(); 
                else if (controlToShow == _gestioneazaIntervievatiControl) _gestioneazaIntervievatiControl.RefreshData(); 
                else if (controlToShow == _topNMelodiiControl) _topNMelodiiControl.RefreshData(); 
                else if (controlToShow == _topNIntervievatiControl) _topNIntervievatiControl.RefreshData(); 
                else if (controlToShow == _listaParticipantiControl) _listaParticipantiControl.RefreshData();
                // No specific refresh for _adaugaMelodieControl, _adaugaIntervievatControl, _voteazaControl, _preziceTopMelodiiControl as they are for input
                
                controlToShow.Visible = true;
                controlToShow.BringToFront(); 
            }
            else // controlToShow is null, meaning we are going back to navigation buttons
            {
                 ShowNavigationButtonsForTab(tabPage);
            }
        }

        /// <summary>
        /// Gestionare eveniment pentru închiderea unui control utilizator (revenire la meniul tab-ului).
        /// </summary>
        private void UserControl_RequestClose(object sender, EventArgs e)
        {
            if (sender is AdaugaMelodieControl || sender is AdaugaIntervievatControl || 
                sender is VoteazaControl || sender is PreziceTopMelodiiControl)
            {
                TabPage parentTabPage = (sender as Control).Parent as TabPage;
                if (parentTabPage != null)
                {
                    ShowNavigationButtonsForTab(parentTabPage);
                    // Optionally, select a default control or clear selection if needed
                    if (parentTabPage == this.tabMelodii) ShowControlInTabPage(parentTabPage, _gestioneazaMelodiiControl);
                    else if (parentTabPage == this.tabIntervievati) ShowControlInTabPage(parentTabPage, _gestioneazaIntervievatiControl);
                    // For other tabs, just showing navigation buttons might be enough
                }
            }
        }

        /// <summary>
        /// Afișează butoanele de navigație pentru o pagină de tab specifică și ascunde controalele utilizator.
        /// </summary>
        /// <param name="tabPage">Pagina de tab pentru care se afișează butoanele.</param>
        private void ShowNavigationButtonsForTab(TabPage tabPage)
        {
            if (tabPage == null) return;

            foreach (Control ctrl in tabPage.Controls)
            {
                if (ctrl is UserControl userCtrl)
                {
                    userCtrl.Visible = false;
                }
                else if (ctrl is Button buttonInTab)
                {
                    buttonInTab.Visible = true;
                }
            }
        }

        /// <summary>
        /// Inițiază actualizarea clasamentelor și reîmprospătează datele în controalele relevante.
        /// Aceasta este apelată la inițializarea MainForm și după acțiuni care modifică datele (vot, salvare melodie etc.).
        /// </summary>
        private void TriggerRankingsUpdateAndRefreshControls()
        {
            try
            {
                if (_melodieRepository != null)
                {
                    _melodieRepository.CalculeazaSiActualizeazaPunctajMelodii();
                }

                if (_intervievatRepository != null)
                {
                    _intervievatRepository.CalculeazaSiActualizeazaScorIntervievati();
                }

                if (_topNMelodiiControl != null && _topNMelodiiControl.Visible) _topNMelodiiControl.RefreshData();
                if (_topNIntervievatiControl != null && _topNIntervievatiControl.Visible) _topNIntervievatiControl.RefreshData();
                if (_gestioneazaMelodiiControl != null && _gestioneazaMelodiiControl.Visible) _gestioneazaMelodiiControl.RefreshData();
                if (_gestioneazaIntervievatiControl != null && _gestioneazaIntervievatiControl.Visible) _gestioneazaIntervievatiControl.RefreshData();
                if (_listaParticipantiControl != null && _listaParticipantiControl.Visible) _listaParticipantiControl.RefreshData();

                Console.WriteLine("Rankings and lists updated automatically.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la actualizarea automată a datelor: {ex.Message}", "Eroare Actualizare Automată", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul de export al participanților sub 18 ani.
        /// Extrage datele relevante și le salvează într-un fișier CSV.
        /// </summary>
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
        
        /// <summary>
        /// Formatează un câmp pentru a fi inclus corect într-un fișier CSV (escape de virgule și ghilimele).
        /// </summary>
        /// <param name="field">Valoarea câmpului de formatat.</param>
        /// <returns>Valoarea formatată pentru CSV.</returns>
        private string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field)) return string.Empty;
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))            
                return $"\"{field.Replace("\"", "\"\"")}\""; 
            return field;
        }

        /// <summary>
        /// Se declanșează la încărcarea formularului principal.
        /// Poate fi folosit pentru inițializări suplimentare.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            ConfigureUIForRole(_currentUserRole);
        }

        /// <summary>
        /// Gestionar de eveniment apelat după ce voturile au fost trimise din VoteazaControl.
        /// Actualizează clasamentele și reîmprospătează controalele relevante.
        /// </summary>
        private void OnVotesSubmitted(object sender, EventArgs e)
        {
            TriggerRankingsUpdateAndRefreshControls();
        }

        /// <summary>
        /// Gestionar de eveniment apelat după ce date administrative (melodii, intervievați, predicții) au fost modificate.
        /// Actualizează clasamentele și reîmprospătează controalele relevante.
        /// </summary>
        private void OnAdminDataChanged(object sender, EventArgs e)
        {
            TriggerRankingsUpdateAndRefreshControls();
        }

        /// <summary>
        /// Gestionar pentru evenimentul RequestGoBack din ListaParticipantiControl.
        /// Afișează meniul principal al tab-ului Rapoarte.
        /// </summary>
        private void ListaParticipantiControl_RequestGoBack(object sender, EventArgs e)
        {
            ShowNavigationButtonsForTab(this.tabRapoarte);
        }

        /// <summary>
        /// Gestionar pentru evenimentul RequestGoBack din controalele TopN (TopNMelodiiControl, TopNIntervievatiControl).
        /// Afișează meniul principal al tab-ului Clasamente Generale.
        /// </summary>
        private void TopNControl_RequestGoBack(object sender, EventArgs e)
        {
            ShowNavigationButtonsForTab(this.tabClasamenteGenerale);
        }
    }
}