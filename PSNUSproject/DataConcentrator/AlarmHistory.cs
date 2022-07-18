using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConcentrator
{
    public class AlarmHistory
    {
        [Key]
        public int Id { get; set; }
        public int AlarmID { get; set; }
        public string VarName { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Acknowledged { get; set; }

        public AlarmHistory()
        {

        }

        public AlarmHistory(int alarmID, string varName, string message, DateTime timeStamp)
        {
            AlarmID = alarmID;
            VarName = varName;
            Message = message;
            TimeStamp = timeStamp;
        }
    }
}
