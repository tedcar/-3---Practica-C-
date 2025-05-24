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
            this.tabAdministrare = new System.Windows.Forms.TabPage();
            this.tabRapoarte = new System.Windows.Forms.TabPage();

            // Button declarations - ensure they are declared only once and are the same instances used throughout
            this.btnAdaugaMelodie = new System.Windows.Forms.Button();
            this.btnGestioneazaMelodii = new System.Windows.Forms.Button();
            this.btnVeziClasamentMelodii = new System.Windows.Forms.Button();
            this.btnAdaugaIntervievat = new System.Windows.Forms.Button();
            this.btnGestioneazaIntervievati = new System.Windows.Forms.Button();
            this.btnVeziClasamentIntervievati = new System.Windows.Forms.Button();
            this.btnInregistreazaVoturi = new System.Windows.Forms.Button();
            this.btnInregistreazaPredictii = new System.Windows.Forms.Button();
            this.btnActualizeazaClasamente = new System.Windows.Forms.Button();
            this.btnListaParticipanti = new System.Windows.Forms.Button();
            this.btnExportParticipantiSub18 = new System.Windows.Forms.Button();
            
            this.mainTabControl.SuspendLayout();
            this.tabMelodii.SuspendLayout();
            this.tabIntervievati.SuspendLayout();
            this.tabVoteazaMelodii.SuspendLayout();
            this.tabManagementPredictii.SuspendLayout();
            this.tabAdministrare.SuspendLayout();
            this.tabRapoarte.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.tabMelodii);
            this.mainTabControl.Controls.Add(this.tabIntervievati);
            this.mainTabControl.Controls.Add(this.tabVoteazaMelodii);
            this.mainTabControl.Controls.Add(this.tabManagementPredictii);
            this.mainTabControl.Controls.Add(this.tabAdministrare);
            this.mainTabControl.Controls.Add(this.tabRapoarte);
            this.mainTabControl.Controls.Add(this.tabDespre); // Added tabDespre
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabControl.Location = new System.Drawing.Point(0, 64); // Assuming MaterialSkin header of 64px
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(800, 536); 
            this.mainTabControl.TabIndex = 0;
            // 
            // tabMelodii
            // 
            this.tabMelodii.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(36))))); 
            this.tabMelodii.Controls.Add(this.btnVeziClasamentMelodii);
            this.tabMelodii.Controls.Add(this.btnGestioneazaMelodii);
            this.tabMelodii.Controls.Add(this.btnAdaugaMelodie);
            this.tabMelodii.Location = new System.Drawing.Point(4, 26);
            this.tabMelodii.Name = "tabMelodii";
            this.tabMelodii.Padding = new System.Windows.Forms.Padding(15);
            this.tabMelodii.Size = new System.Drawing.Size(792, 506);
            this.tabMelodii.TabIndex = 0;
            this.tabMelodii.Text = "Melodii (Admin)";
            // 
            // btnAdaugaMelodie
            // 
            this.btnAdaugaMelodie.FlatAppearance.BorderSize = 0;
            this.btnAdaugaMelodie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdaugaMelodie.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAdaugaMelodie.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAdaugaMelodie.Location = new System.Drawing.Point(18, 18);
            this.btnAdaugaMelodie.Name = "btnAdaugaMelodie";
            this.btnAdaugaMelodie.Size = new System.Drawing.Size(210, 40);
            this.btnAdaugaMelodie.TabIndex = 1;
            this.btnAdaugaMelodie.Text = "Adaugă Melodie Nouă";
            this.btnAdaugaMelodie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAdaugaMelodie.UseVisualStyleBackColor = true;
            // 
            // btnGestioneazaMelodii
            // 
            this.btnGestioneazaMelodii.FlatAppearance.BorderSize = 0;
            this.btnGestioneazaMelodii.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestioneazaMelodii.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnGestioneazaMelodii.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnGestioneazaMelodii.Location = new System.Drawing.Point(18, 68);
            this.btnGestioneazaMelodii.Name = "btnGestioneazaMelodii";
            this.btnGestioneazaMelodii.Size = new System.Drawing.Size(210, 40);
            this.btnGestioneazaMelodii.TabIndex = 2;
            this.btnGestioneazaMelodii.Text = "Gestionează Melodii";
            this.btnGestioneazaMelodii.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnGestioneazaMelodii.UseVisualStyleBackColor = true;
            // 
            // btnVeziClasamentMelodii
            // 
            this.btnVeziClasamentMelodii.FlatAppearance.BorderSize = 0;
            this.btnVeziClasamentMelodii.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVeziClasamentMelodii.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnVeziClasamentMelodii.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnVeziClasamentMelodii.Location = new System.Drawing.Point(18, 118);
            this.btnVeziClasamentMelodii.Name = "btnVeziClasamentMelodii";
            this.btnVeziClasamentMelodii.Size = new System.Drawing.Size(210, 40);
            this.btnVeziClasamentMelodii.TabIndex = 3;
            this.btnVeziClasamentMelodii.Text = "Clasament Melodii";
            this.btnVeziClasamentMelodii.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnVeziClasamentMelodii.UseVisualStyleBackColor = true;
            // 
            // tabIntervievati
            // 
            this.tabIntervievati.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(36)))));
            this.tabIntervievati.Controls.Add(this.btnVeziClasamentIntervievati);
            this.tabIntervievati.Controls.Add(this.btnGestioneazaIntervievati);
            this.tabIntervievati.Controls.Add(this.btnAdaugaIntervievat);
            this.tabIntervievati.Location = new System.Drawing.Point(4, 26);
            this.tabIntervievati.Name = "tabIntervievati";
            this.tabIntervievati.Padding = new System.Windows.Forms.Padding(15);
            this.tabIntervievati.Size = new System.Drawing.Size(792, 506);
            this.tabIntervievati.TabIndex = 1;
            this.tabIntervievati.Text = "Intervievați (Admin)";
            // 
            // btnAdaugaIntervievat
            // 
            this.btnAdaugaIntervievat.FlatAppearance.BorderSize = 0;
            this.btnAdaugaIntervievat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdaugaIntervievat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAdaugaIntervievat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnAdaugaIntervievat.Location = new System.Drawing.Point(18, 18);
            this.btnAdaugaIntervievat.Name = "btnAdaugaIntervievat";
            this.btnAdaugaIntervievat.Size = new System.Drawing.Size(210, 40);
            this.btnAdaugaIntervievat.TabIndex = 4;
            this.btnAdaugaIntervievat.Text = "Adaugă Intervievat Nou";
            this.btnAdaugaIntervievat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAdaugaIntervievat.UseVisualStyleBackColor = true;
            // 
            // btnGestioneazaIntervievati
            // 
            this.btnGestioneazaIntervievati.FlatAppearance.BorderSize = 0;
            this.btnGestioneazaIntervievati.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestioneazaIntervievati.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnGestioneazaIntervievati.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnGestioneazaIntervievati.Location = new System.Drawing.Point(18, 68);
            this.btnGestioneazaIntervievati.Name = "btnGestioneazaIntervievati";
            this.btnGestioneazaIntervievati.Size = new System.Drawing.Size(210, 40);
            this.btnGestioneazaIntervievati.TabIndex = 5;
            this.btnGestioneazaIntervievati.Text = "Gestionează Intervievați";
            this.btnGestioneazaIntervievati.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnGestioneazaIntervievati.UseVisualStyleBackColor = true;
            // 
            // btnVeziClasamentIntervievati
            // 
            this.btnVeziClasamentIntervievati.FlatAppearance.BorderSize = 0;
            this.btnVeziClasamentIntervievati.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVeziClasamentIntervievati.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnVeziClasamentIntervievati.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnVeziClasamentIntervievati.Location = new System.Drawing.Point(18, 118);
            this.btnVeziClasamentIntervievati.Name = "btnVeziClasamentIntervievati";
            this.btnVeziClasamentIntervievati.Size = new System.Drawing.Size(210, 40);
            this.btnVeziClasamentIntervievati.TabIndex = 6;
            this.btnVeziClasamentIntervievati.Text = "Clasament Intervievați";
            this.btnVeziClasamentIntervievati.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnVeziClasamentIntervievati.UseVisualStyleBackColor = true;
            // 
            // tabVoteazaMelodii
            // 
            this.tabVoteazaMelodii.BackColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5");
            this.tabVoteazaMelodii.Controls.Add(this.btnInregistreazaVoturi);
            this.tabVoteazaMelodii.Location = new System.Drawing.Point(4, 26);
            this.tabVoteazaMelodii.Name = "tabVoteazaMelodii";
            this.tabVoteazaMelodii.Padding = new System.Windows.Forms.Padding(15);
            this.tabVoteazaMelodii.Size = new System.Drawing.Size(792, 506);
            this.tabVoteazaMelodii.TabIndex = 2;
            this.tabVoteazaMelodii.Text = "Votează Melodii";
            // 
            // btnInregistreazaVoturi
            // 
            this.btnInregistreazaVoturi.FlatAppearance.BorderSize = 0;
            this.btnInregistreazaVoturi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInregistreazaVoturi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnInregistreazaVoturi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnInregistreazaVoturi.Location = new System.Drawing.Point(18, 18);
            this.btnInregistreazaVoturi.Name = "btnInregistreazaVoturi";
            this.btnInregistreazaVoturi.Size = new System.Drawing.Size(230, 40);
            this.btnInregistreazaVoturi.TabIndex = 7;
            this.btnInregistreazaVoturi.Text = "Votează Melodii (6 puncte)";
            this.btnInregistreazaVoturi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnInregistreazaVoturi.UseVisualStyleBackColor = true;
            // 
            // tabManagementPredictii
            // 
            this.tabManagementPredictii.BackColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5");
            this.tabManagementPredictii.Controls.Add(this.btnInregistreazaPredictii);
            this.tabManagementPredictii.Location = new System.Drawing.Point(4, 26);
            this.tabManagementPredictii.Name = "tabManagementPredictii";
            this.tabManagementPredictii.Padding = new System.Windows.Forms.Padding(15);
            this.tabManagementPredictii.Size = new System.Drawing.Size(792, 506);
            this.tabManagementPredictii.TabIndex = 3;
            this.tabManagementPredictii.Text = "Management Predicții";
            // 
            // btnInregistreazaPredictii
            // 
            this.btnInregistreazaPredictii.FlatAppearance.BorderSize = 0;
            this.btnInregistreazaPredictii.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInregistreazaPredictii.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnInregistreazaPredictii.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnInregistreazaPredictii.Location = new System.Drawing.Point(18, 68);
            this.btnInregistreazaPredictii.Name = "btnInregistreazaPredictii";
            this.btnInregistreazaPredictii.Size = new System.Drawing.Size(230, 40);
            this.btnInregistreazaPredictii.TabIndex = 8;
            this.btnInregistreazaPredictii.Text = "Înregistrează Predicții Top 3";
            this.btnInregistreazaPredictii.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnInregistreazaPredictii.UseVisualStyleBackColor = true;
            // 
            // tabAdministrare
            // 
            this.tabAdministrare.BackColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5");
            this.tabAdministrare.Controls.Add(this.btnActualizeazaClasamente);
            this.tabAdministrare.Location = new System.Drawing.Point(4, 26);
            this.tabAdministrare.Name = "tabAdministrare";
            this.tabAdministrare.Padding = new System.Windows.Forms.Padding(15);
            this.tabAdministrare.Size = new System.Drawing.Size(792, 506);
            this.tabAdministrare.TabIndex = 4;
            this.tabAdministrare.Text = "Administrare Clasamente";
            // 
            // btnActualizeazaClasamente
            // 
            this.btnActualizeazaClasamente.FlatAppearance.BorderSize = 0;
            this.btnActualizeazaClasamente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizeazaClasamente.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnActualizeazaClasamente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnActualizeazaClasamente.Location = new System.Drawing.Point(18, 18);
            this.btnActualizeazaClasamente.Name = "btnActualizeazaClasamente";
            this.btnActualizeazaClasamente.Size = new System.Drawing.Size(210, 40);
            this.btnActualizeazaClasamente.TabIndex = 9;
            this.btnActualizeazaClasamente.Text = "Actualizează Clasamente";
            this.btnActualizeazaClasamente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnActualizeazaClasamente.UseVisualStyleBackColor = true;
            // 
            // tabRapoarte
            // 
            this.tabRapoarte.BackColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5");
            this.tabRapoarte.Controls.Add(this.btnExportParticipantiSub18);
            this.tabRapoarte.Controls.Add(this.btnListaParticipanti);
            this.tabRapoarte.Location = new System.Drawing.Point(4, 26);
            this.tabRapoarte.Name = "tabRapoarte";
            this.tabRapoarte.Padding = new System.Windows.Forms.Padding(15);
            this.tabRapoarte.Size = new System.Drawing.Size(792, 506);
            this.tabRapoarte.TabIndex = 5;
            this.tabRapoarte.Text = "Rapoarte";
            // 
            // btnListaParticipanti
            // 
            this.btnListaParticipanti.FlatAppearance.BorderSize = 0;
            this.btnListaParticipanti.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnListaParticipanti.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnListaParticipanti.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnListaParticipanti.Location = new System.Drawing.Point(18, 18);
            this.btnListaParticipanti.Name = "btnListaParticipanti";
            this.btnListaParticipanti.Size = new System.Drawing.Size(230, 40);
            this.btnListaParticipanti.TabIndex = 10;
            this.btnListaParticipanti.Text = "Listă Participanți Concurs";
            this.btnListaParticipanti.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnListaParticipanti.UseVisualStyleBackColor = true;
            // 
            // btnExportParticipantiSub18
            // 
            this.btnExportParticipantiSub18.FlatAppearance.BorderSize = 0;
            this.btnExportParticipantiSub18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportParticipantiSub18.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnExportParticipantiSub18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExportParticipantiSub18.Location = new System.Drawing.Point(18, 68);
            this.btnExportParticipantiSub18.Name = "btnExportParticipantiSub18";
            this.btnExportParticipantiSub18.Size = new System.Drawing.Size(230, 40);
            this.btnExportParticipantiSub18.TabIndex = 11;
            this.btnExportParticipantiSub18.Text = "Export Participanți Sub 18 Ani";
            this.btnExportParticipantiSub18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExportParticipantiSub18.UseVisualStyleBackColor = true;
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
            this.tabAdministrare.ResumeLayout(false);
            this.tabRapoarte.ResumeLayout(false);
            this.ResumeLayout(false);
            // 
            // tabDespre
            // 
            this.tabDespre = new System.Windows.Forms.TabPage();
            this.tabDespre.BackColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5");
            this.tabDespre.Location = new System.Drawing.Point(4, 26);
            this.tabDespre.Name = "tabDespre";
            this.tabDespre.Padding = new System.Windows.Forms.Padding(15);
            this.tabDespre.Size = new System.Drawing.Size(792, 506);
            this.tabDespre.TabIndex = 6; 
            this.tabDespre.Text = "Despre";
            // 
            // lblDespreInfo 
            // 
            this.lblDespreInfo = new System.Windows.Forms.Label();
            this.lblDespreInfo.AutoSize = true;
            this.lblDespreInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDespreInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049"); // Dark blue text
            this.lblDespreInfo.Location = new System.Drawing.Point(20, 20);
            this.lblDespreInfo.Name = "lblDespreInfo";
            this.lblDespreInfo.Size = new System.Drawing.Size(400, 60); // Example size
            this.lblDespreInfo.Text = "MelodiiApp - Concurs Muzical\nVersiunea 1.0\nDezvoltat de [Your Name/Team]";
            this.tabDespre.Controls.Add(this.lblDespreInfo);
            this.tabDespre.ResumeLayout(false);
            this.tabDespre.PerformLayout();
            // 
            // btnLogout
            // 
            this.btnLogout = new MaterialSkin.Controls.MaterialButton();
            this.btnLogout.AutoSize = false; // Allow custom size
            this.btnLogout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLogout.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.btnLogout.Depth = 0;
            this.btnLogout.HighEmphasis = true;
            this.btnLogout.Icon = null; // Can add an icon later if desired
            this.btnLogout.Location = new System.Drawing.Point(670, 28); // Position in the header area
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnLogout.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.NoAccentTextColor = System.Drawing.Color.Empty;
            this.btnLogout.Size = new System.Drawing.Size(120, 30); // Adjusted size
            this.btnLogout.TabIndex = 12; // Ensure this is a unique and appropriate TabIndex
            this.btnLogout.Text = "Deconectare";
            this.btnLogout.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.btnLogout.UseAccentColor = false;
            this.btnLogout.UseVisualStyleBackColor = true;
            // this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click); // Will be wired in MainForm.cs
            this.Controls.Add(this.btnLogout); // Add to form's controls, not TabControl
            this.btnLogout.BringToFront(); // Ensure it's visible above the TabControl if overlapping


        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabMelodii;
        private System.Windows.Forms.TabPage tabIntervievati;
        private System.Windows.Forms.TabPage tabVoteazaMelodii;
        private System.Windows.Forms.TabPage tabManagementPredictii;
        private System.Windows.Forms.TabPage tabAdministrare;
        private System.Windows.Forms.TabPage tabRapoarte;
        private System.Windows.Forms.TabPage tabDespre; // Added tabDespre field
        private System.Windows.Forms.Label lblDespreInfo; // Added label for tabDespre

        private System.Windows.Forms.Button btnAdaugaMelodie;
        private System.Windows.Forms.Button btnGestioneazaMelodii;
        private System.Windows.Forms.Button btnVeziClasamentMelodii;
        private System.Windows.Forms.Button btnAdaugaIntervievat;
        private System.Windows.Forms.Button btnGestioneazaIntervievati;
        private System.Windows.Forms.Button btnVeziClasamentIntervievati;
        private System.Windows.Forms.Button btnInregistreazaVoturi;
        private System.Windows.Forms.Button btnInregistreazaPredictii;
        private System.Windows.Forms.Button btnActualizeazaClasamente;
        private System.Windows.Forms.Button btnListaParticipanti;
        private System.Windows.Forms.Button btnExportParticipantiSub18;
        private MelodiiApp.UserInterface.Controls.ListaVotantiControl _listaVotantiControl; // Added for new control
        private MaterialSkin.Controls.MaterialButton btnLogout; // Declaration for the logout button
    }
} 