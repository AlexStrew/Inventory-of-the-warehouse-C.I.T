using Inventarisation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventarisation.ViewModel
{

    public class NomenclatureVM
    {
        private static Core db = new Core();

        /// <summary>
        /// Проверка на ввод в TextBox
        /// </summary>
        public bool CheckAddNomenclature(string nameDevice)
        {
            if (String.IsNullOrEmpty(nameDevice))
            {
                throw new Exception("Вы не ввели название устройства.");
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Добавление устройства в БД
        /// </summary>
        /// <param name="nameDevice">Название устройства</param>
        public void AddNomenclature(string nameDevice)
        {
            Nomenclature newNomenclature = new Nomenclature()
            {
                name_device = nameDevice,
                
            };
            db.context.Nomenclature.Add(newNomenclature);
            db.context.SaveChanges();
        }
           
    }
}
