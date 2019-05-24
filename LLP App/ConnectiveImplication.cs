using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class ConnectiveImplication : ConnectiveTwo
    {
        public ConnectiveImplication()
        {

        }
        public override char GetLocalString()
        {
            return '>';
        }
        public override string GetInfix()
        {
            return "(" + con1.GetInfix() + " > " + con2.GetInfix() + ")";
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
            if (!leftAnswer)
            {
                return true;
            }
            else
            {
                bool rightAnswer = con2.GetAnswer(row);
                if (rightAnswer) { return true; }
                else { return false; }
            }
        }

        public override Connective GetNandProposition()
        {
            ConnectiveNand mainNand = new ConnectiveNand();
            ConnectiveNot not = new ConnectiveNot();

            mainNand.setLeftConnective(con1.GetNandProposition());
            not.setLeftConnective(con2.Copy());
            mainNand.setRightConnective(not.GetNandProposition());
            return mainNand;
        }
        public override string GetParseString()
        {
            return ">(" + con1.GetParseString() + "," + con2.GetParseString() + ")";
        }
        public override Connective Copy()
        {
            ConnectiveImplication temp = new ConnectiveImplication();
            temp.setLeftConnective(con1.Copy());
            temp.setRightConnective(con2.Copy());
            return temp;
        }
        public override bool IsTheSameAs(Connective con)
        {
            if (con is ConnectiveImplication)
            {
                ConnectiveTwo c = (ConnectiveTwo)con;

                if (con1.IsTheSameAs(c.Con1) && con2.IsTheSameAs(c.Con2)) { return true; }
            }
            return false;
        }
    }
}
