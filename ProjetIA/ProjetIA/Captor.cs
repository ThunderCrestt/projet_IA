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

        // Cette fonction permet de voir tous les pions d'une certaine couleur.
        // entrée : Couleur (énumération) 
        // sortie : Liste de cases des pions de cette couleur.
        public List<Case> GetPawns(caseState state)
        {
            List<Case> result = new List<Case>();
            for (int x = 0; x < _environment.Grid.Count; x++)
            {
                for (int y = 0; y < _environment.Grid[0].Count; y++)
                {
                    if(_environment.Grid[x][y].State == state)
                    {
                        result.Add(_environment.Grid[x][y]);
                    }
                }
            }
            return result;
        }


        // Cette fonction permet de voir tous les pions présents sur une ligne.
        // entrée : int x, la ligne 
        // sortie : Liste de cases.
        public List<Case> GetPawnsRow(int x)
        {
            List<Case> result = new List<Case>();
            for (int y = 0; y<_environment.Grid[x].Count; y++)
            {
                result.Add(_environment.Grid[x][y]);
            }
            return result;
        }


        // Cette fonction permet de voir tous les pions présents sur une Colonne.
        // entrée : int y, la colonne 
        // sortie : Liste de Cases.
        public List<Case> GetPawnsCol(int y)
        {
            List<Case> result = new List<Case>();
            for (int x = 0; x < _environment.Grid.Count; x++)
            {
                result.Add(_environment.Grid[x][y]);
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
        // entrée : int x, int y
        // sortie : True ou False
        public Boolean IsEmpty(int x, int y)
        {
            return (_environment.Grid[x][y].State == caseState.empty);
        }
    }
}
