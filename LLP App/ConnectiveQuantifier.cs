using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    abstract class ConnectiveQuantifier : ConnectiveOne
    {
        protected char argument;
        public char Argument { get { return argument; } }

        public void SetArgument(char c)
        {
            argument = c;
        }

        //public override Connective Copy()
        //{
        //    throw new NotImplementedException();
        //}

        public override bool GetAnswer(TruthtableRow row)
        {
            throw new NotImplementedException();
        }

        //public override string GetInfix()
        //{
        //    throw new NotImplementedException();
        //}

        //public override string GetLocalString()
        //{
        //    throw new NotImplementedException();
        //}

        public override Connective GetNandProposition()
        {
            throw new NotImplementedException();
        }

        //public override string GetParseString()
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool IsTheSameAs(Connective con)
        //{
        //    throw new NotImplementedException();
        //}

        public override void setLeftConnective(Connective con)
        {
            if (con != null)
            {

                con1 = con;
                
            }
            else { throw new NullReferenceException(); }
        }
        public override bool IsNormalProposition()
        {
            return false;
        }
        public override bool ChangeLocalArgument(char a, char b)
        {
            bool succes = false;
            if(argument == a) { argument = b; succes = true; }
            bool temp1 = con1.ChangeLocalArgument(a, b);
            return (temp1 || succes);
        }
        public override bool AreLocalArgumentsMatching(List<char> LocalArguments, List<char> LocalArgumentsAll)
        {
            if (LocalArgumentsAll.Contains(argument)) { return false; }
            List<char> temp = new List<char>();
            foreach(char c in LocalArguments)
            {
                temp.Add(c);
            }
            temp.Add(argument);
            LocalArgumentsAll.Add(argument);
            return con1.AreLocalArgumentsMatching(temp, LocalArgumentsAll);
        }
    }
}
