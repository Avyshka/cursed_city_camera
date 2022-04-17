using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CursedCity.Spelling
{
    public class SpellItem : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _cost;
        [SerializeField] private Button _button;

        private Spell _spell;

        public event Action<Spell> SelectSpell;
        
        #endregion


        #region UnityMethods

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        #endregion
        
        
        #region Methods

        public void Init(Spell spell)
        {
            _spell = spell;
            
            _icon.sprite = spell.Icon;
            _title.text = spell.Title;
            _cost.text = spell.Cost.ToString();
            
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            SelectSpell?.Invoke(_spell);
        }

        #endregion
    }
}