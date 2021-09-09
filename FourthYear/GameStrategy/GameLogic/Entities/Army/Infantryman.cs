using Strategy.GameLogic.Entities.Army.Base;

namespace Strategy.GameLogic.Entities.Army
{
    public class Infantryman : BaseArmyUnit
    {
        public Infantryman()
        {
            Protection = 6;
            Attack = 8;
            SpeedAndInitiative = 1;
            PreparationTime = 3;
            HiringCost = 20;
        }
    }
}
