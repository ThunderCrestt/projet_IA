using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace ProjetIA
{
    class Agent
    {
        public Agent(Environnement env)
        {
            captor = new Captor(env);
            effector = new Effector(env);
            _beliefs = new Beliefs();
            this._beliefs.environment = env;
        }
        

        public struct Beliefs{
            public Environnement environment;
        }

        public Beliefs _beliefs;
        public List<int> intentions=new List<int>();
        Captor captor;
        public Effector effector;

        int maxDepth = 6;
        public void agentRoutine()
        {
            if (this._beliefs.environment.IsTurnAgent)
            {
                //alphaBeta
                int bestMove = 3;
                negaMax(playerTurn.playerYellow, -this._beliefs.environment.Width * this._beliefs.environment.Height / 2, this._beliefs.environment.Width * this._beliefs.environment.Height / 2, ref bestMove,maxDepth);
                intentions.Add(bestMove);
                this.effector.PutPawn(caseState.yellow, intentions[intentions.Count-1]);
                if(this._beliefs.environment.getWinner(playerTurn.playerYellow)==playerTurn.playerYellow)
                {
                    this._beliefs.environment.restartGame(playerTurn.playerYellow);
                }
                this._beliefs.environment.IsTurnAgent = false;
            }
        }

        //implémentation de negaMax, une amélioration de min max avec de l'élagage aplha Beta
        public int negaMax(playerTurn turn, int alpha, int beta,ref int bestColToPlay,int depth)
        {
            int bestScore = -int.MaxValue;
            if (_beliefs.environment.nbMovePlayed == _beliefs.environment.Height * _beliefs.environment.Width)
            {
                return 0;
            }


            //check on all col if there is a winning move and return a highValue;
            for (int i = 0; i < this._beliefs.environment.Width; i++)
            {
                if (depth == maxDepth)
                {
                    if (this.captor.canPlay(i) && this.captor.isAWinningMove(i, turn))
                    {
                        bestColToPlay = i;
                        return (1000);
                    }
                }
            }

            if (depth <= 0)
            {
                bestScore = eval(turn);
                return bestScore;
            }
            else
            {
                /*
                int max = this._beliefs.environment.Height * this._beliefs.environment.Width - 1 - this._beliefs.environment.nbMovePlayed;

                if (beta > max)
                {
                    beta = max;
                    if (alpha >= beta)
                    {
                        return beta;
                    }
                }
                */

                List<int> columns = new List<int>(){1, 2, 3, 4, 5, 6};
                //shuffle col numbers
                columns.shuffle();
                foreach(int x in columns) // compute the score of all possible next move and keep the best one
                {
                    if (this.captor.canPlay(x))
                    {
                        if (this._beliefs.environment.Grid[this._beliefs.environment.getTheLowestEmptyCellIndexInCol(x)][x].State == caseState.empty)
                        {
                            caseState state = (turn == playerTurn.playerRed) ? caseState.red : caseState.yellow;
                            int row = this._beliefs.environment.getTheLowestEmptyCellIndexInCol(x);
                            this._beliefs.environment.Grid[row][x].State = state;
                            this._beliefs.environment.nbMovePlayed++;

                            int nextBestColToPlay = 3;

                            int score = -negaMax(playerTurn.playerRed, -beta, -alpha, ref nextBestColToPlay, depth--);

                            this._beliefs.environment.Grid[row][x].State = caseState.empty;
                            this._beliefs.environment.nbMovePlayed--;

                            if (score > bestScore)
                            {
                                bestScore = score;
                                bestColToPlay = x;
                            }
                            if (score > alpha)
                            {
                                alpha = bestScore;
                                bestColToPlay = x;
                                if (alpha > beta)
                                {
                                    break;
                                }
                            }

                        }
                    }
                }
            }
            return bestScore;
        }

        //fonction d'évaluation
        public int eval(playerTurn player)
        {
            caseState statePlayer = (player == playerTurn.playerRed) ? caseState.red : caseState.yellow;
            playerTurn otherPlayer = (player == playerTurn.playerRed) ? playerTurn.playerYellow : playerTurn.playerRed;
            caseState stateOtherPlayer = (otherPlayer == playerTurn.playerRed) ? caseState.red : caseState.yellow;
            //score eval
            int score = 1, cpt = 0;
            if (this._beliefs.environment.getWinner(otherPlayer) == otherPlayer)
            {
                return -10000 + this._beliefs.environment.nbMovePlayed;

            }
            else if(this._beliefs.environment.getWinner(player) == player)
            {
                return 10000 - this._beliefs.environment.nbMovePlayed;

            }
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (this._beliefs.environment.Grid[i][j].State == caseState.empty) continue;
                    //Colonnes

                    if (i > 3)
                    {
                        if (this._beliefs.environment.Grid[i][j].State == statePlayer)
                        {
                            ++cpt;

                            while (this._beliefs.environment.Grid[i - cpt][j].State == statePlayer)
                            {
                                ++cpt;

                            }
                            if (this._beliefs.environment.Grid[i - cpt -  1][j].State != stateOtherPlayer)
                            {
                                if (cpt == 3)
                                {
                                    score += 1000;
                                }
                                else if (cpt == 2)
                                {
                                    score += 100;
                                }
                                else
                                {
                                    score += 10;
                                }
                            }
                            cpt = 0;
                        }
                        else if (this._beliefs.environment.Grid[i][j].State == stateOtherPlayer)
                        {
                            ++cpt;

                            while (this._beliefs.environment.Grid[i - cpt][j].State == stateOtherPlayer)
                            {
                                ++cpt;
                            }
                            if (this._beliefs.environment.Grid[i - cpt - 1][j].State != statePlayer)
                            {
                                if (cpt == 3)
                                {
                                    score -= 1000;
                                }
                                else if (cpt == 2)
                                {
                                    score -= 100;
                                }
                                else
                                {
                                    score -= 10;
                                }
                            }
                            cpt = 0;
                        }
                    }
                    // Amelioration à apporter : ne scanne pas les 3 cases à gauche 
                    //Lignes
                    if (j < 3)
                    {
                        if (this._beliefs.environment.Grid[i][j].State == statePlayer)
                        {
                            ++cpt;

                            while (this._beliefs.environment.Grid[i][j + cpt].State == statePlayer)
                            {
                                ++cpt;

                            }
                            if (i != 0)
                            {
                                if (this._beliefs.environment.Grid[i][j + cpt + 1].State != stateOtherPlayer)
                                {
                                    if (cpt == 3)
                                    {
                                        score += 1000;
                                    }
                                    else if (cpt == 2)
                                    {
                                        score += 100;
                                    }
                                    else
                                    {
                                        score += 10;
                                    }
                                }
                            }
                            else
                            {

                                if (this._beliefs.environment.Grid[i][j + cpt + 1].State != stateOtherPlayer | this._beliefs.environment.Grid[i ][j - 1].State!=stateOtherPlayer)
                                {
                                    if (cpt == 3)
                                    {
                                        score += 1000;
                                    }
                                    else if (cpt == 2)
                                    {
                                        score += 100;
                                    }
                                    else
                                    {
                                        score += 10;
                                    }
                                }
                            }
                            cpt = 0;
                        }
                        else if (this._beliefs.environment.Grid[i][j].State == stateOtherPlayer)
                        {
                            ++cpt;

                            while (this._beliefs.environment.Grid[i ][j + cpt].State == stateOtherPlayer)
                            {
                                ++cpt;
                            }

                            if (i != 0)
                            {
                                if (j+cpt+1<7 &&this._beliefs.environment.Grid[i ][j + cpt + 1].State != statePlayer)
                                {
                                    if (cpt == 3)
                                    {
                                        score -= 1000;
                                    }
                                    else if (cpt == 2)
                                    {
                                        score -= 100;
                                    }
                                    else
                                    {
                                        score -= 10;
                                    }
                                }
                            }
                            else
                            {
                                if (this._beliefs.environment.Grid[i ][j + cpt + 1].State != statePlayer || this._beliefs.environment.Grid[i][j - 1].State != statePlayer)
                                {
                                    if (cpt == 3)
                                    {
                                        score -= 1000;
                                    }
                                    else if (cpt == 2)
                                    {
                                        score -= 100;
                                    }
                                    else
                                    {
                                        score -= 10;
                                    }
                                }
                            }
                            cpt = 0;
                        }
                    }
                    //verif diagonale vers haut droite
                    if (i > 2 && j < 3)
                    {
                        if (this._beliefs.environment.Grid[i][j].State == statePlayer)
                        {
                            ++cpt;

                            while (this._beliefs.environment.Grid[i - cpt][j + cpt].State == statePlayer)
                            {
                                ++cpt;
                            }

                            if (i != 5 && j != 0)
                            {
                                if (this._beliefs.environment.Grid[i - cpt][j + cpt].State != stateOtherPlayer || this._beliefs.environment.Grid[i + 1][j - 1].State != stateOtherPlayer)
                                {
                                    if (cpt == 3)
                                    {
                                        score += 1000;
                                    }
                                    else if (cpt == 2)
                                    {
                                        score += 100;
                                    }
                                    else
                                    {
                                        score += 10;
                                    }
                                }
                            }
                            else
                            {
                                if (j + cpt + 1 < 7 && this._beliefs.environment.Grid[i -cpt][j + cpt].State != stateOtherPlayer)
                                {
                                    if (cpt == 3)
                                    {
                                        score += 1000;
                                    }
                                    else if (cpt == 2)
                                    {
                                        score += 100;
                                    }
                                    else
                                    {
                                        score += 10;
                                    }
                                }
                            }
                            cpt = 0;
                        }
                        else if (this._beliefs.environment.Grid[i][j].State == stateOtherPlayer)
                        {
                            ++cpt;

                            while (this._beliefs.environment.Grid[i - cpt][j + cpt].State == stateOtherPlayer)
                            {
                                ++cpt;

                            }

                            if (i != 5 && j != 0)
                            {
                                if (this._beliefs.environment.Grid[i - cpt][j + cpt].State != statePlayer || this._beliefs.environment.Grid[i + 1][j - 1].State != statePlayer)
                                {
                                    if (cpt == 3)
                                    {
                                        score -= 1000;
                                    }
                                    else if (cpt == 2)
                                    {
                                        score -= 100;
                                    }
                                    else
                                    {
                                        score -= 10;
                                    }
                                }
                            }
                            else
                            {
                                if (this._beliefs.environment.Grid[i - cpt][j + cpt].State != statePlayer)
                                {
                                    if (cpt == 3)
                                    {
                                        score -= 1000;
                                    }
                                    else if (cpt == 2)
                                    {
                                        score -= 100;
                                    }
                                    else
                                    {
                                        score -= 10;
                                    }
                                }
                            }
                            cpt = 0;
                        }
                    }

                    //verif diagonale vers haut gauche
                    if (i > 2 && j > 3)
                    {
                        if (this._beliefs.environment.Grid[i][j].State == statePlayer)
                        {
                            ++cpt;
                            while (this._beliefs.environment.Grid[i - cpt][j - cpt].State == statePlayer)
                            {
                                ++cpt;
                            }

                            if (j != 6 && i != 5)
                            {
                                if (this._beliefs.environment.Grid[i - cpt][j - cpt].State != stateOtherPlayer || this._beliefs.environment.Grid[i + 1][j + 1].State != stateOtherPlayer)
                                {
                                    if (cpt == 3)
                                    {
                                        score += 1000;
                                    }
                                    else if (cpt == 2)
                                    {
                                        score += 100;
                                    }
                                    else
                                    {
                                        score += 10;
                                    }
                                }
                            }
                            else
                            {
                                if (this._beliefs.environment.Grid[i - cpt][j - cpt].State != stateOtherPlayer)
                                {
                                    if (cpt == 3)
                                    {
                                        score += 1000;
                                    }
                                    else if (cpt == 2)
                                    {
                                        score += 100;
                                    }
                                    else
                                    {
                                        score += 10;
                                    }
                                }
                            }
                            cpt = 0;
                        }
                        else if (this._beliefs.environment.Grid[i][j].State == stateOtherPlayer)
                        {
                            ++cpt;
 
                            while (this._beliefs.environment.Grid[i - cpt][j - cpt].State == stateOtherPlayer)
                            {
                                ++cpt;

                            }

                            if (i != 5 && j != 6)
                            {
                                if (this._beliefs.environment.Grid[i - cpt][j - cpt].State != statePlayer || this._beliefs.environment.Grid[i + 1][j + 1].State != statePlayer)
                                {
                                    if (cpt == 3)
                                    {
                                        score -= 1000;
                                    }
                                    else if (cpt == 2)
                                    {
                                        score -= 100;
                                    }
                                    else
                                    {
                                        score -= 10;
                                    }
                                }
                            }
                            else
                            {
                                if (this._beliefs.environment.Grid[i - cpt][j - cpt].State != statePlayer)
                                {
                                    if (cpt == 3)
                                    {
                                        score -= 1000;
                                    }
                                    else if (cpt == 2)
                                    {
                                        score -= 100;
                                    }
                                    else
                                    {
                                        score -= 10;
                                    }
                                }
                            }
                            cpt = 0;
                        }
                    }
                }
            }
            return score;

        }
    }




}






