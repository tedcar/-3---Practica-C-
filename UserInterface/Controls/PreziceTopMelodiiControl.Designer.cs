using System.Drawing; // Added to resolve Color issues

namespace MelodiiApp.UserInterface.Controls
{
    partial class PreziceTopMelodiiControl
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

        #region Windows Form Designer generated code -> Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitlu = new System.Windows.Forms.Label();
            this.lblIntervievat = new System.Windows.Forms.Label();
            this.cmbIntervievat = new System.Windows.Forms.ComboBox();
            this.lblMelodieLoc1 = new System.Windows.Forms.Label();
            this.cmbMelodieLoc1 = new System.Windows.Forms.ComboBox();
            this.lblMelodieLoc2 = new System.Windows.Forms.Label();
            this.cmbMelodieLoc2 = new System.Windows.Forms.ComboBox();
            this.lblMelodieLoc3 = new System.Windows.Forms.Label();
            this.cmbMelodieLoc3 = new System.Windows.Forms.ComboBox();
            this.btnSalveazaPredictia = new System.Windows.Forms.Button();
            this.lblStatusPredictie = new System.Windows.Forms.Label();
            this.btnInchidePanou = new System.Windows.Forms.Button();
            this.lblScoringRules = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitlu
            // 
            this.lblTitlu.AutoSize = true;
            this.lblTitlu.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitlu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblTitlu.Location = new System.Drawing.Point(25, 20);
            this.lblTitlu.Name = "lblTitlu";
            this.lblTitlu.Size = new System.Drawing.Size(300, 30);
            this.lblTitlu.TabIndex = 0;
            this.lblTitlu.Text = "Înregistrează Predicții Top 3";
            // 
            // lblIntervievat
            // 
            this.lblIntervievat.AutoSize = true;
            this.lblIntervievat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIntervievat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblIntervievat.Location = new System.Drawing.Point(28, 70);
            this.lblIntervievat.Name = "lblIntervievat";
            this.lblIntervievat.Size = new System.Drawing.Size(78, 19);
            this.lblIntervievat.TabIndex = 1;
            this.lblIntervievat.Text = "Intervievat:";
            // 
            // cmbIntervievat
            // 
            this.cmbIntervievat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(92)))));
            this.cmbIntervievat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIntervievat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbIntervievat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbIntervievat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbIntervievat.FormattingEnabled = true;
            this.cmbIntervievat.Location = new System.Drawing.Point(30, 92);
            this.cmbIntervievat.Name = "cmbIntervievat";
            this.cmbIntervievat.Size = new System.Drawing.Size(380, 25);
            this.cmbIntervievat.TabIndex = 2;
            // 
            // lblMelodieLoc1
            // 
            this.lblMelodieLoc1.AutoSize = true;
            this.lblMelodieLoc1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMelodieLoc1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMelodieLoc1.Location = new System.Drawing.Point(28, 130);
            this.lblMelodieLoc1.Name = "lblMelodieLoc1";
            this.lblMelodieLoc1.Size = new System.Drawing.Size(122, 19);
            this.lblMelodieLoc1.TabIndex = 3;
            this.lblMelodieLoc1.Text = "Predicție Locul 1:";
            // 
            // cmbMelodieLoc1
            // 
            this.cmbMelodieLoc1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(92)))));
            this.cmbMelodieLoc1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMelodieLoc1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMelodieLoc1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbMelodieLoc1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbMelodieLoc1.FormattingEnabled = true;
            this.cmbMelodieLoc1.Location = new System.Drawing.Point(30, 152);
            this.cmbMelodieLoc1.Name = "cmbMelodieLoc1";
            this.cmbMelodieLoc1.Size = new System.Drawing.Size(380, 25);
            this.cmbMelodieLoc1.TabIndex = 4;
            // 
            // lblMelodieLoc2
            // 
            this.lblMelodieLoc2.AutoSize = true;
            this.lblMelodieLoc2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMelodieLoc2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMelodieLoc2.Location = new System.Drawing.Point(28, 190);
            this.lblMelodieLoc2.Name = "lblMelodieLoc2";
            this.lblMelodieLoc2.Size = new System.Drawing.Size(122, 19);
            this.lblMelodieLoc2.TabIndex = 5;
            this.lblMelodieLoc2.Text = "Predicție Locul 2:";
            // 
            // cmbMelodieLoc2
            // 
            this.cmbMelodieLoc2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(92)))));
            this.cmbMelodieLoc2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMelodieLoc2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMelodieLoc2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbMelodieLoc2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbMelodieLoc2.FormattingEnabled = true;
            this.cmbMelodieLoc2.Location = new System.Drawing.Point(30, 212);
            this.cmbMelodieLoc2.Name = "cmbMelodieLoc2";
            this.cmbMelodieLoc2.Size = new System.Drawing.Size(380, 25);
            this.cmbMelodieLoc2.TabIndex = 6;
            // 
            // lblMelodieLoc3
            // 
            this.lblMelodieLoc3.AutoSize = true;
            this.lblMelodieLoc3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMelodieLoc3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMelodieLoc3.Location = new System.Drawing.Point(28, 250);
            this.lblMelodieLoc3.Name = "lblMelodieLoc3";
            this.lblMelodieLoc3.Size = new System.Drawing.Size(122, 19);
            this.lblMelodieLoc3.TabIndex = 7;
            this.lblMelodieLoc3.Text = "Predicție Locul 3:";
            // 
            // cmbMelodieLoc3
            // 
            this.cmbMelodieLoc3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(59)))), ((int)(((byte)(92)))));
            this.cmbMelodieLoc3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMelodieLoc3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMelodieLoc3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbMelodieLoc3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmbMelodieLoc3.FormattingEnabled = true;
            this.cmbMelodieLoc3.Location = new System.Drawing.Point(30, 272);
            this.cmbMelodieLoc3.Name = "cmbMelodieLoc3";
            this.cmbMelodieLoc3.Size = new System.Drawing.Size(380, 25);
            this.cmbMelodieLoc3.TabIndex = 8;
            // 
            // btnSalveazaPredictia
            // 
            this.btnSalveazaPredictia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.btnSalveazaPredictia.FlatAppearance.BorderSize = 0;
            this.btnSalveazaPredictia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalveazaPredictia.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalveazaPredictia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(17)))), ((int)(((byte)(26)))));
            this.btnSalveazaPredictia.Location = new System.Drawing.Point(30, 320);
            this.btnSalveazaPredictia.Name = "btnSalveazaPredictia";
            this.btnSalveazaPredictia.Size = new System.Drawing.Size(270, 35); // Made smaller to accommodate close button
            this.btnSalveazaPredictia.TabIndex = 9;
            this.btnSalveazaPredictia.Text = "Salvează Predicția";
            this.btnSalveazaPredictia.UseVisualStyleBackColor = false;
            // 
            // lblStatusPredictie
            // 
            this.lblStatusPredictie.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusPredictie.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblStatusPredictie.Location = new System.Drawing.Point(30, 395);
            this.lblStatusPredictie.Name = "lblStatusPredictie";
            this.lblStatusPredictie.Size = new System.Drawing.Size(380, 23);
            this.lblStatusPredictie.TabIndex = 10;
            this.lblStatusPredictie.Text = ""; 
            this.lblStatusPredictie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatusPredictie.Visible = false;
            // 
            // lblScoringRules
            // 
            this.lblScoringRules.AutoSize = true;
            this.lblScoringRules.Font = new System.Drawing.Font("Segoe UI Semilight", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScoringRules.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.lblScoringRules.Location = new System.Drawing.Point(30, 360);
            this.lblScoringRules.MaximumSize = new System.Drawing.Size(380, 0);
            this.lblScoringRules.Name = "lblScoringRules";
            this.lblScoringRules.Size = new System.Drawing.Size(350, 30);
            this.lblScoringRules.TabIndex = 12;
            this.lblScoringRules.Text = "Reguli Punctaj: Locul 1 corect = 10 pct; Locul 2 corect = 5 pct; Locul 3 corect = 1 pct.";
            // 
            // btnInchidePanou
            // 
            this.btnInchidePanou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(90)))), ((int)(((byte)(110))))); // A slightly different color
            this.btnInchidePanou.FlatAppearance.BorderSize = 0;
            this.btnInchidePanou.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInchidePanou.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.btnInchidePanou.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnInchidePanou.Location = new System.Drawing.Point(310, 320);
            this.btnInchidePanou.Name = "btnInchidePanou";
            this.btnInchidePanou.Size = new System.Drawing.Size(100, 35);
            this.btnInchidePanou.TabIndex = 11;
            this.btnInchidePanou.Text = "Închide";
            this.btnInchidePanou.UseVisualStyleBackColor = false;
            // 
            // PreziceTopMelodiiControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(28, 35, 49); // Dark Slate #1C2331
            // Removed ClientSize, FormBorderStyle, MaximizeBox, MinimizeBox, StartPosition, Text - not applicable to UserControl
            this.Controls.Add(this.lblScoringRules);
            this.Controls.Add(this.lblStatusPredictie);
            this.Controls.Add(this.btnSalveazaPredictia);
            this.Controls.Add(this.btnInchidePanou); // Added close button
            this.Controls.Add(this.cmbMelodieLoc3);
            this.Controls.Add(this.lblMelodieLoc3);
            this.Controls.Add(this.cmbMelodieLoc2);
            this.Controls.Add(this.lblMelodieLoc2);
            this.Controls.Add(this.cmbMelodieLoc1);
            this.Controls.Add(this.lblMelodieLoc1);
            this.Controls.Add(this.cmbIntervievat);
            this.Controls.Add(this.lblIntervievat);
            this.Controls.Add(this.lblTitlu);
            this.Name = "PreziceTopMelodiiControl"; // Changed name
            this.Padding = new System.Windows.Forms.Padding(15); // Add some padding
            this.Size = new System.Drawing.Size(450, 450); // Adjusted size for new label
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitlu;
        private System.Windows.Forms.Label lblIntervievat;
        private System.Windows.Forms.ComboBox cmbIntervievat;
        private System.Windows.Forms.Label lblMelodieLoc1;
        private System.Windows.Forms.ComboBox cmbMelodieLoc1;
        private System.Windows.Forms.Label lblMelodieLoc2;
        private System.Windows.Forms.ComboBox cmbMelodieLoc2;
        private System.Windows.Forms.Label lblMelodieLoc3;
        private System.Windows.Forms.ComboBox cmbMelodieLoc3;
        private System.Windows.Forms.Button btnSalveazaPredictia;
        private System.Windows.Forms.Label lblStatusPredictie;
        private System.Windows.Forms.Button btnInchidePanou; // Added declaration
        private System.Windows.Forms.Label lblScoringRules; // Add this line
    }
} 