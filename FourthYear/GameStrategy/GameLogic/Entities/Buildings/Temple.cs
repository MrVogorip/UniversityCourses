using Strategy.GameLogic.Entities.Buildings.Base;

namespace Strategy.GameLogic.Entities.Buildings
{
    public class Temple : BaseBuilding
    {
        public Temple()
        {
            CurrentLvl = 1;
            MaxLvl = 10;
            CoefficientForImprovement = 750;
            AttackForArmyOnAttack = 0;
            MaxAttackForArmyOnAttack = 3;
            AttackForArmyOnDefensive = 0;
            MaxAttackForArmyOnDefensive = 5;
        }

        public int AttackForArmyOnAttack { get; set; }

        public int MaxAttackForArmyOnAttack { get; set; }

        public int AttackForArmyOnDefensive { get; set; }

        public int MaxAttackForArmyOnDefensive { get; set; }

        public override void AddLevel()
        {
            base.AddLevel();
            if (CurrentLvl <= MaxLvl)
            {
                if (CurrentLvl % 3 == 0)
                {
                    AttackForArmyOnAttack++;
                    if (AttackForArmyOnAttack >= MaxAttackForArmyOnAttack)
                    {
                        AttackForArmyOnAttack = MaxAttackForArmyOnAttack;
                    }
                }

                if (CurrentLvl % 2 == 0)
                {
                    AttackForArmyOnDefensive++;
                    if (AttackForArmyOnDefensive >= MaxAttackForArmyOnDefensive)
                    {
                        AttackForArmyOnDefensive = MaxAttackForArmyOnDefensive;
                    }
                }
            }
        }
    }
}
