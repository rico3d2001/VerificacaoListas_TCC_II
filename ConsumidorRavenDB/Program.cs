using EntidadesRepositoriosLeitura;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumidorRavenDB
{
    class Program
    {
        static void Main()//string[] args)
        {
            string cola = "Cola1"; //Convert.ToString(args[0]);

            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(cola, false, false, false, null);

                    var consumer = new EventingBasicConsumer(channel);

                    channel.BasicConsume(cola, true, consumer);

                    Console.WriteLine("Esperando as mensagens, Crtl + c para sair...");

                    consumer.Received += (model, ea) =>
                    {
                        var message = Encoding.UTF8.GetString(ea.Body);



                        CmdAcrescimoRevisaoRV.Acrescenta(message);

                        Console.WriteLine("Recebida {0}", message);
                    };

                    channel.BasicConsume(cola, true, consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();

                }
            }

        }
    }
}
