using System;
using System.Collections.Generic;
using System.Linq;
using Strategy.GameLogic.Entities;
using Strategy.GameLogic.Entities.Resources;
using Strategy.GameLogic.Tools;

namespace Strategy.GameLogic.Services
{
    public class BattleService
    {
        public void AssignVictoryAwardCoins(Detachment detachment, Castle castle)
        {
            detachment.VictoryAwardCoins = castle.Resource.Coins / 2;
        }

        public bool CalculateResultBattle(Detachment detachment1, Detachment detachment2)
        {
            if (CheckIsEmptyDetachment(detachment1))
            {
                return false;
            }

            if (CheckIsEmptyDetachment(detachment2))
            {
                return true;
            }

            int countStepBattle = 0;

            do
            {
                Stack<(int, string)> stackAttackPower1 = CreateStackAttackPower(detachment1);
                Stack<(int, string)> stackInitiatives1 = CreateStackInitiatives(detachment1);
                List<(int, string)> listDefenseSquad1 = CreateListDefenseSquad(detachment1);

                Stack<(int, string)> stackAttackPower2 = CreateStackAttackPower(detachment2);
                Stack<(int, string)> stackInitiatives2 = CreateStackInitiatives(detachment2);
                List<(int, string)> listDefenseSquad2 = CreateListDefenseSquad(detachment2);

                if (stackInitiatives2.Count == 0)
                {
                    return false;
                }

                var init2 = stackInitiatives2.Pop();

                if (stackInitiatives1.Count == 0)
                {
                    return true;
                }

                var init1 = stackInitiatives1.Pop();

                if (countStepBattle % 2 == 0)
                {
                    (int, string) attackSquad2 = stackAttackPower2.FirstOrDefault(x => x.Item2 == init2.Item2);
                    (int, string) attackSquad1 = stackAttackPower1.FirstOrDefault(x => x.Item2 == init1.Item2);
                    (int, string) protectionSquad2 = listDefenseSquad2.Min();
                    CreateSquadLoss(detachment2, protectionSquad2, attackSquad1);
                    if (attackSquad1.Item2 != ConstStrings.Shooters)
                    {
                        var protectionSquad1 = listDefenseSquad1.Min();
                        var retaliatoryStrike = CreateRetaliatoryStrike(detachment2, protectionSquad1.Item2);
                        CreateSquadLoss(detachment1, attackSquad1, retaliatoryStrike);
                    }
                }
                else
                {
                    (int, string) attackSquad2 = stackAttackPower2.FirstOrDefault(x => x.Item2 == init2.Item2);
                    (int, string) attackSquad1 = stackAttackPower1.FirstOrDefault(x => x.Item2 == init1.Item2);
                    (int, string) protectionSquad1 = listDefenseSquad1.Min();
                    CreateSquadLoss(detachment1, protectionSquad1, attackSquad2);
                    if (attackSquad2.Item2 != ConstStrings.Shooters)
                    {
                        var protectionSquad2 = listDefenseSquad1.Min();
                        var retaliatoryStrike = CreateRetaliatoryStrike(detachment1, protectionSquad2.Item2);
                        CreateSquadLoss(detachment2, attackSquad2, retaliatoryStrike);
                    }
                }
      
                countStepBattle++;

            } while (!CheckIsEmptyDetachment(detachment1) && !CheckIsEmptyDetachment(detachment2));

            return CheckIsEmptyDetachment(detachment2);
        }

        private bool CheckIsEmptyDetachment(Detachment detachment)
        {
            bool result = false;

            if (detachment.Recruits.Item2 == 0 &&
                detachment.Shooters.Item2 == 0 &&
                detachment.Infantrymans.Item2 == 0 &&
                detachment.Cavalries.Item2 == 0)
            {
                result = true;
            }

            return result;
        }

