using System.Linq;
using CursedCity.ManaPanel;
using CursedCity.Ruins;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CursedCity.Spelling
{
    public class SpellCaster : MonoBehaviour
    {
        #region Fields

        private const float SPAWN_HEIGHT = 5f;
        private const int MOUSE_BUTTON_LEFT = 0;
        private const int MOUSE_BUTTON_RIGHT = 1;

        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _cost;
        [SerializeField] private GameObject _body;
        
        [SerializeField] private ManaModel _mana;
        
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _groundTransform;

        private Spell _spell;
        private bool _activated;
        private Plane _groundPlane;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _body.SetActive(_activated);
            _groundPlane = new Plane(_groundTransform.up, 0);
        }

        private void Update()
        {
            if (!_activated)
            {
                return;
            }
            transform.position = Input.mousePosition;
            UpdateSpellCost();
            if (Input.GetMouseButtonDown(MOUSE_BUTTON_LEFT))
            {
                ApplySpellWithCheck();
            }
            else if (Input.GetMouseButtonDown(MOUSE_BUTTON_RIGHT))
            {
                Deactivate();
            }
        }

        #endregion


        #region Methods

        private void UpdateSpellCost()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray);

            if (WeHit<IRuinUpgradeController>(hits, out var ruin))
            {
                _cost.text = (_spell.Cost * ruin.CostMultiplier).ToString();
                _cost.color = Color.Lerp(Color.yellow, Color.red, 0.5f);
            }
            else
            {
                _cost.text = _spell.Cost.ToString();
                _cost.color = Color.yellow;
            }
        }
        
        public void Activate(Spell spell)
        {
            _spell = spell;

            _activated = true;
            _body.SetActive(_activated);

            _icon.sprite = spell.Icon;
            _title.text = spell.Title;
            _cost.text = spell.Cost.ToString();
        }

        private void Deactivate()
        {
            _spell = null;
            _activated = false;
            _body.SetActive(_activated);
        }

        private void ApplySpellWithCheck()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray);

            if (!_activated || _eventSystem.IsPointerOverGameObject())
            {
                return;
            }
            
            if (WeHit<IRuinUpgradeController>(hits, out var ruin))
            {
                ruin.Upgrade(_spell);
                _mana.Remove(_spell.Cost * ruin.CostMultiplier);
            }
            else
            {
                if (_groundPlane.Raycast(ray, out var enter))
                {
                    SpawnEntity(ray, enter);
                    _mana.Remove(_spell.Cost);
                }
            }
            
            Deactivate();
        }

        private void SpawnEntity(Ray ray, float enter)
        {
            var spellEntity = Instantiate(
                _spell.Entity, 
                ray.origin + ray.direction * enter + Vector3.up * SPAWN_HEIGHT,
                Random.rotation
            );
            if (spellEntity.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddTorque(Vector3.forward, ForceMode.Impulse);
            }
        }

        private bool WeHit<T>(RaycastHit[] hits, out T result) where T : class
        {
            result = default;
            if (hits.Length == 0)
            {
                return false;
            }

            result = hits
                .Select(hit => hit.collider.GetComponentInParent<T>())
                .FirstOrDefault(c => c != null);
            return result != default;
        }

        #endregion
    }
}