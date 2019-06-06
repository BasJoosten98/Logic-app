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

        public void setArgument(char c)
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

        //public override char GetLocalString()
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
    }
}
