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
        

        public struct Beliefs{
            public Environment environment;
        }

        public Beliefs _beliefs;
        Captor captor;
        public Effector effector;
        public void agentRoutine()
        {
            //alphaBeta
            int bestMove = 3;
            negaMax(playerTurn.playerYellow, -this._beliefs.environment.Width * this._beliefs.environment.Height / 2, this._beliefs.environment.Width * this._beliefs.environment.Height / 2, ref bestMove,4);
            Console.WriteLine(bestMove);
            this.effector.PutPawn(caseState.yellow, bestMove);
            this._beliefs.environment.getWinner(playerTurn.playerYellow);
        }

        //implémentation de negaMax, une amélioration de l'élagage aplha Beta
        public int negaMax(playerTurn turn, int alpha, int beta,ref int bestColToPlay,int depth)
        {
            //Assert(alpha  <  beta, "error : beta > aplha");
            int bestScore = -int.MaxValue;
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

            if (depth == 0)
            {
                bestScore = eval(turn);
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
                for (int x = 0; x < this._beliefs.environment.Width; x++) // compute the score of all possible next move and keep the best one
                {
                    if (this.captor.canPlay(x))
                    {
                        if (this._beliefs.environment.Grid[this._beliefs.environment.getTheLowestEmptyCellIndexInCol(x)][x].State == caseState.empty)
                        {
                            caseState state = (turn == playerTurn.playerRed) ? caseState.red : caseState.yellow;
                            this._beliefs.environment.Grid[this._beliefs.environment.getTheLowestEmptyCellIndexInCol(x)][x].State = state;
                            this._beliefs.environment.nbMovePlayed++;

                            int nextBestColToPlay = -1;
                            int score = -negaMax(playerTurn.playerRed, -beta, -alpha, ref nextBestColToPlay, depth--);

                            int truc = this._beliefs.environment.getTheLowestEmptyCellIndexInCol(x);
                            this._beliefs.environment.Grid[this._beliefs.environment.getTheLowestEmptyCellIndexInCol(x) + 1][x].State = caseState.empty;
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

        public int eval(playerTurn player)
        {
            caseState statePlayer = (player == playerTurn.playerRed) ? caseState.red : caseState.yellow;
            playerTurn otherPlayer = (player == playerTurn.playerRed) ? playerTurn.playerYellow : playerTurn.playerRed;
            caseState stateOtherPlayer = (otherPlayer == playerTurn.playerRed) ? caseState.red : caseState.yellow;
            //On compte évalue la valeur de la         this._beliefs.environment.Grid actuelle
            int score = 0, cpt = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (this._beliefs.environment.Grid[i][j].State == caseState.empty) continue;
                    //Colonnes
                    if (j < 3)
                    {
                        if (this._beliefs.environment.Grid[i][j].State == statePlayer)
                        {
                            ++cpt;
                            while (this._beliefs.environment.Grid[i][j + cpt].State == statePlayer)
                            {
                                ++cpt;
                            }
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
                            cpt = 0;
                        }
                        else if (this._beliefs.environment.Grid[i][j].State == stateOtherPlayer)
                        {
                            ++cpt;
                            while (this._beliefs.environment.Grid[i][j + cpt].State == stateOtherPlayer)
                            {
                                ++cpt;
                            }
                            if (this._beliefs.environment.Grid[i][j + cpt + 1].State != statePlayer)
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
                    //Lignes
                    if (i < 4)
                    {
                        if (this._beliefs.environment.Grid[i][j].State == statePlayer)
                        {
                            ++cpt;
                            while (this._beliefs.environment.Grid[i + cpt][j].State == statePlayer)
                            {
                                ++cpt;
                            }
                            if (i != 0)
                            {
                                if (this._beliefs.environment.Grid[i + cpt + 1][j].State != stateOtherPlayer)
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
                                if (this._beliefs.environment.Grid[i + cpt + 1][j].State != stateOtherPlayer || this._beliefs.environment.Grid[i - 1][j].State != stateOtherPlayer)
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
                            while (this._beliefs.environment.Grid[i + cpt][j].State == stateOtherPlayer)
                            {
                                ++cpt;
                            }
                            if (i != 0)
                            {
                                if (this._beliefs.environment.Grid[i + cpt + 1][j].State != statePlayer)
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
                                if (this._beliefs.environment.Grid[i + cpt + 1][j].State != statePlayer || this._beliefs.environment.Grid[i - 1][j].State != statePlayer)
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
                    if (j < 3 && i < 4)
                    {
                        if (this._beliefs.environment.Grid[i][j].State == statePlayer)
                        {
                            ++cpt;
                            while (this._beliefs.environment.Grid[i + cpt][j + cpt].State == statePlayer)
                            {
                                ++cpt;
                            }
                            if (i != 0 && j != 0)
                            {
                                if (this._beliefs.environment.Grid[i + cpt + 1][j + cpt + 1].State != stateOtherPlayer || this._beliefs.environment.Grid[i - 1][j - 1].State != stateOtherPlayer)
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
                                if (this._beliefs.environment.Grid[i + cpt + 1][j + cpt + 1].State != stateOtherPlayer)
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
                            while (this._beliefs.environment.Grid[i + cpt][j + cpt].State == stateOtherPlayer)
                            {
                                ++cpt;
                            }
                            if (i != 0 && j != 0)
                            {
                                if (this._beliefs.environment.Grid[i + cpt + 1][j + cpt + 1].State != statePlayer || this._beliefs.environment.Grid[i - 1][j - 1].State != statePlayer)
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
                                if (this._beliefs.environment.Grid[i + cpt + 1][j + cpt + 1].State != statePlayer)
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
                    if (j < 3 && i > 2)
                    {
                        if (this._beliefs.environment.Grid[i][j].State == statePlayer)
                        {
                            ++cpt;
                            while (this._beliefs.environment.Grid[i - cpt][j + cpt].State == statePlayer)
                            {
                                ++cpt;
                            }
                            if (i != 6 && j != 0)
                            {
                                if (this._beliefs.environment.Grid[i - cpt - 1][j + cpt + 1].State != stateOtherPlayer || this._beliefs.environment.Grid[i + 1][j - 1].State != stateOtherPlayer)
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
                                if (this._beliefs.environment.Grid[i + cpt + 1][j + cpt + 1].State != stateOtherPlayer)
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
                            if (i != 6 && j != 0)
                            {
                                if (this._beliefs.environment.Grid[i - cpt - 1][j + cpt + 1].State != statePlayer || this._beliefs.environment.Grid[i + 1][j - 1].State != statePlayer)
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
                                if (this._beliefs.environment.Grid[i + cpt + 1][j + cpt + 1].State != statePlayer)
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



    

