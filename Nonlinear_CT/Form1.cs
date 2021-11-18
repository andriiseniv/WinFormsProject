using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nonlinear_CT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private double function_in_dot(double x,int id)
        {
            if(id==1)
            {
                return Math.Pow(x, 3) - 2 * Math.Sin(x) - 1;
            }
            else if(id==2)
            {
                return Math.Pow(x, 3) - x - 1;
            }
            else if (id==3)
            {
                return (2 - x) * Math.Pow(Math.E, x) - 0.5;
            }
            return 0;
        }

        private double derivative1_in_dot(double x,int id)
        {       
            if (id == 1)
            {
                return 3 *Math.Pow(x, 2) - 2 * Math.Cos(x);
            }
            else if (id == 2)
            {
                return 3 * Math.Pow(x, 2) - 1;
            }
            else if (id == 3)
            {
                return Math.Pow(Math.E, x) - x * Math.Pow(Math.E, x);
            }
            return 0;
        }

        private double derivative2_in_dot(double x,int id)
        {
            if (id == 1)
            {
                return 6*x + 2 * Math.Sin(x);
            }
            else if (id == 2)
            {
                return 6 * x;
            }
            else if (id == 3)
            {
                return -x * Math.Pow(Math.E, x);
            }
            return 0;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int rad_id = Convert.ToInt32(label20.Text);
            double a1 = Convert.ToDouble(label21.Text);
            double b1 = Convert.ToDouble(label24.Text);
            double a2 = Convert.ToDouble(label25.Text);
            double b2 = Convert.ToDouble(label26.Text);

            method(rad_id, a1,b1,a2,b2);
        }


        private void guna2RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            guna2Button1.Visible = true;
            guna2DataGridView1.Rows.Clear();
            label23.Visible = false;
            label18.Visible = false;
            label19.Visible = false;

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();

            int radio_id = 0; double h2 = 0;
            int n = 0; double n2 = 0;
            double a1 = 0; double b1 = 0;
            double a2 = 0; double b2 = 0;

            double h = Math.Abs((a1 - b1) / n); 

            double x1 = a1; double x2;

            int c = 0;
            if (guna2RadioButton1.Checked == true)
            {
                radio_id = 1;

                n = 450;

                a1 = -1;
                b1 = 2; 

                h = Math.Abs((a1 - b1) / n);

                x1 = a1;

                label20.Text = "1";
            }
            else if (guna2RadioButton2.Checked == true)
            {
                radio_id = 2;

                n = 450;

                a1 = -2;
                b1 = 2;

                h = Math.Abs((a1 - b1) / n);

                x1 = a1;

                label20.Text = "2";
            }
            else if (guna2RadioButton3.Checked == true)
            {
                radio_id = 3;

                n = 450;

                a1 = -5;
                b1 = 2.1;

                h = Math.Abs((a1 - b1) / n);

                x1 = a1;

                label20.Text = "3";
            }

            double okil = 0;
            if (guna2TextBox2.TextLength == 0)
            {
                okil = 1;
            }
            else
            {
                okil = Convert.ToDouble(guna2TextBox2.Text);
            }            
            okil = okil / 2;

            chart1.ChartAreas[0].AxisX.Minimum = a1;
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            guna2DataGridView1.Rows.Add(Math.Round(x1, 4), Math.Round(function_in_dot(x1, radio_id), 4));
            chart1.Series[0].Points.AddXY(x1, function_in_dot(x1, radio_id));

            bool choose;
            for (int i = 0; i < n; i++)
            {
                x1 += h;
                
                guna2DataGridView1.Rows.Add(Math.Round(x1, 4), Math.Round(function_in_dot(x1, radio_id), 4));
                chart1.Series[0].Points.AddXY(x1, function_in_dot(x1, radio_id));

                if (Math.Round(function_in_dot(x1, radio_id), 2) == 0)
                {
                    if (c == 0)
                    {
                        label28.Text = Convert.ToString(Math.Round(x1,4));
                        a1 = Math.Round(x1 - okil, 1);
                        b1 = Math.Round(x1 + okil, 1);

                        chart1.Series[1].Points.AddXY(a1, function_in_dot(a1, radio_id));

                        chart1.Series[1].Points.AddXY(x1, function_in_dot(x1, radio_id));
                        chart1.Series[1].Points.AddXY(b1, function_in_dot(b1, radio_id));

                        n2 = n / 10;
                        h2 = Math.Abs((a1 - b1) / (n / 10));
                        x2 = a1;
                        for (int l = 0; l < n2; l++)
                        {
                            chart1.Series[2].Points.AddXY(x2, function_in_dot(x2, radio_id));
                            x2 += h2;
                        }
                        chart1.Series[2].Points.AddXY(x2, function_in_dot(x2, radio_id));
                    }

                    else if (c == 2)
                    {
                        choose = true;
                        label23.Visible = true;
                        label18.Visible = true;
                        label19.Visible = true;
                        a2 = Math.Round(x1 - okil, 1);
                        b2 = Math.Round(x1 + okil, 1);

                        label28.Text += " ; "+Convert.ToString(Math.Round(x1, 4));

                        chart1.Series[1].Points.AddXY(a2, function_in_dot(a2, radio_id));
                        chart1.Series[1].Points.AddXY(x1, function_in_dot(x1, radio_id));
                        chart1.Series[1].Points.AddXY(b2, function_in_dot(b2, radio_id));
                        n2 = n / 10;
                        h2 = Math.Abs((a2 - b2) / (n / 10));
                        x2 = a2;
                        for (int l = 0; l < n2; l++)
                        {
                            chart1.Series[3].Points.AddXY(x2, function_in_dot(x2, radio_id));
                            x2 += h2;
                        }
                        chart1.Series[3].Points.AddXY(x2, function_in_dot(x2, radio_id));
                    }
                    c++;
                }

            }
            label22.Text = "[" + Convert.ToString(a1) + " ; " + Convert.ToString(b1) + "]";
            label23.Text = "[" + Convert.ToString(a2) + " ; " + Convert.ToString(b2) + "]";

            label21.Text = Convert.ToString(a1);
            label24.Text = Convert.ToString(b1);
            label25.Text = Convert.ToString(a2);
            label26.Text = Convert.ToString(b2);
        }

        private void method(int rad_id,double a1, double b1, double a2, double b2)
        {

            bool ok = true;
            if(label23.Visible==true)
            {
                ok = false;
                if (label27.Text != "1")
                {
                    MessageBox.Show("Виберіть проміжок!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                else
                {
                    ok = true;
                }
            }
            if (guna2ShadowPanel5.Visible == true)
            {

                if (guna2TextBox1.Text.Length == 0 || guna2TextBox3.Text.Length == 0)
                {
                    ok = false;
                    MessageBox.Show("Заповніть всі поля!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ok = true;
                    a1 = Convert.ToDouble(guna2TextBox1.Text);
                    b1 = Convert.ToDouble(guna2TextBox3.Text);
                }
            }

            double eps = 0;
            if (guna2TextBox4.TextLength == 0)
            {
                eps = 0.01;
            }
            else
            {
                eps = Convert.ToDouble(guna2TextBox4.Text);
            }

            if (ok == true)
            {
                label30.Visible = true;
                
                double x1 = 0;
                double x2 = 0;

                int c = 0;

                double dev1_a1 = Math.Round(derivative1_in_dot(a1, rad_id), 4);
                double dev1_b1 = Math.Round(derivative1_in_dot(b1, rad_id), 4);

                double dev2_a1 = Math.Round(derivative2_in_dot(a1, rad_id), 4);
                double dev2_b1 = Math.Round(derivative2_in_dot(b1, rad_id), 4);


                label1.Text = Convert.ToString(dev1_a1);
                label2.Text = Convert.ToString(dev1_b1);

                label3.Text = Convert.ToString(dev2_a1);
                label4.Text = Convert.ToString(dev2_b1);

                if (label23.ForeColor == System.Drawing.Color.Orange)
                {
                    a1 = a2;
                    b1 = b2;
                }

                guna2DataGridView2.Rows.Clear();

                x1 = 0;
                x2 = 0;

                int k = 0;
                x1 = a1 - (a1 - b1) / (function_in_dot(a1, rad_id) - function_in_dot(b1, rad_id)) * function_in_dot(a1, rad_id);
                x2 = b1 - (function_in_dot(b1, rad_id) / derivative1_in_dot(b1, rad_id));

                label5.Text = "f'(x0) f''(x0) > 0";

                if (dev1_a1 * dev2_a1 < 0)
                {
                    double temp = x1;
                    x1 = x2;
                    x2 = temp;
                    label5.Text = "f'(x0) f''(x0) < 0";
                }

                guna2DataGridView2.Rows.Add(k, Math.Round(x1, 4), Math.Round(x2, 4), Math.Round(Math.Abs(x1 - x2), 4));

                while (Math.Abs(x1 - x2) > eps)
                {
                    a1 = x1;
                    b1 = x2;
                    x1 = a1 - (a1 - b1) / (function_in_dot(a1, rad_id) - function_in_dot(b1, rad_id)) * function_in_dot(a1, rad_id);
                    x2 = b1 - (function_in_dot(b1, rad_id) / derivative1_in_dot(b1, rad_id));

                    k++;

                    guna2DataGridView2.Rows.Add(k, Math.Round(x1, 4), Math.Round(x2, 4), Math.Round(Math.Abs(x1 - x2), 4));
                }

                double result = (x1 + x2) / 2;

                label12.Text = Convert.ToString(Math.Round(function_in_dot(result, rad_id), 7));            
                label7.Text = Convert.ToString(k + 1);
                label9.Text = Convert.ToString(Math.Round(result, 4));
                label12.Text = Convert.ToString(Math.Round(function_in_dot(result, rad_id), 5));
            }
        }


        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(guna2CheckBox1.Checked==true)
            {
                guna2ShadowPanel5.Visible = true;
                label22.ForeColor = System.Drawing.Color.White;
                label23.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                guna2ShadowPanel5.Visible = false;
            }
  
        }

        private void label22_Click(object sender, EventArgs e)
        {
            if(label23.Visible==true)
            {
                label22.ForeColor = System.Drawing.Color.DodgerBlue;
                label23.ForeColor = System.Drawing.Color.White;
                label27.Text = "1";
            }

        }

        private void label23_Click(object sender, EventArgs e)
        {
            label22.ForeColor = System.Drawing.Color.White;
            label23.ForeColor = System.Drawing.Color.Orange;
            label27.Text = "1";
        }

        private void guna2TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 45)
            {
                e.Handled = true;
            }
        }

        private void label30_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void guna2TextBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44 && number != 45)
            {
                e.Handled = true;
            }

            guna2CheckBox1.Checked = false;
        }

        private void label38_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }
    }
}
