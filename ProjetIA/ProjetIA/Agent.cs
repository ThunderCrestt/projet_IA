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
            public Environment environment;
        }

        private Beliefs _beliefs;
        Captor captor;
        Effector effector;

        public void agentRoutine()
        {
            //alphaBeta
        }

        //implémentation de negaMax, une amélioration de l'élagage aplha Beta
        public int negaMax(playerTurn turn, int alpha, int beta)
        {
            //Assert(alpha  <  beta, "error : beta > aplha");

            if (_beliefs.environment.nbMovePlayed == _beliefs.environment.Height * _beliefs.environment.Width)
            {
                return 0;
            }
                
            //check if draw game with nbMoves=grid.Count*grid[0].Count

            //check on all col if there is a winning move and return a highValue;
            for (int i = 0; i < this._beliefs.environment.Width; i++)
            {
                if(this.captor.canPlay(i) && this.captor.isAWinningMove(i, turn))
                {
                    return  (this._beliefs.environment.Height * this._beliefs.environment.Width + 1 - this._beliefs.environment.nbMovePlayed);
                }
            }

            int max = this._beliefs.environment.Height * this._beliefs.environment.Width - 1 - this._beliefs.environment.nbMovePlayed;

            if (beta > max)
            {
                beta = max;
                if (alpha >= beta)
                {
                    return beta;
                }
            }



            for (int x = 0; x < this._beliefs.environment.Width; x++) // compute the score of all possible next move and keep the best one
            {
                if (this.captor.canPlay(x))
                {
                    //Position P2(P);
                    //P2.play(x);

                    int score = -negaMax(playerTurn.playerYellow, -beta, -alpha); // explore opponent's score within [-beta;-alpha] windows:
                                                                                  // no need to have good precision for score better than beta (opponent's score worse than -beta)
                                                                                  // no need to check for score worse than alpha (opponent's score worse better than -alpha)
                    if (score >= beta) return score;  // prune the exploration if we find a possible move better than what we were looking for.
                    if (score > alpha) alpha = score; // reduce the [alpha;beta] window for next exploration, as we only 
                }
            }
            return alpha;
        }

    }
}
