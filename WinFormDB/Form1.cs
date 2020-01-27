using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WinFormDB
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;
        public Form1()
        {
            InitializeComponent();
        }


        private async void Form1_Load(object sender, EventArgs e)
        {
            string connection= @"Data Source=DESKTOP-8UVSQOC\SQLEXPRESS;Initial Catalog=PersonalDB;Integrated Security=True";
            sqlConnection = new SqlConnection(connection);
            await sqlConnection.OpenAsync();
            
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT *FROM [PersonalInfoSet]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while(await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + " " + Convert.ToString(sqlReader["FName"]) + " " + Convert.ToString(sqlReader["LName"]) + " " + Convert.ToString(sqlReader["Age"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (label6.Visible == true)
                label6.Visible = false;


            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) &&
                !string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {

                SqlCommand command = new SqlCommand("INSERT INTO [PersonalInfoSet] (FName, LName, Age)VALUES(@FName,@LName,@Age)", sqlConnection);
                command.Parameters.AddWithValue("FName", textBox1.Text);
                command.Parameters.AddWithValue("LName", textBox5.Text);
                command.Parameters.AddWithValue("Age", textBox2.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {

                label6.Visible = true;
                label6.Text = "Поля не заполнены";
            }
            
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT *FROM [PersonalInfoSet]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + " " + Convert.ToString(sqlReader["FName"])+" "+ Convert.ToString(sqlReader["LName"]) + " " + Convert.ToString(sqlReader["Age"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }



        private async void button2_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox3.Text) &&
              !string.IsNullOrWhiteSpace(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [PersonalInfoSet] SET [FName]=@FName, [LName]=@LName, [Age]=@Age WHERE [Id]=@Id", sqlConnection);
                command.Parameters.AddWithValue("Id", textBox9.Text);
                command.Parameters.AddWithValue("FName", textBox3.Text);
                command.Parameters.AddWithValue("LName", textBox4.Text);
                command.Parameters.AddWithValue("Age", textBox6.Text);
                await command.ExecuteNonQueryAsync();
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox8.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [PersonalInfoSet] WHERE [Id]=@Id", sqlConnection);
                command.Parameters.AddWithValue("Id", textBox8.Text);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
