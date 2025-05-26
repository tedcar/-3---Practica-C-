using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess;
using MelodiiApp.UserInterface.Helpers;
using System;
using System.Windows.Forms;

namespace MelodiiApp.UserInterface.Forms
{
    /// <summary>
    /// Formular pentru editarea detaliilor unei melodii existente.
    /// </summary>
    public partial class EditMelodieForm : Form
    {
        private Melodie _melodieToEdit; // Melodia care urmează a fi editată.
        private MelodieRepository _melodieRepository; // Repository pentru operațiuni pe melodii.

        /// <summary>
        /// Constructorul formularului de editare a unei melodii.
        /// </summary>
        /// <param name="melodie">Obiectul Melodie care urmează a fi editat.</param>
        public EditMelodieForm(Melodie melodie)
        {
            InitializeComponent();
            ThemeHelper.ApplyFormTheme(this); // Aplică tema generală a aplicației.

            _melodieToEdit = melodie ?? throw new ArgumentNullException(nameof(melodie));
            _melodieRepository = new MelodieRepository();

            PopulateFields(); // Populează câmpurile formularului cu datele melodiei.
        }

        /// <summary>
        /// Populează câmpurile formularului cu datele melodiei selectate pentru editare.
        /// </summary>
        private void PopulateFields()
        {
            if (_melodieToEdit != null)
            {
                txtTitlu.Text = _melodieToEdit.Titlu;
                txtArtist.Text = _melodieToEdit.Artist;
                txtGenMuzical.Text = _melodieToEdit.GenMuzical;
                // AnLansare ar putea necesita un control NumericUpDown sau validare atentă dacă este TextBox.
                txtAnLansare.Text = _melodieToEdit.AnLansare.ToString(); 
            }
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul de salvare.
        /// Validează datele introduse și actualizează melodia în baza de date.
        /// </summary>
        private void btnSalveaza_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitlu.Text))
            {
                MessageBox.Show("Titlul melodiei nu poate fi gol.", "Validare Eșuată", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitlu.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtArtist.Text))
            {
                MessageBox.Show("Artistul melodiei nu poate fi gol.", "Validare Eșuată", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtArtist.Focus();
                return;
            }
            // Validare de bază pentru anul lansării.
            if (!int.TryParse(txtAnLansare.Text, out int anLansare) || anLansare <= 0 || anLansare > DateTime.Now.Year + 5) 
            {
                 MessageBox.Show($"Anul lansării trebuie să fie un număr valid (ex: între 1800 și {DateTime.Now.Year + 5}).", "Validare Eșuată", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAnLansare.Focus();
                return;
            }

            // Actualizează obiectul _melodieToEdit cu noile valori.
            _melodieToEdit.Titlu = txtTitlu.Text.Trim();
            _melodieToEdit.Artist = txtArtist.Text.Trim();
            _melodieToEdit.GenMuzical = txtGenMuzical.Text.Trim(); // Câmp opțional, poate fi gol.
            _melodieToEdit.AnLansare = anLansare;

            // Apelează repository-ul pentru a actualiza melodia.
            if (_melodieRepository.UpdateMelodie(_melodieToEdit))
            {
                MessageBox.Show("Melodia a fost actualizată cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Indică succesul operațiunii.
                this.Close(); // Închide formularul.
            }
            else
            {
                MessageBox.Show("A apărut o eroare la actualizarea melodiei.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul de anulare.
        /// Închide formularul fără a salva modificările.
        /// </summary>
        private void btnAnuleaza_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Indică anularea.
            this.Close(); // Închide formularul.
        }
    }
} 