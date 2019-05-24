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
        private TableauxSet startTableaux;

        public bool IsTautology { get { return startTableaux.IsTautology; } }

        public TableauxHolder(TableauxSet StartSet)
        {
            this.startTableaux = StartSet;
            this.startTableaux.CreateNextSets();
            this.startTableaux.CalculateIsTautology();
        }
        public  TableauxHolder(string Proposition)
        {
            Connective proposition = PropositionReader.ReadPropositionString(Proposition);

            ConnectiveNot notPorposition = new ConnectiveNot();
            notPorposition.setLeftConnective(proposition);

            TableauxSetElement tse = new TableauxSetElement(notPorposition);
            this.startTableaux = new TableauxSet(new List<TableauxSetElement>() { tse });
            this.startTableaux.CreateNextSets();
            this.startTableaux.CalculateIsTautology();
        }

        //DISPLAY TABLEAUX TREE STRUCTURE
        public void ShowTreeStructure()
        {
            string path = PropositionReader.CreateStructurePicture(startTableaux);
            Process.Start(@path);
        }
    }
}
