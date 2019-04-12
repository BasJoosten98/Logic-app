﻿using System;
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
            return fullList;
        }
    }
}
