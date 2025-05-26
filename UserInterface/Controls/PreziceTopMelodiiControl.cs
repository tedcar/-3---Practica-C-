using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MelodiiApp.UserInterface.Helpers;
using System.Threading.Tasks; // Ensure this is not commented out

namespace MelodiiApp.UserInterface.Controls
{
    public partial class PreziceTopMelodiiControl : UserControl
    {
        private IntervievatRepository _intervievatRepository;
        private MelodieRepository _melodieRepository;
        private PredictieRepository _predictieRepository;

        public event EventHandler RequestClose;

        /// <summary>
        /// Eveniment declanșat după ce o predicție a fost salvată cu succes.
        /// </summary>
        public event EventHandler PredictionSaved;

        public PreziceTopMelodiiControl()
        {
            InitializeComponent();
            _intervievatRepository = new IntervievatRepository();
            _melodieRepository = new MelodieRepository();
            _predictieRepository = new PredictieRepository();

            ThemeHelper.ApplyUserControlTheme(this);

            LoadIntervievati();
            LoadMelodii();
            
            btnSalveazaPredictia.Click += BtnSalveazaPredictia_Click;
            btnInchidePanou.Click += BtnInchidePanou_Click;
            
            cmbIntervievat.SelectedIndexChanged += CmbIntervievat_SelectedIndexChanged;
            this.VisibleChanged += PreziceTopMelodiiControl_VisibleChanged;
        }

