using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIA
{
    class Captor
    {
        private Environment _environment;

        public Captor(Environment environment)
        {
            this._environment = environment;
        }

        public bool isAWinningMove(int col,playerTurn turn)
        {
            caseState stateToCheck = (turn == playerTurn.playerRed) ? caseState.red : caseState.yellow;
            int rowIndexOfLastPawnInCol = this._environment.getTheLowestEmptyCellIndexInCol(col);
            if (rowIndexOfLastPawnInCol < 3 //si on a trois pion aligné sur la même col
               && this._environment.Grid[rowIndexOfLastPawnInCol+1][col].State==stateToCheck
               && this._environment.Grid[rowIndexOfLastPawnInCol+2][col].State == stateToCheck
               && this._environment.Grid[rowIndexOfLastPawnInCol + 3][col].State == stateToCheck) 
            {
                return true;
            }

            for (int dy = -1; dy <= 1; dy++)
            {                                     // Iterate on horizontal (dy = 0) or two diagonal directions (dy = -1 or dy = 1).
                int nb = 0;                       // counter of the number of stones of current player surronding the played stone in tested direction.
                for (int dx = -1; dx <= 1; dx += 2) // count continuous stones of current player on the left, then right of the played column.

                    for (int x = col + dx, y = rowIndexOfLastPawnInCol + dx * dy; x >= 0 && x < this._environment.Width && y >= 0 && y < this._environment.Height && this._environment.Grid[y][x].State == stateToCheck; nb++)
                    {
                        x += dx;
                        y += dx * dy;
                    }
                if (nb >= 3) return true; // there is an aligment if at least 3 other stones of the current user 
                                          // are surronding the played stone in the tested direction.
            }
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

        public Environment GetEnvironment()
        {
            return _environment;
        }
    }
}
