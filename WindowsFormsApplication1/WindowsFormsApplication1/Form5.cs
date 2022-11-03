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
    public partial class Form5 : Form
    {
        DataRow dr;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");
        public Form5()
        {
            InitializeComponent();
            refreshCountry();
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
            //con.Open();
            string query = "exec getStates " + country;
            //MessageBox.Show(query);
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            //con.Close();
            dr = dt.NewRow();
            dr.ItemArray = new object[] { 0, "--Select State--" };
            dt.Rows.InsertAt(dr, 0);

            comboBox2.ValueMember = "StateId";
            comboBox2.DisplayMember = "Name";
            comboBox2.DataSource = dt;
        }

        public void refreshdistrict(int district)
        {
            //con.Open();
            string query = "exec getDistricts " + district;
            //MessageBox.Show(query);
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            //con.Close();
            dr = dt.NewRow();
            dr.ItemArray = new object[] { 0, "--Select District--" };
            dt.Rows.InsertAt(dr, 0);

            comboBox1.ValueMember = "Id";
            comboBox1.DisplayMember = "Name";
            comboBox1.DataSource = dt;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string studentId = textBox1.Text;
            con.Open();
            string query = "exec FillForm "+studentId;
            //MessageBox.Show(query);
            SqlCommand cmd = new SqlCommand(query, con);
            //cmd.Parameters.AddWithValue("@StudentId", textBox1.Text);
            //MessageBox.Show(textBox4.Text);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            //MessageBox.Show(dr.ToString());
            string Gender = string.Empty;
            string country = string.Empty;
            string state = string.Empty;
            string district = string.Empty;
            string gender = string.Empty;
            if (dr.Read())
            {
                textBox3.Text = dr["FirstName"].ToString();
                textBox2.Text = dr["LastName"].ToString();
                textBox4.Text = dr["Street"].ToString();
                dateTimePicker1.Text = dr["DOB"].ToString();
                country = dr["Country"].ToString();
                state = dr["State"].ToString();
                district = dr["City"].ToString();
                if (dr["Gender"].ToString().Equals("Male"))
                {
                    radioButton1.Checked = true;

                }
                else if (dr["Gender"].ToString().Equals("Female"))
                {
                    radioButton2.Checked = true;
                }
                else
                {
                    radioButton3.Checked = true;
                }
            }
            con.Close();
            comboBox3.Text = country;
            comboBox2.Text = state;
            comboBox1.Text = district;

            //radioButton1.Checked = true;
            //radioButton2.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedValue.ToString() != null)
            {
                int countryName = Convert.ToInt32(comboBox3.SelectedValue.ToString());
                refreshstate(countryName);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
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
           // MessageBox.Show(FirstName);
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
            int id = Convert.ToInt32(textBox1.Text);

            string query = "exec modifyStudent " + id +","+ FirstName + "," + lastName + ",'" + date + "'," + Gender + "," + street + "," + District + "," + State + "," + country;

            MessageBox.Show(query);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Updated sucessfully");
            Form3 f3 = new Form3();
            this.Hide();
            f3.ShowDialog();
            con.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }
    }
}
