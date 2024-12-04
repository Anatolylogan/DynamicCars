using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class MakerRepository : BaseRepository<Maker>
    {
        public MakerRepository(string filePath) : base(filePath) { }

        public Maker GetByName(string name)
        {
            return Items.Find(maker => maker.Name == name);
        }
    }

}
