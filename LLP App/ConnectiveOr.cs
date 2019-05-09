using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class ConnectiveOr : ConnectiveTwo
    {
        public ConnectiveOr()
        {

        }
        public override char GetLocalString()
        {
            return '|';
        }
        public override string GetInfix()
        {
            return "(" + con1.GetInfix() + " | " + con2.GetInfix() + ")";
        }
        public override void setLeftConnective(Connective con)
        {
            if (con != null)
            {
                con1 = con;
            }
            else { throw new NullReferenceException(); }
        }

        public override void setRightConnective(Connective con)
        {
            if (con != null)
            {
                con2 = con;
            }
            else { throw new NullReferenceException(); }
        }
        public override bool GetAnswer(TruthtableRow row)
        {
            bool leftAnswer = con1.GetAnswer(row);
            bool rightAnswer = con2.GetAnswer(row);
            if (leftAnswer || rightAnswer)
            {
                return true;
            }
            return false;
        }

        public override Connective GetNandProposition()
        {
            ConnectiveNand mainNand = new ConnectiveNand();
            ConnectiveNot not1 = new ConnectiveNot();
            ConnectiveNot not2 = new ConnectiveNot();

            not1.setLeftConnective(con1.Copy());
            not2.setLeftConnective(con2.Copy());
            mainNand.setLeftConnective(not1.GetNandProposition());
            mainNand.setRightConnective(not2.GetNandProposition());
            return mainNand;
        }
        public override string GetParseString()
        {
            return "|(" + con1.GetParseString() + "," + con2.GetParseString() + ")";
        }
        public override Connective Copy()
        {
            ConnectiveOr temp = new ConnectiveOr();
            temp.setLeftConnective(con1.Copy());
            temp.setRightConnective(con2.Copy());
            return temp;
        }
    }
}
