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
        private static int _idCounter = 0;

        public int MatId { get; set; }
        public string Color { get; set; }
        public string CarBrand { get; set; }
    }
}