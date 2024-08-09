using System.Collections.Generic;

namespace ASP.NET.ProjectTime.Services.BattleService
{
    public class BattleCalculation
    {
        public float CalculateDiceRoll()
        {
            return 0;
        }

        public float CalculateCommanderEffectiveness()
        {
            return 0;
        }

        public float CalculateTerrainEffects()
        {
            return 0;
        }
        
        public float CalculateSelfWeightedAverageProwess()
        {
            return 0;
        }

        public void CalculateRegimentSize()
        {
            //TODO: SUM(Pop Size of Pop Assigned to Regiment x 5)
        }

        public float CalculateRegimentAttack()
        {
            return 0;
        }

        public float CalculateRegimentDefense()
        {
            return 0;
        }

        public float CasualtiesInflicted(float attackerMetalModifier, float defenderMetalModifier)
        {
            var averageProwessTerrainEffect = CalculateSelfWeightedAverageProwess() * (1 + CalculateTerrainEffects());
            var opponentAverageProwessTerrainEffect = CalculateSelfWeightedAverageProwess() * (1 + CalculateTerrainEffects());

            var regimentAttackMetalModifier = 3 * CalculateRegimentAttack() * attackerMetalModifier;
            var opponentRegimentAttackMetalModifier = 2 * CalculateRegimentDefense() * defenderMetalModifier;

            var diceRollCommanderPower = CalculateDiceRoll() + CalculateCommanderEffectiveness();
            var opponentDiceRollCommanderPower = CalculateDiceRoll() + CalculateCommanderEffectiveness();

            var casualty = (averageProwessTerrainEffect / opponentAverageProwessTerrainEffect) *
                           (regimentAttackMetalModifier - opponentRegimentAttackMetalModifier) *
                           (diceRollCommanderPower / opponentDiceRollCommanderPower);

            return casualty;
        }
    }
}