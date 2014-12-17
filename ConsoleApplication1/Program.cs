using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SeaBattle.Service.Player;
using SeaBattle.Service.Ships;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = new Player(new Lugger(), 1, "Name") {Ship = {Coordinates = new Vector2(12, 13), SomethingChanged = true}};

            var bytes = player.Serialize();

            foreach (var item in bytes)
            {
                Console.Write(item);
            }
            Console.WriteLine();

            int i = 0;

            var player2 = new Player(new Lugger(), 3, "Name2");
            player2.DeSerialize(ref i, bytes);

            Console.WriteLine(i);
            Console.WriteLine(player2.Name);


            if (player.Name == player2.Name && Math.Abs(player.Ship.Coordinates.X - player2.Ship.Coordinates.X) < 1 && Math.Abs(player.Ship.Coordinates.Y - player2.Ship.Coordinates.Y) < 1)
            {
                Console.WriteLine("Равны");
            }

            Console.ReadKey();
        }
    }
}
