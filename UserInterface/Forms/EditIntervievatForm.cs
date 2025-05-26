using System;
using System.Windows.Forms;
using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess;

namespace MelodiiApp.UserInterface.Forms
{
    /// <summary>
    /// Formular pentru editarea detaliilor unui intervievat existent.
    /// </summary>
    public partial class EditIntervievatForm : Form
    {
        private readonly IntervievatRepository _repo = new IntervievatRepository();
        private readonly Intervievat _original; // Stochează datele originale ale intervievatului pentru referință.

        /// <summary>
        /// Constructorul formularului de editare a unui intervievat.
        /// </summary>
        /// <param name="intervievat">Obiectul Intervievat care urmează a fi editat.</param>
        public EditIntervievatForm(Intervievat intervievat)
        {
            _original = intervievat;
            InitializeComponent();
            // Populează controalele formularului cu datele intervievatului existent.
            txtNume.Text = intervievat.NumeComplet;
            if (intervievat.Varsta.HasValue)
            {
                // Asigură că valoarea vârstei este în limitele controlului NumericUpDown.
                nudVarsta.Value = Math.Max(nudVarsta.Minimum, Math.Min(intervievat.Varsta.Value, nudVarsta.Maximum));
            }
            else
            {
                nudVarsta.Value = nudVarsta.Minimum; // Valoare implicită dacă vârsta nu este setată.
            }
            txtLocalitate.Text = intervievat.Localitate;
        }

        /// <summary>
        /// Indică dacă modificările au fost salvate cu succes.
        /// </summary>
        public bool Saved { get; private set; }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul de salvare.
        /// Validează datele introduse și actualizează înregistrarea intervievatului în baza de date.
        /// </summary>
        private void btnSalveaza_Click(object sender, EventArgs e)
        {
            string nume = txtNume.Text.Trim();
            int varsta = (int)nudVarsta.Value;
            string localitate = txtLocalitate.Text.Trim();

            if (string.IsNullOrWhiteSpace(nume))
            {
                MessageBox.Show("Completați numele.", "Validare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crează un nou obiect Intervievat cu datele actualizate.
            var updated = new Intervievat(_original.IntervievatID, nume, varsta, localitate);
            // Apelează repository-ul pentru a actualiza datele.
            if (_repo.UpdateIntervievat(updated))
            {
                Saved = true;
                DialogResult = DialogResult.OK; // Indică salvarea cu succes.
                Close(); // Închide formularul.
            }
            else
            {
                MessageBox.Show("Eroare la salvare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul de anulare.
        /// Închide formularul fără a salva modificările.
        /// </summary>
        private void btnAnuleaza_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; // Indică anularea.
            Close(); // Închide formularul.
        }

        /// <summary>
        /// Se declanșează la încărcarea formularului.
        /// Poate fi folosit pentru inițializări suplimentare.
        /// </summary>
        private void EditIntervievatForm_Load(object sender, EventArgs e)
        {
            // Orice logică necesară la încărcarea formularului.
        }
    }
}
