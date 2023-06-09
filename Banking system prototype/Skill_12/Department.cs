using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill_12
{
    public class Department
    {

        /// <summary>
        /// Название департамента
        /// </summary>
        public string DepartamentName { get; set; }
        /// <summary>
        /// Номер Департамента
        /// </summary>
        public int DepartamentID { get; set; }


        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="DepartamentName"></param>
        /// <param name="DepartamentID"></param>
        public Department(string DepartamentName, int DepartamentID)
        {
            this.DepartamentName = DepartamentName;
            this.DepartamentID = DepartamentID;
        }
        /// <summary>
        /// Визуальный вывод
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{DepartamentName}";
        }
    }
}