using System;
using System.Windows.Forms;
using MelodiiApp.Core.DomainModels; // Corrected namespace
using MelodiiApp.DataAccess; // Updated namespace
using MaterialSkin;

namespace MelodiiApp.UserInterface.Forms // Updated namespace
{
    /// <summary>
    /// Formularul de înregistrare pentru utilizatori noi.
    /// Permite utilizatorilor să creeze un cont nou în aplicație.
    /// </summary>
    public partial class RegistrationForm : MaterialSkin.Controls.MaterialForm
    {
        /// <summary>
        /// Constructorul formularului de înregistrare.
        /// Inițializează componentele vizuale.
        /// </summary>
        public RegistrationForm()
        {
            InitializeComponent();
            // Inițializează MaterialSkinManager și adaugă acest formular pentru managementul temei.
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul de înregistrare.
        /// Validează datele introduse de utilizator și încearcă crearea unui nou cont.
        /// Afișează mesaje corespunzătoare în caz de succes sau eroare.
        /// </summary>
        private void button1_Click(object sender, EventArgs e) // Butonul de Înregistrare
        {
            string username = textBox1.Text;
            string email = textBox2.Text;
            string nume = textBox3.Text;
            string prenume = textBox4.Text;
            DateTime dataNastere = dateTimePicker1.Value;
            string password = textBox5.Text;
            string confirmPassword = textBox6.Text;

            if (string.IsNullOrWhiteSpace(username) || 
                string.IsNullOrWhiteSpace(email) || 
                string.IsNullOrWhiteSpace(nume) || 
                string.IsNullOrWhiteSpace(prenume) || 
                string.IsNullOrWhiteSpace(password) || 
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Nume utilizator, Email, Nume, Prenume, Parola și Confirmare Parola sunt obligatorii!", "Eroare Înregistrare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Parolele nu se potrivesc!", "Eroare Înregistrare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password.Length < 4)
            {
                MessageBox.Show("Parola trebuie să conțină cel puțin 4 caractere!", "Eroare Înregistrare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string role = "User"; // Rol implicit
            bool saveCredentials = chkSalvareCredentiale.Checked; // Preluare stare checkbox

            // Apelează serviciul de autentificare pentru a înregistra utilizatorul.
            bool success = AuthService.RegisterUser(username, email, password, role, saveCredentials);

            if (success)
            {
                MessageBox.Show("Înregistrare reușită! Vă puteți autentifica acum.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Închide formularul de înregistrare; LoginForm se va reafișa datorită evenimentului FormClosed
            }
            else
            {
                MessageBox.Show("Numele de utilizator există deja. Vă rugăm alegeți altul.", "Eroare Înregistrare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul de revenire la formularul de autentificare.
        /// Închide formularul curent de înregistrare.
        /// </summary>
        private void button2_Click(object sender, EventArgs e) // Butonul Înapoi la Login
        {
            this.Close(); // Închide formularul de înregistrare; LoginForm se va reafișa datorită evenimentului FormClosed
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
} 