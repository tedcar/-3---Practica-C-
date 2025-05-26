namespace MelodiiApp.UserInterface.Controls
{
    partial class TopNMelodiiControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnGoBack;

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
            this.lblTitluClasament = new System.Windows.Forms.Label();
            this.dgvTopMelodii = new System.Windows.Forms.DataGridView();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnRefreshClasament = new System.Windows.Forms.Button();
            this.btnGoBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopMelodii)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.btnGoBack);
            this.panelHeader.Controls.Add(this.lblTitluClasament);
            this.panelHeader.Controls.Add(this.btnRefreshClasament);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(10);
            this.panelHeader.Size = new System.Drawing.Size(700, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitluClasament
            // 
            this.lblTitluClasament.AutoSize = true;
            this.lblTitluClasament.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblTitluClasament.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this.lblTitluClasament.Location = new System.Drawing.Point(13, 14);
            this.lblTitluClasament.Name = "lblTitluClasament";
            this.lblTitluClasament.Size = new System.Drawing.Size(212, 32);
            this.lblTitluClasament.TabIndex = 0;
            this.lblTitluClasament.Text = "Clasament Melodii";
            // 
            // btnRefreshClasament
            // 
            this.btnRefreshClasament.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshClasament.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnRefreshClasament.FlatAppearance.BorderSize = 0;
            this.btnRefreshClasament.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshClasament.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefreshClasament.ForeColor = System.Drawing.Color.White;
            this.btnRefreshClasament.Location = new System.Drawing.Point(540, 12);
            this.btnRefreshClasament.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefreshClasament.Name = "btnRefreshClasament";
            this.btnRefreshClasament.Size = new System.Drawing.Size(150, 35);
            this.btnRefreshClasament.TabIndex = 1;
            this.btnRefreshClasament.Text = "Reîmprospătează";
            this.btnRefreshClasament.UseVisualStyleBackColor = false;
            // 
            // btnGoBack
            // 
            this.btnGoBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnGoBack.FlatAppearance.BorderSize = 0;
            this.btnGoBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoBack.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnGoBack.ForeColor = System.Drawing.Color.White;
            this.btnGoBack.Location = new System.Drawing.Point(380, 12);
            this.btnGoBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(150, 35);
            this.btnGoBack.TabIndex = 2;
            this.btnGoBack.Text = "Înapoi";
            this.btnGoBack.UseVisualStyleBackColor = false;
            // 
            // dgvTopMelodii
            // 
            this.dgvTopMelodii.AllowUserToAddRows = false;
            this.dgvTopMelodii.AllowUserToDeleteRows = false;
            this.dgvTopMelodii.AllowUserToResizeRows = false;
            this.dgvTopMelodii.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTopMelodii.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopMelodii.BackgroundColor = System.Drawing.Color.White;
            this.dgvTopMelodii.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTopMelodii.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTopMelodii.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTopMelodii.ColumnHeadersHeight = 40;
            this.dgvTopMelodii.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(225)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTopMelodii.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTopMelodii.EnableHeadersVisualStyles = false;
            this.dgvTopMelodii.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvTopMelodii.Location = new System.Drawing.Point(10, 70);
            this.dgvTopMelodii.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvTopMelodii.MultiSelect = false;
            this.dgvTopMelodii.Name = "dgvTopMelodii";
            this.dgvTopMelodii.ReadOnly = true;
            this.dgvTopMelodii.RowHeadersVisible = false;
            this.dgvTopMelodii.RowHeadersWidth = 51;
            this.dgvTopMelodii.RowTemplate.Height = 35;
            this.dgvTopMelodii.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopMelodii.Size = new System.Drawing.Size(680, 370);
            this.dgvTopMelodii.TabIndex = 1;
            // 
            // TopNMelodiiControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.Controls.Add(this.dgvTopMelodii);
            this.Controls.Add(this.panelHeader);
            this.Name = "TopNMelodiiControl";
            this.Size = new System.Drawing.Size(700, 450);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopMelodii)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Label lblTitluClasament;
        private System.Windows.Forms.DataGridView dgvTopMelodii;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button btnRefreshClasament;
    }
} 