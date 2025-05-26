using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using MelodiiApp.UserInterface.Helpers; // Added for ThemeHelper

namespace MelodiiApp.UserInterface.Controls
{
    /// <summary>
    /// Control pentru adăugarea unei melodii noi.
    /// </summary>
    public partial class AdaugaMelodieControl : UserControl
    {
        private readonly MelodieRepository _melodieRepository;
        
        /// <summary>
        /// Eveniment declanșat când se solicită închiderea acestui control.
        /// </summary>
        public event EventHandler RequestClose;

        /// <summary>
        /// Eveniment declanșat după ce o melodie a fost salvată cu succes.
        /// </summary>
        public event EventHandler MelodieSaved;

        /// <summary>
        /// Initializează o nouă instanță a clasei <see cref="AdaugaMelodieControl"/>.
        /// </summary>
        public AdaugaMelodieControl()
        {
            InitializeComponent();
            _melodieRepository = new MelodieRepository();
            ThemeHelper.ApplyUserControlTheme(this); // Apply theme
            
            // Attach event handlers
            btnSalveazaMelodie.Click += BtnSalveazaMelodie_Click;
            btnAnuleaza.Click += BtnAnuleaza_Click;
        }

        private async void BtnSalveazaMelodie_Click(object sender, EventArgs e)
        {
            // Reset status label
            ClearStatusLabel();
            ThemeHelper.StyleSpecificLabel(lblStatus, ThemeHelper.TextColorOnDark); // Reset to default before potential error/success

            // Validări
            if (string.IsNullOrWhiteSpace(txtTitlu.Text))
            {
                ShowErrorStatus("Titlul melodiei nu poate fi gol.");
                txtTitlu.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtArtist.Text))
            {
                ShowErrorStatus("Numele artistului nu poate fi gol.");
                txtArtist.Focus();
                return;
            }

            var melodie = new Melodie
            {
                Titlu = txtTitlu.Text.Trim(),
                Artist = txtArtist.Text.Trim(),
                GenMuzical = txtGenMuzical.Text.Trim(),
                AnLansare = (int)numAnLansare.Value,
                PunctajTotal = 0
            };

            bool success = _melodieRepository.AdaugaMelodie(melodie);

            if (success)
            {
                ShowSuccessStatus("Melodia a fost salvată cu succes!");
                MelodieSaved?.Invoke(this, EventArgs.Empty); // Trigger event
                await Task.Delay(2000); 
                OnRequestClose();
                ClearFormFields();
            }
            else
            {
                ShowErrorStatus("Eroare la salvarea melodiei. Verificați datele introduse sau încercați mai târziu.");
            }
        }

        private void BtnAnuleaza_Click(object sender, EventArgs e)
        {
            OnRequestClose();
            ClearFormFields();
            ClearStatusLabel();
            ThemeHelper.StyleSpecificLabel(lblStatus, ThemeHelper.TextColorOnDark); // Reset to default
        }

        /// <summary>
        /// Declanșează evenimentul RequestClose.
        /// </summary>
        protected virtual void OnRequestClose()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
        
        private void ClearFormFields()
        {
            txtTitlu.Clear();
            txtArtist.Clear();
            txtGenMuzical.Clear();
            numAnLansare.Value = numAnLansare.Minimum; // Or a default year like DateTime.Now.Year
        }

        private void ShowSuccessStatus(string message)
        {
            lblStatus.Text = message;
            ThemeHelper.StyleSpecificLabel(lblStatus, ThemeHelper.SuccessColor); // Use ThemeHelper color if preferred, or keep Color.Green
            lblStatus.Visible = true;
        }

        private void ShowErrorStatus(string message)
        {
            lblStatus.Text = message;
            ThemeHelper.StyleSpecificLabel(lblStatus, ThemeHelper.RedAccent); // Use ThemeHelper color for error
            lblStatus.Visible = true;
        }
        
        private void ClearStatusLabel()
        {
            lblStatus.Text = string.Empty;
            lblStatus.Visible = false;
        }
    }
} 