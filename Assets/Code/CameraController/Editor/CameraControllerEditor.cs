using UnityEditor;
using UnityEngine;

namespace CursedCity.CameraController.Editor
{
    [CustomEditor(typeof(CameraController))]
    public class CameraControllerEditor : UnityEditor.Editor
    {
        #region UnityMethods

        public override void OnInspectorGUI()
        {
            var cameraController = target as CameraController;

            if (cameraController.camera == null)
            {
                cameraController.camera = Camera.main;
            }
            cameraController.camera = (Camera) EditorGUILayout.ObjectField("Camera", cameraController.camera, typeof(Camera), true);

            cameraController.cameraAngle = EditorGUILayout.FloatField("Camera Angle", cameraController.cameraAngle);
            cameraController.Rotate();

            GUILayout.Space(8f);

            GUILayout.Label("Moving", EditorStyles.boldLabel);
            cameraController.moveSpeed = EditorGUILayout.FloatField("Move speed", cameraController.moveSpeed);
            cameraController.panBorderThickness = EditorGUILayout.FloatField("Pan Border Thickness", cameraController.panBorderThickness);
            GUILayout.Space(8f);

            GUILayout.Label("Zooming", EditorStyles.boldLabel);
            cameraController.zoomSpeed = EditorGUILayout.FloatField("Zoom speed", cameraController.zoomSpeed);
            cameraController.zoomDistanceMin = EditorGUILayout.FloatField("Zoom Distance Min", cameraController.zoomDistanceMin);
            cameraController.zoomDistanceMax = EditorGUILayout.FloatField("Zoom Distance Max", cameraController.zoomDistanceMax);
            GUILayout.Space(8f);

            cameraController.isBorderLimits = GUILayout.Toggle(cameraController.isBorderLimits, "Border Limits");
            if (cameraController.isBorderLimits)
            {
                cameraController.limitTop = EditorGUILayout.FloatField("Border Limit Top", cameraController.limitTop);
                cameraController.limitRight = EditorGUILayout.FloatField("Border Limit Right", cameraController.limitRight);
                cameraController.limitBottom = EditorGUILayout.FloatField("Border Limit Bottom", cameraController.limitBottom);
                cameraController.limitLeft = EditorGUILayout.FloatField("Border Limit Left", cameraController.limitLeft);

                if (cameraController.limitLeft >= cameraController.limitRight)
                {
                    EditorGUILayout.HelpBox("RIGHT limit less then LEFT!", MessageType.Error);
                }

                if (cameraController.limitBottom >= cameraController.limitTop)
                {
                    EditorGUILayout.HelpBox("TOP limit less then BOTTOM!", MessageType.Error);
                }
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(cameraController);
            }
        }

        public void OnSceneGUI()
        {
            if (!(target as CameraController).isBorderLimits)
            {
                return;
            }

            Handles.color = Color.magenta;
            AddBorders();
            AddBorderControls();

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target as CameraController);
            }
        }

        #endregion


        #region Methods

        private void AddBorders()
        {
            var camera = target as CameraController;

            var topLeftCorner = new Vector3(camera.limitLeft, 0, camera.limitTop);
            var topRightCorner = new Vector3(camera.limitRight, 0, camera.limitTop);
            var bottomLeftCorner = new Vector3(camera.limitLeft, 0, camera.limitBottom);
            var bottomRightCorner = new Vector3(camera.limitRight, 0, camera.limitBottom);

            Handles.DrawLine(topLeftCorner, topRightCorner);
            Handles.DrawLine(topRightCorner, bottomRightCorner);
            Handles.DrawLine(bottomRightCorner, bottomLeftCorner);
            Handles.DrawLine(bottomLeftCorner, topLeftCorner);
        }

        private void AddBorderControls()
        {
            var camera = target as CameraController;
            var forward = camera.transform.forward;
            var right = camera.transform.right;

            camera.limitTop = Handles.ScaleValueHandle(
                camera.limitTop,
                forward * camera.limitTop + right * (camera.limitLeft + camera.limitRight) * 0.5f,
                Quaternion.Euler(0, 0, 0),
                10,
                Handles.ConeHandleCap,
                1
            );

            camera.limitRight = Handles.ScaleValueHandle(
                camera.limitRight,
                right * camera.limitRight + forward * (camera.limitBottom + camera.limitTop) * 0.5f,
                Quaternion.Euler(0, 90, 0),
                10,
                Handles.ConeHandleCap,
                1
            );

            camera.limitBottom = Handles.ScaleValueHandle(
                camera.limitBottom,
                forward * camera.limitBottom + right * (camera.limitLeft + camera.limitRight) * 0.5f,
                Quaternion.Euler(0, 180f, 0),
                10,
                Handles.ConeHandleCap,
                1
            );

            camera.limitLeft = Handles.ScaleValueHandle(
                camera.limitLeft,
                right * camera.limitLeft + forward * (camera.limitBottom + camera.limitTop) * 0.5f,
                Quaternion.Euler(0, 270f, 0),
                10,
                Handles.ConeHandleCap,
                1
            );
        }

        #endregion
    }
}