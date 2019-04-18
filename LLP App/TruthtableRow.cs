using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class TruthtableRow
    {
        private List<TruthtableRowArgument> arguments;
        public char RowValue;

        public List<TruthtableRowArgument> Arguments { get { return arguments; } }

        public TruthtableRow(List<TruthtableRowArgument> args)
        {
            arguments = args;
        }
        public TruthtableRow(List<TruthtableRowArgument> args, char rowValue)
        {
            arguments = args;
            RowValue = rowValue;
        }

        public char GetValueForArgument(char arg)
        {
            foreach(TruthtableRowArgument a in arguments)
            {
                if(a.Argument == arg)
                {
                    return a.Value;
                }
            }
            throw new Exception("No value found for '" + arg + "'");
        }
    }
}
