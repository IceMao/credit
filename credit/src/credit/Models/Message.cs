using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace credit.Models
{
    public enum State
    {
        正常,
        严重违法,
        经营异常
    }
    public class Message
    {
        public int Id { get; set; }
        public State State { get; set; }
        [ForeignKey("User")]
        public string RegistrationNumber { get; set; }
        public virtual User User { get; set; }
    }
}
