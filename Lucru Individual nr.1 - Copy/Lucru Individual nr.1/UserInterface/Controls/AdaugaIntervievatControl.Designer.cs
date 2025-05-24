namespace MelodiiApp.UserInterface.Controls
{
    partial class AdaugaIntervievatControl
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblNumeComplet = new System.Windows.Forms.Label();
            this.txtNumeComplet = new System.Windows.Forms.TextBox();
            this.lblVarsta = new System.Windows.Forms.Label();
            this.numVarsta = new System.Windows.Forms.NumericUpDown();
            this.lblLocalitate = new System.Windows.Forms.Label();
            this.txtLocalitate = new System.Windows.Forms.TextBox();
            this.btnSalveazaIntervievat = new System.Windows.Forms.Button();
            this.btnAnuleaza = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numVarsta)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNumeComplet
            // 
            this.lblNumeComplet.AutoSize = true;
            this.lblNumeComplet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNumeComplet.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.lblNumeComplet.Location = new System.Drawing.Point(25, 30);
            this.lblNumeComplet.Name = "lblNumeComplet";
            this.lblNumeComplet.Size = new System.Drawing.Size(101, 19);
            this.lblNumeComplet.TabIndex = 0;
            this.lblNumeComplet.Text = "Nume Complet:";
            // 
            // txtNumeComplet
            // 
            this.txtNumeComplet.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNumeComplet.Location = new System.Drawing.Point(140, 27);
            this.txtNumeComplet.Name = "txtNumeComplet";
            this.txtNumeComplet.Size = new System.Drawing.Size(270, 25);
            this.txtNumeComplet.TabIndex = 1;
            this.txtNumeComplet.BackColor = System.Drawing.Color.White;
            this.txtNumeComplet.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.txtNumeComplet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // lblVarsta
            // 
            this.lblVarsta.AutoSize = true;
            this.lblVarsta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVarsta.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.lblVarsta.Location = new System.Drawing.Point(25, 70);
            this.lblVarsta.Name = "lblVarsta";
            this.lblVarsta.Size = new System.Drawing.Size(50, 19);
            this.lblVarsta.TabIndex = 2;
            this.lblVarsta.Text = "Vârstă:";
            // 
            // numVarsta
            // 
            this.numVarsta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numVarsta.Location = new System.Drawing.Point(140, 67);
            this.numVarsta.Maximum = new decimal(new int[] {120, 0, 0, 0});
            this.numVarsta.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.numVarsta.Name = "numVarsta";
            this.numVarsta.Size = new System.Drawing.Size(100, 25);
            this.numVarsta.TabIndex = 2;
            this.numVarsta.Value = new decimal(new int[] {18, 0, 0, 0});
            this.numVarsta.BackColor = System.Drawing.Color.White;
            this.numVarsta.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.numVarsta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // lblLocalitate
            // 
            this.lblLocalitate.AutoSize = true;
            this.lblLocalitate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLocalitate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.lblLocalitate.Location = new System.Drawing.Point(25, 110);
            this.lblLocalitate.Name = "lblLocalitate";
            this.lblLocalitate.Size = new System.Drawing.Size(70, 19);
            this.lblLocalitate.TabIndex = 4;
            this.lblLocalitate.Text = "Localitate:";
            // 
            // txtLocalitate
            // 
            this.txtLocalitate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLocalitate.Location = new System.Drawing.Point(140, 107);
            this.txtLocalitate.Name = "txtLocalitate";
            this.txtLocalitate.Size = new System.Drawing.Size(270, 25);
            this.txtLocalitate.TabIndex = 3;
            this.txtLocalitate.BackColor = System.Drawing.Color.White;
            this.txtLocalitate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.txtLocalitate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // btnSalveazaIntervievat
            // 
            this.btnSalveazaIntervievat.BackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc");
            this.btnSalveazaIntervievat.FlatAppearance.BorderSize = 0;
            this.btnSalveazaIntervievat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalveazaIntervievat.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalveazaIntervievat.ForeColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5");
            this.btnSalveazaIntervievat.Location = new System.Drawing.Point(290, 155);
            this.btnSalveazaIntervievat.Name = "btnSalveazaIntervievat";
            this.btnSalveazaIntervievat.Size = new System.Drawing.Size(120, 35);
            this.btnSalveazaIntervievat.TabIndex = 4;
            this.btnSalveazaIntervievat.Text = "Salvează";
            this.btnSalveazaIntervievat.UseVisualStyleBackColor = false;
            // 
            // btnAnuleaza
            // 
            this.btnAnuleaza.BackColor = System.Drawing.ColorTranslator.FromHtml("#669bbc");
            this.btnAnuleaza.FlatAppearance.BorderSize = 0;
            this.btnAnuleaza.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnuleaza.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnAnuleaza.ForeColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5");
            this.btnAnuleaza.Location = new System.Drawing.Point(160, 155);
            this.btnAnuleaza.Name = "btnAnuleaza";
            this.btnAnuleaza.Size = new System.Drawing.Size(120, 35);
            this.btnAnuleaza.TabIndex = 5;
            this.btnAnuleaza.Text = "Anulează / Închide Panou";
            this.btnAnuleaza.UseVisualStyleBackColor = false;
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.ColorTranslator.FromHtml("#003049");
            this.lblStatus.Location = new System.Drawing.Point(25, 200);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 15);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Visible = false;
            // 
            // AdaugaIntervievatControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#fdf0d5");
            this.Size = new System.Drawing.Size(434, 230);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnAnuleaza);
            this.Controls.Add(this.btnSalveazaIntervievat);
            this.Controls.Add(this.txtLocalitate);
            this.Controls.Add(this.lblLocalitate);
            this.Controls.Add(this.numVarsta);
            this.Controls.Add(this.lblVarsta);
            this.Controls.Add(this.txtNumeComplet);
            this.Controls.Add(this.lblNumeComplet);
            this.Name = "AdaugaIntervievatControl";
            ((System.ComponentModel.ISupportInitialize)(this.numVarsta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblNumeComplet;
        private System.Windows.Forms.TextBox txtNumeComplet;
        private System.Windows.Forms.Label lblVarsta;
        private System.Windows.Forms.NumericUpDown numVarsta;
        private System.Windows.Forms.Label lblLocalitate;
        private System.Windows.Forms.TextBox txtLocalitate;
        private System.Windows.Forms.Button btnSalveazaIntervievat;
        private System.Windows.Forms.Button btnAnuleaza;
        private System.Windows.Forms.Label lblStatus;
    }
} 