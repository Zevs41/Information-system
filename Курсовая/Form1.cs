using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Курсовая
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        private void buttonSign_Click(object sender, EventArgs e)
        {
            sql_con.Open();
            string UserLogin = textBoxLogin.Text;
            string UserPass = textBoxPassword.Text;            
            SQLiteCommand sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = "SELECT * FROM Personal WHERE Login = '" + UserLogin + "' AND Pass = '" + UserPass+"'";
            DB.SelectCommand = sql_cmd;
            DS.Reset();
            DB.Fill(DT);
            sql_cmd.ExecuteNonQuery();

            if (DT.Rows.Count > 0)
            {
                MessageBox.Show("Succesfull");
                Form2 form2 = new Form2();
                form2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Fail");
            }
            LoadData();
            sql_con.Close();
        }

        private void SetConnection()
        {
            sql_con = new SQLiteConnection("Data Source=dbKursach.db;Version=3;Compress=True;");
        }

        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        private void LoadData()
        {
            SetConnection();
            sql_con.Open();
            sql_cmd=sql_con.CreateCommand();
            string ComandText = "SELECT * FROM Personal";
            DB = new SQLiteDataAdapter(ComandText, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            dataGridView1.DataSource = DT;
            sql_con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            textBoxPassword.PasswordChar = '*';
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            sql_con.Close ();
        }
    }
}
