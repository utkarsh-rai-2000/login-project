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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace final_login
{
    public partial class Form1 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_username.Text = "";
            txt_password.Text = "";
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_username.Text) || string.IsNullOrEmpty(txt_password.Text))
            {
                MessageBox.Show("Please enter both username and password");
                return;
            }

            //prevent SQL injection attacks
            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT * FROM user_info WHERE username=@user AND password=@pass";
            SqlCommand cmd = new SqlCommand(query,conn);
            cmd.Parameters.AddWithValue("@user",txt_username.Text);
            cmd.Parameters.AddWithValue("@pass", txt_password.Text);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.HasRows==true)
            {
                user_details form2 = new user_details();
                form2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }

            conn.Close();


        }
    }
}
