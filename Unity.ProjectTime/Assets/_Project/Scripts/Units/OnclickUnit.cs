using _Project.Scripts.Game_Actions;
using UnityEngine;


namespace _Project.Scripts.Units
{
    public class OnclickUnit : MonoBehaviour
    {
        public string unitId;
        private void OnMouseUpAsButton()
        {
            UnitActions.onSelectedUnit?.Invoke(unitId);
        }
    }
}