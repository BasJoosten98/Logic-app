using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LLP_App
{
    class TableauxSet
    {

        private List<TableauxSetElement> elements; //elements in this set
        private List<TableauxSet> sets; //next sets created from this one
        private List<char> usedArguments;
        private List<char> availableArguments;
        private bool isTautology;
        private int id;
        private static int idCounter = 0;
        private static Random rand = new Random();

        public int ID { get { return this.id; } }
        public bool IsTautology { get { return this.isTautology; } }
        public List<TableauxSet> Sets { get { return this.sets; } }

        public TableauxSet(List<TableauxSetElement> SetElements)
        {
            this.elements = SetElements;
            this.sets = new List<TableauxSet>();
            this.id = idCounter;
            this.usedArguments = new List<char>();
            this.availableArguments = new List<char>();
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
            if (usedArguments.Count > 0) {
                holder += "<";
                for (int i = 0; i < usedArguments.Count - 1; i++)
                {
                    holder += usedArguments[i] + ", ";
                }
                holder += usedArguments[usedArguments.Count - 1] + "> \n";
            }
            foreach (TableauxSetElement tse in elements)
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
        public void CreateNextSets(List<char> UsedArguments, List<char> AvailableArguments, bool checkInBetween)
        {
            if (sets.Count == 0)
            {
                this.usedArguments = UsedArguments;
                this.availableArguments = AvailableArguments;

                if (checkInBetween)
                {
                    if (calculateIsTautologyWithOwnElements())
                    {
                        isTautology = true;
                        return;
                    }
                }

                //MAKE COPY OF ARGUMENTS
                List<char> copyUsed = new List<char>();
                List<char> copyAvailable = new List<char>();
                foreach (char c in UsedArguments)
                {
                    copyUsed.Add(c);
                }
                foreach (char c in AvailableArguments)
                {
                    copyAvailable.Add(c);
                }

                //TRY APPLYING RULES
                char usedRule = 'x';
                bool succes = false;
                if (tryApplyingAlfaRules())
                {
                    succes = true;
                    usedRule = 'a';
                }
                else if (tryApplyingDeltaRules(copyUsed, copyAvailable))
                {
                    succes = true;
                    usedRule = 'd';
                }
                else if (tryApplyingBetaRules())
                {
                    succes = true;
                    usedRule = 'b';
                }
                else if (tryApplyingGammaRules(copyUsed))
                {
                    succes = true;
                    usedRule = 'g';
                    
                }
                string holder = "Used rule: " + usedRule + " < ";
                foreach (char c in usedArguments) { holder += c + " "; }
                holder += ">";
                Console.WriteLine(holder);
                Console.WriteLine(GetElementsAsString());

                //CREATING NEXT SETS
                if (succes)
                {
                    if (sets.Count == 0) { throw new Exception("Adding new sets failed"); }
                    foreach (TableauxSet ts in sets)
                    {
                        ts.CreateNextSets(copyUsed, copyAvailable, checkInBetween);
                    }
                }
            }
        }
        private bool tryApplyingAlfaRules()
        {
            //MessageBox.Show("Alfa");
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
        private bool tryApplyingDeltaRules(List<char> used, List<char> available)
        {
            //MessageBox.Show("Delta");
            List<Connective> results;
            List<TableauxSetElement> temp = new List<TableauxSetElement>();
            foreach(TableauxSetElement tse in elements)
            {
                temp.Add(tse);
            }
            while(temp.Count > 0)
            {
                TableauxSetElement tse = temp[rand.Next(0, temp.Count)];

                //MAKE COPY OF ARGUMENTS
                List<char> copyUsed = new List<char>();
                List<char> copyAvailable = new List<char>();
                foreach (char c in used)
                {
                    copyUsed.Add(c);
                }
                foreach (char c in available)
                {
                    copyAvailable.Add(c);
                }

                results = tse.ApplyDeltaTableauxRules(copyUsed, copyAvailable);
                if (results.Count > 0) //applying was a succes
                {
                    if(results.Count != 1) { throw new Exception("Not possible"); }
                    if (addNewSet(new List<Connective>() { results[0] }, tse))
                    {
                        used.Clear();
                        foreach(char c in copyUsed)
                        {
                            used.Add(c);
                        }
                        available.Clear();
                        foreach(char c in copyAvailable)
                        {
                            available.Add(c);
                        }
                        return true;
                    }                 
                }
                temp.Remove(tse);
            }
            return false;
        }
        private bool tryApplyingBetaRules()
        {
            //MessageBox.Show("Beta");
            List<Connective> results;
            foreach (TableauxSetElement tse in elements)
            {
                results = tse.ApplyBetaTableauxRules();
                if (results.Count > 0) //applying was a succes
                {
                    if (results.Count != 2) { throw new Exception("Not possible"); }
                    addNewSet(new List<Connective>() { results[0] }, tse);
                    addNewSet(new List<Connective>() { results[1] }, tse);
                    return true;
                }
            }
            return false;
        }
        private bool tryApplyingGammaRules(List<char> used)
        {
            //MessageBox.Show("Gamma");
            List<Connective> results;
            List<TableauxSetElement> temp = new List<TableauxSetElement>();
            foreach (TableauxSetElement tse in elements)
            {
                temp.Add(tse);
            }
            while (temp.Count > 0)
            {
                TableauxSetElement tse = temp[rand.Next(0, temp.Count)];

                results = tse.ApplyGammaTableauxRules(used);
                if (results.Count > 0) //applying was a succes
                {
                    if(addNewSet(results, null))
                    {
                        return true;
                    }                   
                }
                temp.Remove(tse);
            }
            return false;
        }
        private bool addNewSet(List<Connective> Connectives, TableauxSetElement SourceElement)
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
            bool addedAtLeastOne = false;
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
                    addedAtLeastOne = true;
                }
            }

            //add set
            if (addedAtLeastOne || SourceElement != null) //added one or removed one
            {
                TableauxSet newSet = new TableauxSet(tempElements);
                sets.Add(newSet);
            }
            return (addedAtLeastOne || SourceElement != null);
        }
    }
}
