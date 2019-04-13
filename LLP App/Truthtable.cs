using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class Truthtable
    {
        private List<char> argumentsChar;
        private List<TruthtableRow> rows;
        private ConnectiveHolder conHolder;

        public List<TruthtableRow> Rows { get { return rows; } }
        public Truthtable(ConnectiveHolder ConHolder)
        {
            conHolder = ConHolder;
            argumentsChar = ConHolder.GetListOfAllArguments();
            rows = new List<TruthtableRow>();
            createTruthtableRows();
            fillTruthtableRowsWithAnswers();
        }

        private void createTruthtableRows()
        {
            if(argumentsChar != null)
            {
                //calculting row true/false swap time (when to print true and when to print false)
                List<int> rowLenght = new List<int>();
                for(int i = argumentsChar.Count -1; i >= 0; i--)
                {
                    rowLenght.Add((int)Math.Pow(2, i));
                }

                //creating all rows
                TruthtableRow tempRow;
                TruthtableRowArgument tempArg;
                TruthtableRowArgument[] args = new TruthtableRowArgument[argumentsChar.Count];
                for(int i = rowLenght[0]*2 - 1; i >= 0; i--) //going to next row
                {
                    for(int j = 0; j < argumentsChar.Count; j++) //creating one row
                    {
                        int cal = i / rowLenght[j];
                        if(cal%2 == 0) { tempArg = new TruthtableRowArgument(argumentsChar[j], true); }
                        else { tempArg = new TruthtableRowArgument(argumentsChar[j], false); }
                        args[j] = tempArg;
                    }
                    tempRow = new TruthtableRow(args.ToList());
                    rows.Add(tempRow);
                }
            }
            else { throw new NullReferenceException(); }
        } 
        private void fillTruthtableRowsWithAnswers()
        {
            foreach(TruthtableRow r in rows)
            {
                bool answer = conHolder.GetTruthtableRowAnswer(r);
                r.SetRowValue(answer);
            }
        }
    }
}
