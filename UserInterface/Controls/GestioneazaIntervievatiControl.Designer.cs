namespace MelodiiApp.UserInterface.Controls
{
    partial class GestioneazaIntervievatiControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvIntervievati = new System.Windows.Forms.DataGridView();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnRefreshIntervievati = new System.Windows.Forms.Button();
            this.btnStergeIntervievat = new System.Windows.Forms.Button();
            this.btnModificaIntervievat = new System.Windows.Forms.Button();
            this.btnAdaugaIntervievat = new System.Windows.Forms.Button();
            this.lblStatusGestIntervievati = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIntervievati)).BeginInit();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvIntervievati
            // 
            this.dgvIntervievati.AllowUserToAddRows = false;
            this.dgvIntervievati.AllowUserToDeleteRows = false;
            this.dgvIntervievati.AllowUserToResizeRows = false;
            this.dgvIntervievati.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvIntervievati.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvIntervievati.BackgroundColor = System.Drawing.Color.White;
            this.dgvIntervievati.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvIntervievati.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIntervievati.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvIntervievati.ColumnHeadersHeight = 40;
            this.dgvIntervievati.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(225)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvIntervievati.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvIntervievati.EnableHeadersVisualStyles = false;
            this.dgvIntervievati.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvIntervievati.Location = new System.Drawing.Point(15, 65);
            this.dgvIntervievati.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvIntervievati.MultiSelect = false;
            this.dgvIntervievati.Name = "dgvIntervievati";
            this.dgvIntervievati.ReadOnly = true;
            this.dgvIntervievati.RowHeadersVisible = false;
            this.dgvIntervievati.RowHeadersWidth = 51;
            this.dgvIntervievati.RowTemplate.Height = 35;
            this.dgvIntervievati.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIntervievati.Size = new System.Drawing.Size(770, 350);
            this.dgvIntervievati.TabIndex = 0;
            // 
            // panelActions
            // 
            this.panelActions.Controls.Add(this.btnRefreshIntervievati);
            this.panelActions.Controls.Add(this.btnStergeIntervievat);
            this.panelActions.Controls.Add(this.btnModificaIntervievat);
            this.panelActions.Controls.Add(this.btnAdaugaIntervievat);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(0, 0);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new System.Windows.Forms.Padding(10);
            this.panelActions.Size = new System.Drawing.Size(800, 60);
            this.panelActions.TabIndex = 1;
            // 
            // btnRefreshIntervievati
            // 
            this.btnRefreshIntervievati.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnRefreshIntervievati.FlatAppearance.BorderSize = 0;
            this.btnRefreshIntervievati.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshIntervievati.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefreshIntervievati.ForeColor = System.Drawing.Color.White;
            this.btnRefreshIntervievati.Location = new System.Drawing.Point(430, 12);
            this.btnRefreshIntervievati.Name = "btnRefreshIntervievati";
            this.btnRefreshIntervievati.Size = new System.Drawing.Size(120, 35);
            this.btnRefreshIntervievati.TabIndex = 3;
            this.btnRefreshIntervievati.Text = "Reîmprospătează";
            this.btnRefreshIntervievati.UseVisualStyleBackColor = false;
            // 
            // btnStergeIntervievat
            // 
            this.btnStergeIntervievat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnStergeIntervievat.FlatAppearance.BorderSize = 0;
            this.btnStergeIntervievat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStergeIntervievat.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnStergeIntervievat.ForeColor = System.Drawing.Color.White;
            this.btnStergeIntervievat.Location = new System.Drawing.Point(295, 12);
            this.btnStergeIntervievat.Name = "btnStergeIntervievat";
            this.btnStergeIntervievat.Size = new System.Drawing.Size(120, 35);
            this.btnStergeIntervievat.TabIndex = 2;
            this.btnStergeIntervievat.Text = "Șterge Selectat";
            this.btnStergeIntervievat.UseVisualStyleBackColor = false;
            // 
            // btnModificaIntervievat
            // 
            this.btnModificaIntervievat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(173)))), ((int)(((byte)(78)))));
            this.btnModificaIntervievat.FlatAppearance.BorderSize = 0;
            this.btnModificaIntervievat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificaIntervievat.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnModificaIntervievat.ForeColor = System.Drawing.Color.White;
            this.btnModificaIntervievat.Location = new System.Drawing.Point(160, 12);
            this.btnModificaIntervievat.Name = "btnModificaIntervievat";
            this.btnModificaIntervievat.Size = new System.Drawing.Size(120, 35);
            this.btnModificaIntervievat.TabIndex = 1;
            this.btnModificaIntervievat.Text = "Modifică Selectat";
            this.btnModificaIntervievat.UseVisualStyleBackColor = false;
            // 
            // btnAdaugaIntervievat
            // 
            this.btnAdaugaIntervievat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(184)))), ((int)(((byte)(92)))));
            this.btnAdaugaIntervievat.FlatAppearance.BorderSize = 0;
            this.btnAdaugaIntervievat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdaugaIntervievat.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdaugaIntervievat.ForeColor = System.Drawing.Color.White;
            this.btnAdaugaIntervievat.Location = new System.Drawing.Point(25, 12);
            this.btnAdaugaIntervievat.Name = "btnAdaugaIntervievat";
            this.btnAdaugaIntervievat.Size = new System.Drawing.Size(120, 35);
            this.btnAdaugaIntervievat.TabIndex = 0;
            this.btnAdaugaIntervievat.Text = "Adaugă Nou";
            this.btnAdaugaIntervievat.UseVisualStyleBackColor = false;
            // 
            // lblStatusGestIntervievati
            // 
            this.lblStatusGestIntervievati.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusGestIntervievati.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusGestIntervievati.Location = new System.Drawing.Point(15, 425);
            this.lblStatusGestIntervievati.Name = "lblStatusGestIntervievati";
            this.lblStatusGestIntervievati.Size = new System.Drawing.Size(770, 20);
            this.lblStatusGestIntervievati.TabIndex = 2;
            this.lblStatusGestIntervievati.Text = "Status...";
            this.lblStatusGestIntervievati.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatusGestIntervievati.Visible = false;
            // 
            // GestioneazaIntervievatiControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.lblStatusGestIntervievati);
            this.Controls.Add(this.dgvIntervievati);
            this.Controls.Add(this.panelActions);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "GestioneazaIntervievatiControl";
            this.Size = new System.Drawing.Size(800, 450);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIntervievati)).EndInit();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.DataGridView dgvIntervievati;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnAdaugaIntervievat;
        private System.Windows.Forms.Button btnModificaIntervievat;
        private System.Windows.Forms.Button btnStergeIntervievat;
        private System.Windows.Forms.Button btnRefreshIntervievati;
        private System.Windows.Forms.Label lblStatusGestIntervievati;
    }
} 