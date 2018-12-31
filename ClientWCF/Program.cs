using System;
using System.Net;
using System.Net.Security;
using System.ServiceModel;
using ClientWCF.ServiceReference1;
using sysnova.Infrastructure.Errors;
using System.ServiceModel.Security;

namespace ClientWCF
{
    class Program
    {
        private static void Main(string[] args)
        {

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(
                 delegate { return true; });

            var client = new Service1Client();
            client.ClientCredentials.UserName.UserName = "technisys";
            client.ClientCredentials.UserName.Password = "technisys1";
            try
            {             
                var result = client.GetCategories(4)[0];

                //var cloud = client.DoAddCloud();

                //var help = client.DoAddHelpDesk();

                Console.WriteLine(result);
                //Console.WriteLine(cloud);
                //Console.WriteLine(help);

                client.Close();
                //Console.Read();
            }
            catch (FaultException<GlobalErrorDetails> e)
            {
                Console.WriteLine(e.Detail.Message);
                Console.Read();
            }
            catch (FaultException e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }
            catch (MessageSecurityException e)
            {
                Console.WriteLine(e.InnerException.Message);
                Console.Read();
            }

        }

    }
}