        private Stack<(int, string)> CreateStackAttackPower(Detachment detachment)
        {
            PriorityQueue queueAttackPower = new PriorityQueue();

            if (detachment.Recruits.Item2 > 0)
            {
                int attackPower = detachment.Recruits.Item2 * (detachment.Recruits.Item1.Attack + detachment.AttackerAttackBonus);
                queueAttackPower.Enqueue((attackPower, ConstStrings.Recruits));
            }
            if (detachment.Shooters.Item2 > 0)
            {
                int attackPower = detachment.Shooters.Item2 * (detachment.Shooters.Item1.Attack + detachment.AttackerAttackBonus);
                queueAttackPower.Enqueue((attackPower, ConstStrings.Shooters));
            }
            if (detachment.Infantrymans.Item2 > 0)
            {
                int attackPower = detachment.Infantrymans.Item2 * (detachment.Infantrymans.Item1.Attack + detachment.AttackerAttackBonus);
                queueAttackPower.Enqueue((attackPower, ConstStrings.Infantrymans));
            }
            if (detachment.Cavalries.Item2 > 0)
            {
                int attackPower = detachment.Cavalries.Item2 * (detachment.Cavalries.Item1.Attack + detachment.AttackerAttackBonus);
                queueAttackPower.Enqueue((attackPower, ConstStrings.Cavalries));
            }

            int sizeQueueAttackPowerEnemy = queueAttackPower.Count();
            Stack<(int, string)> stackAttackPower = new Stack<(int, string)>();
            for (int i = 0; i < sizeQueueAttackPowerEnemy; i++)
            {
                stackAttackPower.Push(queueAttackPower.Dequeue());
            }

            return stackAttackPower;
        }

        private Stack<(int, string)> CreateStackInitiatives(Detachment detachment)
        {
            PriorityQueue queueInitiatives = new PriorityQueue();

            if (detachment.Recruits.Item2 > 0)
            {
                queueInitiatives.Enqueue((detachment.Recruits.Item1.SpeedAndInitiative * detachment.Recruits.Item2, ConstStrings.Recruits));
            }
            if (detachment.Shooters.Item2 > 0)
            {
                queueInitiatives.Enqueue((detachment.Shooters.Item1.SpeedAndInitiative * detachment.Recruits.Item2, ConstStrings.Shooters));
            }
            if (detachment.Infantrymans.Item2 > 0)
            {
                queueInitiatives.Enqueue((detachment.Infantrymans.Item1.SpeedAndInitiative * detachment.Recruits.Item2, ConstStrings.Infantrymans));
            }
            if (detachment.Cavalries.Item2 > 0)
            {
                queueInitiatives.Enqueue((detachment.Cavalries.Item1.SpeedAndInitiative * detachment.Recruits.Item2, ConstStrings.Cavalries));
            }

            int sizeQueueAttackPowerEnemy = queueInitiatives.Count();
            Stack<(int, string)> stackInitiatives = new Stack<(int, string)>();
            for (int i = 0; i < sizeQueueAttackPowerEnemy; i++)
            {
                stackInitiatives.Push(queueInitiatives.Dequeue());
            }

            return stackInitiatives;
        }

        private List<(int, string)> CreateListDefenseSquad(Detachment detachment)
        {
            List<(int, string)> listDefenseSquad = new List<(int, string)>();

            if (detachment.Recruits.Item2 > 0)
            {
                int protectionPower = detachment.Recruits.Item2 * (detachment.Recruits.Item1.Protection + detachment.AttackerProtectionBonus);
                listDefenseSquad.Add((protectionPower, ConstStrings.Recruits));
            }
            if (detachment.Shooters.Item2 > 0)
            {
                int protectionPower = detachment.Shooters.Item2 * (detachment.Shooters.Item1.Protection + detachment.AttackerProtectionBonus);
                listDefenseSquad.Add((protectionPower, ConstStrings.Shooters));
            }
            if (detachment.Infantrymans.Item2 > 0)
            {
                int protectionPower = detachment.Infantrymans.Item2 * (detachment.Infantrymans.Item1.Protection + detachment.AttackerProtectionBonus);
                listDefenseSquad.Add((protectionPower, ConstStrings.Infantrymans));
            }
            if (detachment.Cavalries.Item2 > 0)
            {
                int protectionPower = detachment.Cavalries.Item2 * (detachment.Cavalries.Item1.Protection + detachment.AttackerProtectionBonus);
                listDefenseSquad.Add((protectionPower, ConstStrings.Cavalries));
            }

            return listDefenseSquad;
        }

