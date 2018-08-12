namespace GeneratePOCO
{
    partial class FormMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtFilterAll = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPOCOTemplate = new System.Windows.Forms.TextBox();
            this.txtContextTemplate = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddToGenerate = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.grdAll = new GeneratePOCO.CommonGrid();
            this.grdTo = new GeneratePOCO.CommonGrid();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.AutoSize = true;
            this.btnGenerate.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGenerate.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGenerate.Location = new System.Drawing.Point(567, 36);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(112, 27);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate POCO";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "ConnectionString";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.BackColor = System.Drawing.Color.LavenderBlush;
            this.txtConnectionString.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtConnectionString.Location = new System.Drawing.Point(179, 6);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.ReadOnly = true;
            this.txtConnectionString.Size = new System.Drawing.Size(873, 23);
            this.txtConnectionString.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.tableLayoutPanel3.SetRowSpan(this.groupBox1, 2);
            this.groupBox1.Size = new System.Drawing.Size(558, 439);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tables in Database";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grdAll, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtFilterAll, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(552, 417);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // txtFilterAll
            // 
            this.txtFilterAll.Location = new System.Drawing.Point(48, 3);
            this.txtFilterAll.Name = "txtFilterAll";
            this.txtFilterAll.Size = new System.Drawing.Size(311, 23);
            this.txtFilterAll.TabIndex = 2;
            this.txtFilterAll.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Filter:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.groupBox2.Location = new System.Drawing.Point(685, 3);
            this.groupBox2.Name = "groupBox2";
            this.tableLayoutPanel3.SetRowSpan(this.groupBox2, 2);
            this.groupBox2.Size = new System.Drawing.Size(376, 439);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tables To Generate";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.grdTo, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 423F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(370, 417);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtPOCOTemplate);
            this.panel1.Controls.Add(this.txtContextTemplate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtConnectionString);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1064, 101);
            this.panel1.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Red;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.label4.Location = new System.Drawing.Point(12, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "POCO Class Template:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Red;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.label3.Location = new System.Drawing.Point(12, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "DbcontextClass Template:";
            // 
            // txtPOCOTemplate
            // 
            this.txtPOCOTemplate.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPOCOTemplate.Location = new System.Drawing.Point(179, 67);
            this.txtPOCOTemplate.Name = "txtPOCOTemplate";
            this.txtPOCOTemplate.ReadOnly = true;
            this.txtPOCOTemplate.Size = new System.Drawing.Size(873, 23);
            this.txtPOCOTemplate.TabIndex = 2;
            // 
            // txtContextTemplate
            // 
            this.txtContextTemplate.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtContextTemplate.Location = new System.Drawing.Point(179, 34);
            this.txtContextTemplate.Name = "txtContextTemplate";
            this.txtContextTemplate.ReadOnly = true;
            this.txtContextTemplate.Size = new System.Drawing.Size(873, 23);
            this.txtContextTemplate.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 118F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox2, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnAddToGenerate, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnGenerate, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtMessage, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 101);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79.70004F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.29996F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1064, 550);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // btnAddToGenerate
            // 
            this.btnAddToGenerate.AutoSize = true;
            this.btnAddToGenerate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddToGenerate.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddToGenerate.Location = new System.Drawing.Point(567, 3);
            this.btnAddToGenerate.Name = "btnAddToGenerate";
            this.btnAddToGenerate.Size = new System.Drawing.Size(112, 27);
            this.btnAddToGenerate.TabIndex = 0;
            this.btnAddToGenerate.Text = "Add Selected";
            this.btnAddToGenerate.UseVisualStyleBackColor = true;
            this.btnAddToGenerate.Click += new System.EventHandler(this.btnAddToGenerate_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel3.SetColumnSpan(this.txtMessage, 3);
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.ForeColor = System.Drawing.Color.Black;
            this.txtMessage.Location = new System.Drawing.Point(3, 448);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(1058, 99);
            this.txtMessage.TabIndex = 6;
            this.txtMessage.Text = "";
            // 
            // grdAll
            // 
            this.grdAll.AllowUserToAddRows = false;
            this.grdAll.AllowUserToDeleteRows = false;
            this.grdAll.AllowUserToResizeRows = false;
            this.grdAll.AutoCheckBox = true;
            this.grdAll.AutoFilter = false;
            this.grdAll.AutoResize = true;
            this.grdAll.BigSize = false;
            this.grdAll.CheckBoxSyncSelectedRows = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdAll.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.grdAll, 2);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGreen;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdAll.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAll.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdAll.Location = new System.Drawing.Point(3, 32);
            this.grdAll.MouseUpCommit = false;
            this.grdAll.Name = "grdAll";
            this.grdAll.RowHeadersVisible = false;
            this.grdAll.RowTemplate.Height = 23;
            this.grdAll.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdAll.SetHeaderText = true;
            this.grdAll.ShowHeaderCheckBox = true;
            this.grdAll.Size = new System.Drawing.Size(546, 382);
            this.grdAll.Sortable = true;
            this.grdAll.TabIndex = 3;
            // 
            // grdTo
            // 
            this.grdTo.AllowUserToAddRows = false;
            this.grdTo.AllowUserToDeleteRows = false;
            this.grdTo.AllowUserToResizeRows = false;
            this.grdTo.AutoCheckBox = false;
            this.grdTo.AutoFilter = false;
            this.grdTo.AutoResize = true;
            this.grdTo.BigSize = false;
            this.grdTo.CheckBoxSyncSelectedRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdTo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel2.SetColumnSpan(this.grdTo, 2);
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightGreen;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTo.DefaultCellStyle = dataGridViewCellStyle4;
            this.grdTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grdTo.Location = new System.Drawing.Point(3, 3);
            this.grdTo.MouseUpCommit = false;
            this.grdTo.Name = "grdTo";
            this.grdTo.ReadOnly = true;
            this.grdTo.RowHeadersVisible = false;
            this.grdTo.RowTemplate.Height = 23;
            this.grdTo.SetHeaderText = true;
            this.grdTo.ShowHeaderCheckBox = false;
            this.grdTo.Size = new System.Drawing.Size(364, 411);
            this.grdTo.Sortable = true;
            this.grdTo.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 651);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "POCO Generater";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConnectionString;
        private CommonGrid grdAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtFilterAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private CommonGrid grdTo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnAddToGenerate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtContextTemplate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPOCOTemplate;
        private System.Windows.Forms.RichTextBox txtMessage;
    }
}

