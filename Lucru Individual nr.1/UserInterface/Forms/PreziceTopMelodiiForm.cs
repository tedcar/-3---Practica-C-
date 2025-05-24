using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MelodiiApp.UserInterface.Forms
{
    public partial class PreziceTopMelodiiForm : Form
    {
        private IntervievatRepository _intervievatRepository;
        private MelodieRepository _melodieRepository;
        private PredictieRepository _predictieRepository;

        public PreziceTopMelodiiForm()
        {
            InitializeComponent();
            _intervievatRepository = new IntervievatRepository();
            _melodieRepository = new MelodieRepository();
            _predictieRepository = new PredictieRepository();

            LoadIntervievati();
            LoadMelodii();
            btnSalveazaPredictia.Click += BtnSalveazaPredictia_Click;
            this.Load += PreziceTopMelodiiForm_Load;
        }

        private void PreziceTopMelodiiForm_Load(object sender, EventArgs e)
        {
            // Set default colors from theme
            this.BackColor = Color.FromArgb(15, 17, 26); // Deep Navy
            lblTitlu.ForeColor = Color.FromArgb(224, 224, 224); // Light Text
            lblIntervievat.ForeColor = Color.FromArgb(224, 224, 224);
            lblMelodieLoc1.ForeColor = Color.FromArgb(224, 224, 224);
            lblMelodieLoc2.ForeColor = Color.FromArgb(224, 224, 224);
            lblMelodieLoc3.ForeColor = Color.FromArgb(224, 224, 224);
            lblStatusPredictie.ForeColor = Color.FromArgb(224, 224, 224);

            ConfigureComboBox(cmbIntervievat);
            ConfigureComboBox(cmbMelodieLoc1);
            ConfigureComboBox(cmbMelodieLoc2);
            ConfigureComboBox(cmbMelodieLoc3);

            btnSalveazaPredictia.BackColor = Color.FromArgb(110, 181, 255); // Calm Sky-Blue
            btnSalveazaPredictia.ForeColor = Color.FromArgb(15, 17, 26); // Deep Navy text for contrast

            cmbIntervievat.SelectedIndexChanged += CmbIntervievat_SelectedIndexChanged;
        }

        private void ConfigureComboBox(ComboBox comboBox)
        {
            comboBox.BackColor = Color.FromArgb(47, 59, 92); // Muted Indigo variant for inputs
            comboBox.ForeColor = Color.FromArgb(224, 224, 224); // Light Text
            comboBox.FlatStyle = FlatStyle.Flat;
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
                lblStatusPredictie.Text = "Nu există intervievați înregistrați.";
                btnSalveazaPredictia.Enabled = false;
            }
        }

        private void LoadMelodii()
        {
            var melodii = _melodieRepository.GetAllMelodii();
            if (!melodii.Any() || melodii.Count < 3)
            {
                lblStatusPredictie.Text = "Nu există suficiente melodii (minim 3) pentru a face predicții.";
                btnSalveazaPredictia.Enabled = false;
                cmbMelodieLoc1.Enabled = false;
                cmbMelodieLoc2.Enabled = false;
                cmbMelodieLoc3.Enabled = false;
                return;
            }

            // Create separate lists for each ComboBox to allow different selections
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
                cmbMelodieLoc2.SelectedIndex = 1;
                cmbMelodieLoc3.SelectedIndex = 2;
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
                    lblStatusPredictie.Text = "Predicție existentă încărcată.";
                }
                else
                {
                    // Reset to default if no prediction exists
                    if (((List<Melodie>)cmbMelodieLoc1.DataSource).Count >= 3)
                    {
                        cmbMelodieLoc1.SelectedIndex = 0;
                        cmbMelodieLoc2.SelectedIndex = 1;
                        cmbMelodieLoc3.SelectedIndex = 2;
                    }
                    lblStatusPredictie.Text = "Introduceți predicțiile.";
                }
            }
        }

        private void BtnSalveazaPredictia_Click(object sender, EventArgs e)
        {
            lblStatusPredictie.Text = "";

            if (cmbIntervievat.SelectedValue == null)
            {
                lblStatusPredictie.Text = "Vă rugăm selectați un intervievat.";
                lblStatusPredictie.ForeColor = Color.OrangeRed;
                return;
            }

            if (cmbMelodieLoc1.SelectedValue == null || cmbMelodieLoc2.SelectedValue == null || cmbMelodieLoc3.SelectedValue == null)
            {
                lblStatusPredictie.Text = "Vă rugăm selectați o melodie pentru fiecare loc.";
                lblStatusPredictie.ForeColor = Color.OrangeRed;
                return;
            }

            int intervievatId = (int)cmbIntervievat.SelectedValue;
            int melodieIdLoc1 = (int)cmbMelodieLoc1.SelectedValue;
            int melodieIdLoc2 = (int)cmbMelodieLoc2.SelectedValue;
            int melodieIdLoc3 = (int)cmbMelodieLoc3.SelectedValue;

            if (melodieIdLoc1 == melodieIdLoc2 || melodieIdLoc1 == melodieIdLoc3 || melodieIdLoc2 == melodieIdLoc3)
            {
                lblStatusPredictie.Text = "Melodiile selectate pentru Top 3 trebuie să fie distincte.";
                lblStatusPredictie.ForeColor = Color.OrangeRed;
                return;
            }

            Predictie predictie = new Predictie(intervievatId, melodieIdLoc1, melodieIdLoc2, melodieIdLoc3);
            
            bool success = _predictieRepository.SalveazaPredictie(predictie);

            if (success)
            {
                lblStatusPredictie.Text = "Predicția a fost salvată cu succes!";
                lblStatusPredictie.ForeColor = Color.FromArgb(87, 232, 193); // Mint green for success
            }
            else
            {
                lblStatusPredictie.Text = "Eroare la salvarea predicției.";
                lblStatusPredictie.ForeColor = Color.OrangeRed;
            }
        }
    }
} 