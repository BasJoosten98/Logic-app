using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    static class PropositionReader
    {
        private static List<char> PropositionList;
        private static char[] ConnectiveTypes = new char[] { '~', '=', '|', '>', '&', '%', '@', '!' };
        private static char[] Arguments = "ABCDEFGHIJKLMNOPQRSTUVWXYZ10".ToCharArray();
        private static char[] SmallArguments = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        private static Random rand = new Random();

        //READING PROPOSITIONS AND GENERATING CONNECTIVE TREE
        public static Connective ReadPropositionString(string proposition)
        {
            PropositionList = proposition.ToList<char>();
            Connective result = null;
            result = readPropositionStringRec();
            return result;
        }
        private static Connective readPropositionStringRec()
        {
            Connective Head = null;
            int index = 0;
            

            while(PropositionList.Count != 0)
            {
                switch (PropositionList[0])
                {
                    case ',':
                        //ERROR CHECKING
                        if (Head != null)
                        {
                            if(Head is ConnectiveNot || Head is ConnectiveArgument)
                            {
                                throw new Exception("'" + Head.GetLocalString() + "' does not need >2 inputs, please remove ','");
                            }
                            if (Head is ConnectiveOne)
                            {
                                if (((ConnectiveOne)Head).Con1 == null)
                                {
                                    throw new Exception("'" + Head.GetLocalString() + "' is missing a left connective");
                                }
                            }
                        }
                        else { throw new Exception("',' does not belong to any connective"); }

                        //SET INDEX
                        index++;
                        PropositionList.RemoveAt(0);
                        break;
                    case '(':
                        //ERROR CHECKING
                        if (Head == null)
                        {
                            throw new Exception("'(' does not belong to any connective");
                        }
                        if (Head is ConnectiveArgument)
                        {
                            throw new Exception("'" + Head.GetLocalString() + "' has no inputs, please remove '('");
                        }

                        //SET INDEX
                        index = 1;
                        PropositionList.RemoveAt(0);
                        break;
                    case ')':
                        //ERROR CHECKING
                        if (index == 1)
                        {
                            if (Head is ConnectiveArgument)
                            {
                                throw new Exception("'" + Head.GetLocalString() + "' has no inputs, please remove ')'");
                            }
                            if (Head is ConnectiveOne)
                            {
                                if (((ConnectiveOne)Head).Con1 == null)
                                {
                                    throw new Exception("'" + Head.GetLocalString() + "' is missing a left connective");
                                }
                            }
                            if(Head is ConnectiveFunction)
                            {
                                if (((ConnectiveFunction)Head).LocalArguments.Count == 0)
                                {
                                    throw new Exception("Function '" + Head.GetLocalString() + "' is missing parameters");
                                }
                            }
                        }
                        else if(index == 2)
                        {
                            if (Head is ConnectiveNot || Head is ConnectiveArgument)
                            {
                                throw new Exception("'" + Head.GetLocalString() + "' does not need 2 inputs, please remove ')'");
                            }
                            if (Head is ConnectiveTwo)
                            {
                                if (((ConnectiveTwo)Head).Con2 == null)
                                {
                                    throw new Exception("'" + Head.GetLocalString() + "' is missing a right connective");
                                }
                            }
                        }

                        PropositionList.RemoveAt(0);
                        return Head;
                    default:
                        if (ConnectiveTypes.Contains(PropositionList[0])) //type found
                        {
                            Connective con;
                            switch (index)
                            {
                                case 0:
                                    if (Head == null)
                                    {
                                        con = getConnectiveByType(PropositionList[0]);
                                        Head = con;
                                        PropositionList.RemoveAt(0);
                                        if(Head is ConnectiveQuantifier)
                                        {
                                            ConnectiveQuantifier cq = (ConnectiveQuantifier)Head;
                                            if (SmallArguments.Contains(PropositionList[0]))
                                            {
                                                cq.SetArgument(PropositionList[0]);
                                                PropositionList.RemoveAt(0);
                                                PropositionList.RemoveAt(0);
                                            }
                                            else { throw new Exception("Invalid local variable for quantifier: " + PropositionList[0]); }
                                        }
                                    }
                                    else { throw new Exception("'" + PropositionList[0] + "' cannot be placed after another connective or argument"); }
                                    break;
                                case 1:
                                    if(Head != null)
                                    {
                                        if (!(Head is ConnectiveArgument || Head is ConnectiveFunction))
                                        {
                                            con = readPropositionStringRec();
                                            ((ConnectiveOne)Head).setLeftConnective(con);
                                        }
                                        else
                                        {
                                            throw new Exception("Argument/Function does not need any connectives as input");
                                        }
                                    }
                                    else { throw new Exception("Internal index problem occured (index = 1, Head = null)"); }
                                    break;
                                case 2:
                                    if (Head != null)
                                    {
                                        if (Head is ConnectiveTwo)
                                        {
                                            con = readPropositionStringRec();
                                            ((ConnectiveTwo)Head).setRightConnective(con);
                                        }
                                        else
                                        {
                                            throw new Exception("Argument/Function and/or negation/quantifiers does not need second connectives input");
                                        }
                                    }
                                    else { throw new Exception("Internal index problem occured (index = 2, Head = null)"); }
                                    break;
                                default:
                                    throw new Exception("Internal index problem occured (index > 2)");
                            }
                        }
                        else if (Arguments.Contains(PropositionList[0])) //argument found
                        {
                            Connective con;
                            con = new ConnectiveArgument(PropositionList[0]);
                            if (PropositionList.Count > 1)
                            {
                                if (PropositionList[1] == '(')
                                {
                                    con = new ConnectiveFunction();
                                    ((ConnectiveFunction)con).SetFunctionChar(PropositionList[0]);
                                }
                            }

                            switch (index)
                            {
                                case 0:
                                    if (Head == null)
                                    {
                                        PropositionList.RemoveAt(0);
                                        Head = con;
                                    }
                                    else { throw new Exception("'" + PropositionList[0] + "' cannot be placed after another connective or argument"); }
                                    break;
                                case 1:
                                    if (Head != null)
                                    {
                                        if (!(Head is ConnectiveArgument || Head is ConnectiveFunction))
                                        {
                                            if(con is ConnectiveFunction)
                                            {
                                                con = readPropositionStringRec();
                                            }
                                            else
                                            {
                                                PropositionList.RemoveAt(0);
                                            }
                                            ((ConnectiveOne)Head).setLeftConnective(con);
                                            
                                        }
                                        else
                                        {
                                            throw new Exception("Argument does not need any input");
                                        }                                        
                                    }
                                    else { throw new Exception("Internal index problem occured (index = 1, Head = null)"); }
                                    break;
                                case 2:
                                    if (Head != null)
                                    {
                                        if (!(Head is ConnectiveArgument || Head is ConnectiveFunction))
                                        {
                                            if (con is ConnectiveFunction)
                                            {
                                                con = readPropositionStringRec();
                                            }
                                            ((ConnectiveTwo)Head).setRightConnective(con);
                                            PropositionList.RemoveAt(0);
                                        }
                                        else
                                        {
                                            throw new Exception("Argument does not need any input");
                                        }
                                    }
                                    else { throw new Exception("Internal index problem occured (index = 2, Head = null)"); }
                                    break;
                                default:
                                    throw new Exception("Internal index problem occured (index > 2)");
                            }
                        }
                        else if (SmallArguments.Contains(PropositionList[0]))
                        {
                            if(Head != null)
                            {
                                if(Head is ConnectiveFunction)
                                {
                                    ConnectiveFunction cf = (ConnectiveFunction)Head;
                                    cf.AddArgument(PropositionList[0]);
                                    PropositionList.RemoveAt(0);
                                }
                                else
                                {
                                    throw new Exception("Small Argument found outside function/quatifier: " + PropositionList[0]);
                                }
                            }
                            else
                            {
                                throw new Exception("Small Argument found outside function/quatifier: " + PropositionList[0]);
                            }
                        }
                        else if(PropositionList[0] != ' ') //UNKNOWN (no space)
                        {
                            throw new Exception("Unknown character found, char: '" + PropositionList[0] + "'. Please remove it!");
                        }
                        else //SPACE
                        {
                            PropositionList.RemoveAt(0);
                        }
                        break;
                }
            }
            //Final check for main connective
            if(Head != null)
            {
                if(!(Head is ConnectiveArgument))
                {
                    if (Head is ConnectiveNot)
                    {
                        if (((ConnectiveOne)Head).Con1 == null) { throw new Exception("'" + Head.GetLocalString() + "' is missing a left connective"); }
                    }
                    else
                    {
                        if (((ConnectiveTwo)Head).Con1 == null) { throw new Exception("'" + Head.GetLocalString() + "' is missing a left connective"); }
                        if (((ConnectiveTwo)Head).Con2 == null) { throw new Exception("'" + Head.GetLocalString() + "' is missing a right connective"); }
                    }
                }
            }
            return Head;
        }
        private static Connective getConnectiveByType(char Type)
        {
            switch (Type)
            {
                case '>':
                    return new ConnectiveImplication();
                case '=':
                    return new ConnectiveBiImplication();
                case '|':
                    return new ConnectiveOr();
                case '~':
                    return new ConnectiveNot();
                case '&':
                    return new ConnectiveAnd();
                case '%':
                    return new ConnectiveNand();
                case '@':
                    return new QuantifierForAll();
                case '!':
                    return new QuantifierExists();
                default:
                    throw new Exception("No type found for char: '" + Type + "'.");
            }

        }

        //CREATING CONNECTIVE/TABLEAUX TREE PICTURE
        public static string CreateStructurePicture(Connective startCon)
        {
            if(startCon == null) { throw new NullReferenceException(); }
            //Console.WriteLine("PropositionReader: creating structure picture");
            List<Connective> allConnectives = startCon.GetAllConnectives();

            FileStream fs;
            StreamWriter sw;
            string fileName = "abc.dot";
            try
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.WriteLine("graph calculus {");
                sw.WriteLine("node [ fontname = \"Arial\" ]");

                for (int i = 0; i < allConnectives.Count; i++)
                {
                    sw.WriteLine("node" + (allConnectives[i].ID) + " [ label = \"" + allConnectives[i].GetLocalString() + "\" ]");
                    //if (allNodes[i].Parent != null)
                    //{
                    //    sw.WriteLine("node" + (allNodes[i].Parent.ID) + " -- node" + (allNodes[i].ID));
                    //}
                    //if (allConnectives[i].Con1 != null)
                    //{
                    //    sw.WriteLine("node" + (allConnectives[i].ID) + " -- node" + (allConnectives[i].Con1.ID));
                    //}
                    //if (allConnectives[i].Con2 != null)
                    //{
                    //    sw.WriteLine("node" + (allConnectives[i].ID) + " -- node" + (allConnectives[i].Con2.ID));
                    //}
                    if (allConnectives[i] is ConnectiveOne)
                    {
                        sw.WriteLine("node" + (allConnectives[i].ID) + " -- node" + ((ConnectiveOne)allConnectives[i]).Con1.ID);
                    }
                    if (allConnectives[i] is ConnectiveTwo)
                    {
                        sw.WriteLine("node" + (allConnectives[i].ID) + " -- node" + ((ConnectiveTwo)allConnectives[i]).Con2.ID);
                    }

                }
                sw.WriteLine("}");
                sw.Close();

            }
            catch (IOException)
            {
                throw new Exception("Structure picture failed");
            }
            return CreateDotPNG(fileName);
        }
        public static string CreateStructurePicture(TableauxSet startSet)
        {
            if (startSet == null) { throw new NullReferenceException(); }
            //Console.WriteLine("PropositionReader: creating structure picture");
            List<TableauxSet> allSets = startSet.GetAllSets();

            FileStream fs;
            StreamWriter sw;
            string fileName = "abc.dot";
            try
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);
                sw.WriteLine("graph calculus {");
                sw.WriteLine("node [ fontname = \"Arial\" ]");

                for (int i = 0; i < allSets.Count; i++)
                {               
                    if (allSets[i].IsTautology) { sw.WriteLine("node" + (allSets[i].ID) + " [shape=box,color=red,label = \"" + allSets[i].GetElementsAsString() + "\"];"); }
                    else { sw.WriteLine("node" + (allSets[i].ID) + " [shape=box,label = \"" + allSets[i].GetElementsAsString() + "\"];"); }

                    foreach(TableauxSet ts in allSets[i].Sets)
                    {
                        sw.WriteLine("node" + (allSets[i].ID) + " -- node" + ts.ID);
                    }
                }
                sw.WriteLine("}");
                sw.Close();

            }
            catch (IOException)
            {
                throw new Exception("Structure picture failed");
            }
            return CreateDotPNG(fileName);
        }
        private static string CreateDotPNG(string filename)
        {
            Process dot = new Process();
            dot.StartInfo.FileName = "dot.exe";
            dot.StartInfo.Arguments = "-Tpng -oabc.png abc.dot";
            dot.Start();
            dot.WaitForExit();
            //Console.WriteLine("PropositionReader: structure picture created succesfully");
            return "abc.png";
        }

        //CREATE RANDOM PROPOSITION STRING
        public static string CreateRandomPropositionString()
        {
            int totalDives = rand.Next(0, 11);
            int maxDifferentArguments = (totalDives-1) / 2;
            return createRandomPorpositionStringRec(totalDives, maxDifferentArguments);
        }
        private static string createRandomPorpositionStringRec(int totalDives, int maxDifferentArguments) 
        {
            //get random connective
            if (totalDives == 0)
            {
                char randomArgument = Arguments[rand.Next(0, maxDifferentArguments + 1)];
                if (rand.Next(1, 11) <= 2) //chance of getting 1 or 0
                {
                    if (rand.Next(0, 2) == 0) { randomArgument = '0'; }
                    else { randomArgument = '1'; }
                }               
                return randomArgument.ToString();
            }
            int ConnectiveTypeIndex = rand.Next(0, ConnectiveTypes.Length); 
            string holder = ConnectiveTypes[ConnectiveTypeIndex].ToString();
            totalDives--;

            //divide dives over input(s)
            if(holder == "~") //only 1 input possible
            {
                holder += "(";
                holder += createRandomPorpositionStringRec(totalDives, maxDifferentArguments);
                holder += ")";
            }
            else //2 inputs possible
            {
                int dives1 = rand.Next(0, totalDives + 1);
                int dives2 = totalDives - dives1;
                holder += "(";
                holder += createRandomPorpositionStringRec(dives1, maxDifferentArguments);
                holder += ",";
                holder += createRandomPorpositionStringRec(dives2, maxDifferentArguments);
                holder += ")";
            }
            return holder;
        }

        //GIVE SIMPLE TRUTHTABLEROW (WITH * ARGUMENT VALUES) AND GET ALL NON SIMPLE TRUTHTABLE ROWS RETURNED (NO * ARGUMENT VALUES)
        public static List<TruthtableRow> GetSubrowsOfMainRow(TruthtableRow main)
        {
            return getSubrowsOfMainRowRec(main, 0);
        }
        private static List<TruthtableRow> getSubrowsOfMainRowRec(TruthtableRow main, int argIndex)
        {
            if(argIndex == main.Arguments.Count) { return new List<TruthtableRow>() { main }; }
            List<TruthtableRow> subsets = new List<TruthtableRow>();
            if(main.Arguments[argIndex].Value == '*')
            {
                //copy Main and adjust it
                List<TruthtableRowArgument> newMainArguments = new List<TruthtableRowArgument>();
                for(int i = 0; i < main.Arguments.Count; i++) //copy arguments, but change current argument (on index)
                {
                    if(i == argIndex)
                    {
                        newMainArguments.Add(new TruthtableRowArgument(main.Arguments[i].Argument, '1'));
                    }
                    else
                    {
                        newMainArguments.Add(main.Arguments[i]);
                    }
                }
                TruthtableRow newMain = new TruthtableRow(newMainArguments);
                newMain.RowValue = main.RowValue;
                //get subRows if current arg == '1'
                foreach(TruthtableRow r in getSubrowsOfMainRowRec(newMain, argIndex + 1))
                {
                    subsets.Add(r);
                }
                TruthtableRowArgument[] newMainArguments2 = new TruthtableRowArgument[newMainArguments.Count];
                newMainArguments.CopyTo(newMainArguments2);
                newMainArguments2[argIndex] = new TruthtableRowArgument(main.Arguments[argIndex].Argument, '0');
                TruthtableRow newMain2 = new TruthtableRow(newMainArguments2.ToList());
                newMain2.RowValue = main.RowValue;
                //get subRows if current arg == '0'
                foreach (TruthtableRow r in getSubrowsOfMainRowRec(newMain2, argIndex + 1))
                {
                    subsets.Add(r);
                }
            }
            else if (main.Arguments[argIndex].Value == '1')
            {
                foreach (TruthtableRow r in getSubrowsOfMainRowRec(main, argIndex + 1))
                {
                    subsets.Add(r);
                }
            }
            else if (main.Arguments[argIndex].Value == '0')
            {
                foreach (TruthtableRow r in getSubrowsOfMainRowRec(main, argIndex + 1))
                {
                    subsets.Add(r);
                }
            }
            else
            {
                throw new Exception("Unknown char");
            }
            return subsets;
        }

        //READ TABLE AND RETURN DISJUNCTIVE NORMAL FORM AND IT'S PARSE STRING
        public static string[] readDisjunctiveForm(List<TruthtableRow> rows)
        {
            string disHolder = "";
            List<string> parseRowsOr = new List<string>();
            List<string> parseRowsAnd = new List<string>();
            List<char> arguments = new List<char>();
            bool[] argumentUsed;
            string[] disjunctiveFormAndParse = new string[2];

            //GET ARGUMENTS
            foreach (TruthtableRowArgument arg in rows[0].Arguments)
            {
                arguments.Add(arg.Argument);
            }
            argumentUsed = new bool[arguments.Count];

            //GO THROUGH EVERY ROW AND SAVE INFORMATION FOR PARSE AND DISJUNCTIVE
            foreach (TruthtableRow r in rows)
            {
                if (r.RowValue == '1')
                {
                    string disHolder2 = "";

                    //OBTAIN INFORMATION ABOUT CURRENT ROW
                    foreach (TruthtableRowArgument arg in r.Arguments)
                    {
                        if (arg.Value != '*') //NOT ALLOWED (can only be 1 or 0)
                        {
                            if (disHolder2 != "")
                            {
                                disHolder2 += " & ";
                            }
                            if (arg.Value == '1')
                            {
                                disHolder2 += arg.Argument;
                                parseRowsAnd.Add(arg.Argument.ToString());
                            }
                            else
                            {
                                disHolder2 += "~" + arg.Argument;
                                parseRowsAnd.Add("~(" + arg.Argument.ToString() + ")");
                            }
                            int index = arguments.IndexOf(arg.Argument);
                            argumentUsed[index] = true;
                        }
                    }
                    //SAVE INFORMATION AS DISJUNCTIVE PART
                    if (disHolder2 != "")
                    {
                        if (disHolder != "")
                        {
                            disHolder += " | ";
                        }
                        disHolder += "(";
                        disHolder += disHolder2 + ")";
                    }
                    //SAVE INFORMATION AS PARSE PART
                    if (parseRowsAnd.Count != 0)
                    {
                        if (parseRowsAnd.Count == 1) { parseRowsOr.Add(parseRowsAnd[0]); }
                        else
                        {
                            string parseHolder2 = "";
                            for (int i = parseRowsAnd.Count - 1; i >= 0; i--)
                            {
                                if (i >= 2)
                                {
                                    parseHolder2 += "&(" + parseRowsAnd[parseRowsAnd.Count - i - 1] + ",";
                                }
                                else
                                {
                                    parseHolder2 += "&(" + parseRowsAnd[parseRowsAnd.Count - i - 1] + "," + parseRowsAnd[parseRowsAnd.Count - i];
                                    break;
                                }
                            }
                            for (int i = 1; i <= parseRowsAnd.Count - 1; i++)
                            {
                                parseHolder2 += ")";
                            }
                            parseRowsOr.Add(parseHolder2);
                        }
                    }
                    parseRowsAnd = new List<string>();
                }
            }

            //FINALIZE PARSE PART BY ADDING ALL AND-STRINGS WITH OR-STRINGS TO EACH OTHER
            string parseHolder = "";
            if (parseRowsOr.Count == 0) { parseHolder = rows[0].RowValue.ToString(); }
            else if (parseRowsOr.Count == 1) { parseHolder = parseRowsOr[0]; }
            else
            {
                for (int i = parseRowsOr.Count - 1; i >= 0; i--)
                {
                    if (i >= 2)
                    {
                        parseHolder += "|(" + parseRowsOr[parseRowsOr.Count - i - 1] + ",";
                    }
                    else
                    {
                        parseHolder += "|(" + parseRowsOr[parseRowsOr.Count - i - 1] + "," + parseRowsOr[parseRowsOr.Count - i];
                        break;
                    }
                }
                for (int i = 1; i <= parseRowsOr.Count - 1; i++)
                {
                    parseHolder += ")";
                }
            }

            //ADD MISSING ARGUMENTS TO GAIN THE SAME HASH CODE - JORIS
            string tempHolder = "";
            int counter = 0;
            for (int i = 0; i < argumentUsed.Length; i++)
            {
                if (argumentUsed[i] == false)
                {
                    tempHolder += "&(" + arguments[i] + ",";
                    counter++;
                }
            }
            if (counter > 0)
            {
                tempHolder += "0";
                for (int i = 0; i < counter; i++)
                {
                    tempHolder += ')';
                }
                parseHolder = "|(" + tempHolder + "," + parseHolder + ")";
            }

            //RETURN RESULTING INFORMATION
            disjunctiveFormAndParse[0] = disHolder;
            disjunctiveFormAndParse[1] = parseHolder;
            return disjunctiveFormAndParse;
        }

    }
}
