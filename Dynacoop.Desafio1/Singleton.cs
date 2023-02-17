
using Microsoft.Xrm.Tooling.Connector;

namespace Dynacoop.Desafio1
{
    public class Singleton
    {
        public static CrmServiceClient GetService()
        {
            var url = "https://org2607c67f.crm2.dynamics.com/";
            var clientId = "";
            var clientSecret = "";

            return new CrmServiceClient($"AuthType=ClientSecret;url={url};AppId={clientId};ClientSecret={clientSecret};");
        }
    }
}
