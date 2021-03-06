﻿using System;
using System.ComponentModel.DataAnnotations;


namespace MsDbLibraryNS.StaffModel
{
    public class Pupil
    {

        [Key]     
        public int PupilId { get; set; }
        public int PupilIdOld { get; set; }
        [MaxLength(500)]
        public string FirstName { get; set; }
        [MaxLength(500)]
        public string LastName { get; set; }
        [MaxLength(500)]
        public string MiddleName { get; set; }
        [MaxLength(1000)]
        public string FullFIO { get; set; }
        [MaxLength(10)]
        public string Clas { get; set; }
        public int EljurAccountId { get; set; }
        public Boolean NotifyEnable { get; set; }

    }
}