        private (int, string) CreateRetaliatoryStrike(Detachment detachment, string name)
        {
            (int, string) retaliatoryStrikeResult = (0, string.Empty);

            if (detachment.Recruits.Item2 > 0 && name == ConstStrings.Recruits)
            {
                int retaliatoryStrike = (int)(detachment.Recruits.Item2 * (detachment.Recruits.Item1.Protection + detachment.AttackerAttackBonus) * 0.75);
                retaliatoryStrikeResult = (retaliatoryStrike, name);
            }
            if (detachment.Infantrymans.Item2 > 0 && name == ConstStrings.Infantrymans)
            {
                int retaliatoryStrike = (int)(detachment.Infantrymans.Item2 * (detachment.Infantrymans.Item1.Protection + detachment.AttackerAttackBonus) * 0.75);
                retaliatoryStrikeResult = (retaliatoryStrike, name);
            }
            if (detachment.Cavalries.Item2 > 0 && name == ConstStrings.Cavalries)
            {
                int retaliatoryStrike = (int)(detachment.Cavalries.Item2 * (detachment.Cavalries.Item1.Protection + detachment.AttackerAttackBonus) * 0.75);
                retaliatoryStrikeResult = (retaliatoryStrike, name);
            }

            return retaliatoryStrikeResult;
        }

        private void CreateSquadLoss(Detachment detachment, (int, string) protectionSquad, (int, string) squadEnemy)
        {
            if (protectionSquad.Item2 == ConstStrings.Recruits)
            {
                int protection = detachment.Recruits.Item1.Protection;
                int squadLoss = (protectionSquad.Item1 - squadEnemy.Item1) / (protection + detachment.AttackerProtectionBonus);
                squadLoss = Math.Abs(squadLoss);
                if (squadLoss == 0)
                {
                    squadLoss = 1;
                }
                if (squadLoss >= detachment.Recruits.Item2)
                {
                    squadLoss = detachment.Recruits.Item2;
                }
                detachment.Recruits = (detachment.Recruits.Item1, detachment.Recruits.Item2 - squadLoss);
            }
            if (protectionSquad.Item2 == ConstStrings.Shooters)
            {
                int protection = detachment.Shooters.Item1.Protection;
                int squadLoss = (protectionSquad.Item1 - squadEnemy.Item1) / (protection + detachment.AttackerProtectionBonus);
                squadLoss = Math.Abs(squadLoss);
                if (squadLoss == 0)
                {
                    squadLoss = 1;
                }
                if (squadLoss >= detachment.Shooters.Item2)
                {
                    squadLoss = detachment.Shooters.Item2;
                }
                detachment.Shooters = (detachment.Shooters.Item1, detachment.Shooters.Item2 - squadLoss);
            }
            if (protectionSquad.Item2 == ConstStrings.Infantrymans)
            {
                int protection = detachment.Infantrymans.Item1.Protection;
                int squadLoss = (protectionSquad.Item1 - squadEnemy.Item1) / (protection + detachment.AttackerProtectionBonus);
                squadLoss = Math.Abs(squadLoss);
                if (squadLoss == 0)
                {
                    squadLoss = 1;
                }
                if (squadLoss >= detachment.Infantrymans.Item2)
                {
                    squadLoss = detachment.Infantrymans.Item2;
                }
                detachment.Infantrymans = (detachment.Infantrymans.Item1, detachment.Infantrymans.Item2 - squadLoss);
            }
            if (protectionSquad.Item2 == ConstStrings.Cavalries)
            {
                int protection = detachment.Cavalries.Item1.Protection;
                int squadLoss = (protectionSquad.Item1 - squadEnemy.Item1) / (protection + detachment.AttackerProtectionBonus);
                squadLoss = Math.Abs(squadLoss);
                if (squadLoss == 0)
                {
                    squadLoss = 1;
                }
                if (squadLoss >= detachment.Cavalries.Item2)
                {
                    squadLoss = detachment.Cavalries.Item2;
                }
                detachment.Cavalries = (detachment.Cavalries.Item1, detachment.Cavalries.Item2 - squadLoss);
            }
        }
    }
}
