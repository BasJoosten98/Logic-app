using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class QuantifierForAll : ConnectiveQuantifier
    {
        public override Connective Copy()
        {
            QuantifierForAll qe = new QuantifierForAll();
            qe.SetArgument(base.argument);
            qe.setLeftConnective(con1.Copy());
            return qe;
        }

        public override string GetInfix()
        {
            return "ForAll " + argument + " in [" + con1.GetInfix() + "]";
        }

        public override char GetLocalString()
        {
            return '@';
        }

        public override string GetParseString()
        {
            return "@" + argument + ".(" + con1.GetParseString() + ")";
        }

        public override bool IsTheSameAs(Connective con)
        {
            if (con is QuantifierForAll)
            {
                QuantifierForAll c = (QuantifierForAll)con;

                if (con1.IsTheSameAs(c.Con1)) { return true; }
            }
            return false;
        }
    }
}
