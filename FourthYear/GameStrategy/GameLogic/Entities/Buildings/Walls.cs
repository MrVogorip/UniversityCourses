using Strategy.GameLogic.Entities.Buildings.Base;

namespace Strategy.GameLogic.Entities.Buildings
{
    public class Walls : BaseBuilding
    {
        public Walls()
        {
            CurrentLvl = 1;
            MaxLvl = 10;
            CoefficientForImprovement = 500;
            DefenseForArmy = 0;
            MaxDefenseForArmy = 5;
        }

        public int DefenseForArmy { get; set; }

        public int MaxDefenseForArmy { get; set; }

        public override void AddLevel()
        {
            base.AddLevel();

            if (CurrentLvl % 2 == 0)
            {
                DefenseForArmy++;

                if (DefenseForArmy >= MaxDefenseForArmy)
                {
                    DefenseForArmy = MaxDefenseForArmy;
                }
            }
        }
    }
}
