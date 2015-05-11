using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Controls.Desktop;
using SeaBattle.Common.Localization;
using SeaBattle.Common.Service;
using SeaBattle.NetWork;
using InputControl = SeaBattle.Input.InputControl;

namespace SeaBattle.Screens
{
    internal class NewAccountScreen : GameScreen
    {
        private static Texture2D _texture;

        private LabelControl _loginLabel;
        private LabelControl _passwordLabel;

        private InputControl _loginBox;
        private InputControl _passwordBox;

        private ButtonControl _backButton;
        private ButtonControl _okButton;

        public NewAccountScreen()
        {
            CreateControls();
            InitializeControls();
        }

        public override ScreenManager.ScreenEnum ScreenType
        {
            get { return ScreenManager.ScreenEnum.NewAccountScreen; }
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
                IsHidden = false,
                Bounds = new UniRectangle(new UniScalar(0.5f, -100f), new UniScalar(0.4f, -30), 200, 30),
                Text = string.Empty
            };

            // Password Input
            _passwordBox = new InputControl
            {
                IsHidden = true,
                Bounds =
                    new UniRectangle(new UniScalar(0.5f, -100f), new UniScalar(0.4f, 30), 200, 30),
                RealText = string.Empty,
                Text = string.Empty
            };

            // Login Label
            _loginLabel = new LabelControl("Username")
            {
                Bounds = new UniRectangle(new UniScalar(0.5f, -32), new UniScalar(0.4f, -70), 100, 30)
            };

            // Password Label
            _passwordLabel = new LabelControl("Password")
            {
                Bounds =
                    new UniRectangle(new UniScalar(0.5f, -32), new UniScalar(0.4f, 0), 100, 30)
            };

            // Back Button
            _backButton = new ButtonControl
            {
                Text = "Back",
                Bounds =
                    new UniRectangle(new UniScalar(0.5f, -210f), new UniScalar(0.4f, 70), 100, 32)
            };

            // Login Button
            _okButton = new ButtonControl
            {
                Text = "Create",
                Bounds = new UniRectangle(new UniScalar(0.5f, 110), new UniScalar(0.4f, 70), 100, 32)
            };
        }

        private void InitializeControls()
        {
            Desktop.Children.Add(_loginBox);
            Desktop.Children.Add(_passwordBox);
            Desktop.Children.Add(_loginLabel);
            Desktop.Children.Add(_passwordLabel);
            Desktop.Children.Add(_backButton);
            Desktop.Children.Add(_okButton);

            ScreenManager.Instance.Controller.AddListener(_backButton, BackButtonPressed);
            ScreenManager.Instance.Controller.AddListener(_okButton, OkButtonPressed);
        }

        private void BackButtonPressed(object sender, EventArgs args)
        {
            ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.LoginScreen);
        }

        private void OkButtonPressed(object sender, EventArgs args)
        {
            if (_loginBox.Text.Length < 3)
            {
                MessageBox.Message = Strings.Short_Login_Data;
                MessageBox.Next = ScreenManager.ScreenEnum.NewAccountScreen;
                ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.MessageBoxScreen);
            }
            else if (_passwordBox.Text.Length < 3)
            {
                MessageBox.Message = Strings.Short_Login_Data;
                MessageBox.Next = ScreenManager.ScreenEnum.NewAccountScreen;
                ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.MessageBoxScreen);
            }
            else
            {
                Settings.Default.login = _loginBox.Text;
                Settings.Default.password = _passwordBox.RealText;
                Settings.Default.Save();

                AccountManagerErrorCode errorCode = ConnectionManager.Instance.Register(_loginBox.Text, _passwordBox.RealText);

                if (errorCode == AccountManagerErrorCode.Ok)
                {
                    ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.LoginScreen);
                }
                else
                {
                    string message = Strings.Registration_Failed;
                    switch (errorCode)
                    {
                        case AccountManagerErrorCode.UnknownExceptionOccured:
                            message += Strings.Unexpected_Error;
                            break;
                        case AccountManagerErrorCode.UsernameTaken:
                            message += Strings.Username_Is_Already_Taken;
                            break;
                        case AccountManagerErrorCode.UnknownError:
                            message += Strings.Unknoun_Error;
                            break;
                        default:
                            message += Strings.Unexpected_Error;
                            break;
                    }
                    MessageBox.Message = message;
                    MessageBox.Next = ScreenManager.ScreenEnum.LoginScreen;
                    ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.MessageBoxScreen);
                }
                ConnectionManager.Instance.Logout();
            }
        }
    }
}
