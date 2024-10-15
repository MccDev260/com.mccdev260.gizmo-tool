#if UNITY_EDITOR
    using UnityEngine;
    using UnityEditor;

namespace MccDev260.GizmoTool
{
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
        serProp_mat,
        serProp_useTransformVals,
        serProp_useMeshOnFilter,
        serProp_useOriginTransformValues;
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

            serProp_useTransformVals = serializedObject.FindProperty("useAttachedTransformValues");
            serProp_useMeshOnFilter = serializedObject.FindProperty("useMeshOnFilter");
            serProp_useOriginTransformValues = serializedObject.FindProperty("useOriginTransformValues");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.Space();

            // Type selection
            EditorGUILayout.PropertyField(serProp_gizmoType, true);
            EditorGUILayout.Space();

            if (_showSingleOrigin && ShowOriginOptions(gizmoDrawer))
            {
                // Transform
                EditorGUILayout.PropertyField(serProp_originTrans);

                // Show Vector3 to set position directly if theres no transform selected.
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

        static void DrawInspector(GizmoType type, GizmoDrawerInspector editor, out bool showColour, out bool showSingleOrigin)
        {
            showSingleOrigin = false;
            showColour = false;

            switch (type)
            {
                case GizmoType.Sphere or GizmoType.WireSphere:
                    showSingleOrigin = true;

                    showColour = true;
                    EditorGUILayout.PropertyField(editor.serProp_floatRadius);
                    break;

                case GizmoType.Cube or GizmoType.WireCube:
                    showSingleOrigin = true;

                    showColour = true;
                    EditorGUILayout.PropertyField(editor.serProp_vec3Scale);
                    break;

                case GizmoType.Icon:
                    showSingleOrigin = true;
                    showColour = true;

                    EditorGUILayout.PropertyField(editor.serProp_filePathString);
                    EditorGUILayout.PropertyField(editor.serProp_allowScalingBool);
                    break;

                case GizmoType.Mesh or GizmoType.WireMesh:
                    showSingleOrigin = true;
                    showColour = true;

                    if (CanShowMeshOption(editor.gizmoDrawer))
                    {
                        EditorGUILayout.PropertyField(editor.serProp_mesh);
                    }

                    if (editor.gizmoDrawer.transform.GetComponent<MeshFilter>())
                    {
                        EditorGUILayout.PropertyField(editor.serProp_useMeshOnFilter);
                    }

                    EditorGUILayout.PropertyField(editor.serProp_useTransformVals);
                    bool useTransform = editor.serProp_useTransformVals.boolValue;

                    if (!useTransform && editor.gizmoDrawer.originTransform != null)
                        EditorGUILayout.PropertyField(editor.serProp_useOriginTransformValues);
                    else editor.gizmoDrawer.useOriginTransformValues = false;

                    /* Show rotation and scale fields if we're not using either the transform of the object
                     * the gizmo is on, or the origin transfom.
                     */
                    if (useTransform || editor.serProp_useOriginTransformValues.boolValue == true) return;

                    EditorGUILayout.PropertyField(editor.serProp_meshRot);
                    EditorGUILayout.PropertyField(editor.serProp_vec3Scale);
                    break;

                case GizmoType.Line:
                    showSingleOrigin = true;
                    showColour = true;


                    EditorGUILayout.PropertyField(editor.serProp_targetTrans);

                    if (editor.gizmoDrawer.targetTransform == null)
                    {
                        EditorGUILayout.PropertyField(editor.serProp_targetPos);
                    }
                    break;

                case GizmoType.LineList or GizmoType.LineStrip:
                    showSingleOrigin = false;
                    showColour = true;

                    EditorGUILayout.PropertyField(editor.serProp_useTransformArrayBool);

                    if (type == GizmoType.LineStrip)
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

                case GizmoType.GuiTexture:
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

        static bool ShowOriginOptions(GizmoDrawer drawer)
        {
            if (drawer.gizmoType == GizmoType.Mesh || drawer.gizmoType == GizmoType.WireMesh)
            {
                if (drawer.useAttachedTransformValues) 
                    return false;
            }

            return true;
        }

        static bool CanShowMeshOption(GizmoDrawer drawer)
        {
            if (drawer.useMeshOnFilter)
                return false;

            return true;
        }
    }
}
#endif
