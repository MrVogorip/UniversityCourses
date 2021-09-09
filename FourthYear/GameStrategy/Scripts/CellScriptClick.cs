using Strategy.GameLogic;
using Strategy.GameLogic.Tools;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellScriptClick : MonoBehaviour, IPointerClickHandler
{
    public Cell Cell;
    public GameObject Recruit;
    public GameObject Cavalry;
    public GameObject Infantryman;
    public GameObject Shooter;
    public GameObject MyRecruit;
    public GameObject MyCavalry;
    public GameObject MyInfantryman;
    public GameObject MyShooter;
    public GameObject Castle;
    public GameObject MyCastle;
    public BordScript Bord;

    void Update()
    {
        if (Cell != null)
        {
            if (Cell.CastleYou)
            {
                MyCastle.SetActive(true);
            }
            else
            {
                MyCastle.SetActive(false);
            }
            if (Cell.CastleEnemy)
            {
                Castle.SetActive(true);
            }
            else
            {
                Castle.SetActive(false);
            }
            if (Cell.Cavalry)
            {
                Cavalry.SetActive(true);
            }
            else
            {
                Cavalry.SetActive(false);
            }
            if (Cell.Recruit)
            {
                Recruit.SetActive(true);
            }
            else
            {
                Recruit.SetActive(false);
            }
            if (Cell.Infantryman)
            {
                Infantryman.SetActive(true);
            }
            else
            {
                Infantryman.SetActive(false);
            }
            if (Cell.Shooter)
            {
                Shooter.SetActive(true);
            }
            else
            {
                Shooter.SetActive(false);
            }

            if (Cell.CavalryYou)
            {
                MyCavalry.SetActive(true);
            }
            else
            {
                MyCavalry.SetActive(false);
            }
            if (Cell.RecruitYou)
            {
                MyRecruit.SetActive(true);
            }
            else
            {
                MyRecruit.SetActive(false);
            }
            if (Cell.InfantrymanYou)
            {
                MyInfantryman.SetActive(true);
            }
            else
            {
                MyInfantryman.SetActive(false);
            }
            if (Cell.ShooterYou)
            {
                MyShooter.SetActive(true);
            }
            else
            {
                MyShooter.SetActive(false);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Bord.IsMenu)
        {
            Position position = new Position { I = Cell.I, J = Cell.J };

            var isSendDetachment = BordScript.Game.IsCanTargetDetachment(position);

            if (isSendDetachment)
            {
                var detachmentEnemy = BordScript.Game.GetDetachmentTargetPosition(position);
                if (detachmentEnemy.Item1 == null)
                    return;

                Bord.DetachmentEnemy = detachmentEnemy;
                Bord.isChoice = true;

                if (!Bord.IsMenu)
                {
                    Bord.IsMenu = true;
                    Bord.IsCreateDetachment = true;
                }
            }
        }
    }
}
