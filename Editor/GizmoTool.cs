#if UNITY_EDITOR
using System;
using UnityEngine;

namespace MccDev260.GizmoTool
{
    [Serializable]
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

    internal static class GizmoTool
    {
        internal static void DrawGizmo(GizmoDrawer drawer)
        {
            var scale = drawer.scale;

            switch (drawer.gizmoType)
            {
                case GizmoType.Sphere:
                    Gizmos.DrawSphere(GetOriginPosition(drawer), drawer.floatRadius, drawer.gizmoColor);
                    break;

                case GizmoType.Cube:
                    Gizmos.DrawCube(GetOriginPosition(drawer), scale, drawer.gizmoColor);
                    break;

                case GizmoType.WireSphere:
                    Gizmos.DrawWireSphere(GetOriginPosition(drawer), drawer.floatRadius, drawer.gizmoColor);
                    break;

                case GizmoType.WireCube:
                    Gizmos.DrawWireCube(GetOriginPosition(drawer), scale, drawer.gizmoColor);
                    break;

                case GizmoType.Icon:
                    Gizmos.DrawIcon(GetOriginPosition(drawer), drawer.filePathString, drawer.allowScaling, drawer.gizmoColor);
                    break;

                case GizmoType.Mesh:
                    if (drawer.useAttachedTransformValues)
                        drawer.originTransform = drawer.transform;

                    Gizmos.DrawMesh(GetMesh(drawer), GetOriginPosition(drawer), GetMeshRotation(drawer), GetMeshScale(drawer), drawer.gizmoColor);
                    break;

                case GizmoType.WireMesh:
                    if (drawer.useAttachedTransformValues)
                        drawer.originTransform = drawer.transform;

                    Gizmos.DrawWireMesh(GetMesh(drawer), GetOriginPosition(drawer), GetMeshRotation(drawer), GetMeshScale(drawer), drawer.gizmoColor);
                    break;

                case GizmoType.Line:
                    drawer.targetPosition = GetLineTargetPosition(drawer);
                    Gizmos.DrawLine(GetOriginPosition(drawer), drawer.targetPosition, drawer.gizmoColor);
                    break;

                case GizmoType.LineList or GizmoType.LineStrip:

                    if (drawer.useTransformArray)
                    {
                        SyncPositionArrays(drawer, out drawer.points, out drawer.transformPoints);
                    }

                    if (drawer.gizmoType == GizmoType.LineStrip)
                        Gizmos.DrawLineStrip(drawer.points, drawer.gizmoColor, drawer.looped);
                    else
                        Gizmos.DrawLineList(drawer.points, drawer.gizmoColor);
                    break;

                case GizmoType.GuiTexture:
                    if (drawer.texture != null)
                        Gizmos.DrawGuiTexture(drawer.screenRect, drawer.texture, drawer.mat);
                    break;
            }
        }

        static Vector3 GetLineTargetPosition(GizmoDrawer drawer)
        {
            if (drawer.targetTransform != null)
            {
                return drawer.targetTransform.position;
            }

            return drawer.targetPosition;
        }

        static Vector3 GetOriginPosition(GizmoDrawer drawer)
        {
            if (drawer.originTransform != null)
            {
                drawer.originPos = drawer.originTransform.position;
            }
            return drawer.originPos;
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

        static Quaternion GetMeshRotation(GizmoDrawer drawer)
        {
            if (drawer.useAttachedTransformValues)
            {
                return drawer.gameObject.transform.rotation;
            }
            else if (drawer.originTransform != null && drawer.useOriginTransformValues)
            {
                return drawer.originTransform.rotation;
            }
                
            return Quaternion.Euler(drawer.meshRotation);
        }

        static Vector3 GetMeshScale(GizmoDrawer drawer)
        {
            if (drawer.useAttachedTransformValues)
            {
                return drawer.gameObject.transform.localScale;
            }
            else if (drawer.originTransform != null && drawer.useOriginTransformValues)
            {
                return drawer.originTransform.localScale;

            }

            return drawer.scale;
        }

        static Mesh GetMesh(GizmoDrawer drawer) 
        {
            var filter = drawer.transform.GetComponent<MeshFilter>();

            if (filter && drawer.useMeshOnFilter)
            {
                return filter.sharedMesh;
            }

            return drawer.mesh;
        }
    }
}
#endif