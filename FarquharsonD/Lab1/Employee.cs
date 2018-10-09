using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    /// <summary>
    /// Employee class has qualities entered of name, number, rate, and hours
    /// it also contains calculates gross pay in the constructor
    /// </summary>
    class Employee
    {
        private string _name;
        public string Name { get => _name; set => _name = value; }

        private int _number;
        public int Number {
            get { return _number; }
            set { _number = value; }
        }
        private decimal _rate;
        public decimal Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }
        private double _hours;
        public double Hours { get => _hours; set => _hours = value; }

        private decimal _gross { get; }
        public decimal Gross { get => _gross; }

        /// <summary>
        /// constructor used in Employees class also used to call calcGross method and assign the value
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="number">number</param>
        /// <param name="rate">pay rate</param>
        /// <param name="hours">hours</param>
        public Employee(string name, int number, decimal rate, double hours)
        {
            _name = name;
            _number = number;
            _rate = rate;
            _hours = hours;
            _gross = calcGross(_rate, _hours);
        }

        public Employee()
        {
        }
        /// <summary>
        /// calculates gross pay by taking hours and multiplying everything over 40 by 1.5
        /// and then multiplying that by the rate and returns this
        /// </summary>
        /// <param name="rateIn">rate</param>
        /// <param name="hoursIn">hours</param>
        /// <returns>gross</returns>
        public decimal calcGross(decimal rateIn, double hoursIn)
        {
            var fullHours = hoursIn;
            if (fullHours > 40)
            {
                fullHours -= 40;
                fullHours *= 1.5;
                fullHours += 40;
            }
            return (decimal)fullHours * rateIn;
        }

        public override string ToString()
        {
            return string.Format("{0,-20}      {1,6}       {2,5}       {3,5:0.00}       {4,10:0.00}", _name,_number,_rate,_hours,_gross);
        }
    }
}
