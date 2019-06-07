using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class QuantifierExists : ConnectiveQuantifier
    {
        public override Connective Copy()
        {
            QuantifierExists qe = new QuantifierExists();
            qe.SetArgument(base.argument);
            qe.setLeftConnective(con1.Copy());
            return qe;
        }

        public override string GetInfix()
        {
            return "EX." + argument +"[" + con1.GetInfix() + "]";
        }

        public override char GetLocalString()
        {
            return '!';
        }

        public override string GetParseString()
        {
            return "!" + argument + ".(" + con1.GetParseString() + ")";
        }

        public override bool IsTheSameAs(Connective con)
        {
            if (con is QuantifierExists)
            {
                QuantifierExists c = (QuantifierExists)con;

                if (con1.IsTheSameAs(c.Con1)) { return true; }
            }
            return false;
        }
    }
}
