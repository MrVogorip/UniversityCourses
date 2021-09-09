using Strategy.GameLogic.Entities.Army;
using Strategy.GameLogic.Entities.Buildings.Base;

namespace Strategy.GameLogic.Entities.Buildings
{
    public class TownHall : BaseBuilding
    {
        public TownHall()
        {
            CurrentLvl = 1;
            MaxLvl = 10;
            CoefficientForImprovement = 200;
            TaxPercentage = 10;
            CoefficientForTaxPercentageImprovement = 2;
            MaxTaxPercentage = 28;
        }

        public int TaxPercentage { get; set; }

        public int CoefficientForTaxPercentageImprovement { get; private set; }

        public int MaxTaxPercentage { get; }

        public override void AddLevel()
        {
            base.AddLevel();

            TaxPercentage += CoefficientForTaxPercentageImprovement;

            if (TaxPercentage >= MaxTaxPercentage)
            {
                TaxPercentage = MaxTaxPercentage;
            }
        }

        public Recruit HireRecruit()
        {
            return new Recruit();
        }
    }
}
