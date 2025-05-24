using MelodiiApp.Core.DomainModels;
using MelodiiApp.DataAccess;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MelodiiApp.UserInterface.Helpers;

namespace MelodiiApp.UserInterface.Controls
{
    /// <summary>
    /// Control pentru afișarea clasamentului celor mai populare N melodii.
    /// </summary>
    public partial class TopNMelodiiControl : UserControl
    {
        private readonly MelodieRepository _melodieRepository;
        private const int DefaultTopN = 10; // Default number of songs to show

        /// <summary>
        /// Initializează o nouă instanță a clasei <see cref="TopNMelodiiControl"/>.
        /// </summary>
        public TopNMelodiiControl()
        {
            InitializeComponent();
            _melodieRepository = new MelodieRepository();

            SetupDataGridView();
            ThemeHelper.ApplyUserControlTheme(this);
            
            this.VisibleChanged += TopNMelodiiControl_VisibleChanged;
            btnRefreshClasament.Click += BtnRefreshClasament_Click;
            UpdateTitleLabel(DefaultTopN);
        }

        private void TopNMelodiiControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadTopMelodii();
            }
        }
        
        private void UpdateTitleLabel(int n)
        {
            lblTitluClasament.Text = $"Top {n} Melodii Populare";
        }

        private void SetupDataGridView()
        {
            dgvTopMelodii.AutoGenerateColumns = false;
            dgvTopMelodii.Columns.Clear();
            dgvTopMelodii.Columns.Add(new DataGridViewTextBoxColumn { Name = "RankCol", HeaderText = "#", Width = 40, ReadOnly = true, DataPropertyName = "Rank" });
            dgvTopMelodii.Columns.Add(new DataGridViewTextBoxColumn { Name = "TitluCol", DataPropertyName = "Titlu", HeaderText = "Titlu", Width = 200, ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvTopMelodii.Columns.Add(new DataGridViewTextBoxColumn { Name = "ArtistCol", DataPropertyName = "Artist", HeaderText = "Artist", Width = 150, ReadOnly = true });
            dgvTopMelodii.Columns.Add(new DataGridViewTextBoxColumn { Name = "GenCol", DataPropertyName = "GenMuzical", HeaderText = "Gen Muzical", Width = 100, ReadOnly = true });
            dgvTopMelodii.Columns.Add(new DataGridViewTextBoxColumn { Name = "AnCol", DataPropertyName = "AnLansare", HeaderText = "An", Width = 60, ReadOnly = true });
            dgvTopMelodii.Columns.Add(new DataGridViewTextBoxColumn { Name = "PunctajCol", DataPropertyName = "PunctajTotal", HeaderText = "Punctaj", Width = 70, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } });
            
            dgvTopMelodii.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTopMelodii.MultiSelect = false;
            dgvTopMelodii.AllowUserToAddRows = false;
            dgvTopMelodii.AllowUserToDeleteRows = false;
            dgvTopMelodii.AllowUserToResizeRows = false;
            dgvTopMelodii.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvTopMelodii.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
        }

        /// <summary>
        /// Încarcă și afișează primele N melodii în clasament.
        /// </summary>
        /// <param name="n">Numărul de melodii de afișat în top. Valoarea implicită este 10.</param>
        public void LoadTopMelodii(int n = DefaultTopN)
        {
            try
            {
                var topMelodii = _melodieRepository.GetTopNMelodii(n)
                    .Select((melodie, index) => new 
                    {
                        Rank = index + 1,
                        melodie.MelodieID,
                        melodie.Titlu,
                        melodie.Artist,
                        melodie.GenMuzical,
                        melodie.AnLansare,
                        melodie.PunctajTotal
                    }).ToList();

                dgvTopMelodii.DataSource = null;
                dgvTopMelodii.DataSource = topMelodii;
                UpdateTitleLabel(n); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la încărcarea clasamentului melodiilor: {ex.Message}", "Eroare Clasament", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefreshClasament_Click(object sender, EventArgs e)
        {
            LoadTopMelodii();
        }

        /// <summary>
        /// Public method to refresh the data displayed in the control.
        /// </summary>
        public void RefreshData()
        {
            LoadTopMelodii(); // Uses the default N value or could be parameterized if needed
        }
    }
} 