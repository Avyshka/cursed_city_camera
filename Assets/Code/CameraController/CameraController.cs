using UnityEngine;

namespace CursedCity.CameraController
{
    public class CameraController : MonoBehaviour
    {
        #region Fields

        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";
        private const string ZOOMING_AXIS = "Mouse ScrollWheel";
        private const float PAN_ACCELERATE_DAMPING = 0.93f;

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
        private float panAccelerateX;
        private float panAccelerateZ;

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
            panAccelerateX *= PAN_ACCELERATE_DAMPING;
            if (Input.GetAxis(HORIZONTAL_AXIS) > 0.0f || Input.GetAxis(HORIZONTAL_AXIS) < 0.0f)
            {
                panAccelerateX = Input.GetAxis(HORIZONTAL_AXIS);
            }
            
            if (Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                panAccelerateX = 1;
            }

            if (Input.mousePosition.x <= panBorderThickness)
            {
                panAccelerateX = -1;
            }

            return panAccelerateX;
        }

        private float GetVerticalAxisValue()
        {
            panAccelerateZ *= PAN_ACCELERATE_DAMPING;
            if (Input.GetAxis(VERTICAL_AXIS) > 0.0f || Input.GetAxis(VERTICAL_AXIS) < 0.0f)
            {
                panAccelerateZ = Input.GetAxis(VERTICAL_AXIS);
            }
            
            if (Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                panAccelerateZ = 1;
            }

            if (Input.mousePosition.y <= panBorderThickness)
            {
                panAccelerateZ = -1;
            }

            return panAccelerateZ;
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