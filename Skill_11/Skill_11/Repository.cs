using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Skill_11
{
    class Repository
    {
        string pathClients = "clients.json";
        string pathDepartments = "departments.json";
        List<Client> clients = new List<Client>();
        List<Department> departments = new List<Department>();
       
        /// <summary>
        /// Получает информацию о клиентах из json
        /// </summary>
        /// <returns>Лист клиентов из json</returns>
        public List<Client> GetClientsInfoFromJson()
        {
            
            List<Client> tempList = new List<Client>();
            if (File.Exists(pathClients))
            {
                string JsonClients = File.ReadAllText(pathClients);
                tempList = JsonConvert.DeserializeObject<List<Client>>(JsonClients);
            }
            return tempList;
        }
        /// <summary>
        /// Получает информацию о департаментах из json
        /// </summary>
        /// <returns>Лист департаментов из json</returns>
        public List<Department> GetDepartmentsInfoFromJson()
        {

            List<Department> tempList = new List<Department>();
            if (File.Exists(pathDepartments))
            {
                string JsonDepartments = File.ReadAllText(pathDepartments);
                tempList = JsonConvert.DeserializeObject<List<Department>>(JsonDepartments);
            }
            return tempList;
        }
        /// <summary>
        /// Сохраняет информацию о клиентах в json
        /// </summary>
        /// <param name="tempList"></param>
        public void SaveClientsToJson(List<Client> tempList)
        {
            string jsonSer;
            jsonSer = JsonConvert.SerializeObject(tempList);
            File.WriteAllText(pathClients, jsonSer);
        }
        /// <summary>
        /// Сохраняет информацию о департаментах в json
        /// </summary>
        /// <param name="tempList"></param>
        public void SaveDepartmentsToJson(List<Department> tempList)
        {
            string jsonSer;
            jsonSer = JsonConvert.SerializeObject(tempList);
            File.WriteAllText(pathDepartments, jsonSer);
        }

        
    }
}
