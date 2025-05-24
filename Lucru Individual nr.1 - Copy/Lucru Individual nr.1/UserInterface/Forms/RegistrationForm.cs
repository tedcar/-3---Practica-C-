using System;
using System.Windows.Forms;
using MelodiiApp.Core.DomainModels; // Corrected namespace
using MelodiiApp.DataAccess; // Updated namespace
using MaterialSkin;

namespace MelodiiApp.UserInterface.Forms // Updated namespace
{
    public partial class RegistrationForm : MaterialSkin.Controls.MaterialForm
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // Register button
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

            string role = "User"; // Default role

            bool success = InMemoryAuthService.RegisterUser(username, email, password, role);

            if (success)
            {
                // Creează intervievat pentru acest user
                var intervievatRepo = new IntervievatRepository();
                var intervievatNou = new Intervievat
                {
                    NumeComplet = $"{nume} {prenume}",
                    Varsta = Math.Max(1, (int)Math.Floor((DateTime.Now - dataNastere).TotalDays / 365.25)),
                    Localitate = "-" // placeholder
                };
                intervievatRepo.AdaugaIntervievat(intervievatNou);

                // Asociază intervievatul cu utilizatorul
                var user = InMemoryAuthService.GetUserByUsername(username);
                if (user != null)
                {
                    user.IntervievatID = intervievatNou.IntervievatID;
                }

                MessageBox.Show("Înregistrare reușită! Vă puteți autentifica acum.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Close registration form, LoginForm will re-show due to FormClosed event
            }
            else
            {
                MessageBox.Show("Numele de utilizator există deja. Vă rugăm alegeți altul.", "Eroare Înregistrare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e) // Back to Login button
        {
            this.Close(); // Close registration form, LoginForm will re-show due to FormClosed event
        }
    }
} 