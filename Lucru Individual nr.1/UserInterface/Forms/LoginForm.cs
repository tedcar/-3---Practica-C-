using System;
using System.Windows.Forms;
using MelodiiApp.DataAccess; // Updated namespace
using MelodiiApp.Core.DomainModels; // Corrected namespace for AppUser and other models
using MelodiiApp.Core.Models; // Added for UserRole
using MaterialSkin;
using MaterialSkin.Controls;

namespace MelodiiApp.UserInterface.Forms // Updated namespace
{
    public partial class LoginForm : MaterialForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // Initialization if needed
        }

        private void button1_Click(object sender, EventArgs e) // Login button
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vă rugăm introduceți numele de utilizator și parola.", "Eroare Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AppUser authenticatedUser = InMemoryAuthService.AuthenticateUser(username, password);

            if (authenticatedUser != null)
            {
                InMemoryAuthService.CurrentLoggedInUser = authenticatedUser;

                // Determine UserRole from AppUser.Role string
                UserRole roleEnum;
                if (Enum.TryParse<UserRole>(authenticatedUser.Role, true, out roleEnum))
                {
                    // Successfully parsed
                }
                else
                {
                    // Default to User role if parsing fails or role is unrecognized
                    // Log this ideally
                    roleEnum = UserRole.User; 
                    MessageBox.Show("Rolul utilizatorului nu a putut fi determinat, se va folosi rolul implicit User.", "Atenție Rol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                MainForm mainForm = new MainForm(roleEnum); // Pass the role
                mainForm.FormClosed += (s, args) => this.Show(); // Show login form when main form closes
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Nume de utilizator sau parolă incorectă.", "Eroare Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e) // To Registration form
        {
            RegistrationForm registrationForm = new RegistrationForm(); 
            registrationForm.FormClosed += (s, args) => this.Show(); // Show login form when registration form closes
            registrationForm.Show();
            this.Hide(); 
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // If validation or other logic is needed here, it can be added.
        }
    }
} 