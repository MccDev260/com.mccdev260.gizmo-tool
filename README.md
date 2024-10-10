Unity Gizmo Drawer Tool
==================

![Visualizing waypoints for a moving platform and character look source.](ReadMeImgs/MovingPlatformExample-LineStrip.gif)

Description
-----------

The Gizmo Drawer tool is a custom script that allows you to draw built in gizmos with customizable properties in the Unity scene. This script provides a versatile way to draw gizmos with a custom inspector to adjust their properties from within the editor.


Features
--------
* Has a custom Inspector to easily adjust gizmo properties directly in the Editor.
* Supports nearly all gizmo types, **except Frustum**, as of Editor v2023.3
* Customize the properties of each gizmo type, such as position, scale, color, and more.

## Supported Types:
*   Sphere
*   Wire Sphere
*   Cube
*   Wire Cube
*   Mesh
*   Wire Mesh
*   Line
*   Line List
*   Line Strip
*   Icon
*   GUI Texture

Installation
------------

1.  Open Unity package manager window.
2.  From the '+' menu in the top left corner, select '*Add package from git URL...*'
3.  Copy and paste this repository's URL into the URL field.

Usage
-----
1.  Attach the `GizmoDrawer` script to a GameObject in your scene.
2.  In the Inspector, configure the gizmo properties based on your desired gizmo type.
3.  Toggle the `drawOnSelectOnly` option to control whether the gizmo should be drawn only when the GameObject is selected.
4.  Customize the gizmo's appearance by adjusting the color, size, scale, texture, and other relevant properties.
5.  To see the gizmos in the Scene view, ensure the Gizmos toggle is enabled at the top of the Scene view.

Example
-------
To draw a sphere gizmo:

1.  Attach the `GizmoDrawer` script to a GameObject.
2.  Set the `gizmoType` to `Sphere`.
3.  Either manually set the gizmos origin via the exposed Vector3 `originPos` property, or reference a target Transform with the `originTransfom` property.
4.  Adjust the `floatRadius` to control the sphere's radius.
5.  Optionally, customize the `gizmoColor` to change the sphere's color.
6.  In the Scene view, the sphere gizmo will be drawn with the desired properties.

Contributing
------------

Contributions are welcome! If you find any issues or want to enhance the project, feel free to fork this repository and submit pull requests.

License
-------

This project is licensed under the MIT License. See the [LICENSE](LICENSE.md) file for details.
