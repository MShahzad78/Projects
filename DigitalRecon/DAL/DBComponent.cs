using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalRecon.DAL
{
   
    class DBComponent
    {

      
        public SqlConnection con = new SqlConnection();
        public void CreateConnection()
        {
            
            con.ConnectionString = CommClass.conn;
        }
    }
}
