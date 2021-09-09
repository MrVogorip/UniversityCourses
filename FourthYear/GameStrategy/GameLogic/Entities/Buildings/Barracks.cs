using Strategy.GameLogic.Entities.Army;
using Strategy.GameLogic.Entities.Buildings.Base;
using Strategy.GameLogic.Exceptions;
using Strategy.GameLogic.Tools;

namespace Strategy.GameLogic.Entities.Buildings
{
    public class Barracks : BaseBuilding
    {
        public Barracks()
        {
            CurrentLvl = 1;
            MaxLvl = 10;
            CoefficientForImprovement = 300;
        }

        public Shooter HireShooter(bool isInfo = false)
        {
            Shooter shooter = new Shooter();

            if (isInfo)
            {
                return shooter;
            }

            if (CurrentLvl == 2)
            {
                shooter.PreparationTime -= 1;
                return shooter;
            }

            if (CurrentLvl >= 3)
            {
                shooter.PreparationTime -= 2;
                return shooter;
            }

            return shooter;
        }

        public Infantryman HireInfantryman(bool isInfo = false)
        {
            Infantryman infantryman = new Infantryman();

            if (isInfo)
            {
                return infantryman;
            }

            //if (CurrentLvl < 4)
            //    throw new NotEnoughLevelException(ConstStrings.LvlHiringErrorMessage);

            if (CurrentLvl == 5)
            {
                infantryman.PreparationTime -= 1;
                return infantryman;
            }

            if (CurrentLvl >= 6)
            {
                infantryman.PreparationTime -= 2;
                return infantryman;
            }

            return infantryman;
        }

        public Cavalry HireCavalry(bool isInfo = false)
        {
            Cavalry cavalry = new Cavalry();

            if (isInfo)
            {
                return cavalry;
            }

            //if (CurrentLvl < 7)
            //    throw new NotEnoughLevelException(ConstStrings.LvlHiringErrorMessage);

            if (CurrentLvl == 8)
            {
                cavalry.PreparationTime -= 1;
                return cavalry;
            }

            if (CurrentLvl == 9)
            {
                cavalry.PreparationTime -= 2;
                return cavalry;
            }

            if (CurrentLvl >= 10)
            {
                cavalry.PreparationTime -= 3;
                return cavalry;
            }

            return cavalry;
        }

        public override void AddLevel()
        {
            base.AddLevel();
        }
    }
}
