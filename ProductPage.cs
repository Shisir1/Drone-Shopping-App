﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agile_Project
{
    public partial class ProductPage : UserControl
    {
        public ProductPage()
        {
            InitializeComponent();
            loadData();
            //adding items to the comboBox
            comboBox1.Items.Add("Multi-Rotor");
            comboBox1.Items.Add("Small Drones");
            comboBox1.Items.Add("GPS Drones");
            comboBox1.Items.Add("Photography Drones");
            comboBox1.Items.Add("Weatherproof Drone");
        }
        DataTable mydt = new DataTable();
        SqlConnection con;

        public void loadData()
        {
            con = new SqlConnection();
            //Jorge Calle - Connection
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jorgecalle\\source\\repos\\Agile-Project\\DB-Product.mdf;Integrated Security=True";
            con.Open();

            //Build a command object to hold the SQL statement 
            SqlCommand mycommand = new SqlCommand();

            mycommand.CommandText = "SELECT * FROM Product ";


            mycommand.Connection = con;

            //use dataadapter class to carry the command 
            //to the DBMS and return the results 

            SqlDataAdapter myadpter = new SqlDataAdapter();
            mydt = new DataTable();
            myadpter.SelectCommand = mycommand;


            //binding]
            dataGridView.AutoGenerateColumns = false;
            dataGridView.DataSource = mydt;
            myadpter.Fill(mydt);




        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            ProductDetail pDetail = new ProductDetail();


            pDetail.pictureBox1.Image = ConvertByteArrayToImage((byte[])dataGridView.CurrentRow.Cells[0].Value);
            pDetail.lblName.Text = this.dataGridView.CurrentRow.Cells[1].Value.ToString();
            pDetail.lblPrice.Text = this.dataGridView.CurrentRow.Cells[2].Value.ToString();
            pDetail.lblId.Text = this.dataGridView.CurrentRow.Cells[3].Value.ToString();
            pDetail.lblBrand.Text = this.dataGridView.CurrentRow.Cells[4].Value.ToString();
            pDetail.lblModel.Text = this.dataGridView.CurrentRow.Cells[5].Value.ToString();
            pDetail.lblType.Text = this.dataGridView.CurrentRow.Cells[6].Value.ToString();
            pDetail.lblDescription.Text = this.dataGridView.CurrentRow.Cells[7].Value.ToString();


   
            pDetail.Show();
           
        }
        private static Image ConvertByteArrayToImage(byte[] byteArrayToConvert)
        {
            Image ret;

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(byteArrayToConvert, 0, byteArrayToConvert.Length);
                    ret = new Bitmap(ms);
                }
            }
            catch (Exception) { throw; }

            return ret;
        }

        public void searchData()
        {
            con = new SqlConnection();
            //Jorge Calle - Connection
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\jorgecalle\\source\\repos\\Agile-Project\\DB-Product.mdf;Integrated Security=True";
            con.Open();

            //Build a command object to hold the SQL statement 
            SqlCommand mycommand = new SqlCommand();

            mycommand.Parameters.Add("@type", SqlDbType.VarChar, 100);
            mycommand.Parameters["@type"].Value = comboBox1.Text.ToString();


            mycommand.CommandText = "SELECT * FROM Product WHERE Type=@type";


            mycommand.Connection = con;

            //use dataadapter class to carry the command 
            //to the DBMS and return the results 

            SqlDataAdapter myadpter = new SqlDataAdapter();
            mydt = new DataTable();
            myadpter.SelectCommand = mycommand;


            //binding]
            dataGridView.AutoGenerateColumns = false;
            dataGridView.DataSource = mydt;
            myadpter.Fill(mydt);


        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            searchData();
        }
    }

}
   
