using Newtonsoft.Json;
using Notebook.Models;
using System.Text;

namespace Notebook.Data
{
    public class DataApi
    {
        private HttpClient httpClient { get; set; }

        public DataApi()
        {
            httpClient = new HttpClient();
        }
        /// <summary>
        /// Получение всех клиентов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Client> GetClients()
        {
            string url = @"https://localhost:44317/api/values";

            string json = httpClient.GetStringAsync(url).Result;

            var result = JsonConvert.DeserializeObject<IEnumerable<Client>>(json);

            return result;
        }
        /// <summary>
        /// Получение конкретного клиента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Client GetClientById(int? id)
        {
            string url = @"https://localhost:44317/api/values/" + id;

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<Client>(json);
        }
        /// <summary>
        /// Добавление клмента
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Client client)
        {
            string url = @"https://localhost:44317/api/values";

            var r = httpClient.PostAsync(requestUri: url,
                                         content: new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8,
                                         mediaType: "application/json")).Result;
        }
        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="id"></param>
        public void DeleteClient(int? id)
        {
            string url = @"https://localhost:44317/api/values/" + id;

            var r = httpClient.DeleteAsync(url).Result;
        }
        /// <summary>
        /// Обновление и сохранение измененной информации о клиенте
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public async Task FinishEdit(Client client)
        {
            string url = @"https://localhost:44317/api/values";


            HttpResponseMessage response = await httpClient.PutAsJsonAsync<Client>(url, client);
            response.EnsureSuccessStatusCode();
        }


    }
}
