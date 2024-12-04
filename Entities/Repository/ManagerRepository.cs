using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    namespace Domain.Repository
    {
    public class ManagerRepository : BaseRepository<Manager>
    {
        public ManagerRepository(string filePath) : base(filePath) { }

        public Manager GetByName(string name)
        {
            return Items.Find(manager => manager.Name == name);
        }
    }

}