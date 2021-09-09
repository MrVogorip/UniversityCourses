using System;
using System.Collections.Generic;
using Strategy.GameLogic.Entities;
using Strategy.GameLogic.Entities.Army;
using Strategy.GameLogic.Entities.Army.Base;
using Strategy.GameLogic.Entities.Resources;
using Strategy.GameLogic.Exceptions;
using Strategy.GameLogic.Tools;

namespace Strategy.GameLogic.Services
{
    public class DetachmentService
    {
        public void MoveDetachmentToTarget(Castle castle, Cell[,] othersPositions, List<Detachment> detachmentsEnemys = null)
        {
            foreach (var detachment in castle.Detachments)
            {
                if (detachmentsEnemys != null && detachment.IsPursues)
                {
                    foreach (var detachmentEnemy in detachmentsEnemys)
                    {
                        if (detachmentEnemy.Name == detachment.TargetName)
                        {
                            detachment.TargetPosition = detachmentEnemy.Position;
                        }
                    }
                }

                if (!detachment.IsWin)
                {
                    MoveDetachment(detachment, othersPositions);
                }
            }
        }

        public void MoveDetachmentToHome(Detachment detachment, Castle castle, Cell[,] othersPositions)
        {
            detachment.TargetPosition = castle.Position;

            MoveDetachment(detachment, othersPositions);

            if (detachment.Position == castle.Position)
            {
                ReturnResurses(detachment, castle);
            }
        }

        private void ReturnResurses(Detachment detachment, Castle castle)
        {
            castle.Resource.Coins += detachment.VictoryAwardCoins;

            if (detachment.Recruits.Item2 > 0)
            {
                castle.CastleDetachment.Recruits
                    = (castle.CastleDetachment.Recruits.Item1,
                    castle.CastleDetachment.Recruits.Item2 + detachment.Recruits.Item2);
            }

            if (detachment.Shooters.Item2 > 0)
            {
                castle.CastleDetachment.Shooters
                    = (castle.CastleDetachment.Shooters.Item1,
                    castle.CastleDetachment.Shooters.Item2 + detachment.Shooters.Item2);
            }

            if (detachment.Infantrymans.Item2 > 0)
            {
                castle.CastleDetachment.Infantrymans
                    = (castle.CastleDetachment.Infantrymans.Item1,
                    castle.CastleDetachment.Infantrymans.Item2 + detachment.Infantrymans.Item2);
            }

            if (detachment.Cavalries.Item2 > 0)
            {
                castle.CastleDetachment.Cavalries
                    = (castle.CastleDetachment.Cavalries.Item1,
                    castle.CastleDetachment.Cavalries.Item2 + detachment.Cavalries.Item2);
            }

            castle.Detachments.Remove(detachment);
        }

        private void MoveDetachment(Detachment detachment, Cell[,] othersPositions)
        {
            var position = detachment.Position;
            var targetPosition = detachment.TargetPosition;

            var newPosition = new Position();

            if (position.I - targetPosition.I > 0)
            {
                newPosition.I = position.I - detachment.SpeedDetachment;
            }
            else
            {
                newPosition.I = position.I + detachment.SpeedDetachment;
                if (position.I - targetPosition.I == 0)
                {
                    newPosition.I = position.I;
                }
            }

            if (position.J - targetPosition.J > 0)
            {
                newPosition.J = position.J - detachment.SpeedDetachment;
            }
            else
            {
                newPosition.J = position.J + detachment.SpeedDetachment;
                if (position.J - targetPosition.J == 0)
                {
                    newPosition.J = position.J;
                }
            }

            if (Math.Abs(position.I - targetPosition.I) <= detachment.SpeedDetachment)
            {
                newPosition.I = targetPosition.I;
            }

            if (Math.Abs(position.J - targetPosition.J) <= detachment.SpeedDetachment)
            {
                newPosition.J = targetPosition.J;
            }

            if (newPosition == detachment.TargetPosition)
            {
                detachment.CountStay++;
                if (detachment.CountStay >= 4)
                {
                    detachment.IsWin = true;
                }
            }

            if (newPosition != detachment.TargetPosition)
            {
                if (!string.IsNullOrEmpty(othersPositions[newPosition.I, newPosition.J].Info))
                {
                    SetAdjacentPosition(newPosition, detachment.SpeedDetachment, othersPositions);
                }
            }

            detachment.Position = newPosition;
        }

