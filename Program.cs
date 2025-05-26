using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MelodiiApp.UserInterface.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace MelodiiApp
{
    static class Program
    {
        /// <summary>
        /// Punctul principal de intrare pentru aplicație.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Inițializează MaterialSkinManager pentru întreaga aplicație
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.BlueGrey800,   // Culoare primară
                Primary.BlueGrey900,   // Culoare primară închisă
                Primary.BlueGrey700,   // Culoare primară deschisă
                Accent.LightBlue200,   // Culoare de accent
                TextShade.WHITE        // Nuanță text
            );

            Application.Run(new LoginForm()); // Rulează formularul de autentificare ca punct de start.
        }
    }
}
