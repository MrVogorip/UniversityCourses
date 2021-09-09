using Strategy.GameLogic.Entities;
using Strategy.GameLogic.Entities.Army;
using Strategy.GameLogic.Entities.Army.Base;
using Strategy.GameLogic.Exceptions;
using Strategy.GameLogic.Tools;

namespace Strategy.GameLogic.Services
{
    public class ArmyService
    {
        public void NextMove(Castle castle)
        {
            int sizeQueue = castle.QueueDetachment.QueueRecruits.Count;

            while (sizeQueue > 0)
            {
                var queueValue = castle.QueueDetachment.QueueRecruits.Dequeue();
                queueValue.Item1--;
                if (queueValue.Item1 <= 0)
                {
                    castle.CastleDetachment.Recruits =
                        (castle.CastleDetachment.Recruits.Item1,
                        castle.CastleDetachment.Recruits.Item2 + queueValue.Item2);
                }
                else
                    castle.QueueDetachment.QueueRecruits.Enqueue(queueValue);

                sizeQueue--;
            }

            sizeQueue = castle.QueueDetachment.QueueShooters.Count;

            while (sizeQueue > 0)
            {
                var queueValue = castle.QueueDetachment.QueueShooters.Dequeue();
                queueValue.Item1--;
                if (queueValue.Item1 <= 0)
                {
                    castle.CastleDetachment.Shooters =
                        (castle.CastleDetachment.Shooters.Item1,
                        castle.CastleDetachment.Shooters.Item2 + queueValue.Item2);
                }
                else
                    castle.QueueDetachment.QueueShooters.Enqueue(queueValue);

                sizeQueue--;
            }

            sizeQueue = castle.QueueDetachment.QueueInfantrymans.Count;

            while (sizeQueue > 0)
            {
                var queueValue = castle.QueueDetachment.QueueInfantrymans.Dequeue();
                queueValue.Item1--;
                if (queueValue.Item1 <= 0)
                {
                    castle.CastleDetachment.Infantrymans =
                        (castle.Barracks.HireInfantryman(),
                        castle.CastleDetachment.Infantrymans.Item2 + queueValue.Item2);
                }
                else
                    castle.QueueDetachment.QueueInfantrymans.Enqueue(queueValue);

                sizeQueue--;
            }

            sizeQueue = castle.QueueDetachment.QueueCavalries.Count;

            while (sizeQueue > 0)
            {
                var queueValue = castle.QueueDetachment.QueueCavalries.Dequeue();
                queueValue.Item1--;
                if (queueValue.Item1 <= 0)
                {
                    castle.CastleDetachment.Cavalries =
                        (castle.CastleDetachment.Cavalries.Item1,
                        castle.CastleDetachment.Cavalries.Item2 + queueValue.Item2);
                }
                else
                    castle.QueueDetachment.QueueCavalries.Enqueue(queueValue);

                sizeQueue--;
            }
        }

        public void HireRecruitToArmy(Castle castle, int n)
        {
            Recruit newRecruit = castle.TownHall.HireRecruit();

            if (n < 0)
            {
                n = 0;
            }

            int coinsRequiredForHireUit = CalculateCoinsRequiredForHireUnit(newRecruit, n);

            if (castle.Resource.Coins < coinsRequiredForHireUit)
            {
                throw new NotEnoughCoinsException(ConstStrings.CostHiringErrorMessage);
            }

            castle.Resource.Coins -= coinsRequiredForHireUit;

            castle.QueueDetachment.QueueRecruits.Enqueue((newRecruit.PreparationTime, n));
        }

        public void HireShooterToArmy(Castle castle, int n)
        {
            Shooter newShooter = castle.Barracks.HireShooter();

            if (n < 0)
            {
                n = 0;
            }

            int coinsRequiredForHireUit = CalculateCoinsRequiredForHireUnit(newShooter, n);

            if (castle.Resource.Coins < coinsRequiredForHireUit)
            {
                throw new NotEnoughCoinsException(ConstStrings.CostHiringErrorMessage);
            }

            castle.Resource.Coins -= coinsRequiredForHireUit;

            castle.QueueDetachment.QueueShooters.Enqueue((newShooter.PreparationTime, n));
        }

        public void HireInfantrymanToArmy(Castle castle, int n)
        {
            Infantryman newInfantryman = castle.Barracks.HireInfantryman();

            if (n < 0)
            {
                n = 0;
            }

            int coinsRequiredForHireUit = CalculateCoinsRequiredForHireUnit(newInfantryman, n);

            if (castle.Resource.Coins < coinsRequiredForHireUit)
            {
                throw new NotEnoughCoinsException(ConstStrings.CostHiringErrorMessage);
            }

            castle.Resource.Coins -= coinsRequiredForHireUit;

            castle.QueueDetachment.QueueInfantrymans.Enqueue((newInfantryman.PreparationTime, n));
        }

        public void HireCavalryToArmy(Castle castle, int n)
        {
            Cavalry newCavalry = castle.Barracks.HireCavalry();

            if (n < 0)
            {
                n = 0;
            }

            int coinsRequiredForHireUit = CalculateCoinsRequiredForHireUnit(newCavalry, n);

            if (castle.Resource.Coins < coinsRequiredForHireUit)
            {
                throw new NotEnoughCoinsException(ConstStrings.CostHiringErrorMessage);
            }

            castle.Resource.Coins -= coinsRequiredForHireUit;

            castle.QueueDetachment.QueueCavalries.Enqueue((newCavalry.PreparationTime, n));
        }

        private int CalculateCoinsRequiredForHireUnit(BaseArmyUnit baseArmyUnit, int n)
        {
            return baseArmyUnit.HiringCost * n;
        }
    }
}
