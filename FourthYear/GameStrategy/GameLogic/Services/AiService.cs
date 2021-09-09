using DecisionMaking;
using System;
using System.Collections.Generic;
using System.Linq;
using Strategy.GameLogic.Services.GameStyle;
using System.Diagnostics;

namespace Strategy.GameLogic.Services
{
    public class AiService
    {
        private readonly List<Player> _enemies;
        private readonly Player _user;
        private readonly BuildingService _buildingService;
        private readonly DetachmentService _detachmentService;
        private readonly ArmyService _armyService;

        public AiService(
            List<Player> enemies,
            Player user,
            BuildingService buildingService,
            DetachmentService detachmentService,
            ArmyService armyService)
        {
            _enemies = enemies;
            _user = user;
            _buildingService = buildingService;
            _detachmentService = detachmentService;
            _armyService = armyService;
        }

        public void NextMove()
        {
            var matrix = new List<List<double>>();
            var alternativeNames = new List<string>()
            {
                "army", "attack", "building"
            };
            var natureStateNames = new List<string>()
            {
                "Builder", "Normal", "Aggressor"
            };

            var detachmentsCounts = _enemies.Select(x => x.Castle.Detachments.Count).Sum();
            var army = new List<double>();
            var attack = new List<double>();
            var building = new List<double>();
            foreach (var enemy in _enemies)
            {
               int coinsAll = enemy.Castle.Resource.Coins;
                var summ =
                    enemy.Castle.Walls.CalculateCoinsRequiredForLvlUp() +
                    enemy.Castle.Temple.CalculateCoinsRequiredForLvlUp() +
                    enemy.Castle.Barracks.CalculateCoinsRequiredForLvlUp() +
                    enemy.Castle.ResidentialBuildings.CalculateCoinsRequiredForLvlUp() +
                    enemy.Castle.TownHall.CalculateCoinsRequiredForLvlUp();

                var detCout = enemy.Castle.Detachments.Count + coinsAll/ enemy.Castle.Barracks.CalculateCoinsRequiredForLvlUp();
                var detDiv = detachmentsCounts / (enemy.Castle.Detachments.Count + 1) + enemy.Castle.Barracks.CurrentLvl;

                army.Add(detCout <= 2 ? 1 : detCout);
                attack.Add(detDiv <= 1 ? 1 : detDiv);
                building.Add(summ / (coinsAll+1));
            }

            matrix.Add(army);
            matrix.Add(attack);
            matrix.Add(building);

            var matrixGame = new GameMatrix(matrix, alternativeNames, natureStateNames);
            ICriteria waldCriteria = new WaldCriteria(matrixGame);
            var optimum = waldCriteria.Optimum().First();
            string info = "Matrix:\n";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    info += matrixGame.Matrix[i][j] + " ";
                }
                info += "\n";
            }
            info += "Optimum: ";
            for (int i = 0; i < 3; i++)
            {
                info += optimum.Values[i] + " ";
            }
            Debug.WriteLine(info);

