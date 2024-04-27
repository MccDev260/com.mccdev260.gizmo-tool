#if UNITY_EDITOR
    using UnityEngine;

    namespace MccDev260.GizmoTool
    {
        [ExecuteInEditMode]
        public class GizmoDrawer : MonoBehaviour
        {
            [SerializeField] private bool drawOnSelectOnly;

            #region Properties
            //Shared
            [HideInInspector] public Transform originTransform;
            [HideInInspector] public Vector3 originPos;
            [HideInInspector] public GizmoType gizmoType;
            [HideInInspector] public Color gizmoColor = Color.blue;

            //Icon
            [HideInInspector] public string filePathString;
            [HideInInspector] public bool allowScaling;

            //Sphere
            [HideInInspector] public float floatRadius = 1f;
            //Cube
            [HideInInspector] public bool uniformScale;
            [HideInInspector] public Vector3 scale = Vector3.one;

            // Mesh
            [HideInInspector] public Mesh mesh;
            [HideInInspector] public Vector3 meshRotation;
            [HideInInspector] public bool useAttachedTransformValues;
            [HideInInspector] public bool useMeshOnFilter;
            //Line
            [HideInInspector] public Transform targetTransform;
            [HideInInspector] public Vector3 targetPosition;

            //LineList & LineStrip
            [HideInInspector] public Vector3[] points = new Vector3[0];

            [HideInInspector] public bool useTransformArray;
            [HideInInspector] public Transform[] transformPoints = new Transform[0];

            [HideInInspector] public bool looped;

            // GUI Texture
            /// <summary>
            /// The size and position of the texture on the "screen" defined by the XY plane.
            /// </summary>
            [HideInInspector] public Rect screenRect;
            /// <summary>
            /// The texture to be displayed.
            /// </summary>
            [HideInInspector] public Texture texture;
            /// <summary>
            /// An optional material to apply the texture.
            /// </summary>
            [HideInInspector] public Material mat;
            #endregion

            private void OnDrawGizmosSelected()
            {
                if (drawOnSelectOnly)
                    GizmoTool.DrawGizmo(this);
            }

            private void OnDrawGizmos()
            {
                if (!drawOnSelectOnly)
                    GizmoTool.DrawGizmo(this);
            }
        }
    }
#endif
