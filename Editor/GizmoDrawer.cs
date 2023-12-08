using UnityEngine;

namespace MccDev260.GizmoTool
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
    public class GizmoDrawer : MonoBehaviour
    {
        [SerializeField] private bool drawOnSelectOnly;

        #region Properties
        [HideInInspector]
        public enum GizmoType
        {
            Sphere,
            WireSphere,

            Cube,
            WireCube,

            Mesh,
            WireMesh,

            Line,
            LineList,
            LineStrip,

            Icon,
            GuiTexture,
        }

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
                DrawGizmo(this);
        }

        private void OnDrawGizmos()
        {
            if (!drawOnSelectOnly)
                DrawGizmo(this);
        }

        static Vector3 LineTargetPosition(GizmoDrawer drawer)
        {
            if (drawer.targetTransform != null)
            {
                return drawer.targetTransform.position;
            }

            return drawer.targetPosition;
        }

        static Vector3 OriginPosition(GizmoDrawer drawer)
        {
            if (drawer.originTransform != null)
            {
                return drawer.originTransform.position;
            }
            else
            {
                return drawer.originPos;
            }
        }

        static void SyncPositionArrays(GizmoDrawer drawer, out Vector3[] points, out Transform[] transforms)
        {
            points = drawer.points;
            transforms = drawer.transformPoints;

            if (transforms.Length > 0)
            {
                points = new Vector3[transforms.Length];

                for (int i = 0; i < transforms.Length; i++)
                {
                    if (transforms[i] != null)
                    {
                        points[i] = transforms[i].position;
                    }
                }
            }
        }

        static void DrawGizmo(GizmoDrawer drawer)
        {
            drawer.originPos = OriginPosition(drawer);
            var origin = drawer.originPos;

            var scale = drawer.scale;

            switch (drawer.gizmoType)
            {
                case GizmoType.Sphere:
                    GizmoUtil.DrawSphere(origin, drawer.floatRadius, drawer.gizmoColor);
                    break;

                case GizmoType.Cube:
                    GizmoUtil.DrawCube(origin, scale, drawer.gizmoColor);
                    break;

                case GizmoType.WireSphere:
                    GizmoUtil.DrawWireSphere(origin, drawer.floatRadius, drawer.gizmoColor);
                    break;

                case GizmoType.WireCube:
                    GizmoUtil.DrawWireCube(origin, scale, drawer.gizmoColor);
                    break;

                case GizmoType.Icon:
                    GizmoUtil.DrawIcon(origin, drawer.filePathString, drawer.allowScaling, drawer.gizmoColor);
                    break;

                case GizmoType.Mesh:
                    var rotation = Quaternion.Euler(drawer.meshRotation);
                    GizmoUtil.DrawMesh(drawer.mesh, origin, rotation, scale, drawer.gizmoColor);
                    break;

                case GizmoType.WireMesh:
                    rotation = Quaternion.Euler(drawer.meshRotation);
                    GizmoUtil.DrawWireMesh(drawer.mesh, origin, rotation, scale, drawer.gizmoColor);
                    break;

                case GizmoType.Line:
                    drawer.targetPosition = LineTargetPosition(drawer);
                    GizmoUtil.DrawLine(origin, drawer.targetPosition, drawer.gizmoColor);
                    break;

                case GizmoType.LineList or GizmoType.LineStrip:

                    if (drawer.useTransformArray)
                    {
                        SyncPositionArrays(drawer, out drawer.points, out drawer.transformPoints);
                    }

                    if (drawer.gizmoType == GizmoType.LineStrip)
                        GizmoUtil.DrawLineStrip(drawer.points, drawer.gizmoColor, drawer.looped);
                    else
                        GizmoUtil.DrawLineList(drawer.points, drawer.gizmoColor);
                    break;

                case GizmoType.GuiTexture:
                    if (drawer.texture != null)
                        GizmoUtil.DrawGuiTexture(drawer.screenRect, drawer.texture, drawer.mat);
                    break;
            }
        }
    }
#endif
}
