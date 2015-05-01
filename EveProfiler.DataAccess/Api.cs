using EveProfiler.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EveProfiler.DataAccess
{
    public class Api
    {
        public static void getCharacterList(Account account, Action<object> response)
        {
            getCharacterList(account.Keys, response);
        }

        public static void getCharacterList(Dictionary<string, string> keys, Action<object> response)
        {
            Core.HttpHelper.get(@"/account/Characters.xml.aspx", keys, new Action<HttpResponseMessage>(tResponse =>
            {
                if (tResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    tResponse.Content.ReadAsStringAsync().ContinueWith(t =>
                    {
                        return cParse.parseCharacterList(t.Result);
                    }).ContinueWith(t => aResult(t.Result));
                }
            }));
        }
    }
}
