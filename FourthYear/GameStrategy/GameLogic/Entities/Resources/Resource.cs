namespace Strategy.GameLogic.Entities.Resources
{
    public class Resource
    {
        public Resource()
        {
            Coins = 500;
            Population = 100;
        }

        public int Coins { get; set; }

        public int Population { get; set; }
    }
}
