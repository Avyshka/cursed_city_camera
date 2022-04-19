using CursedCity.Spelling;

namespace CursedCity.Ruins
{
    public interface IRuinUpgradeController
    {
        float CostMultiplier { get; }

        void Upgrade(Spell spell);
    }
}