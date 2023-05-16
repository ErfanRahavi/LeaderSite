using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaderSite
{
    internal class Database : Driver
    {
        string path = "table_leader.db";
        public void CreateDb()
        {
            if (!File.Exists(path))
            {
                Thread.Sleep(1000);
                SQLiteConnection.CreateFile(path);
                using (var sqlite = new SQLiteConnection(@"Data Source=" + path))
                {
                    sqlite.Open();
                    string sql = "create table leader(statements TEXT)";
                    SQLiteCommand command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();
                }

            }

        }
        public void SaveDb()
        {
            try
            {
                var newsTopic = driver.FindElement(By.XPath("/html/body/main/div[1]/section/main/article[1]/div[2]/h3")).Text;
                var newsText = driver.FindElement(By.XPath("/html/body/main/div[1]/section/main/article[2]/div[2]")).Text;
                string statementsDetails = $"{newsTopic}\n\n + {newsText}";

                SQLiteConnection con = new SQLiteConnection("Data Source = table_leader.db");
                con.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.CommandText = "insert into leader(statements) VALUES(@statements)";
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@statements", statementsDetails);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex); 
            }
        }
    }
}
