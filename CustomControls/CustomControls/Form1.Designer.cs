
namespace CustomControls
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
            this.button1 = new System.Windows.Forms.Button();
            this.myButton2 = new CustomControls.MyButton();
            this.myButton1 = new CustomControls.MyButton();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(187, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(357, 155);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Paint += new System.Windows.Forms.PaintEventHandler(this.button1_Paint);
            // 
            // myButton2
            // 
            this.myButton2.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.myButton2.Location = new System.Drawing.Point(187, 291);
            this.myButton2.Name = "myButton2";
            this.myButton2.Size = new System.Drawing.Size(423, 150);
            this.myButton2.TabIndex = 1;
            this.myButton2.Text = "LALALA";
            this.myButton2.UseVisualStyleBackColor = false;
            // 
            // myButton1
            // 
            this.myButton1.BackColor = System.Drawing.Color.DeepPink;
            this.myButton1.Location = new System.Drawing.Point(616, 271);
            this.myButton1.Name = "myButton1";
            this.myButton1.Size = new System.Drawing.Size(423, 150);
            this.myButton1.TabIndex = 1;
            this.myButton1.Text = "myButton1";
            this.myButton1.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1207, 637);
            this.Controls.Add(this.myButton2);
            this.Controls.Add(this.myButton1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private MyButton myButton1;
        private MyButton myButton2;
    }
}

