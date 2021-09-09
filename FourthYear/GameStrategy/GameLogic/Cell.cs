namespace Strategy.GameLogic
{
    public class Cell
    {
        public string Info { get; set; }
        public bool CastleEnemy { get; set; }
        public bool CastleYou { get; set; }
        public bool Cavalry { get; set; }
        public bool Infantryman { get; set; }
        public bool Recruit { get; set; }
        public bool Shooter { get; set; }
        public bool CavalryYou { get; set; }
        public bool InfantrymanYou { get; set; }
        public bool RecruitYou { get; set; }
        public bool ShooterYou { get; set; }
        public int I { get; set; }
        public int J { get; set; }
    }
}
