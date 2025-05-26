namespace MelodiiApp.UserInterface.Forms
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabMelodii = new System.Windows.Forms.TabPage();
            this.tabIntervievati = new System.Windows.Forms.TabPage();
            this.tabVoteazaMelodii = new System.Windows.Forms.TabPage();
            this.tabManagementPredictii = new System.Windows.Forms.TabPage();
            this.tabRapoarte = new System.Windows.Forms.TabPage();
            this.tabClasamenteGenerale = new System.Windows.Forms.TabPage();

            // Button declarations - ensure they are declared only once and are the same instances used throughout
            this.btnAdaugaMelodie = new System.Windows.Forms.Button();
            this.btnGestioneazaMelodii = new System.Windows.Forms.Button();
            this.btnAdaugaIntervievat = new System.Windows.Forms.Button();
            this.btnGestioneazaIntervievati = new System.Windows.Forms.Button();
            this.btnInregistreazaVoturi = new System.Windows.Forms.Button();
            this.btnInregistreazaPredictii = new System.Windows.Forms.Button();
            this.btnListaParticipanti = new System.Windows.Forms.Button();
            this.btnExportParticipantiSub18 = new System.Windows.Forms.Button();
            
            this.mainTabControl.SuspendLayout();
            this.tabMelodii.SuspendLayout();
            this.tabIntervievati.SuspendLayout();
            this.tabVoteazaMelodii.SuspendLayout();
            this.tabManagementPredictii.SuspendLayout();
            this.tabRapoarte.SuspendLayout();
            this.tabClasamenteGenerale.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.tabMelodii);
            this.mainTabControl.Controls.Add(this.tabIntervievati);
            this.mainTabControl.Controls.Add(this.tabVoteazaMelodii);
            this.mainTabControl.Controls.Add(this.tabManagementPredictii);
            this.mainTabControl.Controls.Add(this.tabRapoarte);
            this.mainTabControl.Controls.Add(this.tabClasamenteGenerale);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabControl.Location = new System.Drawing.Point(0, 64);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(800, 536);
            this.mainTabControl.TabIndex = 0;
            this.mainTabControl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            // 
            // tabMelodii
            // 
            this.tabMelodii.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(36))))); 
            this.tabMelodii.Controls.Add(this.btnGestioneazaMelodii);
            this.tabMelodii.Controls.Add(this.btnAdaugaMelodie);
            this.tabMelodii.Location = new System.Drawing.Point(4, 29);
            this.tabMelodii.Name = "tabMelodii";
            this.tabMelodii.Padding = new System.Windows.Forms.Padding(15);
            this.tabMelodii.Size = new System.Drawing.Size(792, 503);
            this.tabMelodii.TabIndex = 0;
            this.tabMelodii.Text = "Melodii (Admin)";
            // 
            // btnAdaugaMelodie
            // 
            this.btnAdaugaMelodie.BackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc");
            this.btnAdaugaMelodie.FlatAppearance.BorderSize = 0;
            this.btnAdaugaMelodie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdaugaMelodie.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdaugaMelodie.ForeColor = System.Drawing.Color.White;
            this.btnAdaugaMelodie.Location = new System.Drawing.Point(18, 18);
            this.btnAdaugaMelodie.MinimumSize = new System.Drawing.Size(180, 40);
            this.btnAdaugaMelodie.Name = "btnAdaugaMelodie";
            this.btnAdaugaMelodie.Size = new System.Drawing.Size(210, 40);
            this.btnAdaugaMelodie.TabIndex = 1;
            this.btnAdaugaMelodie.Text = "Adaugă Melodie Nouă";
            this.btnAdaugaMelodie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAdaugaMelodie.UseVisualStyleBackColor = false;
            // 
            // btnGestioneazaMelodii
            // 
            this.btnGestioneazaMelodii.BackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc");
            this.btnGestioneazaMelodii.FlatAppearance.BorderSize = 0;
            this.btnGestioneazaMelodii.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestioneazaMelodii.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnGestioneazaMelodii.ForeColor = System.Drawing.Color.White;
            this.btnGestioneazaMelodii.Location = new System.Drawing.Point(18, 68);
            this.btnGestioneazaMelodii.MinimumSize = new System.Drawing.Size(180, 40);
            this.btnGestioneazaMelodii.Name = "btnGestioneazaMelodii";
            this.btnGestioneazaMelodii.Size = new System.Drawing.Size(210, 40);
            this.btnGestioneazaMelodii.TabIndex = 2;
            this.btnGestioneazaMelodii.Text = "Gestionează Melodii";
            this.btnGestioneazaMelodii.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnGestioneazaMelodii.UseVisualStyleBackColor = false;
            // 
            // tabIntervievati
            // 
            this.tabIntervievati.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(36)))));
            this.tabIntervievati.Controls.Add(this.btnGestioneazaIntervievati);
            this.tabIntervievati.Controls.Add(this.btnAdaugaIntervievat);
            this.tabIntervievati.Location = new System.Drawing.Point(4, 29);
            this.tabIntervievati.Name = "tabIntervievati";
            this.tabIntervievati.Padding = new System.Windows.Forms.Padding(15);
            this.tabIntervievati.Size = new System.Drawing.Size(792, 503);
            this.tabIntervievati.TabIndex = 1;
            this.tabIntervievati.Text = "Intervievați (Admin)";
            // 
            // btnAdaugaIntervievat
            // 
            this.btnAdaugaIntervievat.BackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc");
            this.btnAdaugaIntervievat.FlatAppearance.BorderSize = 0;
            this.btnAdaugaIntervievat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdaugaIntervievat.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdaugaIntervievat.ForeColor = System.Drawing.Color.White;
            this.btnAdaugaIntervievat.Location = new System.Drawing.Point(18, 18);
            this.btnAdaugaIntervievat.MinimumSize = new System.Drawing.Size(180, 40);
            this.btnAdaugaIntervievat.Name = "btnAdaugaIntervievat";
            this.btnAdaugaIntervievat.Size = new System.Drawing.Size(210, 40);
            this.btnAdaugaIntervievat.TabIndex = 4;
            this.btnAdaugaIntervievat.Text = "Adaugă Intervievat Nou";
            this.btnAdaugaIntervievat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAdaugaIntervievat.UseVisualStyleBackColor = false;
            // 
            // btnGestioneazaIntervievati
            // 
            this.btnGestioneazaIntervievati.BackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc");
            this.btnGestioneazaIntervievati.FlatAppearance.BorderSize = 0;
            this.btnGestioneazaIntervievati.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestioneazaIntervievati.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnGestioneazaIntervievati.ForeColor = System.Drawing.Color.White;
            this.btnGestioneazaIntervievati.Location = new System.Drawing.Point(18, 68);
            this.btnGestioneazaIntervievati.MinimumSize = new System.Drawing.Size(180, 40);
            this.btnGestioneazaIntervievati.Name = "btnGestioneazaIntervievati";
            this.btnGestioneazaIntervievati.Size = new System.Drawing.Size(210, 40);
            this.btnGestioneazaIntervievati.TabIndex = 5;
            this.btnGestioneazaIntervievati.Text = "Gestionează Intervievați";
            this.btnGestioneazaIntervievati.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnGestioneazaIntervievati.UseVisualStyleBackColor = false;
            // 
            // tabVoteazaMelodii
            // 
            this.tabVoteazaMelodii.BackColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5");
            this.tabVoteazaMelodii.Controls.Add(this.btnInregistreazaVoturi);
            this.tabVoteazaMelodii.Location = new System.Drawing.Point(4, 29);
            this.tabVoteazaMelodii.Name = "tabVoteazaMelodii";
            this.tabVoteazaMelodii.Padding = new System.Windows.Forms.Padding(15);
            this.tabVoteazaMelodii.Size = new System.Drawing.Size(792, 503);
            this.tabVoteazaMelodii.TabIndex = 2;
            this.tabVoteazaMelodii.Text = "Votează Melodii";
            // 
            // btnInregistreazaVoturi
            // 
            this.btnInregistreazaVoturi.BackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc");
            this.btnInregistreazaVoturi.FlatAppearance.BorderSize = 0;
            this.btnInregistreazaVoturi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInregistreazaVoturi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnInregistreazaVoturi.ForeColor = System.Drawing.Color.White;
            this.btnInregistreazaVoturi.Location = new System.Drawing.Point(18, 18);
            this.btnInregistreazaVoturi.MinimumSize = new System.Drawing.Size(180, 40);
            this.btnInregistreazaVoturi.Name = "btnInregistreazaVoturi";
            this.btnInregistreazaVoturi.Size = new System.Drawing.Size(230, 40);
            this.btnInregistreazaVoturi.TabIndex = 7;
            this.btnInregistreazaVoturi.Text = "Votează Melodii (6 puncte)";
            this.btnInregistreazaVoturi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnInregistreazaVoturi.UseVisualStyleBackColor = false;
            // 
            // tabManagementPredictii
            // 
            this.tabManagementPredictii.BackColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5");
            this.tabManagementPredictii.Controls.Add(this.btnInregistreazaPredictii);
            this.tabManagementPredictii.Location = new System.Drawing.Point(4, 29);
            this.tabManagementPredictii.Name = "tabManagementPredictii";
            this.tabManagementPredictii.Padding = new System.Windows.Forms.Padding(15);
            this.tabManagementPredictii.Size = new System.Drawing.Size(792, 503);
            this.tabManagementPredictii.TabIndex = 3;
            this.tabManagementPredictii.Text = "Management Predicții (Admin)";
            // 
            // btnInregistreazaPredictii
            // 
            this.btnInregistreazaPredictii.BackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc");
            this.btnInregistreazaPredictii.FlatAppearance.BorderSize = 0;
            this.btnInregistreazaPredictii.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInregistreazaPredictii.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnInregistreazaPredictii.ForeColor = System.Drawing.Color.White;
            this.btnInregistreazaPredictii.Location = new System.Drawing.Point(18, 68);
            this.btnInregistreazaPredictii.MinimumSize = new System.Drawing.Size(180, 40);
            this.btnInregistreazaPredictii.Name = "btnInregistreazaPredictii";
            this.btnInregistreazaPredictii.Size = new System.Drawing.Size(230, 40);
            this.btnInregistreazaPredictii.TabIndex = 8;
            this.btnInregistreazaPredictii.Text = "Înregistrează Predicții Top 3";
            this.btnInregistreazaPredictii.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnInregistreazaPredictii.UseVisualStyleBackColor = false;
            // 
            // tabRapoarte
            // 
            this.tabRapoarte.BackColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5");
            this.tabRapoarte.Controls.Add(this.btnExportParticipantiSub18);
            this.tabRapoarte.Controls.Add(this.btnListaParticipanti);
            this.tabRapoarte.Location = new System.Drawing.Point(4, 29);
            this.tabRapoarte.Name = "tabRapoarte";
            this.tabRapoarte.Padding = new System.Windows.Forms.Padding(15);
            this.tabRapoarte.Size = new System.Drawing.Size(792, 503);
            this.tabRapoarte.TabIndex = 5;
            this.tabRapoarte.Text = "Rapoarte";
            // 
            // btnListaParticipanti
            // 
            this.btnListaParticipanti.BackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc");
            this.btnListaParticipanti.FlatAppearance.BorderSize = 0;
            this.btnListaParticipanti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnListaParticipanti.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnListaParticipanti.ForeColor = System.Drawing.Color.White;
            this.btnListaParticipanti.Location = new System.Drawing.Point(18, 18);
            this.btnListaParticipanti.MinimumSize = new System.Drawing.Size(180, 40);
            this.btnListaParticipanti.Name = "btnListaParticipanti";
            this.btnListaParticipanti.Size = new System.Drawing.Size(230, 40);
            this.btnListaParticipanti.TabIndex = 10;
            this.btnListaParticipanti.Text = "Listă Participanți Concurs";
            this.btnListaParticipanti.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnListaParticipanti.UseVisualStyleBackColor = false;
            // 
            // btnExportParticipantiSub18
            // 
            this.btnExportParticipantiSub18.BackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc");
            this.btnExportParticipantiSub18.FlatAppearance.BorderSize = 0;
            this.btnExportParticipantiSub18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportParticipantiSub18.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnExportParticipantiSub18.ForeColor = System.Drawing.Color.White;
            this.btnExportParticipantiSub18.Location = new System.Drawing.Point(18, 68);
            this.btnExportParticipantiSub18.MinimumSize = new System.Drawing.Size(180, 40);
            this.btnExportParticipantiSub18.Name = "btnExportParticipantiSub18";
            this.btnExportParticipantiSub18.Size = new System.Drawing.Size(230, 40);
            this.btnExportParticipantiSub18.TabIndex = 11;
            this.btnExportParticipantiSub18.Text = "Export Participanți Sub 18 Ani";
            this.btnExportParticipantiSub18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExportParticipantiSub18.UseVisualStyleBackColor = false;
            // 
            // tabClasamenteGenerale
            // 
            this.tabClasamenteGenerale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(36)))));
            this.tabClasamenteGenerale.Controls.Add(this.btnShowMelodyRankingsCG);
            this.tabClasamenteGenerale.Controls.Add(this.btnShowIntervieweeRankingsCG);
            this.tabClasamenteGenerale.Location = new System.Drawing.Point(4, 29);
            this.tabClasamenteGenerale.Name = "tabClasamenteGenerale";
            this.tabClasamenteGenerale.Padding = new System.Windows.Forms.Padding(15);
            this.tabClasamenteGenerale.Size = new System.Drawing.Size(792, 503);
            this.tabClasamenteGenerale.TabIndex = 4;
            this.tabClasamenteGenerale.Text = "Clasamente Generale";
            // Add buttons to tabClasamenteGenerale
            this.btnShowMelodyRankingsCG = new System.Windows.Forms.Button();
            this.btnShowIntervieweeRankingsCG = new System.Windows.Forms.Button();
            this.tabClasamenteGenerale.Controls.Add(this.btnShowMelodyRankingsCG);
            this.tabClasamenteGenerale.Controls.Add(this.btnShowIntervieweeRankingsCG);
            // 
            // btnShowMelodyRankingsCG
            // 
            this.btnShowMelodyRankingsCG.BackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc");
            this.btnShowMelodyRankingsCG.FlatAppearance.BorderSize = 0;
            this.btnShowMelodyRankingsCG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowMelodyRankingsCG.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnShowMelodyRankingsCG.ForeColor = System.Drawing.Color.White;
            this.btnShowMelodyRankingsCG.Location = new System.Drawing.Point(18, 18);
            this.btnShowMelodyRankingsCG.MinimumSize = new System.Drawing.Size(180, 40);
            this.btnShowMelodyRankingsCG.Name = "btnShowMelodyRankingsCG";
            this.btnShowMelodyRankingsCG.Size = new System.Drawing.Size(230, 40);
            this.btnShowMelodyRankingsCG.TabIndex = 12;
            this.btnShowMelodyRankingsCG.Text = "Clasament Melodii";
            this.btnShowMelodyRankingsCG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnShowMelodyRankingsCG.UseVisualStyleBackColor = false;
            // 
            // btnShowIntervieweeRankingsCG
            // 
            this.btnShowIntervieweeRankingsCG.BackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc");
            this.btnShowIntervieweeRankingsCG.FlatAppearance.BorderSize = 0;
            this.btnShowIntervieweeRankingsCG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowIntervieweeRankingsCG.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnShowIntervieweeRankingsCG.ForeColor = System.Drawing.Color.White;
            this.btnShowIntervieweeRankingsCG.Location = new System.Drawing.Point(18, 68);
            this.btnShowIntervieweeRankingsCG.MinimumSize = new System.Drawing.Size(180, 40);
            this.btnShowIntervieweeRankingsCG.Name = "btnShowIntervieweeRankingsCG";
            this.btnShowIntervieweeRankingsCG.Size = new System.Drawing.Size(230, 40);
            this.btnShowIntervieweeRankingsCG.TabIndex = 13;
            this.btnShowIntervieweeRankingsCG.Text = "Clasament Intervievați";
            this.btnShowIntervieweeRankingsCG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnShowIntervieweeRankingsCG.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(17)))), ((int)(((byte)(26))))); 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.mainTabControl); 
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MelodiiApp - Concurs Muzical"; 
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.mainTabControl.ResumeLayout(false);
            this.tabMelodii.ResumeLayout(false);
            this.tabIntervievati.ResumeLayout(false);
            this.tabVoteazaMelodii.ResumeLayout(false);
            this.tabManagementPredictii.ResumeLayout(false);
            this.tabRapoarte.ResumeLayout(false);
            this.tabClasamenteGenerale.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabMelodii;
        private System.Windows.Forms.TabPage tabIntervievati;
        private System.Windows.Forms.TabPage tabVoteazaMelodii;
        private System.Windows.Forms.TabPage tabManagementPredictii;
        private System.Windows.Forms.TabPage tabRapoarte;
        private System.Windows.Forms.TabPage tabClasamenteGenerale;

        private System.Windows.Forms.Button btnAdaugaMelodie;
        private System.Windows.Forms.Button btnGestioneazaMelodii;
        private System.Windows.Forms.Button btnAdaugaIntervievat;
        private System.Windows.Forms.Button btnGestioneazaIntervievati;
        private System.Windows.Forms.Button btnInregistreazaVoturi;
        private System.Windows.Forms.Button btnInregistreazaPredictii;
        private System.Windows.Forms.Button btnListaParticipanti;
        private System.Windows.Forms.Button btnExportParticipantiSub18;
        private System.Windows.Forms.Button btnShowMelodyRankingsCG;
        private System.Windows.Forms.Button btnShowIntervieweeRankingsCG;
    }
} 