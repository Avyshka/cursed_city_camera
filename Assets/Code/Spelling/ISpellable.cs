namespace CursedCity.Spelling
{
    public interface ISpellable
    {
        #region Properties

        bool Generated { get; }
        float CostMultiplier { get; }

        #endregion

        
        #region Methods

        void Dispel();

        #endregion
    }
}