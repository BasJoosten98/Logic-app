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
            conHolder = new ConnectiveHolder(proposition);
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
            int differentArguments = (int)nbDifferentArguments.Value;
            string proposition = PropositionReader.CreateRandomPropositionString(differentArguments);
            tbProposition.Text = proposition;
        }
    }
}
