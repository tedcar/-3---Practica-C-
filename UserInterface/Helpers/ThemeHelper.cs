using System.Drawing;
using System.Windows.Forms;

namespace MelodiiApp.UserInterface.Helpers
{
    /// <summary>
    /// Clasă statică helper pentru aplicarea unei teme vizuale consistente în întreaga aplicație.
    /// Se concentrează pe un stil asemănător MaterialSkin Dark.
    /// </summary>
    public static class ThemeHelper
    {
        // --- Paletă de culori actualizată pentru tema DARK MaterialSkin ---
        // Suprafață temă MaterialSkin Dark: #303030
        // Culoare primară MaterialSkin (BlueGrey800): #37474F
        // Culoare de accent MaterialSkin (LightBlue200): #81D4FA (aproximativ)
        // Nuanță text MaterialSkin (WHITE): Alb

        /// <summary> Culoare de fundal standard pentru controale în tema dark. </summary>
        public static Color ControlDarkBackground { get; } = ColorTranslator.FromHtml("#FF303030"); 
        /// <summary> Culoare text pe fundaluri închise (de obicei alb). </summary>
        public static Color TextColorOnDark { get; } = Color.White;
        /// <summary> Culoare text (negru) pentru butoane cu fundal deschis de accent. </summary>
        public static Color TextColorOnLightAccent { get; } = ColorTranslator.FromHtml("#FF000000");

        // Culori primare și de accent din MaterialSkin pentru referință sau utilizare directă dacă este necesar pentru controale standard.
        /// <summary> Culoare primară MaterialSkin - BlueGrey800. </summary>
        public static Color MatPrimaryBlueGrey800 { get; } = ColorTranslator.FromHtml("#FF37474F");
        /// <summary> Culoare de accent MaterialSkin - LightBlue200. </summary>
        public static Color MatAccentLightBlue200 { get; } = ColorTranslator.FromHtml("#FF81D4FA");

        // Elemente UI specifice - de revizuit și armonizat
        /// <summary> Culoare albastru închis (Moștenit - Revizuire: Posibil de înlocuit cu MatPrimary). </summary>
        public static Color DarkBlue { get; } = ColorTranslator.FromHtml("#003049"); 
        /// <summary> Culoare albastru mediu (Moștenit - Revizuire: Posibil de înlocuit cu MatAccent sau o nuanță). </summary>
        public static Color MidBlue { get; } = ColorTranslator.FromHtml("#669bbc");  
        /// <summary> Culoare crem (Moștenit - OBSOLET pentru fundalul temei dark). </summary>
        public static Color Cream { get; } = ColorTranslator.FromHtml("#fdf0d5"); 

        /// <summary> Culoare roșu de accent, utilizată pentru erori. </summary>
        public static Color RedAccent { get; } = ColorTranslator.FromHtml("#c1121f"); 
        /// <summary> Culoare roșu închis de accent, utilizată pentru erori. </summary>
        public static Color DarkRedAccent { get; } = ColorTranslator.FromHtml("#780000"); 
        /// <summary> Culoare utilizată pentru a indica succesul unei operațiuni. </summary>
        public static Color SuccessColor { get; } = ColorTranslator.FromHtml("#28a745");

        // Utilizare directă OBSOLETĂ, folosiți TextColorOnDark sau TextColorOnLightAccent
        // public static Color TextOnLight { get; } = DarkBlue;
        // public static Color TextOnDark { get; } = Cream;

        /// <summary> Font standard pentru butoane. </summary>
        public static Font ButtonFont { get; } = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        /// <summary> Font standard pentru etichete. </summary>
        public static Font LabelFont { get; } = new Font("Segoe UI", 10F);
        /// <summary> Font standard pentru casete de text. </summary>
        public static Font TextBoxFont { get; } = new Font("Segoe UI", 10F);

        /// <summary>
        /// Aplică tema generală unui UserControl și tuturor controalelor sale copil.
        /// </summary>
        /// <param name="control">UserControl-ul căruia i se aplică tema.</param>
        public static void ApplyUserControlTheme(UserControl control)
        {
            control.BackColor = ControlDarkBackground; 
            control.Font = LabelFont; 

            foreach (Control ctrl in control.Controls)
            {
                ApplyThemeToControl(ctrl);
            }
        }

        /// <summary>
        /// Aplică tema generală unui Form și tuturor controalelor sale copil.
        /// </summary>
        /// <param name="form">Formularul căruia i se aplică tema.</param>
        public static void ApplyFormTheme(Form form)
        {
            form.BackColor = ControlDarkBackground; 
            form.Font = LabelFont; 

            foreach (Control ctrl in form.Controls)
            {
                ApplyThemeToControl(ctrl);
            }
        }

