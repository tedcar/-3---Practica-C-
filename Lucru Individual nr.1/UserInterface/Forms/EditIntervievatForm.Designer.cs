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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new Label();
            this.lblNume = new Label();
            this.txtNume = new TextBox();
            this.lblVarsta = new Label();
            this.nudVarsta = new NumericUpDown();
            this.lblLocalitate = new Label();
            this.txtLocalitate = new TextBox();
            this.btnSalveaza = new Button();
            this.btnAnuleaza = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudVarsta)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(188, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Editare Intervievat";
            // 
            // lblNume
            // 
            this.lblNume.AutoSize = true;
            this.lblNume.Location = new System.Drawing.Point(14, 54);
            this.lblNume.Name = "lblNume";
            this.lblNume.Size = new System.Drawing.Size(96, 15);
            this.lblNume.TabIndex = 1;
            this.lblNume.Text = "Nume Complet:";
            // 
            // txtNume
            // 
            this.txtNume.Location = new System.Drawing.Point(17, 72);
            this.txtNume.Name = "txtNume";
            this.txtNume.Size = new System.Drawing.Size(330, 23);
            this.txtNume.TabIndex = 0;
            // 
            // lblVarsta
            // 
            this.lblVarsta.AutoSize = true;
            this.lblVarsta.Location = new System.Drawing.Point(14, 108);
            this.lblVarsta.Name = "lblVarsta";
            this.lblVarsta.Size = new System.Drawing.Size(43, 15);
            this.lblVarsta.TabIndex = 1;
            this.lblVarsta.Text = "Vârstă:";
            // 
            // nudVarsta
            // 
            this.nudVarsta.Location = new System.Drawing.Point(17, 126);
            this.nudVarsta.Minimum = new decimal(new int[] {1,0,0,0});
            this.nudVarsta.Maximum = new decimal(new int[] {120,0,0,0});
            this.nudVarsta.Name = "nudVarsta";
            this.nudVarsta.Size = new System.Drawing.Size(120, 23);
            this.nudVarsta.TabIndex = 1;
            // 
            // lblLocalitate
            // 
            this.lblLocalitate.AutoSize = true;
            this.lblLocalitate.Location = new System.Drawing.Point(14, 162);
            this.lblLocalitate.Name = "lblLocalitate";
            this.lblLocalitate.Size = new System.Drawing.Size(67, 15);
            this.lblLocalitate.TabIndex = 1;
            this.lblLocalitate.Text = "Localitate:";
            // 
            // txtLocalitate
            // 
            this.txtLocalitate.Location = new System.Drawing.Point(17, 180);
            this.txtLocalitate.Name = "txtLocalitate";
            this.txtLocalitate.Size = new System.Drawing.Size(330, 23);
            this.txtLocalitate.TabIndex = 2;
            // 
            // btnSalveaza
            // 
            this.btnSalveaza.Location = new System.Drawing.Point(191, 223);
            this.btnSalveaza.Name = "btnSalveaza";
            this.btnSalveaza.Size = new System.Drawing.Size(75, 30);
            this.btnSalveaza.TabIndex = 3;
            this.btnSalveaza.Text = "Salvează";
            this.btnSalveaza.UseVisualStyleBackColor = true;
            this.btnSalveaza.Click += new System.EventHandler(this.btnSalveaza_Click);
            // 
            // btnAnuleaza
            // 
            this.btnAnuleaza.Location = new System.Drawing.Point(272, 223);
            this.btnAnuleaza.Name = "btnAnuleaza";
            this.btnAnuleaza.Size = new System.Drawing.Size(75, 30);
            this.btnAnuleaza.TabIndex = 4;
            this.btnAnuleaza.Text = "Anulează";
            this.btnAnuleaza.UseVisualStyleBackColor = true;
            this.btnAnuleaza.Click += new System.EventHandler(this.btnAnuleaza_Click);
            // 
            // EditIntervievatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 268);
            this.Controls.Add(this.btnAnuleaza);
            this.Controls.Add(this.btnSalveaza);
            this.Controls.Add(this.txtLocalitate);
            this.Controls.Add(this.lblLocalitate);
            this.Controls.Add(this.nudVarsta);
            this.Controls.Add(this.lblVarsta);
            this.Controls.Add(this.txtNume);
            this.Controls.Add(this.lblNume);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Name = "EditIntervievatForm";
            ((System.ComponentModel.ISupportInitialize)(this.nudVarsta)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
