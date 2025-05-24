using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MelodiiApp.UserInterface.Helpers;
using MelodiiApp.DataAccess;

namespace MelodiiApp.UserInterface.Controls
{
    public partial class TopNIntervievatiControl : UserControl
    {
        private readonly IntervievatRepository _intervievatRepository;
        private const int DefaultTopN = 10;
        private System.Windows.Forms.DataGridView dgvTopIntervievati;
        private System.Windows.Forms.Label lblTitluClasamentIntervievati;
        private System.Windows.Forms.Button btnRefreshClasamentIntervievati;

        public TopNIntervievatiControl()
        {
            InitializeComponent();
            _intervievatRepository = new IntervievatRepository();
            InitializeCustomComponents();

            SetupDataGridView();
            ThemeHelper.ApplyUserControlTheme(this); 

            this.VisibleChanged += TopNIntervievatiControl_VisibleChanged;
            if (btnRefreshClasamentIntervievati != null) 
                btnRefreshClasamentIntervievati.Click += (s,e) => RefreshData();
            
            UpdateTitleLabel(DefaultTopN);
        }

        private void InitializeCustomComponents()
        {
            // Title Label
            this.lblTitluClasamentIntervievati = new System.Windows.Forms.Label();
            this.lblTitluClasamentIntervievati.AutoSize = true;
            this.lblTitluClasamentIntervievati.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitluClasamentIntervievati.Location = new System.Drawing.Point(10, 10);
            this.lblTitluClasamentIntervievati.Name = "lblTitluClasamentIntervievati";
            this.lblTitluClasamentIntervievati.Text = "Top N Intervievați";
            this.Controls.Add(this.lblTitluClasamentIntervievati);

            // Refresh Button
            this.btnRefreshClasamentIntervievati = new System.Windows.Forms.Button();
            this.btnRefreshClasamentIntervievati.Location = new System.Drawing.Point(10, 40);
            this.btnRefreshClasamentIntervievati.Name = "btnRefreshClasamentIntervievati";
            this.btnRefreshClasamentIntervievati.Size = new System.Drawing.Size(120, 30);
            this.btnRefreshClasamentIntervievati.Text = "Actualizează";
            this.Controls.Add(this.btnRefreshClasamentIntervievati);

            // DataGridView
            this.dgvTopIntervievati = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopIntervievati)).BeginInit();
            this.dgvTopIntervievati.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTopIntervievati.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopIntervievati.Location = new System.Drawing.Point(10, 80);
            this.dgvTopIntervievati.Name = "dgvTopIntervievati";
            this.dgvTopIntervievati.Size = new System.Drawing.Size(this.Width - 20, this.Height - 90);
            this.dgvTopIntervievati.TabIndex = 1; 
            this.Controls.Add(this.dgvTopIntervievati);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopIntervievati)).EndInit();

            // Hide placeholder label if it exists from designer, as we are adding proper controls
            if (this.Controls.ContainsKey("lblPlaceholder"))
            {
                this.Controls["lblPlaceholder"].Visible = false;
            }
        }

        private void TopNIntervievatiControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                RefreshData();
            }
        }

        private void UpdateTitleLabel(int n)
        {
            if (lblTitluClasamentIntervievati != null) 
                lblTitluClasamentIntervievati.Text = $"Top {n} Intervievați după Scor";
        }

        private void SetupDataGridView()
        {
            if (dgvTopIntervievati == null) return;
            dgvTopIntervievati.AutoGenerateColumns = false;
            dgvTopIntervievati.Columns.Clear();
            dgvTopIntervievati.Columns.Add(new DataGridViewTextBoxColumn { Name = "RankCol", HeaderText = "#", Width = 40, ReadOnly = true, DataPropertyName = "Rank" });
            dgvTopIntervievati.Columns.Add(new DataGridViewTextBoxColumn { Name = "NumeCol", DataPropertyName = "NumeComplet", HeaderText = "Nume Complet", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvTopIntervievati.Columns.Add(new DataGridViewTextBoxColumn { Name = "VarstaCol", DataPropertyName = "Varsta", HeaderText = "Vârstă", Width = 70, ReadOnly = true });
            dgvTopIntervievati.Columns.Add(new DataGridViewTextBoxColumn { Name = "LocalitateCol", DataPropertyName = "Localitate", HeaderText = "Localitate", Width = 150, ReadOnly = true });
            dgvTopIntervievati.Columns.Add(new DataGridViewTextBoxColumn { Name = "ScorCol", DataPropertyName = "ScorTotalConcurs", HeaderText = "Scor Concurs", Width = 100, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
        }

        public void RefreshData(int n = DefaultTopN)
        {
            if (dgvTopIntervievati == null) return;
            try
            {
                var topIntervievati = _intervievatRepository.GetAllIntervievati()
                    .OrderByDescending(i => i.ScorTotalConcurs)
                    .Take(n)
                    .Select((interv, index) => new 
                    {
                        Rank = index + 1,
                        interv.IntervievatID,
                        interv.NumeComplet,
                        interv.Varsta,
                        interv.Localitate,
                        interv.ScorTotalConcurs
                    }).ToList();

                dgvTopIntervievati.DataSource = null;
                dgvTopIntervievati.DataSource = topIntervievati;
                UpdateTitleLabel(n);
            }
            catch (Exception ex)
            {
                 MessageBox.Show($"Eroare la încărcarea clasamentului intervievaților: {ex.Message}", "Eroare Clasament", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 