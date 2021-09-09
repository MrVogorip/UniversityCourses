namespace Strategy.GameLogic.Entities.Buildings.Base
{
    public class BaseBuilding
    {
        public int CurrentLvl { get; set; }

        public int MaxLvl { get; protected set; }

        public int CoefficientForImprovement { get; protected set; }

        public virtual void AddLevel()
        {
            CurrentLvl++;
            if (CurrentLvl >= MaxLvl)
            {
                CurrentLvl = MaxLvl;
            }
        }

        public virtual int CalculateCoinsRequiredForLvlUp()
        {
            return CoefficientForImprovement * CurrentLvl;
        }
    }
}
