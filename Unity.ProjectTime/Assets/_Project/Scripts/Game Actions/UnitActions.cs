using System;
using ASP.NET.ProjectTime.Services;

namespace _Project.Scripts.Game_Actions
{
    public static class UnitActions
    {
        public static Action<string> onSelectedUnit;
        public static Action<string, string> onUnitMovedToTile;

        public static Action<string> OnClickSettleASettlement;
        public static Action<string, string> OnSettlerSettleASettlement;
        public static Action<string, Directions> OnSelectedTileToMoveInDirection;
    }
}