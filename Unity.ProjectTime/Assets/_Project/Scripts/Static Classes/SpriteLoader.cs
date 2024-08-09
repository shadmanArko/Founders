using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Static_Classes
{
    public static class SpriteLoader
    {
        private const string ResourceIconFolderPath = "Resources Icons";
        public static Sprite GetResourceIcon(this NaturalResource naturalResource)
        {
            string spritePath = $"{ResourceIconFolderPath}/{naturalResource.IconName}";
            Sprite sprite = Resources.Load<Sprite>(spritePath);
            if (sprite == null)
            {
                // Debug.LogError($"Sprite '{naturalResource.IconName}' not found at path '{spritePath}'");
                spritePath = $"{ResourceIconFolderPath}/sheep";
                sprite = Resources.Load<Sprite>(spritePath);
            }
            return sprite;
        }
    }
}
