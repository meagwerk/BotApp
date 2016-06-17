using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
using System.Text;

namespace Bot_Application1
{
    [Serializable]

    public class EchoDialog : IDialog<object>

    {

        private int count = 1;
        private string frage1 = "Wie möchten Sie Ihre Anlage tätigen ?";
        private string frage2 = "Ihre bisherigen Erfahrungen (seit wenigstens zwei Jahren getätigte Geschäfte)";
        private string frage3 = "Ihre Kenntnisse zur Geldanlage";
        private string frage4 = "Was sind Ihre Anlageziele? ";
        private string frage5 = "Ihre Einstellung zur Wertentwicklung?";
        private string frage6 = "Die geplante Anlage entspricht ..." + "\n\r" + "(Einmalanlage oder Jahresvolumen bei regelm. Sparen)";
        private string frage7 = "Die geplante Anlage beträgt ...  " + "\n\r" + "(Einmalanlage oder Jahresvolumen bei regelm. Sparen. Vermögen = Geld, Wertpapiere, Immobilien etc.abzüglich Kredite, Darlehen, Hypotheken etc.)";

        private string antwort1 = "1.Durch regelmäßiges Sparen (ggf. zusätzliche Ersteinzahlung)" + "\n\r" + "2.Durch eine einmalige Zahlung (ohne Folgezahlungen)";
        private string antwort2 = "1.Ich habe noch nie Wertpapiere gekauft." + "\n\r" + "2.Ich habe bislang nur in festverzinsliche Wertpapiere oder Immobilienfonds investiert." + "\n\r" + "3.Ich habe auch schon in Aktien, Aktienfonds oder gemischte Fonds mit Aktienanteil investiert.";
        private string antwort3 = "1.Ich habe mich bislang nicht mit dem Thema Geldanlage beschäftigt." + "\n\r" + "2.Ich habe bislang nur geringe Kenntnisse, finde das Thema Geldanlage aber interessant." + "\n\r" + "3.Ich kenne mich auch mit Aktien oder Aktienfonds aus. Ich weiß, dass die Ertragsmöglichkeiten dieser Anlageform mit Verlustrisiken einher gehen.";
        private string antwort4 = "1.Ich möchte liquide sein. Meine beabsichtigte Anlagedauer beträgt weniger als zwei Jahre." + "\n\r" + "2.Ich möchte mir bestimmte Wünsche erfüllen (z.B. Anschaffungen, Urlaub)." + "\n\r" + "3.Ich beabsichtige das Geld ca. 5-8 Jahre anzulegen." + "\n\r" + "4.Ich möchte Vermögen aufbauen (z.B. finanzielle Unabhängigkeit, Altersvorsorge, Startkapital für Kinder, Immobilie). Ich beabsichtige das Geld mindestens 8 Jahre anzulegen.";
        private string antwort5 = "1.Ich bin auf eine möglichst gleichmäßige Wertsteigerung meiner Anlage angewiesen. Sicherheit ist mir wichtiger als hohe Ertragserwartungen." + "\n\r" + "2.Für hohe Ertragserwartungen bin ich bereit, gewisse Schwankungsrisiken einzugehen." + "\n\r" + "3.Ich lege das Geld mit dem Ziel einer möglichst hohen Ertragserwartung an. Dafür bin ich auch bereit, hohe Schwankungsrisiken einzugehen.";
        private string antwort6 = "1.nur einem geringen Teil (unter 10%) meines derzeitigen jährlichen Bruttoeinkommens." + "\n\r" + "2.ca. 10% - 30% meines jährlichen Bruttoeinkommens." + "\n\r" + "3.über 30% meines jährlichen Bruttoeinkommens.";
        private string antwort7 = "1.weniger als 30% meines Vermögens." + "\n\r" + "2.ca. 30%-100% meines Vermögens." + "\n\r" + "3.stammt ganz oder teilweise aus Krediten.";
        private string[] Antworte1 = { "1", "2" };
        private int[] alleAnworte = new int[7];
        private string[] fonds = { "Sicherheit", "Ertrag", "Wachstum", "Chance" };

        private int[][] antwortTabelle1 =
        {
            new int [] {3,4,4},
            new int [] {3,4,4},
            new int [] {4,4,4,4},
            new int [] {3,4,4},
            new int [] {4,4,4},
            new int [] {4,3,1}
        };

        private int[][] antwortTabelle2 =
            {
            new int[] { 2,3,4},
            new int[] { 3,3,4},
            new int[] { 1,2,3,4},
            new int[] { 2,3,4},
            new int[] { 4,4,3},
            new int[] {4,3,1}
            };
        private int[][] tempAntwort = new int[6][];

