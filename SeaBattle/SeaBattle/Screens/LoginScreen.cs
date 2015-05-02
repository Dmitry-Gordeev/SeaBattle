using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Controls.Desktop;
using SeaBattle.Common.Service;
using SeaBattle.Common.Localization;
using SeaBattle.Game;
using InputControl = SeaBattle.Input.InputControl;

namespace SeaBattle.Screens
{
    internal class LoginScreen : GameScreen
    {
        private static Texture2D _texture;

        private LabelControl _loginLabel;
        private LabelControl _passwordLabel;

        private InputControl _loginBox;
        private InputControl _passwordBox;

        private ButtonControl _exitButton;
        private ButtonControl _loginButton;
        private ButtonControl _newAccountButton;

        public LoginScreen()
        {
            CreateControls();
            InitializeControls();
        }

        public override ScreenManager.ScreenEnum ScreenType
        {
            get { return ScreenManager.ScreenEnum.LoginScreen; }
        }

        public override void LoadContent()
        {
            _texture = ContentManager.Load<Texture2D>("Textures/screens/main_back_ground_screen");
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(_texture, Vector2.Zero, Color.White);
            SpriteBatch.End();
        }

        private void CreateControls()
        {
            // Login Input
            _loginBox = new InputControl
            {
                Bounds = new UniRectangle(new UniScalar(0.5f, -100f), new UniScalar(0.4f, -30), 200, 30),
                IsHidden = false,
                MaxLength = 20,
                RealText = Settings.Default.login,
                Text = Settings.Default.login
            };

            // Password Input
            _passwordBox = new InputControl
            {
                Bounds = new UniRectangle(new UniScalar(0.5f, -100f), new UniScalar(0.4f, 30), 200, 30),
                IsHidden = true,
                MaxLength = 20,
                RealText = Settings.Default.password,
                Text = InputControl.HiddenText(Settings.Default.password)
            };

            // Login Label
            _loginLabel = new LabelControl(Strings.User_Name)
            {
                Bounds = new UniRectangle(new UniScalar(0.5f, -32), new UniScalar(0.4f, -60), 100, 30)
            };

            // Password Label
            _passwordLabel = new LabelControl(Strings.Password)
            {
                Bounds = new UniRectangle(new UniScalar(0.5f, -32), new UniScalar(0.4f, 0), 100, 30)
            };

            // Login Button
            _loginButton = new ButtonControl
            {
                Text = Strings.Login,
                Bounds = new UniRectangle(new UniScalar(0.5f, 110), new UniScalar(0.4f, 70), 100, 32)
            };

            // Back Button
            _exitButton = new ButtonControl
            {
                Text = Strings.Exit,
                Bounds = new UniRectangle(new UniScalar(0.5f, -210f), new UniScalar(0.4f, 70), 100, 32),
            };

            // New Account Button
            _newAccountButton = new ButtonControl
            {
                Text = Strings.Create_New_Account,
                Bounds = new UniRectangle(new UniScalar(0.5f, -75f), new UniScalar(0.4f, 70), 150, 32)
            };
        }

        private void InitializeControls()
        {
            Desktop.Children.Add(_loginBox);
            Desktop.Children.Add(_passwordBox);
            Desktop.Children.Add(_loginLabel);
            Desktop.Children.Add(_passwordLabel);
            Desktop.Children.Add(_exitButton);
            Desktop.Children.Add(_newAccountButton);
            Desktop.Children.Add(_loginButton);

            ScreenManager.Instance.Controller.AddListener(_loginButton, LoginButtonPressed);
            ScreenManager.Instance.Controller.AddListener(_exitButton, ExitButtonPressed);
            ScreenManager.Instance.Controller.AddListener(_newAccountButton, NewAccountButtonPressed);
        }

        private void ExitButtonPressed(object sender, EventArgs args)
        {
            ScreenManager.Instance.Game.Exit();
        }

        private void LoginButtonPressed(object sender, EventArgs args)
        {
            if (_loginBox.Text.Length < 3)
            {
                MessageBox.Message = Strings.Short_Login_Data;
                MessageBox.Next = ScreenManager.ScreenEnum.LoginScreen;
                ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.MessageBoxScreen);
            }
            else if (_passwordBox.Text.Length < 3)
            {
                MessageBox.Message = Strings.Short_Login_Data;
                MessageBox.Next = ScreenManager.ScreenEnum.LoginScreen;
                ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.MessageBoxScreen);
            }
            else
            {
                Settings.Default.login = _loginBox.Text;
                Settings.Default.password = _passwordBox.RealText;
                Settings.Default.Save();

                AccountManagerErrorCode errorCode = GameController.Instance.Login(_loginBox.Text, _passwordBox.RealText);

                if (errorCode == AccountManagerErrorCode.Ok)
                {
                    ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.MainMenuScreen);
                }
                else
                {
                    string message;
                    switch (errorCode)
                    {
                        case AccountManagerErrorCode.UnknownExceptionOccured:
                            message = Strings.Unknoun_Exception;
                            break;
                        case AccountManagerErrorCode.InvalidUsernameOrPassword:
                            message = Strings.Invzlid_Name_Or_Password;
                            break;
                        case AccountManagerErrorCode.UserIsAlreadyOnline:
                            message = Strings.Already_Online;
                            break;
                        case AccountManagerErrorCode.UnknownError:
                            message = Strings.Unknoun_Error;
                            break;
                        default:
                            message = Strings.Unexpected_Error;
                            break;
                    }
                    MessageBox.Message = message;
                    MessageBox.Next = ScreenManager.ScreenEnum.LoginScreen;
                    ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.MessageBoxScreen);
                }
            }
        }

        private void NewAccountButtonPressed(object sender, EventArgs args)
        {
            ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.NewAccountScreen);
        }
    }
}
