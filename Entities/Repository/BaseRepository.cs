using System.Text.Json;
using Domain.Entities;
using System.IO;

namespace Infrastructure.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : Entity
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
                LogError($"Ошибка при чтении файла {_filePath}: {ex.Message}");
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
                LogError($"Ошибка при записи в файл {_filePath}: {ex.Message}");
            }
        }
        private void LogError(string message)
        {
            Console.WriteLine($"[Error] {message}");
        }
        public List<T> GetAll()
        {
            return Items;
        }
        public T? GetById(int id)
        {
            return Items.FirstOrDefault(item => item.Id == id);
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
        public void Update(T item)
        {
            var existingItem = Items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                var index = Items.IndexOf(existingItem);
                Items[index] = item;
                SaveToFile();
            }
        }
    }
}
