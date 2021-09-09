using Strategy.GameLogic;
using Strategy.GameLogic.Entities.Resources;
using Strategy.GameLogic.Exceptions;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BordScript : MonoBehaviour
{
    public static Game Game;
    public GameObject RowsPrfab;
    public GameObject CellPrfab;
    public bool LostOrWin = false;
    public bool IsMenu = false;
    public bool IsCreateDetachment = false;
    public bool IsBarracks = false;
    public bool IsResidentialBuildings = false;
    public bool IsTemple = false;
    public bool IsTownHall = false;
    public bool IsWalls = false;
    public GUIStyle styleLabel;
    public bool IsWin = false;
    public Texture2D TextureButton;
    public Texture2D TextureButtonHover;
    public Texture2D TextureBackground;

    private GUIStyle StyleButton;
    private GUIStyle StyleBack;

    public void NewGame()
    {
        Game = new Game();
        LostOrWin = false;
    }

    private void Start()
    {
        NewGame();
        for (int i = 0; i < Game.GameBoardSize; i++)
        {
            GameObject rows = Instantiate(RowsPrfab);
            rows.transform.SetParent(gameObject.transform, false);
            var panel = gameObject.transform.GetChild(i);

            for (int j = 0; j < Game.GameBoardSize; j++)
            {
                GameObject cell = Instantiate(CellPrfab);
                cell.transform.SetParent(panel.transform, false);
            }
        }
    }

    private void Update()
    {
        Game?.UpdatePosition();
        for (int i = 0; i < Game.GameBoardSize; i++)
        {
            var panel = gameObject.transform.GetChild(i);
            for (int j = 0; j < Game.GameBoardSize; j++)
            {
                var button = panel.transform.GetChild(j);
                var text = button.transform.GetComponentInChildren<Text>();
                text.fontSize = 11;
                text.text = Game.MatrixBoard[i, j].Info;
                var cell = button.transform.GetComponentInChildren<CellScriptClick>();
                cell.Cell = Game.MatrixBoard[i, j];
                cell.Bord = this;
            }
        }
    }

    void OnGUI()
    {
        if (Game != null && Game.User != null)
        {
            StyleButton = new GUIStyle(GUI.skin.button);
            StyleBack = new GUIStyle(GUI.skin.button);
            StyleBack.normal.background = TextureBackground;
            StyleBack.hover.background = TextureBackground;
            StyleBack.active.background = TextureBackground;
            StyleBack.focused.background = TextureBackground;
            StyleBack.onHover.background = TextureBackground;
            StyleBack.onActive.background = TextureBackground;
            StyleBack.onFocused.background = TextureBackground;
            StyleBack.onNormal.background = TextureBackground;
            StyleButton.normal.textColor = Color.white;
            StyleButton.normal.background = TextureButton;
            StyleButton.hover.background = TextureButtonHover;
            StyleButton.fontSize = 20;
            styleLabel.fontSize = 22;
            styleLabel.normal.textColor = Color.white;
            DrawLabelOutline(new Rect(15, 60, 500, 30), $"Coins: {Game.User.Castle.Resource.Coins}", 2, styleLabel);
            DrawLabelOutline(new Rect(15, 120, 500, 30), $"Population: {Game.User.Castle.Resource.Population}", 2, styleLabel);
            DrawLabelOutline(new Rect(15, 180, 500, 30), $"Recruits: {Game.User.Castle.CastleDetachment.Recruits.Item2} " +
                $"({Game.User.Castle.QueueDetachment.QueueRecruits.Select(x => x.Item2).Sum()})", 2, styleLabel);
            DrawLabelOutline(new Rect(15, 240, 500, 30), $"Shooters: {Game.User.Castle.CastleDetachment.Shooters.Item2} " +
                $"({Game.User.Castle.QueueDetachment.QueueShooters.Select(x => x.Item2).Sum()})", 2, styleLabel);
            DrawLabelOutline(new Rect(15, 300, 500, 30), $"Infantrys: {Game.User.Castle.CastleDetachment.Infantrymans.Item2} " +
                $"({Game.User.Castle.QueueDetachment.QueueInfantrymans.Select(x => x.Item2).Sum()})", 2, styleLabel);
            DrawLabelOutline(new Rect(15, 360, 500, 30), $"Cavaliers: {Game.User.Castle.CastleDetachment.Cavalries.Item2} " +
                $"({Game.User.Castle.QueueDetachment.QueueCavalries.Select(x => x.Item2).Sum()})", 2, styleLabel);
            if (!IsMenu)
            {
                if (!LostOrWin)
                {
                    if (GUI.Button(new Rect(50, Screen.height - 80, 150, 70), "Next Step", StyleButton))
                    {
                        try
                        {
                            Game.NextMove();
                        }
                        catch (LostException)
                        {
                            LostOrWin = true;
                            IsWin = false;
                        }
                        catch (WinException)
                        {
                            LostOrWin = true;
                            IsWin = true;
                        }
                        catch (Exception)
                        {
                            LostOrWin = true;
                            IsWin = false;
                        }
                    }
                    if (GUI.Button(new Rect(85, 460, 100, 50), "Town Hall", StyleButton))
                    {
                        IsMenu = true;
                        IsTownHall = true;
                    }
                    if (GUI.Button(new Rect(25, 520, 100, 50), "Temple", StyleButton))
                    {
                        IsMenu = true;
                        IsTemple = true;
                    }
                    if (GUI.Button(new Rect(25, 580, 100, 50), "Buildings", StyleButton))
                    {
                        IsMenu = true;
                        IsResidentialBuildings = true;
                    }
                    if (GUI.Button(new Rect(150, 520, 100, 50), "Walls", StyleButton))
                    {
                        IsMenu = true;
                        IsWalls = true;
                    }
                    if (GUI.Button(new Rect(150, 580, 100, 50), "Barracks", StyleButton))
                    {
                        IsMenu = true;
                        IsBarracks = true;
                    }
                }
                else
                {
                    GUI.Window(0, new Rect(0, 0, Screen.width, Screen.height), GameOverFunction, "Game Over");
                }
            }
            else
            {
                if (isChoice)
                {
                    if (IsCreateDetachment)
                    {
                        GUI.Window(0, new Rect(0, 0, Screen.width, Screen.height), CreateDetachmentFunction, "", StyleBack);
                    }
                }
                if (IsResidentialBuildings)
                {
                    GUI.Window(0, new Rect(0, 0, Screen.width, Screen.height), ResidentialBuildingsFunction, "", StyleBack);
                }
                if (IsTemple)
                {
                    GUI.Window(0, new Rect(0, 0, Screen.width, Screen.height), TempleFunction, "", StyleBack);
                }
                if (IsTownHall)
                {
                    GUI.Window(0, new Rect(0, 0, Screen.width, Screen.height), TownHallFunction, "", StyleBack);
                }
                if (IsWalls)
                {
                    GUI.Window(0, new Rect(0, 0, Screen.width, Screen.height), WallsFunction, "", StyleBack);
                }
                if (IsBarracks)
                {
                    GUI.Window(0, new Rect(0, 0, Screen.width, Screen.height), BarracksFunction, "", StyleBack);
                }
            }
        }
    }

    private float InputRecruit = 0.0f;
    private float InputCavalry = 0.0f;
    private float InputInfantryman = 0.0f;
    private float InputShooter = 0.0f;
    public (Detachment, bool) DetachmentEnemy;
    public bool isChoice;

    void CreateDetachmentFunction(int windowID)
    {
        int r = (int)InputRecruit;
        int c = (int)InputCavalry;
        int i = (int)InputInfantryman;
        int s = (int)InputShooter;

        DrawLabelOutline(new Rect(250, 60, 1000, 30),
            $"Want to send a squad to attack a position ({DetachmentEnemy.Item1?.Position?.I}:{DetachmentEnemy.Item1?.Position?.J}) ?", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 120, 500, 30), $"Recruits {r}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 180, 500, 30), $"Cavalrys {c}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 240, 500, 30), $"Infantrymans {i}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 300, 500, 30), $"Shooters {s}", 2, styleLabel);

        if (Game.User.Castle.CastleDetachment.Recruits.Item2 >= 1)
            InputRecruit = GUI.HorizontalSlider(new Rect(700, 120, 200, 30), InputRecruit, 0.0f, Game.User.Castle.CastleDetachment.Recruits.Item2);
        if (Game.User.Castle.CastleDetachment.Cavalries.Item2 >= 1)
            InputCavalry = GUI.HorizontalSlider(new Rect(700, 180, 200, 30), InputCavalry, 0.0f, Game.User.Castle.CastleDetachment.Cavalries.Item2);
        if (Game.User.Castle.CastleDetachment.Infantrymans.Item2 >= 1)
            InputInfantryman = GUI.HorizontalSlider(new Rect(700, 240, 200, 30), InputInfantryman, 0.0f, Game.User.Castle.CastleDetachment.Infantrymans.Item2);
        if (Game.User.Castle.CastleDetachment.Shooters.Item2 >= 1)
            InputShooter = GUI.HorizontalSlider(new Rect(700, 300, 200, 30), InputShooter, 0.0f, Game.User.Castle.CastleDetachment.Shooters.Item2);

        if (GUI.Button(new Rect(500, 360, 150, 50), "Send a squad", StyleButton))
        {
            try
            {
                if (DetachmentEnemy.Item2)
                {
                    Game.DetachmentService.CreateDetachment(Game.User.Castle, r, s, i, c, Game.User.Castle.Position,
                        DetachmentEnemy.Item1.Position, DetachmentEnemy.Item1);
                    isChoice = false;
                    DetachmentEnemy = (null, false);
                }
                else
                {
                    Game.DetachmentService.CreateDetachment(Game.User.Castle, r, s, i, c, Game.User.Castle.Position,
                        DetachmentEnemy.Item1.Position);
                    isChoice = false;
                    DetachmentEnemy = (null, false);
                }
                IsMenu = false;
                IsCreateDetachment = false;
                InputRecruit = 0;
                InputCavalry = 0;
                InputInfantryman = 0;
                InputShooter = 0;
            }
            catch (Exception)
            {
            }
        }

        if (GUI.Button(new Rect(50, Screen.height - 80, 150, 70), "Exit", StyleButton))
        {
            IsMenu = false;
            IsCreateDetachment = false;
        }
    }

    void ResidentialBuildingsFunction(int windowID)
    {
        DrawLabelOutline(new Rect(250, 60, 500, 30), $"Buildings Level: {Game.User.Castle.ResidentialBuildings.CurrentLvl}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 120, 500, 30), $"Upgrade cost: {Game.User.Castle.ResidentialBuildings.CalculateCoinsRequiredForLvlUp()}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 180, 500, 30), $"Population limit: {Game.User.Castle.ResidentialBuildings.MaxCurrentPopulace}", 2, styleLabel);
        DrawLabelOutline(new Rect(600, 60, 800, 30), $"Increases population limit", 2, styleLabel);

        if (GUI.Button(new Rect(250, 360, 100, 50), "Level Up", StyleButton))
        {
            try
            {
                Game.BuildingService.AddLevelForBuilding(Game.User.Castle.ResidentialBuildings, Game.User.Castle);
            }
            catch (Exception)
            {
            }
        }

        if (GUI.Button(new Rect(50, Screen.height - 80, 150, 70), "Exit", StyleButton))
        {
            IsMenu = false;
            IsResidentialBuildings = false;
        }
    }

    void TempleFunction(int windowID)
    {
        DrawLabelOutline(new Rect(250, 60, 500, 30), $"Temple Level: {Game.User.Castle.Temple.CurrentLvl}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 120, 500, 30), $"Upgrade cost: {Game.User.Castle.Temple.CalculateCoinsRequiredForLvlUp()}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 180, 500, 30), $"Attack bonus for squad in attack: {Game.User.Castle.Temple.AttackForArmyOnAttack}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 240, 500, 30), $"Attack bonus for squad in defensive: {Game.User.Castle.Temple.AttackForArmyOnDefensive}", 2, styleLabel);
        DrawLabelOutline(new Rect(600, 60, 800, 30), $"Increases attack of squad in attack and increases attack of squad in defense", 2, styleLabel);

        if (GUI.Button(new Rect(250, 360, 100, 50), "Level Up", StyleButton))
        {
            try
            {
                Game.BuildingService.AddLevelForBuilding(Game.User.Castle.Temple, Game.User.Castle);
            }
            catch (Exception)
            {
            }
        }

        if (GUI.Button(new Rect(50, Screen.height - 80, 150, 70), "Exit", StyleButton))
        {
            IsMenu = false;
            IsTemple = false;
        }
    }

    private string InputRecruitBox;
    void TownHallFunction(int windowID)
    {
        DrawLabelOutline(new Rect(250, 60, 500, 30), $"TownHall Level: {Game.User.Castle.TownHall.CurrentLvl}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 120, 500, 30), $"Upgrade cost: {Game.User.Castle.TownHall.CalculateCoinsRequiredForLvlUp()}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 180, 500, 30), $"Current Tax: {Game.User.Castle.TownHall.TaxPercentage}%", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 240, 500, 30), $"Hiring Recruit cost: {Game.User.Castle.TownHall.HireRecruit().HiringCost}", 2, styleLabel);
        DrawLabelOutline(new Rect(600, 60, 800, 30), $"Brings income in form of tax and allows you to hire recruits", 2, styleLabel);

        InputRecruitBox = GUI.TextField(new Rect(600, 240, 100, 30), InputRecruitBox);
        if (GUI.Button(new Rect(750, 240, 180, 50), "Hiring Recruit", StyleButton))
        {
            try
            {
                if (!int.TryParse(InputRecruitBox, out int n))
                    return;
                Game.ArmyService.HireRecruitToArmy(Game.User.Castle, n);
            }
            catch (Exception)
            {
            }
        }

        if (GUI.Button(new Rect(250, 360, 100, 50), "Level Up", StyleButton))
        {
            try
            {
                Game.BuildingService.AddLevelForBuilding(Game.User.Castle.TownHall, Game.User.Castle);
            }
            catch (Exception)
            {
            }
        }

        if (GUI.Button(new Rect(50, Screen.height - 80, 150, 70), "Exit", StyleButton))
        {
            IsMenu = false;
            IsTownHall = false;
        }
    }

    void WallsFunction(int windowID)
    {
        DrawLabelOutline(new Rect(250, 60, 500, 30), $"Walls Level: {Game.User.Castle.Walls.CurrentLvl}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 120, 500, 30), $"Upgrade cost: {Game.User.Castle.Walls.CalculateCoinsRequiredForLvlUp()}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 180, 500, 30), $"Defense of army on defensive: {Game.User.Castle.Walls.DefenseForArmy}", 2, styleLabel);
        DrawLabelOutline(new Rect(600, 60, 800, 30), $"Increases defense of castle in defense and increases defense of army in castle", 2, styleLabel);

        if (GUI.Button(new Rect(250, 360, 100, 50), "Level Up", StyleButton))
        {
            try
            {
                Game.BuildingService.AddLevelForBuilding(Game.User.Castle.Walls, Game.User.Castle);
            }
            catch (Exception)
            {
            }
        }

        if (GUI.Button(new Rect(50, Screen.height - 80, 150, 70), "Exit", StyleButton))
        {
            IsMenu = false;
            IsWalls = false;
        }
    }

    private string InputCavalryBox;
    private string InputInfantrymanBox;
    private string InputShooterBox;
    void BarracksFunction(int windowID)
    {
        DrawLabelOutline(new Rect(250, 60, 500, 30), $"Barracks Level: {Game.User.Castle.Barracks.CurrentLvl}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 120, 500, 30), $"Upgrade cost: {Game.User.Castle.Barracks.CalculateCoinsRequiredForLvlUp()}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 180, 800, 30), $"Hiring Cavalry cost: {Game.User.Castle.Barracks.HireCavalry(true).HiringCost}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 240, 800, 30), $"Hiring Infantryman cost: {Game.User.Castle.Barracks.HireInfantryman(true).HiringCost}", 2, styleLabel);
        DrawLabelOutline(new Rect(250, 300, 800, 30), $"Hiring Shooter cost: {Game.User.Castle.Barracks.HireShooter(true).HiringCost}", 2, styleLabel);
        DrawLabelOutline(new Rect(600, 60, 500, 30), $" Hires heroes", 2, styleLabel);

        InputCavalryBox = GUI.TextField(new Rect(600, 180, 100, 30), InputCavalryBox);
        InputInfantrymanBox = GUI.TextField(new Rect(600, 240, 100, 30), InputInfantrymanBox);
        InputShooterBox = GUI.TextField(new Rect(600, 300, 100, 30), InputShooterBox);

        if (GUI.Button(new Rect(750, 180, 200, 50), "Hiring Cavalry", StyleButton))
        {
            try
            {
                if (!int.TryParse(InputCavalryBox, out int n))
                    return;
                Game.ArmyService.HireCavalryToArmy(Game.User.Castle, n);
            }
            catch (Exception)
            {
            }
        }
        if (GUI.Button(new Rect(750, 240, 200, 50), "Hiring Infantryman", StyleButton))
        {
            try
            {
                if (!int.TryParse(InputInfantrymanBox, out int n))
                    return;
                Game.ArmyService.HireInfantrymanToArmy(Game.User.Castle, n);
            }
            catch (Exception)
            {
            }
        }
        if (GUI.Button(new Rect(750, 300, 200, 50), "Hiring Shooter", StyleButton))
        {
            try
            {
                if (!int.TryParse(InputShooterBox, out int n))
                    return;
                Game.ArmyService.HireShooterToArmy(Game.User.Castle, n);
            }
            catch (Exception)
            {
            }
        }
        if (GUI.Button(new Rect(250, 360, 200, 50), "Level Up", StyleButton))
        {
            try
            {
                Game.BuildingService.AddLevelForBuilding(Game.User.Castle.Barracks, Game.User.Castle);
            }
            catch (Exception)
            {
            }
        }

        if (GUI.Button(new Rect(50, Screen.height - 80, 150, 70), "Exit", StyleButton))
        {
            IsMenu = false;
            IsBarracks = false;
        }
    }

    void GameOverFunction(int windowID)
    {
        if (IsWin)
            DrawLabelOutline(new Rect(250, 60, 500, 30), $"WIN", 2, styleLabel);
        else
            DrawLabelOutline(new Rect(250, 60, 500, 30), $"LOSS", 2, styleLabel);

        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 4, 100, 50), "Restart", StyleButton))
        {
            NewGame();
        }
    }

    void DrawLabelOutline(Rect rect, string text, int strength, GUIStyle style)
    {
        DrawOutline(rect, text, strength, style);
        GUI.Label(rect, text, style);
    }
    void DrawOutline(Rect rect, string text, int strength, GUIStyle style)
    {
        GUI.color = new Color(0, 0, 0, 1);
        int i;
        for (i = -strength; i <= strength; i++)
        {
            GUI.Label(new Rect(rect.x - strength, rect.y + i, rect.width, rect.height), text, style);
            GUI.Label(new Rect(rect.x + strength, rect.y + i, rect.width, rect.height), text, style);
        }
        for (i = -strength + 1; i <= strength - 1; i++)
        {
            GUI.Label(new Rect(rect.x + i, rect.y - strength, rect.width, rect.height), text, style);
            GUI.Label(new Rect(rect.x + i, rect.y + strength, rect.width, rect.height), text, style);
        }
        GUI.color = new Color(1, 1, 1, 1);
    }
}
