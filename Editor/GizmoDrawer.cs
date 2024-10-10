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
        [Tooltip("Sets the origin of the gizmo to the position of this transform.")]
        [HideInInspector] public Transform originTransform;
        [Tooltip("Directly enter world postion for the gizmo origin.")]
        [HideInInspector] public Vector3 originPos;
        [HideInInspector] public GizmoType gizmoType;
        [HideInInspector] public Color gizmoColor = Color.blue;

        //Icon
        [Tooltip("Source should be placed in Assets/Gizmos folder.")]
        [HideInInspector] public string filePathString;
        [HideInInspector] public bool allowScaling;

        //Sphere
        [HideInInspector] public float floatRadius = 1f;
        //Cube
        [HideInInspector] public bool uniformScale; // Not implemented yet.

        // Mesh
        [HideInInspector] public Mesh mesh;
        [Tooltip("Copy the transform values from the object this script is on.")]
        [HideInInspector] public bool useAttachedTransformValues;
        [Tooltip("Use the mesh thats on this object as the gizmo.")]
        [HideInInspector] public bool useMeshOnFilter;
        [Tooltip("Copy values from the transform referenced as the origin.")]
        [HideInInspector] public bool useOriginTransformValues;

        [HideInInspector] public Vector3 meshRotation;
        [HideInInspector] public Vector3 scale = Vector3.one;

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

        #region Public Methods
        public void SetColour(string hexCode) => SetColour(HexToColor(hexCode));

        public void SetColour(Color colour)
        {
            gizmoColor = colour;
        }
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

        private static Color HexToColor(string hex)
        {
            if (hex.StartsWith("#"))
            {
                hex = hex.Substring(1);
            }

            if (hex.Length != 6)
            {
                Debug.LogError("Invalid hex color code");
            }

            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            return new Color(r / 255f, g / 255f, b / 255f);
        }
    }
}
#endif
