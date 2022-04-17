using System;
using System.Collections.Generic;
using UnityEngine;

namespace CursedCity.Spelling
{
    public class CastPanelView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private Transform _spawnTransform;

        private readonly List<SpellItem> _items = new List<SpellItem>();
        
        public event Action<Spell> SelectSpellCallback;

        #endregion


        #region Methods

        public void AddSpellItem(Spell spell)
        {
            var spellItemPrefab = Instantiate(_itemPrefab, _spawnTransform);
            var spellItem = spellItemPrefab.GetComponent<SpellItem>();
            spellItem.Init(spell);
            spellItem.SelectSpell += SelectSpell;
            _items.Add(spellItem);
        }
        
        public void RemoveAllItems()
        {
            foreach (var spellItem in _items)
            {
                spellItem.SelectSpell -= SelectSpell;
                _items.Remove(spellItem);
                Destroy(spellItem.gameObject);
            }
        }
        
        private void SelectSpell(Spell spell)
        {
            SelectSpellCallback?.Invoke(spell);
        }

        #endregion
    }
}