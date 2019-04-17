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
                tbInfix.Text = conHolder.GetInfixString();

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
        
        private void btnGenerateTruthtable_Click(object sender, EventArgs e)
        {
            lbTruthTableInfo.Items.Clear();
            dgvTruthTable.Columns.Clear();
            dgvTruthTable.Rows.Clear();

            if (conHolder == null) { MessageBox.Show("No proposition has been parsed yet"); return; }

            table = new Truthtable(conHolder);
            List<char> arguments = conHolder.GetListOfAllArguments();
            List<TruthtableRow> rows = table.Rows;

            //create columns
            int counter = 0;
            foreach(char c in arguments)
            {
                dgvTruthTable.Columns.Add(c.ToString(), c.ToString());
                dgvTruthTable.Columns[counter].Width = 20;
                counter++;
            }
            dgvTruthTable.Columns.Add("Result", "Result");
            dgvTruthTable.Columns[counter].Width = 50;

            //create rows
            DataGridViewRow row;
            foreach (TruthtableRow r in rows)
            {
                row = (DataGridViewRow)dgvTruthTable.Rows[0].Clone();
                counter = 0;
                foreach(char c in arguments)
                {
                    bool result = r.GetValueForArgument(c);
                    if (result) { row.Cells[counter].Value = 1; }
                    else { row.Cells[counter].Value = 0; }
                    counter++;
                }
                bool endResult = r.RowValue;
                if (endResult) { row.Cells[counter].Value = 1; }
                else { row.Cells[counter].Value = 0; }
                dgvTruthTable.Rows.Add(row);
            }

            //Read hash code
            string hashCodeBinary = "";
            for(int r = 0; r < dgvTruthTable.Rows.Count; r++)
            {
                hashCodeBinary += dgvTruthTable.Rows[r].Cells[dgvTruthTable.Rows[r].Cells.Count - 1].Value;
            }
            string hashCodeHexa = BinaryReader.BinaryToHexadecimal(hashCodeBinary);
            lbTruthTableInfo.Items.Add("Arguments: " + conHolder.getAllArgumentsString());
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
