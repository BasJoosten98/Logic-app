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

        //CHECKS IF THIS ROW IS A SUBSET OF THE GIVEN MAIN
        public bool isPartOfRow(TruthtableRow main)
        {
            if (main.arguments.Count != this.arguments.Count) { throw new Exception("Rows cannot be compared!"); }
            bool isPart = true;
            if(main.RowValue != this.RowValue) { return false; }

            for(int i = 0; i < main.Arguments.Count; i++)
            {
                if(main.Arguments[i].Argument == this.arguments[i].Argument) //same argument 
                {
                    if(main.Arguments[i].Value != '*') //main arg is 1 or 0, thus this arg should be the same as main arg
                    {
                        if(main.Arguments[i].Value != this.arguments[i].Value)
                        {
                            isPart = false;
                            break;
                        }
                    }
                }
                else
                {
                    throw new Exception("Rows cannot be compared!");
                }
            }
            return isPart;
        }
    }
}
