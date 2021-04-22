using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetIA
{
    enum playerTurn
    {
        playerRed=0,
        playerYellow=1,
        none
    }

    public partial class GameForm : Form
    {
        private Rectangle[] _columns;
        //private int[,] board;
        private playerTurn _turn;
        private Environment _environment;
        public GameForm(Environment environment)
        {
            InitializeComponent();
            this._columns = new Rectangle[7]; // 7 columns
            //this.board = new int[6, 7 ];
            this._turn = playerTurn.playerRed;
            this._environment = environment;
        }

        private void GameForm_Load(object sender, EventArgs e)

        {
        }

        private void GameForm_Paint_1(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Blue, 24, 24, 340, 300);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i == 0)
                    {
                        this._columns[j] = new Rectangle(32 + 48 * j, 24, 32, 300);
                    }
                    e.Graphics.FillEllipse(Brushes.White, 32 + 48 * j, 32 + 48 * i, 32, 32);
                }

            }
        }

        private void GameForm_MouseClick(object sender, MouseEventArgs e)
        {
            int colIndex = this.getColumnNumber(e.Location);
            if(colIndex !=-1)
            {
                int rowIndex = this.getTheLowestEmptyCellIndexInCol(colIndex);
                if(rowIndex!=-1)
                {
                    //this.board[rowIndex, colIndex] = (this._turn==playerTurn.playerRed) ? 1 : 2; //add in board the piece 
                    this._environment.setCaseNewState(colIndex,rowIndex, (this._turn == playerTurn.playerRed) ? caseState.red : caseState.yellow);
                    Graphics graphics = this.CreateGraphics();
                    if (this._turn == playerTurn.playerRed)
                    {
                        graphics.FillEllipse(Brushes.Red, 32+48 * colIndex, 32 + 48 * rowIndex, 32, 32);
                    } else if(this._turn==playerTurn.playerYellow)
                    {
                        graphics.FillEllipse(Brushes.Yellow, 32 + 48 * colIndex, 32 + 48 * rowIndex, 32, 32);
                    }
                    //check if someone has won

                    playerTurn winner = getWinner(this._turn);
                    if(winner!=playerTurn.none)
                    {
                        string messagePlayerWon = (winner == playerTurn.playerRed) ? "Red" : "Yellow";
                        MessageBox.Show("Congrulation " + messagePlayerWon + " player ! You Have Won ! ");
                        Application.Restart();
                    }

                    //if nobody has won we give the other player the turn
                    if (this._turn == playerTurn.playerRed)
                    {
                        this._turn = playerTurn.playerYellow;
                    }
                    else if (this._turn == playerTurn.playerYellow)
                    {
                        this._turn = playerTurn.playerRed;
                    }

                }
            }
        }

        private playerTurn getWinner(playerTurn playerToCheck)
        {
            #region vertical win check(|)
            //vertical win check
            for (int row=0; row<this._environment.Grid.Count-3;row++)
            {
                for(int col=0;col<this._environment.Grid[0].Count; col++)
                {
                    if(this.allStateCaseAreEqual(playerToCheck,this._environment.Grid[row][col], this._environment.Grid[row+1][col], this._environment.Grid[row+2][col], this._environment.Grid[row + 3][col]))
                    {
                        return playerToCheck;
                    }
                }
            }
            #endregion

            #region horizontal win check (-)

            //horizontal win check
            for (int row = 0; row < this._environment.Grid.Count; row++)
            {
                for (int col = 0; col < this._environment.Grid[0].Count-3; col++)
                {
                    if (this.allStateCaseAreEqual(playerToCheck, this._environment.Grid[row][col], this._environment.Grid[row][col+1], this._environment.Grid[row][col + 2], this._environment.Grid[row][col + 3]))
                    {
                        return playerToCheck;
                    }
                }
            }
            #endregion

            #region top left diagonal win check (\)
            for (int row = 0; row < this._environment.Grid.Count-3; row++)
            {
                for (int col = 0; col < this._environment.Grid[0].Count - 3; col++)
                {
                    if (this.allStateCaseAreEqual(playerToCheck, this._environment.Grid[row][col], this._environment.Grid[row+1][col + 1], this._environment.Grid[row+2][col + 2], this._environment.Grid[row+3][col + 3]))
                    {
                        return playerToCheck;
                    }
                }
            }
            #endregion

            #region top right diagonal win check (/)
            //top right diagonal win check (/)
            for (int row = 0; row < this._environment.Grid.Count - 3; row++)
            {
                for (int col = 3; col < this._environment.Grid[0].Count; col++)
                {
                    if (this.allStateCaseAreEqual(playerToCheck, this._environment.Grid[row][col], this._environment.Grid[row + 1][col - 1], this._environment.Grid[row + 2][col - 2], this._environment.Grid[row + 3][col - 3]))
                    {
                        return playerToCheck;
                    }
                }
            }
            #endregion
            return playerTurn.none;
            
        }


        //read if all cases in a list is equal
        private bool allStateCaseAreEqual(playerTurn toCheck,params Case[] cases)
        {
            caseState stateToCheck = (toCheck == playerTurn.playerRed) ? caseState.red : caseState.yellow;
            foreach (Case c in cases)
            {
                if(c.State!= stateToCheck)
                {
                    return false;
                }
            }
            return true;
        }

        //get the columnIndex when mouse is clicked
        private int getColumnNumber(Point mouse)
        {
            for(int i =0; i < this._columns.Length;i++)
            {
                if((mouse.X>=this._columns[i].X) && (mouse.Y >= this._columns[i].Y))
                {
                    if((mouse.X <= this._columns[i].X+this._columns[i].Width) && (mouse.Y <= this._columns[i].Y + this._columns[i].Height))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }


        //get the empty cell that is at the lowest point in the column ( lowest in interface highest in index )
        private int getTheLowestEmptyCellIndexInCol(int col)
        {
            for (int i = 5; i >= 0; i--)
            {
                if (this._environment.Grid[i][col].State==caseState.empty)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
