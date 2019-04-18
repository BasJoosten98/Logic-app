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
        private char argumentValue;

        public char Argument { get { return argument; } }
        public char Value { get { return argumentValue; } }

        public TruthtableRowArgument(char arg, char value)
        {
            argument = arg;
            argumentValue = value;
        }
    }
}
