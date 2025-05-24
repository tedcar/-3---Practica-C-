using MelodiiApp.DataAccess;
using MelodiiApp.UserInterface.Helpers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MelodiiApp.UserInterface.Controls
{
    public partial class ListaParticipantiControl : UserControl
    {
        private readonly IntervievatRepository _intervievatRepository;
        private DataGridView dgvParticipanti;
        private Button btnRefreshLista;
        private Label lblTitluLista;

        public ListaParticipantiControl()
        {
            InitializeComponent(); // For any designer components
            _intervievatRepository = new IntervievatRepository();
            InitializeCustomComponents();
            SetupDataGridView();
            ThemeHelper.ApplyUserControlTheme(this);

            this.VisibleChanged += (s, e) => { if (this.Visible) RefreshData(); };
            if (btnRefreshLista != null) btnRefreshLista.Click += (s, e) => RefreshData();
        }

        private void InitializeCustomComponents()
        {
            this.lblTitluLista = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Location = new Point(10, 10),
                Name = "lblTitluLista",
                Text = "Listă Completă Participanți (Intervievați)"
            };
            this.Controls.Add(this.lblTitluLista);

            this.btnRefreshLista = new Button
            {
                Location = new Point(10, 40),
                Name = "btnRefreshLista",
                Size = new Size(120, 30),
                Text = "Actualizează Lista"
            };
            this.Controls.Add(this.btnRefreshLista);
            
            // Optional Close Button if this control is shown in a dedicated way
            // Button btnInchide = new Button { Text = "Închide", Location = new Point(140, 40), Size = new Size(100,30)};
            // // btnInchide.Click += (s,e) => RequestClose?.Invoke(this, EventArgs.Empty); // Can be uncommented if a close button is added here
            // this.Controls.Add(btnInchide);

            this.dgvParticipanti = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParticipanti)).BeginInit();
            this.dgvParticipanti.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvParticipanti.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParticipanti.Location = new Point(10, 80);
            this.dgvParticipanti.Name = "dgvParticipanti";
            this.dgvParticipanti.Size = new Size(this.Width - 20, this.Height - 90); // Adjust as needed
            this.dgvParticipanti.TabIndex = 1;
            this.Controls.Add(this.dgvParticipanti);
            ((System.ComponentModel.ISupportInitialize)(this.dgvParticipanti)).EndInit();
        }

        private void SetupDataGridView()
        {
            if (dgvParticipanti == null) return;
            dgvParticipanti.AutoGenerateColumns = false;
            dgvParticipanti.Columns.Clear();
            dgvParticipanti.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdCol", DataPropertyName = "IntervievatID", HeaderText = "ID", Width = 50 });
            dgvParticipanti.Columns.Add(new DataGridViewTextBoxColumn { Name = "NumeCol", DataPropertyName = "NumeComplet", HeaderText = "Nume Complet", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvParticipanti.Columns.Add(new DataGridViewTextBoxColumn { Name = "VarstaCol", DataPropertyName = "Varsta", HeaderText = "Vârstă", Width = 70 });
            dgvParticipanti.Columns.Add(new DataGridViewTextBoxColumn { Name = "LocalitateCol", DataPropertyName = "Localitate", HeaderText = "Localitate", Width = 150 });
            dgvParticipanti.Columns.Add(new DataGridViewTextBoxColumn { Name = "ScorCol", DataPropertyName = "ScorTotalConcurs", HeaderText = "Scor Concurs", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            // Add more columns if needed, e.g., Email from AppUser if linked and relevant for this report
        }

        public void RefreshData()
        {
            if (dgvParticipanti == null) return;
            try
            {
                var participanti = _intervievatRepository.GetAllIntervievati()
                                     .OrderBy(i => i.NumeComplet).ToList(); // Order by name
                dgvParticipanti.DataSource = null;
                dgvParticipanti.DataSource = participanti;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea listei de participanți: {ex.Message}", "Eroare Listă", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Partial class might have an InitializeComponent from a designer file if one is created.
        // If not, this is fine. If a designer file is added, ensure InitializeComponent() is called.
    }
} 