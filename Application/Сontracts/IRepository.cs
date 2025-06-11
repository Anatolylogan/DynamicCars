namespace Application.Сontracts
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T? GetById(int id);
        void Add(T item);
        void Remove(T item);
        void Update(T item);
    }
}

