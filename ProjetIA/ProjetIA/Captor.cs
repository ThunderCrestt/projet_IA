using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIA
{
    class Captor
    {
        private Environnement environnement;

        public Captor(Environnement environnement){
            this.environnement = environnement;
        }

        // Cette fonction permet de voir tout les pions d'une certaine couleur.
        // entrée : Couleur (énumération) 
        // sortie : Liste de coordonnée [(x,y),(x,y), ...] des pions de cette couleur.
        public List<(int,int)> getNumberPawns(Color color){
            List<(int,int)> result = new List<(int, int)>();
            // Parcours de la matrice ou délégation
            return result;
        }


        // Cette fonction permet de voir tout les pions présents sur une ligne.
        // entrée : int x, la ligne 
        // sortie : Liste de coordonnée [(int,int,Color),(int,int,Color), ...].
        public List<(int,int,Color)> getNumberPawnsRow(int x){
            List<(int,int,Color)> result = new List<(int,int,Color)>();
            // Parcours de la matrice ou délégation
            return result;
        }


        // Cette fonction permet de voir tout les pions présents sur une Colonne.
        // entrée : int y, la colonne 
        // sortie : Liste de coordonnée [(int,int,Color),(int,int,Color), ...].
        public List<(int,int,Color)> getNumberPawnsCol(int y){
            List<(int,int,Color)> result = new List<(int,int,Color)>();
            // Parcours de la matrice ou délégation
            return result;
        }


        // Cette fonction permet de voir tout les pions présents sur une diagonale.
        // entrée : int x, int y, int x_, int y_ deux points de la matrice
        // sortie : Liste de coordonnée [(int,int,Color),(int,int,Color), ...].
        public List<(int,int,Color)> getNumberPawnsDiag(int x, int y, int x_, int y_){
            List<(int,int,Color)> result = new List<(int,int,Color)>();
            // Parcours de la matrice ou délégation
            return result;
        }

        // Cette fonction permet de voir si un pion est présent sur la case demandée.
        // entrée : int x, int y
        // sortie : True ou False
        public Boolean IsEmpty(int x, int y){
            return true;
        }
    }
}