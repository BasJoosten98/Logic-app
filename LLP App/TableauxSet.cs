using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class TableauxSet
    {
        private List<TableauxSetElement> elements; //elements in this set
        private List<TableauxSet> sets; //next sets created from this one
        private bool isTautology;
        private int id;
        private static int idCounter = 0;

        public int ID { get { return this.id; } }
        public bool IsTautology { get { return this.isTautology; } }
        public List<TableauxSet> Sets { get { return this.sets; } }

        public TableauxSet(List<TableauxSetElement> SetElements)
        {
            this.elements = SetElements;
            this.sets = new List<TableauxSet>();
            this.id = idCounter;
            idCounter++;
        }
        
        //METHODS FOR TREE STRUCTURE IMAGE
        public List<TableauxSet> GetAllSets()
        {
            List<TableauxSet> temp = new List<TableauxSet>();
            temp.Add(this);
            foreach(TableauxSet ts in sets)
            {
                foreach(TableauxSet ts2 in ts.GetAllSets())
                {
                    temp.Add(ts2);
                }
            }
            return temp;
        }
        public string GetElementsAsString()
        {
            string holder = "";
            foreach(TableauxSetElement tse in elements)
            {
                holder += tse.Element.GetParseString() + "\n";
            }
            return holder;
        }

        //CALCULATE IF THIS IS A TAUTOLOGY
        public bool CalculateIsTautology()
        {
            if (sets.Count > 0)
            {
                bool tautology = true;
                foreach (TableauxSet ts in sets)
                {
                    if(ts.CalculateIsTautology() == false)
                    {
                        tautology = false;
                        //break;    //Can not break here, because then some ts will not have a valid IsTautology field
                    }
                }
                this.isTautology = tautology;
                return tautology;
            }
            isTautology = calculateIsTautologyWithOwnElements();
            return isTautology;
        }
        private bool calculateIsTautologyWithOwnElements()
        {

            Connective adaptedCon; 

            for(int i = 0; i < elements.Count; i++)
            {
                if(elements[i].Element is ConnectiveNot) //reference to child (ignore the not parent) OR check if argument == false
                {
                    ConnectiveNot not = (ConnectiveNot)elements[i].Element;
                    if(not.Con1 is ConnectiveArgument)
                    {
                        if(((ConnectiveArgument)not.Con1).Argument == '1')
                        {
                            return true;
                        }
                    }
                    adaptedCon = not.Con1;
                }
                else //add not as a parent OR check if argument == false
                {
                    if(elements[i].Element is ConnectiveArgument)
                    {
                        if (((ConnectiveArgument)elements[i].Element).Argument == '0')
                        {
                            return true;
                        }
                    }
                    adaptedCon = new ConnectiveNot();
                    ((ConnectiveNot)adaptedCon).setLeftConnective(elements[i].Element);
                }

                for(int j = i + 1; j< elements.Count; j++)
                {
                    if (elements[j].Element.IsTheSameAs(adaptedCon))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //CREATING NEXT SETS IF POSSIBLE
        public void CreateNextSets()
        {
            if (sets.Count == 0)
            {
                bool succes = false;
                if (tryApplyingAlfaRules())
                {
                    succes = true;
                }
                else if (tryApplyingBetaRules())
                {
                    succes = true;
                }

                if (succes)
                {
                    if (sets.Count == 0) { throw new Exception("Adding new sets failed"); }
                    foreach (TableauxSet ts in sets)
                    {
                        ts.CreateNextSets();
                    }
                }
            }
        }
        private bool tryApplyingAlfaRules()
        {
            List<Connective> results;
            foreach(TableauxSetElement tse in elements)
            {
                results = tse.ApplyAlfaTableauxRules();
                if(results.Count > 0) //applying was a succes
                {
                    addNewSet(results, tse);
                    return true;
                }
            }
            return false;
        }
        private bool tryApplyingBetaRules()
        {
            List<Connective> results;
            foreach (TableauxSetElement tse in elements)
            {
                results = tse.ApplyBetaTableauxRules();
                if (results.Count > 0) //applying was a succes
                {
                    if(results.Count != 2) { throw new Exception("Not possible"); }
                    addNewSet(new List<Connective>() { results[0] }, tse);
                    addNewSet(new List<Connective>() { results[1] }, tse);
                    return true;
                }
            }
            return false;
        }
        private void addNewSet(List<Connective> Connectives, TableauxSetElement SourceElement)
        {
            //copy elements except SourceElement
            List<TableauxSetElement> tempElements = new List<TableauxSetElement>();
            foreach (TableauxSetElement tse in elements)
            {
                if (tse != SourceElement)
                {
                    tempElements.Add(tse);
                }
            }

            //add new Elements created from SourceElement if such an element does not exist yet
            foreach (Connective con in Connectives)
            {
                bool sameFound = false;
                foreach(TableauxSetElement tse in tempElements)
                {
                    if (tse.Element.IsTheSameAs(con))
                    {
                        sameFound = true;
                        break;
                    }
                }
                if (!sameFound)
                {
                    TableauxSetElement newElement = new TableauxSetElement(con);
                    tempElements.Add(newElement);
                }
            }

            //add set
            TableauxSet newSet = new TableauxSet(tempElements);
            sets.Add(newSet);
        }
    }
}
