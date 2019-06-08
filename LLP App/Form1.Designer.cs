namespace LLP_App
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbProposition = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnParseProposition = new System.Windows.Forms.Button();
            this.btnViewTree = new System.Windows.Forms.Button();
            this.btnCreateRandomProposition = new System.Windows.Forms.Button();
            this.gbBinaryReaderTester = new System.Windows.Forms.GroupBox();
            this.btnHexadecimal = new System.Windows.Forms.Button();
            this.btnNumber = new System.Windows.Forms.Button();
            this.btnBinary = new System.Windows.Forms.Button();
            this.tbHexadecimal = new System.Windows.Forms.TextBox();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.tbBinary = new System.Windows.Forms.TextBox();
            this.lbTruthTableInfo = new System.Windows.Forms.ListBox();
            this.dgvTruthTable = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.tbInfix = new System.Windows.Forms.TextBox();
            this.dgvSimpleTable = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDisjunctive = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDisjunctiveSimple = new System.Windows.Forms.TextBox();
            this.tbDisjunctiveParse = new System.Windows.Forms.TextBox();
            this.tbDisjunctiveSimpleParse = new System.Windows.Forms.TextBox();
            this.btnParseDisjunctive = new System.Windows.Forms.Button();
            this.btnDisjunctiveSimpleParse = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnStartTimer = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbNand = new System.Windows.Forms.TextBox();
            this.btnNandParse = new System.Windows.Forms.Button();
            this.btnTableaux = new System.Windows.Forms.Button();
            this.panelInput = new System.Windows.Forms.GroupBox();
            this.btnNandSimpleParse = new System.Windows.Forms.Button();
            this.tbNandSimple = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gbBinaryReaderTester.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTruthTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimpleTable)).BeginInit();
            this.panelInput.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbProposition
            // 
            this.tbProposition.Location = new System.Drawing.Point(71, 13);
            this.tbProposition.Name = "tbProposition";
            this.tbProposition.Size = new System.Drawing.Size(315, 20);
            this.tbProposition.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Proposition";
            // 
            // btnParseProposition
            // 
            this.btnParseProposition.Location = new System.Drawing.Point(392, 10);
            this.btnParseProposition.Name = "btnParseProposition";
            this.btnParseProposition.Size = new System.Drawing.Size(44, 23);
            this.btnParseProposition.TabIndex = 2;
            this.btnParseProposition.Text = "Parse";
            this.btnParseProposition.UseVisualStyleBackColor = true;
            this.btnParseProposition.Click += new System.EventHandler(this.btnParseProposition_Click);
            // 
            // btnViewTree
            // 
            this.btnViewTree.Location = new System.Drawing.Point(509, 10);
            this.btnViewTree.Name = "btnViewTree";
            this.btnViewTree.Size = new System.Drawing.Size(65, 23);
            this.btnViewTree.TabIndex = 3;
            this.btnViewTree.Text = "View Tree";
            this.btnViewTree.UseVisualStyleBackColor = true;
            this.btnViewTree.Click += new System.EventHandler(this.btnViewTree_Click);
            // 
            // btnCreateRandomProposition
            // 
            this.btnCreateRandomProposition.Location = new System.Drawing.Point(580, 10);
            this.btnCreateRandomProposition.Name = "btnCreateRandomProposition";
            this.btnCreateRandomProposition.Size = new System.Drawing.Size(151, 23);
            this.btnCreateRandomProposition.TabIndex = 4;
            this.btnCreateRandomProposition.Text = "Create Random Proposition";
            this.btnCreateRandomProposition.UseVisualStyleBackColor = true;
            this.btnCreateRandomProposition.Click += new System.EventHandler(this.btnCreateRandomProposition_Click);
            // 
            // gbBinaryReaderTester
            // 
            this.gbBinaryReaderTester.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gbBinaryReaderTester.Controls.Add(this.btnHexadecimal);
            this.gbBinaryReaderTester.Controls.Add(this.btnNumber);
            this.gbBinaryReaderTester.Controls.Add(this.btnBinary);
            this.gbBinaryReaderTester.Controls.Add(this.tbHexadecimal);
            this.gbBinaryReaderTester.Controls.Add(this.tbNumber);
            this.gbBinaryReaderTester.Controls.Add(this.tbBinary);
            this.gbBinaryReaderTester.Location = new System.Drawing.Point(434, 366);
            this.gbBinaryReaderTester.Name = "gbBinaryReaderTester";
            this.gbBinaryReaderTester.Size = new System.Drawing.Size(300, 80);
            this.gbBinaryReaderTester.TabIndex = 8;
            this.gbBinaryReaderTester.TabStop = false;
            this.gbBinaryReaderTester.Text = "Binary Reader Tester";
            // 
            // btnHexadecimal
            // 
            this.btnHexadecimal.Location = new System.Drawing.Point(200, 47);
            this.btnHexadecimal.Name = "btnHexadecimal";
            this.btnHexadecimal.Size = new System.Drawing.Size(88, 23);
            this.btnHexadecimal.TabIndex = 5;
            this.btnHexadecimal.Text = "Hexadecimal";
            this.btnHexadecimal.UseVisualStyleBackColor = true;
            this.btnHexadecimal.Click += new System.EventHandler(this.btnHexadecimal_Click);
            // 
            // btnNumber
            // 
            this.btnNumber.Location = new System.Drawing.Point(106, 47);
            this.btnNumber.Name = "btnNumber";
            this.btnNumber.Size = new System.Drawing.Size(88, 23);
            this.btnNumber.TabIndex = 4;
            this.btnNumber.Text = "Number";
            this.btnNumber.UseVisualStyleBackColor = true;
            this.btnNumber.Click += new System.EventHandler(this.btnNumber_Click);
            // 
            // btnBinary
            // 
            this.btnBinary.Location = new System.Drawing.Point(12, 47);
            this.btnBinary.Name = "btnBinary";
            this.btnBinary.Size = new System.Drawing.Size(88, 23);
            this.btnBinary.TabIndex = 3;
            this.btnBinary.Text = "Binary";
            this.btnBinary.UseVisualStyleBackColor = true;
            this.btnBinary.Click += new System.EventHandler(this.btnBinary_Click);
            // 
            // tbHexadecimal
            // 
            this.tbHexadecimal.Location = new System.Drawing.Point(200, 19);
            this.tbHexadecimal.Name = "tbHexadecimal";
            this.tbHexadecimal.Size = new System.Drawing.Size(88, 20);
            this.tbHexadecimal.TabIndex = 2;
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(106, 19);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(88, 20);
            this.tbNumber.TabIndex = 1;
            // 
            // tbBinary
            // 
            this.tbBinary.Location = new System.Drawing.Point(12, 19);
            this.tbBinary.Name = "tbBinary";
            this.tbBinary.Size = new System.Drawing.Size(88, 20);
            this.tbBinary.TabIndex = 0;
            // 
            // lbTruthTableInfo
            // 
            this.lbTruthTableInfo.FormattingEnabled = true;
            this.lbTruthTableInfo.Location = new System.Drawing.Point(257, 182);
            this.lbTruthTableInfo.Name = "lbTruthTableInfo";
            this.lbTruthTableInfo.Size = new System.Drawing.Size(171, 264);
            this.lbTruthTableInfo.TabIndex = 9;
            // 
            // dgvTruthTable
            // 
            this.dgvTruthTable.AllowUserToOrderColumns = true;
            this.dgvTruthTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTruthTable.Location = new System.Drawing.Point(12, 182);
            this.dgvTruthTable.Name = "dgvTruthTable";
            this.dgvTruthTable.Size = new System.Drawing.Size(239, 264);
            this.dgvTruthTable.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Infix";
            // 
            // tbInfix
            // 
            this.tbInfix.Location = new System.Drawing.Point(38, 39);
            this.tbInfix.Name = "tbInfix";
            this.tbInfix.Size = new System.Drawing.Size(348, 20);
            this.tbInfix.TabIndex = 12;
            // 
            // dgvSimpleTable
            // 
            this.dgvSimpleTable.AllowUserToOrderColumns = true;
            this.dgvSimpleTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSimpleTable.Location = new System.Drawing.Point(434, 182);
            this.dgvSimpleTable.Name = "dgvSimpleTable";
            this.dgvSimpleTable.Size = new System.Drawing.Size(239, 178);
            this.dgvSimpleTable.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Disjunctive Normal Form";
            // 
            // tbDisjunctive
            // 
            this.tbDisjunctive.Location = new System.Drawing.Point(133, 65);
            this.tbDisjunctive.Name = "tbDisjunctive";
            this.tbDisjunctive.Size = new System.Drawing.Size(253, 20);
            this.tbDisjunctive.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Disjunctive Normal Form Simple";
            // 
            // tbDisjunctiveSimple
            // 
            this.tbDisjunctiveSimple.Location = new System.Drawing.Point(163, 91);
            this.tbDisjunctiveSimple.Name = "tbDisjunctiveSimple";
            this.tbDisjunctiveSimple.Size = new System.Drawing.Size(223, 20);
            this.tbDisjunctiveSimple.TabIndex = 17;
            // 
            // tbDisjunctiveParse
            // 
            this.tbDisjunctiveParse.Location = new System.Drawing.Point(392, 65);
            this.tbDisjunctiveParse.Name = "tbDisjunctiveParse";
            this.tbDisjunctiveParse.Size = new System.Drawing.Size(289, 20);
            this.tbDisjunctiveParse.TabIndex = 18;
            // 
            // tbDisjunctiveSimpleParse
            // 
            this.tbDisjunctiveSimpleParse.Location = new System.Drawing.Point(392, 91);
            this.tbDisjunctiveSimpleParse.Name = "tbDisjunctiveSimpleParse";
            this.tbDisjunctiveSimpleParse.Size = new System.Drawing.Size(289, 20);
            this.tbDisjunctiveSimpleParse.TabIndex = 19;
            // 
            // btnParseDisjunctive
            // 
            this.btnParseDisjunctive.Location = new System.Drawing.Point(687, 63);
            this.btnParseDisjunctive.Name = "btnParseDisjunctive";
            this.btnParseDisjunctive.Size = new System.Drawing.Size(44, 23);
            this.btnParseDisjunctive.TabIndex = 20;
            this.btnParseDisjunctive.Text = "Parse";
            this.btnParseDisjunctive.UseVisualStyleBackColor = true;
            this.btnParseDisjunctive.Click += new System.EventHandler(this.btnParseDisjunctive_Click);
            // 
            // btnDisjunctiveSimpleParse
            // 
            this.btnDisjunctiveSimpleParse.Location = new System.Drawing.Point(687, 89);
            this.btnDisjunctiveSimpleParse.Name = "btnDisjunctiveSimpleParse";
            this.btnDisjunctiveSimpleParse.Size = new System.Drawing.Size(44, 23);
            this.btnDisjunctiveSimpleParse.TabIndex = 21;
            this.btnDisjunctiveSimpleParse.Text = "Parse";
            this.btnDisjunctiveSimpleParse.UseVisualStyleBackColor = true;
            this.btnDisjunctiveSimpleParse.Click += new System.EventHandler(this.btnDisjunctiveSimpleParse_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnStartTimer
            // 
            this.btnStartTimer.Location = new System.Drawing.Point(679, 337);
            this.btnStartTimer.Name = "btnStartTimer";
            this.btnStartTimer.Size = new System.Drawing.Size(54, 23);
            this.btnStartTimer.TabIndex = 22;
            this.btnStartTimer.Text = "Start";
            this.btnStartTimer.UseVisualStyleBackColor = true;
            this.btnStartTimer.Click += new System.EventHandler(this.btnStartTimer_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Nand";
            // 
            // tbNand
            // 
            this.tbNand.Location = new System.Drawing.Point(45, 117);
            this.tbNand.Name = "tbNand";
            this.tbNand.Size = new System.Drawing.Size(636, 20);
            this.tbNand.TabIndex = 25;
            // 
            // btnNandParse
            // 
            this.btnNandParse.Location = new System.Drawing.Point(687, 115);
            this.btnNandParse.Name = "btnNandParse";
            this.btnNandParse.Size = new System.Drawing.Size(44, 23);
            this.btnNandParse.TabIndex = 26;
            this.btnNandParse.Text = "Parse";
            this.btnNandParse.UseVisualStyleBackColor = true;
            this.btnNandParse.Click += new System.EventHandler(this.btnNandParse_Click);
            // 
            // btnTableaux
            // 
            this.btnTableaux.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnTableaux.Location = new System.Drawing.Point(442, 10);
            this.btnTableaux.Name = "btnTableaux";
            this.btnTableaux.Size = new System.Drawing.Size(61, 23);
            this.btnTableaux.TabIndex = 27;
            this.btnTableaux.Text = "Tableaux";
            this.btnTableaux.UseVisualStyleBackColor = false;
            this.btnTableaux.Click += new System.EventHandler(this.btnTableaux_Click);
            // 
            // panelInput
            // 
            this.panelInput.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelInput.Controls.Add(this.btnNandSimpleParse);
            this.panelInput.Controls.Add(this.tbNandSimple);
            this.panelInput.Controls.Add(this.label6);
            this.panelInput.Controls.Add(this.label1);
            this.panelInput.Controls.Add(this.btnTableaux);
            this.panelInput.Controls.Add(this.tbDisjunctiveSimple);
            this.panelInput.Controls.Add(this.tbProposition);
            this.panelInput.Controls.Add(this.label4);
            this.panelInput.Controls.Add(this.btnNandParse);
            this.panelInput.Controls.Add(this.tbDisjunctiveParse);
            this.panelInput.Controls.Add(this.btnParseProposition);
            this.panelInput.Controls.Add(this.tbDisjunctive);
            this.panelInput.Controls.Add(this.tbNand);
            this.panelInput.Controls.Add(this.tbDisjunctiveSimpleParse);
            this.panelInput.Controls.Add(this.btnViewTree);
            this.panelInput.Controls.Add(this.label3);
            this.panelInput.Controls.Add(this.label5);
            this.panelInput.Controls.Add(this.btnParseDisjunctive);
            this.panelInput.Controls.Add(this.btnCreateRandomProposition);
            this.panelInput.Controls.Add(this.tbInfix);
            this.panelInput.Controls.Add(this.label2);
            this.panelInput.Controls.Add(this.btnDisjunctiveSimpleParse);
            this.panelInput.Location = new System.Drawing.Point(3, 2);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(731, 174);
            this.panelInput.TabIndex = 29;
            this.panelInput.TabStop = false;
            this.panelInput.Text = "User Input Panel";
            // 
            // btnNandSimpleParse
            // 
            this.btnNandSimpleParse.Location = new System.Drawing.Point(687, 139);
            this.btnNandSimpleParse.Name = "btnNandSimpleParse";
            this.btnNandSimpleParse.Size = new System.Drawing.Size(44, 23);
            this.btnNandSimpleParse.TabIndex = 30;
            this.btnNandSimpleParse.Text = "Parse";
            this.btnNandSimpleParse.UseVisualStyleBackColor = true;
            this.btnNandSimpleParse.Click += new System.EventHandler(this.btnNandSimpleParse_Click);
            // 
            // tbNandSimple
            // 
            this.tbNandSimple.Location = new System.Drawing.Point(79, 141);
            this.tbNandSimple.Name = "tbNandSimple";
            this.tbNandSimple.Size = new System.Drawing.Size(602, 20);
            this.tbNandSimple.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Nand Simple";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(740, 450);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.btnStartTimer);
            this.Controls.Add(this.dgvSimpleTable);
            this.Controls.Add(this.dgvTruthTable);
            this.Controls.Add(this.lbTruthTableInfo);
            this.Controls.Add(this.gbBinaryReaderTester);
            this.Name = "Form1";
            this.Text = " ";
            this.gbBinaryReaderTester.ResumeLayout(false);
            this.gbBinaryReaderTester.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTruthTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSimpleTable)).EndInit();
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbProposition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnParseProposition;
        private System.Windows.Forms.Button btnViewTree;
        private System.Windows.Forms.Button btnCreateRandomProposition;
        private System.Windows.Forms.GroupBox gbBinaryReaderTester;
        private System.Windows.Forms.Button btnHexadecimal;
        private System.Windows.Forms.Button btnNumber;
        private System.Windows.Forms.Button btnBinary;
        private System.Windows.Forms.TextBox tbHexadecimal;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.TextBox tbBinary;
        private System.Windows.Forms.ListBox lbTruthTableInfo;
        private System.Windows.Forms.DataGridView dgvTruthTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbInfix;
        private System.Windows.Forms.DataGridView dgvSimpleTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDisjunctive;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDisjunctiveSimple;
        private System.Windows.Forms.TextBox tbDisjunctiveParse;
        private System.Windows.Forms.TextBox tbDisjunctiveSimpleParse;
        private System.Windows.Forms.Button btnParseDisjunctive;
        private System.Windows.Forms.Button btnDisjunctiveSimpleParse;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnStartTimer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbNand;
        private System.Windows.Forms.Button btnNandParse;
        private System.Windows.Forms.Button btnTableaux;
        private System.Windows.Forms.GroupBox panelInput;
        private System.Windows.Forms.Button btnNandSimpleParse;
        private System.Windows.Forms.TextBox tbNandSimple;
        private System.Windows.Forms.Label label6;
    }
}

