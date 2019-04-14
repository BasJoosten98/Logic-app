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
            catch(NullReferenceException ex)
            {
                MessageBox.Show("Parsing failed: please make sure that you wrote a proposition");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Parsing failed: " + ex.Message);
            }
        }

        private void btnViewTree_Click(object sender, EventArgs e)
        {
            if(conHolder != null)
            {
                conHolder.ShowTreeStructure();
            }
            else { MessageBox.Show("No proposition has been parsed yet"); }
        }

        private void btnCreateRandomProposition_Click(object sender, EventArgs e)
        {
            string proposition = PropositionReader.CreateRandomPropositionString();
            tbProposition.Text = proposition;
        }

        private void btnShowArguments_Click(object sender, EventArgs e)
        {
            MessageBox.Show(getShowArgumentString());
        }
        private string getShowArgumentString()
        {
            if (conHolder != null)
            {
                List<char> fullList = conHolder.GetListOfAllArguments();
                string holder = "";
                foreach (char c in fullList)
                {
                    holder += c + ", ";
                }
                return holder;
            }
            else { MessageBox.Show("No proposition has been parsed yet"); return ""; }
        }

        private void btnGenerateTruthtable_Click(object sender, EventArgs e)
        {
            lbTruthTable.Items.Clear();
            lbTruthTableInfo.Items.Clear();

            if(conHolder == null) { MessageBox.Show("No proposition has been parsed yet"); return; }

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
            string hashCodeBinary = "";
            foreach(TruthtableRow r in rows)
            {
                foreach(char c in arguments)
                {
                    bool result = r.GetValueForArgument(c);
                    if (result) { tile += "1  "; }
                    else { tile += "0  "; }
                }
                bool endResult = r.RowValue;
                if (endResult) { tile += "1"; hashCodeBinary = "1" + hashCodeBinary; }
                else { tile += "0"; hashCodeBinary = "0" + hashCodeBinary; }

                lbTruthTable.Items.Add(tile);
                tile = "";
            }
            string hashCodeHexa = BinaryReader.BinaryToHexadecimal(hashCodeBinary);
            lbTruthTableInfo.Items.Add("Arguments: " + getShowArgumentString());
            lbTruthTableInfo.Items.Add("Hash (binary): " + hashCodeBinary);
            lbTruthTableInfo.Items.Add("Hash (hexadecimal): " + hashCodeHexa);
        }

        private void btnBinary_Click(object sender, EventArgs e)
        {
            string binary = tbBinary.Text;
            int number = BinaryReader.BinaryToNumber(binary);
            string hexadecimal = BinaryReader.BinaryToHexadecimal(binary);
            tbNumber.Text = number.ToString();
            tbHexadecimal.Text = hexadecimal;
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            int number = int.Parse(tbNumber.Text);
            string binary = BinaryReader.NumberToBinary(number, true);
            string hexadecimal = BinaryReader.BinaryToHexadecimal(binary);
            tbBinary.Text = binary;
            tbHexadecimal.Text = hexadecimal;
        }

        private void btnHexadecimal_Click(object sender, EventArgs e)
        {
            string hexadecimal = tbHexadecimal.Text;
            string binary = BinaryReader.HexadecimalToBinary(hexadecimal);
            int number = BinaryReader.BinaryToNumber(binary);
            tbNumber.Text = number.ToString();
            tbBinary.Text = binary;
        }
    }
}
