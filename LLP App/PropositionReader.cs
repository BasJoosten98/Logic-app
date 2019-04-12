using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLP_App
{
    static class PropositionReader
    {
        private static List<char> PropositionList;
        private static char[] ConnectiveTypes = new char[] { '>', '=', '|', '~', '&' };
        private static char[] Arguments = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
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
        
    }
}
