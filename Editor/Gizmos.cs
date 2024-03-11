using System;
using UnityEngine;

# if UNITY_EDITOR
namespace MccDev260.GizmoTool
{
    internal static class Gizmos
    {
        public static void DrawSphere(Vector3 worldPos, float radius, Color colour)
        {
            UnityEngine.Gizmos.color = colour;
            UnityEngine.Gizmos.DrawSphere(worldPos, radius);
        }

        public static void DrawWireSphere(Vector3 worldPos, float radius, Color colour)
        {
            UnityEngine.Gizmos.color = colour;
            UnityEngine.Gizmos.DrawWireSphere(worldPos, radius);
        }

        public static void DrawCube(Vector3 position, Vector3 size, Color colour)
        {
            UnityEngine.Gizmos.color = colour;
            UnityEngine.Gizmos.DrawCube(position, size);
        }

        public static void DrawWireCube(Vector3 position, Vector3 size, Color colour)
        {
            UnityEngine.Gizmos.color = colour;
            UnityEngine.Gizmos.DrawWireCube(position, size);
        }

        /// <summary>
        /// (UnityDocs:2022.3-Gizmos.DrawIcon)
        /// Draw an icon at a position in the Scene view.
        /// The image file should be placed in the Assets/Gizmos folder.
        /// </summary>
        /// <param name="position">Location of the icon in world space.</param>
        /// <param name="fileName">File should be placed in the Assets/Gizmos folder.</param>
        /// <param name="allowScaling">Determines if the icon is allowed to be scaled.</param>
        public static void DrawIcon(Vector3 position, string fileName, bool allowScaling, Color colour) 
        {
            UnityEngine.Gizmos.DrawIcon(position, fileName, allowScaling, colour);
        }

        public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale, Color colour)
        {
            UnityEngine.Gizmos.color = colour;
            UnityEngine.Gizmos.DrawMesh(mesh, position, rotation, scale);
        }

        public static void DrawWireMesh(Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale, Color colour)
        {
            UnityEngine.Gizmos.color = colour;
            UnityEngine.Gizmos.DrawWireMesh(mesh, position, rotation, scale);
        }

        public static void DrawLine(Vector3 startPos, Vector3 endPos, Color colour)
        {
            UnityEngine.Gizmos.color = colour;
            UnityEngine.Gizmos.DrawLine(startPos, endPos);
        }

        public static void DrawLineList(Vector3[] points, Color colour)
        {
            UnityEngine.Gizmos.color = colour;
            try
            {
                UnityEngine.Gizmos.DrawLineList(points);
            }
            catch (UnityException)
            {
                Debug.LogWarning("Points array size must be even!");
            }
        }

        public static void DrawLineStrip(Vector3[] points, Color colour, bool looped)
        {
            UnityEngine.Gizmos.color = colour;
            UnityEngine.Gizmos.DrawLineStrip(points, looped);
        }

        public static void DrawGuiTexture(Rect screenRect, Texture texture, Material mat = null)
        {
            UnityEngine.Gizmos.DrawGUITexture(screenRect, texture, mat);
        }
    }
}
#endif
