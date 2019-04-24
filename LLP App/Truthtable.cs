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
                if (argumentsChar.Count > 0)
                {
                    //calculting row true/false swap time (when to print true and when to print false)
                    List<int> rowLenght = new List<int>();
                    for (int i = argumentsChar.Count - 1; i >= 0; i--)
                    {
                        rowLenght.Add((int)Math.Pow(2, i));
                    }

                    //creating all rows
                    TruthtableRow tempRow;
                    TruthtableRowArgument tempArg;
                    TruthtableRowArgument[] args = new TruthtableRowArgument[argumentsChar.Count];
                    for (int i = rowLenght[0] * 2 - 1; i >= 0; i--) //going to next row
                    {
                        for (int j = 0; j < argumentsChar.Count; j++) //creating one row
                        {
                            int cal = i / rowLenght[j];
                            if (cal % 2 == 0) { tempArg = new TruthtableRowArgument(argumentsChar[j], '1'); }
                            else { tempArg = new TruthtableRowArgument(argumentsChar[j], '0'); }
                            args[j] = tempArg;
                        }
                        tempRow = new TruthtableRow(args.ToList());
                        rows.Add(tempRow);
                    }
                }
                else //Only 1 and/or 0 arguments
                {
                    rows.Add(new TruthtableRow(new List<TruthtableRowArgument>()));
                }
            }
            else { throw new NullReferenceException(); }
        } 
        private void fillTruthtableRowsWithAnswers()
        {
            foreach(TruthtableRow r in rows)
            {
                bool answer = conHolder.GetTruthtableRowAnswer(r);
                if (answer) { r.RowValue = '1'; }
                else { r.RowValue = '0'; }
            }
        }

        public List<TruthtableRow> GetSimpleTable()
        {
            if(argumentsChar.Count == 0) //only 0 and/or 1
            {
                List<TruthtableRow> tempList = new List<TruthtableRow>();
                TruthtableRow tempRow = new TruthtableRow(new List<TruthtableRowArgument>());
                bool rowValue = conHolder.GetTruthtableRowAnswer(tempRow);
                if (rowValue) { tempRow.RowValue = '1'; }
                else { tempRow.RowValue = '0'; }
                tempList.Add(tempRow);
                return tempList;
            }
            return getSimpleTableRec(new List<TruthtableRowArgument>(), 0);
        }
        private List<TruthtableRow> getSimpleTableRec(List<TruthtableRowArgument> argList, int argIndex)
        {
            if(argumentsChar.Count <= argIndex)
            {
                return new List<TruthtableRow>();
            }

            List<TruthtableRow> acceptedRows = new List<TruthtableRow>(); //Rows to send back
            List<TruthtableRow> receivedRows; //Memory spot for receiving recursive rows
            TruthtableRowArgument temporaryRowArg;
            char result;

            bool divisionSucces; //argList has been enough divided

            //check argument == *   
            temporaryRowArg = new TruthtableRowArgument(argumentsChar[argIndex], '*');
            argList.Add(temporaryRowArg);
            result = getSimpleCharResult(argList);
            if(result == '*') //Not yet divided enough for simple table row
            {
                divisionSucces = false;
                receivedRows = getSimpleTableRec(argList, argIndex + 1);
                foreach(TruthtableRow r in receivedRows)
                {
                    acceptedRows.Add(r);
                }
            }
            else //Divided enough for simple table row
            {
                //Create other arguments
                divisionSucces = true;
                List<TruthtableRowArgument> rowArgs = new List<TruthtableRowArgument>();
                for(int i = 0; i < argumentsChar.Count; i++)
                {
                    if(i < argList.Count)
                    {
                        rowArgs.Add(argList[i]);
                    }
                    else
                    {
                        rowArgs.Add(new TruthtableRowArgument(argumentsChar[i], '*'));
                    }
                }
                //Create row and add it
                acceptedRows.Add(new TruthtableRow(rowArgs, result));
            }
            argList.Remove(temporaryRowArg);

            if (!divisionSucces) 
            {
                List<TruthtableRow> trueAndFalseRows = new List<TruthtableRow>(); 
                List<char> trueAndFalse = new List<char>() { '0', '1' };

                foreach(char value in trueAndFalse)
                {
                    //check argument == false/true
                    temporaryRowArg = new TruthtableRowArgument(argumentsChar[argIndex], value);
                    argList.Add(temporaryRowArg);
                    result = getSimpleCharResult(argList);
                    if (result == '*') //Not yet divided enough for simple table row
                    {
                        receivedRows = getSimpleTableRec(argList, argIndex + 1);
                        foreach (TruthtableRow rr in receivedRows)
                        {
                            bool addIt = true;
                            foreach(TruthtableRow ar in acceptedRows)
                            {
                                if (ar.RowValue == rr.RowValue)
                                {
                                    bool foundDifference = false;
                                    for (int i = argIndex + 1; i < argumentsChar.Count; i++)
                                    {
                                        if (ar.GetValueForArgument(argumentsChar[i]) != '*')
                                        {
                                            if (rr.GetValueForArgument(argumentsChar[i]) != ar.GetValueForArgument(argumentsChar[i]))
                                            {
                                                //Difference found! Check next AR with RR
                                                foundDifference = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (!foundDifference) //it is the same or part of AR, thus do not add it!
                                    {
                                        addIt = false;
                                        break;
                                    }
                                }
                            }
                            if (addIt)
                            {
                                trueAndFalseRows.Add(rr);
                            }
                        }
                    }
                    else //Divided enough for simple table row
                    {
                        //Create other arguments
                        List<TruthtableRowArgument> rowArgs = new List<TruthtableRowArgument>();
                        for (int i = 0; i < argumentsChar.Count; i++)
                        {
                            if (i < argList.Count)
                            {
                                rowArgs.Add(argList[i]);
                            }
                            else
                            {
                                rowArgs.Add(new TruthtableRowArgument(argumentsChar[i], '*'));
                            }
                        }
                        //Create row and add it
                        acceptedRows.Add(new TruthtableRow(rowArgs, result));
                    }
                    argList.Remove(temporaryRowArg);
                }   
                foreach(TruthtableRow r in trueAndFalseRows)
                {
                    acceptedRows.Add(r);
                }
            }
            return acceptedRows;

        }
        private char getSimpleCharResult(List<TruthtableRowArgument> argList)
        {
            //copy existing rows
            if(rows == null) { throw new Exception("Unable to calculate simple truthtable, because list rows is missing"); }
            List<TruthtableRow> acceptedRows = new List<TruthtableRow>();
            foreach(TruthtableRow r in rows)
            {
                acceptedRows.Add(r);
            }

            //filter out rows that does not satisfy argList
            foreach(TruthtableRowArgument arg in argList)
            {
                if(arg.Value == '*')
                {
                    //Do nothing
                }
                else if(arg.Value == '1' || arg.Value == '0')
                {
                    for(int i = 0; i < acceptedRows.Count; i++)
                    {
                        if(acceptedRows[i].GetValueForArgument(arg.Argument) != arg.Value)
                        {
                            acceptedRows.Remove(acceptedRows[i]);
                            i--;
                        }
                    }
                }
                else
                {
                    throw new Exception("Unknown symbol encountered");
                }
            }

            //compare rowvalue's and give result
            if(acceptedRows.Count == 0) { throw new Exception("Empty list should not be possible here!"); }
            char result;
            result = acceptedRows[0].RowValue;
            for(int i = 1; i < acceptedRows.Count; i++)
            {
                 if(acceptedRows[i].RowValue != result)
                {
                    return '*';
                }
            }
            return result;
        }
    }
}
