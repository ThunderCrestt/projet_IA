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


    public partial class GameForm : Form
    {
        private Rectangle[] _columns;
        private Environment _environment;
        public GameForm(Environment environment)
        {
            InitializeComponent();
            this._columns = new Rectangle[7]; // 7 columns
            this._environment = environment;
            this._environment.turn = playerTurn.playerRed;

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
            if (this._environment.turn == playerTurn.playerRed)
            {
                int colIndex = this.getColumnNumber(e.Location);
                if (colIndex != -1)
                {
                    int rowIndex = this._environment.getTheLowestEmptyCellIndexInCol(colIndex);
                    if (rowIndex != -1)
                    {
                        //this.board[rowIndex, colIndex] = (this._turn==playerTurn.playerRed) ? 1 : 2; //add in board the piece 
                        this._environment.setCaseNewState(colIndex, rowIndex, (this._environment.turn == playerTurn.playerRed) ? caseState.red : caseState.yellow);
                        //DrawPawn(colIndex, rowIndex);
                        //check if someone has won
                        this._environment.nbMovePlayed++;
                        playerTurn winner = getWinner(this._environment.turn);
                        if (winner != playerTurn.none)
                        {
                            string messagePlayerWon = (winner == playerTurn.playerRed) ? "Red" : "Yellow";
                            MessageBox.Show("Congrulation " + messagePlayerWon + " player ! You Have Won ! ");
                            Application.Restart();
                            this._environment.restartGame();
                        }

                        //if nobody has won we give the other player the turn
                        if (this._environment.turn == playerTurn.playerRed)
                        {
                            this._environment.turn = playerTurn.playerYellow;
                        }
                        else if (this._environment.turn == playerTurn.playerYellow)
                        {
                            this._environment.turn = playerTurn.playerRed;
                        }
                        this._environment.IsTurnAgent = true;

                    }
                }
            }
        }

        public void DrawPawn(int colIndex,int rowIndex)
        {
            Graphics graphics = this.CreateGraphics();
            if (this._environment.turn == playerTurn.playerRed)
            {
                graphics.FillEllipse(Brushes.Red, 32 + 48 * colIndex, 32 + 48 * rowIndex, 32, 32);
            }
            else if (this._environment.turn == playerTurn.playerYellow)
            {
                graphics.FillEllipse(Brushes.Yellow, 32 + 48 * colIndex, 32 + 48 * rowIndex, 32, 32);
            }
        }


        private playerTurn getWinner(playerTurn playerToCheck)
        {
            return this._environment.getWinner(playerToCheck);
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

    }
}
