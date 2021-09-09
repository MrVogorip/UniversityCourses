﻿namespace Strategy.GameLogic.Services.GameStyle
{
    public class StyleAggressor : IGameStyle
    {
        public bool CalculateAttackArmyProbability()
        {
            return Game.Random.Next(0, 101) < 50;
        }

        public int CalculateCoinsForArmy(int coinsAll)
        {
            return (int)(coinsAll * 0.70);
        }

        public int CalculateCoinsForBuildings(int coinsAll)
        {
            return (int)(coinsAll * 0.10);
        }

        public bool CalculateUpdateArmyProbability()
        {
            return Game.Random.Next(0, 101) < 50;
        }
    }
}
