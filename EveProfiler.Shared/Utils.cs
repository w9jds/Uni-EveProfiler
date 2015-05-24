using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;

namespace EveProfiler.Shared
{
    public class Utils
    {
        public static object Console { get; private set; }

        public static async Task SaveSerializedToLocalFile(string filename, string content)
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, content);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static async Task<string> GetSerializedFromLocalFile(string filename)
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                return await FileIO.ReadTextAsync(file);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return string.Empty;
            }
        }
    }
}
