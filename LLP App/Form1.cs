using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LLP_App
{
    public partial class Form1 : Form
    {
        ConnectiveHolder conHolder;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnParseProposition_Click(object sender, EventArgs e)
        {
            string proposition = tbProposition.Text;
            try
            {
                conHolder = new ConnectiveHolder(proposition);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnViewTree_Click(object sender, EventArgs e)
        {
            if(conHolder != null)
            {
                conHolder.ShowTreeStructure();
            }
            else { throw new NullReferenceException(); }
        }

        private void btnCreateRandomProposition_Click(object sender, EventArgs e)
        {
            string proposition = PropositionReader.CreateRandomPropositionString();
            tbProposition.Text = proposition;
        }

        private void btnShowArguments_Click(object sender, EventArgs e)
        {
            if (conHolder != null)
            {
                List<char> fullList = conHolder.GetListOfAllArguments();
                string holder = "";
                foreach(char c in fullList)
                {
                    holder += c + ", ";
                }
                MessageBox.Show(holder);
            }
            else { throw new NullReferenceException(); }
        }
    }
}