        private void SetAdjacentPosition(Position position, int distance, Cell[,] othersPositions)
        {
            if (position.I - distance >= 0)
                if (string.IsNullOrEmpty(othersPositions[position.I - distance, position.J].Info))
                {
                    position.I -= distance;
                    return;
                }
            if (position.I + distance < othersPositions.GetLength(0))
                if (string.IsNullOrEmpty(othersPositions[position.I + distance, position.J].Info))
                {
                    position.I += distance;
                    return;
                }
            if (position.J - distance >= 0)
                if (string.IsNullOrEmpty(othersPositions[position.I, position.J - distance].Info))
                {
                    position.J -= distance;
                    return;
                }
            if (position.J + distance < othersPositions.GetLength(1))
                if (string.IsNullOrEmpty(othersPositions[position.I, position.J + distance].Info))
                {
                    position.J += distance;
                    return;
                }

            if (position.I - distance >= 0 && position.J - distance >= 0)
                if (string.IsNullOrEmpty(othersPositions[position.I - distance, position.J - distance].Info))
                {
                    position.I -= distance;
                    position.J -= distance;
                    return;
                }

            if (position.I - distance >= 0 && position.J + distance < othersPositions.GetLength(1))
                if (string.IsNullOrEmpty(othersPositions[position.I - distance, position.J + distance].Info))
                {
                    position.I -= distance;
                    position.J += distance;
                    return;
                }

            if (position.I + distance < othersPositions.GetLength(0) && position.J + distance < othersPositions.GetLength(1))
                if (string.IsNullOrEmpty(othersPositions[position.I + distance, position.J + distance].Info))
                {
                    position.I += distance;
                    position.J += distance;
                    return;
                }

            if (position.I + distance < othersPositions.GetLength(0) && position.J - distance >= 0)
                if (string.IsNullOrEmpty(othersPositions[position.I + distance, position.J - distance].Info))
                {
                    position.I += distance;
                    position.J -= distance;
                    return;
                }

        }

        public void CreateDetachment(
            Castle castle,
            int countRecruits,
            int countShooters,
            int countInfantrymens,
            int countCavalries,
            Position position,
            Position targetPosition,
            Detachment detachmentEnemy = null)
        {
            if (countRecruits <= 0 && countShooters <= 0 && countInfantrymens <= 0 && countCavalries <= 0)
            {
                throw new CreateDetachmentException(ConstStrings.CreateDetachmentError);
            }

            Detachment newDetachment = new Detachment(
                position,
                targetPosition,
                castle.Walls.DefenseForArmy,
                castle.Temple.AttackForArmyOnAttack,
                castle.Temple.AttackForArmyOnDefensive);

            int speed = castle.Barracks.HireCavalry(true).SpeedAndInitiative;

            if (countRecruits > 0)
            {
                newDetachment.Recruits = ((Recruit, int))CalculateUnitForNewDetachment(
                    castle.CastleDetachment.Recruits,
                    countRecruits);

                castle.CastleDetachment.Recruits = ((Recruit, int))RemoveUnitsFromSquad(
                    castle.CastleDetachment.Recruits,
                    countRecruits);

                speed = newDetachment.Recruits.Item1.SpeedAndInitiative < speed ? newDetachment.Recruits.Item1.SpeedAndInitiative : speed;
            }

            if (countShooters > 0)
            {
                newDetachment.Shooters = ((Shooter, int))CalculateUnitForNewDetachment(
                    castle.CastleDetachment.Shooters,
                    countShooters);

                castle.CastleDetachment.Shooters = ((Shooter, int))RemoveUnitsFromSquad(
                    castle.CastleDetachment.Shooters,
                    countShooters);

                speed = newDetachment.Shooters.Item1.SpeedAndInitiative < speed ? newDetachment.Shooters.Item1.SpeedAndInitiative : speed;
            }

            if (countInfantrymens > 0)
            {
                newDetachment.Infantrymans = ((Infantryman, int))CalculateUnitForNewDetachment(
                    castle.CastleDetachment.Infantrymans,
                    countInfantrymens);

                castle.CastleDetachment.Infantrymans = ((Infantryman, int))RemoveUnitsFromSquad(
                    castle.CastleDetachment.Infantrymans,
                    countInfantrymens);

                speed = newDetachment.Infantrymans.Item1.SpeedAndInitiative < speed ? newDetachment.Infantrymans.Item1.SpeedAndInitiative : speed;
            }

            if (countCavalries > 0)
            {
                newDetachment.Cavalries = ((Cavalry, int))CalculateUnitForNewDetachment(
                    castle.CastleDetachment.Cavalries,
                    countCavalries);

                castle.CastleDetachment.Cavalries = ((Cavalry, int))RemoveUnitsFromSquad(
                    castle.CastleDetachment.Cavalries,
                    countCavalries);
            }

            newDetachment.SpeedDetachment = speed;

            if (detachmentEnemy != null)
            {
                newDetachment.IsPursues = true;
                newDetachment.TargetName = detachmentEnemy.Name;
            }

            castle.Detachments.Add(newDetachment);
        }

        private (BaseArmyUnit, int) RemoveUnitsFromSquad((BaseArmyUnit, int) units, int count)
        {
            units = (units.Item1, units.Item2 - count);

            return units;
        }

        private (BaseArmyUnit, int) CalculateUnitForNewDetachment((BaseArmyUnit, int) units, int count)
        {
            var newUnits = (units.Item1, count);

            return newUnits;
        }
    }
}
