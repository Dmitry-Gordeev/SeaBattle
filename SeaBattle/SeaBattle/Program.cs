﻿namespace SeaBattle
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (var seaBattleGame = new SeaBattleGame())
            {
                seaBattleGame.Run();
            }
        }
    }
#endif
}

