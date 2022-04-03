using UnityEngine;

namespace CursedCity.CameraController
{
    public class CameraController : MonoBehaviour
    {
        #region Fields

        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";
        private const string ZOOMING_AXIS = "Mouse ScrollWheel";

        [SerializeField] private float _cameraAngle;

        [Header("Moving")] [SerializeField] private float _moveSpeed;
        [SerializeField] private float _limitX;
        [SerializeField] private float _limitY;
        [SerializeField] private float _panBorderThickness;

        [Header("Zooming")] [SerializeField] private float _zoomSpeed;
        [SerializeField] private float _zoomDistanceMin;
        [SerializeField] private float _zoomDistanceMax;

        private Camera _camera;

        #endregion

        #region UnityMethods

        private void Awake()
        {
            _camera = Camera.main;
            _camera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            _camera.transform.position = Vector3.zero;
        }

        private void Start()
        {
            _camera.transform.rotation = Quaternion.Euler(_cameraAngle, 0f, 0f);
            _camera.transform.position = _camera.transform.forward * -_zoomDistanceMax;
        }

        private void Update()
        {
            Move();
            LimitPosition();
            Zoom();
        }

        #endregion


        #region Methods

        private void Move()
        {
            var inputX = GetHorizontalAxisValue();
            var inputZ = GetVerticalAxisValue();
            var direction = transform.forward * inputZ + transform.right * inputX;
            transform.position += direction * _moveSpeed * Time.deltaTime;
        }

        private float GetHorizontalAxisValue()
        {
            if (Input.mousePosition.x >= Screen.width - _panBorderThickness)
            {
                return 1;
            }

            if (Input.mousePosition.x <= _panBorderThickness)
            {
                return -1;
            }

            return Input.GetAxis(HORIZONTAL_AXIS);
        }

        private float GetVerticalAxisValue()
        {
            if (Input.mousePosition.y >= Screen.height - _panBorderThickness)
            {
                return 1;
            }

            if (Input.mousePosition.y <= _panBorderThickness)
            {
                return -1;
            }

            return Input.GetAxis(VERTICAL_AXIS);
        }

        private void LimitPosition()
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -_limitX, _limitX),
                transform.position.y,
                Mathf.Clamp(transform.position.z, -_limitY, _limitY)
            );
        }

        private void Zoom()
        {
            var scrollInput = Input.GetAxis(ZOOMING_AXIS);

            if (
                _camera.transform.position.y <= _zoomDistanceMin && scrollInput > 0.0f ||
                _camera.transform.position.y >= _zoomDistanceMax && scrollInput < 0.0f
            )
            {
                return;
            }

            _camera.transform.position += _camera.transform.forward * scrollInput * _zoomSpeed;
        }

        #endregion
    }
}