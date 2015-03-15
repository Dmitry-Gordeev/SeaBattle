using SeaBattle.Common.Objects;
using SeaBattle.Common.Session;
using SeaBattle.View;

namespace SeaBattle.Game
{
    internal class GameFactory
    {
        public static DrawableGameObject CreateClientGameObject(IObject serverGameObject)
        {
            switch (serverGameObject.IsStatic)
            {
                default:
                    return new DrawableGameObject(serverGameObject, Textures.Cross);
            }
        }

        public static GameLevel CreateClientGameLevel(GameLevel gameLevel)
        {
            return new GameLevel(gameLevel.Width, gameLevel.Height);
        }
    }
}
