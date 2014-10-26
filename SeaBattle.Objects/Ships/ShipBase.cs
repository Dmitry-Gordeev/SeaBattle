﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeaBattle.Common.Interfaces;
using SeaBattle.Objects.ShipSupplies;

namespace SeaBattle.Objects.Ships
{
    public abstract class ShipBase : IShip
    {
        #region Constructors

        protected ShipBase()
        {
            
        }

        #endregion

        #region Fields

        protected string Name;
        protected float ShipWeight;
        
        protected int Rowers;
        protected int Gunners;
        protected int Sailors;
        protected int PirateFighters;
        protected Cannons Cannons;
        
        #endregion

        #region Properties

        public bool IsStatic { get { return false; } }
        public abstract float Height { get; }

        public int NumberOfPeople
        {
            get
            {
                return Rowers + Gunners + Sailors + PirateFighters;
            }
        }
        
        public float LoadWeight
        {
            get { return 10f; }
        }

        public float FullWeight { get { return ShipWeight + LoadWeight; } }

        #endregion

        #region Methods

        protected abstract void InicializeFields();

        #endregion

        
    }
}
