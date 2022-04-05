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
            var camera = target as CameraController;
          
            camera.cameraAngle = EditorGUILayout.FloatField("Camera Angle", camera.cameraAngle);
            GUILayout.Space(8f);
            
            GUILayout.Label ("Moving", EditorStyles.boldLabel);
            camera.moveSpeed = EditorGUILayout.FloatField("Move speed", camera.moveSpeed);
            camera.panBorderThickness = EditorGUILayout.FloatField("Pan Border Thickness", camera.panBorderThickness);
            GUILayout.Space(8f);
            
            GUILayout.Label ("Zooming", EditorStyles.boldLabel);
            camera.zoomSpeed = EditorGUILayout.FloatField("Zoom speed", camera.zoomSpeed);
            camera.zoomDistanceMin = EditorGUILayout.FloatField("Zoom Distance Min", camera.zoomDistanceMin);
            camera.zoomDistanceMax = EditorGUILayout.FloatField("Zoom Distance Max", camera.zoomDistanceMax);
            GUILayout.Space(8f);

            camera.isBorderLimits = GUILayout.Toggle(camera.isBorderLimits, "Border Limits");
            if (camera.isBorderLimits)
            {
                camera.limitTop = EditorGUILayout.FloatField("Border Limit Top", camera.limitTop);
                camera.limitRight = EditorGUILayout.FloatField("Border Limit Right", camera.limitRight);
                camera.limitBottom = EditorGUILayout.FloatField("Border Limit Bottom", camera.limitBottom);
                camera.limitLeft = EditorGUILayout.FloatField("Border Limit Left", camera.limitLeft);

                if (camera.limitLeft >= camera.limitRight)
                {
                    EditorGUILayout.HelpBox("RIGHT limit less then LEFT!", MessageType.Error);
                }
                if (camera.limitBottom >= camera.limitTop)
                {
                    EditorGUILayout.HelpBox("TOP limit less then BOTTOM!", MessageType.Error);
                }
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