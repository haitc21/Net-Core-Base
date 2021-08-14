using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace ADO
{
    class Program
    {
        public static void ShowTable(DataTable table)
        {
            Console.WriteLine($"Ten bang: {table.TableName}");
            Console.WriteLine($"So sot: {table.Columns.Count}");
            Console.WriteLine($"So hang: {table.Rows.Count}");
            string cot = "";
            foreach (DataColumn col in table.Columns)
            {
                cot += $"{col.ColumnName,20}";
            }
            Console.WriteLine(cot);
            foreach (DataRow row in table.Rows)
            {
                string hang = "";
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    hang += $"{row[i], 20}";
                }
                Console.WriteLine(hang);
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder["Server"] = "localhost,1433";
            connectionStringBuilder["Database"] = "xtlab";
            connectionStringBuilder["UID"] = "sa";
            connectionStringBuilder["PWD"] = "Password123";
            string connectionString = connectionStringBuilder.ToString();
            //  Console.WriteLine(connectionString);

            //  string connectionString = "Data Source = localhost,1433; Initial Catalog= xtlab; User ID = sa; Password = Password123";

            using var connection = new SqlConnection(connectionString);
            connection.Open();
            // Console.WriteLine(connection.State); // trang thai cua connection

            // using DbCommand command = new SqlCommand();
            // command.Connection = connection;
            // command.CommandText = "select Top(10) * from Sanpham";
            // command.CommandText = "select Count(*) from Danhmuc D WHERE D.DanhmucID >= @DanhmucID";

            // truyen tham so vao query
            // var sqlParam = new SqlParameter("DanhmucID", 10);
            // command.Parameters.Add(sqlParam);
            // sqlParam.Value = 3;

            // co 3 cahc thuc thi query
            // using var dataReader = command.ExecuteReader(); //dung khi ket qua tra ve co nhieu dong
            // var dataTable = new DataTable();
            // dataTable.Load(dataReader);

            // if (dataReader.HasRows)
            // {
            //     while (dataReader.Read())
            //     {
            //         Console.WriteLine(dataReader["TenDanhMuc"]);
            //     }
            // }
            // else
            // {
            //     Console.WriteLine("Khong co du lieu");
            // }

            // var dataReader2 = command.ExecuteNonQuery(); // tra ve so ban ghi bi tac dong, hay dung khi thay doi du lieu


            // var dataReader3 = command.ExecuteScalar(); // tra ve gia tri dong 1 cot 1, hay dung khi muon lay gia tri cua 1 truy van
            // var scalarValue = command.ExecuteScalar();
            // Console.WriteLine(scalarValue);

            // exec Stroe
            // command.CommandText = "TestSP";
            // command.CommandType = CommandType.StoredProcedure;
            // var id = new SqlParameter("@Id", 10);
            // command.Parameters.Add(id);
            // var kq = command.ExecuteReader();
            // if (kq.HasRows)
            // {
            //     while (kq.Read())
            //     {
            //         var row = $"SP: {kq["TenSanpham"].ToString()} - DM: {kq["TenDanhMuc"].ToString()}";
            //         Console.WriteLine(row);
            //     }
            // }
            // else
            // {
            //     Console.WriteLine("Khong co du lieu");
            // }

            // // DataSet là đối tượng chứa các áDâtTable
            // // trong ADO thi DataTable ánh xạ trực tiếp đến bảng trong DB

            // var dataSet = new DataSet();
            // var table = new DataTable("MyTable");
            // dataSet.Tables.Add(table);

            // table.Columns.Add("STT");
            // table.Columns.Add("Ten");
            // table.Columns.Add("Tuoi");

            // table.Rows.Add(1, "Hai", 26);
            // table.Rows.Add(2, "Tran", 27);
            // table.Rows.Add(3, "Cao", 28);

            // ShowTable(table);

            // // SqlDataAdapter chứa các DbCommand kiểu như insert, select, delete... thao tác với db
            var dtAdapter = new SqlDataAdapter();
            dtAdapter.TableMappings.Add("Table", "NhanVien");
            string query = "select  NhanviennID, Ho + ' ' + Ten, CONVERT(VARCHAR, NgaySinh, 103) from NhanVien";
            dtAdapter.SelectCommand = new SqlCommand(query, connection);

            var dtSet = new DataSet();
            dtAdapter.Fill(dtSet);
            var dtTable = dtSet.Tables["NhanVien"];
            ShowTable(dtTable);







            connection.Close();


        }
    }
}
