using System;
using System.Collections.Generic;
using System.Linq;
using Strategy.GameLogic.Entities.Resources;
using Strategy.GameLogic.Exceptions;
using Strategy.GameLogic.Services;
using Strategy.GameLogic.Tools;

namespace Strategy.GameLogic
{
    public class Game
    {
        static Game()
        {
            Random = new Random();
        }

        public Game()
        {
            GameBoardSize = 9;
            MatrixBoard = new Cell[GameBoardSize, GameBoardSize];
            int CountEnemies = 3;
            Position[] positions = new Position[CountEnemies + 1];
            for (int i = 0; i < positions.Length; i++)
            {
                Position newPosition;
                do
                {
                    newPosition = new Position
                    {
                        I = Random.Next(0, GameBoardSize),
                        J = Random.Next(0, GameBoardSize),
                    };
                }
                while (IsThereAlreadyPosition(positions, newPosition));

                positions[i] = newPosition;
            }
            for (int i = 0; i < CountEnemies; i++)
                Enemies.Add(new Player(positions[i]));

            User = new Player(positions[CountEnemies]);
            BattleService = new BattleService();
            BuildingService = new BuildingService();
            ResourceService = new ResourceService();
            DetachmentService = new DetachmentService();
            ArmyService = new ArmyService();
            InformationBoard = new InformationBoard();
            Ai = new AiService(Enemies, User, BuildingService, DetachmentService, ArmyService);
            foreach (var enemy in Enemies)
                enemy.GameStyle = Ai.ChoiseGameStyle();

            UpdatePosition();
        }

        public static Random Random { get; }

        public Player User { get; }

        public List<Player> Enemies { get; set; } = new List<Player>();

        public static int GameBoardSize { get; private set; }

        public Cell[,] MatrixBoard { get; private set; }

        public AiService Ai { get; }

        public BattleService BattleService { get; }

        public BuildingService BuildingService { get; }

        public ResourceService ResourceService { get; }

        public DetachmentService DetachmentService { get; }

        public ArmyService ArmyService { get; }

        public InformationBoard InformationBoard { get; }

        public void UpdatePosition()
        {
            for (int i = 0; i < MatrixBoard.GetLength(0); i++)
                for (int j = 0; j < MatrixBoard.GetLength(1); j++)
                {
                    MatrixBoard[i, j] = new Cell();
                    MatrixBoard[i, j].Info = string.Empty;
                    MatrixBoard[i, j].I = i;
                    MatrixBoard[i, j].J = j;
                }

            var positionUser = User.Castle.Position;
            MatrixBoard[positionUser.I, positionUser.J].Info = ConstStrings.You;
            MatrixBoard[positionUser.I, positionUser.J].CastleYou = true;
            foreach (var detachment in User.Castle.Detachments)
            {
                var position = detachment.Position;
                MatrixBoard[position.I, position.J].Info = ConstStrings.YourUnit;
                MatrixBoard[position.I, position.J].CavalryYou = detachment.Cavalries.Item2 != 0;
                MatrixBoard[position.I, position.J].RecruitYou = detachment.Recruits.Item2 != 0;
                MatrixBoard[position.I, position.J].InfantrymanYou = detachment.Infantrymans.Item2 != 0;
                MatrixBoard[position.I, position.J].ShooterYou = detachment.Shooters.Item2 != 0;
            }

            foreach (var enemy in Enemies)
            {
                var positionEnemy = enemy.Castle.Position;
                MatrixBoard[positionEnemy.I, positionEnemy.J].Info = ConstStrings.Enemy;
                MatrixBoard[positionEnemy.I, positionEnemy.J].CastleEnemy = true;

                foreach (var detachmentEnemy in enemy.Castle.Detachments)
                {
                    var position = detachmentEnemy.Position;
                    MatrixBoard[position.I, position.J].Info = ConstStrings.EnemyUnit;
                    MatrixBoard[position.I, position.J].Cavalry = detachmentEnemy.Cavalries.Item2 != 0;
                    MatrixBoard[position.I, position.J].Recruit = detachmentEnemy.Recruits.Item2 != 0;
                    MatrixBoard[position.I, position.J].Infantryman = detachmentEnemy.Infantrymans.Item2 != 0;
                    MatrixBoard[position.I, position.J].Shooter = detachmentEnemy.Shooters.Item2 != 0;
                }
            }
        }

        public void ClearInformationBoard()
        {
            InformationBoard.Info = string.Empty;
        }

        public void NextMove()
        {
            Ai.NextMove();

            ArmyService.NextMove(User.Castle);

            foreach (var enemy in Enemies)
            {
                BuildingService.CalculateTaxFromPopulation(enemy.Castle);
                ResourceService.GenerateResourcesForNextStep(enemy.Castle);
                DetachmentService.MoveDetachmentToTarget(enemy.Castle, MatrixBoard);
                ArmyService.NextMove(enemy.Castle);
            }

            var detachmentsEnemy = new List<Detachment>();
            Enemies.ForEach(x => detachmentsEnemy.AddRange(x.Castle.Detachments));

            BuildingService.CalculateTaxFromPopulation(User.Castle);
            ResourceService.GenerateResourcesForNextStep(User.Castle);
            DetachmentService.MoveDetachmentToTarget(User.Castle, MatrixBoard, detachmentsEnemy);

            CheckMyBattle();
            CheckEnemyBattle();
            UpdateAfterBattle();
            HomecomingVictoriousDetachments();

            if (User.IsLost)
            {
                throw new LostException(ConstStrings.Lost);
            }

            if (Enemies.Count == 0)
            {
                throw new WinException(ConstStrings.Win);
            }
        }

