﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetIA
{
    class Agent
    {
        public Agent(Environment env)
        {
            captor = new Captor(env);
            effector = new Effector();
            _beliefs = new Beliefs();
        }
        

        private struct Beliefs{
            public List<Case> caseWithYellowPawn;
            public List<Case> caseWithRedPawn;
        }

        private Beliefs _beliefs;
        Captor captor;
        Effector effector;

        public void updateBeliefs()
        {
            _beliefs.caseWithRedPawn = captor.GetPawns(caseState.red);
            _beliefs.caseWithYellowPawn = captor.GetPawns(caseState.yellow);
        }



    }
}
