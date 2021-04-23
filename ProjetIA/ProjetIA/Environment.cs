using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIA
{


    public class Environment
    {
        private int _height;
        public int Height
        {
            get { return _height; }
        }

        private int _width;
        public int Width
        {
            get { return _width; }
        }

        private List<List<Case>> _grid = new List<List<Case>>();
        public List<List<Case>> Grid
        {
            get { return _grid; }
        }

        public int nbMovePlayed = 0;
        private GameForm ui;
        public GameForm UI
        {
            get { return ui; }
        }

        public Environment()
        {
            _height = 6;
            _width = 7;
            for (int j = 0; j < _height; j++)
            {
                List<Case> line = new List<Case> ();
                for (int i = 0; i < _width; i++)
                {
                    Case y = new Case(i, j);
                    line.Add(y);                    
                }
                _grid.Add(line);
            }
        }
        ~Environment() { }
        //Cette fonction vide le puissance 4

        public void setUI(GameForm ui)
        {
            this.ui = ui;
        }
        public void restartGame()
        {
            for (int j = 0; j < 6; j ++)
            {
                for (int i = 0; i < 7; i ++)
                {
                    _grid[i][j].State = caseState.empty;
                }
            }
        }

        // Cette fonction permet de retourner une case en fonction de ses coordonées
        public Case FoundCase(int x, int y)
        {
            return _grid[y][x];
        }
        
        public void setCaseNewState(int x,int y,caseState newState)
        {
            this.Grid[y][x].State = newState;
            ui.DrawPawn(x, y);
        }


        public bool allStateCaseAreEqual(playerTurn toCheck, params Case[] cases)
        {
            caseState stateToCheck = (toCheck == playerTurn.playerRed) ? caseState.red : caseState.yellow;
            foreach (Case c in cases)
            {
                if (c.State != stateToCheck)
                {
                    return false;
                }
            }
            return true;
        }

        public int getTheLowestEmptyCellIndexInCol(int col)
        {
            for (int i = 5; i >= 0; i--)
            {
                if (Grid[i][col].State == caseState.empty)
                {
                    return i;
                }
            }
            return -1;
        }

        public playerTurn getWinner(playerTurn playerToCheck)
        {
            #region vertical win check(|)
            //vertical win check
            for (int row = 0; row < Grid.Count - 3; row++)
            {
                for (int col = 0; col < Grid[0].Count; col++)
                {
                    if (this.allStateCaseAreEqual(playerToCheck,Grid[row][col],Grid[row + 1][col], Grid[row + 2][col], Grid[row + 3][col]))
                    {
                        return playerToCheck;
                    }
                }
            }
            #endregion

            #region horizontal win check (-)

            //horizontal win check
            for (int row = 0; row < Grid.Count; row++)
            {
                for (int col = 0; col < Grid[0].Count - 3; col++)
                {
                    if (this.allStateCaseAreEqual(playerToCheck,Grid[row][col], Grid[row][col + 1], Grid[row][col + 2], Grid[row][col + 3]))
                    {
                        return playerToCheck;
                    }
                }
            }
            #endregion

            #region top left diagonal win check (\)
            for (int row = 0; row < Grid.Count - 3; row++)
            {
                for (int col = 0; col <Grid[0].Count - 3; col++)
                {
                    if (this.allStateCaseAreEqual(playerToCheck, Grid[row][col], Grid[row + 1][col + 1],Grid[row + 2][col + 2], Grid[row + 3][col + 3]))
                    {
                        return playerToCheck;
                    }
                }
            }
            #endregion

            #region top right diagonal win check (/)
            //top right diagonal win check (/)
            for (int row = 0; row < Grid.Count - 3; row++)
            {
                for (int col = 3; col < Grid[0].Count; col++)
                {
                    if (this.allStateCaseAreEqual(playerToCheck, Grid[row][col], Grid[row + 1][col - 1],Grid[row + 2][col - 2], Grid[row + 3][col - 3]))
                    {
                        return playerToCheck;
                    }
                }
            }
            #endregion
            return playerTurn.none;

        }
    }
}
