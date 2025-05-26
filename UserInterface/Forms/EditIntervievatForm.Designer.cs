using System.Windows.Forms;

namespace MelodiiApp.UserInterface.Forms
{
    partial class EditIntervievatForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblNume;
        private TextBox txtNume;
        private Label lblVarsta;
        private NumericUpDown nudVarsta;
        private Label lblLocalitate;
        private TextBox txtLocalitate;
        private Button btnSalveaza;
        private Button btnAnuleaza;

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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblNume = new System.Windows.Forms.Label();
            this.txtNume = new System.Windows.Forms.TextBox();
            this.lblVarsta = new System.Windows.Forms.Label();
            this.nudVarsta = new System.Windows.Forms.NumericUpDown();
            this.lblLocalitate = new System.Windows.Forms.Label();
            this.txtLocalitate = new System.Windows.Forms.TextBox();
            this.btnSalveaza = new System.Windows.Forms.Button();
            this.btnAnuleaza = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudVarsta)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(10, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(152, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Editare Intervievat";
            // 
            // lblNume
            // 
            this.lblNume.AutoSize = true;
            this.lblNume.Location = new System.Drawing.Point(12, 47);
            this.lblNume.Name = "lblNume";
            this.lblNume.Size = new System.Drawing.Size(79, 13);
            this.lblNume.TabIndex = 1;
            this.lblNume.Text = "Nume Complet:";
            // 
            // txtNume
            // 
            this.txtNume.Location = new System.Drawing.Point(15, 62);
            this.txtNume.Name = "txtNume";
            this.txtNume.Size = new System.Drawing.Size(283, 20);
            this.txtNume.TabIndex = 0;
            // 
            // lblVarsta
            // 
            this.lblVarsta.AutoSize = true;
            this.lblVarsta.Location = new System.Drawing.Point(12, 94);
            this.lblVarsta.Name = "lblVarsta";
            this.lblVarsta.Size = new System.Drawing.Size(40, 13);
            this.lblVarsta.TabIndex = 1;
            this.lblVarsta.Text = "Vârstă:";
            // 
            // nudVarsta
            // 
            this.nudVarsta.Location = new System.Drawing.Point(15, 109);
            this.nudVarsta.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.nudVarsta.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudVarsta.Name = "nudVarsta";
            this.nudVarsta.Size = new System.Drawing.Size(103, 20);
            this.nudVarsta.TabIndex = 1;
            this.nudVarsta.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblLocalitate
            // 
            this.lblLocalitate.AutoSize = true;
            this.lblLocalitate.Location = new System.Drawing.Point(12, 140);
            this.lblLocalitate.Name = "lblLocalitate";
            this.lblLocalitate.Size = new System.Drawing.Size(56, 13);
            this.lblLocalitate.TabIndex = 1;
            this.lblLocalitate.Text = "Localitate:";
            // 
            // txtLocalitate
            // 
            this.txtLocalitate.Location = new System.Drawing.Point(15, 156);
            this.txtLocalitate.Name = "txtLocalitate";
            this.txtLocalitate.Size = new System.Drawing.Size(283, 20);
            this.txtLocalitate.TabIndex = 2;
            // 
            // btnSalveaza
            // 
            this.btnSalveaza.Location = new System.Drawing.Point(164, 193);
            this.btnSalveaza.Name = "btnSalveaza";
            this.btnSalveaza.Size = new System.Drawing.Size(64, 26);
            this.btnSalveaza.TabIndex = 3;
            this.btnSalveaza.Text = "Salvează";
            this.btnSalveaza.UseVisualStyleBackColor = true;
            this.btnSalveaza.Click += new System.EventHandler(this.btnSalveaza_Click);
            // 
            // btnAnuleaza
            // 
            this.btnAnuleaza.Location = new System.Drawing.Point(233, 193);
            this.btnAnuleaza.Name = "btnAnuleaza";
            this.btnAnuleaza.Size = new System.Drawing.Size(64, 26);
            this.btnAnuleaza.TabIndex = 4;
            this.btnAnuleaza.Text = "Anulează";
            this.btnAnuleaza.UseVisualStyleBackColor = true;
            this.btnAnuleaza.Click += new System.EventHandler(this.btnAnuleaza_Click);
            // 
            // EditIntervievatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 232);
            this.Controls.Add(this.btnAnuleaza);
            this.Controls.Add(this.btnSalveaza);
            this.Controls.Add(this.txtLocalitate);
            this.Controls.Add(this.lblLocalitate);
            this.Controls.Add(this.nudVarsta);
            this.Controls.Add(this.lblVarsta);
            this.Controls.Add(this.txtNume);
            this.Controls.Add(this.lblNume);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditIntervievatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.EditIntervievatForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudVarsta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
