﻿using System;
using System.Collections.Generic;
using MsDbLibraryNS.StaffModel;


namespace MsDbLibraryNS.MsDbNS.FillerNS
{
    public class StaffFiller : MsDbBaseClass
    {
        internal protected String nameorConnectionString { get; set; }

        public StaffFiller(String NameorConnectionString = "name=StaffContext") 
            : base(new Message(), new StaffContext(NameorConnectionString)) {
            this.nameorConnectionString = NameorConnectionString;
        }
 
        public void FillStaffDb(List<object[]> AllStaff)
        {
            using (this.StaffCtx = new StaffContext(nameorConnectionString))
            {
                foreach (object[] row in AllStaff)
                {

                    Pupil Student = new Pupil();
                    Student.PupilIdOld = Convert.ToInt32(row[0]);
                    Student.FirstName = row[2].ToString();
                    Student.LastName = row[1].ToString();
                    Student.MiddleName = row[3].ToString();
                    Student.FullFIO = row[22].ToString();
                    Student.Clas = row[21].ToString();
                    Student.EljurAccountId = Convert.ToInt32(row[20]);

                    StaffCtx.Pupils.Add(Student);
                    StaffCtx.SaveChanges();
                    message.Display("Student success saved", "Warn");
                }
                var students = StaffCtx.Pupils;
                message.Display("List of objects:", "Info");
                foreach (Pupil p in students)
                {
                    message.Display(String.Format("{0}.{1} - {2} - {3} - {4} - {5}", p.PupilId, p.FirstName, p.LastName, p.MiddleName, p.FullFIO, p.Clas), "Info");
                }

            }
        }

       


    }
}
