using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class ConnectiveBiImplication : ConnectiveTwo
    {
        public ConnectiveBiImplication()
        {

        }
        public override char GetLocalString()
        {
            return '=';
        }
        public override string GetInfix()
        {
            return "(" + con1.GetInfix() + " = " + con2.GetInfix() + ")";
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
            if (leftAnswer == rightAnswer)
            {
                return true;
            }
            return false;
        }

        public override Connective GetNandProposition()
        {
            ConnectiveNand mainNand = new ConnectiveNand();
            ConnectiveNand nand1 = new ConnectiveNand();
            ConnectiveNand nand2 = new ConnectiveNand();
            ConnectiveNot not1 = new ConnectiveNot();
            ConnectiveNot not2 = new ConnectiveNot();

            not1.setLeftConnective(con1.Copy());
            not2.setLeftConnective(con2.Copy());
            nand2.setLeftConnective(not1.GetNandProposition());
            nand2.setRightConnective(not2.GetNandProposition());
            nand1.setLeftConnective(con1.GetNandProposition());
            nand1.setRightConnective(con2.GetNandProposition());
            mainNand.setLeftConnective(nand1);
            mainNand.setRightConnective(nand2);
            return mainNand;
        }
        public override string GetParseString()
        {
            return "=(" + con1.GetParseString() + "," + con2.GetParseString() + ")";
        }
        public override Connective Copy()
        {
            ConnectiveBiImplication temp = new ConnectiveBiImplication();
            temp.setLeftConnective(con1.Copy());
            temp.setRightConnective(con2.Copy());
            return temp;
        }
        public override bool IsTheSameAs(Connective con)
        {
            if (con is ConnectiveBiImplication)
            {
                ConnectiveTwo c = (ConnectiveTwo)con;

                if (con1.IsTheSameAs(c.Con1) && con2.IsTheSameAs(c.Con2)) { return true; }
            }
            return false;
        }
    }
}
