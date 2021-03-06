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
        ConnectiveHolder conHolderNandSimple;
        ConnectiveHolder conHolderDisjunctive;
        ConnectiveHolder conHolderDisjunctiveSimple;
        Truthtable table;
        Truthtable tableNand;
        Truthtable tableNandSimple;
        Truthtable tableDisjunctive;
        Truthtable tableDisjunctiveSimple;
        TableauxHolder tabHolder;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnParseProposition_Click(object sender, EventArgs e)
        {
            string proposition = tbProposition.Text;
            createAllConHoldersAndTruthtables(proposition);
        }
        private bool createAllConHoldersAndTruthtables(string proposition)
        {
            try
            {
                //CREATING PROPOSITION
                Connective con = PropositionReader.ReadPropositionString(proposition);

                if (con.IsNormalProposition())
                {
                    conHolder = new ConnectiveHolder(con);
                    table = new Truthtable(conHolder);
                    printDisjunctive();

                    conHolderDisjunctive = new ConnectiveHolder(tbDisjunctiveParse.Text);
                    tableDisjunctive = new Truthtable(conHolderDisjunctive);

                    conHolderDisjunctiveSimple = new ConnectiveHolder(tbDisjunctiveSimpleParse.Text);
                    tableDisjunctiveSimple = new Truthtable(conHolderDisjunctiveSimple);

                    conHolderNand = conHolder.GetNandHolder();
                    tableNand = new Truthtable(conHolderNand);

                    conHolderNandSimple = conHolderDisjunctiveSimple.GetNandHolder();
                    tableNandSimple = new Truthtable(conHolderNandSimple);

                    tbInfix.Text = conHolder.GetInfixString();
                    tbNand.Text = conHolderNand.GetParseString();
                    tbNandSimple.Text = conHolderNandSimple.GetParseString();
                    showTableauxTree = false;

                    //DRAWING AND PRINTING TABLE INFORMATION
                    printVisualTruthtables(table);
                    printTablesInformation();
                }
                else
                {
                    if (!con.AreLocalArgumentsMatching(new List<char>(), new List<char>())) { throw new Exception("Local Arguments are mismatching or there are quantifiers with the same Local Argument"); }
                    conHolder = new ConnectiveHolder(con);
                    tbInfix.Text = conHolder.GetInfixString();
                    showTableauxTree = false;
                }
                Console.WriteLine("Succesfully parsed proposition: " + proposition);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Parsing failed: please make sure that you wrote a proposition");
                Console.WriteLine("Failed to parse proposition: " + proposition);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Parsing failed: " + ex.Message);
                Console.WriteLine("Failed to parse proposition: " + proposition);
                return false;
            }
            return true;
        }

        private void btnViewTree_Click(object sender, EventArgs e)
        {
            if (showTableauxTree)
            {
                if (tabHolder != null)
                {
                    tabHolder.ShowTreeStructure();
                }
                else { MessageBox.Show("No proposition has been parsed yet"); }
            }
            else
            {
                if (conHolder != null)
                {
                    conHolder.ShowTreeStructure();
                }
                else { MessageBox.Show("No proposition has been parsed yet"); }
            }
        }

        private void btnCreateRandomProposition_Click(object sender, EventArgs e)
        {
            string proposition = PropositionReader.CreateRandomPropositionString();
            tbProposition.Text = proposition;
            createAllConHoldersAndTruthtables(proposition);
        }
        
        private void printVisualTruthtables(Truthtable chosenTable)
        {
            //clear tables
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
        }

        private void printTablesInformation()
        {
            lbTruthTableInfo.Items.Clear();

            //GETTING HASH CODE
            lbTruthTableInfo.Items.Add("Arguments: " + table.ConHolder.getAllArgumentsString());
            lbTruthTableInfo.Items.Add(" ");
            lbTruthTableInfo.Items.Add("---HASH---");
            lbTruthTableInfo.Items.Add("Binary: " + table.GetHashCodeBinary());
            lbTruthTableInfo.Items.Add("Hexadecimal: " + table.GetHashCodeHexadecimal());
            lbTruthTableInfo.Items.Add("Disjunctive: " + tableDisjunctive.GetHashCodeHexadecimal());
            lbTruthTableInfo.Items.Add("DisjunctiveSimple: " + tableDisjunctiveSimple.GetHashCodeHexadecimal());
            lbTruthTableInfo.Items.Add("Nand: " + tableNand.GetHashCodeHexadecimal());
            lbTruthTableInfo.Items.Add("NandSimple: " + tableNandSimple.GetHashCodeHexadecimal());
        }
        private void printDisjunctive()
        {
            //GETTING DISJUNCTIVE AND IT'S PARSE
            string[] disjunctAndParse = PropositionReader.readDisjunctiveForm(table.Rows);
            tbDisjunctive.Text = disjunctAndParse[0];
            tbDisjunctiveParse.Text = disjunctAndParse[1];
            disjunctAndParse = PropositionReader.readDisjunctiveForm(table.GetSimpleTable());
            tbDisjunctiveSimple.Text = disjunctAndParse[0];
            tbDisjunctiveSimpleParse.Text = disjunctAndParse[1];
        }

        private void btnBinary_Click(object sender, EventArgs e)
        {
            try
            {
                string binary = tbBinary.Text;
                if (binary != "")
                {
                    int number = BinaryReader.BinaryToNumber(binary);
                    string hexadecimal = BinaryReader.BinaryToHexadecimal(binary);
                    tbNumber.Text = number.ToString();
                    tbHexadecimal.Text = hexadecimal;
                }
            }
            catch (Exception ex) { MessageBox.Show("Binary Reader failed: " + ex.Message); }
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            int number;
            if (tbNumber.Text != "")
            {
                if (int.TryParse(tbNumber.Text, out number))
                {
                    string binary = BinaryReader.NumberToBinary(number, true);
                    string hexadecimal = BinaryReader.BinaryToHexadecimal(binary);
                    tbBinary.Text = binary;
                    tbHexadecimal.Text = hexadecimal;
                }
                else { MessageBox.Show("Binary Reader failed: Number in wrong format"); }
            }
        }

        private void btnHexadecimal_Click(object sender, EventArgs e)
        {
            try
            {
                string hexadecimal = tbHexadecimal.Text;
                if (hexadecimal != "")
                {
                    string binary = BinaryReader.HexadecimalToBinary(hexadecimal);
                    int number = BinaryReader.BinaryToNumber(binary);
                    tbNumber.Text = number.ToString();
                    tbBinary.Text = binary;
                }
            }
            catch(Exception ex) { MessageBox.Show("Binary Reader failed: " + ex.Message); }
        }

        private void btnParseDisjunctive_Click(object sender, EventArgs e)
        {
            string proposition = tbDisjunctiveParse.Text;
            createAllConHoldersAndTruthtables(proposition);
        }

        private void btnDisjunctiveSimpleParse_Click(object sender, EventArgs e)
        {
            string proposition = tbDisjunctiveSimpleParse.Text;
            createAllConHoldersAndTruthtables(proposition);
        }

        private void btnStartTimer_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                btnStartTimer.Text = "Start";
                Console.WriteLine("Stopping testing...");
                Console.WriteLine("Test results: " + testCounter + " proposition(s) came succesfully through the test");
                MessageBox.Show("Test results: " + testCounter + " proposition(s) came succesfully through the test");
                panelInput.Enabled = true;
            }
            else
            {
                Console.WriteLine("Starting testing...");
                testCounter = 0;
                timer1.Enabled = true;
                btnStartTimer.Text = "Stop";
                panelInput.Enabled = false;
            }
        }

        private int testCounter = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            string hashCode;
            string proposition;

            //GENEARTE ALL TABLES AND CONHOLDERS AND TABLEAUX
            proposition = PropositionReader.CreateRandomPropositionString();
            tbProposition.Text = proposition;
            if (createAllConHoldersAndTruthtables(proposition))
            {
                printVisualTruthtables(table);
                printTablesInformation();
            }
            CreateTableaux();

            //GET HASH CODE OF MAIN TABLE
            hashCode = table.GetHashCodeHexadecimal();

            //COMPARE HASH CODE WITH OTHER TABLES
            if (hashCode != tableDisjunctive.GetHashCodeHexadecimal()) { testFailed("Disjunctive hashcode does not match", proposition); return; }
            if (hashCode != tableDisjunctiveSimple.GetHashCodeHexadecimal()) { testFailed("DisjunctiveSimple hashcode does not match", proposition); return; }
            if (hashCode != tableNand.GetHashCodeHexadecimal()) { testFailed("Nand hashcode does not match", proposition); return; }
            if (hashCode != tableNandSimple.GetHashCodeHexadecimal()) { testFailed("NandSimple hashcode does not match", proposition); return; }

            //COMPARE TABLEAUX WITH SIMPLE TRUTHTABLE
            bool simpleTableResult;
            if(dgvSimpleTable.Rows.Count > 2) { simpleTableResult = false; } //contains 1 empty row at the bottom, should ignore!
            else
            {
                if (dgvSimpleTable.Rows.Count == 2)
                {
                    string rowResult = dgvSimpleTable.Rows[0].Cells[dgvSimpleTable.Columns.Count - 1].Value.ToString();
                    if(rowResult == "1") { simpleTableResult = true; }
                    else if(rowResult == "0") { simpleTableResult = false; }
                    else { throw new Exception("Unknown character"); }
                }
                else
                {
                     throw new Exception("Not possible scenario"); 
                }
            }
            if(simpleTableResult != tabHolder.IsTautology) { testFailed("Tautology result from SimpleTable does not match tautology result from Tableaux", proposition); return; }
            testCounter++;
        }
        private void testFailed(string message, string proposition)
        {
            timer1.Enabled = false;
            Console.WriteLine("Stopping testing...");
            Console.WriteLine("Test results: " + testCounter + " proposition(s) came succesfully through the test");
            Console.WriteLine("Test results: " + proposition + " failed the test, reason: " + message);
            MessageBox.Show("Test results: " + testCounter + " proposition(s) came succesfully through the test. \n\n" + "Testing failed for " + proposition + "\nReason: " + message);
            btnStartTimer.Text = "Start";
            panelInput.Enabled = true;
            return;
        }
        private void btnNandParse_Click(object sender, EventArgs e)
        {
            string proposition = tbNand.Text;
            createAllConHoldersAndTruthtables(proposition);
        }

        private bool showTableauxTree = false;
        private void btnTableaux_Click(object sender, EventArgs e)
        {
            CreateTableaux();
        }
        private void CreateTableaux()
        {
            try
            {
                string proposition = tbProposition.Text;
                tabHolder = new TableauxHolder(proposition);
                tbInfix.Text = tabHolder.GetInfixString();
                if (tabHolder.IsTautology) { btnTableaux.BackColor = Color.Green; }
                else { btnTableaux.BackColor = Color.Red; }

                showTableauxTree = true;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Parsing failed: please make sure that you wrote a proposition");
            }
            catch (Exception ex) { MessageBox.Show("Parsing failed: " + ex.Message); }
        }

        private void btnNandSimpleParse_Click(object sender, EventArgs e)
        {
            string proposition = tbNandSimple.Text;
            createAllConHoldersAndTruthtables(proposition);
        }
    }
}
