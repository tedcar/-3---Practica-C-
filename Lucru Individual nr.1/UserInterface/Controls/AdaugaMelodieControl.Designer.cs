namespace MelodiiApp.UserInterface.Controls
{
    partial class AdaugaMelodieControl
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
            this.lblTitlu = new System.Windows.Forms.Label();
            this.txtTitlu = new System.Windows.Forms.TextBox();
            this.lblArtist = new System.Windows.Forms.Label();
            this.txtArtist = new System.Windows.Forms.TextBox();
            this.lblGenMuzical = new System.Windows.Forms.Label();
            this.txtGenMuzical = new System.Windows.Forms.TextBox();
            this.lblAnLansare = new System.Windows.Forms.Label();
            this.numAnLansare = new System.Windows.Forms.NumericUpDown();
            this.btnSalveazaMelodie = new System.Windows.Forms.Button();
            this.btnAnuleaza = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numAnLansare)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitlu
            // 
            this.lblTitlu.AutoSize = true;
            this.lblTitlu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitlu.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.lblTitlu.Location = new System.Drawing.Point(25, 30);
            this.lblTitlu.Name = "lblTitlu";
            this.lblTitlu.Size = new System.Drawing.Size(40, 19);
            this.lblTitlu.TabIndex = 0;
            this.lblTitlu.Text = "Titlu:";
            // 
            // txtTitlu
            // 
            this.txtTitlu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTitlu.Location = new System.Drawing.Point(130, 27);
            this.txtTitlu.Name = "txtTitlu";
            this.txtTitlu.Size = new System.Drawing.Size(280, 25);
            this.txtTitlu.TabIndex = 1;
            this.txtTitlu.BackColor = System.Drawing.Color.White;
            this.txtTitlu.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.txtTitlu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // lblArtist
            // 
            this.lblArtist.AutoSize = true;
            this.lblArtist.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblArtist.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.lblArtist.Location = new System.Drawing.Point(25, 70);
            this.lblArtist.Name = "lblArtist";
            this.lblArtist.Size = new System.Drawing.Size(45, 19);
            this.lblArtist.TabIndex = 2;
            this.lblArtist.Text = "Artist:";
            // 
            // txtArtist
            // 
            this.txtArtist.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtArtist.Location = new System.Drawing.Point(130, 67);
            this.txtArtist.Name = "txtArtist";
            this.txtArtist.Size = new System.Drawing.Size(280, 25);
            this.txtArtist.TabIndex = 2;
            this.txtArtist.BackColor = System.Drawing.Color.White;
            this.txtArtist.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.txtArtist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // lblGenMuzical
            // 
            this.lblGenMuzical.AutoSize = true;
            this.lblGenMuzical.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGenMuzical.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.lblGenMuzical.Location = new System.Drawing.Point(25, 110);
            this.lblGenMuzical.Name = "lblGenMuzical";
            this.lblGenMuzical.Size = new System.Drawing.Size(88, 19);
            this.lblGenMuzical.TabIndex = 4;
            this.lblGenMuzical.Text = "Gen Muzical:";
            // 
            // txtGenMuzical
            // 
            this.txtGenMuzical.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGenMuzical.Location = new System.Drawing.Point(130, 107);
            this.txtGenMuzical.Name = "txtGenMuzical";
            this.txtGenMuzical.Size = new System.Drawing.Size(280, 25);
            this.txtGenMuzical.TabIndex = 3;
            this.txtGenMuzical.BackColor = System.Drawing.Color.White;
            this.txtGenMuzical.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.txtGenMuzical.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // lblAnLansare
            // 
            this.lblAnLansare.AutoSize = true;
            this.lblAnLansare.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAnLansare.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.lblAnLansare.Location = new System.Drawing.Point(25, 150);
            this.lblAnLansare.Name = "lblAnLansare";
            this.lblAnLansare.Size = new System.Drawing.Size(79, 19);
            this.lblAnLansare.TabIndex = 6;
            this.lblAnLansare.Text = "An Lansare:";
            // 
            // numAnLansare
            // 
            this.numAnLansare.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numAnLansare.Location = new System.Drawing.Point(130, 147);
            this.numAnLansare.Maximum = new decimal(new int[] {
            System.DateTime.Now.Year, 
            0,
            0,
            0});
            this.numAnLansare.Minimum = new decimal(new int[] {
            1800,
            0,
            0,
            0});
            this.numAnLansare.Name = "numAnLansare";
            this.numAnLansare.Size = new System.Drawing.Size(120, 25);
            this.numAnLansare.TabIndex = 4;
            this.numAnLansare.Value = new decimal(new int[] {
            System.DateTime.Now.Year,
            0,
            0,
            0});
            this.numAnLansare.BackColor = System.Drawing.Color.White;
            this.numAnLansare.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.numAnLansare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // btnSalveazaMelodie
            // 
            this.btnSalveazaMelodie.BackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc");
            this.btnSalveazaMelodie.FlatAppearance.BorderSize = 0;
            this.btnSalveazaMelodie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalveazaMelodie.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalveazaMelodie.ForeColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5");
            this.btnSalveazaMelodie.Location = new System.Drawing.Point(290, 195);
            this.btnSalveazaMelodie.Name = "btnSalveazaMelodie";
            this.btnSalveazaMelodie.Size = new System.Drawing.Size(120, 35);
            this.btnSalveazaMelodie.TabIndex = 5;
            this.btnSalveazaMelodie.Text = "Salvează";
            this.btnSalveazaMelodie.UseVisualStyleBackColor = false;
            // 
            // btnAnuleaza
            // 
            this.btnAnuleaza.BackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc");
            this.btnAnuleaza.FlatAppearance.BorderSize = 0;
            this.btnAnuleaza.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnuleaza.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAnuleaza.ForeColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5");
            this.btnAnuleaza.Location = new System.Drawing.Point(160, 195);
            this.btnAnuleaza.Name = "btnAnuleaza";
            this.btnAnuleaza.Size = new System.Drawing.Size(120, 35);
            this.btnAnuleaza.TabIndex = 6;
            this.btnAnuleaza.Text = "Anulează / Închide";
            this.btnAnuleaza.UseVisualStyleBackColor = false;
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.lblStatus.Location = new System.Drawing.Point(25, 240);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 15);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Visible = false;
            // 
            // AdaugaMelodieControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5");
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(450, 300);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnAnuleaza);
            this.Controls.Add(this.btnSalveazaMelodie);
            this.Controls.Add(this.numAnLansare);
            this.Controls.Add(this.lblAnLansare);
            this.Controls.Add(this.txtGenMuzical);
            this.Controls.Add(this.lblGenMuzical);
            this.Controls.Add(this.txtArtist);
            this.Controls.Add(this.lblArtist);
            this.Controls.Add(this.txtTitlu);
            this.Controls.Add(this.lblTitlu);
            this.Name = "AdaugaMelodieControl";
            ((System.ComponentModel.ISupportInitialize)(this.numAnLansare)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitlu;
        private System.Windows.Forms.TextBox txtTitlu;
        private System.Windows.Forms.Label lblArtist;
        private System.Windows.Forms.TextBox txtArtist;
        private System.Windows.Forms.Label lblGenMuzical;
        private System.Windows.Forms.TextBox txtGenMuzical;
        private System.Windows.Forms.Label lblAnLansare;
        private System.Windows.Forms.NumericUpDown numAnLansare;
        private System.Windows.Forms.Button btnSalveazaMelodie;
        private System.Windows.Forms.Button btnAnuleaza;
        private System.Windows.Forms.Label lblStatus;
    }
} 