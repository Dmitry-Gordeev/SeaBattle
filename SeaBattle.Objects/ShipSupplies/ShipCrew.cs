using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SeaBattle.Common.Objects;

namespace SeaBattle.Service.ShipSupplies
{
    public class ShipCrew : ISerializableObject
    {
        public bool SomethingChanged { get; set; }
        public int Rowers { get; set; }
        public int Gunners { get; set; }
        public int Sailors { get; set; }
        public int PirateFighters { get; set; }

        public ShipCrew(int pirateFighters, int sailors, int gunners, int rowers)
        {
            PirateFighters = pirateFighters;
            Sailors = sailors;
            Gunners = gunners;
            Rowers = rowers;
        }

        public int NumberOfPeople
        {
            get
            {
                return Rowers + Gunners + Sailors + PirateFighters;
            }
        }

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize()
        {
            if (!SomethingChanged) return new byte[] { 0 };
            var result = new byte[] { 1 };

            result = (byte[])result.Concat(BitConverter.GetBytes(Rowers));
            result = (byte[])result.Concat(BitConverter.GetBytes(Gunners));
            result = (byte[])result.Concat(BitConverter.GetBytes(Sailors));
            result = (byte[])result.Concat(BitConverter.GetBytes(PirateFighters));

            SomethingChanged = false;
            return result;
        }
    }
}
