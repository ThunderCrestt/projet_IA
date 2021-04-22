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
        playerYellow=1
    }

    public partial class GameForm : Form
    {
        private Rectangle[] columns;
        private int[,] board;
        private playerTurn turn;
        public GameForm()
        {
            InitializeComponent();
            this.columns = new Rectangle[7]; // 7 columns
            this.board = new int[6, 7 ];
            this.turn = playerTurn.playerRed;
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
                        this.columns[j] = new Rectangle(32 + 48 * j, 24, 32, 300);
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
                    this.board[rowIndex, colIndex] = (this.turn==playerTurn.playerRed) ? 1 : 2; //add in board the piece 
                    Graphics graphics = this.CreateGraphics();
                    if (this.turn == playerTurn.playerRed)
                    {
                        graphics.FillEllipse(Brushes.Red, 32+48 * colIndex, 32 + 48 * rowIndex, 32, 32);
                        this.turn = playerTurn.playerYellow;
                    } else if(this.turn==playerTurn.playerYellow)
                    {
                        graphics.FillEllipse(Brushes.Yellow, 32 + 48 * colIndex, 32 + 48 * rowIndex, 32, 32);
                        this.turn = playerTurn.playerRed;
                    }
                }
            }
        }

        private playerTurn getWinner(playerTurn playerToCheck)
        {

            //vertical win check
            for(int row =0; row <this.board.GetLength(0)-3;row++)
            {
                for(int col=0;col < this.board.GetLength(1);col++)
                {
                    if(this.allNumbersAreEqual(playerToCheck,this.board[row,col],this.board[row+1,col],this.board[row,col+1]))
                    {

                    }
                }
            }
            return playerTurn.playerRed;
        }

        private bool allNumbersAreEqual(playerTurn toCheck,params int[] numbers)
        {
            int toCheckInInt = (this.turn == playerTurn.playerRed) ? 1 : 2;
            foreach (int num in numbers)
            {
                if(num!= toCheckInInt)
                {
                    return false;
                }
            }
            return true;
        }

        //get the columnIndex when mouse is clicked
        private int getColumnNumber(Point mouse)
        {
            for(int i =0; i < this.columns.Length;i++)
            {
                if((mouse.X>=this.columns[i].X) && (mouse.Y >= this.columns[i].Y))
                {
                    if((mouse.X <= this.columns[i].X+this.columns[i].Width) && (mouse.Y <= this.columns[i].Y + this.columns[i].Height))
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
            for(int i =5;i>=0;i--)
            {
                if(this.board[i,col]==0)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
