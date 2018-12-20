using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace eljur_notifier
{
    class Firebird
    {
        internal protected FbConnection FbCon { get; set; }
        internal protected String ConnectStr { get; set; }
        internal protected DateTime beforeDt { get; set; }
        internal protected DateTime afterDt { get; set; }

        public Firebird(String ConnectStr)
        {
            this.ConnectStr = ConnectStr;
            this.FbCon = Firebird.getConnection(this.ConnectStr);
            this.beforeDt = Convert.ToDateTime("2000-12-31 23:59:59");
            this.afterDt = Convert.ToDateTime("2000-12-31 23:59:59");
        }

        public static FbConnection getConnection(String ConnectStr)
        {
            FbConnection fbcon = new FbConnection(ConnectStr);
            return fbcon;
        }

        public List<object[]> getOneStaff(int staffNumber)
        {
            this.FbCon.Open();
            FbCommand command = this.FbCon.CreateCommand();
            command.CommandText = "select * from STAFF";
            FbDataReader reader = command.ExecuteReader();
            int count = 0;
            int count_staff = 0;
            var staff = new List<object[]>();

            while (reader.Read())
            {
                if (count == staffNumber || staffNumber == -1)
                {
                    var columns = new object[reader.FieldCount];
                    reader.GetValues(columns);
                    staff.Add(columns);
                    //Console.WriteLine(staff);
                    //Console.WriteLine("staff[" + count + "][id]: " + staff[count_staff][0].ToString());
                    //Console.WriteLine("staff[" + count + "][LastName]: " + staff[count_staff][1].ToString());
                    //Console.WriteLine("staff[" + count + "][FirstName]: " + staff[count_staff][2].ToString());
                    //Console.WriteLine("staff[" + count + "][MiddleName]: " + staff[count_staff][3].ToString());
                    count_staff++;
                }
                count++;
            }
            Console.WriteLine("count: " + count);
            reader.Close();
            command.Dispose();
            this.FbCon.Close();
            return staff;
        }
        public List<object[]> getAllStaff()
        {
            return this.getOneStaff(-1);
        }

        public List<object[]> getAnySqlQuery(String SqlQuery)
        {
            this.FbCon.Open();
            var rows = new List<object[]>();
            FbCommand command = this.FbCon.CreateCommand();
            command.CommandText = SqlQuery;
            FbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var columns = new object[reader.FieldCount];
                reader.GetValues(columns);
                rows.Add(columns);
            }
            this.FbCon.Close();
            return rows; 
        }

        public List<object[]> getStaffByTimeStamp(DateTime TimeStamp, TimeSpan IntervalRequest)
        {
            var staff = new List<object[]>();
            if (this.beforeDt == Convert.ToDateTime("2000-12-31 23:59:59"))
            {
                Console.WriteLine("TRUE");
                this.afterDt = DateTime.Now;
                this.beforeDt = this.afterDt.Subtract(IntervalRequest); //Add(new TimeSpan(0, -1, 0));
            }
            else
            {
                Console.WriteLine("False");
                Console.WriteLine("IntervalRequest is: " + IntervalRequest);            
                //+1 sec because Firebird sql operation Between  is inclusive
                this.beforeDt = this.afterDt.Add(new TimeSpan(0, 0, 1)); // +1 second to IntervalRequest only for beforeDt
                this.afterDt = this.afterDt.Add(IntervalRequest);
            }
            
            Console.WriteLine("Time from new beforeDt: " + this.beforeDt.ToString());
            Console.WriteLine("Time from new afterDt: " + this.afterDt.ToString());

            String beforeStr = this.beforeDt.ToLongTimeString();
            String afterStr = this.beforeDt.ToLongTimeString();
           
            staff = getAnySqlQuery("select time_ev, staff_id  from REG_EVENTS WHERE  time_ev BETWEEN '" + beforeStr + "' AND '" + afterStr + "' ORDER BY time_ev ");
            foreach (object[] row in staff)
            {
                foreach (object element in row)
                {

                    //Console.WriteLine(element.ToString());
                    //Console.WriteLine(element.GetType());
                }
                break;
            }
            return staff;

        }

    }
}
