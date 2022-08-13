using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace Lemniscate
{
    public partial class Form1 : Form
    {
        private double leftBorder, rightBorder, step, parameterC;
        private double x, y;
        private void DefaultParams()
        {
            leftBorder = -150;
            rightBorder = 150;
            step = 0.1;
            parameterC = 99;
            textBox_to.Text = rightBorder.ToString();
            textBox_from.Text = leftBorder.ToString();
            textBox_param.Text = parameterC.ToString();
            textBox_step.Text = step.ToString();
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button_clear_Click(sender, e);
        }
        private void buildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button_build_Click(sender, e);
        }
        private void button_build_Click(object sender, EventArgs e)
        {
            Algorithm algorithm = new Algorithm();
            List<FunctionArg> list = new List<FunctionArg>();
            if (CorrectParam())
            {
                double asymptote = parameterC;
                x = leftBorder;
                double c = parameterC;
                this.chart.Series[0].Points.Clear();
                while (x <= rightBorder)
                {
                    y = Math.Sqrt(Math.Sqrt(Math.Pow(c, 4) + 4 * Math.Pow(x, 2) * Math.Pow(c, 2)) - Math.Pow(x, 2) - Math.Pow(c, 2));
                    this.chart.Series[0].Points.AddXY(x, y);
                    x += step;
                }
                x = leftBorder;
                while (x <= rightBorder)
                {
                    y = -Math.Sqrt(Math.Sqrt(Math.Pow(c, 4) + 4 * Math.Pow(x, 2) * Math.Pow(c, 2)) - Math.Pow(x, 2) - Math.Pow(c, 2));
                    this.chart.Series[0].Points.AddXY(x, y);
                    x += step;
                }
                list = algorithm.SaveToList(leftBorder, rightBorder, step, parameterC);
                table.DataSource = list;
            }
        }
        private void button_clear_Click(object sender, EventArgs e)
        {
            List<int> list = new List<int>();
            this.chart.Series[0].Points.Clear();
            textBox_from.Clear();
            textBox_param.Clear();
            textBox_step.Clear();
            textBox_to.Clear();
            table.DataSource = list;
        }
        private bool CorrectParam()
        {
            Algorithm algorithm = new Algorithm();
            double rightFrom = 0.0;
            double rightTo = 0.0;
            double rstep = 0.0;

            if (textBox_from.Text == "" || textBox_to.Text == "" || textBox_step.Text == "" || textBox_param.Text == "")
            {
                if (MessageBox.Show("Use default settings? \n from -150 to 150 with step 0,1 \n param c = 99", "Attention", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DefaultParams();
                    return true;
                }
                else
                {
                    MessageBox.Show("Enter the parameter values!", "Attention");
                    return false;
                }
            }
            else
            {
                leftBorder = Convert.ToDouble(textBox_from.Text);
                rightBorder = Convert.ToDouble(textBox_to.Text);
                step = Convert.ToDouble(textBox_step.Text);
                parameterC = Convert.ToDouble(textBox_param.Text);
                double distance = rightBorder - leftBorder;
                if (leftBorder >= rightBorder)
                {
                    MessageBox.Show("The value of B must be greater than the value of A!", "Attention");
                    return false;
                }
                if (step == 0)
                {
                    MessageBox.Show("The step cannot be zero!", "Attention");
                    return false;
                }
                algorithm.RightBorders(ref rightFrom, ref rightTo, parameterC);
                if ((leftBorder > rightFrom) || (rightBorder < rightTo) || (distance / parameterC > 5))
                {
                    if (MessageBox.Show("In the specified range, the graph may not exist or be displayed incorrectly.\n" +
                        "Would you like to change the range to [" + rightFrom + " .. " + rightTo + "]?", "Attention", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        leftBorder = rightFrom;
                        rightBorder = rightTo;
                        textBox_from.Text = rightFrom.ToString();
                        textBox_to.Text = rightTo.ToString();
                    }
                }
                algorithm.RightStep(ref rstep, rightBorder, leftBorder);
                if (distance / step < 200)
                {
                    if (MessageBox.Show("At this step, the graph may be displayed incorrectly.\n" +
                        "Would you like to change the step to " + rstep + "?", "Attention", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        step = rstep;
                        textBox_step.Text = rstep.ToString();
                    }
                }
            }
            return true;
        }
        private bool RightKey(ref KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                return true;
            }
            if (Char.IsControl(e.KeyChar))
            {
                return true;
            }
            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
                return true;
            }
            if (e.KeyChar == ',')
            {
                return true;
            }
            return false;
        }
        private void textBox_to_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool right = RightKey(ref e);
            if (e.KeyChar == '-')
            {
                return;
            }
            if (right)
            {
                if (e.KeyChar == ',')
                {
                    if (textBox_to.Text == "")
                    {
                        e.Handled = true;
                    }
                    if (textBox_to.Text.IndexOf(',') != -1)
                    {
                        e.Handled = true;
                    }
                    return;
                }
                return;
            }
            e.Handled = true;
        }
        private void textBox_from_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool right = RightKey(ref e);
            if (e.KeyChar == '-')
            {
                return;
            }
            if (right)
            {
                if (e.KeyChar == ',')
                {
                    if (textBox_from.Text == "")
                    {
                        e.Handled = true;
                    }
                    if (textBox_from.Text.IndexOf(',') != -1)
                    {
                        e.Handled = true;
                    }
                    return;
                }
                return;
            }
            e.Handled = true;
        }
        private void saveTheResultToAFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Algorithm algorithm = new Algorithm();

            leftBorder = textBox_from.Text == "" ? 0 : Convert.ToDouble(textBox_from.Text);
            rightBorder = textBox_to.Text == "" ? 0 : Convert.ToDouble(textBox_to.Text);
            step = textBox_step.Text == "" ? 0 : Convert.ToDouble(textBox_step.Text);
            parameterC = textBox_param.Text == "" ? 0 : Convert.ToDouble(textBox_param.Text);

            if (leftBorder == 0 || rightBorder == 0 || step == 0)
            {
                MessageBox.Show("Check borders and step", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<FunctionArg> list = algorithm.SaveToList(leftBorder, rightBorder, step, parameterC);

                FileWork.SaveResults(rightBorder.ToString(), leftBorder.ToString(), step.ToString(), parameterC.ToString(), list);
            }
        }
        private void saveTheInputToAFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leftBorder = textBox_from.Text == "" ? 0 : Convert.ToDouble(textBox_from.Text);
            rightBorder = textBox_to.Text == "" ? 0 : Convert.ToDouble(textBox_to.Text);
            step = textBox_step.Text == "" ? 0 : Convert.ToDouble(textBox_step.Text);
            parameterC = textBox_param.Text == "" ? 0 : Convert.ToDouble(textBox_param.Text);

            if (leftBorder == 0 || rightBorder == 0 || step == 0)
            {
                MessageBox.Show("Check borders and step", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                FileWork.SaveInput(leftBorder.ToString(), rightBorder.ToString(), step.ToString(), parameterC.ToString());
            }
        }
        bool GetDouble(string text, out double num)
        {
            return (double.TryParse(text, out num));
        }
        bool CheckData(string strLeftBorder, string strRightBorder, string strStep, string strA)
        {
            bool flag = true;
            if (!GetDouble(strA, out parameterC))
            {
                flag = false;
            }

            if (!GetDouble(strLeftBorder, out leftBorder))
            {
                flag = false;
            }
            if (!GetDouble(strRightBorder, out rightBorder))
            {
                flag = false;
            }
            if (!GetDouble(strStep, out step))
            {
                flag = false;
            }
            if ((rightBorder - leftBorder) - step <= 0)
            {
                flag = false;
            }
            if (leftBorder >= rightBorder)
            {
                flag = false;
            }
            if (parameterC == 0)
            {
                flag = false;
            }
            return flag;
        }
        private void EnterData(object sender, EventArgs e)
        {
            OpenFileDialog fileTable = new OpenFileDialog();
            fileTable.Filter = "Text files(*.txt)|*.txt";
            fileTable.ShowDialog();
            string filename = fileTable.FileName;
            try
            {
                string[] readText = System.IO.File.ReadAllLines(filename);
                if (readText.Length >= 4)
                {
                    if (CheckData(readText[0], readText[1], readText[2], readText[3]))
                    {
                        textBox_from.Text = readText[0];
                        textBox_to.Text = readText[1];
                        textBox_step.Text = readText[2];
                        textBox_param.Text = readText[3];
                    }
                    else
                    {
                        textBox_from.Text = "";
                        textBox_to.Text = "";
                        textBox_step.Text = "";
                        textBox_param.Text = "";
                        MessageBox.Show("Invalid data format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Invalid data format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("File was not selected, data was not read", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void textBox_step_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool right = RightKey(ref e);
            if (right)
            {
                if (e.KeyChar == ',')
                {
                    if (textBox_step.Text == "")
                    {
                        e.Handled = true;
                    }
                    if (textBox_step.Text.IndexOf(',') != -1)
                    {
                        e.Handled = true;
                    }
                    return;
                }
                return;
            }
            e.Handled = true;
        }
        private void textBox_param_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool right = RightKey(ref e);
            if (e.KeyChar == '-')
            {
                return;
            }
            if (right)
            {
                if (e.KeyChar == ',')
                {
                    if (textBox_param.Text == "")
                    {
                        e.Handled = true;
                    }
                    if (textBox_param.Text.IndexOf(',') != -1)
                    {
                        e.Handled = true;
                    }
                    return;
                }
                return;
            }
            e.Handled = true;
        }
        private void Form1_Load(object sender, EventArgs e) { }
        public Form1()
        {
            InitializeComponent();
        }

        private void ShowHello(object sender, EventArgs e)
        {
            try
            {
                if (bool.Parse(ConfigurationManager.AppSettings["showHello"]))
                {
                    About about = new About();
                    about.ShowDialog();
                }
            }
            catch
            {
                About about = new About();
                about.ShowDialog();
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowGreetingForm();
        }
        private void showAboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            bool show = !bool.Parse(ConfigurationManager.AppSettings["showHello"]);
            config.AppSettings.Settings["showHello"].Value = (show).ToString();
            config.Save();
            showAboutToolStripMenuItem1.Checked = show;
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void ShowGreetingForm()
        {
            new About().ShowDialog();
        }
    }
}