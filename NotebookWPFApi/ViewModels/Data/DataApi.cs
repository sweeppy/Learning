using Newtonsoft.Json;
using NotebookWPFApi.Authorization;
using NotebookWPFApi.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotebookWPFApi.Data
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

        public async Task<HttpStatusCode> Login(string userName, string userPassword)
        {
            string url = @"https://localhost:44317/api/values/Login";

            UserLogin model = new UserLogin
            {
                LoginProp = userName,
                Password = userPassword
            };

            var response = await httpClient.PostAsJsonAsync(url, model);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var message = await response.Content.ReadAsStringAsync();
                MessageBox.Show(message);
                return HttpStatusCode.BadRequest;
            }
            else
            {
                var t = response.EnsureSuccessStatusCode();
                return t.StatusCode;
            }

        }

        public async Task<List<string>> GetUserRoles(string userName)
        {
            string url = @"https://localhost:44317/api/values/GetUser/" + userName;

            var response = httpClient.GetAsync(url).Result;
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                MessageBox.Show(await response.Content.ReadAsStringAsync());
                return null;
            }

            string json = httpClient.GetStringAsync(url).Result;



            return JsonConvert.DeserializeObject<List<string>>(json);
        }

        public async void Registration(UserRegistration model)
        {
            string url = @"https://localhost:44317/api/values/Registration";

            var response = await httpClient.PostAsJsonAsync(url, model);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var message = await response.Content.ReadAsStringAsync();
                MessageBox.Show(message);
            }
            else
            {
                response.EnsureSuccessStatusCode();
            }
        }


    }
}
