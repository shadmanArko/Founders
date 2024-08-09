using System;
using System.Collections.Generic;

namespace ASP.NET.ProjectTime.Models.PathFinding
{
    public class AStarPathfinding
    {
        
        //todo Connect with main program
        // public int GridSizeX = 10; // Adjust this based on your grid size
        // public int GridSizeY = 10; // Adjust this based on your grid size
        
        //private Node[,] _grid;
        private List<Node> _openSet;
        private HashSet<Node> _closedSet;

        private readonly int _gridSizeX;
        private readonly int _gridSizeY;
        //private readonly Node[,] _grid;

        public AStarPathfinding(int gridSizeX, int gridSizeY /*, Node[,] grid*/)
        {
            _gridSizeX = gridSizeX;
            _gridSizeY = gridSizeY;
            //_grid = grid;
        }


        public List<TileCoordinates> FindPath(TileCoordinates startTileCoordinates, TileCoordinates goalTileCoordinates, Node[,] grid)
        {
            // _grid = new Node[GridSizeX, GridSizeY];
            //
            // for (int x = 0; x < GridSizeX; x++)
            // {
            //     for (int y = 0; y < GridSizeY; y++)
            //     {
            //         var tempTileCoord = new TileCoordinates
            //         {
            //             X = x,
            //             Y = y
            //         };
            //        // _grid[x, y] = new Node(tempTileCoord, null, int.MaxValue, float.MaxValue, true); //todo change is walkable
            //     }
            // }

            _openSet = new List<Node>();
            _closedSet = new HashSet<Node>();

            Node startNode = grid[startTileCoordinates.X, startTileCoordinates.Y];
            Node goalNode = grid[goalTileCoordinates.X, goalTileCoordinates.Y];
            
            startNode.GCost = 0; // Set GCost of the start node to zero
            startNode.HCost = GetDistance(startNode, goalNode); // Set HCost using the heuristic
            
            _openSet.Add(startNode);

            while (_openSet.Count > 0)
            {
                Node currentNode = GetLowestFCostNode(_openSet);

                if (currentNode == goalNode)
                {
                    // Path found
                    return RetracePath(startNode, goalNode);
                }

                _openSet.Remove(currentNode);
                _closedSet.Add(currentNode);

                foreach (Node neighbor in GetNeighbors(currentNode, grid))
                {
                    if (!neighbor.IsWalkable || _closedSet.Contains(neighbor))
                        continue;

                    float newGCost = currentNode.GCost + GetDistance(currentNode, neighbor);

                    if (newGCost < neighbor.GCost || !_openSet.Contains(neighbor))
                    {
                        neighbor.GCost = newGCost;
                        neighbor.HCost = GetDistance(neighbor, goalNode);
                        neighbor.Parent = currentNode;

                        if (!_openSet.Contains(neighbor))
                            _openSet.Add(neighbor);
                    }
                }
            }

            // No path found
            // Debug.Log("No path found.");
            return null;
        }
        private Node GetLowestFCostNode(List<Node> nodeList)
        {
            Node lowestFCostNode = nodeList[0];

            for (int i = 1; i < nodeList.Count; i++)
            {
                if (nodeList[i].FCost < lowestFCostNode.FCost)
                    lowestFCostNode = nodeList[i];
            }

            return lowestFCostNode;
        }
        
        private List<Node> GetNeighbors(Node node, Node[,] grid)
        {
            List<Node> neighbors = new List<Node>();

            int[] neighborOffsets = { -1, 0, 1 };

            foreach (int xOffset in neighborOffsets)
            {
                foreach (int yOffset in neighborOffsets)
                {
                    // Skip the center (current) node and include diagonal neighbors
                    if (xOffset != 0 || yOffset != 0)
                    {
                        int neighborX = node.TileCoordinates.X + xOffset;
                        int neighborY = node.TileCoordinates.Y + yOffset;

                        if (neighborX >= 0 && neighborX < _gridSizeX && neighborY >= 0 && neighborY < _gridSizeY)
                        {
                            Node neighbor = grid[neighborX, neighborY];
                            // Adjust the movement cost for diagonal neighbors to be higher
                            int movementCost = xOffset != 0 && yOffset != 0 ? 14 : 10; // 14 for diagonals, 10 for straight
                            neighbor.GCost = node.GCost + movementCost;
                            neighbors.Add(neighbor);
                        }
                    }
                }
            }

            return neighbors;
        }

        
        private List<TileCoordinates> RetracePath(Node startNode, Node endNode)
        {
            List<TileCoordinates> path = new List<TileCoordinates>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode.TileCoordinates);// todo If it makes problem then create new TileCoord and assign it.
                currentNode = currentNode.Parent;
            }

            path.Reverse();
            return path;
        }
        
        private int GetDistance(Node nodeA, Node nodeB)
        {
            int distanceX = Math.Abs(nodeA.TileCoordinates.X - nodeB.TileCoordinates.X);
            int distanceY = Math.Abs(nodeA.TileCoordinates.Y - nodeB.TileCoordinates.Y);

            // Adjust the cost of diagonal movement as desired (e.g., multiply by 1.4 for 45-degree movement)
            float diagonalCost = 3; // Cost of diagonal movement (approximately 1.4 times straight cost)
            float straightCost = 2; // Cost of straight movement

            int diagonalSteps = Math.Min(distanceX, distanceY);
            int straightSteps = Math.Abs(distanceX - distanceY);

            if (diagonalSteps != 0)
            {
                nodeB.MovecostFromPreviousTile = 3;
            }
            else
            {
                nodeB.MovecostFromPreviousTile = 2;
            }

            return (int)((diagonalCost * diagonalSteps) + (straightCost * straightSteps));
        }


    }
    
    
}