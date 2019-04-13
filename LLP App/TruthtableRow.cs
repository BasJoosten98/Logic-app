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
        private bool rowValue;
        private bool valueHasBeenSet;

        public List<TruthtableRowArgument> Arguments { get { return arguments; } }
        public bool RowValue { get { if (valueHasBeenSet) { return rowValue; } else { throw new Exception("RowValue has not been set yet"); } } }
        public bool RowValueHasBeenSet { get { return valueHasBeenSet; } }

        public TruthtableRow(List<TruthtableRowArgument> args)
        {
            arguments = args;
        }

        public void SetRowValue(bool value)
        {
            valueHasBeenSet = true;
            rowValue = value;
        }

        public bool GetValueForArgument(char arg)
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
