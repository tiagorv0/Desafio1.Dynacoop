using Microsoft.Xrm.Tooling.Connector;

namespace Dynacoop.Desafio2
{
    public class Singleton
    {
        public static CrmServiceClient GetService()
        {
            var url = "https://org2607c67f.crm2.dynamics.com/";
            var clientId = "08103a4c-8095-49f4-9472-4f352935610b";
            var clientSecret = "v_U8Q~DzR7ecg3tQFgPpSOWwxOaCwjmi4Tr81bie";

            return new CrmServiceClient($"AuthType=ClientSecret;url={url};AppId={clientId};ClientSecret={clientSecret};");
        }
    }
}