        public async Task StartAsync(IDialogContext context)

        {

            context.Wait(MessageReceivedAsync);

        }


        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<Message> argument)

        {
            int number;
            var message = await argument;

            if (count == 14)

            {
                this.count = 1;
            }

            else


            {

                if (count == 1)
                {

                  //  await context.PostAsync(string.Format("Hallo. Ich bin RoboadvisoryBot und versuche Ihnen helfen, richtige Fonds zu wählen."));
                    await context.PostAsync(string.Format("{1}", this.count++, "Hallo.Ich bin RoboadvisoryBot und versuche Ihnen helfen, richtige Fonds zu wählen."+"\n\r"+"Nach der Fragestellung bekommen Sie Auswahl von Antworte. Wählen Sie bitte richtige Antwort und geben Sie bitte entsprechende Zahl ein." + "\n\r" + "Haben Sie verstanden?" + "\n\r" + "Antworten Sie bitte mit 'Ja' oder 'Nein'"));
                    context.Wait(MessageReceivedAsync);
                }

                else if (count == 2 && message.Text.ToLower() == "ja")
                {


                    await context.PostAsync(string.Format("Gut, jetzt können wir anfangen" + "\n\r"+ frage1));
                    //await context.PostAsync(string.Format(frage1));
                    await context.PostAsync(string.Format("{1}", this.count++, antwort1));
                    context.Wait(MessageReceivedAsync);


                }

                else if (count == 2 && message.Text.ToLower() != "ja")
                {
                    await context.PostAsync(string.Format("Leider, andere Symbole verstehe ich nicht, deswegen sollen Sie damit leben :). Ja?"));
                    context.Wait(MessageReceivedAsync);

                }
                else if (Int32.TryParse(message.Text, out number) || this.count == 10)
                {

                    switch (count)

                    {

                        case 3:

                            if ((Array.Exists(Antworte1, x => x == message.Text)))
                            {
                                if (message.Text == "1")
                                    Array.Copy(antwortTabelle1, 0, tempAntwort, 0, 6);


                                else
                                {

                                    Array.Copy(antwortTabelle2, 0, tempAntwort, 0, 6);

                                }

                                await context.PostAsync(string.Format(frage2));
                                await context.PostAsync(string.Format("{1}", this.count++, antwort2));
                                alleAnworte[0] = int.Parse(message.Text);
                                context.Wait(MessageReceivedAsync);
                            }


                            else if ((Array.Exists(Antworte1, x => x != message.Text)))
                            {
                                await context.PostAsync(string.Format(falscheAntwort()));
                                context.Wait(MessageReceivedAsync);
                            }

                            break;
                        case 4:
                            if (tempAntwort[0].Length >= Int32.Parse(message.Text) && Int32.Parse(message.Text) > 0)
                            {
                                alleAnworte[1] = tempAntwort[0][int.Parse(message.Text) - 1];
                                await context.PostAsync(string.Format(frage3));
                                await context.PostAsync(string.Format("{1}", this.count++, antwort3));
                                context.Wait(MessageReceivedAsync);
                            }
                            else
                            {
                                await context.PostAsync(string.Format(falscheAntwort()));
                                context.Wait(MessageReceivedAsync);
                            }
                            break;

                        case 5:
                            if (tempAntwort[1].Length >= Int32.Parse(message.Text) && Int32.Parse(message.Text) > 0)
                            {
                                alleAnworte[2] = tempAntwort[1][int.Parse(message.Text) - 1];
                                await context.PostAsync(string.Format(frage4));
                                await context.PostAsync(string.Format("{1}", this.count++, antwort4));

                                context.Wait(MessageReceivedAsync);
                            }

                            else
                            {
                                await context.PostAsync(string.Format(falscheAntwort()));
                                context.Wait(MessageReceivedAsync);
                            }
                            break;
                        case 6:
                            if (tempAntwort[2].Length >= Int32.Parse(message.Text) && Int32.Parse(message.Text) > 0)
                            {
                               // await context.PostAsync(tempAntwort[2].Length.ToString());

                                alleAnworte[3] = tempAntwort[2][int.Parse(message.Text) - 1];
                                await context.PostAsync(string.Format(frage5));
                                await context.PostAsync(string.Format("{1}", this.count++, antwort5));

                                context.Wait(MessageReceivedAsync);
                            }
                            else
                            {
                                await context.PostAsync(string.Format(falscheAntwort()));
                                context.Wait(MessageReceivedAsync);
                            }
                            break;
                        case 7:
                            if (tempAntwort[3].Length >= Int32.Parse(message.Text) && Int32.Parse(message.Text) > 0)
                            {
                                alleAnworte[4] = tempAntwort[3][int.Parse(message.Text) - 1];
                                await context.PostAsync(string.Format(frage6));
                                await context.PostAsync(string.Format("{1}", this.count++, antwort6));
                                context.Wait(MessageReceivedAsync);
                            }
                            else
                            {
                                await context.PostAsync(string.Format(falscheAntwort()));
                                context.Wait(MessageReceivedAsync);
                            }

                            break;
                        case 8:
                            if (tempAntwort[4].Length >= Int32.Parse(message.Text) && Int32.Parse(message.Text) > 0)
                            {
                                alleAnworte[5] = tempAntwort[4][int.Parse(message.Text) - 1];
                                await context.PostAsync(string.Format(frage7));
                                await context.PostAsync(string.Format("{1}", this.count ++, antwort7));
                                context.Wait(MessageReceivedAsync);
                            }
                            else
                            {
                                await context.PostAsync(string.Format(falscheAntwort()));
                                context.Wait(MessageReceivedAsync);
                            }

                            break;
                        case 9:
                            if (tempAntwort[5].Length >= Int32.Parse(message.Text) && Int32.Parse(message.Text) > 0)
                            {
                                alleAnworte[6] = tempAntwort[5][int.Parse(message.Text) - 1];
                                await context.PostAsync("Danke für Ihre Interesse an unsere Umfrage");
                                await context.PostAsync(string.Format("{1}", this.count++, "Ihre Fonds der Anlagestrategie ist -" + '"' + passendeFond(fonds, alleAnworte) + '"' + "\n\r"+ "Wenn Sie weitere Interesse haben, kann ich Ihnen Link mit mehr Info über Foms schicken. 'Ja' oder 'Nein'"+count));
                                //await context.PostAsync(string.Format("{1}",count));
                                context.Wait(MessageReceivedAsync);
                                
                            }
                            else

                            {
                                await context.PostAsync(string.Format(falscheAntwort()));
                                context.Wait(MessageReceivedAsync);
                            }
                            break;
                        case 10:
                            {
                                if (message.Text.ToLower() == "ja")
                                {


                                    await context.PostAsync(string.Format("{1}",this.count++,"Danke für Ihre Interesse" + "\n\r" + "http://dems803680.eu.munichre.com/anlegeranalyse/wwwwebs/prod/anlegeranalyse/anlegeranalyse.html"));
                                    
                                }
                                else
                                {
                                    await context.PostAsync(string.Format("{1}",this.count++,"Ok, danke für Ihre Aufmerksamkeit"));
                                    count++;
                                }

                            }
                            break;

                    }
                    //await context.PostAsync(string.Format(ConvertStringArrayToString(alleAnworte)));
                }

                else
                {
                    await context.PostAsync(string.Format("{1}", this.count, "Geben Sie bitte ein zahl"));
                    context.Wait(MessageReceivedAsync);

                }


            }

        }

