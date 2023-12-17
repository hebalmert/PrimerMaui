using System.Net.Http.Json;
using System.Text.Json;

namespace MyWeb.Clases
{
    public class ClienteHttp
    {
        public static async Task<List<T>> GetAll<T>(string urlBase, string rutaApi, string token="")
        {
            try
            {
                var Client = new HttpClient();
                Client.BaseAddress = new Uri(urlBase);
                //Agregamos al header del Client el Token de seguridad
                if (token != "")Client.DefaultRequestHeaders.Add("token", token);
                string cadena = await Client.GetStringAsync(rutaApi);

                List<T> lista = JsonSerializer.Deserialize<List<T>>(cadena)!;

                return lista;
            }
            catch (Exception ex)
            {

                return new List<T>();
            }

        }

        public static async Task<T> Get<T>(string urlBase, string rutaApi, string token = "")
        {
            try
            {
                var Client = new HttpClient();
                Client.BaseAddress = new Uri(urlBase);
                //Agregamos al header del Client el Token de seguridad
                if (token != "") Client.DefaultRequestHeaders.Add("token", token);
                string cadena = await Client.GetStringAsync(rutaApi);

                T lista = JsonSerializer.Deserialize<T>(cadena)!;

                return lista;
            }
            catch (Exception ex)
            {

                return (T)Activator.CreateInstance(typeof(T))!;
            }

        }

        public static async Task<int> GetInt(string urlBase, string rutaApi)
        {
            try
            {
                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri(urlBase);
                string cadena = await Client.GetStringAsync(rutaApi);

                return int.Parse(cadena);
            }
            catch (Exception ex)
            {

                return 0;
            }

        }

        public static async Task<int> Delete(string urlBase, string rutaApi, string token = "")
        {
            try
            {
                var Client = new HttpClient();
                Client.BaseAddress = new Uri(urlBase);
                //Agregamos al header del Client el Token de seguridad
                if (token != "") Client.DefaultRequestHeaders.Add("token", token);
                var response = await Client.DeleteAsync(rutaApi);
                if (response.IsSuccessStatusCode)
                {
                    string cadena = await response.Content.ReadAsStringAsync();
                    return int.Parse(cadena);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }

        }

        public static async Task<int> Post<T>(string urlBase, string rutaApi, T obj, string token = "")
        {
            try
            {
                var Client = new HttpClient();
                Client.BaseAddress = new Uri(urlBase);
                //Agregamos al header del Client el Token de seguridad
                if (token != "") Client.DefaultRequestHeaders.Add("token", token);
                var response = await Client.PostAsJsonAsync<T>(rutaApi, obj);
                if (response.IsSuccessStatusCode)
                {
                    string cadena = await response.Content.ReadAsStringAsync();
                    return int.Parse(cadena);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }

        }

        public static async Task<List<T>> PostList<T>(string urlBase, string rutaApi, T obj, string token = "")
        {
            try
            {
                var Client = new HttpClient();
                Client.BaseAddress = new Uri(urlBase);
                //Agregamos al header del Client el Token de seguridad
                if (token != "") Client.DefaultRequestHeaders.Add("token", token);
                var response = await Client.PostAsJsonAsync<T>(rutaApi, obj);
                if (response.IsSuccessStatusCode)
                {
                    string cadena = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<T>>(cadena)!;
                }
                else
                {
                    return new List<T>();
                }
            }
            catch (Exception ex)
            {

                return new List<T>();
            }

        }

    }
}
