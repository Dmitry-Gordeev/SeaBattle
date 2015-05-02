using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nuclex.Input;
using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Controls.Desktop;

namespace SeaBattle.Input
{
    public class KeyboardAndMouse : Controller
    {
        private KeyboardState _currentKeyboardState;
        private KeyboardState _lastKeyboardState;

        private MouseState _currentMouseState;
        private MouseState _lastMouseState;

        public KeyboardAndMouse(InputManager inputManager)
            : base(inputManager)
        {
            _currentKeyboardState = InputManager.GetKeyboard().GetState();
            _currentMouseState = InputManager.GetMouse().GetState();
        }

        public override Vector2 SightPosition
        {
            get { return new Vector2(_currentMouseState.X, _currentMouseState.Y); }
        }

        public override bool ShootButtonPressed
        {
            get { return    _currentMouseState.LeftButton == ButtonState.Pressed &&
                            _lastMouseState.LeftButton == ButtonState.Released; }
        }

        public override void Update()
        {
            _lastKeyboardState = _currentKeyboardState;
            _lastMouseState = _currentMouseState;

            _currentKeyboardState = InputManager.GetKeyboard().GetState();
            _currentMouseState = InputManager.GetMouse().GetState();
            
            if (IsNewKeyPressed(Keys.Down))
            {
                Index++;
                Index %= Length;
                FocusChanged();
            }
            if (IsNewKeyPressed(Keys.Up))
            {
                Index--;
                if (Index == -1)
                    Index = Length - 1;
                FocusChanged();
            }

            if (IsNewKeyPressed(Keys.Enter))
            {
                NotifyListeners(Index);
            }
        }

        public override void AddListener(Control control, EventHandler buttonPressed)
        {
            base.AddListener(control, buttonPressed);

            Debug.Assert(control is ButtonControl);
            (control as ButtonControl).Pressed += buttonPressed;
        }

        public bool IsUnpressed(Keys key)
        {
            return _currentKeyboardState.IsKeyUp(key) && _lastKeyboardState.IsKeyDown(key);
        }

        public bool IsNewKeyPressed(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key) && _lastKeyboardState.IsKeyUp(key);
        }

        public int MouseWheelValue
        {
            get
            {
                int currentScroolWheel = _currentMouseState.ScrollWheelValue;
                int prevScrollWheel = _lastMouseState.ScrollWheelValue;
                return currentScroolWheel - prevScrollWheel;
            }
        }
    }
}
