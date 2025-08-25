using System;
using System.Windows.Forms;
using P_TB2.Controllers;

namespace P_TB2
{
    public partial class Form1 : Form
    {
        private QuadraticController controller;

        public Form1()
        {
            InitializeComponent();
            controller = new QuadraticController();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!controller.ValidateInput(textBox1.Text) || 
                    !controller.ValidateInput(textBox2.Text) || 
                    !controller.ValidateInput(textBox3.Text))
                {
                    MessageBox.Show("Vui lòng nhập đúng định dạng số cho các hệ số!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double a = double.Parse(textBox1.Text);
                double b = double.Parse(textBox2.Text);
                double c = double.Parse(textBox3.Text);

                string result = controller.SolveEquation(a, b, c);
                
                textBox4.Text = $"Phương trình: {a}x² + {b}x + {c} = 0\r\n\r\n{result}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox1.Focus();
        }
    }
}
