using System;
using System.Windows.Forms;

namespace WindowsFormsApp12
{
    public partial class Form2 : Form
    {
        private Cosmonaut cosmonaut;
        public Form2()
        {
            InitializeComponent();
            cosmonaut = new Cosmonaut();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //вычисление времени полёта в часах
            int totalHours = cosmonaut.CalculateTotalHours();
            textBox5.Text = $"{totalHours} часов.";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //добавление полёта
            if (int.TryParse(textBox4.Text, out int days))
            {
                cosmonaut.AddFlight(days);
                MessageBox.Show($"Полет добавлен. Теперь у космонавта {cosmonaut.FlightCount} полетов.");
            }
            else
            {
                MessageBox.Show("Введите корректное число дней полета.");
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //вычисление среднего количества часов в полете (в часах)
            if (cosmonaut.FlightCount > 0)
            {
                double averageHours = (double)cosmonaut.CalculateTotalHours() / cosmonaut.FlightCount;
                textBox6.Text = $"{averageHours:F2} часов.";
            }
            else
            {
                MessageBox.Show("Космонавт пока не совершал полетов.");
            }
        }
    }
}
