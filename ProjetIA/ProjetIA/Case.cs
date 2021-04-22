using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIA
{
    enum caseState
    {
        yellow,
        red,
        empty,
    }
    class Case
    {
        private int _x;
        private int _y;
        public int X
        {
            get { return _x; }
        }
        public int Y
        {
            get { return _y; }
        }

        private caseState _state;
        public caseState State
        {
            get { return _state; }
            set { _state = value; }
        }

        public Case(int x, int y)
        {
            _x = x;
            _y = y;
            _state = caseState.empty;
        }
        ~Case() { }
    }
}
