using System;
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
        public override string GetInfix()
        {
            return argument.ToString();
        }
        public override string GetLocalString()
        {
            return Argument.ToString();
        }
        public override List<char> GetAllArguments()
        {
            if(argument == '0' || argument == '1') { return new List<char>(); }
            List<char> fullList = new List<char>();
            fullList.Add(argument);
            return fullList;
        }
        public override bool GetAnswer(TruthtableRow row)
        {
            if(argument == '1') { return true; }
            else if(argument == '0') { return false; }
            else if(row.GetValueForArgument(argument) == '1'){
                return true;
            }
            else if (row.GetValueForArgument(argument) == '0')
            {
                return false;
            }
            else
            {
                throw new Exception("Encountered invalid symbol value for argument " + argument);
            }
        }

        public override List<Connective> GetAllConnectives()
        {
            return new List<Connective>() { this };
        }

        public override Connective GetNandProposition()
        {
            return this.Copy();
        }
        public override string GetParseString()
        {
            return argument.ToString();
        }
        public override Connective Copy()
        {
            ConnectiveArgument temp = new ConnectiveArgument(argument);
            return temp;
        }
        public override bool IsTheSameAs(Connective con)
        {
            if (con is ConnectiveArgument)
            {
                ConnectiveArgument c = (ConnectiveArgument)con;

                if (c.Argument == this.argument) { return true; }
            }
            return false;
        }
        public override bool IsNormalProposition()
        {
            return true;
        }
        public override bool ChangeLocalArgument(char a, char b)
        {
            return false;
        }
        public override List<char> GetAllLocalArguments()
        {
            return new List<char>();
        }
        public override bool AreLocalArgumentsMatching(List<char> LocalArguments, List<char> LocalArgumentsAll)
        {
            return true;
        }
    }
}
