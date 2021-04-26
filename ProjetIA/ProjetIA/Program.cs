using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetIA
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        /// 
        static void uiThread(GameForm ui)
        {
            Application.Run(ui);

        }

        static void agentThread(Agent agent)
        {
            while(true)
            {
                if(agent._beliefs.environment.turn==playerTurn.playerYellow)
                {
                    //Console.WriteLine("call to agentRoutine");
                    agent.agentRoutine();
                }
                else
                {
                    //Console.WriteLine("truc");
                }
            }
        }

        [STAThread]
        static void Main()
        {

            Environment env = new Environment();
            //Captor cap = new Captor(env);
            Agent agent = new Agent(env);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GameForm ui = new GameForm(env);
            env.setUI(ui);
            Thread Thread1 = new Thread(() => uiThread(ui));
            Thread Thread2 = new Thread(()=>agentThread(agent));
            Thread1.Start();
            Thread2.Start();

            Console.WriteLine("truc");
        }
    }
}