        // Falsche Eingabe bei Antwort auf der Frage
        public string falscheAntwort()
        {
            return ("Diese Antwort habe ich nicht");
        }
        public static string ConvertStringArrayToString(int[] array)
        {
            //
            // Concatenate all the elements into a StringBuilder.
            //
            StringBuilder builder = new StringBuilder();
            foreach (int value in array)
            {
                builder.Append(value);
                builder.Append('.');
            }
            return builder.ToString();
        }

        //Richtige Fond als antwort ausgeben
        private static string passendeFond(string[] arrey1, int[] arrey2)
        {
            int[] lokalArrey = new int[6];
            Array.Copy(arrey2, 1, lokalArrey, 0, 6);
            return arrey1[lokalArrey.Min() - 1];
        }


        //Überprüffen on ein Int oder String
        private static bool ZahlOderNicht(string a)
        {
            return (Char.IsNumber(System.Convert.ToChar(a)));

        }


        public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)

        {

            var confirm = await argument;

            if (confirm)

            {

                this.count = 1;

                await context.PostAsync("Reset count.");

            }

            else

            {

                await context.PostAsync("Did not reset count.");

            }

            context.Wait(MessageReceivedAsync);

        }

    }




    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {

            if (message.Type == "Message")

            {

                // return our reply to the user

                return await Conversation.SendAsync(message, () => new EchoDialog());

            }

            else

            {

                return HandleSystemMessage(message);

            }


        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}