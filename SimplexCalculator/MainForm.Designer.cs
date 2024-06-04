namespace SimplexCalculator
{
    partial class MainForm
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
            this.nOfContraintsTextBox = new System.Windows.Forms.TextBox();
            this.nOfVariablesTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.constraintsGridView = new System.Windows.Forms.DataGridView();
            this.functionGridView = new System.Windows.Forms.DataGridView();
            this.extrComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.resultsGridView = new System.Windows.Forms.DataGridView();
            this.goBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.defaultBtn = new System.Windows.Forms.Button();
            this.resultsLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.constraintsGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.functionGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // nOfContraintsTextBox
            // 
            this.nOfContraintsTextBox.Location = new System.Drawing.Point(496, 14);
            this.nOfContraintsTextBox.Name = "nOfContraintsTextBox";
            this.nOfContraintsTextBox.Size = new System.Drawing.Size(49, 29);
            this.nOfContraintsTextBox.TabIndex = 0;
            this.nOfContraintsTextBox.TextChanged += new System.EventHandler(this.nOfVariablesTextBox_TextChanged);
            // 
            // nOfVariablesTextBox
            // 
            this.nOfVariablesTextBox.Location = new System.Drawing.Point(259, 14);
            this.nOfVariablesTextBox.Name = "nOfVariablesTextBox";
            this.nOfVariablesTextBox.Size = new System.Drawing.Size(49, 29);
            this.nOfVariablesTextBox.TabIndex = 1;
            this.nOfVariablesTextBox.TextChanged += new System.EventHandler(this.nOfVariablesTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(336, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Кількість обмежень:";
            // 
            // constraintsGridView
            // 
            this.constraintsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.constraintsGridView.BackgroundColor = System.Drawing.Color.White;
            this.constraintsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.constraintsGridView.Location = new System.Drawing.Point(82, 192);
            this.constraintsGridView.Name = "constraintsGridView";
            this.constraintsGridView.Size = new System.Drawing.Size(865, 123);
            this.constraintsGridView.TabIndex = 5;
            this.constraintsGridView.SelectionChanged += new System.EventHandler(this.DataGridView_SelectionChanged);
            // 
            // functionGridView
            // 
            this.functionGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.functionGridView.BackgroundColor = System.Drawing.Color.White;
            this.functionGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.functionGridView.Location = new System.Drawing.Point(82, 94);
            this.functionGridView.Name = "functionGridView";
            this.functionGridView.Size = new System.Drawing.Size(757, 62);
            this.functionGridView.TabIndex = 6;
            this.functionGridView.SelectionChanged += new System.EventHandler(this.DataGridView_SelectionChanged);
            // 
            // extrComboBox
            // 
            this.extrComboBox.AllowDrop = true;
            this.extrComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.extrComboBox.FormattingEnabled = true;
            this.extrComboBox.Items.AddRange(new object[] {
            "max",
            "min"});
            this.extrComboBox.Location = new System.Drawing.Point(878, 108);
            this.extrComboBox.Name = "extrComboBox";
            this.extrComboBox.Size = new System.Drawing.Size(69, 29);
            this.extrComboBox.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Кількість змінних:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(845, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "->";
            // 
            // resultsGridView
            // 
            this.resultsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultsGridView.BackgroundColor = System.Drawing.Color.White;
            this.resultsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultsGridView.Location = new System.Drawing.Point(28, 390);
            this.resultsGridView.Name = "resultsGridView";
            this.resultsGridView.Size = new System.Drawing.Size(980, 287);
            this.resultsGridView.TabIndex = 9;
            this.resultsGridView.SelectionChanged += new System.EventHandler(this.DataGridView_SelectionChanged);
            // 
            // goBtn
            // 
            this.goBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.goBtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.goBtn.Location = new System.Drawing.Point(299, 334);
            this.goBtn.Name = "goBtn";
            this.goBtn.Size = new System.Drawing.Size(608, 41);
            this.goBtn.TabIndex = 10;
            this.goBtn.Text = "Розрахувати";
            this.goBtn.UseVisualStyleBackColor = false;
            this.goBtn.Click += new System.EventHandler(this.goBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.clearBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.clearBtn.Location = new System.Drawing.Point(116, 334);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(157, 41);
            this.clearBtn.TabIndex = 11;
            this.clearBtn.Text = "Очистити";
            this.clearBtn.UseVisualStyleBackColor = false;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // defaultBtn
            // 
            this.defaultBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultBtn.Location = new System.Drawing.Point(890, 17);
            this.defaultBtn.Name = "defaultBtn";
            this.defaultBtn.Size = new System.Drawing.Size(118, 36);
            this.defaultBtn.TabIndex = 12;
            this.defaultBtn.Text = "Моя задача";
            this.defaultBtn.UseVisualStyleBackColor = true;
            this.defaultBtn.Click += new System.EventHandler(this.defaultBtn_Click);
            // 
            // resultsLbl
            // 
            this.resultsLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.resultsLbl.AutoSize = true;
            this.resultsLbl.Location = new System.Drawing.Point(20, 688);
            this.resultsLbl.Name = "resultsLbl";
            this.resultsLbl.Size = new System.Drawing.Size(0, 21);
            this.resultsLbl.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(435, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 21);
            this.label4.TabIndex = 14;
            this.label4.Text = "Цільова функція:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(435, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 21);
            this.label5.TabIndex = 15;
            this.label5.Text = "Умови-обмеження:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1027, 728);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.resultsLbl);
            this.Controls.Add(this.defaultBtn);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.goBtn);
            this.Controls.Add(this.resultsGridView);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.extrComboBox);
            this.Controls.Add(this.functionGridView);
            this.Controls.Add(this.constraintsGridView);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nOfVariablesTextBox);
            this.Controls.Add(this.nOfContraintsTextBox);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MainForm";
            this.Text = "Simplex Calculator";
            ((System.ComponentModel.ISupportInitialize)(this.constraintsGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.functionGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nOfContraintsTextBox;
        private System.Windows.Forms.TextBox nOfVariablesTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView constraintsGridView;
        private System.Windows.Forms.DataGridView functionGridView;
        private System.Windows.Forms.ComboBox extrComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView resultsGridView;
        private System.Windows.Forms.Button goBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.Button defaultBtn;
        private System.Windows.Forms.Label resultsLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

