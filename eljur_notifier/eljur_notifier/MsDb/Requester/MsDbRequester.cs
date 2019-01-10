﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using eljur_notifier.StaffModel;
using eljur_notifier.MsDbNS;
using eljur_notifier.AppCommon;
using eljur_notifier.MsDbNS.SetterNS;

namespace eljur_notifier.MsDbNS.RequesterNS
{
    public class MsDbRequester
    {
        internal protected Message message { get; set; }
        internal protected MsDbSetter msDbSetter { get; set; }
        internal protected SqlConnection dbcon { get; set; }
        internal protected StaffContext StaffCtx { get; set; }

        public MsDbRequester()
        {
            this.message = new Message();
            this.msDbSetter = new MsDbSetter();

        }

        public static string RandomString(int length)
        {
            Random random = new Random(); 
            const string chars = "АБВ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public int getPupilIdOldByFullFio(String FullFIO)
        {
            using (this.StaffCtx = new StaffContext())
            {
                var query = from p in StaffCtx.Pupils
                            where p.FullFIO == FullFIO
                            select p.PupilIdOld;
                int PupilIdOld = query.First();
                return PupilIdOld;
            }
        }



        public String getClasByPupilIdOld(int PupilIdOld)
        {
            String Clas = "Default Clas";
            return Clas;
        }
        //{
        //    String Clas = "Default Clas";
        //    using (this.dbcon = new SqlConnection(config.ConStrMsDB))
        //    {
        //        dbcon.Open();
        //        using (SqlCommand command = new SqlCommand("SELECT Clas FROM Pupils WHERE PupilIdOld = '" + PupilIdOld + "'", dbcon))
        //        {
        //            message.Display("SELECT Clas FROM Pupils WHERE PupilIdOld = '" + PupilIdOld + "'", "Warn");
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    message.Display(String.Format("{0}", reader[0]), "Trace");
        //                    Clas = reader[0].ToString();
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    return Clas;
        //}



        public TimeSpan getEndTimeLessonsByClas(String Clas)
        {
            TimeSpan EndTimeLessons = TimeSpan.Parse("23:59:59");
            return EndTimeLessons;
        }
        //{
        //    TimeSpan EndTimeLessons = TimeSpan.Parse("23:59:59");
        //    using (this.dbcon = new SqlConnection(config.ConStrMsDB))
        //    {
        //        dbcon.Open();
        //        using (SqlCommand command = new SqlCommand("SELECT EndTimeLessons FROM Schedules where Clas = '" + Clas + "'", dbcon))
        //        {
        //            message.Display("SELECT EndTimeLessons FROM Schedules where Clas = '" + Clas + "'", "Warn");
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    var colums = new object[reader.FieldCount];
        //                    reader.GetValues(colums);
        //                    message.Display(string.Format("{0}", colums[0].ToString()), "Trace");
        //                    EndTimeLessons = TimeSpan.Parse(colums[0].ToString());
        //                    //break;
        //                }
        //            }
        //        }
        //    }
        //    return EndTimeLessons;
        //}


        public TimeSpan getStartTimeLessonsByClas(String Clas)
        {
            TimeSpan StartTimeLessons = TimeSpan.Parse("00:00:01");
            return StartTimeLessons;
        }
        //{
        //    TimeSpan StartTimeLessons = TimeSpan.Parse("00:00:01");
        //    using (this.dbcon = new SqlConnection(config.ConStrMsDB))
        //    {
        //        dbcon.Open();
        //        using (SqlCommand command = new SqlCommand("SELECT StartTimeLessons FROM Schedules where Clas = '" + Clas + "'", dbcon))
        //        {
        //            message.Display("SELECT StartTimeLessons FROM Schedules where Clas = '" + Clas + "'", "Warn");
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    var colums = new object[reader.FieldCount];
        //                    reader.GetValues(colums);
        //                    message.Display(string.Format("{0}", colums[0].ToString()), "Trace");
        //                    StartTimeLessons = TimeSpan.Parse(colums[0].ToString());
        //                    //break;
        //                }
        //            }
        //        }
        //    }
        //    return StartTimeLessons;
        //}


        public int getEljurAccountIdByPupilIdOld(int PupilIdOld)
        {
            int EljurAccountId = 0;
            return EljurAccountId;
        }
        //{
        //    int EljurAccountId = 0;
        //    using (this.dbcon = new SqlConnection(config.ConStrMsDB))
        //    {
        //        dbcon.Open();
        //        using (SqlCommand command = new SqlCommand("SELECT EljurAccountId FROM Pupils where PupilIdOld = '" + PupilIdOld + "'", dbcon))
        //        {
        //            message.Display("SELECT eljuraccountid FROM Pupils where PupilIdOld = '" + PupilIdOld + "'", "Warn");
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    var colums = new object[reader.FieldCount];
        //                    reader.GetValues(colums);
        //                    message.Display(string.Format("{0}", colums[0].ToString()), "Trace");
        //                    EljurAccountId = Convert.ToInt32(colums[0]);
        //                    //break;
        //                }
        //            }
        //        }
        //    }
        //    return EljurAccountId;
        //}


        public String getFullFIOByPupilIdOld(int PupilIdOld)
        {
            String FullFIO = "Default FullFIO";
            return FullFIO;
        }
        //{
        //    String FullFIO = "Default FullFIO";
        //    using (this.dbcon = new SqlConnection(config.ConStrMsDB))
        //    {
        //        dbcon.Open();
        //        using (SqlCommand command = new SqlCommand("SELECT FullFIO FROM Pupils WHERE PupilIdOld = '" + PupilIdOld + "'", dbcon))
        //        {
        //            message.Display("SELECT FullFIO FROM Pupils WHERE PupilIdOld = '" + PupilIdOld + "'", "Warn");
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    message.Display(String.Format("{0}", reader[0]), "Trace");
        //                    FullFIO = reader[0].ToString();
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    return FullFIO;
        //}


        public DateTime getModifyDate()
        {
            DateTime ModifyDate = new DateTime();
            return ModifyDate;
        }
        //{
        //    DateTime ModifyDate = new DateTime();
        //    using (this.dbcon = new SqlConnection(config.ConStrMsDB))
        //    {
        //        dbcon.Open();
        //        using (SqlCommand command = new SqlCommand("SELECT modify_date FROM sys.tables order by modify_date", dbcon))
        //        {
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                message.Display("SELECT modify_date FROM sys.tables order by modify_date", "Warn");
        //                while (reader.Read())
        //                {
        //                    message.Display(String.Format("{0}", reader[0]), "Trace");
        //                    ModifyDate = Convert.ToDateTime(reader[0].ToString());
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    return ModifyDate;
        //}



    }
}
