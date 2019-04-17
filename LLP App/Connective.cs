using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    abstract class Connective
    {
        private static int idCounter = 0;
        private int id;
        protected Connective con1;
        protected Connective con2;

        public Connective Con1 { get { return con1; } }
        public Connective Con2 { get { return con2; } } //give Null when not have any
        public int ID { get { return this.id; } }

        public Connective()
        {
            this.id = idCounter;
            con1 = null;
            con2 = null;
            idCounter++;
        }

        public virtual void setLeftConnective(Connective con)
        {
            throw new NotImplementedException();
        }
        public virtual void setRightConnective(Connective con)
        {
            throw new NotImplementedException();
        }
        public List<Connective> GetAllConnectives()
        {
            List<Connective> fullList = new List<Connective>();

            fullList.Add(this);
            if(con1 != null)
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
        public abstract char GetLocalString();
        public virtual List<char> GetAllArguments()
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
        public abstract bool GetAnswer(TruthtableRow row);
        public abstract string GetInfix();
    }
}
