using System;
using System.IO;
using System.Runtime.Serialization;
using Windows.Storage;

namespace EveProfiler.Classes
{
    public class StorageSerialization
    {
        // TODO: Rewrite using Netwonsoft JSON serializer
        public static void WriteFileAsync(Type type, string fileName, object serializeItem)
        {
            DataContractSerializer serializer = new DataContractSerializer(type);
            ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting)
                .AsTask().ContinueWith((file) =>
                {
                    file.Result.OpenTransactedWriteAsync().AsTask().ContinueWith((stream) =>
                    { 
                        serializer.WriteObject(stream.Result.Stream.AsStreamForWrite(), serializeItem);
                        stream.Result.CommitAsync();
                    });
                });
        }

        public static void ReadFileAsync(Type type, string fileName, Action<object> result)
        {
            DataContractSerializer serializer = new DataContractSerializer(type);
            ApplicationData.Current.LocalFolder.GetFileAsync(fileName)
                .AsTask().ContinueWith((file) =>
                {
                    file.Result.OpenReadAsync().AsTask().ContinueWith((stream) =>
                    {
                        return serializer.ReadObject(stream.Result.AsStreamForRead());
                    }).ContinueWith(t => result(t.Result));
                });
        }


    }
}
