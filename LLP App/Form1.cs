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
        Truthtable table;
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

        private void btnGenerateTruthtable_Click(object sender, EventArgs e)
        {
            lbTruthTable.Items.Clear();

            table = new Truthtable(conHolder);
            List<char> arguments = conHolder.GetListOfAllArguments();
            List<TruthtableRow> rows = table.Rows;          

            string head = "";
            foreach(char c in arguments)
            {
                head += c + "  ";
            }
            head += "V";
            lbTruthTable.Items.Add(head);

            string tile = "";
            foreach(TruthtableRow r in rows)
            {
                foreach(char c in arguments)
                {
                    bool result = r.GetValueForArgument(c);
                    if (result) { tile += "1  "; }
                    else { tile += "0  "; }
                }
                bool endResult = r.RowValue;
                if (endResult) { tile += "1"; }
                else { tile += "0"; }

                lbTruthTable.Items.Add(tile);
                tile = "";
            }
        }
    }
}
