using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ParkingManagementSystem.Models;
using System.Data;

namespace ParkingManagementSystem.Controllers
{
    public class report : Controller
    {
        public IActionResult Index()
        {
            // Kết nối đến cơ sở dữ liệu và truy vấn dữ liệu từ procedure
            string connectionString = "server=TRUONGDUY\\SQLEXPRESS;Database=ParkingManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true;encrypt=false";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.SLVthang", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Lấy dữ liệu trả về và lưu vào list
                        List<ChartData> thongKeList = new List<ChartData>();
                        string[] listsl;
                        while (reader.Read())
                        {
                            ChartData thongKe = new ChartData();

                            thongKe.Thang = reader["Thang"].ToString();
                            thongKe.SoLuongVe = Convert.ToInt32(reader["SoLuongVe"]);
                            thongKeList.Add(thongKe);
                        }
                        ViewBag.ThongKeList = thongKeList;

                    }
                }
            }

            return View();
        }
    }
}
