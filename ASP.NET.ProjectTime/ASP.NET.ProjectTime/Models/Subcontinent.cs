using System;
using System.Collections.Generic;
using ASP.NET.ProjectTime.Services;

namespace ASP.NET.ProjectTime.Models
{
    [Serializable]
    public class Subcontinent : Base
    {
        public string subcontinentName;
        public Position2 subcontinentPosition;
        public List<Position2> landChunkList;
        public List<Position2> mountainChunkList;
        public List<Position2> dryChunkList;
        public List<MapTemperatureInChunkRow> mapTemperatureInChunkRows;

        public IntMinToMaxValue numberOfPeninsula;
        public IntMinToMaxValue heightOfPeninsula;
        
        public IntMinToMaxValue numberOfSmallIslands;
        public IntMinToMaxValue minDistanceOfSmallIslandsFromLand;
        public IntMinToMaxValue numberOfSmallIslandLandTiles;

        public IntMinToMaxValue numberOfSecondaryMountainRange;
        public IntMinToMaxValue lengthOfSecondaryMountainRange;

        public int hillCreationProbabilityAtMountainTile;
        public int hillCreationProbabilityBesideMountainTile;
        
        public IntMinToMaxValue numberOfLakes;
        
        public IntMinToMaxValue numberOfSwamps;

        public IntMinToMaxValue riverOriginDistanceFromCoast;
        public IntMinToMaxValue numberOfRivers;
        public IntMinToMaxValue numberOfRiverBranches;
        public IntMinToMaxValue riverBranchOriginDistanceFromCoast;

        public List<MandatoryRiver> mandatoryRivers;
    }
}