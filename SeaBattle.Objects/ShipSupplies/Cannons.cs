using System.Collections.Generic;
using System.Linq;
using SeaBattle.Common.Objects;

namespace SeaBattle.Service.ShipSupplies
{
    public class Cannons : ISerializableObject
    {
        public bool SomethingChanged { get; set; }

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
            return cannons.Sum(cannon => cannon ? 1 : 0);
        }

        private void Initialization()
        {
            
        }

        #endregion

        public object DeSerialize(ref long position, byte[] dataBytes)
        {
            throw new System.NotImplementedException();
        }

        public byte[] Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
