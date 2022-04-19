using System;
using UnityEngine;

namespace CursedCity.ManaPanel
{
    [CreateAssetMenu(fileName = nameof(ManaModel), menuName = "Mana/" + nameof(ManaModel), order = 0)]
    public class ManaModel : ScriptableObject
    {
        #region Fields
        
        [SerializeField] private float _totalValue;
        [SerializeField] private float _currentValue;

        #endregion
        
        
        #region Properties

        public float TotalValue => _totalValue;
        public float CurrentValue => _currentValue;
        public float Capacity => CurrentValue / TotalValue;
        public event Action<float, float, float> OnManaChanged;

        #endregion
        
        
        #region Methods
        
        public void Add(float value)
        {
            _currentValue += value;
            if (_currentValue > _totalValue)
            {
                _currentValue = _totalValue;
            }
            OnManaChanged?.Invoke(CurrentValue, TotalValue, Capacity);
        }
        
        public void Remove(float value)
        {
            _currentValue -= value;
            if (_currentValue <= 0)
            {
                _currentValue = 0;
                Debug.LogError("Mana is empty! Game Over!");
            }
            OnManaChanged?.Invoke(CurrentValue, TotalValue, Capacity);
        }
        
        /**
         * todo: Only for tests. Please remove
         */
        public void Refill()
        {
            _currentValue = _totalValue;
            OnManaChanged?.Invoke(CurrentValue, TotalValue, Capacity);
        }
        
        #endregion
    }
}