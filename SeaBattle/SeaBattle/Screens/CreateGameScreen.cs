using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;
using Nuclex.UserInterface.Controls.Desktop;
using SeaBattle.Common.Session;
using SeaBattle.NetWork;
using SeaBattle.Service.Player;

namespace SeaBattle.Screens
{
    internal class CreateGameScreen : GameScreen
    {
        private static Texture2D _texture;

        private ListControl _maxPlayersList;
        private ListControl _tileList;
        private ListControl _gameModList;

        private LabelControl _maxPlayersLabel;
        private LabelControl _tileLabel;
        private LabelControl _gameModeLabel;
        private LabelControl _maxPlayers;
        private LabelControl _tile;
        private LabelControl _gameMode;

        private ButtonControl _backButton;
        private ButtonControl _createButton;

        public CreateGameScreen()
        {
            CreateControls();
            InitializeControls();
        }

        public override void LoadContent()
        {
            _texture = ContentManager.Load<Texture2D>("Textures/screens/main_back_ground_screen");
        }

        public override void OnShow()
        {
            const int maxPlayers = 0;
            const int tile = 0;
            const int gameMode = 0;

            // число игроков
            _maxPlayersList.SelectedItems[0] = maxPlayers;
            _maxPlayers.Text = _maxPlayersList.Items[maxPlayers];

            // карта
            _tileList.SelectedItems[0] = 0;
            _tile.Text = _tileList.Items[tile];

            // мод
            _gameModList.SelectedItems[0] = 0;
            _gameMode.Text = _gameModList.Items[gameMode];
        }

        public override void Update(GameTime gameTime)
        {
            _maxPlayers.Text = _maxPlayersList.Items[_maxPlayersList.SelectedItems[0]];
            _tile.Text = _tileList.Items[_tileList.SelectedItems[0]];
            _gameMode.Text = _gameModList.Items[_gameModList.SelectedItems[0]];
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(_texture, Vector2.Zero, Color.White);
            SpriteBatch.End();
        }

        private void CreateControls()
        {
            // выбора максимального кол-ва игроков
            _maxPlayersLabel = new LabelControl
            {
                Bounds = new UniRectangle(-60f, -34f, 100f, 24f),
                Text = "Max Players"
            };

            // выбор карты
            _tileLabel = new LabelControl
            {
                Bounds = new UniRectangle(60f, -34f, 150f, 24f),
                Text = "Map"
            };

            _tileList = new ListControl
            {
                Bounds = new UniRectangle(60f, -4f, 150f, 300f)
            };
            _tileList.Items.Add("Empty");
            _tileList.Items.Add("With stones");
            _tileList.SelectionMode = ListSelectionMode.Single;
            _tileList.SelectedItems.Add(0);

            _tileList.Slider.Bounds.Location.X.Offset -= 1.0f;
            _tileList.Slider.Bounds.Location.Y.Offset += 1.0f;
            _tileList.Slider.Bounds.Size.Y.Offset -= 2.0f;

            // выбор режима игры
            _gameModeLabel = new LabelControl
            {
                Bounds = new UniRectangle(230f, -34f, 150f, 24f),
                Text = "Mode"
            };

            _maxPlayersList = new ListControl
            {
                Bounds = new UniRectangle(-60f, -4f, 100f, 300f)
            };
            _maxPlayersList.Slider.Bounds.Location.X.Offset -= 1.0f;
            _maxPlayersList.Slider.Bounds.Location.Y.Offset += 1.0f;
            _maxPlayersList.Slider.Bounds.Size.Y.Offset -= 2.0f;

            for (int i = 1; i < 11; i++)
            {
                _maxPlayersList.Items.Add(i + string.Empty);
            }

            _maxPlayersList.SelectionMode = ListSelectionMode.Single;
            _maxPlayersList.SelectedItems.Add(0);

            _maxPlayers = new LabelControl
            {
                Bounds = new UniRectangle(500f, 50f, 150f, 24f),
            };

            _tile = new LabelControl
            {
                Bounds = new UniRectangle(500f, 80f, 150f, 24f),
            };

            _gameModList = new ListControl
            {
                Bounds = new UniRectangle(230f, -4f, 150f, 300f)
            };

            _gameModList.Items.Add("Simple");
            _gameModList.Items.Add("Real");
            _gameModList.SelectionMode = ListSelectionMode.Single;
            _gameModList.SelectedItems.Add(0);

            _gameModList.Slider.Bounds.Location.X.Offset -= 1.0f;
            _gameModList.Slider.Bounds.Location.Y.Offset += 1.0f;
            _gameModList.Slider.Bounds.Size.Y.Offset -= 2.0f;

            _gameMode = new LabelControl
            {
                Bounds = new UniRectangle(500f, 110f, 150f, 24f),
            };

            // Create Button
            _createButton = new ButtonControl
            {
                Text = "Create",
                Bounds =
                    new UniRectangle(new UniScalar(0.5f, 178f), new UniScalar(0.4f, -15f), 110, 32)
            };

            // Back Button
            _backButton = new ButtonControl
            {
                Text = "Back",
                Bounds =
                    new UniRectangle(new UniScalar(0.5f, -380f), new UniScalar(0.4f, 170f), 120, 32)
            };
        }

        private void InitializeControls()
        {
            Desktop.Children.Add(_maxPlayersList);
            Desktop.Children.Add(_maxPlayersLabel);
            Desktop.Children.Add(_tileLabel);
            Desktop.Children.Add(_tileList);
            Desktop.Children.Add(_gameModeLabel);
            Desktop.Children.Add(_gameModList);
            Desktop.Children.Add(_maxPlayers);
            Desktop.Children.Add(_createButton);
            Desktop.Children.Add(_gameMode);
            Desktop.Children.Add(_backButton);

            ScreenManager.Instance.Controller.AddListener(_createButton, CreateButtonPressed);
            ScreenManager.Instance.Controller.AddListener(_backButton, BackButtonPressed);
        }

        private void BackButtonPressed(object sender, EventArgs args)
        {
            ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.MultiplayerScreen);
        }

        private void CreateButtonPressed(object sender, EventArgs args)
        {
            GameMode mode;
            switch (_gameMode.Text)
            {
                case "Simple":
                    mode = GameMode.Simple;
                    break;
                case "Real":
                    mode = GameMode.Real;
                    break;
                default:
                    mode = GameMode.Simple;
                    break;
            }

            int gameId =
                ConnectionManager.Instance.CreateGame(mode, Convert.ToInt32(_maxPlayers.Text));
            if (gameId == -1)
                return;
            WaitScreen.GameMode = _gameMode.Text;
            WaitScreen.MaxPlayers = _maxPlayers.Text;
            WaitScreen.GameId = gameId;

            ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.WaitScreen);
        }
    }
}
