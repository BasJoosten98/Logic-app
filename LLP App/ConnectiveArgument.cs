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
        public override char GetLocalString()
        {
            return Argument;
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
            return this;
        }
        public override string GetParseString()
        {
            return argument.ToString();
        }
    }
}
