namespace MelodiiApp.UserInterface.Controls
{
    partial class VoteazaControl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitluPanou = new System.Windows.Forms.Label();
            this.lblSelecteazaIntervievat = new System.Windows.Forms.Label();
            this.cmbIntervievati = new System.Windows.Forms.ComboBox();
            this.lblPuncteDisponibileInfo = new System.Windows.Forms.Label();
            this.lblPuncteDisponibile = new System.Windows.Forms.Label();
            this.lblCautaMelodie = new System.Windows.Forms.Label();
            this.txtCautaMelodie = new System.Windows.Forms.TextBox();
            this.btnCautaMelodie = new System.Windows.Forms.Button();
            this.dgvMelodiiPentruVot = new System.Windows.Forms.DataGridView();
            this.colMelodieId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitlu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colArtist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPuncteAlocate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdaugaPunct = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnFinalizeazaAlocarea = new System.Windows.Forms.Button();
            this.btnAnuleaza = new System.Windows.Forms.Button();
            this.lblStatusVoteaza = new System.Windows.Forms.Label();
            this.pnlConfirmareVot = new System.Windows.Forms.Panel();
            this.lblConfirmareIntervievatInfo = new System.Windows.Forms.Label();
            this.lblConfirmareIntervievat = new System.Windows.Forms.Label();
            this.lblRezumatVoturiInfo = new System.Windows.Forms.Label();
            this.txtRezumatVoturi = new System.Windows.Forms.TextBox();
            this.lblMesajConfirmare = new System.Windows.Forms.Label();
            this.btnConfirmaVotFinal = new System.Windows.Forms.Button();
            this.btnRevinoLaAlocare = new System.Windows.Forms.Button();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMelodiiPentruVot)).BeginInit();
            this.pnlConfirmareVot.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitluPanou
            // 
            this.lblTitluPanou.AutoSize = true;
            this.lblTitluPanou.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblTitluPanou.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this.lblTitluPanou.Location = new System.Drawing.Point(11, 12);
            this.lblTitluPanou.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitluPanou.Name = "lblTitluPanou";
            this.lblTitluPanou.Size = new System.Drawing.Size(175, 25);
            this.lblTitluPanou.TabIndex = 0;
            this.lblTitluPanou.Text = "Alocare Puncte Vot";
            // 
            // lblSelecteazaIntervievat
            // 
            this.lblSelecteazaIntervievat.AutoSize = true;
            this.lblSelecteazaIntervievat.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSelecteazaIntervievat.Location = new System.Drawing.Point(13, 49);
            this.lblSelecteazaIntervievat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSelecteazaIntervievat.Name = "lblSelecteazaIntervievat";
            this.lblSelecteazaIntervievat.Size = new System.Drawing.Size(135, 17);
            this.lblSelecteazaIntervievat.TabIndex = 1;
            this.lblSelecteazaIntervievat.Text = "Selectează Intervievat:";
            // 
            // cmbIntervievati
            // 
            this.cmbIntervievati.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIntervievati.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbIntervievati.FormattingEnabled = true;
            this.cmbIntervievati.Location = new System.Drawing.Point(155, 46);
            this.cmbIntervievati.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cmbIntervievati.Name = "cmbIntervievati";
            this.cmbIntervievati.Size = new System.Drawing.Size(226, 25);
            this.cmbIntervievati.TabIndex = 2;
            // 
            // lblPuncteDisponibileInfo
            // 
            this.lblPuncteDisponibileInfo.AutoSize = true;
            this.lblPuncteDisponibileInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblPuncteDisponibileInfo.Location = new System.Drawing.Point(387, 49);
            this.lblPuncteDisponibileInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPuncteDisponibileInfo.Name = "lblPuncteDisponibileInfo";
            this.lblPuncteDisponibileInfo.Size = new System.Drawing.Size(116, 19);
            this.lblPuncteDisponibileInfo.TabIndex = 3;
            this.lblPuncteDisponibileInfo.Text = "Puncte de alocat:";
            // 
            // lblPuncteDisponibile
            // 
            this.lblPuncteDisponibile.AutoSize = true;
            this.lblPuncteDisponibile.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblPuncteDisponibile.ForeColor = System.Drawing.Color.Green;
            this.lblPuncteDisponibile.Location = new System.Drawing.Point(500, 49);
            this.lblPuncteDisponibile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPuncteDisponibile.Name = "lblPuncteDisponibile";
            this.lblPuncteDisponibile.Size = new System.Drawing.Size(31, 19);
            this.lblPuncteDisponibile.TabIndex = 4;
            this.lblPuncteDisponibile.Text = "6/6";
            // 
            // lblCautaMelodie
            // 
            this.lblCautaMelodie.AutoSize = true;
            this.lblCautaMelodie.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblCautaMelodie.Location = new System.Drawing.Point(13, 81);
            this.lblCautaMelodie.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCautaMelodie.Name = "lblCautaMelodie";
            this.lblCautaMelodie.Size = new System.Drawing.Size(96, 17);
            this.lblCautaMelodie.TabIndex = 5;
            this.lblCautaMelodie.Text = "Caută Melodie:";
            // 
            // txtCautaMelodie
            // 
            this.txtCautaMelodie.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtCautaMelodie.Location = new System.Drawing.Point(109, 79);
            this.txtCautaMelodie.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCautaMelodie.Name = "txtCautaMelodie";
            this.txtCautaMelodie.Size = new System.Drawing.Size(324, 25);
            this.txtCautaMelodie.TabIndex = 6;
            this.toolTipInfo.SetToolTip(this.txtCautaMelodie, "Introduceți titlul sau artistul melodiei și apăsați Enter sau Caută");
            // 
            // btnCautaMelodie
            // 
            this.btnCautaMelodie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnCautaMelodie.FlatAppearance.BorderSize = 0;
            this.btnCautaMelodie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCautaMelodie.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCautaMelodie.ForeColor = System.Drawing.Color.White;
            this.btnCautaMelodie.Location = new System.Drawing.Point(442, 77);
            this.btnCautaMelodie.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCautaMelodie.Name = "btnCautaMelodie";
            this.btnCautaMelodie.Size = new System.Drawing.Size(75, 27);
            this.btnCautaMelodie.TabIndex = 7;
            this.btnCautaMelodie.Text = "Caută";
            this.btnCautaMelodie.UseVisualStyleBackColor = false;
            // 
            // dgvMelodiiPentruVot
            // 
            this.dgvMelodiiPentruVot.AllowUserToAddRows = false;
            this.dgvMelodiiPentruVot.AllowUserToDeleteRows = false;
            this.dgvMelodiiPentruVot.AllowUserToResizeRows = false;
            this.dgvMelodiiPentruVot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMelodiiPentruVot.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMelodieId,
            this.colTitlu,
            this.colArtist,
            this.colPuncteAlocate,
            this.colAdaugaPunct});
            this.dgvMelodiiPentruVot.Location = new System.Drawing.Point(15, 114);
            this.dgvMelodiiPentruVot.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvMelodiiPentruVot.MultiSelect = false;
            this.dgvMelodiiPentruVot.Name = "dgvMelodiiPentruVot";
            this.dgvMelodiiPentruVot.RowHeadersVisible = false;
            this.dgvMelodiiPentruVot.RowHeadersWidth = 51;
            this.dgvMelodiiPentruVot.RowTemplate.Height = 28;
            this.dgvMelodiiPentruVot.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvMelodiiPentruVot.Size = new System.Drawing.Size(502, 203);
            this.dgvMelodiiPentruVot.TabIndex = 8;
            // 
            // colMelodieId
            // 
            this.colMelodieId.HeaderText = "ID";
            this.colMelodieId.MinimumWidth = 6;
            this.colMelodieId.Name = "colMelodieId";
            this.colMelodieId.ReadOnly = true;
            this.colMelodieId.Visible = false;
            this.colMelodieId.Width = 50;
            // 
            // colTitlu
            // 
            this.colTitlu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colTitlu.DefaultCellStyle = dataGridViewCellStyle1;
            this.colTitlu.FillWeight = 60F;
            this.colTitlu.HeaderText = "Titlu Melodie";
            this.colTitlu.MinimumWidth = 6;
            this.colTitlu.Name = "colTitlu";
            this.colTitlu.ReadOnly = true;
            // 
            // colArtist
            // 
            this.colArtist.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colArtist.DefaultCellStyle = dataGridViewCellStyle1;
            this.colArtist.FillWeight = 40F;
            this.colArtist.HeaderText = "Artist";
            this.colArtist.MinimumWidth = 6;
            this.colArtist.Name = "colArtist";
            this.colArtist.ReadOnly = true;
            // 
            // colPuncteAlocate
            // 
            this.colPuncteAlocate.DataPropertyName = "Puncte";
            this.colPuncteAlocate.HeaderText = "Puncte (0-6)";
            this.colPuncteAlocate.MinimumWidth = 6;
            this.colPuncteAlocate.Name = "colPuncteAlocate";
            this.colPuncteAlocate.ReadOnly = true;
            this.colPuncteAlocate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colAdaugaPunct
            // 
            this.colAdaugaPunct.HeaderText = "Votează";
            this.colAdaugaPunct.MinimumWidth = 6;
            this.colAdaugaPunct.Name = "colAdaugaPunct";
            this.colAdaugaPunct.Text = "+";
            this.colAdaugaPunct.UseColumnTextForButtonValue = true;
            this.colAdaugaPunct.Width = 70;
            // 
            // btnFinalizeazaAlocarea
            // 
            this.btnFinalizeazaAlocarea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnFinalizeazaAlocarea.FlatAppearance.BorderSize = 0;
            this.btnFinalizeazaAlocarea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizeazaAlocarea.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnFinalizeazaAlocarea.ForeColor = System.Drawing.Color.White;
            this.btnFinalizeazaAlocarea.Location = new System.Drawing.Point(375, 325);
            this.btnFinalizeazaAlocarea.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnFinalizeazaAlocarea.Name = "btnFinalizeazaAlocarea";
            this.btnFinalizeazaAlocarea.Size = new System.Drawing.Size(142, 32);
            this.btnFinalizeazaAlocarea.TabIndex = 9;
            this.btnFinalizeazaAlocarea.Text = "Finalizează Alocarea";
            this.btnFinalizeazaAlocarea.UseVisualStyleBackColor = false;
            // 
            // btnAnuleaza
            // 
            this.btnAnuleaza.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnAnuleaza.FlatAppearance.BorderSize = 0;
            this.btnAnuleaza.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnuleaza.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnAnuleaza.ForeColor = System.Drawing.Color.White;
            this.btnAnuleaza.Location = new System.Drawing.Point(255, 325);
            this.btnAnuleaza.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAnuleaza.Name = "btnAnuleaza";
            this.btnAnuleaza.Size = new System.Drawing.Size(112, 32);
            this.btnAnuleaza.TabIndex = 10;
            this.btnAnuleaza.Text = "Anulează / Închide";
            this.btnAnuleaza.UseVisualStyleBackColor = false;
            // 
            // lblStatusVoteaza
            // 
            this.lblStatusVoteaza.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusVoteaza.Location = new System.Drawing.Point(15, 366);
            this.lblStatusVoteaza.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatusVoteaza.Name = "lblStatusVoteaza";
            this.lblStatusVoteaza.Size = new System.Drawing.Size(502, 19);
            this.lblStatusVoteaza.TabIndex = 11;
            this.lblStatusVoteaza.Text = "Status:";
            this.lblStatusVoteaza.Visible = false;
            // 
            // pnlConfirmareVot
            // 
            this.pnlConfirmareVot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlConfirmareVot.Controls.Add(this.txtRezumatVoturi);
            this.pnlConfirmareVot.Controls.Add(this.lblRezumatVoturiInfo);
            this.pnlConfirmareVot.Controls.Add(this.lblConfirmareIntervievat);
            this.pnlConfirmareVot.Controls.Add(this.lblConfirmareIntervievatInfo);
            this.pnlConfirmareVot.Controls.Add(this.lblMesajConfirmare);
            this.pnlConfirmareVot.Controls.Add(this.btnConfirmaVotFinal);
            this.pnlConfirmareVot.Controls.Add(this.btnRevinoLaAlocare);
            this.pnlConfirmareVot.Location = new System.Drawing.Point(75, 155);
            this.pnlConfirmareVot.Margin = new System.Windows.Forms.Padding(2);
            this.pnlConfirmareVot.Name = "pnlConfirmareVot";
            this.pnlConfirmareVot.Size = new System.Drawing.Size(376, 200);
            this.pnlConfirmareVot.TabIndex = 12;
            this.pnlConfirmareVot.Visible = false;
            // 
            // lblConfirmareIntervievatInfo
            // 
            this.lblConfirmareIntervievatInfo.AutoSize = true;
            this.lblConfirmareIntervievatInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblConfirmareIntervievatInfo.Location = new System.Drawing.Point(10, 35);
            this.lblConfirmareIntervievatInfo.Name = "lblConfirmareIntervievatInfo";
            this.lblConfirmareIntervievatInfo.Size = new System.Drawing.Size(73, 15);
            this.lblConfirmareIntervievatInfo.TabIndex = 3;
            this.lblConfirmareIntervievatInfo.Text = "Intervievat:";
            // 
            // lblConfirmareIntervievat
            // 
            this.lblConfirmareIntervievat.AutoSize = true;
            this.lblConfirmareIntervievat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblConfirmareIntervievat.Location = new System.Drawing.Point(85, 35);
            this.lblConfirmareIntervievat.Name = "lblConfirmareIntervievat";
            this.lblConfirmareIntervievat.Size = new System.Drawing.Size(12, 15);
            this.lblConfirmareIntervievat.TabIndex = 4;
            this.lblConfirmareIntervievat.Text = "-";
            // 
            // lblRezumatVoturiInfo
            // 
            this.lblRezumatVoturiInfo.AutoSize = true;
            this.lblRezumatVoturiInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRezumatVoturiInfo.Location = new System.Drawing.Point(10, 60);
            this.lblRezumatVoturiInfo.Name = "lblRezumatVoturiInfo";
            this.lblRezumatVoturiInfo.Size = new System.Drawing.Size(92, 15);
            this.lblRezumatVoturiInfo.TabIndex = 5;
            this.lblRezumatVoturiInfo.Text = "Rezumat Voturi:";
            // 
            // txtRezumatVoturi
            // 
            this.txtRezumatVoturi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRezumatVoturi.Location = new System.Drawing.Point(13, 80);
            this.txtRezumatVoturi.Multiline = true;
            this.txtRezumatVoturi.Name = "txtRezumatVoturi";
            this.txtRezumatVoturi.ReadOnly = true;
            this.txtRezumatVoturi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRezumatVoturi.Size = new System.Drawing.Size(345, 70);
            this.txtRezumatVoturi.TabIndex = 6;
            // 
            // lblMesajConfirmare
            // 
            this.lblMesajConfirmare.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblMesajConfirmare.Location = new System.Drawing.Point(10, 10);
            this.lblMesajConfirmare.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMesajConfirmare.Name = "lblMesajConfirmare";
            this.lblMesajConfirmare.Size = new System.Drawing.Size(348, 19);
            this.lblMesajConfirmare.TabIndex = 0;
            this.lblMesajConfirmare.Text = "Confirmați voturile alocate?";
            this.lblMesajConfirmare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnConfirmaVotFinal
            // 
            this.btnConfirmaVotFinal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnConfirmaVotFinal.FlatAppearance.BorderSize = 0;
            this.btnConfirmaVotFinal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmaVotFinal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnConfirmaVotFinal.ForeColor = System.Drawing.Color.White;
            this.btnConfirmaVotFinal.Location = new System.Drawing.Point(246, 158);
            this.btnConfirmaVotFinal.Margin = new System.Windows.Forms.Padding(2);
            this.btnConfirmaVotFinal.Name = "btnConfirmaVotFinal";
            this.btnConfirmaVotFinal.Size = new System.Drawing.Size(112, 32);
            this.btnConfirmaVotFinal.TabIndex = 1;
            this.btnConfirmaVotFinal.Text = "Confirmă Final";
            this.btnConfirmaVotFinal.UseVisualStyleBackColor = false;
            // 
            // btnRevinoLaAlocare
            // 
            this.btnRevinoLaAlocare.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnRevinoLaAlocare.FlatAppearance.BorderSize = 0;
            this.btnRevinoLaAlocare.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevinoLaAlocare.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRevinoLaAlocare.ForeColor = System.Drawing.Color.White;
            this.btnRevinoLaAlocare.Location = new System.Drawing.Point(127, 158);
            this.btnRevinoLaAlocare.Margin = new System.Windows.Forms.Padding(2);
            this.btnRevinoLaAlocare.Name = "btnRevinoLaAlocare";
            this.btnRevinoLaAlocare.Size = new System.Drawing.Size(112, 32);
            this.btnRevinoLaAlocare.TabIndex = 2;
            this.btnRevinoLaAlocare.Text = "Modifică Vot";
            this.btnRevinoLaAlocare.UseVisualStyleBackColor = false;
            // 
            // VoteazaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Controls.Add(this.pnlConfirmareVot);
            this.Controls.Add(this.lblStatusVoteaza);
            this.Controls.Add(this.btnAnuleaza);
            this.Controls.Add(this.btnFinalizeazaAlocarea);
            this.Controls.Add(this.dgvMelodiiPentruVot);
            this.Controls.Add(this.btnCautaMelodie);
            this.Controls.Add(this.txtCautaMelodie);
            this.Controls.Add(this.lblCautaMelodie);
            this.Controls.Add(this.lblPuncteDisponibile);
            this.Controls.Add(this.lblPuncteDisponibileInfo);
            this.Controls.Add(this.cmbIntervievati);
            this.Controls.Add(this.lblSelecteazaIntervievat);
            this.Controls.Add(this.lblTitluPanou);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "VoteazaControl";
            this.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.Size = new System.Drawing.Size(719, 523);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMelodiiPentruVot)).EndInit();
            this.pnlConfirmareVot.ResumeLayout(false);
            this.pnlConfirmareVot.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Label lblTitluPanou;
        private System.Windows.Forms.Label lblSelecteazaIntervievat;
        private System.Windows.Forms.ComboBox cmbIntervievati;
        private System.Windows.Forms.Label lblPuncteDisponibileInfo;
        private System.Windows.Forms.Label lblPuncteDisponibile;
        private System.Windows.Forms.Label lblCautaMelodie;
        private System.Windows.Forms.TextBox txtCautaMelodie;
        private System.Windows.Forms.Button btnCautaMelodie;
        private System.Windows.Forms.DataGridView dgvMelodiiPentruVot;
        private System.Windows.Forms.Button btnFinalizeazaAlocarea;
        private System.Windows.Forms.Button btnAnuleaza;
        private System.Windows.Forms.Label lblStatusVoteaza;
        private System.Windows.Forms.Panel pnlConfirmareVot;
        private System.Windows.Forms.Label lblMesajConfirmare;
        private System.Windows.Forms.Button btnConfirmaVotFinal;
        private System.Windows.Forms.Button btnRevinoLaAlocare;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMelodieId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitlu;
        private System.Windows.Forms.DataGridViewTextBoxColumn colArtist;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPuncteAlocate;
        private System.Windows.Forms.DataGridViewButtonColumn colAdaugaPunct;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Label lblConfirmareIntervievatInfo;
        private System.Windows.Forms.Label lblConfirmareIntervievat;
        private System.Windows.Forms.Label lblRezumatVoturiInfo;
        private System.Windows.Forms.TextBox txtRezumatVoturi;
    }
} 