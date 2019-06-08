using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class ConnectiveFunction : Connective
    {
        private List<char> localArguments;
        private char functionChar;

        public char FunctionChar { get { return functionChar; } }
        public List<char> LocalArguments { get { return localArguments; } }

        public ConnectiveFunction()
        {
            localArguments = new List<char>();
        }
        public void SetFunctionChar(char c)
        {
            functionChar = c;
        }
        public void AddArgument(char c)
        {
            localArguments.Add(c);
        }


        public override Connective Copy()
        {
            ConnectiveFunction cf = new ConnectiveFunction();
            cf.SetFunctionChar(functionChar);
            foreach(char c in localArguments)
            {
                cf.AddArgument(c);
            }
            return cf;
        }

        public override List<char> GetAllArguments()
        {
            throw new NotImplementedException();
        }

        public override List<Connective> GetAllConnectives()
        {
            return new List<Connective>() { this };
        }

        public override bool GetAnswer(TruthtableRow row)
        {
            throw new NotImplementedException();
        }

        public override string GetInfix()
        {
            string holder = functionChar + "(" + localArguments[0];
            for(int i = 1; i < localArguments.Count; i++)
            {
                holder += ", ";
                holder += localArguments[i];
            }
            holder += ")";
            return holder;
        }

        public override string GetLocalString()
        {
            return functionChar + "(...)";
        }

        public override Connective GetNandProposition()
        {
            throw new NotImplementedException();
        }

        public override string GetParseString()
        {
            string holder = functionChar + "(" + localArguments[0];
            for (int i = 1; i < localArguments.Count; i++)
            {
                holder += ",";
                holder += localArguments[i];
            }
            holder += ")";
            return holder;
        }

        public override bool IsTheSameAs(Connective con)
        {
            if (con is ConnectiveFunction)
            {
                ConnectiveFunction c = (ConnectiveFunction)con;

                if(c.FunctionChar == this.functionChar)
                {
                    if(c.LocalArguments.Count == this.localArguments.Count)
                    {
                        bool differenceFound = false;
                        for(int i = 0; i < this.localArguments.Count; i++)
                        {
                            if(c.LocalArguments[i] != this.localArguments[i])
                            {
                                differenceFound = true;
                                break;
                            }
                        }
                        return !differenceFound;
                    }
                }
            }
            return false;
        }
        public override bool IsNormalProposition()
        {
            return false;
        }
        public override bool ChangeLocalArgument(char a, char b)
        {
            bool succes = false;
            for(int i = 0; i < this.localArguments.Count; i++)
            {
                if(localArguments[i] == a)
                {
                    localArguments[i] = b;
                    succes = true;
                }
            }
            return succes;
        }
        public override List<char> GetAllLocalArguments()
        {
            List<char> temp = new List<char>();
            foreach(char c in localArguments)
            {
                temp.Add(c);
            }
            return temp;
        }
        public override bool AreLocalArgumentsMatching(List<char> LocalArguments, List<char> LocalArgumentsAll)
        {
            foreach(char c in localArguments)
            {
                if (!LocalArguments.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
