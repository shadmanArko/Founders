using System.Threading.Tasks;
using ASP.NET.ProjectTime.Models;
using UnityEngine;
using UnityEngine.Networking;

namespace _Project.Scripts.HelperScripts
{
    public static class SpriteLoader
    {
        public static async Task<Sprite> LoadSpriteFromPath(this PngAndAddressableFileLocation pngAndAddressableFileLocation)
        {
            string location = pngAndAddressableFileLocation.PngFileLocation;
            if (location == null) return null;
            var request = UnityWebRequestTexture.GetTexture(location);
            var taskCompletionSource = new TaskCompletionSource<Sprite>();
            var asyncOperation = request.SendWebRequest();

            asyncOperation.completed += _ =>
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    var texture2D = DownloadHandlerTexture.GetContent(request);
                    var loadedSprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.one * 0.5f);
                    taskCompletionSource.SetResult(loadedSprite);
                }
                else
                    taskCompletionSource.SetResult(null);
            };
            
            return await taskCompletionSource.Task;
        }
    }
}
