using CursedCity.Ruins.Enums;
using CursedCity.Spelling;
using UnityEngine;

namespace CursedCity.Ruins
{
    public class RuinUpgradeController : MonoBehaviour, IRuinUpgradeController
    {
        #region Fields

        [SerializeField] private RuinSize _size;
        [SerializeField] private float _costMultiplier;
        [SerializeField] private GameObject _originView;

        private GameObject _upgradedView;
        
        #endregion


        #region Properties

        private int Size => (int)_size;
        public float CostMultiplier => _costMultiplier;

        #endregion
        
        
        #region Methods

        public void Upgrade(Spell spell)
        {
            _originView.gameObject.SetActive(false);
            if (_upgradedView != null)
            {
                Destroy(_upgradedView.gameObject);
            }

            _upgradedView = Instantiate(spell.Ruins[Size], transform);
        }

        #endregion
    }
}