        /// <summary>
        /// Aplică tema unui control individual, în funcție de tipul său.
        /// Se aplică recursiv pentru controalele container.
        /// </summary>
        /// <param name="ctrl">Controlul căruia i se aplică tema.</param>
        public static void ApplyThemeToControl(Control ctrl)
        {
            if (ctrl is Button btn) ApplyButtonStyle(btn);
            else if (ctrl is Label lbl) ApplyLabelStyle(lbl);
            else if (ctrl is TextBox txt) ApplyTextBoxStyle(txt);
            else if (ctrl is NumericUpDown nud) ApplyNumericUpDownStyle(nud);
            else if (ctrl is DataGridView dgv) ApplyDataGridViewStyle(dgv);
            else if (ctrl is ComboBox cmb) ApplyComboBoxStyle(cmb);
            // Aplică recursiv controalelor copil pentru containere (Panel, GroupBox etc.)
            else if (ctrl.HasChildren)
            {
                // Pentru Panel sau GroupBox, setează și fundalul dacă nu sunt transparente implicit.
                if (ctrl is Panel || ctrl is GroupBox)
                {
                    ctrl.BackColor = ControlDarkBackground; // Sau Color.Transparent dacă ar trebui să se amestece
                }
                foreach(Control childCtrl in ctrl.Controls)
                {
                    ApplyThemeToControl(childCtrl);
                }
            }
        }

        /// <summary>
        /// Aplică stilul specific temei unui buton.
        /// </summary>
        /// <param name="button">Butonul de stilizat.</param>
        public static void ApplyButtonStyle(Button button)
        {
            button.BackColor = MatAccentLightBlue200; // Utilizează accentul Material
            button.ForeColor = TextColorOnLightAccent; // Text negru pe albastru deschis
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = ButtonFont;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.MinimumSize = new Size(100, 35);

            Color originalBackColor = button.BackColor;
            // Efect subtil de hover: întunecă ușor culoarea de accent sau folosește o bordură
            Color hoverBackColor = ControlPaint.Dark(originalBackColor, 0.1f); 
            Color downBackColor = ControlPaint.Dark(originalBackColor, 0.2f);

            button.FlatAppearance.MouseOverBackColor = hoverBackColor;
            button.MouseEnter += (sender, e) => { ((Button)sender).BackColor = hoverBackColor; };
            button.MouseLeave += (sender, e) => { ((Button)sender).BackColor = originalBackColor; };
            button.MouseDown += (sender, e) => { ((Button)sender).BackColor = downBackColor; };
            button.MouseUp += (sender, e) => { ((Button)sender).BackColor = hoverBackColor; }; // Sau originalBackColor dacă se preferă
        }

        /// <summary>
        /// Aplică stilul specific temei unei etichete (Label).
        /// </summary>
        /// <param name="label">Eticheta de stilizat.</param>
        public static void ApplyLabelStyle(Label label)
        {
            label.ForeColor = TextColorOnDark; // Text alb pe fundal închis
            label.Font = LabelFont;
            label.BackColor = Color.Transparent; 
        }

        /// <summary>
        /// Aplică stilul specific temei unei etichete de tip ToolStripStatusLabel.
        /// </summary>
        /// <param name="toolStripLabel">Eticheta ToolStripStatusLabel de stilizat.</param>
        public static void ApplyLabelStyle(ToolStripStatusLabel toolStripLabel) 
        {
            toolStripLabel.ForeColor = TextColorOnDark; // Text alb pe fundal închis
            toolStripLabel.Font = LabelFont;
            toolStripLabel.BackColor = Color.Transparent; 
        }

        /// <summary>
        /// Aplică stilul specific temei unei casete de text (TextBox).
        /// </summary>
        /// <param name="textBox">Caseta de text de stilizat.</param>
        public static void ApplyTextBoxStyle(TextBox textBox)
        {
            // Pentru teme dark, casetele de text au adesea un fundal închis puțin mai deschis decât suprafața principală
            textBox.BackColor = ControlPaint.Light(ControlDarkBackground, 0.2f); // Nuanță mai deschisă a fundalului dark
            textBox.ForeColor = TextColorOnDark; // Text alb
            textBox.Font = TextBoxFont;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            // Se poate considera o culoare de bordură care se potrivește temei dark, ex: un gri mai deschis sau accent
            // textBox.BorderColor = Color.Gray; // Exemplu, dacă se folosește un TextBox personalizat ce suportă BorderColor
        }

