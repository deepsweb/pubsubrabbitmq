using System;
using RabbitMQ.Client;
using System.Text;

class Send
{
    public static void Main()
    {

        ConnectionFactory factory = new ConnectionFactory();
        factory.UserName = "deep";
        factory.Password = "sita2017";
        factory.VirtualHost = "/";
        //factory.AmqpUriSslProtocols = Protocols.FromEnvironment();
        factory.HostName = "52.224.128.255";
        factory.Port = AmqpTcpEndpoint.UseDefaultPort;
       // IConnection conn = factory.CreateConnection();
        //var factory = new ConnectionFactory() 
        //{ 
        //    HostName = "amqp://deep:sita2017@52.224.128.255:5672/"
        //};

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {

            //channel.QueueDeclare(queue: "hello",
            //                     durable: false,
            //                     exclusive: false,
            //                     autoDelete: false,
            //                     arguments: null);

            channel.ExchangeDeclare("PaxCheck", "fanout", false, true, null);

            string message = "";

            while (message != "end")
            {
                message = Console.ReadLine();


                var body = Encoding.UTF8.GetBytes(message);


                channel.BasicPublish(exchange: "PaxCheck",
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0} type end to exit", message);


            }

            
            channel.Close();
            //connection.Close();
        }

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
        
    }
}