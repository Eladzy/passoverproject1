using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace PassOver1
{
    class CalcDao:ICalcDao
    {
        
        static string connString = "Data Source=.;Initial Catalog=XYCALC;Integrated Security=True;"+ "MultipleActiveResultSets=True";//using MARS

        public void GetNumbers(List<double> listX, List<double> listY)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                for (int i = 0; i < listX.Count; i++)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT_NUM", connection))
                {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@X", listX[i].ToString());
                        cmd.Parameters.AddWithValue("@Y", listY[i].ToString());
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            TableCreation();
        }

      
        public void TableCreation()
        {
            
            using (SqlConnection connection = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("FILL_X_Y_OP", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.ExecuteScalar();
                    connection.Close();
                }
                using (SqlCommand cmd1=new SqlCommand("SELECT * FROM FINALE",connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {



                        while (reader.Read())
                        {
                            var rowInfo = new
                            {
                                Id = (int)reader["ID"],
                                X = Convert.ToDouble(reader["_X"]),
                                Y = Convert.ToDouble(reader["_Y"]),
                                oP = (string)reader["Operator"],
                            };
                            double finaleResult=0;
                            if(rowInfo.oP.Contains("+"))
                                finaleResult = rowInfo.X + rowInfo.Y;
                            if (rowInfo.oP.Contains("-"))
                                finaleResult = rowInfo.X - rowInfo.Y;
                            if (rowInfo.oP.Contains("*"))
                                finaleResult = rowInfo.X * rowInfo.Y;
                            if (rowInfo.oP.Contains("/"))
                                finaleResult = rowInfo.X / rowInfo.Y;
                         
                            using (SqlCommand updater = new SqlCommand("update FINALE set Result=@result where ID=@id", connection))
                            {
                                updater.Parameters.AddWithValue("@result", finaleResult.ToString());
                                updater.Parameters.AddWithValue("id", rowInfo.Id.ToString());
                                updater.ExecuteNonQuery();
                            };
                        }
                    } 
                   
                }
                
                connection.Close();
            }

    }   }
}
