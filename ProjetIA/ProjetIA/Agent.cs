using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIA
{
    class Agent
    {
        public Agent(Environment env)
        {
            captor = new Captor(env);
            effector = new Effector(env);
            _beliefs = new Beliefs();
            this._beliefs.environment = env;
        }
        

        private struct Beliefs{
            public List<Case> caseWithYellowPawn;
            public List<Case> caseWithRedPawn;
            public Environment environment;
        }

        private Beliefs _beliefs;
        Captor captor;
        Effector effector;

        public void updateBeliefs()
        {
            _beliefs.caseWithRedPawn = captor.GetPawns(caseState.red);
            _beliefs.caseWithYellowPawn = captor.GetPawns(caseState.yellow);
        }

        public void agentRoutine()
        {
            updateBeliefs();
            //alphaBeta
        }

        //implémentation de negaMax, une amélioration de l'élagage aplha Beta
        public int negaMax(Case c, int alpha, int beta)
        {
            //check if draw game with nbMoves=grid.Count*grid[0].Count

            //check on all col if there is a winning move and return a highValue;

            //

            return 0;
        }

    }
}
