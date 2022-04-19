﻿using CursedCity.Spelling;

namespace CursedCity.Ruins
{
    public interface IRuinUpgradeController
    {
        #region Properties

        float CostMultiplier { get; }

        #endregion


        #region Methods

        void Upgrade(Spell spell);

        #endregion
    }
}