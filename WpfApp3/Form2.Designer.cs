namespace WpfApp3
{
    partial class Form2
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_QUANTITY = new System.Windows.Forms.TextBox();
            this.textBox_RATE = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_DISCOUNT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_TAX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_AMOUNT = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(143, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Item Details";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Quantity";
            // 
            // textBox_QUANTITY
            // 
            this.textBox_QUANTITY.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBox_QUANTITY.Location = new System.Drawing.Point(143, 36);
            this.textBox_QUANTITY.Name = "textBox_QUANTITY";
            this.textBox_QUANTITY.Size = new System.Drawing.Size(121, 20);
            this.textBox_QUANTITY.TabIndex = 3;
            this.textBox_QUANTITY.TextChanged += new System.EventHandler(this.textBox_QUANTITY_TextChanged);
            // 
            // textBox_RATE
            // 
            this.textBox_RATE.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBox_RATE.Location = new System.Drawing.Point(146, 73);
            this.textBox_RATE.Name = "textBox_RATE";
            this.textBox_RATE.Size = new System.Drawing.Size(121, 20);
            this.textBox_RATE.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Rate";
            // 
            // textBox_DISCOUNT
            // 
            this.textBox_DISCOUNT.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBox_DISCOUNT.Location = new System.Drawing.Point(145, 115);
            this.textBox_DISCOUNT.Name = "textBox_DISCOUNT";
            this.textBox_DISCOUNT.Size = new System.Drawing.Size(121, 20);
            this.textBox_DISCOUNT.TabIndex = 7;
            this.textBox_DISCOUNT.TextChanged += new System.EventHandler(this.textBox_DISCOUNT_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Discount";
            // 
            // textBox_TAX
            // 
            this.textBox_TAX.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBox_TAX.Location = new System.Drawing.Point(146, 152);
            this.textBox_TAX.Name = "textBox_TAX";
            this.textBox_TAX.Size = new System.Drawing.Size(121, 20);
            this.textBox_TAX.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Tax";
            // 
            // textBox_AMOUNT
            // 
            this.textBox_AMOUNT.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textBox_AMOUNT.Location = new System.Drawing.Point(146, 185);
            this.textBox_AMOUNT.Name = "textBox_AMOUNT";
            this.textBox_AMOUNT.Size = new System.Drawing.Size(121, 20);
            this.textBox_AMOUNT.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Amount";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Location = new System.Drawing.Point(96, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.Location = new System.Drawing.Point(186, 233);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(271, 262);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_AMOUNT);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_TAX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_DISCOUNT);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_RATE);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_QUANTITY);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form2";
            this.Text = "Add Item for SalesInvoice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_QUANTITY;
        private System.Windows.Forms.TextBox textBox_RATE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_DISCOUNT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_TAX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_AMOUNT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}