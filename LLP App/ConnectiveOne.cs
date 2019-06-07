using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    abstract class ConnectiveOne : Connective
    {
        protected Connective con1;

        public Connective Con1 { get { return con1; } }

        public abstract void setLeftConnective(Connective con);

        public override List<char> GetAllArguments()
        {
            List<char> fullList = new List<char>();
            if (con1 != null)
            {
                foreach (char arg in con1.GetAllArguments())
                {
                    if (!fullList.Contains(arg))
                    {
                        fullList.Add(arg);
                    }
                }
            }
            return fullList;
        }
        public override List<Connective> GetAllConnectives()
        {
            List<Connective> fullList = new List<Connective>();

            fullList.Add(this);
            if (con1 != null)
            {
                foreach (Connective con in con1.GetAllConnectives())
                {
                    fullList.Add(con);
                }
            }
            return fullList;
        }

        public override bool IsNormalProposition()
        {
            return (con1.IsNormalProposition());
        }
        public override bool ChangeLocalArgument(char a, char b)
        {
            return con1.ChangeLocalArgument(a, b);
        }
        public override List<char> GetAllLocalArguments()
        {
            return con1.GetAllLocalArguments();
        }

    }
}
