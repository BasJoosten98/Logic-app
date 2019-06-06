using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    abstract class ConnectiveTwo : ConnectiveOne
    {
        protected Connective con2;

        public Connective Con2 { get { return con2; } }

        public abstract void setRightConnective(Connective con);

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
            if (con2 != null)
            {
                foreach (char arg in con2.GetAllArguments())
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
            if (con2 != null)
            {
                foreach (Connective con in con2.GetAllConnectives())
                {
                    fullList.Add(con);
                }
            }
            return fullList;
        }
        public override bool IsNormalProposition()
        {
            return (con1.IsNormalProposition() && con2.IsNormalProposition());
        }
        public override void ChangeLocalArgument(char a, char b)
        {
            con1.ChangeLocalArgument(a, b);
            con2.ChangeLocalArgument(a, b);
        }
    }
}
