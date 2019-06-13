using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class TableauxHolder
    {
        private static char[] SmallArguments = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private List<char> usedArguments;
        private List<char> availableArguments;
        private TableauxSet startTableaux;
        private bool isNormalProposition;
        private string infixString;

        public bool IsTautology { get { return startTableaux.IsTautology; } }

        public  TableauxHolder(string Proposition)
        {
            Connective proposition = PropositionReader.ReadPropositionString(Proposition);
            infixString = proposition.GetInfix();

            ConnectiveNot notPorposition = new ConnectiveNot();
            notPorposition.setLeftConnective(proposition);

            TableauxSetElement tse = new TableauxSetElement(notPorposition);
            this.startTableaux = new TableauxSet(new List<TableauxSetElement>() { tse });
            isNormalProposition = notPorposition.IsNormalProposition();
            if(!notPorposition.AreLocalArgumentsMatching(new List<char>(), new List<char>())) { throw new Exception("Local Arguments are mismatching or there are quantifiers with the same Local Argument"); }
            calculateFreeArguments(notPorposition);
            Console.WriteLine("Succesfully parsed proposition: " + Proposition);
            Console.WriteLine("Creating tableaux nodes...   (In-Progress Feedback: " + TableauxSet.provideFeedback + ")");
            this.startTableaux.CreateNextSets(new List<char>(), availableArguments, true); //Can be (!isNormalProposition) OR (true)
            this.startTableaux.CalculateIsTautology();
            Console.WriteLine("Succesfully created all teableaux nodes");
        }
        //CALCULATE USED/AVAILABLE ARGUMENTS
        private void calculateFreeArguments(Connective startCon)
        {
            usedArguments = startCon.GetAllLocalArguments();
            availableArguments = new List<char>();
            foreach(char c in SmallArguments)
            {
                if (!usedArguments.Contains(c))
                {
                    availableArguments.Add(c);
                }
            }
        }

        public string GetInfixString()
        {
            return infixString;
        }

        //DISPLAY TABLEAUX TREE STRUCTURE
        public void ShowTreeStructure()
        {
            string path = PropositionReader.CreateStructurePicture(startTableaux);
            Process.Start(@path);
        }
    }
}
