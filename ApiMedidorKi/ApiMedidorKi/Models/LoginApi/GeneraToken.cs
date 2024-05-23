using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiMedidorKi.Models.LoginApi
{
    public class GeneraToken
    {
        public static string ApiUrl { get; set; }

        public async Task<Token> Generar(string usuario, string contrasenia)
        {
            try
            {                
                ApiUrl = ConfigurationManager.AppSettings["APIURL"].ToString();
                Token token = new Token();

                using (HttpClient client = new HttpClient())
                {
                    ApiUrl += "token";

                    var form = new Dictionary<string, string>
                    {
                        {"grant_type", "password"},
                        {"username", usuario},
                        {"password", contrasenia}                        
                    };

                    client.BaseAddress = new Uri(ApiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsync(ApiUrl, new FormUrlEncodedContent(form));

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonPuro = await response.Content.ReadAsStringAsync();
                        var jsonDesarializado = JsonConvert.DeserializeObject(jsonPuro);

                        token = JsonConvert
                                 .DeserializeObject<Token>(jsonPuro.ToString()
                                 , new JsonSerializerSettings()
                                 {
                                     MissingMemberHandling =
                                         MissingMemberHandling.Ignore
                                 });

                        token.Estado = true;
                    }
                    else
                    {
                        var dato = await response.Content.ReadAsStringAsync();
                        token = JsonConvert
                                .DeserializeObject<Token>(dato.ToString()
                                , new JsonSerializerSettings()
                                {
                                    MissingMemberHandling =
                                        MissingMemberHandling.Ignore
                                });

                        token.Estado = false;
                    }
                }

                return token;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}