using Microsoft.Data.SqlClient;
using System.Data;

namespace ParkingManagementSystem.Models
{
    public class db
    {
        SqlConnection con = new SqlConnection("Data Source=BAO-HNG;Initial Catalog=ParkingManagementSystem.Data;Integrated Security=true");
        public DataTable GetRecord() { 
            SqlCommand com = new SqlCommand("select * from Sessions ",con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt= new DataTable();
            da.Fill(dt);
            return dt;  
        }
        public DataTable GetParkingSpace()
        {
            SqlCommand com = new SqlCommand("select * from ParkingSpaces ", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataTable GetParkingLot()
        {
            SqlCommand com = new SqlCommand("select * from ParkingLots ", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
