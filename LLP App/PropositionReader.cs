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
        private static char[] ConnectiveTypes = new char[] { '~', '=', '|', '>', '&' };
        private static char[] Arguments = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private static Random rand = new Random();

        public static Connective ReadPropositionString(string proposition)
        {
            Console.WriteLine("PropositionReader: Reading string '" + proposition + "'.");
            PropositionList = proposition.ToList<char>();
            Connective result = null;
            result = readPropositionStringRec();
            Console.WriteLine("PropositionReader: Read string '" + proposition + "' succesfully.");
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
                        if (Head != null)
                        {
                            if(Head is ConnectiveNot || Head is ConnectiveArgument)
                            {
                                throw new Exception("'" + Head.GetLocalString() + "' does not need 2 inputs, please remove ','");
                            }
                            if (Head.Con1 == null)
                            {
                                throw new Exception("'" + Head.GetLocalString() + "' is missing a left connective");
                            }
                            if (index == 2)
                            {
                                throw new Exception("No connective has more than 2 inputs, problem found by '" + Head.GetLocalString() + "'");
                            }
                        }
                        else { throw new Exception("',' does not belong to any connective"); }
                        index = 2;
                        PropositionList.RemoveAt(0);
                        break;
                    case '(':
                        if (Head == null)
                        {
                            throw new Exception("'(' does not belong to any connective");
                        }
                        if (Head is ConnectiveArgument)
                        {
                            throw new Exception("'" + Head.GetLocalString() + "' has no inputs, please remove '('");
                        }
                        index = 1;
                        PropositionList.RemoveAt(0);
                        break;
                    case ')':
                        if(index == 1)
                        {
                            if (Head is ConnectiveArgument)
                            {
                                throw new Exception("'" + Head.GetLocalString() + "' has no inputs, please remove ')'");
                            }
                            if (Head.Con1 == null)
                            {
                                throw new Exception("'" + Head.GetLocalString() + "' is missing a left connective");
                            }
                        }
                        else if(index == 2)
                        {
                            if (Head is ConnectiveNot || Head is ConnectiveArgument)
                            {
                                throw new Exception("'" + Head.GetLocalString() + "' does not need 2 inputs, please remove ')'");
                            }
                            if (Head.Con2 == null)
                            {
                                throw new Exception("'" + Head.GetLocalString() + "' is missing a right connective");
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
                                    }
                                    else { throw new Exception("'" + PropositionList[0] + "' cannot be placed after another connective or argument"); }
                                    break;
                                case 1:
                                    if(Head != null)
                                    {
                                        con = readPropositionStringRec();
                                        Head.setLeftConnective(con);
                                    }
                                    else { throw new Exception("Internal index problem occured (index = 1, Head = null)"); }
                                    break;
                                case 2:
                                    if (Head != null)
                                    {
                                        con = readPropositionStringRec();
                                        Head.setRightConnective(con);
                                    }
                                    else { throw new Exception("Internal index problem occured (index = 2, Head = null)"); }
                                    break;
                                default:
                                    throw new Exception("Internal index problem occured (index > 2)");
                            }
                        }
                        else if (Arguments.Contains(PropositionList[0])) //argument found
                        {
                            Connective con = new ConnectiveArgument(PropositionList[0]);
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
                                        Head.setLeftConnective(con);
                                        PropositionList.RemoveAt(0);
                                    }
                                    else { throw new Exception("Internal index problem occured (index = 1, Head = null)"); }
                                    break;
                                case 2:
                                    if (Head != null)
                                    {
                                        Head.setRightConnective(con);
                                        PropositionList.RemoveAt(0);
                                    }
                                    else { throw new Exception("Internal index problem occured (index = 2, Head = null)"); }
                                    break;
                                default:
                                    throw new Exception("Internal index problem occured (index > 2)");
                            }
                        }
                        else if(PropositionList[0] != ' ') //UNKNOWN (no space)
                        {
                            throw new Exception("Unknown character found, char: '" + PropositionList[0] + "'. Please remove it!");
                        }
                        else
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
                        if (Head.Con1 == null) { throw new Exception("'" + Head.GetLocalString() + "' is missing a left connective"); }
                    }
                    else
                    {
                        if (Head.Con1 == null) { throw new Exception("'" + Head.GetLocalString() + "' is missing a left connective"); }
                        if (Head.Con2 == null) { throw new Exception("'" + Head.GetLocalString() + "' is missing a right connective"); }
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
                default:
                    throw new Exception("No type found for char: '" + Type + "'.");
            }

        }
        public static string CreateStructurePicture(Connective startCon)
        {
            if(startCon == null) { throw new NullReferenceException(); }
            Console.WriteLine("PropositionReader: creating structure picture");
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
                    if (allConnectives[i].Con1 != null)
                    {
                        sw.WriteLine("node" + (allConnectives[i].ID) + " -- node" + (allConnectives[i].Con1.ID));
                    }
                    if (allConnectives[i].Con2 != null)
                    {
                        sw.WriteLine("node" + (allConnectives[i].ID) + " -- node" + (allConnectives[i].Con2.ID));
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
            Console.WriteLine("PropositionReader: structure picture created succesfully");
            return "abc.png";
        }
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
    }
}
