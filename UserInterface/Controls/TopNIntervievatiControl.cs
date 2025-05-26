using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MelodiiApp.UserInterface.Helpers;
using MelodiiApp.DataAccess;

namespace MelodiiApp.UserInterface.Controls
{
    /// <summary>
    /// Control utilizator pentru afișarea clasamentului Top N intervievați.
    /// Permite reîmprospătarea listei și navigarea înapoi.
    /// </summary>
    public partial class TopNIntervievatiControl : UserControl
    {
        private readonly IntervievatRepository _intervievatRepository;
        private const int DefaultTopN = 10; // Numărul implicit de intervievați de afișat în top.
        // Componentele UI (dgvTopIntervievati, lblTitluClasamentIntervievati etc.) sunt acum definite în fișierul Designer.cs

        /// <summary>
        /// Eveniment declanșat când utilizatorul dorește să navigheze înapoi.
        /// </summary>
        public event EventHandler RequestGoBack;

        /// <summary>
        /// Constructorul controlului TopNIntervievatiControl.
        /// Inițializează componentele, repository-ul, aplică tema și configurează evenimentele.
        /// </summary>
        public TopNIntervievatiControl()
        {
            InitializeComponent(); // Inițializează componentele definite în Designer.cs
            _intervievatRepository = new IntervievatRepository();
            // InitializeCustomComponents(); // Eliminat - componentele sunt acum în Designer.cs

            SetupDataGridView(); // Configurează coloanele DataGridView.
            ThemeHelper.ApplyUserControlTheme(this); 

            this.VisibleChanged += TopNIntervievatiControl_VisibleChanged;
            // Atașează manual evenimentele pentru butoanele definite în Designer
            if (this.btnRefreshClasamentIntervievati != null) 
                this.btnRefreshClasamentIntervievati.Click += (s,e) => RefreshData();
            if (this.btnGoBack != null)
                this.btnGoBack.Click += BtnGoBack_Click;
            
            UpdateTitleLabel(DefaultTopN); // Setează titlul inițial.

            // Menține handler-ul SizeChanged pentru ajustări dinamice de layout dacă este necesar.
            // Designerul ar trebui să gestioneze majoritatea prin Anchors, dar logica specifică poate rămâne aici.
            this.SizeChanged += OnSizeChangedLayoutAdjustments;
        }

        // Metoda InitializeCustomComponents a fost eliminată.
        // Toată inițializarea componentelor vizuale se face acum în TopNIntervievatiControl.Designer.cs

        /// <summary>
        /// Se declanșează la schimbarea vizibilității controlului.
        /// Reîmprospătează datele dacă controlul devine vizibil.
        /// </summary>
        private void TopNIntervievatiControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                RefreshData();
            }
        }

        /// <summary>
        /// Actualizează textul etichetei titlului pentru a reflecta numărul N de intervievați afișați.
        /// </summary>
        /// <param name="n">Numărul de intervievați din top.</param>
        private void UpdateTitleLabel(int n)
        {
            if (lblTitluClasamentIntervievati != null) 
                lblTitluClasamentIntervievati.Text = $"Top {n} Intervievați după Scor";
        }

        /// <summary>
        /// Configurează manual coloanele și proprietățile pentru DataGridView-ul Top N Intervievați.
        /// </summary>
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

        /// <summary>
        /// Gestionează evenimentul de click pentru butonul de revenire.
        /// Declanșează evenimentul RequestGoBack.
        /// </summary>
        private void BtnGoBack_Click(object sender, EventArgs e)
        {
            RequestGoBack?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Reîmprospătează datele afișate în clasamentul Top N Intervievați.
        /// Preia intervievații, îi ordonează, selectează primii N și îi afișează.
        /// </summary>
        /// <param name="n">Numărul de intervievați de afișat în top. Implicit este DefaultTopN.</param>
        public void RefreshData(int n = DefaultTopN)
        {
            if (dgvTopIntervievati == null) return;
            try
            {
                // Asigură-te că scorurile sunt actualizate înainte de a prelua datele.
                _intervievatRepository.CalculeazaSiActualizeazaScorIntervievati();

                var topIntervievati = _intervievatRepository.GetAllIntervievati()
                    .OrderByDescending(i => i.ScorTotalConcurs)
                    .ThenBy(i => i.NumeComplet)
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
                UpdateTitleLabel(n); // Actualizează titlul pentru a reflecta numărul N.
            }
            catch (Exception ex)
            {
                 MessageBox.Show($"Eroare la încărcarea clasamentului intervievaților: {ex.Message}", "Eroare Clasament", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ajustează layout-ul componentelor la redimensionarea controlului.
        /// Această metodă este păstrată pentru ajustări fine care nu sunt complet gestionate de Anchors/Docking.
        /// </summary>
        private void OnSizeChangedLayoutAdjustments(object sender, EventArgs e)
        {
            if (panelHeader == null || btnRefreshClasamentIntervievati == null || btnGoBack == null || dgvTopIntervievati == null) return;

            // Poziționarea butoanelor în panelHeader, relativ la marginea dreaptă a panelului.
            // Padding-ul panelului este luat în considerare.
            btnRefreshClasamentIntervievati.Location = new System.Drawing.Point(panelHeader.Width - btnRefreshClasamentIntervievati.Width - panelHeader.Padding.Right, btnRefreshClasamentIntervievati.Top);
            btnGoBack.Location = new System.Drawing.Point(btnRefreshClasamentIntervievati.Left - btnGoBack.Width - 10, btnGoBack.Top); // 10px spațiu între butoane

            // Ajustarea DataGridView pentru a umple spațiul rămas sub panelHeader.
            // dgvTopIntervievati.Location = new System.Drawing.Point(10, panelHeader.Height + 10); // Designer-ul ar trebui să seteze asta corect cu Anchors
            // dgvTopIntervievati.Size = new System.Drawing.Size(this.Width - 20, this.Height - (panelHeader.Height + 20)); // Gestionat de Anchors
        }
    }
} 