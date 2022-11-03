using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        DataRow dr;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
            refreshCountry();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        public void refreshCountry()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("exec displayCountry", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            dr = dt.NewRow();
            dr.ItemArray = new object[] { 0, "--Select Country--" };
            dt.Rows.InsertAt(dr, 0);
            comboBox3.ValueMember = "Id";
            comboBox3.DisplayMember = "Name";
            comboBox3.DataSource = dt;
        }
        public void refreshstate(int country)
        {
            con.Open();
            string query = "exec getStates "+country;
            //MessageBox.Show(query);
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            dr = dt.NewRow();
            dr.ItemArray = new object[] { 0, "--Select State--" };
            dt.Rows.InsertAt(dr, 0);

            comboBox2.ValueMember = "StateId";
            comboBox2.DisplayMember = "Name";
            comboBox2.DataSource = dt;
        }

        public void refreshdistrict(int district)
        {
            con.Open();
            string query = "exec getDistricts " + district;
            //MessageBox.Show(query);
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            dr = dt.NewRow();
            dr.ItemArray = new object[] { 0, "--Select District--" };
            dt.Rows.InsertAt(dr, 0);

            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Name";
            comboBox1.DataSource = dt;
        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox3.SelectedValue.ToString() != null)
            {
                int countryName = Convert.ToInt32(comboBox3.SelectedValue.ToString());
                refreshstate(countryName);
            }
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue.ToString() != null)
            {
                int stateName = Convert.ToInt32(comboBox2.SelectedValue.ToString());
                refreshdistrict(stateName);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            con.Open();
            string Gender;
            string FirstName = textBox3.Text;
            string lastName = textBox2.Text;
            dateTimePicker1.Format = DateTimePickerFormat.Long;
            string date = dateTimePicker1.Value.Date.ToString("dd-MM-yyyy");
            if (radioButton1.Checked == true)
            {
                Gender = "Male";
            }
            else if (radioButton2.Checked == true)
            {
                Gender = "Female";
            }
            else
            {
                Gender = "Others";
            }

            string street = textBox4.Text;
            string District = comboBox1.Text;
            string State = comboBox2.Text;
            string country = comboBox3.Text;
            ;

            string query = "exec addStudent " + FirstName + "," + lastName + ",'" + Convert.ToDateTime(date) + "'," + Gender + "," + street + "," + District + "," + State + "," + country;
            MessageBox.Show(query);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Inserted sucessfully");
            Form3 f3 = new Form3();
            this.Hide();
            f3.ShowDialog();
            con.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
    }
}
