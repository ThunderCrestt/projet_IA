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

            List<Case> test = cap.GetPawns(caseState.empty);

            for (int i = 0; i<test.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(test[i]);
            }
            System.Diagnostics.Debug.WriteLine(test.Count);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
