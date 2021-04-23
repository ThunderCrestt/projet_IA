using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetIA
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Environment env = new Environment();
            Captor cap = new Captor(env);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GameForm ui = new GameForm(env);
            env.setUI(ui);
            Application.Run(ui);
        }
    }
}
