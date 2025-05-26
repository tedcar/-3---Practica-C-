namespace MelodiiApp.UserInterface.Controls
{
    partial class GestioneazaMelodiiControl
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
            this.dgvMelodii = new System.Windows.Forms.DataGridView();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnRefreshMelodii = new System.Windows.Forms.Button();
            this.btnStergeMelodie = new System.Windows.Forms.Button();
            this.btnModificaMelodie = new System.Windows.Forms.Button();
            this.btnAdaugaMelodie = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusGestMelodii = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMelodii)).BeginInit();
            this.panelActions.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMelodii
            // 
            this.dgvMelodii.AllowUserToAddRows = false;
            this.dgvMelodii.AllowUserToDeleteRows = false;
            this.dgvMelodii.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMelodii.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMelodii.Location = new System.Drawing.Point(0, 50); // Adjusted for panelActions
            this.dgvMelodii.Margin = new System.Windows.Forms.Padding(4);
            this.dgvMelodii.MultiSelect = false;
            this.dgvMelodii.Name = "dgvMelodii";
            this.dgvMelodii.ReadOnly = true;
            this.dgvMelodii.RowHeadersWidth = 51;
            this.dgvMelodii.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMelodii.Size = new System.Drawing.Size(700, 328); // Base size, will be docked
            this.dgvMelodii.TabIndex = 0;
            // 
            // panelActions
            // 
            this.panelActions.Controls.Add(this.btnRefreshMelodii);
            this.panelActions.Controls.Add(this.btnStergeMelodie);
            this.panelActions.Controls.Add(this.btnModificaMelodie);
            this.panelActions.Controls.Add(this.btnAdaugaMelodie);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(0, 0);
            this.panelActions.Margin = new System.Windows.Forms.Padding(4);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.panelActions.Size = new System.Drawing.Size(700, 50);
            this.panelActions.TabIndex = 1;
            // 
            // btnRefreshMelodii
            // 
            this.btnRefreshMelodii.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshMelodii.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnRefreshMelodii.FlatAppearance.BorderSize = 0;
            this.btnRefreshMelodii.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshMelodii.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefreshMelodii.ForeColor = System.Drawing.Color.White;
            this.btnRefreshMelodii.Location = new System.Drawing.Point(550, 7);
            this.btnRefreshMelodii.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefreshMelodii.Name = "btnRefreshMelodii";
            this.btnRefreshMelodii.Size = new System.Drawing.Size(140, 35);
            this.btnRefreshMelodii.TabIndex = 3;
            this.btnRefreshMelodii.Text = "Reîmprospătează";
            this.btnRefreshMelodii.UseVisualStyleBackColor = false;
            // 
            // btnStergeMelodie
            // 
            this.btnStergeMelodie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnStergeMelodie.FlatAppearance.BorderSize = 0;
            this.btnStergeMelodie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStergeMelodie.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnStergeMelodie.ForeColor = System.Drawing.Color.White;
            this.btnStergeMelodie.Location = new System.Drawing.Point(320, 7);
            this.btnStergeMelodie.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.btnStergeMelodie.Name = "btnStergeMelodie";
            this.btnStergeMelodie.Size = new System.Drawing.Size(140, 35);
            this.btnStergeMelodie.TabIndex = 2;
            this.btnStergeMelodie.Text = "Șterge Selectată";
            this.btnStergeMelodie.UseVisualStyleBackColor = false;
            // 
            // btnModificaMelodie
            // 
            this.btnModificaMelodie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnModificaMelodie.FlatAppearance.BorderSize = 0;
            this.btnModificaMelodie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificaMelodie.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnModificaMelodie.ForeColor = System.Drawing.Color.White;
            this.btnModificaMelodie.Location = new System.Drawing.Point(164, 7);
            this.btnModificaMelodie.Margin = new System.Windows.Forms.Padding(4, 4, 8, 4);
            this.btnModificaMelodie.Name = "btnModificaMelodie";
            this.btnModificaMelodie.Size = new System.Drawing.Size(140, 35);
            this.btnModificaMelodie.TabIndex = 1;
            this.btnModificaMelodie.Text = "Modifică Selectată";
            this.btnModificaMelodie.UseVisualStyleBackColor = false;
            // 
            // btnAdaugaMelodie
            // 
            this.btnAdaugaMelodie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnAdaugaMelodie.FlatAppearance.BorderSize = 0;
            this.btnAdaugaMelodie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdaugaMelodie.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAdaugaMelodie.ForeColor = System.Drawing.Color.White;
            this.btnAdaugaMelodie.Location = new System.Drawing.Point(8, 7);
            this.btnAdaugaMelodie.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdaugaMelodie.Name = "btnAdaugaMelodie";
            this.btnAdaugaMelodie.Size = new System.Drawing.Size(140, 35);
            this.btnAdaugaMelodie.TabIndex = 0;
            this.btnAdaugaMelodie.Text = "Adaugă Nouă";
            this.btnAdaugaMelodie.UseVisualStyleBackColor = false;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusGestMelodii});
            this.statusStrip.Location = new System.Drawing.Point(0, 378);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(700, 22); // Height 22 for default status strip
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 2;
            // 
            // lblStatusGestMelodii
            // 
            this.lblStatusGestMelodii.Name = "lblStatusGestMelodii";
            this.lblStatusGestMelodii.Size = new System.Drawing.Size(0, 16);
            this.lblStatusGestMelodii.Spring = true;
            this.lblStatusGestMelodii.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GestioneazaMelodiiControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240))))); // Light Gray background
            this.Controls.Add(this.dgvMelodii);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.statusStrip);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GestioneazaMelodiiControl";
            this.Size = new System.Drawing.Size(700, 400);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMelodii)).EndInit();
            this.panelActions.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMelodii;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnAdaugaMelodie;
        private System.Windows.Forms.Button btnModificaMelodie;
        private System.Windows.Forms.Button btnStergeMelodie;
        private System.Windows.Forms.Button btnRefreshMelodii;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusGestMelodii;
    }
} 