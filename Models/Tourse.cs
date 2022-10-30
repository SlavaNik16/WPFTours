using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTours
{
    public class Tourse : ICloneable
    {
        /// <summary>
        /// Направление
        /// </summary>
        public Direction direction { get; set; }
        /// <summary>
        /// Дату вылета
        /// </summary>
        public DateTime DateDeparture { get; set; }
        /// <summary>
        /// Количество ночей
        /// </summary>
        public int NumberNight { get; set; }
        /// <summary>
        /// Стоимость за отдыхающего(руб).
        /// </summary>
        public decimal CostVac { get; set; }
        /// <summary>
        /// Количество отдыхающих
        /// </summary>
        public int NumberVac { get; set; }
        /// <summary>
        /// Наличие Wi-Fi
        /// </summary>
        public bool Wi_Fi { get; set; }
        /// <summary>
        /// Доплаты(руб).
        /// </summary>
        public decimal Surcharges { get; set; }
        /// <summary>
        /// Общая стоимость
        /// </summary>
        public decimal TotalAmount { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
