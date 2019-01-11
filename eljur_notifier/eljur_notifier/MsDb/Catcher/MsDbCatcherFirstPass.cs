﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eljur_notifier.AppCommonNS;
using eljur_notifier.EljurNS;
using eljur_notifier.MsDbNS.SetterNS;
using eljur_notifier.StaffModel;

namespace eljur_notifier.MsDbNS.CatcherNS
{
    public class MsDbCatcherFirstPass: EljurBaseClassWithConfigAndStaffContext
    {    
        internal protected MsDbSetter msDbSetter { get; set; }
        internal protected EljurApiSender eljurApiSender { get; set; }

        public MsDbCatcherFirstPass()
        {
            this.msDbSetter = new MsDbSetter();
            this.eljurApiSender = new EljurApiSender(config);
        }

        public void catchFirstPass()
        {
            using (this.StaffCtx = new StaffContext())
            {
                var PupilIdOldAndTimeRows = from e in StaffCtx.Events
                                            where e.NotifyWasSend == false && e.EventName == "Первый проход"
                                            orderby e.EventTime
                                            select new 
                                            {
                                                PupilIdOld = Convert.ToInt32(e.PupilIdOld),
                                                EventTime = TimeSpan.Parse(e.EventTime.ToString())
                                            };
                foreach (var PupilIdOldAndTime in PupilIdOldAndTimeRows)
                {
                    var PupilIdOldAndTimeMassObjects = new object[2];
                    PupilIdOldAndTimeMassObjects[0] = PupilIdOldAndTime.PupilIdOld;
                    PupilIdOldAndTimeMassObjects[1] = PupilIdOldAndTime.EventTime;
                    Boolean result = eljurApiSender.SendNotifyFirstPass(PupilIdOldAndTimeMassObjects);
                    if (result == true)
                    {
                        msDbSetter.SetStatusNotifyWasSend(Convert.ToInt32(PupilIdOldAndTime.PupilIdOld));
                    }
                }

            }

            
        }
        
    }
}
