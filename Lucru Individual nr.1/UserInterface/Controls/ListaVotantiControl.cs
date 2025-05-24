using MelodiiApp.DataAccess;
using MelodiiApp.UserInterface.Helpers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MelodiiApp.Core.DomainModels; // Required for AppUser

namespace MelodiiApp.UserInterface.Controls
{
    public partial class ListaVotantiControl : UserControl
    {
        private DataGridView dgvVotanti;
        private Label lblTitluListaVotanti;
        // No explicit refresh button is requested, relying on VisibleChanged

        public ListaVotantiControl()
        {
            // This call is for any components that might be defined in a separate .Designer.cs file if it existed.
            // Since we are defining components programmatically here, ensure this doesn't cause issues.
            // If there's no .Designer.cs, this could be removed or a dummy one created if the project structure demands it.
            // For now, we'll assume it's safe or will be handled by project structure.
            // InitializeComponent(); 

            InitializeCustomComponents(); // Initialize our programmatically added controls
            SetupDataGridView();
            ThemeHelper.ApplyUserControlTheme(this); // Apply general theme

            this.VisibleChanged += (s, e) => { if (this.Visible) RefreshData(); };
        }

        // A minimal InitializeComponent to satisfy the partial class structure if no .Designer.cs file is present.
        // If a .Designer.cs file IS added to the project later, this method would be auto-generated there.
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ListaVotantiControl
            // 
            this.Name = "ListaVotantiControl";
            this.Size = new System.Drawing.Size(500, 350); // Default size, will be docked in MainForm
            this.ResumeLayout(false);
        }

        private void InitializeCustomComponents()
        {
            this.lblTitluListaVotanti = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(10, 10),
                Name = "lblTitluListaVotanti",
                Text = "Listă Votanți Înregistrați (Utilizatori Rol 'User')"
            };
            this.Controls.Add(this.lblTitluListaVotanti);

            this.dgvVotanti = new DataGridView
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                Location = new Point(10, 40), // Position below the title
                Name = "dgvVotanti",
                TabIndex = 1,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                // Calculate dynamic size based on parent control dimensions when docked
                Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - 50) 
            };
            this.Controls.Add(this.dgvVotanti);

            // Handle resizing of the UserControl to resize the DataGridView
            this.SizeChanged += (s, e) => {
                if (dgvVotanti != null) {
                    dgvVotanti.Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - 50);
                }
            };
        }

        private void SetupDataGridView()
        {
            if (dgvVotanti == null) return;

            dgvVotanti.AutoGenerateColumns = false;
            dgvVotanti.Columns.Clear();

            dgvVotanti.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "IdCol", 
                DataPropertyName = "Id", 
                HeaderText = "ID Utilizator", 
                Width = 80 
            });
            dgvVotanti.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "UsernameCol", 
                DataPropertyName = "Username", 
                HeaderText = "Username", 
                Width = 150 
            });
            dgvVotanti.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "EmailCol", 
                DataPropertyName = "Email", 
                HeaderText = "Email", 
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill 
            });
            dgvVotanti.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                Name = "RoleCol", 
                DataPropertyName = "Role", 
                HeaderText = "Rol", 
                Width = 100 
            });
            
            ThemeHelper.ApplyDataGridViewStyle(dgvVotanti); // Apply custom styling
        }

        public void RefreshData()
        {
            if (dgvVotanti == null) return;
            try
            {
                var votanti = InMemoryAuthService.Users
                                     .Where(u => u.Role == "User")
                                     .OrderBy(u => u.Username)
                                     .ToList();
                
                dgvVotanti.DataSource = null; 
                if (votanti.Any())
                {
                    dgvVotanti.DataSource = votanti;
                }
                else
                {
                    // Optionally, display a message if the list is empty,
                    // though a blank grid also indicates this.
                    // For example, by adding a Label control or using a MessageBox.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea listei de votanți: {ex.Message}", "Eroare Listă Votanți", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
