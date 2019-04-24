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
            createConnectiveHolder(proposition);
        }
        private void createConnectiveHolder(string proposition)
        {
            try
            {
                conHolder = new ConnectiveHolder(proposition);
                tbInfix.Text = conHolder.GetInfixString();

            }
            catch (NullReferenceException ex)
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
        
        private void btnGenerateTruthtable_Click(object sender, EventArgs e)
        {
            lbTruthTableInfo.Items.Clear();
            dgvTruthTable.Columns.Clear();
            dgvTruthTable.Rows.Clear();
            dgvSimpleTable.Columns.Clear();
            dgvSimpleTable.Rows.Clear();

            if (conHolder == null) { MessageBox.Show("No proposition has been parsed yet"); return; }

            table = new Truthtable(conHolder);
            List<char> arguments = conHolder.GetListOfAllArguments();
            List<TruthtableRow> rows = table.Rows;
            List<TruthtableRow> simpleRows = table.GetSimpleTable();

            //create columns
            int counter = 0;
            foreach(char c in arguments)
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

            //create rows
            DataGridViewRow row;
            foreach (TruthtableRow r in rows) //print table
            {
                row = (DataGridViewRow)dgvTruthTable.Rows[0].Clone();
                counter = 0;
                foreach(char c in arguments)
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
            readHashCodeFromTable();
            readDisjunctiveForm(rows, false);
            readDisjunctiveForm(simpleRows, true);
            
        }
        private void readHashCodeFromTable()
        {
            //Read hash code
            string hashCodeBinary = "";
            for (int r = 0; r < dgvTruthTable.Rows.Count; r++)
            {
                hashCodeBinary += dgvTruthTable.Rows[r].Cells[dgvTruthTable.Rows[r].Cells.Count - 1].Value;
            }
            string hashCodeHexa = BinaryReader.BinaryToHexadecimal(hashCodeBinary);
            lbTruthTableInfo.Items.Add("Arguments: " + conHolder.getAllArgumentsString());
            lbTruthTableInfo.Items.Add("Hash (binary): " + hashCodeBinary);
            lbTruthTableInfo.Items.Add("Hash (hexadecimal): " + hashCodeHexa);
        }
        private void readDisjunctiveForm(List<TruthtableRow> rows, bool fromSimple)
        {
            List<TruthtableRow> filteredRows = new List<TruthtableRow>(); //filter out rows that containt arguments == '*'
            //if (fromSimple)
            //{
            //    foreach (TruthtableRow r in rows)
            //    {
            //        foreach (TruthtableRow fr in PropositionReader.GetSubrowsOfMainRow(r))
            //        {
            //            filteredRows.Add(fr);
            //        }
            //    }
            //}
            //else
            //{
            //    filteredRows = rows;
            //}
            filteredRows = rows;
            string disHolder = ""; 
            List<string> parseRowsOr = new List<string>();
            List<string> parseRowsAnd = new List<string>();

            foreach (TruthtableRow r in filteredRows)
            {
                if(r.RowValue == '1')
                {
                    string disHolder2 = "";

                    //OBTAIN INFORMATION
                    foreach(TruthtableRowArgument arg in r.Arguments)
                    {
                        if (arg.Value != '*')
                        {
                            if (disHolder2 != "")
                            {
                                disHolder2 += " & ";
                            }
                            if (arg.Value == '1') { disHolder2 += arg.Argument; parseRowsAnd.Add(arg.Argument.ToString()); }
                            else { disHolder2 += "~" + arg.Argument; parseRowsAnd.Add("~(" + arg.Argument.ToString() + ")" ); }
                        }
                    }
                    //READER PART
                    if (disHolder2 != "")
                    {
                        if (disHolder != "")
                        {
                            disHolder += " | ";
                        }
                        disHolder += "(";
                        disHolder += disHolder2 + ")";
                    }
                    //PARSE PART (Count == 0 will be skipped)
                    if (parseRowsAnd.Count != 0)
                    {
                        if (parseRowsAnd.Count == 1) { parseRowsOr.Add(parseRowsAnd[0]); }
                        else
                        {
                            string parseHolder2 = "";
                            for (int i = parseRowsAnd.Count - 1; i >= 0; i--)
                            {
                                if (i >= 2)
                                {
                                    parseHolder2 += "&(" + parseRowsAnd[parseRowsAnd.Count - i - 1] + ",";
                                }
                                else
                                {
                                    parseHolder2 += "&(" + parseRowsAnd[parseRowsAnd.Count - i - 1] + "," + parseRowsAnd[parseRowsAnd.Count - i];
                                    break;
                                }
                            }
                            for (int i = 1; i <= parseRowsAnd.Count - 1; i++)
                            {
                                parseHolder2 += ")";
                            }
                            parseRowsOr.Add(parseHolder2);
                        }
                    }
                    parseRowsAnd = new List<string>();
                }
            }
            string parseHolder = "";
            if(parseRowsOr.Count == 0) { parseHolder = filteredRows[0].RowValue.ToString(); }
            else if (parseRowsOr.Count == 1) { parseHolder = parseRowsOr[0]; }
            else
            {               
                for (int i = parseRowsOr.Count - 1; i >= 0; i--)
                {
                    if (i >= 2)
                    {
                        parseHolder += "|(" + parseRowsOr[parseRowsOr.Count - i - 1] + ",";
                    }
                    else
                    {
                        parseHolder += "|(" + parseRowsOr[parseRowsOr.Count - i - 1] + "," + parseRowsOr[parseRowsOr.Count - i];
                        break;
                    }
                }
                for (int i = 1; i <= parseRowsOr.Count - 1; i++)
                {
                    parseHolder += ")";
                }
            }
            if (fromSimple) { tbDisjunctiveSimple.Text = disHolder; tbDisjunctiveSimpleParse.Text = parseHolder; }
            else { tbDisjunctive.Text = disHolder; tbDisjunctiveParse.Text = parseHolder; }
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
        }

        private void btnDisjunctiveSimpleParse_Click(object sender, EventArgs e)
        {
            string proposition = tbDisjunctiveSimpleParse.Text;
            createConnectiveHolder(proposition);
        }
    }
}
