using CursedCity.Spelling.Enums;
using UnityEngine;

namespace CursedCity.Spelling
{
    [CreateAssetMenu(fileName = nameof(Spell), menuName = "Spelling/" + nameof(Spell), order = 0)]
    public class Spell : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _title;
        [SerializeField] private SpellType _type;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _cost;
        [SerializeField] private GameObject _ruin;
        [SerializeField] private GameObject _entity;

        #endregion


        #region Properties

        public string Title => _title;
        public SpellType Type => _type;
        public Sprite Icon => _icon;
        public float Cost => _cost;
        public GameObject Ruin => _ruin;
        public GameObject Entity => _entity;

        #endregion
    }
}