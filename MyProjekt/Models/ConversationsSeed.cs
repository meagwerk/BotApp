using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyProjekt.Models
{
    public class ConversationsSeed : System.Data.Entity.DropCreateDatabaseIfModelChanges<ConversationsContext>
    {
        protected override void Seed(ConversationsContext context)
        {
            Conversations testConvers = new Conversations { ID = 0, Status = 0, Score = 0, MessageDate = DateTime.Now, Messages = "Test" };

            context.Conversations.Add(testConvers);
            context.SaveChangesAsync();
            base.Seed(context);
        }
    }
}