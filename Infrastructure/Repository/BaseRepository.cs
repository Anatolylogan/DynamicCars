using System.Text.Json;
using Domain.Entities;
using Domain.Сontracts;

namespace Infrastructure.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly string _filePath;
        protected List<T> Items;

        public BaseRepository(string filePath)
        {
            _filePath = filePath;
            Items = LoadFromFile();
        }

        private List<T> LoadFromFile()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    File.WriteAllText(_filePath, "[]");
                    return new List<T>();
                }

                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла {_filePath}: {ex.Message}");
                return new List<T>();
            }
        }

        private void SaveToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(Items, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи файла {_filePath}: {ex.Message}");
            }
        }

        public List<T> GetAll()
        {
            return Items;
        }

        public void Add(T item)
        {
            Items.Add(item);
            SaveToFile();
        }

        public void Remove(T item)
        {
            Items.Remove(item);
            SaveToFile();
        }

        public T? GetById(int id)
        {
            return Items.FirstOrDefault(i => (i as Entity)?.Id == id);
        }

        public void Update(T item)
        {
            var existingItem = Items.FirstOrDefault(i => (i as Entity)?.Id == (item as Entity)?.Id);
            if (existingItem != null)
            {
                Items.Remove(existingItem);
                Items.Add(item);
                SaveToFile();
            }
        }
    }
}
