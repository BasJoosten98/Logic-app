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
            this.tbProposition = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnParseProposition = new System.Windows.Forms.Button();
            this.btnViewTree = new System.Windows.Forms.Button();
            this.btnCreateRandomProposition = new System.Windows.Forms.Button();
            this.btnShowArguments = new System.Windows.Forms.Button();
            this.lbTruthTable = new System.Windows.Forms.ListBox();
            this.btnGenerateTruthtable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbProposition
            // 
            this.tbProposition.Location = new System.Drawing.Point(78, 9);
            this.tbProposition.Name = "tbProposition";
            this.tbProposition.Size = new System.Drawing.Size(311, 20);
            this.tbProposition.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Proposition";
            // 
            // btnParseProposition
            // 
            this.btnParseProposition.Location = new System.Drawing.Point(395, 6);
            this.btnParseProposition.Name = "btnParseProposition";
            this.btnParseProposition.Size = new System.Drawing.Size(101, 23);
            this.btnParseProposition.TabIndex = 2;
            this.btnParseProposition.Text = "Parse Proposition";
            this.btnParseProposition.UseVisualStyleBackColor = true;
            this.btnParseProposition.Click += new System.EventHandler(this.btnParseProposition_Click);
            // 
            // btnViewTree
            // 
            this.btnViewTree.Location = new System.Drawing.Point(502, 6);
            this.btnViewTree.Name = "btnViewTree";
            this.btnViewTree.Size = new System.Drawing.Size(75, 23);
            this.btnViewTree.TabIndex = 3;
            this.btnViewTree.Text = "View Tree";
            this.btnViewTree.UseVisualStyleBackColor = true;
            this.btnViewTree.Click += new System.EventHandler(this.btnViewTree_Click);
            // 
            // btnCreateRandomProposition
            // 
            this.btnCreateRandomProposition.Location = new System.Drawing.Point(583, 6);
            this.btnCreateRandomProposition.Name = "btnCreateRandomProposition";
            this.btnCreateRandomProposition.Size = new System.Drawing.Size(151, 23);
            this.btnCreateRandomProposition.TabIndex = 4;
            this.btnCreateRandomProposition.Text = "Create Random Proposition";
            this.btnCreateRandomProposition.UseVisualStyleBackColor = true;
            this.btnCreateRandomProposition.Click += new System.EventHandler(this.btnCreateRandomProposition_Click);
            // 
            // btnShowArguments
            // 
            this.btnShowArguments.Location = new System.Drawing.Point(395, 35);
            this.btnShowArguments.Name = "btnShowArguments";
            this.btnShowArguments.Size = new System.Drawing.Size(101, 23);
            this.btnShowArguments.TabIndex = 5;
            this.btnShowArguments.Text = "Show Arguments";
            this.btnShowArguments.UseVisualStyleBackColor = true;
            this.btnShowArguments.Click += new System.EventHandler(this.btnShowArguments_Click);
            // 
            // lbTruthTable
            // 
            this.lbTruthTable.FormattingEnabled = true;
            this.lbTruthTable.Location = new System.Drawing.Point(12, 169);
            this.lbTruthTable.Name = "lbTruthTable";
            this.lbTruthTable.Size = new System.Drawing.Size(183, 277);
            this.lbTruthTable.TabIndex = 6;
            // 
            // btnGenerateTruthtable
            // 
            this.btnGenerateTruthtable.Location = new System.Drawing.Point(12, 140);
            this.btnGenerateTruthtable.Name = "btnGenerateTruthtable";
            this.btnGenerateTruthtable.Size = new System.Drawing.Size(183, 23);
            this.btnGenerateTruthtable.TabIndex = 7;
            this.btnGenerateTruthtable.Text = "Generate Truthtable";
            this.btnGenerateTruthtable.UseVisualStyleBackColor = true;
            this.btnGenerateTruthtable.Click += new System.EventHandler(this.btnGenerateTruthtable_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 450);
            this.Controls.Add(this.btnGenerateTruthtable);
            this.Controls.Add(this.lbTruthTable);
            this.Controls.Add(this.btnShowArguments);
            this.Controls.Add(this.btnCreateRandomProposition);
            this.Controls.Add(this.btnViewTree);
            this.Controls.Add(this.btnParseProposition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbProposition);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbProposition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnParseProposition;
        private System.Windows.Forms.Button btnViewTree;
        private System.Windows.Forms.Button btnCreateRandomProposition;
        private System.Windows.Forms.Button btnShowArguments;
        private System.Windows.Forms.ListBox lbTruthTable;
        private System.Windows.Forms.Button btnGenerateTruthtable;
    }
}

