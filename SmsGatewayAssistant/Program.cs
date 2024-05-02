using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmsGatewayAssistant
{
    class Program
    {
        static void Main(string[] args)
        {
            string apiUrl = "https://entsms.microntechbd.com:8080/bulksms/personalizedbulksms";
            string username = "MTLBilling";
            string password = "12345678";
            string destinationNumber = "8801779747913"; // The fixed number you want to send SMS to
            string source = "8809601000201"; // Your source number or name
            string message = "Hellooo123"; // Your message

            try
            {
                SendSMS(apiUrl, username, password, destinationNumber, source, message);
                Console.WriteLine("SMS Sent!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send SMS. Error: {ex.Message}");
            }
            Console.ReadLine();
        }

        
        static void SendSMS(string apiUrl, string username, string password, string destinationNumber, string source, string message)
        {
            using (var client = new WebClient())
            {
                var queryString = $"?username={username}&password={password}&type=0&dlr=1&destination={destinationNumber}&source={source}&message={message}";
                var response = client.DownloadString(apiUrl + queryString);
                Console.WriteLine(response);
                // You can check the response here if needed
            }
        }
    }
}
