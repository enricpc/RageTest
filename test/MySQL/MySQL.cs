using System;
using System.IO;
using System.Reflection;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace test.MySQL
{
    class MySQL
    {
        public static bool IsConnectionSetUp = false;
        public static MySqlConnection conn;
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }


        public MySQL()
        {
            this.Host = "localhost";
            this.Username = "root";
            this.Password = "";
            this.Database = "test";
        }

        public static void InitConnection()
        {
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"SQLInfo.json");
            MySQL sql = new MySQL();
            if (File.Exists(filePath))
            {
                Main.consolelog("Fichero encontrado");
                String SQLData = File.ReadAllText(filePath);
                NAPI.Util.FromJson<MySQL>(SQLData);
                Main.consolelog(sql.ToString());
                string sqlConnection = "SERVER=" + sql.Host + ";PASSWORD=" + sql.Password + ";UID=" + sql.Username + ";DATABASE=" + sql.Database + ";";
                conn = new MySqlConnection(sqlConnection);
                try
                {
                    conn.Open();
                    Main.consolelog("db con set");
                    IsConnectionSetUp = true;
                }catch(Exception ex)
                {
                    Main.consolelog(ex.ToString());
                }
            }
            else
            {
                Main.consolelog("Fichero no encontrado");

                string sqlData = NAPI.Util.ToJson(sql);
                    using(StreamWriter writer= new StreamWriter(filePath))
                {
                    writer.WriteLine(sqlData);
                }
                Main.consolelog("Fichero creado");
                InitConnection();
            }
        }
    }
}
