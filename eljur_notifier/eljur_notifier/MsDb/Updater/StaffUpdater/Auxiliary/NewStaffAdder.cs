﻿using System;
using System.Collections.Generic;
using System.Linq;
using eljur_notifier.AppCommonNS;
using eljur_notifier.EljurNS;
using eljur_notifier.StaffModel;

namespace eljur_notifier.MsDbNS.UpdaterNS.StaffUpdaterNS
{
    public class NewStaffAdder : EljurBaseClass
    {
        public NewStaffAdder(String NameorConnectionString = "name=StaffContext") : base(new Message(), new StaffContext(NameorConnectionString), new EljurApiRequester()) { }

        public void AddNewPupil(List<object[]> AllStaff)
        {
            using (this.StaffCtx)
            {
                var PupilsToAdd = new List<Pupil>();
                foreach (object[] row in AllStaff)
                {
                    int Int32Row0 = Convert.ToInt32(row[0]);
                    var result = StaffCtx.Pupils.SingleOrDefault(p => p.PupilIdOld == Int32Row0);
                    if (result == null)
                    {
                        Pupil Student = new Pupil();
                        Student.PupilIdOld = Convert.ToInt32(row[0]);
                        Student.FirstName = row[2].ToString();
                        Student.LastName = row[1].ToString();
                        Student.MiddleName = row[3].ToString();
                        Student.FullFIO = row[22].ToString();
                        String clas = eljurApiRequester.getClasByFullFIO(Student.FullFIO);
                        Student.Clas = clas;
                        int eljurAccountId = eljurApiRequester.getEljurAccountIdByFullFIO(Student.FullFIO);
                        Student.EljurAccountId = eljurAccountId;
                        PupilsToAdd.Add(Student);                      
                    }
                }
                foreach (Pupil p in PupilsToAdd)
                {
                    StaffCtx.Pupils.Add(p);
                    StaffCtx.SaveChanges();
                    message.Display("New Student success saved", "Warn");
                }

            }

        }

    }
}
