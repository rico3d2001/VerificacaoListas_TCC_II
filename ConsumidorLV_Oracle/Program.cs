using ConsumidorLV_Oracle.Comandos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ConsumidorLV_Oracle
{
    class Program
    {
        static void Main() //string[] args)
        {

            string cola = "Cola2"; //Convert.ToString(args[0]);

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

                        CmdsOraConfirmacaoRevisao.Confirma(message);

                        //Console.WriteLine("Recebida {0}", message);
                        Console.WriteLine("Confirmado");
                    };

                    channel.BasicConsume(cola, true, consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();

                }
            }

        }
    }
}
