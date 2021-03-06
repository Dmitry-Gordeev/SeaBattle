﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeaBattle.Common;
using SeaBattle.Common.Session;
using SeaBattle.Service.Ships;

namespace SeaBattle.Test.Serealization
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void SerializanionTest()
        {
            var player = new Player("name", ShipType.Lugger, Guid.NewGuid());
            var ship1 = new Corvette();
            var bytes = ship1.Serialize().ToArray();

            int i = 0;

            var ship2 = new Corvette();
            ship2.DeSerialize(ref i, bytes);

            /*
            Assert.AreEqual(player.Name, player2.Name);
            Assert.IsTrue(Math.Abs(player.Ship.Coordinates.X - player2.Ship.Coordinates.X) < 1);
            Assert.IsTrue(Math.Abs(player.Ship.Coordinates.Y - player2.Ship.Coordinates.Y) < 1);*/
        }
    }
}
