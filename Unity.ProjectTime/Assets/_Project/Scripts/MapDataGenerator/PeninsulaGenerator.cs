using System.Collections.Generic;
using _Project.Scripts.HelperScripts;
using ASP.NET.ProjectTime.Services;
using UnityEngine;

namespace _Project.Scripts.MapDataGenerator
{
    public class PeninsulaGenerator
    {
        private NewGameDataGenerator _newGameDataGenerator;

        private List<string> peninsulaDirections = new List<string>()
            { "UpperLeft, UpperRight", "RightUp", "RightDown", "DownRight", "DownLeft", "LeftUp", "LeftDown" };

    

        public PeninsulaGenerator(NewGameDataGenerator newGameDataGenerator)
        {
            _newGameDataGenerator = newGameDataGenerator;
        }

        public void MakePeninsula()
        {
            int maxNumberOfPeninsula = _newGameDataGenerator.subcontinent.numberOfPeninsula.RandomValueInRange();
            int maxHeightOfPeninsula = _newGameDataGenerator.subcontinent.heightOfPeninsula.RandomValueInRange();
            _newGameDataGenerator.GetEdgeTiles();
            int createdPeninsula = 0;
            _newGameDataGenerator.edgeTiles = _newGameDataGenerator.edgeTiles.Shuffle();
            foreach (var edgeTile in _newGameDataGenerator.edgeTiles)
            {
                // Debug.Log("Edge tile " + edgeTile.XPosition + " " + edgeTile.YPosition);
                List<string> shuffledPeninsulaDirections = peninsulaDirections.Shuffle();
                foreach (var peninsulaDirection in shuffledPeninsulaDirections)
                {
                    switch (peninsulaDirection)
                    {
                        case "UpperLeft":
                            if (edgeTile.IsUpLeftPeninsulaPossible(maxHeightOfPeninsula, _newGameDataGenerator.listOfTiles))
                            {
                                // Debug.Log("Creating Upper Left peninsula for " + edgeTile.XPosition +", "+ edgeTile.YPosition);
                                edgeTile.GenerateVerticalPeninsula(maxHeightOfPeninsula, Directions.Left, Directions.Up, _newGameDataGenerator.listOfTiles);
                                createdPeninsula++;
                            }
                            break;
                        case "UpperRight":
                            if (edgeTile.IsUpRightPeninsulaPossible(maxHeightOfPeninsula, _newGameDataGenerator.listOfTiles))
                            {
                                // Debug.Log("Creating Upper Left peninsula for " + edgeTile.XPosition +", "+ edgeTile.YPosition);
                                edgeTile.GenerateVerticalPeninsula(maxHeightOfPeninsula, Directions.Right, Directions.Up, _newGameDataGenerator.listOfTiles);
                                createdPeninsula++;
                            }
                            break;
                        case "DownRight":
                            if (edgeTile.IsDownRightPeninsulaPossible(maxHeightOfPeninsula, _newGameDataGenerator.listOfTiles))
                            {
                                // Debug.Log("Creating Down Right peninsula for " + edgeTile.XPosition +", "+ edgeTile.YPosition);
                                edgeTile.GenerateVerticalPeninsula(maxHeightOfPeninsula, Directions.Right, Directions.Down, _newGameDataGenerator.listOfTiles);
                                createdPeninsula++;
                            }
                            break;
                        case "DownLeft":
                            if (edgeTile.IsDownLeftPeninsulaPossible(maxHeightOfPeninsula, _newGameDataGenerator.listOfTiles))
                            {
                                // Debug.Log("Creating Down Left peninsula for " + edgeTile.XPosition +", "+ edgeTile.YPosition);
                                edgeTile.GenerateVerticalPeninsula(maxHeightOfPeninsula, Directions.Left, Directions.Down, _newGameDataGenerator.listOfTiles);
                                createdPeninsula++;
                            }
                            break;
                        case "LeftUp":
                            if (edgeTile.IsLeftUpPeninsulaPossible(maxHeightOfPeninsula, _newGameDataGenerator.listOfTiles))
                            {
                                // Debug.Log("Creating Left Up peninsula for " + edgeTile.XPosition +", "+ edgeTile.YPosition);
                                edgeTile.GenerateHorizontalPeninsula(maxHeightOfPeninsula, Directions.Left, Directions.Up, _newGameDataGenerator.listOfTiles);
                                createdPeninsula++;
                            }
                            break;
                        case "LeftDown":
                            if (edgeTile.IsLeftDownPeninsulaPossible(maxHeightOfPeninsula, _newGameDataGenerator.listOfTiles))
                            {
                                // Debug.Log("Creating Left Down peninsula for " + edgeTile.XPosition +", "+ edgeTile.YPosition);
                                edgeTile.GenerateHorizontalPeninsula(maxHeightOfPeninsula, Directions.Left, Directions.Down, _newGameDataGenerator.listOfTiles);
                                createdPeninsula++;
                            }
                            break;
                        case "RightDown":
                            if (edgeTile.IsRightDownPeninsulaPossible(maxHeightOfPeninsula, _newGameDataGenerator.listOfTiles))
                            {
                                // Debug.Log("Creating Right Down peninsula for " + edgeTile.XPosition +", "+ edgeTile.YPosition);
                                edgeTile.GenerateHorizontalPeninsula(maxHeightOfPeninsula, Directions.Right, Directions.Down, _newGameDataGenerator.listOfTiles);
                                createdPeninsula++;
                            }
                            break;
                        case "RightUp":
                            if (edgeTile.IsRightUpPeninsulaPossible(maxHeightOfPeninsula, _newGameDataGenerator.listOfTiles))
                            {
                                // Debug.Log("Creating Right Up peninsula for " + edgeTile.XPosition +", "+ edgeTile.YPosition);
                                edgeTile.GenerateHorizontalPeninsula(maxHeightOfPeninsula, Directions.Right, Directions.Up, _newGameDataGenerator.listOfTiles);
                                createdPeninsula++;
                            }
                            break;
                        default:
                            break;
                    }
                }
                if(createdPeninsula >= maxNumberOfPeninsula) break;
            }
        }
    }
}