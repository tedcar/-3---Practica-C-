using System;
using System.Windows.Forms;
using MelodiiApp.DataAccess; // Updated namespace
using MelodiiApp.Core.DomainModels; // Corrected namespace for AppUser and other models
using MelodiiApp.Core.Models; // Added for UserRole
using MaterialSkin;
using MaterialSkin.Controls;

namespace MelodiiApp.UserInterface.Forms // Updated namespace
{
    /// <summary>
    /// Formularul de autentificare pentru aplicație.
    /// Permite utilizatorilor existenți să se conecteze și oferă o opțiune de navigare către formularul de înregistrare.
    /// </summary>
    public partial class LoginForm : MaterialForm
    {
        /// <summary>
        /// Constructorul formularului de autentificare.
        /// Inițializează componentele vizuale și configurează managerul MaterialSkin pentru temă.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            // Inițializează MaterialSkinManager și adaugă acest formular pentru managementul temei.
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            // Tema și schema de culori sunt gestionate global prin ThemeHelper sau la pornirea aplicației,
            // deci nu mai este necesară setarea lor explicită aici dacă sunt deja configurate.
        }

        /// <summary>
        /// Se declanșează la încărcarea formularului.
        /// Poate fi folosit pentru inițializări suplimentare după ce controalele sunt create.
        /// </summary>
        private void Login_Load(object sender, EventArgs e)
        {
            // Orice logică necesară la încărcarea formularului.
            // Reaplicarea temei aici poate fi redundantă dacă este gestionată global.
        }

        /// <summary>
        /// gestionează evenimentul de click pentru butonul de autentificare.
        /// Validează datele de intrare și încearcă autentificarea utilizatorului.
        /// La succes, deschide formularul principal; altfel, afișează un mesaj de eroare.
        /// </summary>
        private void button1_Click(object sender, EventArgs e) // Butonul de Login
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vă rugăm introduceți numele de utilizator și parola.", "Eroare Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AppUser authenticatedUser = AuthService.AuthenticateUser(username, password);

            if (authenticatedUser != null)
            {
                AuthService.CurrentLoggedInUser = authenticatedUser;

                UserRole roleEnum;
                // Încearcă conversia rolului din string în enum UserRole.
                if (Enum.TryParse<UserRole>(authenticatedUser.Role, true, out roleEnum))
                {
                    // Parsare cu succes.
                }
                else
                {
                    // Implicit User dacă parsarea eșuează sau rolul este necunoscut.
                    // Ideal, acest caz ar trebui logat.
                    roleEnum = UserRole.User; 
                    MessageBox.Show("Rolul utilizatorului nu a putut fi determinat, se va folosi rolul implicit User.", "Atenție Rol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Crează și afișează formularul principal, pasând rolul utilizatorului.
                MainForm mainForm = new MainForm(roleEnum); 
                mainForm.FormClosed += (s, args) => 
                {
                    // La închiderea MainForm, se reafișează LoginForm.
                    // Asigură-te că tema este consistentă dacă este necesar.
                    this.Show(); 
                };
                this.Hide(); 
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Nume de utilizator sau parolă incorectă.", "Eroare Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul care duce la formularul de înregistrare.
        /// Ascunde formularul curent și afișează formularul de înregistrare.
        /// </summary>
        private void button2_Click(object sender, EventArgs e) // Butonul către formularul de Înregistrare
        {
            // Crează și afișează formularul de înregistrare.
            RegistrationForm registrationForm = new RegistrationForm(); 
            registrationForm.FormClosed += (s, args) => 
            {
                // La închiderea RegistrationForm, se reafișează LoginForm.
                this.Show(); 
            };
            this.Hide(); 
            registrationForm.Show();
        }

        /// <summary>
        /// Se declanșează la modificarea textului în câmpul pentru username.
        /// Poate fi utilizat pentru validări în timp real sau alte logici.
        /// </summary>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Logică suplimentară dacă este necesară la schimbarea textului.
        }
    }
} 