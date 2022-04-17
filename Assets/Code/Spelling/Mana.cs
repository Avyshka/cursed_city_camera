using System;
using UnityEngine;

namespace CursedCity.Spelling
{
    [CreateAssetMenu(fileName = nameof(Mana), menuName = "Spelling/" + nameof(Mana), order = 0)]
    public class Mana : ScriptableObject
    {
        #region Fields
        
        [SerializeField] private float _totalValue;
        [SerializeField] private float _currentValue;

        #endregion
        
        
        #region Properties

        public float TotalValue => _totalValue;
        public float CurrentValue => _currentValue;
        public float Capacity => CurrentValue / TotalValue;

        #endregion
        
        
        #region Methods
        
        public event Action<float, float, float> OnManaChanged;

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
                Debug.LogError("Mana is empty!");
            }
            OnManaChanged?.Invoke(CurrentValue, TotalValue, Capacity);
        }
        
        #endregion
    }
}