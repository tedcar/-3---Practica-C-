namespace MelodiiApp.UserInterface.Forms
{
    partial class EditMelodieForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitluForm;
        private System.Windows.Forms.Label lblTitlu;
        private System.Windows.Forms.TextBox txtTitlu;
        private System.Windows.Forms.Label lblArtist;
        private System.Windows.Forms.TextBox txtArtist;
        private System.Windows.Forms.Label lblGenMuzical;
        private System.Windows.Forms.TextBox txtGenMuzical;
        private System.Windows.Forms.Label lblAnLansare;
        private System.Windows.Forms.TextBox txtAnLansare; // Consider NumericUpDown for better UX
        private System.Windows.Forms.Button btnSalveaza;
        private System.Windows.Forms.Button btnAnuleaza;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitluForm = new System.Windows.Forms.Label();
            this.lblTitlu = new System.Windows.Forms.Label();
            this.txtTitlu = new System.Windows.Forms.TextBox();
            this.lblArtist = new System.Windows.Forms.Label();
            this.txtArtist = new System.Windows.Forms.TextBox();
            this.lblGenMuzical = new System.Windows.Forms.Label();
            this.txtGenMuzical = new System.Windows.Forms.TextBox();
            this.lblAnLansare = new System.Windows.Forms.Label();
            this.txtAnLansare = new System.Windows.Forms.TextBox();
            this.btnSalveaza = new System.Windows.Forms.Button();
            this.btnAnuleaza = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitluForm
            // 
            this.lblTitluForm.AutoSize = true;
            this.lblTitluForm.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitluForm.Location = new System.Drawing.Point(25, 20);
            this.lblTitluForm.Name = "lblTitluForm";
            this.lblTitluForm.Size = new System.Drawing.Size(200, 30);
            this.lblTitluForm.TabIndex = 0;
            this.lblTitluForm.Text = "Modifică Melodie";
            // 
            // lblTitlu
            // 
            this.lblTitlu.AutoSize = true;
            this.lblTitlu.Location = new System.Drawing.Point(27, 70);
            this.lblTitlu.Name = "lblTitlu";
            this.lblTitlu.Size = new System.Drawing.Size(40, 15);
            this.lblTitlu.TabIndex = 1;
            this.lblTitlu.Text = "Titlu:";
            // 
            // txtTitlu
            // 
            this.txtTitlu.Location = new System.Drawing.Point(150, 67);
            this.txtTitlu.Name = "txtTitlu";
            this.txtTitlu.Size = new System.Drawing.Size(250, 23);
            this.txtTitlu.TabIndex = 2;
            // 
            // lblArtist
            // 
            this.lblArtist.AutoSize = true;
            this.lblArtist.Location = new System.Drawing.Point(27, 110);
            this.lblArtist.Name = "lblArtist";
            this.lblArtist.Size = new System.Drawing.Size(42, 15);
            this.lblArtist.TabIndex = 3;
            this.lblArtist.Text = "Artist:";
            // 
            // txtArtist
            // 
            this.txtArtist.Location = new System.Drawing.Point(150, 107);
            this.txtArtist.Name = "txtArtist";
            this.txtArtist.Size = new System.Drawing.Size(250, 23);
            this.txtArtist.TabIndex = 4;
            // 
            // lblGenMuzical
            // 
            this.lblGenMuzical.AutoSize = true;
            this.lblGenMuzical.Location = new System.Drawing.Point(27, 150);
            this.lblGenMuzical.Name = "lblGenMuzical";
            this.lblGenMuzical.Size = new System.Drawing.Size(79, 15);
            this.lblGenMuzical.TabIndex = 5;
            this.lblGenMuzical.Text = "Gen Muzical:";
            // 
            // txtGenMuzical
            // 
            this.txtGenMuzical.Location = new System.Drawing.Point(150, 147);
            this.txtGenMuzical.Name = "txtGenMuzical";
            this.txtGenMuzical.Size = new System.Drawing.Size(250, 23);
            this.txtGenMuzical.TabIndex = 6;
            // 
            // lblAnLansare
            // 
            this.lblAnLansare.AutoSize = true;
            this.lblAnLansare.Location = new System.Drawing.Point(27, 190);
            this.lblAnLansare.Name = "lblAnLansare";
            this.lblAnLansare.Size = new System.Drawing.Size(70, 15);
            this.lblAnLansare.TabIndex = 7;
            this.lblAnLansare.Text = "An Lansare:";
            // 
            // txtAnLansare
            // 
            this.txtAnLansare.Location = new System.Drawing.Point(150, 187);
            this.txtAnLansare.Name = "txtAnLansare";
            this.txtAnLansare.Size = new System.Drawing.Size(100, 23);
            this.txtAnLansare.TabIndex = 8;
            // 
            // btnSalveaza
            // 
            this.btnSalveaza.Location = new System.Drawing.Point(150, 230);
            this.btnSalveaza.Name = "btnSalveaza";
            this.btnSalveaza.Size = new System.Drawing.Size(120, 35);
            this.btnSalveaza.TabIndex = 9;
            this.btnSalveaza.Text = "Salvează Modificări";
            this.btnSalveaza.UseVisualStyleBackColor = true;
            this.btnSalveaza.Click += new System.EventHandler(this.btnSalveaza_Click);
            // 
            // btnAnuleaza
            // 
            this.btnAnuleaza.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnuleaza.Location = new System.Drawing.Point(280, 230);
            this.btnAnuleaza.Name = "btnAnuleaza";
            this.btnAnuleaza.Size = new System.Drawing.Size(120, 35);
            this.btnAnuleaza.TabIndex = 10;
            this.btnAnuleaza.Text = "Anulează";
            this.btnAnuleaza.UseVisualStyleBackColor = true;
            this.btnAnuleaza.Click += new System.EventHandler(this.btnAnuleaza_Click);
            // 
            // EditMelodieForm
            // 
            this.AcceptButton = this.btnSalveaza;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnAnuleaza;
            this.ClientSize = new System.Drawing.Size(434, 291); // Adjusted size
            this.Controls.Add(this.btnAnuleaza);
            this.Controls.Add(this.btnSalveaza);
            this.Controls.Add(this.txtAnLansare);
            this.Controls.Add(this.lblAnLansare);
            this.Controls.Add(this.txtGenMuzical);
            this.Controls.Add(this.lblGenMuzical);
            this.Controls.Add(this.txtArtist);
            this.Controls.Add(this.lblArtist);
            this.Controls.Add(this.txtTitlu);
            this.Controls.Add(this.lblTitlu);
            this.Controls.Add(this.lblTitluForm);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditMelodieForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modifică Melodie";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
} 