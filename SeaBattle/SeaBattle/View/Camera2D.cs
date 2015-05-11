using Microsoft.Xna.Framework;
using SeaBattle.Common.Session;
using SeaBattle.Game;

namespace SeaBattle.View
{
    public class Camera2D
    {
        private static Vector2 _screenCenter;
        public Matrix MatrixScreen;

        public void Update()
        {
            _screenCenter = new Vector2(GameController.Instance.MyShip.Ship.Coordinates.X - Constants.LevelWidth / 2, 
                                        GameController.Instance.MyShip.Ship.Coordinates.Y - Constants.LevelHeigh/2);
            MatrixScreen = Matrix.CreateScale(new Vector3(1,1,0)) * Matrix.CreateTranslation(new Vector3(-_screenCenter.X, -_screenCenter.Y, 0));
        }

        public static Vector2 RelativePosition(Vector2 position)
        {
            return position + _screenCenter;
        }
    }
}
