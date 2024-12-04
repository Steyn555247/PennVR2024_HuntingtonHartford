using UnityEditor;
using UnityEngine;
using System.Collections;

public class AutoPivotCreator : EditorWindow
{
    public GameObject Mesh_AddPivotTo;
    private int TopBottom = 0;
    private int LeftRightFrontBack = 0;
    private int TopBottomPreviousSelection = 0;
    private int LeftRightFrontBackPreviousSelection = 0;

    private GameObject previousMesh;

    private int spacing = 10;
    private GameObject newTransformObject;

    [MenuItem("XRF/AutoPivotCreator")]

    static void Init()
    {
        var window = GetWindowWithRect<AutoPivotCreator>(new Rect(0, 0, 200, 250));
        window.Show();
    }



    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        Mesh_AddPivotTo = EditorGUILayout.ObjectField(Mesh_AddPivotTo, typeof(GameObject), true) as GameObject;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(spacing);


        string[] UpDownOptions = new string[] { "Top", "Center", "Bottom" };
        TopBottom = GUILayout.SelectionGrid(TopBottom, UpDownOptions, 1, EditorStyles.radioButton);

        EditorGUILayout.Space(spacing);

        string[] LeftRightFrontBack_Options = new string[] { "Left", "Right", "Front", "Back", "Center" };
        LeftRightFrontBack = GUILayout.SelectionGrid(LeftRightFrontBack, LeftRightFrontBack_Options, 1, EditorStyles.radioButton);


        if (Mesh_AddPivotTo != null)
        {
            if (Mesh_AddPivotTo == previousMesh)
            {
                //any time i touch something, if Mesh_AddPivotTo is not null, add a temporary object...
                if (LeftRightFrontBack != LeftRightFrontBackPreviousSelection || TopBottom != TopBottomPreviousSelection)
                {
                    if (newTransformObject == null)
                    {
                        newTransformObject = new GameObject("AutoPivot_" + Mesh_AddPivotTo.name);
                    }

                    UpdatePivotPosition();
                }
            }
            else
            {
                //new mesh
                if (newTransformObject != null)
                {
                    DestroyImmediate(newTransformObject);
                }

                newTransformObject = new GameObject("AutoPivot_" + Mesh_AddPivotTo.name);
                previousMesh = Mesh_AddPivotTo;
                UpdatePivotPosition();
            }
        }

        EditorGUILayout.Space(spacing);


        if (GUILayout.Button("Generate Pivot Point"))
        {
            if (Mesh_AddPivotTo != null)
            {
                GameObject duplicate = GameObject.Instantiate(Mesh_AddPivotTo);
                duplicate.transform.position = Mesh_AddPivotTo.transform.position;
                duplicate.transform.rotation = Mesh_AddPivotTo.transform.rotation;
                duplicate.transform.localScale = Mesh_AddPivotTo.transform.localScale;

                duplicate.transform.parent = newTransformObject.transform;
                Mesh_AddPivotTo.SetActive(false);

                Selection.activeGameObject = duplicate;
                Selection.activeGameObject = newTransformObject;

                Mesh_AddPivotTo = null;
                newTransformObject = null;
            }
        }
    }
    void UpdatePivotPosition()
    {
        Selection.activeGameObject = newTransformObject;
        Transform parentTransform = Mesh_AddPivotTo.transform.parent;
        Bounds theBounds = Mesh_AddPivotTo.GetComponent<MeshRenderer>().bounds;

        Vector3 theCenter = theBounds.center;
        float theX = theBounds.extents.x;
        float theY = theBounds.extents.y;
        float theZ = theBounds.extents.z;

        if (TopBottom == 0)
        {
            Debug.Log("Top is selected");
            theCenter = theCenter + new Vector3(0, theY, 0);
        }
        else if (TopBottom == 1)
        {
            Debug.Log("Center is selected");
            theCenter = theCenter - new Vector3(0, 0, 0);
        }
        else if (TopBottom == 2)
        {
            Debug.Log("Bottom is selected");
            theCenter = theCenter - new Vector3(0, theY, 0);
        }


        if (LeftRightFrontBack == 0)
        {
            Debug.Log("Left is selected");
            theCenter = theCenter - new Vector3(theX, 0, 0);
        }
        else if (LeftRightFrontBack == 1)
        {
            Debug.Log("Right is selected");
            theCenter = theCenter + new Vector3(theX, 0, 0);
        }
        else if (LeftRightFrontBack == 2)
        {
            Debug.Log("Front is selected");
            theCenter = theCenter - new Vector3(0, 0, theZ);
        }
        else if (LeftRightFrontBack == 3)
        {
            Debug.Log("Back is selected");
            theCenter = theCenter + new Vector3(0, 0, theZ);
        }
        else if (LeftRightFrontBack == 4)
        {
            Debug.Log("Center is selected");
            theCenter = theCenter + new Vector3(0, 0, 0);
        }

        newTransformObject.transform.position = theCenter;
        newTransformObject.transform.parent = parentTransform;
        newTransformObject.transform.localRotation = Quaternion.identity;

        LeftRightFrontBackPreviousSelection = LeftRightFrontBack;
        TopBottomPreviousSelection = TopBottom;
    }
}