using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.ServiceBus.Messaging;
using System.Threading;
using System.Runtime.Serialization;

namespace NAVAzureServiceBus
{
    class Program
    {        
        static void Main(string[] args)
        {
            string ServiceBusConnectionString = "Endpoint=sb://navsbqueue-ns.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=GD2cxyENOHyATwzFVAK0bF9AVJjDB+n42i6wZFkVhXI=";

            string QueueName = "NAVSBQueue";

            var client = QueueClient.CreateFromConnectionString(ServiceBusConnectionString, QueueName);

            BrokeredMessage message = null;

            //INVIO OGGETTO ALLA CODA DEL SERVICE BUS

            NAVInterface NAV = new NAVInterface();


            //Messaggio di testo
            //message = new BrokeredMessage("TEST MESSAGE");
            //client.Send(message);

            Classes.NAVOrder order = NAV.GetNAVOrder();

            message = new BrokeredMessage(order, new DataContractSerializer(typeof(Classes.NAVOrder)));

            client.Send(message);




            //RICEZIONE MESSAGGIO DALLA CODA DEL SERVICE BUS

            
            Console.WriteLine("\nReceiving message from Queue…");
            

            while (true)
            {
                try
                {
                    //receive messages from Queue 
                    message = client.Receive(TimeSpan.FromSeconds(5));

                    if (message != null)
                    {
                        //Ricezione messaggio di testo
                        //Console.WriteLine(string.Format("Message received: Id = {0}, Body = {1}", message.MessageId, message.GetBody<string>()));

                        //Ricezione oggetto Ordine
                        Console.WriteLine(string.Format("Message received: Id = {0} ", message.MessageId));
                        Classes.NAVOrder orderReceived = message.GetBody<Classes.NAVOrder>(new DataContractSerializer(typeof(Classes.NAVOrder)));
                        // Further custom message processing could go here… 
                        message.Complete();
                    }
                    else
                    {
                        //no more messages in the queue 
                        break;
                    }
                }
                catch (MessagingException e)
                {
                    if (!e.IsTransient)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                    else
                    {
                        HandleTransientErrors(e);
                    }
                }
            }

            Console.ReadLine();
        }

        private static void HandleTransientErrors(MessagingException e)
        {
            //If transient error/exception, let’s back-off for 2 seconds and retry 
            Console.WriteLine(e.Message);
            Console.WriteLine("Will retry sending the message in 2 seconds");
            Thread.Sleep(2000);
        }
    }
}
