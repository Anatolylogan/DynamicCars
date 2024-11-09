using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class MakerRepository
    {
        private readonly List<Maker> makers = new List<Maker>();
        public void Add(Maker maker)//Добавление изготовителя
        {
            makers.Add(maker);
        }
        public Maker GetById(int id)
        {
            return makers.FirstOrDefault(m => m.MakerId == id);
        }
        public List<Maker> GetAll()
        {
            return makers;
        }
    }
}
