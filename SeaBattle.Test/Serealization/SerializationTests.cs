using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using SeaBattle.Service.Player;
using SeaBattle.Service.Ships;

namespace SeaBattle.Test.Serealization
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var player = new Player(new Lugger(), 1, "Name")
            {
                Ship = {Coordinates = new Vector2(12, 13), SomethingChanged = true}
            };
            var bytes = player.Serialize();

            int i = 0;

            var player2 = new Player(new Lugger(), 3, "Name2");
            player2.DeSerialize(ref i, bytes);

            Assert.AreEqual(player.Name, player2.Name);
            Assert.IsTrue(Math.Abs(player.Ship.Coordinates.X - player2.Ship.Coordinates.X) < 1);
            Assert.IsTrue(Math.Abs(player.Ship.Coordinates.Y - player2.Ship.Coordinates.Y) < 1);
        }
    }
}
