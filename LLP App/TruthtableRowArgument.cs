using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class TruthtableRowArgument
    {
        private char argument;
        private bool argumentValue;

        public char Argument { get { return argument; } }
        public bool Value { get { return argumentValue; } }

        public TruthtableRowArgument(char arg, bool value)
        {
            argument = arg;
            argumentValue = value;
        }
    }
}
