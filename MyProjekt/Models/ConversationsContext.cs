namespace MyProjekt.Models
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ConversationsContext : DbContext
    {
        // Der Kontext wurde für die Verwendung einer ConversationsContext-Verbindungszeichenfolge aus der 
        // Konfigurationsdatei ('App.config' oder 'Web.config') der Anwendung konfiguriert. Diese Verbindungszeichenfolge hat standardmäßig die 
        // Datenbank 'Bot_Application1.Models.ConversationsContext' auf der LocalDb-Instanz als Ziel. 
        // 
        // Wenn Sie eine andere Datenbank und/oder einen anderen Anbieter als Ziel verwenden möchten, ändern Sie die ConversationsContext-Zeichenfolge 
        // in der Anwendungskonfigurationsdatei.
        public ConversationsContext()
            : base("name=conversationscontext")
        {
            Database.SetInitializer<ConversationsContext>(new ConversationsSeed());
        }

        // Fügen Sie ein 'DbSet' für jeden Entitätstyp hinzu, den Sie in das Modell einschließen möchten. Weitere Informationen 
        // zum Konfigurieren und Verwenden eines Code First-Modells finden Sie unter 'http://go.microsoft.com/fwlink/?LinkId=390109'.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Conversations> Conversations { get; set; }
    }
}