using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MelodiiApp.UserInterface.Helpers;

namespace MelodiiApp.UserInterface.Controls
{
    public partial class PreziceTopMelodiiControl : UserControl
    {
        private IntervievatRepository _intervievatRepository;
        private MelodieRepository _melodieRepository;
        private PredictieRepository _predictieRepository;

        public event EventHandler RequestClose;

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
            if (!melodii.Any() || melodii.Count < 3)
            {
                ShowStatus("Nu există suficiente melodii (minim 3) pentru a face predicții.", ThemeHelper.RedAccent);
                btnSalveazaPredictia.Enabled = false;
                cmbMelodieLoc1.Enabled = false;
                cmbMelodieLoc2.Enabled = false;
                cmbMelodieLoc3.Enabled = false;
                return;
            }
            
            btnSalveazaPredictia.Enabled = true;
            cmbMelodieLoc1.Enabled = true;
            cmbMelodieLoc2.Enabled = true;
            cmbMelodieLoc3.Enabled = true;

            cmbMelodieLoc1.DataSource = new List<Melodie>(melodii);
            cmbMelodieLoc1.DisplayMember = "TitluArtist";
            cmbMelodieLoc1.ValueMember = "MelodieID";

            cmbMelodieLoc2.DataSource = new List<Melodie>(melodii);
            cmbMelodieLoc2.DisplayMember = "TitluArtist";
            cmbMelodieLoc2.ValueMember = "MelodieID";

            cmbMelodieLoc3.DataSource = new List<Melodie>(melodii);
            cmbMelodieLoc3.DisplayMember = "TitluArtist";
            cmbMelodieLoc3.ValueMember = "MelodieID";
            
            if (melodii.Count >= 3) {
                cmbMelodieLoc1.SelectedIndex = 0;
                cmbMelodieLoc2.SelectedIndex = melodii.Count > 1 ? 1 : 0;
                cmbMelodieLoc3.SelectedIndex = melodii.Count > 2 ? 2 : (melodii.Count > 1 ? 1 : 0);
            }
        }

        private void CmbIntervievat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIntervievat.SelectedValue is int intervievatId)
            {
                var predictieExistenta = _predictieRepository.GetPredictieByIntervievatId(intervievatId);
                if (predictieExistenta != null)
                {
                    cmbMelodieLoc1.SelectedValue = predictieExistenta.MelodieID_Loc1;
                    cmbMelodieLoc2.SelectedValue = predictieExistenta.MelodieID_Loc2;
                    cmbMelodieLoc3.SelectedValue = predictieExistenta.MelodieID_Loc3;
                    ShowStatus("Predicție existentă încărcată.", ThemeHelper.MidBlue);
                }
                else
                {
                    var melodii = (List<Melodie>)cmbMelodieLoc1.DataSource;
                    if (melodii != null && melodii.Count >= 3) 
                    {
                        cmbMelodieLoc1.SelectedIndex = 0;
                        cmbMelodieLoc2.SelectedIndex = 1;
                        cmbMelodieLoc3.SelectedIndex = 2;
                    }
                    ShowStatus("Introduceți predicțiile.", ThemeHelper.TextOnLight);
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
            
            bool success = _predictieRepository.SalveazaPredictie(predictie);

            if (success)
            {
                ShowStatus("Predicția a fost salvată cu succes!", Color.Green);
            }
            else
            {
                ShowStatus("Eroare la salvarea predicției.", ThemeHelper.RedAccent);
            }
        }
        
        private void ShowStatus(string message, Color foreColor)
        {
            ThemeHelper.StyleSpecificLabel(lblStatusPredictie, foreColor);
            lblStatusPredictie.Text = message;
            lblStatusPredictie.Visible = true;
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