﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommon;
using eljur_notifier.MsDbNS;
using System.Data.SqlClient;

namespace eljur_notifier.EljurNS
{
    class EljurApiSender
    {
        internal protected Message message { get; set; }
        internal protected MsDb msDb { get; set; }
        internal protected Config config { get; set; }

        public EljurApiSender(Config Config, MsDb MsDb)
        {
            this.message = new Message();
            this.msDb = MsDb;
            this.config = Config;
        }

        public Boolean SendNotifyFirstPass(object[] PupilIdOldAndTime)
        {
            int EljurAccountId = msDb.getEljurAccountIdByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));          
            String FullFIO = msDb.getFullFIOByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));
            String Clas = msDb.getClasByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));
            TimeSpan StartTimeLessons = msDb.getStartTimeLessonsByClas(Clas);
            //TimeSpan EndTimeLessons = msDb.getEndTimeLessonsByClas(Clas);
 
            //var timeNow = DateTime.Now.TimeOfDay;
            var EventTime = TimeSpan.Parse(PupilIdOldAndTime[1].ToString());
            if (EventTime > StartTimeLessons)
            {
                msDb.SetStatusCameTooLate(Convert.ToInt32(PupilIdOldAndTime[0]));
                message.Display("Notify about student " + FullFIO + " who came too late was sent to " + EljurAccountId + " EljurAccountId", "Warn");
            }
            else
            {
                message.Display("Notify about FirstPass by student " + FullFIO + " was sent to " + EljurAccountId + " EljurAccountId", "Warn");
            }             
            return true;
        }

        public Boolean SendNotifyLastPass(object[] PupilIdOldAndTime)
        {
            int EljurAccountId = msDb.getEljurAccountIdByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));
            String FullFIO = msDb.getFullFIOByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));
            String Clas = msDb.getClasByPupilIdOld(Convert.ToInt32(PupilIdOldAndTime[0]));
            TimeSpan EndTimeLessons = msDb.getEndTimeLessonsByClas(Clas);

            var EventTime = TimeSpan.Parse(PupilIdOldAndTime[1].ToString());
            var EventTimePlus15Min = EventTime.Add(new TimeSpan(0, 15, 0));
            var timeNow = DateTime.Now.TimeOfDay;

            if (timeNow > EventTimePlus15Min)
            {
                if (EventTime > EndTimeLessons)
                {
                    message.Display("Notify about student " + FullFIO + " who went home was sent to " + EljurAccountId + " EljurAccountId", "Warn");
                    return true;
                }
                else
                {
                    message.Display("Notify about student " + FullFIO + " who went home too early was sent to " + EljurAccountId + " EljurAccountId", "Warn");
                    msDb.SetStatusWentHomeTooEarly(Convert.ToInt32(PupilIdOldAndTime[0]));
                    return true;
                }          
            }
            else
            {
                message.Display("Too early to make a decision. 15 minutes have not passed yet.", "Warn");
                return false;
            }
        }      
    }
}