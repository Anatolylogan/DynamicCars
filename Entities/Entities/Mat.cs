using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Mat
    {
        public static int nextMatId = 1;
        public int MatId { get; set; }
        public string Color { get; set; }
        public string CarBrand { get; set; }
        public Mat(int id, string color, string carBrand)
        {
            MatId = id;
            Color = color;
            CarBrand = carBrand;
        }
    }
}