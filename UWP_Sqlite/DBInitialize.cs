using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_Sqlite
{
    class DBInitialize
    {
        public SQLiteConnection conn;
        public static int totalMoney;
        public static bool CreateTables()
        {
            var conn = new SQLiteConnection("sqlite.db");
            string sql = @"CREATE TABLE IF NOT EXISTS PersonalTransaction
            (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
            Name VARCHAR( 140 ),
            Detail TEXT,
            Description TEXT,
            Money DOUBLE,
            CreatedDate DATE,
            Category INT
);";

            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
            return true;
        }

        public static bool InsertTables(Person personal)
        {
            var conn = new SQLiteConnection("sqlite.db");
            try
            {
                using (var personalTransaction = 
                    conn.Prepare("INSERT INTO PersonalTransaction" +
                    "(Name, Detail, Description, Money, CreatedDate, Category) VALUES(?, ? , ? , ? , ?, ?)"))
                {
                    personalTransaction.Bind(1, personal.Name);
                    personalTransaction.Bind(2, personal.Description);
                    personalTransaction.Bind(3, personal.Detail);
                    personalTransaction.Bind(4, personal.Money);
                    personalTransaction.Bind(5, personal.CreatedDate.ToString("yyyy-MM-dd"));
                    personalTransaction.Bind(6, personal.Category);
                    personalTransaction.Step();
                }
                return true;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
                return false;
            }
          
        }

        public static List<Person> GetList()
        {
            var conn = new SQLiteConnection("sqlite.db");
            //Person person = null;
            List<Person> PersonWithAddressList = new List<Person>();
            try
            {
                using (var statement = conn.Prepare("SELECT * FROM PersonalTransaction  "))
                {
                    while (statement.Step() == SQLitePCL.SQLiteResult.ROW)
                    {
                        var personWithAddress = new Person();
                        personWithAddress.Name = (string)statement[1];
                        personWithAddress.Detail = (string)statement[2];
                        personWithAddress.Description = (string)statement[3];
                        personWithAddress.Money = (double)statement[4];
                        personWithAddress.CreatedDate = Convert.ToDateTime(statement[5]);
                        personWithAddress.Category = Convert.ToInt32(statement[6]);

                        PersonWithAddressList.Add(personWithAddress);
                    }
                }
                return PersonWithAddressList;
            }
            catch(Exception e)
            {
                Debug.WriteLine("Error list" +e);
                return null;
            }
        }

        public static List<Person> ListTransactionByCategory(int Category)
        {
            totalMoney = 0;
            var list = new List<Person>();
            try
            {
                var conn = new SQLiteConnection("sqlite.db");
                using (var stt = conn.Prepare($"select * from PersonalTransaction where Category = {Category}"))
                {
                    while (stt.Step() == SQLiteResult.ROW)
                    {
                        var personal = new Person()
                        {
                            Name = (string)stt["Name"],
                            Detail = (string)stt["Detail"],
                            Description = (string)stt["Description"],
                            Money = Convert.ToDouble(stt["Money"]),
                            CreatedDate = Convert.ToDateTime(stt["CreatedDate"]),
                            Category = Convert.ToInt32(stt["Category"]),
                        };
                        list.Add(personal);
                        totalMoney += Convert.ToInt32(stt["Money"]);
                    }
                }
                //Debug.WriteLine(list[0]);
                return list;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Co loi list" + ex);
                return null;
            }
        }

        public static List<Person> ListTransactionByStartDateAndEndDate(string startDate, string endDate)
        {
            totalMoney = 0;
            var list = new List<Person>();
            try
            {
                var conn = new SQLiteConnection("sqlite.db");
                using (var stt = conn.Prepare($"select * from PersonalTransaction where CreatedDate between '{startDate}' and '{endDate}'"))
                {
                    while (stt.Step() == SQLiteResult.ROW)
                    {
                        var personal = new Person()
                        {
                            Name = (string)stt["Name"],
                            Detail = (string)stt["Detail"],
                            Description = (string)stt["Description"],
                            Money = Convert.ToDouble(stt["Money"]),
                            CreatedDate = Convert.ToDateTime(stt["CreatedDate"]),
                            Category = Convert.ToInt32(stt["Category"]),
                        };
                        list.Add(personal);
                        totalMoney += Convert.ToInt32(stt["Money"]);
                    }
                }
                //Debug.WriteLine(list[0]);
                return list;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Co loi list" + ex);
                return null;
            }
        }


    }
}
