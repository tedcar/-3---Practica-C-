using System;
using System.Windows.Forms;
using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess;

namespace MelodiiApp.UserInterface.Forms
{
    public partial class EditIntervievatForm : Form
    {
        private readonly IntervievatRepository _repo = new IntervievatRepository();
        private readonly Intervievat _original;

        public EditIntervievatForm(Intervievat intervievat)
        {
            _original = intervievat;
            InitializeComponent();
            // Populate controls
            txtNume.Text = intervievat.NumeComplet;
            nudVarsta.Value = intervievat.Varsta <= nudVarsta.Maximum ? intervievat.Varsta : nudVarsta.Maximum;
            txtLocalitate.Text = intervievat.Localitate;
        }

        public bool Saved { get; private set; }

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

            var updated = new Intervievat(_original.IntervievatID, nume, varsta, localitate);
            if (_repo.UpdateIntervievat(updated))
            {
                Saved = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Eroare la salvare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAnuleaza_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
