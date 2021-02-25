using RabbitMQ.Client;
using System.Linq;
using System.Text;

namespace WebApiLV.Eventos
{
    public class ComandoDispara<T>
    {
        public static void Dispara(Envio<T> envio)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        for (int i = 0; i < envio.IdColas.Count(); i++)
                        {
                            string cola = Cola.Colas.First(x => x.ColaId == envio.IdColas[i]).Nome;

                            channel.QueueDeclare(cola, false, false, false, null);

                            string message = envio.MSG;

                            var body = Encoding.UTF8.GetBytes(message);

                            channel.BasicPublish("", cola, null, body);
                        }

                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}