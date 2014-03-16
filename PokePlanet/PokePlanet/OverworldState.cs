namespace PokePlanet
{
    class OverworldState : IGameState
    {
        public static Player Player;

        

        public void Initialize()
        {
            //player and movement
            Player = new Player();
            
        }
    }
}
