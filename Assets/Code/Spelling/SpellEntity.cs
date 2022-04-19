using UnityEngine;

namespace CursedCity.Spelling
{
    public class SpellEntity : MonoBehaviour, ISpellable
    {
        #region Properties

        public bool Generated => true;
        public float CostMultiplier => 1;

        #endregion


        #region Methods

        public void Dispel()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}