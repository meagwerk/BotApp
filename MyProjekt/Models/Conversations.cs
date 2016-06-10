using System;
using System.Collections.Generic;

namespace MyProjekt.Models
{
    public class Conversations
    {
        public int ID { get; set; }
        public int Status { get; set; } //0 - ongoing, 1 - finished, 2 - continuation pending
        public int Score { get; set; } //bot evaluation score
        public DateTime MessageDate { get; set; }
        public String Messages { get; set; }

        /*  public Conversations(int id, int status, int score, DateTime messageDate, string messages)
          {
              this.ID = id;
              this.Status = status;
              this.MessageDate = messageDate;
              this.Messages = messages;
          }*/
    }
}