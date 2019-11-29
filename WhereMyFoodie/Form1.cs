﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace WhereMyFoodie
{
    public partial class Form1 : Form
    {
        #region Variables
        string host = "127.0.0.1";
        string database = "db_food";
        string user = "root";
        string password = "";
        string connectionString;
        string sql;
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private MySqlDataAdapter adapter;
        private DataSet dst;
        #endregion
        public Form1()
        {
            ///TODO: add validation before input, check ada isi atau engga.
            ///bagaimana cara pindah ke form lain
            ///form pertama itu form user, lalu ada tombol untuk ke form admin, untuk insert data
            ///form user itu cuman bisa search, searchnya bisa pake type, ada dropdown list
            ///form admin itu buat insert data punya
            ///buat table lagi, dimana isinya foodSource, 
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connectionString = "Data source="+ host+"; Database="+database+"; User ID="+user+"; Password="+password+"; Charset=utf8; AllowUserVariables=true";
            conn = new MySqlConnection(connectionString);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                MessageBox.Show("Welcome");
                ShowDGV();
            }
        }

        private void ShowDGV()
        {
            dgvFood.Rows.Clear();
            sql = "select * from foods";
            cmd = new MySqlCommand(sql, conn);
            adapter = new MySqlDataAdapter(cmd);
            dst = new DataSet();
            adapter.Fill(dst, "foods");
            for (int i = 0; i < dst.Tables["foods"].Rows.Count; i++)
            {
                dgvFood.Rows.Add(1);
                dgvFood.Rows[i].Cells[0].Value = dst.Tables["foods"].Rows[i][0].ToString();
                dgvFood.Rows[i].Cells[1].Value = dst.Tables["foods"].Rows[i][1].ToString();
                dgvFood.Rows[i].Cells[2].Value = dst.Tables["foods"].Rows[i][2].ToString();
                dgvFood.Rows[i].Cells[3].Value = dst.Tables["foods"].Rows[i][3].ToString();
                dgvFood.Rows[i].Cells[4].Value = dst.Tables["foods"].Rows[i][4].ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (isEmpty() == true)
            {
                MessageBox.Show("please insert complete data");
            }
            else
            {
                sql = "insert into foods (foodName, foodSource, foodPlace, foodDescription)values ('" + txtFoodName.Text + "', '" + txtFoodSource.Text + "', '" + txtFoodPlace.Text + "', '" + txtFoodDescription.Text + "')";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data is inserted");
                ShowDGV();
            }
        }

        private void DGV_CellClick(DataGridView dgv, int cellIndex)
        {
            DataGridViewRow row = dgv.Rows[cellIndex];
            txtFoodId.Text = row.Cells[0].Value.ToString();
            txtFoodName.Text = row.Cells[1].Value.ToString();
            txtFoodSource.Text = row.Cells[2].Value.ToString();
            txtFoodPlace.Text = row.Cells[3].Value.ToString();
            txtFoodDescription.Text = row.Cells[4].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (isEmpty() == true)
            {
                MessageBox.Show("please insert complete data");
            }
            else
            {
                sql = "update foods set foodName = '" + txtFoodName.Text + "', foodSource = '" + txtFoodSource.Text + "', foodPlace = '" + txtFoodPlace.Text + "', foodDescription = '" + txtFoodDescription.Text + "' where foodId = '" + txtFoodId.Text + "'";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("update complete");
                ShowDGV();
            }
        }

        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DGV_CellClick(dgvFood, e.RowIndex );
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (isEmpty() == true)
            {
                MessageBox.Show("please insert complete data");
            } else
            {
                sql = "delete from foods where foodId = '" + txtFoodId.Text + "'";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("record deleted");
                ShowDGV();
            }
            
        }

        private bool isEmpty()
        {
            bool empty = false;
            if (string.IsNullOrEmpty(txtFoodId.Text))
            {
                empty = true;
            }
            if (string.IsNullOrEmpty(txtFoodName.Text))
            {
                empty = true;
            }
            if (string.IsNullOrEmpty(txtFoodPlace.Text))
            {
                empty = true;
            }
            if (string.IsNullOrEmpty(txtFoodSource.Text))
            {
                empty = true;
            }
            if (string.IsNullOrEmpty(txtFoodDescription.Text))
            {
                empty = true;
            }
            return empty;
        }
    }
}