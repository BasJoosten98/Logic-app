using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    class ConnectiveHolder
    {
        private Connective startConnective;

        public ConnectiveHolder(Connective con)
        {
            if(con != null)
            {
                startConnective = con;
            }
            else { throw new NullReferenceException(); }
        }
        public ConnectiveHolder(string proposition)
        {
            if(proposition != null)
            {
                Connective con = PropositionReader.ReadPropositionString(proposition);
                if (con != null)
                {
                    startConnective = con;
                }
                else { throw new NullReferenceException(); }
            }
            else { throw new NullReferenceException(); }
        }

        public void ShowTreeStructure()
        {
            string path = PropositionReader.CreateStructurePicture(startConnective);
            Process.Start(@path);
        }

        public List<char> GetListOfAllArguments()
        {
            List<char> fullList = startConnective.GetAllArguments();
            List<char> orderedList = new List<char>();
            int chosenIndex = 0;

            //order alphabeticaly
            for (int i = 0; i < fullList.Count; i++)
            {
                if (i != chosenIndex)
                {
                    int result = string.Compare(fullList[chosenIndex].ToString(), fullList[i].ToString());
                    if (result > 0) // compare(B,A) 
                    {
                        chosenIndex = i;
                        i = 0;
                    }
                }
                if(i == fullList.Count - 1) //end reached
                {
                    orderedList.Add(fullList[chosenIndex]);
                    fullList.RemoveAt(chosenIndex);
                    chosenIndex = 0;
                    i = -1;
                }
            }
            return orderedList;
        }
        public string getAllArgumentsString()
        {
            List<char> fullList = GetListOfAllArguments();
            if(fullList.Count == 0) { return ""; }
            string holder = "";
            for(int i = 0; i < fullList.Count - 1; i++)
            {
                holder += fullList[i] + ", ";
            }
            holder += fullList[fullList.Count - 1];
            return holder;
        }
        public bool GetTruthtableRowAnswer(TruthtableRow row)
        {
            return startConnective.GetAnswer(row);
        }
        public string GetInfixString()
        {
            return startConnective.GetInfix();
        }
        public string GetParseString()
        {
            return startConnective.GetParseString();
        }

        public ConnectiveHolder GetNandHolder()
        {
            Connective startCon = startConnective.GetNandProposition();
            ConnectiveHolder temp = new ConnectiveHolder(startCon);
            return temp;
        }
    }
}
