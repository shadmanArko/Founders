using System.Threading.Tasks;
using ASP.NET.ProjectTime.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Project.Scripts.HelperScripts
{
    public static class AddressableSpriteLoader
    {
        public static async Task<Sprite> LoadSpriteFromAssetBundle(this PngAndAddressableFileLocation pngAndAddressableFileLocation)
        {
            string bundleName = pngAndAddressableFileLocation.BundleName;
            string bundlePath = pngAndAddressableFileLocation.BundleLocation;
            string spritePath = pngAndAddressableFileLocation.PngFileLocation;
            if (bundleName == null || bundlePath == null || spritePath == null) return null;
            //var bundle = LoadAssetBundle(bundleName, bundlePath);
            
            //var request = bundle.LoadAssetAsync<Sprite>(spritePath);
            
            var handle = Addressables.LoadAssetAsync<Sprite>(spritePath);
            
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.LogWarning("Sprite Loaded successfully");
                return handle.Result;
            }
            
            Debug.LogError("Sprite load failed");
            return null;

            // while (!request.isDone)
            //     await Task.Yield();
            //
            // if (request.asset != null)
            // {
            //     var loadedSprite = request.asset as Sprite;
            //     bundle.Unload(false); // Unload the AssetBundle when done.
            //     return loadedSprite;
            // }
                
            // bundle.Unload(false); // Unload the AssetBundle when done.
            // Debug.LogError("Failed to load Sprite from AssetBundle.");
            // return null;
        }

        private static AssetBundle LoadAssetBundle(string bundleName, string bundlePath)
        {
            var bundle = AssetBundle.LoadFromFile($"{bundlePath}/{bundleName}");
            return bundle;
        }
    }
}
