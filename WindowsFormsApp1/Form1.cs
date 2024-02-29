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
using Newtonsoft.Json;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();

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
                MessageBox.Show("Đã kết nối với Data Base");
            }

            dt.Columns.Add("ID");
            dt.Columns.Add("Ho va ten");
            dt.Columns.Add("Gioi tinh");
            dt.Columns.Add("Dia chi");
            dt.Columns.Add("CMND");
            dt.Columns.Add("Ngay thang nam");
            dt.Columns.Add("Phone");
            dt.Columns.Add("Mail"); 
            
            dataGridView1.DataSource = dt;
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
                ngaythangnam = formattedDate,
                phone = textBox6.Text,
                mail = textBox9.Text
            };
            if (data.id == "" || data.ten == "" || data.gioitinh == "" || data.diachi == "" || data.cmnd == "" || data.ngaythangnam == "" || data.phone == "" || data.mail == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin");
                return;
            }

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
            FirebaseResponse response = await client.GetTaskAsync("data/"+data.id);
            if (response.Body != "null")
            {
                MessageBox.Show("ID hiện có trên DB");
                return;
            }
            response = await client.SetTaskAsync("data/"+data.id+"/", data);
            Data result = response.ResultAs<Data>();
        }

        private async void button2_Click(object sender, EventArgs e)
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
                ngaythangnam = formattedDate,
                phone = textBox6.Text,
                mail = textBox9.Text
            };
            if (data.id == "" || data.ten == "" || data.gioitinh == "" || data.diachi == "" || data.cmnd == "" || data.ngaythangnam == "" || data.phone == "" || data.mail == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin");
                return;
            }

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
            FirebaseResponse response = await client.GetTaskAsync("data/"+data.id);
            if (response.Body == "null")
            {
                MessageBox.Show("ID không tồn tại");
                return;
            }
            response = await client.SetTaskAsync("data/" + data.id + "/", data);
            Data result = response.ResultAs<Data>();
        }

        private async void button3_Click(object sender, EventArgs e)
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
                ngaythangnam = formattedDate,
                phone = textBox6.Text,
                mail = textBox9.Text
            };
            FirebaseResponse response = await client.GetTaskAsync("data/"+data.id);
            if (response.Body == "null")
            {
                MessageBox.Show("ID không tồn tại");
                return;
            }
            client.Delete("data/"+data.id);
            MessageBox.Show("Đã xóa ID: "+data.id);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            var id = textBox1.Text;
            FirebaseResponse response = await client.GetTaskAsync("data/"+id);
            if (response.Body == "null")
            {
                MessageBox.Show("ID không tồn tại");
                return;
            }
            Data obj = response.ResultAs<Data>();
            textBox1.Text = obj.id;
            textBox2.Text = obj.ten;
            textBox3.Text = obj.gioitinh;
            textBox4.Text = obj.diachi;
            textBox5.Text = obj.cmnd;
            dateTimePicker1.Value = DateTime.ParseExact(obj.ngaythangnam, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            textBox6.Text = obj.phone;
            textBox9.Text = obj.mail;
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            dt.Rows.Clear();

            FirebaseResponse response = await client.GetTaskAsync("data/");
            if (response.Body != "null")
            {
                List<Data> dataList = JsonConvert.DeserializeObject<List<Data>>(response.Body).Where(data => data != null).ToList();

                foreach (Data data in dataList)
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = data.id;
                    row["Ho va ten"] = data.ten;
                    row["Gioi tinh"] = data.gioitinh;
                    row["Dia chi"] = data.diachi;
                    row["CMND"] = data.cmnd;
                    row["Ngay thang nam"] = data.ngaythangnam;
                    row["Phone"] = data.phone;
                    row["Mail"] = data.mail;

                    dt.Rows.Add(row);
                }
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
 

                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox5.Text = row.Cells[4].Value.ToString();
                dateTimePicker1.Value = DateTime.ParseExact(row.Cells[5].Value.ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                textBox6.Text = row.Cells[6].Value.ToString();
                textBox9.Text = row.Cells[7].Value.ToString();
            }
        }

    }
}
