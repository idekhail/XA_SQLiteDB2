using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace XA_SQLiteDB2
{
    public class SQLiteDB
    {
        //database path
        private readonly string dbPath = Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MyDB4.db3");
        public SQLiteDB()
        {
            //Creating database, if it doesn't already exist 
            if (!File.Exists(dbPath))
            {
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Users>();
                //  db.CreateTable<Students>();
            }
        }
        //  Insert the object to Users table
        //  ادخال مستخدم
        public void InsertUser(Users newUser)
        {
            var db = new SQLiteConnection(dbPath);
            db.Insert(newUser);
        }
        // Object ارجاع بيانات مستخدم واحد على شكل   
        public Users GetUser(string username, string password)
        {
            var db = new SQLiteConnection(dbPath);
            Console.WriteLine("Reading data From Table");
            var table = db.Table<Users>();
            try
            {
                foreach (var s in table)
                {
                    if (string.Equals(s.Username, username) && string.Equals(s.Password, password))
                        return s;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public Users GetUser(string username)
        {
            var db = new SQLiteConnection(dbPath);
            Console.WriteLine("Reading data From Table");
            var table = db.Table<Users>();
            try
            {
                foreach (var s in table)
                {
                    if (string.Equals(s.Username, username))
                        return s;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        //=================================
        // تحديث مستخدم
        public void UpdateUser(Users user)
        {
            var db = new SQLiteConnection(dbPath);
            db.Update(user);
        }

        public string GetAllUsers()
        {
            string data = "";
            var db = new SQLiteConnection(dbPath);
            Console.WriteLine("Reading data From Table");
            var table = db.Table<Users>();
            try
            {
                foreach (var s in table)
                    data += s.UId + "\t" + s.Username + "\t" + s.Password + "\t" + s.Mobile + "\t" + s.Email + "\n";
                return data;
            }
            catch
            {
                return "Empty";
            }
        }


        [Table("Users")]
        public class Users
        {
            [PrimaryKey, AutoIncrement, Column("_uid")]
            public int UId { get; set; }
            [MaxLength(3)]
            public string Username { get; set; }
            [MaxLength(8)]
            public string Password { get; set; }
            [MaxLength(10)]
            public string Mobile { get; set; }
            [MaxLength(0)]
            public string Email { get; set; }
        }      
    } 
}