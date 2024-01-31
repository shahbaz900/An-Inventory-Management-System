using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace final_project_DB
{
    public partial class sales : sample
    {
        OracleConnection con;
        public sales()
        {
            InitializeComponent();
        }

        private void sales_Load(object sender, EventArgs e)
        {

            string conStr = @"DATA SOURCE = localhost:1521 / XE; USER ID = 21L-5430_project; PASSWORD = 123";
            con = new OracleConnection(conStr);
            perprice.Enabled = false;
            total.Enabled = false;

        }
        private void updateGrid()
        {
            con.Open();
            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = " SELECT * FROM product";
            getEmps.CommandType = CommandType.Text;
            OracleDataReader empDR = getEmps.ExecuteReader();//display
            DataTable empDT = new DataTable();//get data from datatable
            empDT.Load(empDR);
            dataGridView1.DataSource = empDT;
            
            con.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int hello = Convert.ToInt32(pid.Text);
            con.Open();
            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = "SELECT per_unit_price FROM product WHERE product_id = :productId";
            getEmps.Parameters.Add("productId", OracleDbType.Int32).Value = hello;
            OracleDataReader empDR = getEmps.ExecuteReader();
            if (empDR.Read())
            {
                perprice.Text = empDR.GetDecimal(0).ToString(); // get the value of the first column as a decimal and convert it to a string
            }
            else
            {
                perprice.Text = ""; // no rows returned, set the textbox to empty
            }
           
            int fasih = Convert.ToInt32(perprice.Text) * Convert.ToInt32(quantity.Text);

            total.Text = fasih.ToString();
            con.Close();


            //OracleCommand getEmp = con.CreateCommand();
            //getEmps.CommandText = "SELECT per_unit_price FROM product WHERE product_id = hello";
            //getEmps.Parameters.Add("productId", OracleDbType.Int32).Value = hello;
            //OracleDataReader empDR = getEmps.ExecuteReader();
            //if (empDR.Read())
            //{
            //    perprice.Text = empDR.GetDecimal(0).ToString(); // get the value of the first column as a decimal and convert it to a string
            //}
            //else
            //{
            //    perprice.Text = ""; // no rows returned, set the textbox to empty
            //}


            //con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {try
                {
                con.Open();
                OracleCommand insertEmp = con.CreateCommand();
                insertEmp.CommandText = " INSERT INTO sales (customer_id,product_id,quantity,per_unit_price,total) VALUES('" + cid.Text + "','" + pid.Text + "','" + quantity.Text + "','" + perprice.Text + "','" + total.Text + "')";
                insertEmp.CommandType = CommandType.Text;
                int rows = insertEmp.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show(" Inventory updated successfully! Thankyou! ");
                    con.Close();
                }
            }
            catch
            {
                con.Close();
            }
            
            updateGrid();
            cid.Clear();
            pid.Clear();
            quantity.Clear();
            perprice.Clear();
            total.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            main_screen obj = new main_screen();
            main.showWindow(obj, this, mdi.ActiveForm);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
          

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            con.Open();
            //dataGridView1.Columns.Clear();
            //dataGridView1.Rows.Clear();
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dataGridView1.RowCount > 0)
            {
                dt.Clear();
            }
          
            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = " SELECT * FROM sales";
            getEmps.CommandType = CommandType.Text;
            OracleDataReader empDR = getEmps.ExecuteReader();//display
            DataTable empDT = new DataTable();//get data from datatable
            empDT.Load(empDR);
            dataGridView1.DataSource = empDT;
            con.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pid_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;
            if (dataGridView1.RowCount > 0)
            {
                dt.Clear();
            }

            //dt.Clear();
            updateGrid();

        }
    }
}
