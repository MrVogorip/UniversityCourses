using Strategy.GameLogic.Entities;
using Strategy.GameLogic.Entities.Buildings.Base;

namespace Strategy.GameLogic.Services
{
    public class ResourceService
    {
        public void GenerateResourcesForNextStep(Castle castle)
        {
            CalculateNewCoins(castle);
            CalculateNewPopulation(castle);
        }

        private void CalculateNewPopulation(Castle castle)
        {
            int newPopulation = Game.Random.Next(castle.ResidentialBuildings.NewPopulace, castle.ResidentialBuildings.MaxPopulaceOneTurn);

            newPopulation += AddPopulationBasedOnBuildingLevel(castle.TownHall);
            newPopulation += AddPopulationBasedOnBuildingLevel(castle.ResidentialBuildings);
            newPopulation += AddPopulationBasedOnBuildingLevel(castle.Walls);
            newPopulation += AddPopulationBasedOnBuildingLevel(castle.Temple);
            newPopulation += AddPopulationBasedOnBuildingLevel(castle.Barracks);

            if (newPopulation >= castle.ResidentialBuildings.MaxPopulaceOneTurn)
            {
                newPopulation = castle.ResidentialBuildings.MaxPopulaceOneTurn;
            }

            castle.Resource.Population += newPopulation;

            if (castle.Resource.Population >= castle.ResidentialBuildings.MaxCurrentPopulace)
            {
                castle.Resource.Population = castle.ResidentialBuildings.MaxCurrentPopulace;

                if (castle.Resource.Population >= castle.ResidentialBuildings.MaxPopulace)
                {
                    castle.Resource.Population = castle.ResidentialBuildings.MaxPopulace;
                }
            }
        }

        private void CalculateNewCoins(Castle castle)
        {
            int newCoins = 0;

            newCoins += AddCoinsBasedOnBuildingLevel(castle.TownHall);
            newCoins += AddCoinsBasedOnBuildingLevel(castle.ResidentialBuildings);
            newCoins += AddCoinsBasedOnBuildingLevel(castle.Walls);
            newCoins += AddCoinsBasedOnBuildingLevel(castle.Temple);
            newCoins += AddCoinsBasedOnBuildingLevel(castle.Barracks);

            castle.Resource.Coins += newCoins / 5;
        }

        private int AddPopulationBasedOnBuildingLevel(BaseBuilding baseBuilding)
        {
            if (baseBuilding.CurrentLvl % 2 == 0)
            {
                return Game.Random.Next(0, baseBuilding.MaxLvl);
            }

            return Game.Random.Next(0, baseBuilding.CurrentLvl);
        }

        private int AddCoinsBasedOnBuildingLevel(BaseBuilding baseBuilding)
        {
            if (baseBuilding.CurrentLvl < baseBuilding.MaxLvl / 2)
                return Game.Random.Next(baseBuilding.CoefficientForImprovement,
                    baseBuilding.CurrentLvl * baseBuilding.CoefficientForImprovement);

            return Game.Random.Next((baseBuilding.CurrentLvl - 2) * baseBuilding.CoefficientForImprovement,
                (baseBuilding.CurrentLvl + 1) * baseBuilding.CoefficientForImprovement);
        }
    }
}
