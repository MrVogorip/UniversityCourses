using Strategy.GameLogic.Entities.Buildings.Base;

namespace Strategy.GameLogic.Entities.Buildings
{
    public class ResidentialBuildings : BaseBuilding
    {
        public ResidentialBuildings()
        {
            CurrentLvl = 1;
            MaxLvl = 25;
            CoefficientForImprovement = 150;
            CoefficientForPopulaceImprovement = 200;
            NewPopulace = 10;
            CoefficientForNewPopulaceImprovement = 10;
            MaxCurrentPopulace = 200;
            MaxPopulace = 5000;
            MaxPopulaceOneTurn = 250;
        }

        public int CoefficientForPopulaceImprovement { get; }

        public int NewPopulace { get; private set; }

        public int CoefficientForNewPopulaceImprovement { get; }
        
        public int MaxCurrentPopulace { get; set; }

        public int MaxPopulace { get; set; }

        public int MaxPopulaceOneTurn { get; set; }

        public override void AddLevel()
        {
            base.AddLevel();

            MaxCurrentPopulace += CoefficientForPopulaceImprovement;

            if(MaxCurrentPopulace >= MaxPopulace)
            {
                MaxCurrentPopulace = MaxPopulace;
            }

            NewPopulace += CoefficientForNewPopulaceImprovement;

            if(NewPopulace >= MaxPopulaceOneTurn)
            {
                NewPopulace = MaxPopulaceOneTurn;
            }
        }
    }
}
