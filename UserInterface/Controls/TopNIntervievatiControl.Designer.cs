namespace MelodiiApp.UserInterface.Controls
{
    partial class TopNIntervievatiControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitluClasamentIntervievati = new System.Windows.Forms.Label();
            this.btnRefreshClasamentIntervievati = new System.Windows.Forms.Button();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.dgvTopIntervievati = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopIntervievati)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblTitluClasamentIntervievati);
            this.panelHeader.Controls.Add(this.btnRefreshClasamentIntervievati);
            this.panelHeader.Controls.Add(this.btnGoBack);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(10);
            this.panelHeader.Size = new System.Drawing.Size(400, 60); // Default width, will be adjusted by Dock
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitluClasamentIntervievati
            // 
            this.lblTitluClasamentIntervievati.AutoSize = true;
            this.lblTitluClasamentIntervievati.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblTitluClasamentIntervievati.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this.lblTitluClasamentIntervievati.Location = new System.Drawing.Point(10, 14);
            this.lblTitluClasamentIntervievati.Name = "lblTitluClasamentIntervievati";
            this.lblTitluClasamentIntervievati.Size = new System.Drawing.Size(175, 25); // Approx size, AutoSize is true
            this.lblTitluClasamentIntervievati.TabIndex = 0;
            this.lblTitluClasamentIntervievati.Text = "Top N Intervievați";
            // 
            // btnRefreshClasamentIntervievati
            // 
            this.btnRefreshClasamentIntervievati.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshClasamentIntervievati.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnRefreshClasamentIntervievati.FlatAppearance.BorderSize = 0;
            this.btnRefreshClasamentIntervievati.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshClasamentIntervievati.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefreshClasamentIntervievati.ForeColor = System.Drawing.Color.White;
            this.btnRefreshClasamentIntervievati.Location = new System.Drawing.Point(226, 12); // Adjusted for panel padding
            this.btnRefreshClasamentIntervievati.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefreshClasamentIntervievati.Name = "btnRefreshClasamentIntervievati";
            this.btnRefreshClasamentIntervievati.Size = new System.Drawing.Size(150, 35);
            this.btnRefreshClasamentIntervievati.TabIndex = 1;
            this.btnRefreshClasamentIntervievati.Text = "Actualizează";
            this.btnRefreshClasamentIntervievati.UseVisualStyleBackColor = false;
            // 
            // btnGoBack
            // 
            this.btnGoBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnGoBack.FlatAppearance.BorderSize = 0;
            this.btnGoBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoBack.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnGoBack.ForeColor = System.Drawing.Color.White;
            this.btnGoBack.Location = new System.Drawing.Point(62, 12); // Adjusted: Relative to Refresh button
            this.btnGoBack.Margin = new System.Windows.Forms.Padding(4);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(150, 35);
            this.btnGoBack.TabIndex = 2;
            this.btnGoBack.Text = "Înapoi";
            this.btnGoBack.UseVisualStyleBackColor = false;
            // 
            // dgvTopIntervievati
            // 
            this.dgvTopIntervievati.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTopIntervievati.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopIntervievati.Location = new System.Drawing.Point(10, 70); // Below panelHeader
            this.dgvTopIntervievati.Name = "dgvTopIntervievati";
            this.dgvTopIntervievati.Size = new System.Drawing.Size(380, 220); // Adjusted for panel and padding
            this.dgvTopIntervievati.TabIndex = 3;
            // 
            // TopNIntervievatiControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvTopIntervievati); // Add DGV after panel to ensure panel is on top if overlapping during resize
            this.Controls.Add(this.panelHeader);
            this.Name = "TopNIntervievatiControl";
            this.Size = new System.Drawing.Size(400, 300);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopIntervievati)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitluClasamentIntervievati;
        private System.Windows.Forms.Button btnRefreshClasamentIntervievati;
        private System.Windows.Forms.Button btnGoBack;
        private System.Windows.Forms.DataGridView dgvTopIntervievati;
    }
} 