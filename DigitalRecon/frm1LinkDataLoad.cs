using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DigitalRecon.DAL;

namespace DigitalRecon
{
    public partial class frm1LinkDataLoad : Form
    {
        DataTable dt = new DataTable();
        public frm1LinkDataLoad()
        {
            InitializeComponent();
          
            grdLoadData2.Visible = false;
            materialButton2.Visible = false;

         

        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            try
            {
               

                OpenFileDialog odlg1 = new OpenFileDialog();
                odlg1.ShowDialog();

                pcloader.Visible = true;
                pcloader.Dock = DockStyle.Fill;
                backgroundWorker1.RunWorkerAsync();

                // your code here 
                string CSVFilePathName = odlg1.FileName;


                using (StreamReader streamReader = File.OpenText(CSVFilePathName))
                {
                    string text = streamReader.ReadToEnd();
                    string[] lines = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    DataRow Row;
                    Int64 i = 0;
                    dt.Columns.Add("SETTLEMENT_DATE", typeof(string));
                    dt.Columns.Add("ACQ_INS_ID", typeof(string));
                    dt.Columns.Add("PAN", typeof(string));
                    dt.Columns.Add("AMOUNT", typeof(string));
                    dt.Columns.Add("FEE_CHARGES", typeof(string));
                    dt.Columns.Add("TRN_TIME", typeof(string));
                    dt.Columns.Add("STAN", typeof(string));
                    dt.Columns.Add("TERMINAL", typeof(string));
                    dt.Columns.Add("FROM_ACCOUNT", typeof(string));
                    dt.Columns.Add("TO_ACCOUNT", typeof(string));
                    dt.Columns.Add("DEST_IMD", typeof(string));
                    dt.Columns.Add("LOADBY", typeof(string));
                    dt.Columns.Add("LOADON", typeof(DateTime));
                    foreach (string line in lines)
                    {
                        if (i > 0 && i < lines.Count() - 2)
                        {
                            Row = dt.NewRow();
                            Row[0] = line.Substring(10, 4);
                            Row[1] = line.Substring(14, 6);
                            Row[2] = line.Substring(26, 20);
                            Row[3] = line.Substring(46, 12);
                            Row[4] = line.Substring(58, 9);
                            Row[5] = line.Substring(67, 10);
                            Row[6] = line.Substring(77, 6);
                            Row[7] = line.Substring(83, 4);
                            Row[8] = line.Substring(95, 21);
                            Row[9] = line.Substring(127, 24);
                            Row[10] = line.Substring(155, 9);
                            Row[11] = "admin";
                            Row[12] = DateTime.Now;
                            dt.Rows.Add(Row);
                        }
                        i++;
                    }
                }
                grdLoadData2.DataSource = dt;
                grdLoadData2.Visible = true;
                materialButton1.Visible = false;
                materialButton2.Visible = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error is " + ex.ToString());
                throw;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
                Thread.Sleep(5000);
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pcloader.Visible = false;
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            pcloader.Visible = true;
            pcloader.Dock = DockStyle.Fill;
            backgroundWorker1.RunWorkerAsync();

            DataTable dts = new DataTable();
            // SqlConnection con = new SqlConnection();
            // con.ConnectionString = GetConnectionString();
            DBComponent db = new DBComponent();
          
            db.CreateConnection();
            if (db.con.State == ConnectionState.Closed)
            {
                db.con.Open();
            }
            SqlBulkCopy objbulk = new SqlBulkCopy(db.con);
            //assigning Destination table name
            objbulk.DestinationTableName = "IBFT_1LINK";

            objbulk.ColumnMappings.Add("SETTLEMENT_DATE", "SETTLEMENT_DATE");
            objbulk.ColumnMappings.Add("ACQ_INS_ID", "ACQ_INS_ID");
            objbulk.ColumnMappings.Add("PAN", "PAN");
            objbulk.ColumnMappings.Add("AMOUNT", "AMOUNT");
            objbulk.ColumnMappings.Add("FEE_CHARGES", "FEE_CHARGES");
            objbulk.ColumnMappings.Add("TRN_TIME", "TRN_TIME");
            objbulk.ColumnMappings.Add("STAN", "STAN");
            objbulk.ColumnMappings.Add("TERMINAL", "TERMINAL");
            objbulk.ColumnMappings.Add("FROM_ACCOUNT", "FROM_ACCOUNT");
            objbulk.ColumnMappings.Add("TO_ACCOUNT", "TO_ACCOUNT");
            objbulk.ColumnMappings.Add("DEST_IMD", "DEST_IMD");
            objbulk.ColumnMappings.Add("LOADBY", "LOADBY");
            objbulk.ColumnMappings.Add("LOADON", "LOADON");




            objbulk.WriteToServer(dt);
            db.con.Close();
            MessageBox.Show("Data has been Imported successfully.", "Imported", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
           

        }
        private static string GetConnectionString()
        // To avoid storing the sourceConnection string in your code,
        // you can retrieve it from a configuration file.
        {
            return "Data Source=172.20.0.31;Initial Catalog=LBS_TEST;User ID= DRecon;Password=abc@1234";
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
