using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using MelodiiApp.UserInterface.Helpers;

namespace MelodiiApp.UserInterface.Controls
{
    /// <summary>
    /// Control pentru adăugarea unui intervievat nou.
    /// </summary>
    public partial class AdaugaIntervievatControl : UserControl
    {
        private readonly IntervievatRepository _intervievatRepository;

        /// <summary>
        /// Eveniment declanșat când se solicită închiderea acestui control.
        /// </summary>
        public event EventHandler RequestClose;

        /// <summary>
        /// Initializează o nouă instanță a clasei <see cref="AdaugaIntervievatControl"/>.
        /// </summary>
        public AdaugaIntervievatControl()
        {
            InitializeComponent();
            _intervievatRepository = new IntervievatRepository();
            ThemeHelper.ApplyUserControlTheme(this);

            btnSalveazaIntervievat.Click += BtnSalveazaIntervievat_Click;
            btnAnuleaza.Click += BtnAnuleaza_Click;
            ClearStatusLabel();
        }

        private async void BtnSalveazaIntervievat_Click(object sender, EventArgs e)
        {
            ClearStatusLabel();
            ThemeHelper.StyleSpecificLabel(lblStatus, ThemeHelper.TextOnLight);

            if (string.IsNullOrWhiteSpace(txtNumeComplet.Text))
            {
                ShowErrorStatus("Numele complet nu poate fi gol.");
                txtNumeComplet.Focus();
                return;
            }

            var intervievat = new Intervievat
            {
                NumeComplet = txtNumeComplet.Text.Trim(),
                Varsta = (int)numVarsta.Value,
                Localitate = txtLocalitate.Text.Trim(), 
                ScorTotalConcurs = 0 
            };

            bool success = _intervievatRepository.AdaugaIntervievat(intervievat);

            if (success)
            {
                ShowSuccessStatus("Intervievatul a fost salvat cu succes!");
                await Task.Delay(2000); 
                OnRequestClose();
                ClearFormFields();
            }
            else
            {
                ShowErrorStatus("Eroare la salvarea intervievatului. Verificați datele sau încercați mai târziu.");
            }
        }

        private void BtnAnuleaza_Click(object sender, EventArgs e)
        {
            OnRequestClose();
            ClearFormFields();
            ClearStatusLabel();
            ThemeHelper.StyleSpecificLabel(lblStatus, ThemeHelper.TextOnLight);
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
            txtNumeComplet.Clear();
            txtLocalitate.Clear();
            numVarsta.Value = 18;
        }

        private void ShowSuccessStatus(string message)
        {
            lblStatus.Text = message;
            ThemeHelper.StyleSpecificLabel(lblStatus, Color.Green);
            lblStatus.Visible = true;
        }

        private void ShowErrorStatus(string message)
        {
            lblStatus.Text = message;
            ThemeHelper.StyleSpecificLabel(lblStatus, ThemeHelper.RedAccent);
            lblStatus.Visible = true;
        }
        
        private void ClearStatusLabel()
        {
            lblStatus.Text = string.Empty;
            lblStatus.Visible = false;
        }
    }
} 