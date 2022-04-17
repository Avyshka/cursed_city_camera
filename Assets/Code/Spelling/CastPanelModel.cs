using System.Collections.Generic;
using UnityEngine;

namespace CursedCity.Spelling
{
    [CreateAssetMenu(fileName = nameof(CastPanelModel), menuName = "Spelling/" + nameof(CastPanelModel), order = 0)]
    public class CastPanelModel : ScriptableObject
    {
        #region Fields

        [SerializeField] private List<Spell> _spells;

        #endregion


        #region Properties

        public List<Spell> Spells => _spells;

        #endregion
    }
}