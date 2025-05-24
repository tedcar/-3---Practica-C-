using System.Drawing;
using System.Windows.Forms;

namespace MelodiiApp.UserInterface.Helpers
{
    public static class ThemeHelper
    {
        // Color Palette
        public static Color DarkBlue { get; } = ColorTranslator.FromHtml("#003049");
        public static Color MidBlue { get; } = ColorTranslator.FromHtml("#669bbc");
        public static Color Cream { get; } = ColorTranslator.FromHtml("#fdf0d5");
        public static Color RedAccent { get; } = ColorTranslator.FromHtml("#c1121f");
        public static Color DarkRedAccent { get; } = ColorTranslator.FromHtml("#780000");
        public static Color TextOnLight { get; } = DarkBlue; // Or Color.Black
        public static Color TextOnDark { get; } = Cream;    // Or Color.White

        public static Font ButtonFont { get; } = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
        public static Font LabelFont { get; } = new Font("Segoe UI", 10F);
        public static Font TextBoxFont { get; } = new Font("Segoe UI", 10F);

        public static void ApplyUserControlTheme(UserControl control)
        {
            control.BackColor = Cream;
            control.Font = LabelFont; // Default font for the control

            foreach (Control ctrl in control.Controls)
            {
                ApplyThemeToControl(ctrl);
            }
        }

        public static void ApplyFormTheme(Form form)
        {
            form.BackColor = Cream;
            form.Font = LabelFont; // Default font for the form

            foreach (Control ctrl in form.Controls)
            {
                ApplyThemeToControl(ctrl);
            }
        }

        public static void ApplyThemeToControl(Control ctrl)
        {
            if (ctrl is Button btn) ApplyButtonStyle(btn);
            else if (ctrl is Label lbl) ApplyLabelStyle(lbl);
            else if (ctrl is TextBox txt) ApplyTextBoxStyle(txt);
            else if (ctrl is NumericUpDown nud) ApplyNumericUpDownStyle(nud);
            else if (ctrl is DataGridView dgv) ApplyDataGridViewStyle(dgv);
            else if (ctrl is ComboBox cmb) ApplyComboBoxStyle(cmb);
            // Apply to child controls recursively for panels, groupboxes etc.
            else if (ctrl.HasChildren)
            {
                foreach(Control childCtrl in ctrl.Controls)
                {
                    ApplyThemeToControl(childCtrl);
                }
            }
        }

        public static void ApplyButtonStyle(Button button)
        {
            button.BackColor = MidBlue;
            button.ForeColor = TextOnDark;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = ButtonFont;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.MinimumSize = new Size(100, 35); // Basic min size

            // Hover effect - make it more subtle by adding a border
            Color originalBackColor = button.BackColor;
            button.FlatAppearance.MouseOverBackColor = originalBackColor; // Keep back color same on hover initially

            button.MouseEnter += (sender, e) => 
            {
                Button btn = (Button)sender;
                btn.FlatAppearance.BorderSize = 1; // Show border
                btn.FlatAppearance.BorderColor = DarkBlue; // Use DarkBlue for border
                // Optionally, slightly change BackColor if still desired
                // btn.BackColor = Color.FromArgb(Math.Min(255, originalBackColor.R + 10), Math.Min(255, originalBackColor.G + 10), Math.Min(255, originalBackColor.B + 10)); 
            };
            button.MouseLeave += (sender, e) => 
            {
                Button btn = (Button)sender;
                btn.FlatAppearance.BorderSize = 0; // Hide border
                btn.BackColor = originalBackColor; // Ensure it reverts fully
            };
        }

        public static void ApplyLabelStyle(Label label)
        {
            label.ForeColor = TextOnLight;
            label.Font = LabelFont;
            label.BackColor = Color.Transparent; // Ensure labels on cream bg are transparent
        }

        public static void ApplyLabelStyle(ToolStripStatusLabel toolStripLabel) // Overload for ToolStripStatusLabel
        {
            toolStripLabel.ForeColor = TextOnLight;
            toolStripLabel.Font = LabelFont;
            toolStripLabel.BackColor = Color.Transparent; // Or specific strip color if needed
        }

        public static void ApplyTextBoxStyle(TextBox textBox)
        {
            textBox.BackColor = Color.White;
            textBox.ForeColor = TextOnLight;
            textBox.Font = TextBoxFont;
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        public static void ApplyNumericUpDownStyle(NumericUpDown numericUpDown)
        {
            numericUpDown.BackColor = Color.White;
            numericUpDown.ForeColor = TextOnLight;
            numericUpDown.Font = TextBoxFont; // Consistent with TextBox
            numericUpDown.BorderStyle = BorderStyle.FixedSingle;
        }
        
        public static void ApplyComboBoxStyle(ComboBox comboBox)
        {
            comboBox.BackColor = Color.White;
            comboBox.ForeColor = TextOnLight;
            comboBox.Font = TextBoxFont;
            comboBox.FlatStyle = FlatStyle.Flat; // Modern look
        }

        public static void ApplyDataGridViewStyle(DataGridView dgv)
        {
            // General Style
            dgv.BackgroundColor = Cream;
            dgv.BorderStyle = BorderStyle.None;
            dgv.GridColor = MidBlue; // Grid lines
            dgv.ForeColor = TextOnLight; // Default text color for cells

            // Column Header Style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = DarkBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = TextOnDark;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.EnableHeadersVisualStyles = false; // Important to apply custom styles

            // Row Style
            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            dgv.RowsDefaultCellStyle.ForeColor = TextOnLight;
            dgv.RowsDefaultCellStyle.SelectionBackColor = MidBlue;
            dgv.RowsDefaultCellStyle.SelectionForeColor = TextOnDark;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#f0f8ff"); // AliceBlue, a very light blue/cream alternative

            // Cell Style
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            
            // Remove row headers (the leftmost column for selecting entire rows) if not needed
            // dgv.RowHeadersVisible = false;

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true; // Common for display grids, can be overridden
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public static void StyleSpecificLabel(Label label, Color foreColor, Font font = null)
        {
            label.ForeColor = foreColor;
            if (font != null)
            {
                label.Font = font;
            }
            label.BackColor = Color.Transparent;
        }

        public static void StyleSpecificLabel(ToolStripStatusLabel toolStripLabel, Color foreColor, Font font = null) // Overload for ToolStripStatusLabel
        {
            toolStripLabel.ForeColor = foreColor;
            if (font != null)
            {
                toolStripLabel.Font = font;
            }
            toolStripLabel.BackColor = Color.Transparent; // Or specific strip color if needed
        }
    }
} 