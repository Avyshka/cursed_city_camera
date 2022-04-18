﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CursedCity.Spelling
{
    public class ManaPanel : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Mana _mana;
        [SerializeField] private Slider _manaBar;
        [SerializeField] private TMP_Text _manaValue;
        
        #endregion

        #region UnityMethods

        private void Start()
        {
            OnUpdateMana(_mana.CurrentValue, _mana.TotalValue, _mana.Capacity);
            _mana.OnManaChanged += OnUpdateMana;
        }

        /**
         * todo: Only for tests. Please remove
         */
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _mana.Remove(100);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                _mana.Add(100);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                _mana.Refill();
            }
        }

        #endregion


        #region Methods

        private void OnUpdateMana(float currentMana, float TotalMana, float capacityMana)
        {
            _manaBar.value = capacityMana;
            _manaValue.text = $"{currentMana} / {TotalMana}";
        }

        #endregion
    }
}