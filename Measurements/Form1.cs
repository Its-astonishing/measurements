using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Measurements
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.inputMeasurementsData.RowCount = 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        void displayErrorWindows(String errorTitle, String errorMessage)
        {
            MessageBox.Show(errorMessage, errorTitle,
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void storeTableToData(int index)
        {

                //List<double> items = new List<double>();
                //foreach (DataGridViewRow row in inputMeasurementsData.Rows)
                //{
                //    // items.Add(row.Cells[0]);
                //    foreach (DataGridViewCell dc in row.Cells)
                //    {
                //        double value;
                //        if (double.TryParse((string)dc.Value, out value))
                //        {
                //            items.Add(value);
                //        } else
                //        {
                //            string message = "Некорректное число в строке " + (dc.RowIndex + 1);
                //            displayErrorWindows("Ошибка", message);
                //        }

                //    }
                //}

                //measurements[index - 1] = items;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void calculate_everything()
        {
            double delta;
            if (double.TryParse(textBox1.Text, out delta))
            {
                delta_test = delta;
            }
            else
            {
                displayErrorWindows("Ошибка", "Недопустимая погрешность измерений");
            }

            List<double> items = new List<double>();
            foreach (DataGridViewRow dr in inputMeasurementsData.Rows)
            {
                double item = new double();
                foreach (DataGridViewCell dc in dr.Cells)
                {
                    if (dc.Value != null)
                    {
                        if (double.TryParse((string)dc.Value, out item))
                        {
                            // item = item;
                        }
                        else
                        {
                            displayErrorWindows("Ошибка", "Недопустимое значение в таблице в строчке " + dr.Index);
                            item = 0;
                        }
                        items.Add(item);
                    }
                }

            }

            data = items;

            Calculator calc = new Calculator();

            update_table(calc.get_neopredelennost(data, delta_test), calc.get_value(data, delta_test));
            double value = calc.get_value(data, delta_test);
            calc.round_if_required(ref value, trackBar1.Value);
            string first = value.ToString();

            double value2 = calc.get_neopredelennost(data, delta_test).summarn_neopr * 1.65;
            calc.round_if_required(ref value2, trackBar1.Value);
            string second = (value2).ToString();
            resultBox.Text = "(" + first + " ± " + second + ")";
            is_calculated = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            calculate_everything();
        }

        private void update_table(Calculator.neopredelennost neopr_value, double value)
        {
            Calculator calc = new Calculator();
            calc.round_if_required(ref neopr_value.neopr_a_value, trackBar1.Value);
            calc.round_if_required(ref neopr_value.neopr_a_percent, trackBar1.Value);
            calc.round_if_required(ref neopr_value.neopr_b_value, trackBar1.Value);
            calc.round_if_required(ref neopr_value.summarn_neopr, trackBar1.Value);
            neopr_value.neopr_b_percent = 100 - neopr_value.neopr_a_percent;
            textBox7.Text = neopr_value.neopr_a_value.ToString();
            textBox10.Text = neopr_value.neopr_a_percent.ToString();
            textBox8.Text = neopr_value.neopr_b_value.ToString();
            textBox11.Text = neopr_value.neopr_b_percent.ToString();
            // label12.Text = neopr_value.summarn_neopr.ToString();

            textBox9.Text = neopr_value.summarn_neopr.ToString();
        }

        private double delta_test;
        private List<double> data;

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (is_calculated)
            {
                calculate_everything();
            }
        }

        bool is_calculated = false;
    }
}
