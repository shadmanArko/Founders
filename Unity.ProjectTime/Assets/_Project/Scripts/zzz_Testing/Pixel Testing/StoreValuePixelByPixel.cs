using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;
using Color = System.Drawing.Color;
using Image = UnityEngine.UI.Image;

namespace _Project.Scripts.zzz_Testing.Pixel_Testing
{
    public class StoreValuePixelByPixel : MonoBehaviour
    {
        public Color Color;
        public Sprite spriteToConvert;
        public List<ListOfColor> ListOfPixels;
        Bitmap img = new Bitmap("D:/Unity Project/ProjectTime/Unity.ProjectTime/Assets/_Project/Arts/2D/Prototype/Map Textures/Drylands/drylands 1.png");

        [FormerlySerializedAs("renderer")] public Image image;
    
        // Start is called before the first frame update
        void Start()
        {
            StoreValue();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void StoreValue()
        {
            for (int i = 0; i < img.Width; i++)
            {
                ListOfColor listOfColor = new ListOfColor
                {
                    unitPixel = new List<UnityEngine.Color>()
                };
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i,j);
                    UnityEngine.Color unityPixel = new UnityEngine.Color(
                        pixel.R / 255f,   // Divide by 255 to convert to the range [0, 1]
                        pixel.G / 255f,
                        pixel.B / 255f,
                        pixel.A / 255f
                    );
                    listOfColor.unitPixel.Add(unityPixel);
                }
                ListOfPixels.Add(listOfColor);
            }
        }
    
        [ContextMenu("Get Texture")]
        public void GetTexture()
        {
            var newMemoryStream = new MemoryStream();
            img.Save(newMemoryStream, img.RawFormat);
            spriteToConvert.texture.LoadImage(newMemoryStream.ToArray());
            image.sprite = spriteToConvert;
        }
    }

    [System.Serializable]
    public class ListOfColor
    {
        public List<UnityEngine.Color> unitPixel;
    }
}