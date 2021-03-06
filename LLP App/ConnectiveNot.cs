using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class ConnectiveNot : ConnectiveOne
    {
        public ConnectiveNot()
        {

        }
        public override string GetLocalString()
        {
            return "~";
        }
        public override string GetInfix()
        {
            return "~(" + con1.GetInfix() + ")";
        }
        public override void setLeftConnective(Connective con)
        {
            if (con != null)
            {
                con1 = con;
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
            return false;
        }

        public override Connective GetNandProposition()
        {
            ConnectiveNand nand = new ConnectiveNand();
            nand.setLeftConnective(con1.GetNandProposition());
            nand.setRightConnective(con1.GetNandProposition());
            return nand;
        }
        public override string GetParseString()
        {
            return "~(" + con1.GetParseString() + ")";
        }
        public override Connective Copy()
        {
            ConnectiveNot temp = new ConnectiveNot();
            temp.setLeftConnective(con1.Copy());
            return temp;
        }
        public override bool IsTheSameAs(Connective con)
        {
            if (con is ConnectiveNot)
            {
                ConnectiveOne c = (ConnectiveOne)con;

                if (con1.IsTheSameAs(c.Con1)) { return true; }
            }
            return false;
        }
    }
}
