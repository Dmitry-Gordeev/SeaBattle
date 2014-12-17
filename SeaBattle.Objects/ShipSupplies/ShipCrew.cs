using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Utils;

namespace SeaBattle.Service.ShipSupplies
{
    public class ShipCrew : ISerializableObject
    {
        public bool SomethingChanged { get; set; }
        public object Lock { get; set; }
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
            if (dataBytes[position++] == 0) return;

            Rowers = CommonSerializer.GetInt(ref position, dataBytes);
            Gunners = CommonSerializer.GetInt(ref position, dataBytes);
            Sailors = CommonSerializer.GetInt(ref position, dataBytes);
            PirateFighters = CommonSerializer.GetInt(ref position, dataBytes);
        }

        public byte[] Serialize()
        {
            if (!SomethingChanged) return new byte[] { 0 };
            var result = new byte[] { 1 };

            result = result.Concat(BitConverter.GetBytes(Rowers)).ToArray();
            result = result.Concat(BitConverter.GetBytes(Gunners)).ToArray();
            result = result.Concat(BitConverter.GetBytes(Sailors)).ToArray();
            result = result.Concat(BitConverter.GetBytes(PirateFighters)).ToArray();

            SomethingChanged = false;
            return result;
        }
    }
}
