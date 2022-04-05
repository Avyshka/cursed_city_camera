using UnityEngine;

namespace CursedCity.CameraController
{
    public class CameraController : MonoBehaviour
    {
        #region Fields

        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";
        private const string ZOOMING_AXIS = "Mouse ScrollWheel";

        public float cameraAngle;

        public float moveSpeed;
        public float panBorderThickness;
        
        public bool isBorderLimits;
        public float limitLeft;
        public float limitRight;
        public float limitTop;
        public float limitBottom;

        public float zoomSpeed;
        public float zoomDistanceMin;
        public float zoomDistanceMax;

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
            _camera.transform.rotation = Quaternion.Euler(cameraAngle, 0f, 0f);
            _camera.transform.position = transform.position + _camera.transform.forward * -zoomDistanceMax;
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
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        private float GetHorizontalAxisValue()
        {
            if (Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                return 1;
            }

            if (Input.mousePosition.x <= panBorderThickness)
            {
                return -1;
            }

            return Input.GetAxis(HORIZONTAL_AXIS);
        }

        private float GetVerticalAxisValue()
        {
            if (Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                return 1;
            }

            if (Input.mousePosition.y <= panBorderThickness)
            {
                return -1;
            }

            return Input.GetAxis(VERTICAL_AXIS);
        }

        private void LimitPosition()
        {
            if (!isBorderLimits)
            {
                return;
            }
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, limitLeft, limitRight),
                transform.position.y,
                Mathf.Clamp(transform.position.z, limitBottom, limitTop)
            );
        }

        private void Zoom()
        {
            var scrollInput = Input.GetAxis(ZOOMING_AXIS);

            if (
                _camera.transform.position.y <= zoomDistanceMin && scrollInput > 0.0f ||
                _camera.transform.position.y >= zoomDistanceMax && scrollInput < 0.0f
            )
            {
                return;
            }

            _camera.transform.position += _camera.transform.forward * scrollInput * zoomSpeed;
        }
        
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(transform.position, Vector3.one);
        }

        #endregion
    }
}