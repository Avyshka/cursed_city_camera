using CursedCity.Spelling;
using UnityEngine;

namespace Code
{
    public class Starter : MonoBehaviour
    {
        #region Fields

        [SerializeField] private CastPanelModel _castPanelModel;
        [SerializeField] private CastPanelView _castPanelView;

        private CastPanelController _castPanel;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _castPanel = new CastPanelController(_castPanelModel, _castPanelView);
        }

        #endregion
    }
}