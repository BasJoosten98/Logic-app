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
        public override char GetLocalString()
        {
            return '~';
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
    }
}
