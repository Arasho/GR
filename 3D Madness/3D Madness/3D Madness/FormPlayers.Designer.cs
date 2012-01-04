namespace _3D_Madness
{
    partial class FormPlayers
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
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radio_p1_black = new System.Windows.Forms.RadioButton();
            this.radio_p1_green = new System.Windows.Forms.RadioButton();
            this.radio_p1_blue = new System.Windows.Forms.RadioButton();
            this.radio_p1_red = new System.Windows.Forms.RadioButton();
            this.radio_p1_yellow = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radio_p2_black = new System.Windows.Forms.RadioButton();
            this.radio_p2_green = new System.Windows.Forms.RadioButton();
            this.radio_p2_blue = new System.Windows.Forms.RadioButton();
            this.radio_p2_red = new System.Windows.Forms.RadioButton();
            this.radio_p2_yellow = new System.Windows.Forms.RadioButton();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radio_p3_black = new System.Windows.Forms.RadioButton();
            this.radio_p3_green = new System.Windows.Forms.RadioButton();
            this.radio_p3_blue = new System.Windows.Forms.RadioButton();
            this.radio_p3_red = new System.Windows.Forms.RadioButton();
            this.radio_p3_yellow = new System.Windows.Forms.RadioButton();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radio_p4_black = new System.Windows.Forms.RadioButton();
            this.radio_p4_green = new System.Windows.Forms.RadioButton();
            this.radio_p4_blue = new System.Windows.Forms.RadioButton();
            this.radio_p4_red = new System.Windows.Forms.RadioButton();
            this.radio_p4_yellow = new System.Windows.Forms.RadioButton();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radio_p5_black = new System.Windows.Forms.RadioButton();
            this.radio_p5_green = new System.Windows.Forms.RadioButton();
            this.radio_p5_blue = new System.Windows.Forms.RadioButton();
            this.radio_p5_red = new System.Windows.Forms.RadioButton();
            this.radio_p5_yellow = new System.Windows.Forms.RadioButton();
            this.textBox5 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(95, 247);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Wybiesz liczbę graczy:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(140, 14);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(96, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(140, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Kolor:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Nazwa:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radio_p1_black);
            this.groupBox1.Controls.Add(this.radio_p1_green);
            this.groupBox1.Controls.Add(this.radio_p1_blue);
            this.groupBox1.Controls.Add(this.radio_p1_red);
            this.groupBox1.Controls.Add(this.radio_p1_yellow);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(21, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 35);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            // 
            // radio_p1_black
            // 
            this.radio_p1_black.AutoSize = true;
            this.radio_p1_black.BackColor = System.Drawing.Color.Black;
            this.radio_p1_black.Location = new System.Drawing.Point(177, 13);
            this.radio_p1_black.Name = "radio_p1_black";
            this.radio_p1_black.Size = new System.Drawing.Size(14, 13);
            this.radio_p1_black.TabIndex = 49;
            this.radio_p1_black.TabStop = true;
            this.radio_p1_black.UseVisualStyleBackColor = false;
            this.radio_p1_black.CheckedChanged += new System.EventHandler(this.radio_p1_black_CheckedChanged);
            // 
            // radio_p1_green
            // 
            this.radio_p1_green.AutoSize = true;
            this.radio_p1_green.BackColor = System.Drawing.Color.Lime;
            this.radio_p1_green.Location = new System.Drawing.Point(163, 13);
            this.radio_p1_green.Name = "radio_p1_green";
            this.radio_p1_green.Size = new System.Drawing.Size(14, 13);
            this.radio_p1_green.TabIndex = 48;
            this.radio_p1_green.TabStop = true;
            this.radio_p1_green.UseVisualStyleBackColor = false;
            this.radio_p1_green.CheckedChanged += new System.EventHandler(this.radio_p1_green_CheckedChanged);
            // 
            // radio_p1_blue
            // 
            this.radio_p1_blue.AutoSize = true;
            this.radio_p1_blue.BackColor = System.Drawing.Color.Blue;
            this.radio_p1_blue.Location = new System.Drawing.Point(149, 13);
            this.radio_p1_blue.Name = "radio_p1_blue";
            this.radio_p1_blue.Size = new System.Drawing.Size(14, 13);
            this.radio_p1_blue.TabIndex = 47;
            this.radio_p1_blue.TabStop = true;
            this.radio_p1_blue.UseVisualStyleBackColor = false;
            this.radio_p1_blue.CheckedChanged += new System.EventHandler(this.radio_p1_blue_CheckedChanged);
            // 
            // radio_p1_red
            // 
            this.radio_p1_red.AutoSize = true;
            this.radio_p1_red.BackColor = System.Drawing.Color.Red;
            this.radio_p1_red.Location = new System.Drawing.Point(135, 13);
            this.radio_p1_red.Name = "radio_p1_red";
            this.radio_p1_red.Size = new System.Drawing.Size(14, 13);
            this.radio_p1_red.TabIndex = 46;
            this.radio_p1_red.TabStop = true;
            this.radio_p1_red.UseVisualStyleBackColor = false;
            this.radio_p1_red.CheckedChanged += new System.EventHandler(this.radio_p1_red_CheckedChanged);
            // 
            // radio_p1_yellow
            // 
            this.radio_p1_yellow.AutoSize = true;
            this.radio_p1_yellow.BackColor = System.Drawing.Color.Yellow;
            this.radio_p1_yellow.Location = new System.Drawing.Point(120, 13);
            this.radio_p1_yellow.Name = "radio_p1_yellow";
            this.radio_p1_yellow.Size = new System.Drawing.Size(14, 13);
            this.radio_p1_yellow.TabIndex = 45;
            this.radio_p1_yellow.TabStop = true;
            this.radio_p1_yellow.UseVisualStyleBackColor = false;
            this.radio_p1_yellow.CheckedChanged += new System.EventHandler(this.radio_p1_yellow_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(10, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 44;
            this.textBox1.Text = "Gracz 1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radio_p2_black);
            this.groupBox2.Controls.Add(this.radio_p2_green);
            this.groupBox2.Controls.Add(this.radio_p2_blue);
            this.groupBox2.Controls.Add(this.radio_p2_red);
            this.groupBox2.Controls.Add(this.radio_p2_yellow);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Location = new System.Drawing.Point(21, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 36);
            this.groupBox2.TabIndex = 89;
            this.groupBox2.TabStop = false;
            // 
            // radio_p2_black
            // 
            this.radio_p2_black.AutoSize = true;
            this.radio_p2_black.BackColor = System.Drawing.Color.Black;
            this.radio_p2_black.Location = new System.Drawing.Point(178, 15);
            this.radio_p2_black.Name = "radio_p2_black";
            this.radio_p2_black.Size = new System.Drawing.Size(14, 13);
            this.radio_p2_black.TabIndex = 79;
            this.radio_p2_black.TabStop = true;
            this.radio_p2_black.UseVisualStyleBackColor = false;
            this.radio_p2_black.CheckedChanged += new System.EventHandler(this.radio_p2_black_CheckedChanged);
            // 
            // radio_p2_green
            // 
            this.radio_p2_green.AutoSize = true;
            this.radio_p2_green.BackColor = System.Drawing.Color.Lime;
            this.radio_p2_green.Location = new System.Drawing.Point(164, 15);
            this.radio_p2_green.Name = "radio_p2_green";
            this.radio_p2_green.Size = new System.Drawing.Size(14, 13);
            this.radio_p2_green.TabIndex = 78;
            this.radio_p2_green.TabStop = true;
            this.radio_p2_green.UseVisualStyleBackColor = false;
            this.radio_p2_green.CheckedChanged += new System.EventHandler(this.radio_p2_green_CheckedChanged);
            // 
            // radio_p2_blue
            // 
            this.radio_p2_blue.AutoSize = true;
            this.radio_p2_blue.BackColor = System.Drawing.Color.Blue;
            this.radio_p2_blue.Location = new System.Drawing.Point(150, 15);
            this.radio_p2_blue.Name = "radio_p2_blue";
            this.radio_p2_blue.Size = new System.Drawing.Size(14, 13);
            this.radio_p2_blue.TabIndex = 77;
            this.radio_p2_blue.TabStop = true;
            this.radio_p2_blue.UseVisualStyleBackColor = false;
            this.radio_p2_blue.CheckedChanged += new System.EventHandler(this.radio_p2_blue_CheckedChanged);
            // 
            // radio_p2_red
            // 
            this.radio_p2_red.AutoSize = true;
            this.radio_p2_red.BackColor = System.Drawing.Color.Red;
            this.radio_p2_red.Location = new System.Drawing.Point(136, 15);
            this.radio_p2_red.Name = "radio_p2_red";
            this.radio_p2_red.Size = new System.Drawing.Size(14, 13);
            this.radio_p2_red.TabIndex = 76;
            this.radio_p2_red.TabStop = true;
            this.radio_p2_red.UseVisualStyleBackColor = false;
            this.radio_p2_red.CheckedChanged += new System.EventHandler(this.radio_p2_red_CheckedChanged);
            // 
            // radio_p2_yellow
            // 
            this.radio_p2_yellow.AutoSize = true;
            this.radio_p2_yellow.BackColor = System.Drawing.Color.Yellow;
            this.radio_p2_yellow.Location = new System.Drawing.Point(121, 15);
            this.radio_p2_yellow.Name = "radio_p2_yellow";
            this.radio_p2_yellow.Size = new System.Drawing.Size(14, 13);
            this.radio_p2_yellow.TabIndex = 75;
            this.radio_p2_yellow.TabStop = true;
            this.radio_p2_yellow.UseVisualStyleBackColor = false;
            this.radio_p2_yellow.CheckedChanged += new System.EventHandler(this.radio_p2_yellow_CheckedChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(11, 11);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 74;
            this.textBox2.Text = "Gracz 2";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radio_p3_black);
            this.groupBox3.Controls.Add(this.radio_p3_green);
            this.groupBox3.Controls.Add(this.radio_p3_blue);
            this.groupBox3.Controls.Add(this.radio_p3_red);
            this.groupBox3.Controls.Add(this.radio_p3_yellow);
            this.groupBox3.Controls.Add(this.textBox3);
            this.groupBox3.Location = new System.Drawing.Point(21, 121);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 39);
            this.groupBox3.TabIndex = 90;
            this.groupBox3.TabStop = false;
            // 
            // radio_p3_black
            // 
            this.radio_p3_black.AutoSize = true;
            this.radio_p3_black.BackColor = System.Drawing.Color.Black;
            this.radio_p3_black.Location = new System.Drawing.Point(177, 16);
            this.radio_p3_black.Name = "radio_p3_black";
            this.radio_p3_black.Size = new System.Drawing.Size(14, 13);
            this.radio_p3_black.TabIndex = 84;
            this.radio_p3_black.TabStop = true;
            this.radio_p3_black.UseVisualStyleBackColor = false;
            this.radio_p3_black.CheckedChanged += new System.EventHandler(this.radio_p3_black_CheckedChanged);
            // 
            // radio_p3_green
            // 
            this.radio_p3_green.AutoSize = true;
            this.radio_p3_green.BackColor = System.Drawing.Color.Lime;
            this.radio_p3_green.Location = new System.Drawing.Point(163, 16);
            this.radio_p3_green.Name = "radio_p3_green";
            this.radio_p3_green.Size = new System.Drawing.Size(14, 13);
            this.radio_p3_green.TabIndex = 83;
            this.radio_p3_green.TabStop = true;
            this.radio_p3_green.UseVisualStyleBackColor = false;
            this.radio_p3_green.CheckedChanged += new System.EventHandler(this.radio_p3_green_CheckedChanged);
            // 
            // radio_p3_blue
            // 
            this.radio_p3_blue.AutoSize = true;
            this.radio_p3_blue.BackColor = System.Drawing.Color.Blue;
            this.radio_p3_blue.Location = new System.Drawing.Point(149, 16);
            this.radio_p3_blue.Name = "radio_p3_blue";
            this.radio_p3_blue.Size = new System.Drawing.Size(14, 13);
            this.radio_p3_blue.TabIndex = 82;
            this.radio_p3_blue.TabStop = true;
            this.radio_p3_blue.UseVisualStyleBackColor = false;
            this.radio_p3_blue.CheckedChanged += new System.EventHandler(this.radio_p3_blue_CheckedChanged);
            // 
            // radio_p3_red
            // 
            this.radio_p3_red.AutoSize = true;
            this.radio_p3_red.BackColor = System.Drawing.Color.Red;
            this.radio_p3_red.Location = new System.Drawing.Point(135, 16);
            this.radio_p3_red.Name = "radio_p3_red";
            this.radio_p3_red.Size = new System.Drawing.Size(14, 13);
            this.radio_p3_red.TabIndex = 81;
            this.radio_p3_red.TabStop = true;
            this.radio_p3_red.UseVisualStyleBackColor = false;
            this.radio_p3_red.CheckedChanged += new System.EventHandler(this.radio_p3_red_CheckedChanged);
            // 
            // radio_p3_yellow
            // 
            this.radio_p3_yellow.AutoSize = true;
            this.radio_p3_yellow.BackColor = System.Drawing.Color.Yellow;
            this.radio_p3_yellow.Location = new System.Drawing.Point(120, 16);
            this.radio_p3_yellow.Name = "radio_p3_yellow";
            this.radio_p3_yellow.Size = new System.Drawing.Size(14, 13);
            this.radio_p3_yellow.TabIndex = 80;
            this.radio_p3_yellow.TabStop = true;
            this.radio_p3_yellow.UseVisualStyleBackColor = false;
            this.radio_p3_yellow.CheckedChanged += new System.EventHandler(this.radio_p3_yellow_CheckedChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(10, 14);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 79;
            this.textBox3.Text = "Gracz 3";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radio_p4_black);
            this.groupBox4.Controls.Add(this.radio_p4_green);
            this.groupBox4.Controls.Add(this.radio_p4_blue);
            this.groupBox4.Controls.Add(this.radio_p4_red);
            this.groupBox4.Controls.Add(this.radio_p4_yellow);
            this.groupBox4.Controls.Add(this.textBox4);
            this.groupBox4.Location = new System.Drawing.Point(21, 156);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 38);
            this.groupBox4.TabIndex = 91;
            this.groupBox4.TabStop = false;
            // 
            // radio_p4_black
            // 
            this.radio_p4_black.AutoSize = true;
            this.radio_p4_black.BackColor = System.Drawing.Color.Black;
            this.radio_p4_black.Location = new System.Drawing.Point(177, 15);
            this.radio_p4_black.Name = "radio_p4_black";
            this.radio_p4_black.Size = new System.Drawing.Size(14, 13);
            this.radio_p4_black.TabIndex = 89;
            this.radio_p4_black.TabStop = true;
            this.radio_p4_black.UseVisualStyleBackColor = false;
            this.radio_p4_black.CheckedChanged += new System.EventHandler(this.radio_p4_black_CheckedChanged);
            // 
            // radio_p4_green
            // 
            this.radio_p4_green.AutoSize = true;
            this.radio_p4_green.BackColor = System.Drawing.Color.Lime;
            this.radio_p4_green.Location = new System.Drawing.Point(163, 15);
            this.radio_p4_green.Name = "radio_p4_green";
            this.radio_p4_green.Size = new System.Drawing.Size(14, 13);
            this.radio_p4_green.TabIndex = 88;
            this.radio_p4_green.TabStop = true;
            this.radio_p4_green.UseVisualStyleBackColor = false;
            this.radio_p4_green.CheckedChanged += new System.EventHandler(this.radio_p4_green_CheckedChanged);
            // 
            // radio_p4_blue
            // 
            this.radio_p4_blue.AutoSize = true;
            this.radio_p4_blue.BackColor = System.Drawing.Color.Blue;
            this.radio_p4_blue.Location = new System.Drawing.Point(149, 15);
            this.radio_p4_blue.Name = "radio_p4_blue";
            this.radio_p4_blue.Size = new System.Drawing.Size(14, 13);
            this.radio_p4_blue.TabIndex = 87;
            this.radio_p4_blue.TabStop = true;
            this.radio_p4_blue.UseVisualStyleBackColor = false;
            this.radio_p4_blue.CheckedChanged += new System.EventHandler(this.radio_p4_blue_CheckedChanged);
            // 
            // radio_p4_red
            // 
            this.radio_p4_red.AutoSize = true;
            this.radio_p4_red.BackColor = System.Drawing.Color.Red;
            this.radio_p4_red.Location = new System.Drawing.Point(135, 15);
            this.radio_p4_red.Name = "radio_p4_red";
            this.radio_p4_red.Size = new System.Drawing.Size(14, 13);
            this.radio_p4_red.TabIndex = 86;
            this.radio_p4_red.TabStop = true;
            this.radio_p4_red.UseVisualStyleBackColor = false;
            this.radio_p4_red.CheckedChanged += new System.EventHandler(this.radio_p4_red_CheckedChanged);
            // 
            // radio_p4_yellow
            // 
            this.radio_p4_yellow.AutoSize = true;
            this.radio_p4_yellow.BackColor = System.Drawing.Color.Yellow;
            this.radio_p4_yellow.Location = new System.Drawing.Point(120, 15);
            this.radio_p4_yellow.Name = "radio_p4_yellow";
            this.radio_p4_yellow.Size = new System.Drawing.Size(14, 13);
            this.radio_p4_yellow.TabIndex = 85;
            this.radio_p4_yellow.TabStop = true;
            this.radio_p4_yellow.UseVisualStyleBackColor = false;
            this.radio_p4_yellow.CheckedChanged += new System.EventHandler(this.radio_p4_yellow_CheckedChanged);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(10, 12);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 84;
            this.textBox4.Text = "Gracz 4";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radio_p5_black);
            this.groupBox5.Controls.Add(this.radio_p5_green);
            this.groupBox5.Controls.Add(this.radio_p5_blue);
            this.groupBox5.Controls.Add(this.radio_p5_red);
            this.groupBox5.Controls.Add(this.radio_p5_yellow);
            this.groupBox5.Controls.Add(this.textBox5);
            this.groupBox5.Location = new System.Drawing.Point(21, 190);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 36);
            this.groupBox5.TabIndex = 92;
            this.groupBox5.TabStop = false;
            // 
            // radio_p5_black
            // 
            this.radio_p5_black.AutoSize = true;
            this.radio_p5_black.BackColor = System.Drawing.Color.Black;
            this.radio_p5_black.Location = new System.Drawing.Point(177, 14);
            this.radio_p5_black.Name = "radio_p5_black";
            this.radio_p5_black.Size = new System.Drawing.Size(14, 13);
            this.radio_p5_black.TabIndex = 94;
            this.radio_p5_black.TabStop = true;
            this.radio_p5_black.UseVisualStyleBackColor = false;
            this.radio_p5_black.CheckedChanged += new System.EventHandler(this.radio_p5_black_CheckedChanged);
            // 
            // radio_p5_green
            // 
            this.radio_p5_green.AutoSize = true;
            this.radio_p5_green.BackColor = System.Drawing.Color.Lime;
            this.radio_p5_green.Location = new System.Drawing.Point(163, 14);
            this.radio_p5_green.Name = "radio_p5_green";
            this.radio_p5_green.Size = new System.Drawing.Size(14, 13);
            this.radio_p5_green.TabIndex = 93;
            this.radio_p5_green.TabStop = true;
            this.radio_p5_green.UseVisualStyleBackColor = false;
            this.radio_p5_green.CheckedChanged += new System.EventHandler(this.radio_p5_green_CheckedChanged);
            // 
            // radio_p5_blue
            // 
            this.radio_p5_blue.AutoSize = true;
            this.radio_p5_blue.BackColor = System.Drawing.Color.Blue;
            this.radio_p5_blue.Location = new System.Drawing.Point(149, 14);
            this.radio_p5_blue.Name = "radio_p5_blue";
            this.radio_p5_blue.Size = new System.Drawing.Size(14, 13);
            this.radio_p5_blue.TabIndex = 92;
            this.radio_p5_blue.TabStop = true;
            this.radio_p5_blue.UseVisualStyleBackColor = false;
            this.radio_p5_blue.CheckedChanged += new System.EventHandler(this.radio_p5_blue_CheckedChanged);
            // 
            // radio_p5_red
            // 
            this.radio_p5_red.AutoSize = true;
            this.radio_p5_red.BackColor = System.Drawing.Color.Red;
            this.radio_p5_red.Location = new System.Drawing.Point(135, 14);
            this.radio_p5_red.Name = "radio_p5_red";
            this.radio_p5_red.Size = new System.Drawing.Size(14, 13);
            this.radio_p5_red.TabIndex = 91;
            this.radio_p5_red.TabStop = true;
            this.radio_p5_red.UseVisualStyleBackColor = false;
            this.radio_p5_red.CheckedChanged += new System.EventHandler(this.radio_p5_red_CheckedChanged);
            // 
            // radio_p5_yellow
            // 
            this.radio_p5_yellow.AutoSize = true;
            this.radio_p5_yellow.BackColor = System.Drawing.Color.Yellow;
            this.radio_p5_yellow.Location = new System.Drawing.Point(120, 14);
            this.radio_p5_yellow.Name = "radio_p5_yellow";
            this.radio_p5_yellow.Size = new System.Drawing.Size(14, 13);
            this.radio_p5_yellow.TabIndex = 90;
            this.radio_p5_yellow.TabStop = true;
            this.radio_p5_yellow.UseVisualStyleBackColor = false;
            this.radio_p5_yellow.CheckedChanged += new System.EventHandler(this.radio_p5_yellow_CheckedChanged);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(10, 11);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 89;
            this.textBox5.Text = "Gracz 5";
            // 
            // FormPlayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 282);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPlayers";
            this.ShowIcon = false;
            this.Text = "Nowa gra";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radio_p1_black;
        private System.Windows.Forms.RadioButton radio_p1_green;
        private System.Windows.Forms.RadioButton radio_p1_blue;
        private System.Windows.Forms.RadioButton radio_p1_red;
        private System.Windows.Forms.RadioButton radio_p1_yellow;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radio_p2_black;
        private System.Windows.Forms.RadioButton radio_p2_green;
        private System.Windows.Forms.RadioButton radio_p2_blue;
        private System.Windows.Forms.RadioButton radio_p2_red;
        private System.Windows.Forms.RadioButton radio_p2_yellow;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radio_p3_black;
        private System.Windows.Forms.RadioButton radio_p3_green;
        private System.Windows.Forms.RadioButton radio_p3_blue;
        private System.Windows.Forms.RadioButton radio_p3_red;
        private System.Windows.Forms.RadioButton radio_p3_yellow;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radio_p4_black;
        private System.Windows.Forms.RadioButton radio_p4_green;
        private System.Windows.Forms.RadioButton radio_p4_blue;
        private System.Windows.Forms.RadioButton radio_p4_red;
        private System.Windows.Forms.RadioButton radio_p4_yellow;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radio_p5_black;
        private System.Windows.Forms.RadioButton radio_p5_green;
        private System.Windows.Forms.RadioButton radio_p5_blue;
        private System.Windows.Forms.RadioButton radio_p5_red;
        private System.Windows.Forms.RadioButton radio_p5_yellow;
        private System.Windows.Forms.TextBox textBox5;
    }
}