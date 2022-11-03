using SAP_Interface_DeliveryNum;
using SCGP.COA.BUSINESSLOGIC.Services.Interface;
using SCGP.COA.COMMON.Attributes;
using SCGP.COA.COMMON.Models;
using SCGP.COA.COMMON.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCGP.COA.BUSINESSLOGIC.Services
{
    [TransientPriorityRegistration]
    public class SAPService : ISAPService
    {
        private ChannelFactory<SI_DeliveryInquiry_OS> DeliveryInquiry()
        {
            //Uri SAPPIServer = new Uri("https://swqwd.scg.com/XISOAPAdapter/MessageServlet?senderParty=&senderService=BS_PAPER_EDN_Q&receiverParty=&receiverService=&interface=SI_DeliveryInquiry_OS&interfaceNamespace=urn:scg.co.th:PAPER:EDN:Delivery");
            //EndpointAddress endpointAddress = new EndpointAddress(SAPPIServer);
            var endpointAddress = SI_DeliveryInquiry_OSClient.GetEndpointAddress(SI_DeliveryInquiry_OSClient.EndpointConfiguration.Default);        
            BasicHttpBinding binding = new BasicHttpBinding
            {
                Name = "basicHttpBinding",
                AllowCookies = false,
                BypassProxyOnLocal = false,
                MaxBufferSize = Int32.MaxValue,
                MaxReceivedMessageSize = Int32.MaxValue,
                MaxBufferPoolSize = Int32.MaxValue,
                OpenTimeout = TimeSpan.FromMinutes(5),
                ReceiveTimeout = TimeSpan.FromMinutes(5),
                SendTimeout = TimeSpan.FromMinutes(5),
            };

            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            var channelFactory = new ChannelFactory<SI_DeliveryInquiry_OS>(binding, endpointAddress);
            channelFactory.Credentials.UserName.UserName = "PPGPIEDN";
            channelFactory.Credentials.UserName.Password = "Piedn-01";

            //channelFactory.Endpoint.EndpointBehaviors.Add(new SOAPEndpointBehavior(new LoggingMessageInspector(unit, targetId, AppConfig.Kafka.TopicLocation, kafkaOffset)));
            return channelFactory;
        }
        public async Task<SI_DeliveryInquiry_OSResponse> CallSAPDeliveryInquiry(SI_DeliveryInquiry_OSRequest request)
        {
            using (var channelFactory = DeliveryInquiry())
            {
                try
                {
                    var channel = channelFactory.CreateChannel();
                    return await channel.SI_DeliveryInquiry_OSAsync(request);

                }
                catch (TimeoutException ex)
                {
                    throw new Exception(string.Format("Found timeout for fetch information. Exception : {0}", ex.Message));
                }
                catch (WebException ex)
                {
                    var error = ex.Response;
                    using (var stream = error.GetResponseStream())
                    {
                        var reader = new StreamReader(stream, Encoding.UTF8);
                        var responseString = reader.ReadToEnd();
                        throw new Exception(string.Format("Found web exception : {0}", responseString));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error Message : {0}", ex.Message));
                }
            }
        }

    }
}
