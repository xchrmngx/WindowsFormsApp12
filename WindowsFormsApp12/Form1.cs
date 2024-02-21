using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp12
{
    public partial class Form1 : Form
    {
        //путь к файлу куда должны записываться данные из формы
        private string userDataFilePath = "C:\\Users\\nvidi\\OneDrive\\Рабочий стол\\WindowsFormsApp12\\userData.txt";
        private bool passwordVisible = false;
        private int loginAttempts = 0;
        public String pwd;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !passwordVisible;//скрыть изначально вводимый текст в textBox2
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //скрытие пароля
            passwordVisible = !passwordVisible;
            textBox2.UseSystemPasswordChar = !passwordVisible;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;//логин
            string password = textBox2.Text;//пароль

            // Проверка пароля на сложность
            if (!IsPasswordStrong(password))
            {    
                if (loginAttempts >= 2)
                {
                    // При третьей неудачной попытке входа, требовать капчу
                    MessageBox.Show("Введите капчу!");
                    label9.Visible = true;
                    textBox9.Visible = true;
                    textBox10.Visible = true;
                    button6.Visible = true;
                    // создаем наборы символов, из которых будет формироваться капча
                    String allowchar = " ";
                    allowchar = "A, B, C,D, E, F, G, H, I, J, K, L, M, N, 0, P, Q, R, S, T,U, V, W,X, Y, Z";
                    allowchar += "a,b,c,d,e, f,g, h,i, j,k,1,m,n, o,p,q, r, s, t ,u, v,w,y, z";
                    allowchar += "1,2,3,4,5,6,7,8,9,0";
                    char[] a = { ',' };

                    // записываем набор символов в массив
                    String[] ar = allowchar.Split(a);
                    pwd = "";
                    //переменная в которой будет хранится значение капчи
                    string temp = ""; //переменная, в которую будет записываться рандомный
                    Random r = new Random();
                    int kol = 6; // количество символов в капче
                    for (int i = 0; i < kol; i++)
                    {
                        temp = ar[(r.Next(0, ar.Length))];
                        pwd += temp;
                    }
                    textBox10.Text = pwd;
                }
                else
                {
                    MessageBox.Show("Пароль не удовлетворяет требованиям сложности!");
                    loginAttempts++;
                }
            }
            else
            {
                // Пароль удовлетворяет требованиям, выполнить вход
                if (IsUserValid(username, password))
                {
                    // Открываем следующую форму или выполняем нужные действия при успешной авторизации
                    MessageBox.Show("Успешная авторизация!");
                    Form2 form2 = new Form2();
                    form2.Show();
                    this.Hide();
                    // Здесь можно перейти на следующую форму
                }
                else
                {
                    MessageBox.Show("Неверное имя пользователя или пароль!");
                    loginAttempts++;
                }
            }
            }
        //метод проверки введённого пароля на сложность
        private bool IsPasswordStrong(string password)
        {
            // Проверка пароля на сложность: минимум 8 символов и содержание хотя бы одной буквы и одного символа
            return password.Length >= 8 && Regex.IsMatch(password, @"[a-zA-Z]") && Regex.IsMatch(password, @"[^a-zA-Z\d\s]");
        }

        /* метод сверки данных,
         которые ввёл пользователь во вкладке "Авторизация", с теми, которые он до этого ввёл во вкладке "Регистрация"*/
        private bool IsUserValid(string username, string password)
        {
            string[] userDataLines = File.ReadAllLines(userDataFilePath);
            foreach (string userData in userDataLines)
            {
                string[] userDataParts = userData.Split(',');
                string savedUsername = userDataParts[3].Trim();
                string savedPassword = userDataParts[4].Trim();

                if (username == savedUsername && password == savedPassword)
                {
                    return true; // Найден совпадающий пользователь
                }
            }
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;// переход на вкладку "Авторизация"
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //|блок ввода данных пользователем
            string Name = textBox3.Text;
            string Surname = textBox4.Text;
            string patronymic = textBox5.Text;
            string UserName = textBox6.Text;
            string newPassword = textBox7.Text;
            string passwordTest = textBox8.Text;
            //блок ввода данных пользователем|

            string userData = $"{Name}, {Surname}, {patronymic},{UserName},{newPassword}";
            File.AppendAllText(userDataFilePath, userData + Environment.NewLine);// запись данных в текстовый документ

            // |Проверка пароля на сложность при регистрации
            if (!IsPasswordStrong(newPassword))
            {
                MessageBox.Show("Пароль не удовлетворяет требованиям сложности!");
            }
            // Проверка пароля на сложность при регистрации|
          
            //предупреждение если пароли не совпадают
            else if (newPassword != passwordTest)
            {
                MessageBox.Show("Пароли не совпадают");
            }
            //если пользователь ввёл пароль и подтвердил его, то переход к вкладке авторизация 
            else if (newPassword == passwordTest)
            {
                MessageBox.Show("Регистрация успешно завершена!");
                tabControl1.SelectedTab = tabPage1;
            }
            else
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //|очистка после ввода
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            //очистка после ввода|
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox9.Text != pwd)
            {
                MessageBox.Show("Вы неправильно ввели капчу");// предупреждение если пользователь неправильно ввёл капчу
            }
            else
            {
                Form2 form2 = new Form2();// перех к другой форме
                form2.Show();
                this.Hide();
            }
        }
        //не работает // запрет копирования
        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
          //  if (e.Control && (e.KeyCode == Keys.C || e.KeyCode == Keys.V))
            //{
              //  e.Handled = true;
            //}
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Запретить ввод чисел
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Запретить ввод чисел
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Запретить ввод чисел
            }
        }
    }
}

    