            for (int i = 0; i < _enemies.Count; i++)
            {
                try
                {
                    if (optimum.Name == alternativeNames[0])
                        UpdateArmy(_enemies[i]);
                    if (optimum.Name == alternativeNames[1])
                        CreateAttack(_enemies[i]);
                    if (optimum.Name == alternativeNames[2])
                        UpdateBuilding(_enemies[i]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public IGameStyle ChoiseGameStyle()
        {
            if (Game.Random.Next(0, 101) < 40)
                return new StyleAggressor();

            return new StyleBuilder();
        }

        private void UpdateBuilding(Player enemy)
        {
            int coinsAllNow = enemy.Castle.Resource.Coins;
            int coinsBuilding = enemy.GameStyle.CalculateCoinsForBuildings(coinsAllNow);
            while (coinsAllNow > coinsBuilding)
            {
                int choiceBuilding = Game.Random.Next(0, 5);
                switch (choiceBuilding)
                {
                    case 0:
                        _buildingService.AddLevelForBuilding(enemy.Castle.TownHall, enemy.Castle);
                        break;
                    case 1:
                        _buildingService.AddLevelForBuilding(enemy.Castle.ResidentialBuildings, enemy.Castle);
                        break;
                    case 2:
                        _buildingService.AddLevelForBuilding(enemy.Castle.Walls, enemy.Castle);
                        break;
                    case 3:
                        _buildingService.AddLevelForBuilding(enemy.Castle.Temple, enemy.Castle);
                        break;
                    case 4:
                        _buildingService.AddLevelForBuilding(enemy.Castle.Barracks, enemy.Castle);
                        break;
                    default:
                        break;
                }

                coinsAllNow = enemy.Castle.Resource.Coins;
            }
        }

        public void UpdateArmy(Player enemy)
        {
            if (true)
            {
                int coinsAllNow = enemy.Castle.Resource.Coins;
                int coinsForArmy = enemy.GameStyle.CalculateCoinsForArmy(coinsAllNow);
                while (coinsAllNow > coinsForArmy)
                {
                    int choiceArmy = Game.Random.Next(0, 4);

                    switch (choiceArmy)
                    {
                        case 0:
                            int n1 = coinsForArmy / enemy.Castle.TownHall.HireRecruit().HiringCost;
                            _armyService.HireRecruitToArmy(enemy.Castle, n1);
                            break;
                        case 1:
                            int n2 = coinsForArmy / enemy.Castle.Barracks.HireShooter(true).HiringCost;
                            _armyService.HireShooterToArmy(enemy.Castle, n2);
                            break;
                        case 2:
                            if (enemy.Castle.Barracks.CurrentLvl >= 4)
                            {
                                int n3 = coinsForArmy / enemy.Castle.Barracks.HireInfantryman(true).HiringCost;
                                _armyService.HireInfantrymanToArmy(enemy.Castle, n3);
                            }
                            break;
                        case 3:
                            if (enemy.Castle.Barracks.CurrentLvl >= 7)
                            {
                                int n4 = coinsForArmy / enemy.Castle.Barracks.HireCavalry(true).HiringCost;
                                _armyService.HireCavalryToArmy(enemy.Castle, n4);
                            }
                            break;
                        default:
                            break;
                    }

                    coinsAllNow = enemy.Castle.Resource.Coins;
                }
            }
        }

        private void CreateAttack(Player enemy)
        {
            if (_enemies.Count > 0)
            {
                if (true)
                {
                    int indexEnemy = _enemies.IndexOf(enemy);
                    int choiceBattles = indexEnemy;
                    while (choiceBattles == indexEnemy)
                    {
                        choiceBattles = Game.Random.Next(0, _enemies.Count + 1);
                        if (_enemies.Count == 1)
                        {
                            choiceBattles = _enemies.Count;
                            break;
                        }
                    }
                    if (choiceBattles < _enemies.Count)
                    {
                        int r = Game.Random.Next((enemy.Castle.CastleDetachment.Recruits.Item2 / 20) * 19, enemy.Castle.CastleDetachment.Recruits.Item2);
                        int s = Game.Random.Next((enemy.Castle.CastleDetachment.Shooters.Item2 / 20) * 19, enemy.Castle.CastleDetachment.Shooters.Item2);
                        int i = Game.Random.Next((enemy.Castle.CastleDetachment.Infantrymans.Item2 / 20) * 19, enemy.Castle.CastleDetachment.Infantrymans.Item2);
                        int c = Game.Random.Next((enemy.Castle.CastleDetachment.Cavalries.Item2 / 20) * 19, enemy.Castle.CastleDetachment.Cavalries.Item2);

                        if (Game.Random.Next(0, 101) < 25)
                        {
                            int choiceWhomAttack = Game.Random.Next(0, _enemies[choiceBattles].Castle.Detachments.Count);

                            _detachmentService.CreateDetachment(enemy.Castle, r, s, i, c,
                                enemy.Castle.Position,
                                _enemies[choiceBattles].Castle.Detachments[choiceWhomAttack].Position);
                        }
                        else
                        {
                            _detachmentService.CreateDetachment(enemy.Castle, r, s, i, c,
                                enemy.Castle.Position,
                                _enemies[choiceBattles].Castle.Position);
                        }
                    }
                    else
                    {
                        int r = Game.Random.Next(enemy.Castle.CastleDetachment.Recruits.Item2 / 3, enemy.Castle.CastleDetachment.Recruits.Item2 / 2);
                        int s = Game.Random.Next(enemy.Castle.CastleDetachment.Shooters.Item2 / 3, enemy.Castle.CastleDetachment.Shooters.Item2 / 2);
                        int i = Game.Random.Next(enemy.Castle.CastleDetachment.Infantrymans.Item2 / 3, enemy.Castle.CastleDetachment.Infantrymans.Item2 / 2);
                        int c = Game.Random.Next(enemy.Castle.CastleDetachment.Cavalries.Item2 / 3, enemy.Castle.CastleDetachment.Cavalries.Item2 / 2);

                        if (Game.Random.Next(0, 101) < 25)
                        {
                            int choiceWhomAttack = Game.Random.Next(0, _enemies[choiceBattles].Castle.Detachments.Count);

                            _detachmentService.CreateDetachment(enemy.Castle, r, s, i, c,
                                enemy.Castle.Position,
                                _user.Castle.Detachments[choiceWhomAttack].Position);
                        }
                        else
                        {
                            _detachmentService.CreateDetachment(enemy.Castle, r, s, i, c,
                                enemy.Castle.Position,
                                _user.Castle.Position);
                        }
                    }
                }
            }
        }
    }
}
