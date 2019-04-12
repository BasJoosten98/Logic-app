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
                        if(Head.Con1 == null)
                        {
                            throw new Exception("First input is missing");
                        }
                        if(index == 2)
                        {
                            throw new Exception("Index larger than 2 is not possible");
                        }
                        index = 2;
                        PropositionList.RemoveAt(0);
                        break;
                    case '(':
                        if (Head == null)
                        {
                            throw new Exception("Head is missing");
                        }
                        index = 1;
                        PropositionList.RemoveAt(0);
                        break;
                    case ')':
                        if(index == 1)
                        {
                            if (Head.Con1 == null)
                            {
                                throw new Exception("First input is missing");
                            }
                        }
                        else if(index == 2)
                        {
                            if (Head.Con2 == null)
                            {
                                throw new Exception("Second input is missing");
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
                                    else { throw new Exception("Head was already specified"); }
                                    break;
                                case 1:
                                    if(Head != null)
                                    {
                                        con = readPropositionStringRec();
                                        Head.setLeftConnective(con);
                                    }
                                    else { throw new Exception("Head was not specified and index is 1"); }
                                    break;
                                case 2:
                                    if (Head != null)
                                    {
                                        con = readPropositionStringRec();
                                        Head.setRightConnective(con);
                                    }
                                    else { throw new Exception("Head was not specified and index is 2"); }
                                    break;
                                default:
                                    throw new Exception("Index can not be greater than 2");
                            }
                        }
                        else if (Arguments.Contains(PropositionList[0])) //argument found
                        {
                            Connective con = new ConnectiveArgument(PropositionList[0]);
                            switch (index)
                            {
                                case 0:
                                    PropositionList.RemoveAt(0);
                                    return con;
                                case 1:
                                    if (Head != null)
                                    {
                                        Head.setLeftConnective(con);
                                        PropositionList.RemoveAt(0);
                                    }
                                    else { throw new Exception("Head was not specified and index is 1"); }
                                    break;
                                case 2:
                                    if (Head != null)
                                    {
                                        Head.setRightConnective(con);
                                        PropositionList.RemoveAt(0);
                                    }
                                    else { throw new Exception("Head was not specified and index is 2"); }
                                    break;
                                default:
                                    throw new Exception("Index can not be greater than 2");
                            }
                        }
                        else if(PropositionList[0] != ' ') //UNKNOWN (no space)
                        {
                            throw new Exception("Unknown character found, char: '" + PropositionList[0] + "'.");
                        }
                        else
                        {
                            PropositionList.RemoveAt(0);
                        }
                        break;
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

        public static string CreateRandomPropositionString(int maxDifferentArguments)
        {
            if(maxDifferentArguments > Arguments.Length) { maxDifferentArguments = Arguments.Length; }
            int totalDives = rand.Next(0, 11);
            return createRandomPorpositionStringRec(totalDives, maxDifferentArguments);
        }
        private static string createRandomPorpositionStringRec(int totalDives, int maxDifferentArguments) 
        {
            //get random connective
            if (totalDives == 0)
            {
                char randomArgument = Arguments[rand.Next(0, maxDifferentArguments)];
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
