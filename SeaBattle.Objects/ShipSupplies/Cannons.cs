﻿using System.Collections.Generic;
using System.Linq;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Utils;

namespace SeaBattle.Service.ShipSupplies
{
    public class Cannons : ICustomSerializable
    {
        public bool SomethingChanged { get; set; }
        public object Lock { get; set; }

        #region Constructors

        public Cannons(int leftSideCount, int rightSideCount, int forePartCount, int rearPartCount)
        {
            LeftSideCannons = new bool[leftSideCount];
            RightSideCannons = new bool[rightSideCount];
            ForePartCannons = new bool[forePartCount];
            RearPartCannons = new bool[rearPartCount];
        }

        #endregion

        #region Properties

        public bool[] LeftSideCannons { get; set; }

        public bool[] RightSideCannons { get; set; }

        public bool[] ForePartCannons { get; set; }

        public bool[] RearPartCannons { get; set; }

        public int CountOfLeftSideCannons
        {
            get
            {
                return GetNumberOfCannons(LeftSideCannons);
            }
        }

        public int CountOfRightSideCannons
        {
            get
            {
                return GetNumberOfCannons(RightSideCannons);
            }
        }

        public int CountOfForePartCannons
        {
            get
            {
                return GetNumberOfCannons(ForePartCannons);
            }
        }

        public int CountOfRearPartCannons
        {
            get
            {
                return GetNumberOfCannons(RearPartCannons);
            }
        }

        public int CountOfCannons
        {
            get
            {
                return CountOfForePartCannons + CountOfRearPartCannons + CountOfLeftSideCannons + CountOfRightSideCannons;
            }
        }

        #endregion
        
        #region Methods

        private int GetNumberOfCannons(IEnumerable<bool> cannons)
        {
            return cannons.Sum(cannon => cannon ? 0 : 1);
        }

        private void Initialization()
        {
            
        }

        #endregion

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            if (dataBytes[position++] == 0) return;

            LeftSideCannons = CommonSerializer.BytesToBools(ref position, dataBytes, LeftSideCannons.Count());
            RightSideCannons = CommonSerializer.BytesToBools(ref position, dataBytes, RightSideCannons.Count());
            ForePartCannons = CommonSerializer.BytesToBools(ref position, dataBytes, ForePartCannons.Count());
            RearPartCannons = CommonSerializer.BytesToBools(ref position, dataBytes, RearPartCannons.Count());
        }

        public IEnumerable<byte> Serialize()
        {
            //if (!SomethingChanged) return new byte[] { 0 };
            
            var result = new byte[] { 1 }.Concat(CommonSerializer.BoolArrToBytes(LeftSideCannons));
            result = result.Concat(CommonSerializer.BoolArrToBytes(RightSideCannons));
            result = result.Concat(CommonSerializer.BoolArrToBytes(ForePartCannons));
            result = result.Concat(CommonSerializer.BoolArrToBytes(RearPartCannons));

            SomethingChanged = false;
            return result;
        }
    }
}
