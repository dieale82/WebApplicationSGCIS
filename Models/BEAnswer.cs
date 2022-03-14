using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BEAnswer
    {
        public int answerTypeId { get; set; }
        public int rowId { get; set; }
        public string message { get; set; }
    }

    public enum AnswerTypes
    { 
        Error = 0,
        Succesful = 1,
    }
}
