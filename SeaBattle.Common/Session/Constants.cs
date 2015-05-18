namespace SeaBattle.Common.Session
{
    public class Constants
    {
        //public const int LevelWidth = 960;
        //public const int LevelHeigh = 600;

        public const int LevelWidth = 1024;
        public const int LevelHeigh = 900;

        public const int BackgroundWidth = 1024;
        public const int BackgroundHeigh = 1024;

        public const float TEXT_TEXTURE_LAYER = 0f;

        public const float MOVING_GAME_OBJECTS_TEXTURE_LAYER = 0.2f;
    }

    public enum GameModes
    {
        Simple,
        Real
    }

    public enum MapSet
    {
        Empty,
        WithStones
    }

    public enum ShipType
    {
        Lugger
    }
}
