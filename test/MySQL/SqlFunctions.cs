using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using test.DTO;

namespace test.MySQL
{
    class sqlFunctions
    {
        public static async Task<List<TpPoint>> getTpPoints(GTANetworkAPI.Player player)
        {
            List<TpPoint> tpPoints= new List<TpPoint>();
            Main.consolelog("si");
            string query = $"SELECT * FROM tppoints WHERE owner= @playername;";
            using (MySqlCommand command = new MySqlCommand(query, MySQL.conn))
            {
                command.Parameters.AddWithValue("@playername", player.Name);

                using (var reader = await command.ExecuteReaderAsync())
                {
                   while(await reader.ReadAsync())
                    {    
                        TpPoint temp = new TpPoint();
                        temp.tpid = Convert.ToInt32(reader["tpid"]);
                        temp.tpname = reader["tpname"].ToString();
                        temp.tpcoords = new GTANetworkAPI.Vector3(float.Parse( reader["posX"].ToString()), float.Parse(reader["posY"].ToString()), float.Parse(reader["posZ"].ToString()));
                        temp.owner = reader["owner"].ToString();
                        tpPoints.Add(temp);
                    }
                    
                }
            }
            return tpPoints;
        }
        public static async Task<TpPoint> getTpPoint(GTANetworkAPI.Player player, string name)
        {
             TpPoint tp= new TpPoint();
            Main.consolelog("si");
            string query = $"SELECT * FROM tppoints WHERE owner= @playername and tpname=@name;";
            using (MySqlCommand command = new MySqlCommand(query, MySQL.conn))
            {
                command.Parameters.AddWithValue("@playername", player.Name);
                command.Parameters.AddWithValue("@name", name);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        tp.tpid = Convert.ToInt32(reader["tpid"]);
                        tp.tpname = reader["tpname"].ToString();
                        tp.tpcoords = new GTANetworkAPI.Vector3(float.Parse(reader["posX"].ToString()), float.Parse(reader["posY"].ToString()), float.Parse(reader["posZ"].ToString()));
                        tp.owner = reader["owner"].ToString();

                    }
                }
            }
            Main.consolelog("papa");
            return tp;
        }
        public static async void saveTpPoint( TpPoint  tpPoint)
        {
            string query = $"Insert into tppoints(tpname, posX,posY,posZ, owner) values(@tpname, @posX, @posY, @posZ , @owner);";
            using (MySqlCommand command = new MySqlCommand(query, MySQL.conn))
            {
                command.Parameters.AddWithValue("@tpname", tpPoint.tpname);
                command.Parameters.AddWithValue("@posX", tpPoint.tpcoords.X);
                command.Parameters.AddWithValue("@posY", tpPoint.tpcoords.Y);
                command.Parameters.AddWithValue("@posZ", tpPoint.tpcoords.Z);
                command.Parameters.AddWithValue("@owner", tpPoint.owner);

                var result= command.ExecuteNonQuery();
            }

        }
        public static async void deleteTpPoint(TpPoint tpPoint)
        {
            string query = $"Delete from tppoints where tpname=@tpname;";
            using (MySqlCommand command = new MySqlCommand(query, MySQL.conn))
            {
                command.Parameters.AddWithValue("@tpname", tpPoint.tpname);
                var result = command.ExecuteNonQuery();
            }

        }
    }
}
