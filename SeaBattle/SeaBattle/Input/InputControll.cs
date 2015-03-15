namespace SeaBattle.Input
{
    internal class InputControl : Nuclex.UserInterface.Controls.Desktop.InputControl
    {
        private int _maxLength = 1000;
        private string _realText = string.Empty;
        public bool IsHidden { get; set; }

        public string RealText
        {
            get { return _realText; }
            set { _realText = value; }
        }

        public int MaxLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }

        public static string HiddenText(string text)
        {
            return new string('*', text.Length);
        }

        protected override void OnCharacterEntered(char character)
        {
            if (character == '\b' && RealText.Length > 0)
            {
                RealText = RealText.Substring(0, RealText.Length - 1);
            }
            else if ((RealText.Length < MaxLength) && (char.IsLetter(character) || char.IsDigit(character) || (character == '_')))
            {
                RealText += character;
                Text += IsHidden ? '*' : character;
                CaretPosition++;
            }
        }
    }
}
