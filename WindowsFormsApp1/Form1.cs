using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Text.RegularExpressions;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "Ie36MKjoG33KIISQaOIXiJTx1ur1Zt2wsyS9tnBQ",
            BasePath = "https://win-app-cd8e5-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        IFirebaseClient client;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            if (client != null )
            {
                MessageBox.Show("Connected");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        public bool IsOverSeventeen(DateTime date)
        {
            int age = DateTime.Today.Year - date.Year;
            if (date > DateTime.Today.AddYears(-age)) age--;
            return age >= 17;
        }
        public bool check_mail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@gmail.com$";
            return Regex.IsMatch(email, pattern);
        }
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{3}-\d{4}-\d{3}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;
            string formattedDate = selectedDate.ToString("dd/MM/yyyy");

            var data = new Data
            {
                id = textBox1.Text,
                ten = textBox2.Text,
                gioitinh = textBox3.Text,
                diachi = textBox4.Text,
                cmnd = textBox5.Text,
                ngaythangnam  = formattedDate,
                phone = textBox6.Text,
                mail = textBox9.Text
            };

            if (!IsOverSeventeen(dateTimePicker1.Value))
            {
                MessageBox.Show("Người dùng chưa trên 17 tuổi.");
                return;
            }

            if (!check_mail(data.mail))
            {
                MessageBox.Show("Sai định dạng mail");
                return;
            }
            if (!IsValidPhoneNumber(data.phone))
            {
                MessageBox.Show("Sai định dạng SĐT");
                return;
            }
            SetResponse response = await client.SetTaskAsync(data.id+"/", data);
            Data result = response.ResultAs<Data>();
        }
    }
}
