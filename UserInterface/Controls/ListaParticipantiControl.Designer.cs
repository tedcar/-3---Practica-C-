namespace MelodiiApp.UserInterface.Controls
{
    partial class ListaParticipantiControl
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
            this.lblTitluLista = new System.Windows.Forms.Label();
            this.btnRefreshLista = new System.Windows.Forms.Button();
            this.btnExportPlaceholder = new System.Windows.Forms.Button();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.dgvParticipanti = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParticipanti)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitluLista
            // 
            this.lblTitluLista.AutoSize = true;
            this.lblTitluLista.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitluLista.Location = new System.Drawing.Point(10, 10);
            this.lblTitluLista.Name = "lblTitluLista";
            this.lblTitluLista.Text = "Listă Completă Participanți (Intervievați)";
            // 
            // btnRefreshLista
            // 
            this.btnRefreshLista.Location = new System.Drawing.Point(10, 40);
            this.btnRefreshLista.Name = "btnRefreshLista";
            this.btnRefreshLista.Size = new System.Drawing.Size(120, 30);
            this.btnRefreshLista.Text = "Actualizează";
            this.btnRefreshLista.UseVisualStyleBackColor = true;
            // 
            // btnExportPlaceholder
            // 
            this.btnExportPlaceholder.Location = new System.Drawing.Point(140, 40); // Adjusted: btnRefreshLista.Right + 10
            this.btnExportPlaceholder.Name = "btnExportPlaceholder";
            this.btnExportPlaceholder.Size = new System.Drawing.Size(120, 30);
            this.btnExportPlaceholder.Text = "Exportă Listă";
            this.btnExportPlaceholder.UseVisualStyleBackColor = true;
            // 
            // btnGoBack
            // 
            this.btnGoBack.Location = new System.Drawing.Point(270, 40); // Adjusted: btnExportPlaceholder.Right + 10
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(120, 30);
            this.btnGoBack.Text = "Înapoi";
            this.btnGoBack.UseVisualStyleBackColor = true;
            // 
            // dgvParticipanti
            // 
            this.dgvParticipanti.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvParticipanti.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParticipanti.Location = new System.Drawing.Point(10, 80);
            this.dgvParticipanti.Name = "dgvParticipanti";
            this.dgvParticipanti.Size = new System.Drawing.Size(580, 310); // Default size, will be adjusted by anchors
            this.dgvParticipanti.TabIndex = 1;
            // 
            // ListaParticipantiControl
            // 
            this.Controls.Add(this.lblTitluLista);
            this.Controls.Add(this.btnRefreshLista);
            this.Controls.Add(this.btnExportPlaceholder);
            this.Controls.Add(this.btnGoBack);
            this.Controls.Add(this.dgvParticipanti);
            this.Name = "ListaParticipantiControl";
            this.Size = new System.Drawing.Size(600, 400); // Default size
            ((System.ComponentModel.ISupportInitialize)(this.dgvParticipanti)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout(); // Added to ensure AutoSize for label works correctly if other properties depend on it.

        }

        #endregion

        private System.Windows.Forms.Label lblTitluLista;
        private System.Windows.Forms.Button btnRefreshLista;
        private System.Windows.Forms.Button btnExportPlaceholder;
        private System.Windows.Forms.Button btnGoBack;
        private System.Windows.Forms.DataGridView dgvParticipanti;
    }
} 