        public (Detachment, bool) GetDetachmentTargetPosition(Position position)
        {
            foreach (var enemy in Enemies)
            {
                if (enemy.Castle.Position == position)
                {
                    return (enemy.Castle.CastleDetachment, false);
                }
                foreach (var detachmentEnemy in enemy.Castle.Detachments)
                {
                    if (detachmentEnemy.Position == position)
                    {
                        return (detachmentEnemy, true);
                    }
                }
            }

            return (null, false);
        }

        public bool IsCanTargetDetachment(Position position)
        {
            if (string.IsNullOrEmpty(MatrixBoard[position.I, position.J].Info))
                return false;

            return true;
        }

        private void HomecomingVictoriousDetachments()
        {
            foreach (var detachment in User.Castle.Detachments.ToArray())
            {
                if (detachment.IsWin)
                {
                    DetachmentService.MoveDetachmentToHome(detachment, User.Castle, MatrixBoard);
                }
            }

            foreach (var enemy in Enemies)
            {
                foreach (var detachmentEnemy in enemy.Castle.Detachments.ToArray())
                {
                    if (detachmentEnemy.IsWin)
                    {
                        DetachmentService.MoveDetachmentToHome(detachmentEnemy, enemy.Castle, MatrixBoard);
                    }
                }
            }

        }

        private void UpdateAfterBattle()
        {
            var deadDetachmentMys = User.Castle.Detachments.Where(x => x.IsDead);
            foreach (var detachmentMy in deadDetachmentMys.ToArray())
                User.Castle.Detachments.Remove(detachmentMy);

            foreach (var enemy in Enemies)
            {
                var deadDetachmentEnemys = enemy.Castle.Detachments.Where(x => x.IsDead);
                foreach (var detachmentEnemy in deadDetachmentEnemys.ToArray())
                    enemy.Castle.Detachments.Remove(detachmentEnemy);
            }

            var deadEnemys = Enemies.Where(x => x.IsLost);
            foreach (var enemy in deadEnemys.ToArray())
                Enemies.Remove(enemy);
        }

        private void CheckMyBattle()
        {
            foreach (var enemy in Enemies)
            {
                foreach (var detachmentEnemy in enemy.Castle.Detachments)
                {
                    if (detachmentEnemy.Position == User.Castle.Position)
                    {
                        bool isLost = BattleService.CalculateResultBattle(detachmentEnemy, User.Castle.CastleDetachment);

                        if (isLost)
                        {
                            detachmentEnemy.IsWin = true;
                            User.IsLost = true;
                        }
                        else
                        {
                            detachmentEnemy.IsDead = true;
                        }
                    }
                }
            }

            foreach (var detachmentMy in User.Castle.Detachments)
            {
                foreach (var enemy in Enemies)
                {
                    if (detachmentMy.Position == enemy.Castle.Position)
                    {
                        bool isLost = BattleService.CalculateResultBattle(enemy.Castle.CastleDetachment, detachmentMy);

                        if (isLost)
                        {

                            detachmentMy.IsDead = true;
                            InformationBoard.Info += ConstStrings.LostDetachment;
                        }
                        else
                        {
                            enemy.IsLost = true;
                            detachmentMy.IsWin = true;
                            BattleService.AssignVictoryAwardCoins(detachmentMy, enemy.Castle);

                            InformationBoard.Info += ConstStrings.WinDetachmentFromEnemy;
                        }
                    }

                    foreach (var detachmentEnemy in enemy.Castle.Detachments)
                    {
                        if (detachmentMy.Position == detachmentEnemy.Position)
                        {
                            bool isLost = BattleService.CalculateResultBattle(detachmentEnemy, detachmentMy);

                            if (isLost)
                            {
                                detachmentMy.IsDead = true;
                                detachmentEnemy.IsWin = true;
                                InformationBoard.Info += ConstStrings.LostDetachment;
                            }
                            else
                            {
                                detachmentEnemy.IsDead = true;
                                detachmentMy.IsWin = true;
                                InformationBoard.Info += ConstStrings.WinDetachment;
                            }
                        }
                    }
                }
            }
        }

        private void CheckEnemyBattle()
        {
            var detachmentsEnemy = new List<Detachment>();
            foreach (var enemy in Enemies)
                detachmentsEnemy.AddRange(enemy.Castle.Detachments);

            foreach (var enemy in Enemies)
            {
                foreach (var detachmentEnemy in detachmentsEnemy)
                {
                    if (detachmentEnemy.Position == enemy.Castle.Position)
                    {
                        bool isLost = BattleService.CalculateResultBattle(enemy.Castle.CastleDetachment, detachmentEnemy);

                        if (isLost)
                        {
                            detachmentEnemy.IsDead = true;
                        }
                        else
                        {
                            enemy.IsLost = true;
                            detachmentEnemy.IsWin = true;
                            BattleService.AssignVictoryAwardCoins(detachmentEnemy, enemy.Castle);
                        }
                    }
                }
            }

            for (int i = 1; i < detachmentsEnemy.Count; i++)
            {
                for (int j = 0; j < detachmentsEnemy.Count - i; j++)
                {
                    if (detachmentsEnemy[j].Position == detachmentsEnemy[j + 1].Position)
                    {
                        bool isLost = BattleService.CalculateResultBattle(detachmentsEnemy[j + 1], detachmentsEnemy[j]);

                        if (isLost)
                        {
                            detachmentsEnemy[j].IsDead = true;
                            detachmentsEnemy[j + 1].IsWin = true;
                        }
                        else
                        {
                            detachmentsEnemy[j].IsWin = true;
                            detachmentsEnemy[j + 1].IsDead = true;
                        }
                    }
                }
            }
        }

        private bool IsThereAlreadyPosition(Position[] positions, Position position)
        {
            foreach (var pos in positions)
            {
                if (!(pos is null) && pos == position)
                    return true;
            }

            return false;
        }
    }
}
