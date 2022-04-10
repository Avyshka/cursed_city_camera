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

        public Camera camera;
        public float cameraAngle;

        public float moveSpeed;
        public float panBorderThickness;

        public float zoomSpeed;
        public float zoomDistanceMin;
        public float zoomDistanceMax;
        
        public bool isBorderLimits;
        public float limitLeft;
        public float limitRight;
        public float limitTop;
        public float limitBottom;

        private float _panAccelerateX;
        private float _panAccelerateZ;

        #endregion


        #region UnityMethods
        
        private void Start()
        {
            Rotate();
        }

        private void Update()
        {
            Move();
            LimitPosition();
            Zoom();
        }

        #endregion


        #region Methods

        public void Rotate()
        {
            camera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            camera.transform.position = Vector3.zero;
            camera.transform.rotation = Quaternion.Euler(cameraAngle, 0f, 0f);
            camera.transform.position = transform.position + camera.transform.forward * -zoomDistanceMax;
        }
        
        private void Move()
        {
            var inputX = GetHorizontalAxisValue();
            var inputZ = GetVerticalAxisValue();
            var direction = transform.forward * inputZ + transform.right * inputX;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        private float GetHorizontalAxisValue()
        {
            _panAccelerateX *= PAN_ACCELERATE_DAMPING;
            if (Input.GetAxis(HORIZONTAL_AXIS) > 0.0f || Input.GetAxis(HORIZONTAL_AXIS) < 0.0f)
            {
                _panAccelerateX = Input.GetAxis(HORIZONTAL_AXIS);
            }
            
            if (Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                _panAccelerateX = 1;
            }

            if (Input.mousePosition.x <= panBorderThickness)
            {
                _panAccelerateX = -1;
            }

            return _panAccelerateX;
        }

        private float GetVerticalAxisValue()
        {
            _panAccelerateZ *= PAN_ACCELERATE_DAMPING;
            if (Input.GetAxis(VERTICAL_AXIS) > 0.0f || Input.GetAxis(VERTICAL_AXIS) < 0.0f)
            {
                _panAccelerateZ = Input.GetAxis(VERTICAL_AXIS);
            }
            
            if (Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                _panAccelerateZ = 1;
            }

            if (Input.mousePosition.y <= panBorderThickness)
            {
                _panAccelerateZ = -1;
            }

            return _panAccelerateZ;
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
                camera.transform.position.y <= zoomDistanceMin && scrollInput > 0.0f ||
                camera.transform.position.y >= zoomDistanceMax && scrollInput < 0.0f
            )
            {
                return;
            }

            camera.transform.position += camera.transform.forward * scrollInput * zoomSpeed;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawCube(transform.position, Vector3.one);
            Gizmos.DrawLine(camera.transform.position, transform.position);
        }

        #endregion
    }
}