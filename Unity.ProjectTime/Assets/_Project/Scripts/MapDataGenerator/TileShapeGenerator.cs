using _Project.Scripts.ScriptableObjectDataContainerScripts;
using ASP.NET.ProjectTime.Models;

namespace _Project.Scripts.MapDataGenerator
{
    public class TileShapeGenerator
    {
        private readonly TileMapInitializingDataContainer _tileMapInitializingDataContainer;
        private Vertex[] vertices;

        private int[] triangles;

        public TileShapeGenerator(TileMapInitializingDataContainer tileMapInitializingDataContainer)
        {
            _tileMapInitializingDataContainer = tileMapInitializingDataContainer;
        }

        public (Vertex[], int[]) CreateShape()
        {
            vertices = new Vertex[((_tileMapInitializingDataContainer.gridSizeX * _tileMapInitializingDataContainer.meshDivisions) + 1) * ((_tileMapInitializingDataContainer.gridSizeY* _tileMapInitializingDataContainer.meshDivisions) + 1)];
            for (int i = 0, z = 0; z <= _tileMapInitializingDataContainer.gridSizeY* _tileMapInitializingDataContainer.meshDivisions; z++)
            {
                for (int x = 0; x <= _tileMapInitializingDataContainer.gridSizeX* _tileMapInitializingDataContainer.meshDivisions; x++)
                {
                    //float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) ;
                    float y = 0;
                    //vertices[i] = new Vertex(x, y, z);
                    var divisor = 1.0f / ((float)_tileMapInitializingDataContainer.meshDivisions);
                    vertices[i] = new Vertex(x * divisor, y, z*divisor);
                    i++;
                }
            }
        
            CreateTriangles();
        return (vertices, triangles );
        }
        private void CreateTriangles()
        {

            triangles = new int[ _tileMapInitializingDataContainer.meshDivisions *  _tileMapInitializingDataContainer.meshDivisions * 6];

            int vert = 0;
            int tris = 0;

            for (int z = 0; z <  _tileMapInitializingDataContainer.meshDivisions; z++)
            {
                for (int x = 0; x <  _tileMapInitializingDataContainer.meshDivisions; x++)
                {
                    triangles[tris + 0] = vert + 0;
                    triangles[tris + 1] = vert +  _tileMapInitializingDataContainer.meshDivisions + 1;
                    triangles[tris + 2] = vert + 1;
                    triangles[tris + 3] = vert + 1;
                    triangles[tris + 4] = vert +  _tileMapInitializingDataContainer.meshDivisions + 1;
                    triangles[tris + 5] = vert +  _tileMapInitializingDataContainer.meshDivisions + 2;

                    vert++;
                    tris += 6;
                }

                vert++;
            }
        }

    }
}