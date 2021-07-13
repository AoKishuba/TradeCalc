using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeCalc
{
    class Profit
    {
        decimal _profitPerSquare;
        public string Name { get; set; }
        public decimal ProfitPerSquare
        {
            get => _profitPerSquare;
            set
            {
                _profitPerSquare = value;

                // Format ID to match columns
                ID = Name;
                for (int i = 0; i < 35 - Name.Length - _profitPerSquare.ToString("#,##0.00").Length; i++)
                {
                    ID += " ";
                }
                ID += _profitPerSquare.ToString("#,##0.00");
            }
        }
        public string ID { get; set; }
    }
}
