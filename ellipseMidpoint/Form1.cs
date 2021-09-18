using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ellipseMidpoint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

  

        private void button1_Click(object sender, EventArgs e)
        {

            int xCenter = Convert.ToInt32(textBox1.Text);
            int yCenter = Convert.ToInt32(textBox2.Text);

            int XRadius = Convert.ToInt32(textBox4.Text);
            int YRadius = Convert.ToInt32(textBox3.Text);

           
            Bitmap Bit = new Bitmap(this.Width, this.Height);
            if (xCenter >= 0 & yCenter >= 0 )
            {
                ellipseMidpoint(xCenter, yCenter, XRadius, YRadius, Bit);

            }
            else
            {
                string message = "please enter positive numbers";
                string title = "Error";
                MessageBox.Show(message, title);
            }
        }

        private void ellipseMidpoint(int xCenter, int yCenter, int Rx, int Ry, Bitmap bit)
        {
            int Rx2 = Rx * Rx;
            int Ry2 = Ry * Ry;
            int twoRx2 = 2 * Rx2;
            int twoRy2 = 2 * Ry2;
            int p;
            int x = 0;
            int y = Ry;
            int px = 0;
            int py = twoRx2 * y;
            ellipsePlotPoints(xCenter, yCenter, x, y, bit);
            /* Region 1 */
            p = round(Ry2 - (Rx2 * Ry) + (0.25 * Rx2));
            while (px < py)
            {
                x++;
                px += twoRy2;
                if (p < 0)
                    p += Ry2 + px;
                else
                {
                    y--;
                    py -= twoRx2;
                    p += Ry2 + px - py;
                }
                ellipsePlotPoints(xCenter, yCenter, x, y,bit);
            }
            /* Region 2 */
            p = round(Ry2 * (x + 0.5) * (x + 0.5) + Rx2 * (y - 1) * (y - 1) - Rx2 * Ry2);
            while (y > 0)
            {
                y--;
                py -= twoRx2;
                if (p > 0)
                    p += Rx2 - py;
                else
                {
                    x++;
                    px += twoRy2;
                    p += Rx2 - py + px;
                }
                ellipsePlotPoints(xCenter, yCenter, x, y, bit);
            }

        }

        private int round(double a)
        {
            return (int) (a + 0.5);
        }

        private void ellipsePlotPoints(int xCenter, int yCenter, int x, int y, Bitmap bit)
        {
            bit.SetPixel(xCenter + x, yCenter + y, Color.White);
            bit.SetPixel(xCenter - x, yCenter + y, Color.White);
            bit.SetPixel(xCenter + x, yCenter - y, Color.White);
            bit.SetPixel(xCenter - x, yCenter - y, Color.White);
            pictureBox1.Image = bit;
        }
        private void intVal(KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            intVal(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            intVal(e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            intVal(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            intVal(e);
        }
    }
    
}
