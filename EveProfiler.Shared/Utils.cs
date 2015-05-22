using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace EveProfiler.Shared
{
    public class Utils
    {
        public static async Task SaveSerializedToLocalFile(string filename, string content)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, content);
        }

        public static async Task<string> GetSerializedFromLocalFile(string filename)
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
            return await FileIO.ReadTextAsync(file);
        }
    }
}
