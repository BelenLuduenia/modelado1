using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace modelado1.Client.Servicios
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient httpClient;

        public HttpService(HttpClient http)
        {
            this.httpClient = http;
        }

        public async Task<HttpRespuesta<T>> Get<T>(string url)
        {
            var respuestaHttp = await httpClient.GetAsync(url);
            if (respuestaHttp.IsSuccessStatusCode)
            {

                var respuesta = await DeserializarRespuesta<T>(respuestaHttp);
                return new HttpRespuesta<T>(respuesta, false, respuestaHttp);

            }
            else
            {
                return new HttpRespuesta<T>(default, true, respuestaHttp);

            }

        }

        public async Task<HttpRespuesta<object>> Post<T>(string url, T enviar)
        {
            try
            {
                var enviarJSON = JsonSerializer.Serialize(enviar);
                var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");


                var respuestaHTTP = await httpClient.PostAsync(url, enviarContent);

                return new HttpRespuesta<object>(null, !respuestaHTTP.IsSuccessStatusCode, respuestaHTTP);


            }
            catch (System.Exception e)
            {

                throw;
            }


        }


        public async Task<HttpRespuesta<object>> Put<T>(string url, T enviar)
        {


            try
            {
                var enviarJSON = JsonSerializer.Serialize(enviar);
                var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");


                var respuestaHTTP = await httpClient.PutAsync(url, enviarContent);

                return new HttpRespuesta<object>(null, !respuestaHTTP.IsSuccessStatusCode, respuestaHTTP);


            }
            catch (System.Exception e)
            {

                throw;
            }


        }


        public async Task<HttpRespuesta<object>> Delete(String url)
        {
            var respuestaHTTP = await httpClient.DeleteAsync(url);
            return new HttpRespuesta<object>(null, !respuestaHTTP.IsSuccessStatusCode, respuestaHTTP);
        }



        private async Task<T> DeserializarRespuesta<T>(HttpResponseMessage httRespuesta)
        {
            var RespuestaString = await httRespuesta.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(RespuestaString, new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

        }

    }
}
