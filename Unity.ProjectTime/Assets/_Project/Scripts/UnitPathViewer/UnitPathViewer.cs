using System.Collections.Generic;
using ASP.NET.ProjectTime.Models;
using UnityEngine;

namespace _Project.Scripts.Map
{
    public class UnitPathViewer : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private int _totalWeeksPassedID;
        private int _movementCostInWeeksID;
        private int _filledColorID;
        private void Awake()
        {
            _totalWeeksPassedID = Shader.PropertyToID("_TotalWeeksPassed");
            _movementCostInWeeksID = Shader.PropertyToID("_MovementCostInWeeks");
            _filledColorID = Shader.PropertyToID("_FilledColor");
            _lineRenderer = GetComponent<LineRenderer>();
        }

        public void ShowPath(TileCoordinates startingPos, List<TileCoordinates> listOfTileCoordinates, int startIndex)
        {
            _lineRenderer.positionCount = 0;
            _lineRenderer.positionCount = listOfTileCoordinates.Count + 1 - startIndex;
            int positionCount = 0;
            _lineRenderer.SetPosition(positionCount, new Vector3(startingPos.X + 0.5f, 0.5f, startingPos.Y+0.5f));
            positionCount++;
            int loopCounter = -1;
            foreach (var tileCoordinate in listOfTileCoordinates)
            {
                loopCounter++;
                if(loopCounter < startIndex) continue;
                _lineRenderer.SetPosition(positionCount, new Vector3(tileCoordinate.X + 0.5f, 0.5f, tileCoordinate.Y+0.5f));
                positionCount++;
            
            }
        
        }
        public void ClearLine()
        {
            _lineRenderer.positionCount = 0;

        }
        public void ChangeColorUpToPosition(int positionIndex, Color targetColor)
        {
            if (_lineRenderer != null && positionIndex >= 0 && positionIndex < _lineRenderer.positionCount)
            {
                // Change the color up to the specified position
                for (int i = 0; i <= positionIndex; i++)
                {
                    _lineRenderer.SetPosition(i, _lineRenderer.GetPosition(i));
                    _lineRenderer.startColor = targetColor;
                    _lineRenderer.endColor = targetColor;
                    _lineRenderer.SetPosition(i, _lineRenderer.GetPosition(i)); // Set position again to apply color change
                }

                // Reset the color for the remaining points
                for (int i = positionIndex + 1; i < _lineRenderer.positionCount; i++)
                {
                    _lineRenderer.SetPosition(i, _lineRenderer.GetPosition(i));
                    _lineRenderer.startColor = _lineRenderer.sharedMaterial.color;
                    _lineRenderer.endColor = _lineRenderer.sharedMaterial.color;
                    _lineRenderer.SetPosition(i, _lineRenderer.GetPosition(i)); // Set position again to apply color change
                }
            }
        }
        public void ChangeColorUpToPercentage(int totalWeeksPassed, int movementCostInWeeks, Color filledColor)
        {
            // Debug.Log($"line fill percentage {percentage}");
            _lineRenderer.material.SetInt(_totalWeeksPassedID, totalWeeksPassed);
            _lineRenderer.material.SetInt(_movementCostInWeeksID, movementCostInWeeks); 
            _lineRenderer.material.SetColor(_filledColorID, filledColor);
        }
    
    }
}