        /// <summary>
        /// Aplică stilul specific temei unui control NumericUpDown.
        /// </summary>
        /// <param name="numericUpDown">Controlul NumericUpDown de stilizat.</param>
        public static void ApplyNumericUpDownStyle(NumericUpDown numericUpDown)
        {
            numericUpDown.BackColor = ControlPaint.Light(ControlDarkBackground, 0.2f); // Nuanță mai deschisă a fundalului dark
            numericUpDown.ForeColor = TextColorOnDark; // Text alb
            numericUpDown.Font = TextBoxFont; 
            numericUpDown.BorderStyle = BorderStyle.FixedSingle;
        }
        
        /// <summary>
        /// Aplică stilul specific temei unui control ComboBox.
        /// </summary>
        /// <param name="comboBox">Controlul ComboBox de stilizat.</param>
        public static void ApplyComboBoxStyle(ComboBox comboBox)
        {
            comboBox.BackColor = ControlPaint.Light(ControlDarkBackground, 0.2f); // Nuanță mai deschisă a fundalului dark
            comboBox.ForeColor = TextColorOnDark; // Text alb (se aplică zonei de text)
            comboBox.Font = TextBoxFont;
            comboBox.FlatStyle = FlatStyle.Flat; 
            // Stilizarea listei dropdown în sine este mai complexă și adesea dependentă de SO pentru ComboBox standard
        }

        /// <summary>
        /// Aplică stilul specific temei unui control DataGridView.
        /// </summary>
        /// <param name="dgv">Controlul DataGridView de stilizat.</param>
        public static void ApplyDataGridViewStyle(DataGridView dgv)
        {
            // Stil General
            dgv.BackgroundColor = ControlDarkBackground; 
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = MatPrimaryBlueGrey800; // Linii de grilă folosind o culoare Material
            dgv.ForeColor = TextColorOnDark;       // Culoare text implicită pentru celule (nu întotdeauna respectată, stilul celulei adesea suprascrie)

            // Stil Antet Coloane
            dgv.ColumnHeadersDefaultCellStyle.BackColor = MatPrimaryBlueGrey800; // Antet mai închis
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextColorOnDark;     // Text deschis pe antet
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single; // Sau None
            dgv.EnableHeadersVisualStyles = false; // Necesar pentru ca stilurile personalizate ale antetului să aibă efect

            // Stil Rânduri
            dgv.RowsDefaultCellStyle.BackColor = ControlPaint.Light(ControlDarkBackground, 0.1f); // Puțin mai deschis pentru rânduri
            dgv.RowsDefaultCellStyle.ForeColor = TextColorOnDark;     // Text deschis
            dgv.RowsDefaultCellStyle.SelectionBackColor = MatAccentLightBlue200; // Culoare de accent pentru selecție
            dgv.RowsDefaultCellStyle.SelectionForeColor = TextColorOnLightAccent; // Text negru pe selecția cu accent deschis

            // Rânduri Alternate - diferență foarte subtilă pentru teme dark
            dgv.AlternatingRowsDefaultCellStyle.BackColor = ControlDarkBackground; 
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = TextColorOnDark;

            // Stil Celulă
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            
            // Setări comportament DataGridView
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true; 
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// Stilează o etichetă (Label) specifică cu o anumită culoare de text și opțional un font.
        /// </summary>
        /// <param name="label">Eticheta de stilizat.</param>
        /// <param name="foreColor">Culoarea textului.</param>
        /// <param name="font">Fontul (opțional).</param>
        public static void StyleSpecificLabel(Label label, Color foreColor, Font font = null)
        {
            label.ForeColor = foreColor;
            if (font != null)
            {
                label.Font = font;
            }
            label.BackColor = Color.Transparent;
        }

        /// <summary>
        /// Stilează o etichetă (ToolStripStatusLabel) specifică cu o anumită culoare de text și opțional un font.
        /// </summary>
        /// <param name="toolStripLabel">Eticheta ToolStripStatusLabel de stilizat.</param>
        /// <param name="foreColor">Culoarea textului.</param>
        /// <param name="font">Fontul (opțional).</param>
        public static void StyleSpecificLabel(ToolStripStatusLabel toolStripLabel, Color foreColor, Font font = null)
        {
            toolStripLabel.ForeColor = foreColor;
            if (font != null)
            {
                toolStripLabel.Font = font;
            }
            toolStripLabel.BackColor = Color.Transparent;
        }
    }
} 