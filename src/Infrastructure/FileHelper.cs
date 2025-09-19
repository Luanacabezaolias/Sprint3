using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Challenge_sprint.src.Infrastructure
{
    public static class FileHelper
    {
        public static async Task SaveJsonAsync<T>(string path, T data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(path, json);
        }

        public static async Task<T?> ReadJsonAsync<T>(string path)
        {
            if (!File.Exists(path)) return default;
            var json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<T>(json);
        }

        public static async Task SaveTxtAsync(string path, string content)
        {
            await File.WriteAllTextAsync(path, content);
        }

        public static async Task<string> ReadTxtAsync(string path)
        {
            if (!File.Exists(path)) return string.Empty;
            return await File.ReadAllTextAsync(path);
        }
    }
}
