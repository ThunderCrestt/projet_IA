using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIA
{
    class Captor
    {
        private Environnement _environment;

        public Captor(Environnement environment)
        {
            this._environment = environment;
        }

        public bool isAWinningMove(int col, playerTurn turn)
        {
            caseState stateToCheck = (turn == playerTurn.playerRed) ? caseState.red : caseState.yellow;
            int rowIndexOfLastPawnInCol = this._environment.getTheLowestEmptyCellIndexInCol(col);

            // There is a winning move with columns ?
            if (rowIndexOfLastPawnInCol < 3
               && this._environment.Grid[rowIndexOfLastPawnInCol + 1][col].State == stateToCheck
               && this._environment.Grid[rowIndexOfLastPawnInCol + 2][col].State == stateToCheck
               && this._environment.Grid[rowIndexOfLastPawnInCol + 3][col].State == stateToCheck)
            {
                return true;
            }

            // There is a winning move with line ?
            int nbYellowAtRight = 0;
            int nbYellowAtLeft = 0;
            for (int i = 1; col + i < this._environment.Width; i++)
            {
                if (this._environment.Grid[rowIndexOfLastPawnInCol][col + i].State == stateToCheck)
                {
                    nbYellowAtRight++;
                }
                else
                {
                    break;
                }
            }
            for (int i = 1; col - i >= 0; i++)
            {
                if (this._environment.Grid[rowIndexOfLastPawnInCol][col - i].State == stateToCheck)
                {
                    nbYellowAtLeft++;
                }
                else
                {
                    break;
                }
            }
            if (nbYellowAtLeft >= 3 || nbYellowAtRight >= 3 || nbYellowAtLeft + nbYellowAtRight >= 3)
            {
                return true;
            }

            // There is a winning move with diagonal up-right?
            nbYellowAtRight = 0;
            nbYellowAtLeft = 0;
            for (int i = 1; col + i < this._environment.Width && rowIndexOfLastPawnInCol - i >= 0; i++)
            {
                if (this._environment.Grid[rowIndexOfLastPawnInCol - i][col + i].State == stateToCheck)
                {
                    nbYellowAtRight++;
                }
                else
                {
                    break;
                }
            }
            for (int i = 1; col - i >= 0 && rowIndexOfLastPawnInCol + i < this._environment.Height; i++)
            {
                if (this._environment.Grid[rowIndexOfLastPawnInCol + i][col - i].State == stateToCheck)
                {
                    nbYellowAtLeft++;
                }
                else
                {
                    break;
                }
            }
            if (nbYellowAtLeft >= 3 || nbYellowAtRight >= 3 || nbYellowAtLeft + nbYellowAtRight >= 3)
            {
                return true;
            }

            // There is a winning move with diagonal up-left?
            nbYellowAtRight = 0;
            nbYellowAtLeft = 0;
            for (int i = 1; col - i >= 0 && rowIndexOfLastPawnInCol - i >= 0; i++)
            {
                if (this._environment.Grid[rowIndexOfLastPawnInCol - i][col - i].State == stateToCheck)
                {
                    nbYellowAtLeft++;
                }
                else
                {
                    break;
                }
            }
            for (int i = 1; (col + i) < this._environment.Width && (rowIndexOfLastPawnInCol + i) < this._environment.Height; i++)
            {
                if (this._environment.Grid[rowIndexOfLastPawnInCol + i][col + i].State == stateToCheck)
                {
                    nbYellowAtRight++;
                }
                else
                {
                    break;
                }
            }
            if (nbYellowAtLeft >= 3 || nbYellowAtRight >= 3 || nbYellowAtLeft + nbYellowAtRight >= 3)
            {
                return true;
            }

            // if there is no winning move
            return false;
        }

        // Cette fonction permet de voir tous les pions d'une certaine couleur.
        // entrée : Couleur (énumération) 
        // sortie : Liste de cases des pions de cette couleur.
        public List<Case> GetPawns(caseState state)
        {
            List<Case> result = new List<Case>();
            for (int y = 0; y < _environment.Grid.Count; y++)
            {
                for (int x = 0; x < _environment.Grid[0].Count; x++)
                {
                    if (_environment.Grid[y][x].State == state)
                    {
                        result.Add(_environment.Grid[y][x]);
                    }
                }
            }
            return result;
        }


        // Cette fonction permet de voir tous les pions présents sur une ligne.
        // entrée : int y, la ligne 
        // sortie : Liste de cases.
        public List<Case> GetPawnsRow(int y)
        {
            List<Case> result = new List<Case>();
            for (int x = 0; x < _environment.Grid[y].Count; x++)
            {
                if (_environment.Grid[y][x].State != caseState.empty)
                {
                    result.Add(_environment.Grid[y][x]);
                }
            }
            return result;
        }


        // Cette fonction permet de voir tous les pions présents sur une Colonne.
        // entrée : int x, la colonne 
        // sortie : Liste de Cases.
        public List<Case> GetPawnsCol(int x)
        {
            List<Case> result = new List<Case>();
            for (int y = 0; y < _environment.Grid.Count; y++)
            {
                if (_environment.Grid[y][x].State != caseState.empty)
                {
                    result.Add(_environment.Grid[y][x]);
                }
            }
            return result;
        }


        // Cette fonction permet de voir tous les pions présents la diagonale droite.
        // entrée : int x, int y
        // sortie : Liste de cases.
        public List<Case> GetPawnsDiagRight(int x, int y)
        {
            List<Case> result = new List<Case>();

            return result;
        }

        // Cette fonction permet de voir tous les pions présents sur la diagonale gauche.
        // entrée : int x, int y
        // sortie : Liste de cases.
        public List<Case> GetPawnsDiagLeft(int x, int y)
        {
            List<List<Case>> grid = _environment.Grid;
            List<Case> result = new List<Case>();

            // Parcours de la matrice ou délégation
            return result;
        }

        // Cette fonction permet de voir si un pion est présent sur la case demandée.
        // entrée : int x la col, int y la ligne
        // sortie : True ou False
        public Boolean IsEmpty(int x, int y)
        {
            return (_environment.Grid[y][x].State == caseState.empty);
        }

        // Cette fonction permet de voir si la colonne position x est complete ou non
        // entrée : int x la col
        // sortie : True ou False
        public Boolean canPlay(int x)
        {         
            return !(_environment.getTheLowestEmptyCellIndexInCol(x) == -1);
        }

        public Environnement GetEnvironment()
        {
            return _environment;
        }
    }
}
