using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        ConnectiveHolder conHolderNand;
        Truthtable table;
        Truthtable tableNand;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnParseProposition_Click(object sender, EventArgs e)
        {
            string proposition = tbProposition.Text;
            createConnectiveHolder(proposition);
            table = new Truthtable(conHolder);
            generateTruthtablesAndInformation(table);
            conHolderNand = conHolder.GetNandHolder();
            tbNand.Text = conHolderNand.GetParseString();
        }
        private void createConnectiveHolder(string proposition)
        {
            try
            {
                conHolder = new ConnectiveHolder(proposition);
                tbInfix.Text = conHolder.GetInfixString();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Parsing failed: please make sure that you wrote a proposition");
            }
            catch (Exception ex)
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
        
        private void generateTruthtablesAndInformation(Truthtable chosenTable)
        {
            //clear tables
            lbTruthTableInfo.Items.Clear();
            dgvTruthTable.Columns.Clear();
            dgvTruthTable.Rows.Clear();
            dgvSimpleTable.Columns.Clear();
            dgvSimpleTable.Rows.Clear();

            if (conHolder == null) { MessageBox.Show("No proposition has been parsed yet"); return; }

            //table = new Truthtable(conHolder);
            List<char> arguments = conHolder.GetListOfAllArguments();
            List<TruthtableRow> rows = chosenTable.Rows; //normal truthtable rows
            List<TruthtableRow> simpleRows = chosenTable.GetSimpleTable(); //simple truthtable rows

            //create form table's columns
            int counter = 0;
            foreach (char c in arguments)
            {
                dgvTruthTable.Columns.Add(c.ToString(), c.ToString());
                dgvTruthTable.Columns[counter].Width = 20;
                dgvSimpleTable.Columns.Add(c.ToString(), c.ToString());
                dgvSimpleTable.Columns[counter].Width = 20;
                counter++;
            }
            dgvTruthTable.Columns.Add("Result", "Result");
            dgvTruthTable.Columns[counter].Width = 50;
            dgvSimpleTable.Columns.Add("Result", "Result");
            dgvSimpleTable.Columns[counter].Width = 50;

            //provide form table's with rows
            DataGridViewRow row;
            foreach (TruthtableRow r in rows) //print normal table
            {
                row = (DataGridViewRow)dgvTruthTable.Rows[0].Clone();
                counter = 0;
                foreach (char c in arguments)
                {
                    row.Cells[counter].Value = r.GetValueForArgument(c);
                    counter++;
                }
                row.Cells[counter].Value = r.RowValue;
                dgvTruthTable.Rows.Add(row);
            }
            foreach (TruthtableRow r in simpleRows) //print simple table
            {
                row = (DataGridViewRow)dgvSimpleTable.Rows[0].Clone();
                counter = 0;
                foreach (char c in arguments)
                {
                    row.Cells[counter].Value = r.GetValueForArgument(c);
                    counter++;
                }
                row.Cells[counter].Value = r.RowValue;
                dgvSimpleTable.Rows.Add(row);
            }

            //GETTING HASH CODE
            lbTruthTableInfo.Items.Add("Arguments: " + chosenTable.ConHolder.getAllArgumentsString());
            lbTruthTableInfo.Items.Add("Hash (binary): " + chosenTable.GetHashCodeBinary());
            lbTruthTableInfo.Items.Add("Hash (hexadecimal): " + chosenTable.GetHashCodeHexadecimal());

            //GETTING DISJUNCTIVE AND IT'S PARSE
            string[] disjunctAndParse = PropositionReader.readDisjunctiveForm(rows);
            tbDisjunctive.Text = disjunctAndParse[0];
            tbDisjunctiveParse.Text = disjunctAndParse[1];
            disjunctAndParse = PropositionReader.readDisjunctiveForm(simpleRows);
            tbDisjunctiveSimple.Text = disjunctAndParse[0];
            tbDisjunctiveSimpleParse.Text = disjunctAndParse[1];
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

        private void btnParseDisjunctive_Click(object sender, EventArgs e)
        {
            string proposition = tbDisjunctiveParse.Text;
            createConnectiveHolder(proposition);
            table = new Truthtable(conHolder);
            generateTruthtablesAndInformation(table);
            conHolderNand = conHolder.GetNandHolder();
            tbNand.Text = conHolderNand.GetParseString();
        }

        private void btnDisjunctiveSimpleParse_Click(object sender, EventArgs e)
        {
            string proposition = tbDisjunctiveSimpleParse.Text;
            createConnectiveHolder(proposition);
            table = new Truthtable(conHolder);
            generateTruthtablesAndInformation(table);
            conHolderNand = conHolder.GetNandHolder();
            tbNand.Text = conHolderNand.GetParseString();
        }

        private void btnStartTimer_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                btnStartTimer.Text = "Start";
            }
            else
            {
                timer1.Enabled = true;
                btnStartTimer.Text = "Stop";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string hashCode;

            tbProposition.Text = PropositionReader.CreateRandomPropositionString();
            createConnectiveHolder(tbProposition.Text);
            table = new Truthtable(conHolder);
            generateTruthtablesAndInformation(table);
            hashCode = table.GetHashCodeHexadecimal();

            conHolderNand = conHolder.GetNandHolder();
            tbNand.Text = conHolderNand.GetParseString();
            tableNand = new Truthtable(conHolderNand);
            if (hashCode != tableNand.GetHashCodeHexadecimal()) { timer1.Enabled = false; MessageBox.Show("hashCode test failed, please have a look, timer disabled"); return; }

            createConnectiveHolder(tbDisjunctiveParse.Text);
            table = new Truthtable(conHolder);
            generateTruthtablesAndInformation(table);
            if (hashCode != table.GetHashCodeHexadecimal()) { timer1.Enabled = false; MessageBox.Show("hashCode test failed, please have a look, timer disabled"); return; }

            createConnectiveHolder(tbDisjunctiveSimpleParse.Text);
            table = new Truthtable(conHolder);
            generateTruthtablesAndInformation(table);
            if (hashCode != table.GetHashCodeHexadecimal()) { timer1.Enabled = false; MessageBox.Show("hashCode test failed, please have a look, timer disabled"); return; }
        }

        private void btnNandParse_Click(object sender, EventArgs e)
        {
            string proposition = tbNand.Text;
            createConnectiveHolder(proposition);
            table = new Truthtable(conHolder);
            generateTruthtablesAndInformation(table);
            conHolderNand = conHolder.GetNandHolder();
            tbNand.Text = conHolderNand.GetParseString();
        }
    }
}
