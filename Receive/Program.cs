using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

class Receive
{
    public static void Main()
    {
        ConnectionFactory factory = new ConnectionFactory();
        factory.UserName = "deep";
        factory.Password = "sita2017";
        factory.VirtualHost = "/";
        factory.HostName = "52.224.128.255";
        factory.Port = AmqpTcpEndpoint.UseDefaultPort;

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {

            var queueName = channel.QueueDeclare().QueueName;
            //channel.QueueDeclare(queue: "hello",
            //                     durable: false,
            //                     exclusive: false,
            //                     autoDelete: false,
            //                     arguments: null);

            channel.QueueBind(queue: queueName,
                                exchange: "PaxCheck",
                                routingKey: "");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
            //channel.Close();
            //connection.Close();
        }
    }
}