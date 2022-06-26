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
    public partial class frmCoreDataLoad : Form
    {
        DataTable dt = new DataTable();
        public frmCoreDataLoad()
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
                string[] Lines = File.ReadAllLines(CSVFilePathName);
                string[] Fields;
                Fields = Lines[0].Split(new char[] { ',' });
                int Cols = Fields.GetLength(0);
              //  DataTable dt = new DataTable();
                //1st row must be column names; force lower case to ensure matching later on.
                for (int i = 0; i < Cols; i++)
                    dt.Columns.Add(Fields[i].ToLower(), typeof(string));
                DataRow Row;
                for (int i = 1; i < Lines.GetLength(0); i++)
                {
                    Fields = Lines[i].Split(new char[] { ',' });
                    Row = dt.NewRow();
                    for (int f = 0; f < Cols; f++)
                        Row[f] = Fields[f];
                    dt.Rows.Add(Row);
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
            objbulk.DestinationTableName = "TBL_DIGITAL_RECON_LAND";

            objbulk.ColumnMappings.Add("Card Program ID", "CARD_PROGRAM_ID");
            objbulk.ColumnMappings.Add("Card BIN", "CARD_BIN");
            objbulk.ColumnMappings.Add("Card No", "CARD_NO");
            objbulk.ColumnMappings.Add("Card Reference Number", "CARD_REFERENCE_NUMBER");
            objbulk.ColumnMappings.Add("Card Serial Number", "CARD_SERIAL_NUMBER");
            objbulk.ColumnMappings.Add("Member ID", "MEMBER_ID");
            objbulk.ColumnMappings.Add("Transaction ID", "TRANSACTION_ID");
            objbulk.ColumnMappings.Add("ISO Message Type", "ISO_MESSAGE_TYPE");
            objbulk.ColumnMappings.Add("Transaction Date", "TRANSACTION_DATE");
            objbulk.ColumnMappings.Add("Transaction Time", "TRANSACTION_TIME");
            objbulk.ColumnMappings.Add("Settlement Date ", "SETTLEMENT_DATE");
            objbulk.ColumnMappings.Add("Acquirer ID", "ACQUIRER_ID");
            objbulk.ColumnMappings.Add("Merchant Name", "MERCHANT_NAME");
            objbulk.ColumnMappings.Add("Card Acceptor Code", "CARD_ACCEPTOR_CODE");
            objbulk.ColumnMappings.Add("Merchant Location", "MERCHANT_LOCATION");
            objbulk.ColumnMappings.Add("Merchant City", "MERCHANT_CITY");
            objbulk.ColumnMappings.Add("Merchant State", "MERCHANT_STATE");
            objbulk.ColumnMappings.Add("Merchant Country Code", "MERCHANT_COUNTRY_CODE");
            objbulk.ColumnMappings.Add("Merchant Category Code", "MERCHANT_CATEGORY_CODE");
            objbulk.ColumnMappings.Add("Transaction Local Date time", "TRANSACTION_LOCAL_DATE_TIME");
            objbulk.ColumnMappings.Add("Transaction Type", "TRANSACTION_TYPE");
            objbulk.ColumnMappings.Add("Device Type", "DEVICE_TYPE");
            objbulk.ColumnMappings.Add("Device ID", "DEVICE_ID");
            objbulk.ColumnMappings.Add("Service ID", "SERVICE_ID");
            objbulk.ColumnMappings.Add("Response Code", "RESPONSE_CODE");
            objbulk.ColumnMappings.Add("System Trace Audit Number (STAN)", "STAN");
            objbulk.ColumnMappings.Add("Original Trace Audit Number", "ORIGINAL_TRACE_AUDIT_NUMBER");
            objbulk.ColumnMappings.Add("Amount Requested", "AMOUNT_REQUESTED");
            objbulk.ColumnMappings.Add("Exchange Rate", "EXCHANGE_RATE");
            objbulk.ColumnMappings.Add("Transaction Amount Processed", "TRANSACTION_AMOUNT_PROCESSED");
            objbulk.ColumnMappings.Add("Currency Code", "CURRENCY_CODE");
            objbulk.ColumnMappings.Add("Settlement Amount", "SETTLEMENT_AMOUNT");
            objbulk.ColumnMappings.Add("Settlement Currency", "SETTLEMENT_CURRENCY");
            objbulk.ColumnMappings.Add("Cashback Amount", "CASHBACK_AMOUNT");
            objbulk.ColumnMappings.Add("Running Available balance", "RUNNING_AVAILABL_BALANCE");
            objbulk.ColumnMappings.Add("Running Balance", "RUNNING_BALANCE");
            objbulk.ColumnMappings.Add("Interchange Fee", "INTERCHANGE_FEE");
            objbulk.ColumnMappings.Add("CCA fee", "CCA_FEE");
            objbulk.ColumnMappings.Add("Card Status CODE", "CARD_STATUS_CODE");
            objbulk.ColumnMappings.Add("Card status CODE after transaction", "CARD_STATUS_CODE_AFTER_TRANS");
            objbulk.ColumnMappings.Add("Transaction Description", "TRANSACTION_DESCRIPTION");
            objbulk.ColumnMappings.Add("Switch type", "SWITCH_TYPE");
            objbulk.ColumnMappings.Add("Switch ID", "SWITCH_ID");
            objbulk.ColumnMappings.Add("Network ID", "NETWORK_ID");
            objbulk.ColumnMappings.Add("Network Name", "NETWORK_NAME");
            objbulk.ColumnMappings.Add("Authorization ID response", "AUTHORIZATION_ID");
            objbulk.ColumnMappings.Add("Retrieval Reference Number", "RESPONSE_RETRIEVAL_REFERENCE");
            objbulk.ColumnMappings.Add("Chargeback Flag Indicator", "CHARGEBACK_FLAG");
            objbulk.ColumnMappings.Add("PAN entry mode", "PAN_ENTRY_MODE");
            objbulk.ColumnMappings.Add("Process Mode(Card Presence Indicator)", "PROCESS_MODE");
            objbulk.ColumnMappings.Add("Expiration Date", "EXPIRATION_DATE");
            objbulk.ColumnMappings.Add("Type ID(UCAF Indicator)", "TYPE_ID");
            objbulk.ColumnMappings.Add("Service Definition � FeeFlat ", "SERVICE_DEFINITION_FEEFLAT");
            objbulk.ColumnMappings.Add("Service Definition � FeePercentage", "SERVICE_DEF_FEEPERCENT");
            objbulk.ColumnMappings.Add("Settlement exchange Rate", "SETTLEMENT_EXCHANGE_RATE");
            objbulk.ColumnMappings.Add("Billing Currency", "BILLING_CURRENCY");
            objbulk.ColumnMappings.Add("Billing Exchange Rate", "BILLING_EXCHANGE_RATE");
            objbulk.ColumnMappings.Add("Partial Amount", "PARTIAL_AMOUNT");
            objbulk.ColumnMappings.Add("Pre-Auth Amount", "PREAUTH_AMOUNT");
            objbulk.ColumnMappings.Add("Pre-Auth Multi-Completion Seq", "PREAUTH_MULTI_COMPLETION_SEQ");
            objbulk.ColumnMappings.Add("Additional Amount", "ADDITIONAL_AMOUNT");
            objbulk.ColumnMappings.Add("Pre-Auth Multi-Completion Count", "PREAUTH_MULTI_COMPLETION_COUNT");
            objbulk.ColumnMappings.Add("Acquirer Reference Number", "ACQUIRER_REFERENCE_NUMBER");
            objbulk.ColumnMappings.Add("Forward Institution ID", "FORWARD_INSTITUTION_ID");
            objbulk.ColumnMappings.Add("Exception Code", "EXCEPTION_CODE");
            objbulk.ColumnMappings.Add("Advice Reason Code", "ADVICE_REASON_CODE");
            objbulk.ColumnMappings.Add("Pre-Auth Expiration Date Time", "PREAUTH_EXPIRATION_DATE_TIME");
            objbulk.ColumnMappings.Add("Auth_ Characteristics", "AUTH_CHARACTERISTICS");
            objbulk.ColumnMappings.Add("Switch Serial No", "SWITCH_SERIAL_NO");
            objbulk.ColumnMappings.Add("Acquirer Fee", "ACQUIRER_FEE");
            objbulk.ColumnMappings.Add("Network Data", "NETWORK_DATA");
            objbulk.ColumnMappings.Add("Switch Data", "SWITCH_DATA");
            objbulk.ColumnMappings.Add("Rev_ Response Code", "REV_RESPONSE_CODE");
            objbulk.ColumnMappings.Add("ISO Serial Number", "ISO_SERIAL_NUMBER");
            objbulk.ColumnMappings.Add("Fee TAN", "FEE_TAN");
            objbulk.ColumnMappings.Add("Fee Charged", "FEE_CHARGED");
            objbulk.ColumnMappings.Add("Multi Currency", "MULTI_CURRENCY");
            objbulk.ColumnMappings.Add("PIN Based", "PIN_BASED");
            objbulk.ColumnMappings.Add("International", "INTERNATIONAL");
            objbulk.ColumnMappings.Add("Clerk ID", "CLERK_ID");
            objbulk.ColumnMappings.Add("Card Design", "CARD_DESIGN");
            objbulk.ColumnMappings.Add("POS Condition Codes", "POS_CONDITION_CODES");
            objbulk.ColumnMappings.Add("PIN Entry Mode", "PIN_ENTRY_MODE");
            objbulk.ColumnMappings.Add("Payment Initiation Channel", "PAYMENT_INITIATION_CHANNEL");
            objbulk.ColumnMappings.Add("Terminal Location Indicator", "TERMINAL_LOC_INDICATOR");
            objbulk.ColumnMappings.Add("POS PIN Capture mode", "POS_PIN_CAPTURE_MODE");
            objbulk.ColumnMappings.Add("Funding Card/Account", "FUNDING_CARD_ACCOUNT");
            objbulk.ColumnMappings.Add("Payment Method", "PAYMENT_METHOD");
            objbulk.ColumnMappings.Add("Network Optional Issuer Fee", "NETWORK_OPTIONAL_ISSUER_FEE");
            objbulk.ColumnMappings.Add("Trans Indicator", "TRANS_INDICATOR");
            objbulk.ColumnMappings.Add("Network Risk Code", "NETWORK_RISK_CODE");

           

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
