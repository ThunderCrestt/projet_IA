using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIA
{
    class Effector
    {
        public Environment _environment; //field
        public Environment Environment //property
        {
            get; set;
        }
        
        //Placer un pion aux coordonnées indiquées
        public void PutPawn(caseState pawn, int col)
        {
            int rowIndex = this._environment.getTheLowestEmptyCellIndexInCol(col);
            Environment.setCaseNewState(col,rowIndex,pawn);
        }
    }
}
