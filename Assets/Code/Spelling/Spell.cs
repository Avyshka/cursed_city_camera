using CursedCity.Spelling.Enums;
using UnityEngine;

namespace CursedCity.Spelling
{
    [CreateAssetMenu(fileName = nameof(Spell), menuName = "Spelling/" + nameof(Spell), order = 0)]
    public class Spell : ScriptableObject
    {
        [SerializeField] private string _title;
        [SerializeField] private SpellType _type;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _cost;
    }
}