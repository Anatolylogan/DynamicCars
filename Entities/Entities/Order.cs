using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        private static int _idCounter = 0;
        public int OrderID { get; set; }
        public List<Mat> Mats { get; set; } = new List<Mat>();
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int MakerId { get; set; }
        public Maker Maker { get; set; }
        public string Status { get; set; }
        public List<(string color, string carBrand)> MatChoices { get; set; } = new List<(string, string)>();
    }
}

