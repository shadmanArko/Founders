using ASP.NET.ProjectTime.Models;
namespace _Project.Scripts.Tiles
{
    public interface ITileController
    {
        string Id { get; }
        void Config();
    }
}