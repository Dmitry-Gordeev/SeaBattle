using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeaBattle.Common.Session;
using SeaBattle.View;

namespace SeaBattle.StaticObjects
{
    public class GameplayBackground
    {
        public Texture2D BackgroundTexture;
        public Animation2D BackgroundAnimation;
        
        public Rectangle BackgrounRectangle1;
        public Rectangle BackgrounRectangle2;
        public Rectangle BackgrounRectangle3;
        public Rectangle BackgrounRectangle4;

        private bool _isFirstVerticalPosition;
        private bool _isFirstHorizontallyPosition;

        public GameplayBackground(Texture2D backgroundTexture, Animation2D backgroundAnimation)
        {
            BackgroundTexture = backgroundTexture;
            BackgroundAnimation = backgroundAnimation;
        }

        public void Initialize(Point currentCoordinates)
        {
            BackgrounRectangle1 = new Rectangle(currentCoordinates.X - Constants.BackgroundWidth, currentCoordinates.Y - Constants.BackgroundHeigh, Constants.BackgroundWidth, Constants.BackgroundHeigh);
            BackgrounRectangle2 = new Rectangle(currentCoordinates.X, currentCoordinates.Y - Constants.BackgroundHeigh, Constants.BackgroundWidth, Constants.BackgroundHeigh);
            BackgrounRectangle3 = new Rectangle(currentCoordinates.X, currentCoordinates.Y, Constants.BackgroundWidth, Constants.BackgroundHeigh);
            BackgrounRectangle4 = new Rectangle(currentCoordinates.X - Constants.BackgroundWidth, currentCoordinates.Y, Constants.BackgroundWidth, Constants.BackgroundHeigh);

            _isFirstVerticalPosition = true;
            _isFirstHorizontallyPosition = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundAnimation.CurrentTexture, BackgrounRectangle1, Color.White);
            spriteBatch.Draw(BackgroundAnimation.CurrentTexture, BackgrounRectangle2, Color.White);
            spriteBatch.Draw(BackgroundAnimation.CurrentTexture, BackgrounRectangle3, Color.White);
            spriteBatch.Draw(BackgroundAnimation.CurrentTexture, BackgrounRectangle4, Color.White);
        }

        public void Update(Point currentCoords)
        {
            UpdateHorizontally(currentCoords);
            UpdateVertical(currentCoords);
        }

        private void UpdateHorizontally(Point currentCoords)
        {
            if (_isFirstHorizontallyPosition)
            {
                if (currentCoords.X > BackgrounRectangle2.Center.X)
                {
                    BackgrounRectangle1 = new Rectangle(BackgrounRectangle1.X + 2 * Constants.BackgroundWidth, BackgrounRectangle1.Y, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    BackgrounRectangle4 = new Rectangle(BackgrounRectangle4.X + 2 * Constants.BackgroundWidth, BackgrounRectangle4.Y, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    _isFirstHorizontallyPosition = !_isFirstHorizontallyPosition;
                }
                else if (currentCoords.X < BackgrounRectangle1.Center.X)
                {
                    BackgrounRectangle2 = new Rectangle(BackgrounRectangle2.X - 2 * Constants.BackgroundWidth, BackgrounRectangle2.Y, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    BackgrounRectangle3 = new Rectangle(BackgrounRectangle3.X - 2 * Constants.BackgroundWidth, BackgrounRectangle3.Y, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    _isFirstHorizontallyPosition = !_isFirstHorizontallyPosition;
                }
            }
            else
            {
                if (currentCoords.X > BackgrounRectangle1.Center.X)
                {
                    BackgrounRectangle2 = new Rectangle(BackgrounRectangle2.X + 2 * Constants.BackgroundWidth, BackgrounRectangle2.Y, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    BackgrounRectangle3 = new Rectangle(BackgrounRectangle3.X + 2 * Constants.BackgroundWidth, BackgrounRectangle3.Y, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    _isFirstHorizontallyPosition = !_isFirstHorizontallyPosition;
                }
                else if (currentCoords.X < BackgrounRectangle2.Center.X)
                {
                    BackgrounRectangle1 = new Rectangle(BackgrounRectangle1.X - 2 * Constants.BackgroundWidth, BackgrounRectangle1.Y, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    BackgrounRectangle4 = new Rectangle(BackgrounRectangle4.X - 2 * Constants.BackgroundWidth, BackgrounRectangle4.Y, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    _isFirstHorizontallyPosition = !_isFirstHorizontallyPosition;
                }
            }
        }

        private void UpdateVertical(Point currentCoords)
        {
            if (_isFirstVerticalPosition)
            {
                if (currentCoords.Y < BackgrounRectangle1.Center.Y)
                {
                    BackgrounRectangle3 = new Rectangle(BackgrounRectangle3.X, BackgrounRectangle3.Y - 2 * Constants.BackgroundHeigh, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    BackgrounRectangle4 = new Rectangle(BackgrounRectangle4.X, BackgrounRectangle4.Y - 2 * Constants.BackgroundHeigh, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    _isFirstVerticalPosition = !_isFirstVerticalPosition;
                }
                else if (currentCoords.Y > BackgrounRectangle4.Center.Y)
                {
                    BackgrounRectangle1 = new Rectangle(BackgrounRectangle1.X, BackgrounRectangle1.Y + 2 * Constants.BackgroundHeigh, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    BackgrounRectangle2 = new Rectangle(BackgrounRectangle2.X, BackgrounRectangle2.Y + 2 * Constants.BackgroundHeigh, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    _isFirstVerticalPosition = !_isFirstVerticalPosition;
                }
            }
            else
            {
                if (currentCoords.Y < BackgrounRectangle4.Center.Y)
                {
                    BackgrounRectangle1 = new Rectangle(BackgrounRectangle1.X, BackgrounRectangle1.Y - 2 * Constants.BackgroundHeigh, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    BackgrounRectangle2 = new Rectangle(BackgrounRectangle2.X, BackgrounRectangle2.Y - 2 * Constants.BackgroundHeigh, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    _isFirstVerticalPosition = !_isFirstVerticalPosition;
                }
                else if (currentCoords.Y > BackgrounRectangle1.Center.Y)
                {
                    BackgrounRectangle3 = new Rectangle(BackgrounRectangle3.X, BackgrounRectangle3.Y + 2 * Constants.BackgroundHeigh, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    BackgrounRectangle4 = new Rectangle(BackgrounRectangle4.X, BackgrounRectangle4.Y + 2 * Constants.BackgroundHeigh, Constants.BackgroundWidth, Constants.BackgroundHeigh);
                    _isFirstVerticalPosition = !_isFirstVerticalPosition;
                }
            }
        }
    }
}
