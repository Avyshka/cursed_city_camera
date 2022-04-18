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

        #endregion
    }
}