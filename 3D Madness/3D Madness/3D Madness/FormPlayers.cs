using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _3D_Madness
{
    public partial class FormPlayers : Form
    {
        public bool formClose = false;
        public int numberOfPlayers = 2;
        public int[] colorsOfPlayers = new int[5];
        public string[] namesOfPlayers = new string[5];

        public FormPlayers()
        {
            InitializeComponent();

            groupBox1.Controls.AddRange(new System.Windows.Forms.Control[]  {radio_p1_yellow, radio_p1_red, radio_p1_green, radio_p1_blue, radio_p1_black });
            groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] { radio_p2_yellow, radio_p2_red, radio_p2_green, radio_p2_blue, radio_p2_black });
            groupBox3.Controls.AddRange(new System.Windows.Forms.Control[] { radio_p3_yellow, radio_p3_red, radio_p3_green, radio_p3_blue, radio_p3_black });
            groupBox4.Controls.AddRange(new System.Windows.Forms.Control[] { radio_p4_yellow, radio_p4_red, radio_p4_green, radio_p4_blue, radio_p4_black });
            groupBox5.Controls.AddRange(new System.Windows.Forms.Control[] { radio_p5_yellow, radio_p5_red, radio_p5_green, radio_p5_blue, radio_p5_black });

            groupBox3.Visible = false;
            groupBox4.Visible = false;
            groupBox5.Visible = false;


        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numberOfPlayers = (int)numericUpDown1.Value;

            switch (numberOfPlayers)
            {
                case 2:
                    groupBox1.Visible = true;
                    groupBox2.Visible = true;
                    
                    groupBox3.Visible = false;
                    radio_p3_black.Checked = false;
                    radio_p3_green.Checked = false;
                    radio_p3_blue.Checked = false;
                    radio_p3_red.Checked = false;
                    radio_p3_yellow.Checked = false;

                    groupBox4.Visible = false;
                    radio_p4_black.Checked = false;
                    radio_p4_green.Checked = false;
                    radio_p4_blue.Checked = false;
                    radio_p4_red.Checked = false;
                    radio_p4_yellow.Checked = false;

                    groupBox5.Visible = false;
                    radio_p5_black.Checked = false;
                    radio_p5_green.Checked = false;
                    radio_p5_blue.Checked = false;
                    radio_p5_red.Checked = false;
                    radio_p5_yellow.Checked = false;
                    
                    colorsOfPlayers[2] = 0;
                    colorsOfPlayers[3] = 0;
                    colorsOfPlayers[4] = 0;
                    break;
                case 3:
                    groupBox1.Visible = true;
                    groupBox2.Visible = true;
                    groupBox3.Visible = true;
                   
                    groupBox4.Visible = false;
                    radio_p4_black.Checked = false;
                    radio_p4_green.Checked = false;
                    radio_p4_blue.Checked = false;
                    radio_p4_red.Checked = false;
                    radio_p4_yellow.Checked = false;

                    groupBox5.Visible = false;
                    radio_p5_black.Checked = false;
                    radio_p5_green.Checked = false;
                    radio_p5_blue.Checked = false;
                    radio_p5_red.Checked = false;
                    radio_p5_yellow.Checked = false;
                    
                    colorsOfPlayers[3] = 0;
                    colorsOfPlayers[4] = 0;
                    break;
                case 4:
                    groupBox1.Visible = true;
                    groupBox2.Visible = true;
                    groupBox3.Visible = true;
                    groupBox4.Visible = true;
                    
                    groupBox5.Visible = false;
                    radio_p5_black.Checked = false;
                    radio_p5_green.Checked = false;
                    radio_p5_blue.Checked = false;
                    radio_p5_red.Checked = false;
                    radio_p5_yellow.Checked = false;

                    colorsOfPlayers[4] = 0;
                    break;
                case 5:
                    groupBox1.Visible = true;
                    groupBox2.Visible = true;
                    groupBox3.Visible = true;
                    groupBox4.Visible = true;
                    groupBox5.Visible = true;
                    break;
            }
        }


        #region 1. player RadioButtons
        private void radio_p1_yellow_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p1_yellow.Checked == true)
            {
                colorsOfPlayers[0] = 1;
                radio_p2_yellow.Enabled = false;
                radio_p3_yellow.Enabled = false;
                radio_p4_yellow.Enabled = false;
                radio_p5_yellow.Enabled = false;
            }
            else 
            {
                radio_p2_yellow.Enabled = true;
                radio_p3_yellow.Enabled = true;
                radio_p4_yellow.Enabled = true;
                radio_p5_yellow.Enabled = true;
            }
        }

        private void radio_p1_red_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p1_red.Checked == true)
            {
                colorsOfPlayers[0] = 2;
                radio_p2_red.Enabled = false;
                radio_p3_red.Enabled = false;
                radio_p4_red.Enabled = false;
                radio_p5_red.Enabled = false;
            }
            else
            {
                radio_p2_red.Enabled = true;
                radio_p3_red.Enabled = true;
                radio_p4_red.Enabled = true;
                radio_p5_red.Enabled = true;
            }
        }

        private void radio_p1_blue_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p1_blue.Checked == true)
            {
                colorsOfPlayers[0] = 3;
                radio_p2_blue.Enabled = false;
                radio_p3_blue.Enabled = false;
                radio_p4_blue.Enabled = false;
                radio_p5_blue.Enabled = false;
            }
            else
            {
                radio_p2_blue.Enabled = true;
                radio_p3_blue.Enabled = true;
                radio_p4_blue.Enabled = true;
                radio_p5_blue.Enabled = true;
            }
        }

        private void radio_p1_green_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p1_green.Checked == true)
            {
                colorsOfPlayers[0] = 4;
                radio_p2_green.Enabled = false;
                radio_p3_green.Enabled = false;
                radio_p4_green.Enabled = false;
                radio_p5_green.Enabled = false;
            }
            else
            {
                radio_p2_green.Enabled = true;
                radio_p3_green.Enabled = true;
                radio_p4_green.Enabled = true;
                radio_p5_green.Enabled = true;
            }
        }

        private void radio_p1_black_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p1_black.Checked == true)
            {
                colorsOfPlayers[0] = 5;
                radio_p2_black.Enabled = false;
                radio_p3_black.Enabled = false;
                radio_p4_black.Enabled = false;
                radio_p5_black.Enabled = false;
            }
            else
            {
                radio_p2_black.Enabled = true;
                radio_p3_black.Enabled = true;
                radio_p4_black.Enabled = true;
                radio_p5_black.Enabled = true;
            }
        }

        #endregion

        #region 2. player RadioButtons
        private void radio_p2_yellow_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p2_yellow.Checked == true)
            {
                colorsOfPlayers[1] = 1;
                radio_p1_yellow.Enabled = false;
                radio_p3_yellow.Enabled = false;
                radio_p4_yellow.Enabled = false;
                radio_p5_yellow.Enabled = false;
            }
            else
            {
                radio_p1_yellow.Enabled = true;
                radio_p3_yellow.Enabled = true;
                radio_p4_yellow.Enabled = true;
                radio_p5_yellow.Enabled = true;
            }
        }

        private void radio_p2_red_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p2_red.Checked == true)
            {
                colorsOfPlayers[1] = 2;
                radio_p1_red.Enabled = false;
                radio_p3_red.Enabled = false;
                radio_p4_red.Enabled = false;
                radio_p5_red.Enabled = false;
            }
            else
            {
                radio_p1_red.Enabled = true;
                radio_p3_red.Enabled = true;
                radio_p4_red.Enabled = true;
                radio_p5_red.Enabled = true;
            }
        }

        private void radio_p2_blue_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p2_blue.Checked == true)
            {
                colorsOfPlayers[1] = 3;
                radio_p1_blue.Enabled = false;
                radio_p3_blue.Enabled = false;
                radio_p4_blue.Enabled = false;
                radio_p5_blue.Enabled = false;
            }
            else
            {
                radio_p1_blue.Enabled = true;
                radio_p3_blue.Enabled = true;
                radio_p4_blue.Enabled = true;
                radio_p5_blue.Enabled = true;
            }
        }

        private void radio_p2_green_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p2_green.Checked == true)
            {
                colorsOfPlayers[1] = 4;
                radio_p1_green.Enabled = false;
                radio_p3_green.Enabled = false;
                radio_p4_green.Enabled = false;
                radio_p5_green.Enabled = false;
            }
            else
            {
                radio_p1_green.Enabled = true;
                radio_p3_green.Enabled = true;
                radio_p4_green.Enabled = true;
                radio_p5_green.Enabled = true;
            }
        }

        private void radio_p2_black_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p2_black.Checked == true)
            {
                colorsOfPlayers[1] = 5;
                radio_p1_black.Enabled = false;
                radio_p3_black.Enabled = false;
                radio_p4_black.Enabled = false;
                radio_p5_black.Enabled = false;
            }
            else
            {
                radio_p1_black.Enabled = true;
                radio_p3_black.Enabled = true;
                radio_p4_black.Enabled = true;
                radio_p5_black.Enabled = true;
            }

        }
        #endregion

        #region 3. player RadioButtons
        private void radio_p3_yellow_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p3_yellow.Checked == true)
            {
                colorsOfPlayers[2] = 1;
                radio_p1_yellow.Enabled = false;
                radio_p2_yellow.Enabled = false;
                radio_p4_yellow.Enabled = false;
                radio_p5_yellow.Enabled = false;
            }
            else
            {
                radio_p1_yellow.Enabled = true;
                radio_p2_yellow.Enabled = true;
                radio_p4_yellow.Enabled = true;
                radio_p5_yellow.Enabled = true;
            }
        }

        private void radio_p3_red_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p3_red.Checked == true)
            {
                colorsOfPlayers[2] = 2;
                radio_p1_red.Enabled = false;
                radio_p2_red.Enabled = false;
                radio_p4_red.Enabled = false;
                radio_p5_red.Enabled = false;
            }
            else
            {
                radio_p1_red.Enabled = true;
                radio_p2_red.Enabled = true;
                radio_p4_red.Enabled = true;
                radio_p5_red.Enabled = true;
            }
        }

        private void radio_p3_blue_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p3_blue.Checked == true)
            {
                colorsOfPlayers[2] = 3;
                radio_p1_blue.Enabled = false;
                radio_p2_blue.Enabled = false;
                radio_p4_blue.Enabled = false;
                radio_p5_blue.Enabled = false;
            }
            else
            {
                radio_p1_blue.Enabled = true;
                radio_p2_blue.Enabled = true;
                radio_p4_blue.Enabled = true;
                radio_p5_blue.Enabled = true;
            }
        }

        private void radio_p3_green_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p3_green.Checked == true)
            {
                colorsOfPlayers[2] = 4;
                radio_p1_green.Enabled = false;
                radio_p2_green.Enabled = false;
                radio_p4_green.Enabled = false;
                radio_p5_green.Enabled = false;
            }
            else
            {
                radio_p1_green.Enabled = true;
                radio_p2_green.Enabled = true;
                radio_p4_green.Enabled = true;
                radio_p5_green.Enabled = true;
            }
        }

        private void radio_p3_black_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p3_black.Checked == true)
            {
                colorsOfPlayers[2] = 5;
                radio_p1_black.Enabled = false;
                radio_p2_black.Enabled = false;
                radio_p4_black.Enabled = false;
                radio_p5_black.Enabled = false;
            }
            else
            {
                radio_p1_black.Enabled = true;
                radio_p2_black.Enabled = true;
                radio_p4_black.Enabled = true;
                radio_p5_black.Enabled = true;
            }

        }
        #endregion

        #region 4. player RadioButtons
        private void radio_p4_yellow_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p4_yellow.Checked == true)
            {
                colorsOfPlayers[3] = 1;
                radio_p1_yellow.Enabled = false;
                radio_p2_yellow.Enabled = false;
                radio_p3_yellow.Enabled = false;
                radio_p5_yellow.Enabled = false;
            }
            else
            {
                radio_p1_yellow.Enabled = true;
                radio_p2_yellow.Enabled = true;
                radio_p3_yellow.Enabled = true;
                radio_p5_yellow.Enabled = true;
            }
        }

        private void radio_p4_red_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p4_red.Checked == true)
            {
                colorsOfPlayers[3] = 2;
                radio_p1_red.Enabled = false;
                radio_p2_red.Enabled = false;
                radio_p3_red.Enabled = false;
                radio_p5_red.Enabled = false;
            }
            else
            {
                radio_p1_red.Enabled = true;
                radio_p2_red.Enabled = true;
                radio_p3_red.Enabled = true;
                radio_p5_red.Enabled = true;
            }
        }

        private void radio_p4_blue_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p4_blue.Checked == true)
            {
                colorsOfPlayers[3] = 3;
                radio_p1_blue.Enabled = false;
                radio_p2_blue.Enabled = false;
                radio_p3_blue.Enabled = false;
                radio_p5_blue.Enabled = false;
            }
            else
            {
                radio_p1_blue.Enabled = true;
                radio_p2_blue.Enabled = true;
                radio_p3_blue.Enabled = true;
                radio_p5_blue.Enabled = true;
            }
        }

        private void radio_p4_green_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p4_green.Checked == true)
            {
                colorsOfPlayers[3] = 4;
                radio_p1_green.Enabled = false;
                radio_p2_green.Enabled = false;
                radio_p3_green.Enabled = false;
                radio_p5_green.Enabled = false;
            }
            else
            {
                radio_p1_green.Enabled = true;
                radio_p2_green.Enabled = true;
                radio_p3_green.Enabled = true;
                radio_p5_green.Enabled = true;
            }
        }

        private void radio_p4_black_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p4_black.Checked == true)
            {
                colorsOfPlayers[3] = 5;
                radio_p1_black.Enabled = false;
                radio_p2_black.Enabled = false;
                radio_p3_black.Enabled = false;
                radio_p5_black.Enabled = false;
            }
            else
            {
                radio_p1_black.Enabled = true;
                radio_p2_black.Enabled = true;
                radio_p3_black.Enabled = true;
                radio_p5_black.Enabled = true;
            }

        }
        #endregion

        #region 5. player RadioButtons
        private void radio_p5_yellow_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p5_yellow.Checked == true)
            {
                colorsOfPlayers[4] = 1;
                radio_p1_yellow.Enabled = false;
                radio_p2_yellow.Enabled = false;
                radio_p4_yellow.Enabled = false;
                radio_p3_yellow.Enabled = false;
            }
            else
            {
                radio_p1_yellow.Enabled = true;
                radio_p2_yellow.Enabled = true;
                radio_p4_yellow.Enabled = true;
                radio_p3_yellow.Enabled = true;
            }
        }

        private void radio_p5_red_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p5_red.Checked == true)
            {
                colorsOfPlayers[4] = 2;
                radio_p1_red.Enabled = false;
                radio_p2_red.Enabled = false;
                radio_p4_red.Enabled = false;
                radio_p3_red.Enabled = false;
            }
            else
            {
                radio_p1_red.Enabled = true;
                radio_p2_red.Enabled = true;
                radio_p4_red.Enabled = true;
                radio_p3_red.Enabled = true;
            }
        }

        private void radio_p5_blue_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p5_blue.Checked == true)
            {
                colorsOfPlayers[4] = 3;
                radio_p1_blue.Enabled = false;
                radio_p2_blue.Enabled = false;
                radio_p4_blue.Enabled = false;
                radio_p3_blue.Enabled = false;
            }
            else
            {
                radio_p1_blue.Enabled = true;
                radio_p2_blue.Enabled = true;
                radio_p4_blue.Enabled = true;
                radio_p3_blue.Enabled = true;
            }
        }

        private void radio_p5_green_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p5_green.Checked == true)
            {
                colorsOfPlayers[4] = 4;
                radio_p1_green.Enabled = false;
                radio_p2_green.Enabled = false;
                radio_p4_green.Enabled = false;
                radio_p3_green.Enabled = false;
            }
            else
            {
                radio_p1_green.Enabled = true;
                radio_p2_green.Enabled = true;
                radio_p4_green.Enabled = true;
                radio_p3_green.Enabled = true;
            }
        }

        private void radio_p5_black_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_p5_black.Checked == true)
            {
                colorsOfPlayers[4] = 5;
                radio_p1_black.Enabled = false;
                radio_p2_black.Enabled = false;
                radio_p4_black.Enabled = false;
                radio_p3_black.Enabled = false;
            }
            else
            {
                radio_p1_black.Enabled = true;
                radio_p2_black.Enabled = true;
                radio_p4_black.Enabled = true;
                radio_p3_black.Enabled = true;
            }

        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

            bool ok = false;

            namesOfPlayers[0] = textBox1.Text;
            namesOfPlayers[1] = textBox2.Text;
            namesOfPlayers[2] = textBox3.Text;
            namesOfPlayers[3] = textBox4.Text;
            namesOfPlayers[4] = textBox5.Text;

            for (int i = 0; i < numberOfPlayers; i++)
            {
                if (colorsOfPlayers[i] != 0)
                {
                    ok = true;
                }
                else 
                {
                    ok = false;
                    break;
                }
            }

            if (ok == true)
            {
                formClose = true;
            }
            else 
            {
                MessageBox.Show("Wybierz kolory dla wszystkich graczy");
            }
        }
    }
}
