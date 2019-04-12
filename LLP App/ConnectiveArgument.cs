﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class ConnectiveArgument : Connective
    {
        private char argument;

        public char Argument { get { return argument; } }

        public ConnectiveArgument(char arg)
        {
            argument = arg;
        }
        public override void setLeftConnective(Connective con)
        {
            throw new Exception("Connective Argument does not have any input");
        }
        public override void setRightConnective(Connective con)
        {
            throw new Exception("Connective Argument does not have any input");
        }
    }
}