        private void PreziceTopMelodiiControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadIntervievati();
                LoadMelodii();
            }
        }

        private void LoadIntervievati()
        {
            var intervievati = _intervievatRepository.GetAllIntervievati();
            cmbIntervievat.DataSource = intervievati;
            cmbIntervievat.DisplayMember = "NumeComplet";
            cmbIntervievat.ValueMember = "IntervievatID";
            if (intervievati.Any())
            {
                cmbIntervievat.SelectedIndex = 0;
            }
            else
            {
                ShowStatus("Nu există intervievați înregistrați.", ThemeHelper.RedAccent);
                btnSalveazaPredictia.Enabled = false;
            }
        }

        private void LoadMelodii()
        {
            var melodii = _melodieRepository.GetAllMelodii();

            // Clear previous items and ensure comboboxes are enabled by default before checks
            cmbMelodieLoc1.DataSource = null;
            cmbMelodieLoc2.DataSource = null;
            cmbMelodieLoc3.DataSource = null;
            cmbMelodieLoc1.Items.Clear();
            cmbMelodieLoc2.Items.Clear();
            cmbMelodieLoc3.Items.Clear();

            btnSalveazaPredictia.Enabled = true;
            cmbMelodieLoc1.Enabled = true;
            cmbMelodieLoc2.Enabled = true;
            cmbMelodieLoc3.Enabled = true;

            if (melodii == null || !melodii.Any())
            {
                ShowStatus("Nu există melodii înregistrate pentru a face predicții.", ThemeHelper.RedAccent);
                btnSalveazaPredictia.Enabled = false;
                cmbMelodieLoc1.Enabled = false;
                cmbMelodieLoc2.Enabled = false;
                cmbMelodieLoc3.Enabled = false;
                return;
            }
            
            if (melodii.Count < 3)
            {
                ShowStatus("Nu există suficiente melodii (minim 3 necesare) pentru a face predicții.", ThemeHelper.RedAccent);
                btnSalveazaPredictia.Enabled = false;
                cmbMelodieLoc1.Enabled = false;
                cmbMelodieLoc2.Enabled = false;
                cmbMelodieLoc3.Enabled = false;
                // Still populate the comboboxes with available songs, but keep them disabled
                // This allows the user to see what's there, even if they can't make a prediction yet.
                var availableMelodii = new List<Melodie>(melodii);

                cmbMelodieLoc1.DisplayMember = "TitluArtist";
                cmbMelodieLoc1.ValueMember = "MelodieID";
                cmbMelodieLoc1.DataSource = availableMelodii.ToList(); // ToList creates a new copy for each

                cmbMelodieLoc2.DisplayMember = "TitluArtist";
                cmbMelodieLoc2.ValueMember = "MelodieID";
                cmbMelodieLoc2.DataSource = availableMelodii.ToList();

                cmbMelodieLoc3.DisplayMember = "TitluArtist";
                cmbMelodieLoc3.ValueMember = "MelodieID";
                cmbMelodieLoc3.DataSource = availableMelodii.ToList();

                if(availableMelodii.Count > 0) cmbMelodieLoc1.SelectedIndex = 0;
                if(availableMelodii.Count > 1) cmbMelodieLoc2.SelectedIndex = 0; // Or 1 if you want different defaults
                if(availableMelodii.Count > 2) cmbMelodieLoc3.SelectedIndex = 0; // Or 2
                return; // Return because cannot make prediction yet
            }
            
            // This part executes if melodii.Count >= 3
            ShowStatus("Selectați melodiile pentru predicție.", ThemeHelper.TextColorOnDark); // Clear previous status

            var allMelodii = new List<Melodie>(melodii); // Create one list copy

            // Set DisplayMember and ValueMember before DataSource
            cmbMelodieLoc1.DisplayMember = "TitluArtist";
            cmbMelodieLoc1.ValueMember = "MelodieID";
            cmbMelodieLoc1.DataSource = allMelodii.ToList(); // Assign a new list copy

            cmbMelodieLoc2.DisplayMember = "TitluArtist";
            cmbMelodieLoc2.ValueMember = "MelodieID";
            cmbMelodieLoc2.DataSource = allMelodii.ToList(); // Assign a new list copy

            cmbMelodieLoc3.DisplayMember = "TitluArtist";
            cmbMelodieLoc3.ValueMember = "MelodieID";
            cmbMelodieLoc3.DataSource = allMelodii.ToList(); // Assign a new list copy
            
            // Set default selections only if there are enough items to select from.
            // The check melodii.Count >= 3 is already done above.
            cmbMelodieLoc1.SelectedIndex = 0;
            cmbMelodieLoc2.SelectedIndex = 1; // Assumes at least 2 distinct items if melodii.Count >= 3
            cmbMelodieLoc3.SelectedIndex = 2; // Assumes at least 3 distinct items if melodii.Count >= 3
        }

        private void CmbIntervievat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIntervievat.SelectedValue is int intervievatId)
            {
                var predictieExistenta = _predictieRepository.GetPredictieByIntervievatId(intervievatId);
                if (predictieExistenta != null)
                {
                    // Ensure data sources are not null before trying to set SelectedValue
                    // This is important if LoadMelodii hasn't populated them yet or they got cleared.
                    if (cmbMelodieLoc1.DataSource != null) cmbMelodieLoc1.SelectedValue = predictieExistenta.MelodieID_Loc1;
                    if (cmbMelodieLoc2.DataSource != null) cmbMelodieLoc2.SelectedValue = predictieExistenta.MelodieID_Loc2;
                    if (cmbMelodieLoc3.DataSource != null) cmbMelodieLoc3.SelectedValue = predictieExistenta.MelodieID_Loc3;
                    ShowStatus("Predicție existentă încărcată.", ThemeHelper.MidBlue);
                }
                else
                {
                    // If DataSource is set by LoadMelodii, this should work.
                    var melodii = cmbMelodieLoc1.DataSource as List<Melodie>; // Safer cast
                    if (melodii != null && melodii.Count >= 3) 
                    {
                        // Check if combo boxes actually have items before setting SelectedIndex.
                        if (cmbMelodieLoc1.Items.Count > 0) cmbMelodieLoc1.SelectedIndex = 0;
                        // Ensure there are enough distinct items for default selections
                        if (cmbMelodieLoc2.Items.Count > 1 && melodii.Count > 1) cmbMelodieLoc2.SelectedIndex = 1;
                        else if (cmbMelodieLoc2.Items.Count > 0) cmbMelodieLoc2.SelectedIndex = 0; 

                        if (cmbMelodieLoc3.Items.Count > 2 && melodii.Count > 2) cmbMelodieLoc3.SelectedIndex = 2;
                        else if (cmbMelodieLoc3.Items.Count > 1 && melodii.Count > 1) cmbMelodieLoc3.SelectedIndex = 1;
                        else if (cmbMelodieLoc3.Items.Count > 0) cmbMelodieLoc3.SelectedIndex = 0;
                    }
                    ShowStatus("Introduceți predicțiile.", ThemeHelper.TextColorOnDark);
                }
            }
        }

        private void BtnSalveazaPredictia_Click(object sender, EventArgs e)
        {
            ClearStatus();

            if (cmbIntervievat.SelectedValue == null)
            {
                ShowStatus("Vă rugăm selectați un intervievat.", ThemeHelper.RedAccent);
                return;
            }

            if (cmbMelodieLoc1.SelectedValue == null || cmbMelodieLoc2.SelectedValue == null || cmbMelodieLoc3.SelectedValue == null)
            {
                ShowStatus("Vă rugăm selectați o melodie pentru fiecare loc.", ThemeHelper.RedAccent);
                return;
            }

            int intervievatId = (int)cmbIntervievat.SelectedValue;
            int melodieIdLoc1 = (int)cmbMelodieLoc1.SelectedValue;
            int melodieIdLoc2 = (int)cmbMelodieLoc2.SelectedValue;
            int melodieIdLoc3 = (int)cmbMelodieLoc3.SelectedValue;

            if (melodieIdLoc1 == melodieIdLoc2 || melodieIdLoc1 == melodieIdLoc3 || melodieIdLoc2 == melodieIdLoc3)
            {
                ShowStatus("Melodiile selectate pentru Top 3 trebuie să fie distincte.", ThemeHelper.RedAccent);
                return;
            }

            Predictie predictie = new Predictie(intervievatId, melodieIdLoc1, melodieIdLoc2, melodieIdLoc3);
            
            Console.WriteLine("[DEBUG] BtnSalveazaPredictia_Click: Attempting to save prediction...");
            bool success = _predictieRepository.SalveazaPredictie(predictie);
            Console.WriteLine($"[DEBUG] BtnSalveazaPredictia_Click: SalveazaPredictie returned {success}");

            if (success)
            {
                Console.WriteLine("[DEBUG] BtnSalveazaPredictia_Click: Success branch. Calling ShowEnhancedStatus.");
                HandleSuccessfulSave();
            }
            else
            {
                Console.WriteLine("[DEBUG] BtnSalveazaPredictia_Click: Failure branch. Showing error status.");
                ShowStatus("Eroare la salvarea predicției.", ThemeHelper.RedAccent);
            }
        }
        
        private void ShowStatus(string message, Color foreColor)
        {
            ThemeHelper.StyleSpecificLabel(lblStatusPredictie, foreColor);
            lblStatusPredictie.Text = message;
            lblStatusPredictie.Visible = true;
        }

        private async Task ShowEnhancedStatus(string message, Color foreColor, int durationMs)
        {
            lblStatusPredictie.Font = new Font(lblStatusPredictie.Font.FontFamily, 10, FontStyle.Bold);
            ThemeHelper.StyleSpecificLabel(lblStatusPredictie, foreColor);
            lblStatusPredictie.Text = message;
            lblStatusPredictie.Visible = true;
            lblStatusPredictie.BringToFront(); 
            
            Console.WriteLine("[DEBUG] ShowEnhancedStatus: Label displayed. Starting delay.");
            await Task.Delay(durationMs);
            Console.WriteLine("[DEBUG] ShowEnhancedStatus: Delay finished.");

            if (lblStatusPredictie.Text == message) // Check if text is still the same, to avoid clearing a newer message
            {
                 ClearStatus();
                 Console.WriteLine("[DEBUG] ShowEnhancedStatus: Status cleared.");
            }
        }

        private async void HandleSuccessfulSave()
        {
            await ShowEnhancedStatus("Predicția a fost salvată cu succes!", ThemeHelper.SuccessColor, 2500); // Increased duration slightly
            PredictionSaved?.Invoke(this, EventArgs.Empty);
            Console.WriteLine("[DEBUG] HandleSuccessfulSave: PredictionSaved event invoked after status display.");
        }

        private void ClearStatus()
        {
            lblStatusPredictie.Text = string.Empty;
            lblStatusPredictie.Visible = false;
        }

        protected virtual void OnRequestClose()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void BtnInchidePanou_Click(object sender, EventArgs e)
        {
            OnRequestClose();
        }
    }
} 