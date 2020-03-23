using Worker_7ERFAcraft.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker_7ERFAcraft.Data
{
    public class DB_7ERFAcraft
    {
        readonly SQLiteConnection database;

        public DB_7ERFAcraft(string dbPath)
        {
            try
            {
                database = new SQLiteConnection(dbPath);
                 database.CreateTable<LoggedInUser>();
                database.CreateTable<Settings>();
                database.CreateTable<Lng>();
            }
            catch (Exception ex)
            {

            }
        }


        public int SaveUpdateLng(Lng item)
        {
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }
        public Lng GetLng()
        {
            return database.Table<Lng>().FirstOrDefault();
        }
        public LoggedInUser GetLoggedInUser()
        {
            return database.Table<LoggedInUser>().FirstOrDefault();
        }
        public int SaveUpdateLoggedInUser(LoggedInUser item)
        {
            //database.DeleteAll<LoggedInUser>();
            if (item.ID != 0)
            {
                return database.Update(item);
            }
            else
            {
                return database.Insert(item);
            }
        }
        public int ClearLoginDetails()
        {
            var status = 0;
            try
            {
                var data = database.Table<LoggedInUser>().ToList();
                foreach (var item in data)
                {
                    status = database.Delete(item);
                }
            }
            catch (Exception ex)
            {

            }
            return status;
        }


        public Settings GetSettings()
        {
            return database.Table<Settings>().FirstOrDefault();
        }
        public int SaveSettings(Settings Item)
        {
            try
            {
                database.DeleteAll<Settings>();

                return database.Insert(Item);
                 
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

    }
}
