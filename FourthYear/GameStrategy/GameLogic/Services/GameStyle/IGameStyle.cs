namespace Strategy.GameLogic.Services.GameStyle
{
    public interface IGameStyle
    {
        int CalculateCoinsForBuildings(int coinsAll);

        int CalculateCoinsForArmy(int coinsAll);

        bool CalculateAttackArmyProbability();

        bool CalculateUpdateArmyProbability();
    }
}
