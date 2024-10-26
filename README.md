Unity Gizmo Tool
==================

# Description
The Gizmo Tool enables you to use gizmos as components. This project is useful for level editing and other tasks where simple visualization is needed. See the [Wiki](https://github.com/MccDev260/com.mccdev260.gizmo-tool/wiki) section of the repo for more detail.

# Features
* Draw Gizmos with a component for quick visualization in the editor.
* Use a custom Inspector to easily adjust gizmo properties.
* Supports most gizmo types.
* All scripts in this package only run in the editor and are excluded from builds.

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
*   Ray

## Considerations
Since this package is excluded from builds, be mindful of controlling `GizmoDrawer` with runtime scripts.
See [Wiki/ExtendingBehaviour](https://github.com/MccDev260/com.mccdev260.gizmo-tool/wiki/Extending-Behviour) for a list of public members and suggested approaches.

# Installation
1.  Open Unity package manager window.
2.  From the '+' menu in the top left corner, select '*Add package from git URL...*'
3.  Copy and paste this repository's URL into the URL field.

# Getting Started
1.  Attach the `GizmoDrawer` script to a GameObject in your scene.
2.  Toggle the `drawOnSelectOnly` option to control whether the gizmo should be drawn only when the GameObject is selected.
3.  Configure the gizmos properties with the custom inspector.

For more detailed explanation about each gizmo type and their properties, See the [Wiki](https://github.com/MccDev260/com.mccdev260.gizmo-tool/wiki) section of the repo.

# Troubleshooting
## I can't see gizmos in the scene view?
- Make sure the Gizmos Menu is toggled on in the top right corner.
    -  If this doesn't work - Expand menu, find `GizmoDrawer` in the scripts list and ensure it's enabled.
- Check the `GizmoDrawer` properties in the inspector and ensure its origin is set to the expected value.

References
-------
* [Unity - Scripting API: Gizmos](https://docs.unity3d.com/ScriptReference/Gizmos.html)
* [Unity - Manual: Gizmos Menu](https://docs.unity3d.com/Manual/GizmosMenu.html).

Contributing
------------

Contributions are welcome! If you have any problems or an idea for a new feature, feel free to submit a new issue [here](https://github.com/MccDev260/com.mccdev260.gizmo-tool/issues).

License
-------

This project is licensed under the MIT License. See the [LICENSE](LICENSE.md) file for details.
