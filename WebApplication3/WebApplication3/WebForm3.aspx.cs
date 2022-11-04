using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication3
{
    public partial class WebForm3 : System.Web.UI.Page
    {
    
        DataRow dr;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True");

        //ctor
        
        public void refreshCountry()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("exec displayCountry", con);

            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();
            DropDownList1.DataSource = sdr;
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "Id";
            DropDownList1.DataBind();
            sdr.Close();
            con.Close();

            /*
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            dr = dt.NewRow();
            dr.ItemArray = new object[] { 0, "--Select Country--" };
            dt.Rows.InsertAt(dr, 0);

           // DropDownList3.DataTextField = "Name";
            //DropDownList3.
            //DropDownList3.DataValueField = "Id";
           // DropDownList3.DataSource = dt;
            //comboBox3.ValueMember = "Id";
            //comboBox3.DisplayMember = "Name";
            //comboBox3.DataSource = dt;*/
        }

        public void refreshstate(int country)
        {
            con.Open();
            string query = "exec getStates " + country;
            //MessageBox.Show(query);
            SqlCommand cmd = new SqlCommand(query, con);

            SqlDataReader sdr;
            sdr = cmd.ExecuteReader();
            DropDownList2.DataSource = sdr;
            DropDownList2.DataTextField = "Name";
            DropDownList2.DataValueField = "StateId";
            DropDownList2.DataBind();
            sdr.Close();
            con.Close();

            /*
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            dr = dt.NewRow();
            dr.ItemArray = new object[] { 0, "--Select State--" };
            dt.Rows.InsertAt(dr, 0);

            //comboBox2.ValueMember = "StateId";
            //comboBox2.DisplayMember = "Name";
            //comboBox2.DataSource = dt;*/
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

            //comboBox1.ValueMember = "Id";
            //comboBox1.DisplayMember = "Name";
            //comboBox1.DataSource = dt;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //refreshCountry();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Label11.Text = "HI";
           // Label11.Text = DropDownList1.SelectedValue;
           // refreshCountry();
            int country = Convert.ToInt32(DropDownList1.SelectedValue);
            refreshstate(country);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label11.Text = Convert.ToString(DropDownList1.SelectedItem);
        }
    }
}