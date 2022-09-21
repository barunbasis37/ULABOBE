using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models
{
    public class Schedule: Entity
    {
        public int? ScDay1 { get; set; }
        [ForeignKey("ScDay1")]
        public virtual WeekDay WeekDay1 { get; set; }
        public int? ScDay2 { get; set; }
        [ForeignKey("ScDay2")]
        public virtual WeekDay WeekDay2 { get; set; }
        public int? FromTime { get; set; }
        [ForeignKey("FromTime")]
        public virtual Time FTime { get; set; }
        public int? ToTime { get; set; }
        [ForeignKey("ToTime")]
        public virtual Time TTime { get; set; }
        public bool IsActive { get; set; }
        public int Priority { get; set; }
        


    }
}
