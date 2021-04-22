using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIA
{


    class Environment
    {
        private List<List<Case>> _grid = new List<List<Case>>();
        public List<List<Case>> Grid
        {
            get { return _grid; }
        }

        public Environment()
        {
            for (int j = 0; j < 6; j++)
            {
                List<Case> line = new List<Case> ();
                for (int i = 0; i < 7; i++)
                {
                    Case y = new Case(i, j);
                    line.Add(y);                    
                }
                _grid.Add(line);
            }
        }
        ~Environment() { }

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

        public Case FoundCase(int x, int y)
        {
            return _grid[y][x];
        }
        
    }
}
