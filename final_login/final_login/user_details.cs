using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

namespace final_login
{
    public partial class user_details : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
    
        public user_details()
        {
            InitializeComponent();
            BindGridView();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_id.Text = "";
            txt_username.Text = "";
            txt_password.Text = "";
            txt_address.Text = "";
            txt_email.Text = "";
            txt_accdate.Text = "";
            txt_manager.Text = "";
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_id.Text) || string.IsNullOrEmpty(txt_username.Text) ||
                string.IsNullOrEmpty(txt_password.Text) || string.IsNullOrEmpty(txt_address.Text) ||
                string.IsNullOrEmpty(txt_email.Text) || string.IsNullOrEmpty(txt_accdate.Text) ||
                string.IsNullOrEmpty(txt_manager.Text))
            {
                MessageBox.Show("Please enter all the detials");
                return;
            }

            SqlConnection conn = new SqlConnection(cs);
            string query2 = "SELECT * FROM user_info WHERE id=@id";
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            cmd2.Parameters.AddWithValue("@id", txt_id.Text);
            
            conn.Open();

            SqlDataReader reader = cmd2.ExecuteReader();

            if (reader.HasRows == true)
            {
                MessageBox.Show("ID already exist");
            }
            else
            {
                conn.Close();

                string query = "INSERT INTO user_info VALUES(@id,@uname,@pass,@add,@email,@accdate,@manager)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", txt_id.Text);
                cmd.Parameters.AddWithValue("@uname", txt_username.Text);
                cmd.Parameters.AddWithValue("@pass", txt_password.Text);
                cmd.Parameters.AddWithValue("@add", txt_address.Text);
                cmd.Parameters.AddWithValue("@email", txt_email.Text);
                cmd.Parameters.AddWithValue("@accdate", txt_accdate.Text);
                cmd.Parameters.AddWithValue("@manager", txt_manager.Text);

                conn.Open();
                int check = cmd.ExecuteNonQuery(); //insert,delete,update
                if (check > 0)
                {
                    MessageBox.Show("User has been added");
                    BindGridView();
                }
                else
                {
                    MessageBox.Show("Error");
                }
                conn.Close();
            }

        }

        void BindGridView()
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT * FROM user_info";
            SqlDataAdapter sda = new SqlDataAdapter(query,conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        private void user_details_Load(object sender, EventArgs e)
        {

        }
    }
}
