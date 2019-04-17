using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class ConnectiveImplication : Connective
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
    }
}
