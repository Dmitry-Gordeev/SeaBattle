﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Nuclex.UserInterface;
using SeaBattle.Common.Session;
using SeaBattle.Common.Utils;
using SeaBattle.Input;
using SeaBattle.View;

namespace SeaBattle.Screens
{
    public abstract class GameScreen : Screen
    {
        protected SpriteBatch SpriteBatch { get; private set; }

        protected ContentManager ContentManager { get; private set; }

        protected SpriteFont SpriteFont { get; set; }
        protected GarbageCollector GarbageCollector;


        protected GameScreen()
        {
            Desktop.Bounds = new UniRectangle(
                new UniScalar(0.1f, 0.0f),
                new UniScalar(0.1f, 0.0f),
                new UniScalar(0.8f, 0.0f),
                new UniScalar(0.8f, 0.0f));

            Height = ScreenManager.Instance.Height;
            Width = ScreenManager.Instance.Width;

            SpriteBatch = ScreenManager.Instance.SpriteBatch;

            ContentManager = ScreenManager.Instance.ContentManager;

            SpriteFont = ScreenManager.Instance.Font;
        }

        public bool IsActive { get; set; }

        public abstract ScreenManager.ScreenEnum ScreenType { get; }

        /// <summary>
        /// Загрузка контента, необходимого
        /// для отображения экрана 
        /// </summary>
        public virtual void LoadContent()
        {
        }

        /// <summary>
        /// Уничтожение экрана, освобождение всех ресурсов
        /// </summary>
        public virtual void Destroy()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            if (GarbageCollector == null)
                return;
            GarbageCollector.Update();
        }

        public virtual void HandleInput(Controller controller)
        {
        }

        public virtual void Draw(GameTime gameTime)
        {
        }

        /// <summary>
        /// Вызывается перед отображением экрана
        /// </summary>
        public virtual void OnShow()
        {

        }

        /// <summary>
        /// Вызывается при скрытии экрана
        /// </summary>
        public virtual void OnHide()
        {

        }

        protected void DrawObject(Vector2 coordinates, Vector2 direction, Texture2D texture, Rectangle rectangle)
        {
            SpriteBatch.Draw(texture, rectangle, null, Color.White);
        }

        protected void DrawString(string text, float positionX, float positionY, Color color)
        {
            SpriteBatch.DrawString(
                SpriteFont,
                text,
                new Vector2(positionX, positionY),
                color, 0, new Vector2(0f, 0f), 0.8f, SpriteEffects.None,
                layerDepth: Constants.TEXT_TEXTURE_LAYER);
        }

        protected void DrawString(string text, float positionX, float positionY, Color color, float fontSize)
        {
            SpriteBatch.DrawString(
                SpriteFont,
                text,
                Camera2D.RelativePosition(new Vector2(positionX, positionY)),
                color, 0, new Vector2(0f, 0f), fontSize, SpriteEffects.None,
                layerDepth: Constants.TEXT_TEXTURE_LAYER);
        }
    }
}
