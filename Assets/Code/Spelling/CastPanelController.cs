using UnityEngine;

namespace CursedCity.Spelling
{
    public class CastPanelController
    {
        #region Fields

        private CastPanelModel _model;
        private CastPanelView _view;

        #endregion


        #region ClassLifeCycles

        public CastPanelController(CastPanelModel model, CastPanelView view)
        {
            _model = model;
            _view = view;

            InitItems();
            _view.SelectSpellCallback += SelectSpell;
        }

        #endregion


        #region Methods

        private void InitItems()
        {
            _view.RemoveAllItems();
            foreach (var spell in _model.Spells)
            {
                _view.AddSpellItem(spell);
            }
        }

        private void SelectSpell(Spell spell)
        {
            Debug.Log($"Spell [{spell.Title}] selected");
        }

        #endregion
    }
}