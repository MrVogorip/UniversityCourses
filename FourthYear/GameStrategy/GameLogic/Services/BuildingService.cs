using Strategy.GameLogic.Entities;
using Strategy.GameLogic.Entities.Buildings;
using Strategy.GameLogic.Entities.Buildings.Base;
using Strategy.GameLogic.Exceptions;
using Strategy.GameLogic.Tools;

namespace Strategy.GameLogic.Services
{
    public class BuildingService
    {
        public void CalculateTaxFromPopulation(Castle castle)
        {
            int CoinsWithTax = castle.Resource.Population * castle.TownHall.TaxPercentage / 100;

            castle.Resource.Coins += CoinsWithTax;
        }

        public void AddLevelForBuilding(BaseBuilding baseBuilding, Castle castle)
        {
            int coinsRequiredForLvlUp = baseBuilding.CalculateCoinsRequiredForLvlUp();

            if (baseBuilding.CurrentLvl >= baseBuilding.MaxLvl)
            {
                throw new MaxLvlException(ConstStrings.AddLevelError);
            }

            if (castle.Resource.Coins < coinsRequiredForLvlUp)
            {
                throw new NotEnoughCoinsException(ConstStrings.NotEnoughCoinsError);
            }

            castle.Resource.Coins -= coinsRequiredForLvlUp;

            baseBuilding.AddLevel();


            if (baseBuilding is Walls)
            {
                castle.CastleDetachment.ProtectorProtectionBonus = ((Walls)baseBuilding).DefenseForArmy;
            }

            if(baseBuilding is Temple)
            {
                castle.CastleDetachment.AttackerAttackBonus = ((Temple)baseBuilding).AttackForArmyOnAttack;
                castle.CastleDetachment.AttackerProtectionBonus = ((Temple)baseBuilding).AttackForArmyOnDefensive;
            }
        }
    }
}
