using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class ConnectiveNot : Connective
    {
        public ConnectiveNot()
        {

        }
        public override char GetLocalString()
        {
            return '~';
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
            throw new Exception("Connective NOT does not have a second input");
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
