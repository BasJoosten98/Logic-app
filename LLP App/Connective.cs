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

        public int ID { get { return this.id; } }

        public Connective()
        {
            this.id = idCounter;
            idCounter++;
        }

        public abstract List<Connective> GetAllConnectives();
        public abstract char GetLocalString();
        public abstract List<char> GetAllArguments();
        public abstract bool GetAnswer(TruthtableRow row);
        public abstract string GetInfix();
        public abstract string GetParseString();
        public abstract Connective GetNandProposition();
        public abstract Connective Copy();
        public abstract bool IsTheSameAs(Connective con);
        public abstract bool IsNormalProposition();
        public abstract void ChangeLocalArgument(char a, char b);
    }
}
