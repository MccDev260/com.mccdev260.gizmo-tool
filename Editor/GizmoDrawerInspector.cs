using UnityEngine;
using UnityEditor;

namespace MccDev260.GizmoTool
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GizmoDrawer))]
    public class GizmoDrawerInspector : Editor
    {
        bool _hasColour;
        bool _showSingleOrigin;

        #region SerializedProps
        SerializedProperty
            serProp_colour,
            serProp_originTrans,
            serProp_originPos,
            serProp_gizmoType,
            serProp_floatRadius,
            serProp_vec3Scale,
            serProp_filePathString,
            serProp_allowScalingBool,
            serProp_mesh,
            serProp_meshRot,
            serProp_targetTrans,
            serProp_targetPos,
            serProp_linePointArray,
            serProp_transformLinePointArray,
            serProp_useTransformArrayBool,
            serProp_lineLoopedBool,
            serProp_screenRect,
            serProp_texture,
            serProp_mat;
        #endregion

        GizmoDrawer gizmoDrawer;

        private void OnEnable()
        {
            gizmoDrawer = (GizmoDrawer)target;

            serProp_colour = serializedObject.FindProperty("gizmoColor");
            serProp_originTrans = serializedObject.FindProperty("originTransform");
            serProp_originPos = serializedObject.FindProperty("originPos");
            serProp_gizmoType = serializedObject.FindProperty("gizmoType");

            serProp_floatRadius = serializedObject.FindProperty("floatRadius");

            serProp_vec3Scale = serializedObject.FindProperty("scale");

            serProp_filePathString = serializedObject.FindProperty("filePathString");
            serProp_allowScalingBool = serializedObject.FindProperty("allowScaling");

            serProp_mesh = serializedObject.FindProperty("mesh");
            serProp_meshRot = serializedObject.FindProperty("meshRotation");

            serProp_targetTrans = serializedObject.FindProperty("targetTransform");
            serProp_targetPos = serializedObject.FindProperty("targetPosition");

            serProp_linePointArray = serializedObject.FindProperty("points");
            serProp_transformLinePointArray = serializedObject.FindProperty("transformPoints");
            serProp_useTransformArrayBool = serializedObject.FindProperty("useTransformArray");
            serProp_lineLoopedBool = serializedObject.FindProperty("looped");

            serProp_screenRect = serializedObject.FindProperty("screenRect");
            serProp_texture = serializedObject.FindProperty("texture");
            serProp_mat = serializedObject.FindProperty("mat");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.Space();

            // Type
            EditorGUILayout.PropertyField(serProp_gizmoType, true);
            EditorGUILayout.Space();

            if (_showSingleOrigin)
            {
                // Transform
                EditorGUILayout.PropertyField(serProp_originTrans);

                if (gizmoDrawer.originTransform == null)
                {
                    EditorGUILayout.PropertyField(serProp_originPos);
                }

                EditorGUILayout.Space();
            }

            if (_hasColour)
            {
                EditorGUILayout.PropertyField(serProp_colour);
                EditorGUILayout.Space();
            }

            DrawInspector(gizmoDrawer.gizmoType, this, out _hasColour, out _showSingleOrigin);

            serializedObject.ApplyModifiedProperties();

            if (GUI.changed)
                SceneView.RepaintAll();
        }

        static void DrawInspector(GizmoDrawer.GizmoType type, GizmoDrawerInspector editor, out bool showColour, out bool showSingleOrigin)
        {
            showSingleOrigin = false;
            showColour = false;

            switch (type)
            {
                case GizmoDrawer.GizmoType.Sphere or GizmoDrawer.GizmoType.WireSphere:
                    showSingleOrigin = true;

                    showColour = true;
                    EditorGUILayout.PropertyField(editor.serProp_floatRadius);
                    break;

                case GizmoDrawer.GizmoType.Cube or GizmoDrawer.GizmoType.WireCube:
                    showSingleOrigin = true;

                    showColour = true;
                    EditorGUILayout.PropertyField(editor.serProp_vec3Scale);
                    break;

                case GizmoDrawer.GizmoType.Icon:
                    showSingleOrigin = true;
                    showColour = true;

                    EditorGUILayout.PropertyField(editor.serProp_filePathString);
                    EditorGUILayout.PropertyField(editor.serProp_allowScalingBool);
                    break;

                case GizmoDrawer.GizmoType.Mesh or GizmoDrawer.GizmoType.WireMesh:
                    showSingleOrigin = true;
                    showColour = true;

                    EditorGUILayout.PropertyField(editor.serProp_mesh);
                    EditorGUILayout.PropertyField(editor.serProp_meshRot);
                    EditorGUILayout.PropertyField(editor.serProp_vec3Scale);
                    break;

                case GizmoDrawer.GizmoType.Line:
                    showSingleOrigin = true;
                    showColour = true;


                    EditorGUILayout.PropertyField(editor.serProp_targetTrans);

                    if (editor.gizmoDrawer.targetTransform == null)
                    {
                        EditorGUILayout.PropertyField(editor.serProp_targetPos);
                    }
                    break;

                case GizmoDrawer.GizmoType.LineList or GizmoDrawer.GizmoType.LineStrip:
                    showSingleOrigin = false;
                    showColour = true;

                    EditorGUILayout.PropertyField(editor.serProp_useTransformArrayBool);

                    if (type == GizmoDrawer.GizmoType.LineStrip)
                        EditorGUILayout.PropertyField(editor.serProp_lineLoopedBool);

                    if (editor.gizmoDrawer.useTransformArray)
                    {
                        EditorGUILayout.PropertyField(editor.serProp_transformLinePointArray);
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(editor.serProp_linePointArray, true);
                    }
                    break;

                case GizmoDrawer.GizmoType.GuiTexture:
                    showSingleOrigin = true;
                    showColour = false;
                    EditorGUILayout.PropertyField(editor.serProp_texture);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(editor.serProp_screenRect);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(editor.serProp_mat);
                    break;
            }
        }
    }
#endif
}
