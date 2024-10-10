 using UnityEngine;
 using UnityEditor;
using System.Linq;
using UnityEditor.Events;

[CustomEditor(typeof(XRF_UPenn_InteractionController))]
public class XRF_InteractionControllerEditor : Editor
{
    private int previousOnCount;
    private int previousOffCount;

    public override void OnInspectorGUI()
    {
        XRF_UPenn_InteractionController script = (XRF_UPenn_InteractionController)target;
        script.myType = (XRF_UPenn_InteractionController.InteractionType)EditorGUILayout.EnumPopup("Interaction Type", script.myType);

        //highlight material if raycast is selected only! also, turn off if you are a teleport controller
        if (script.gameObject.GetComponent<Collider>().isTrigger == false && script.myType != XRF_UPenn_InteractionController.InteractionType.TeleportController)
        {
            script.HighlightMaterial = (Material)EditorGUILayout.ObjectField("Highlight Material", script.HighlightMaterial, typeof(Material), true);
            //if this is empty, place a standard material there
            if(script.HighlightMaterial == null)
            {
                script.HighlightMaterial = new Material(Shader.Find("Standard"));
                script.HighlightMaterial.name = "Empty Material";
                script.HighlightMaterial.color = Color.magenta;
            }
        }

        //
        if(script.gameObject.GetComponent<Collider>().isTrigger == true)
        {
            //don't allow you to do grab or teleport if you are a trigger
            if(script.myType == XRF_UPenn_InteractionController.InteractionType.TeleportController || script.myType == XRF_UPenn_InteractionController.InteractionType.GrabAndReturn || script.myType == XRF_UPenn_InteractionController.InteractionType.GrabAndStay)
            {
                script.myType = XRF_UPenn_InteractionController.InteractionType.AnimationController;
            }
        }
        else if (script.myType == XRF_UPenn_InteractionController.InteractionType.OnOffController)
        {
            script.NumberOfThingsToTurnON = EditorGUILayout.IntField("Number of Things to Turn ON", script.NumberOfThingsToTurnON);
            GameObject[] tempOns = script.StartOFFClickON;
            if (previousOnCount != script.NumberOfThingsToTurnON)
            {
                script.StartOFFClickON = new GameObject[script.NumberOfThingsToTurnON];
            }
            for (int i = 0; i < script.NumberOfThingsToTurnON; i++)
            {
                if (tempOns != null)
                {
                    if (tempOns.Length > i)
                    {
                        script.StartOFFClickON[i] = tempOns[i];
                    }
                }
                script.StartOFFClickON[i] = (GameObject)EditorGUILayout.ObjectField("Start OFF Click ON", script.StartOFFClickON[i], typeof(GameObject), true);
            }

            script.NumberOfThingsToTurnOFF = EditorGUILayout.IntField("Number of Things to Turn OFF", script.NumberOfThingsToTurnOFF);
            GameObject[] tempOffs = script.StartONClickOFF;
            if (previousOffCount != script.NumberOfThingsToTurnOFF)
            {
                script.StartONClickOFF = new GameObject[script.NumberOfThingsToTurnOFF];
            }
            for (int i = 0; i < script.NumberOfThingsToTurnOFF; i++)
            {
                if (tempOffs != null)
                {
                    if (tempOffs.Length > i)
                    {
                        script.StartONClickOFF[i] = tempOffs[i];
                    }
                }

                script.StartONClickOFF[i] = (GameObject)EditorGUILayout.ObjectField("Start ON Click OFF", script.StartONClickOFF[i], typeof(GameObject), true);
            }
            previousOnCount = script.NumberOfThingsToTurnON;
            previousOffCount = script.NumberOfThingsToTurnOFF;
        }
        else if (script.myType == XRF_UPenn_InteractionController.InteractionType.AnimationController)
        {
            script.ObjectWithAnimation = (GameObject)EditorGUILayout.ObjectField("Object with Animation", script.ObjectWithAnimation, typeof(GameObject), true);
        }
        else if (script.myType == XRF_UPenn_InteractionController.InteractionType.SceneChangeController)
        {
            script.SceneToLoad = EditorGUILayout.TextField("Scene to Load", script.SceneToLoad);
        }
        
        else if (script.myType == XRF_UPenn_InteractionController.InteractionType.TeleportController)
        {

        }
        else if (script.myType == XRF_UPenn_InteractionController.InteractionType.GrabAndReturn)
        {

        }
        else if (script.myType == XRF_UPenn_InteractionController.InteractionType.GrabAndStay)
        {

        }
        else if (script.myType == XRF_UPenn_InteractionController.InteractionType.AudioPlayer)
        {
            script.theAudioSource = (AudioSource)EditorGUILayout.ObjectField("Audio Source in Scene", script.theAudioSource, typeof(AudioSource), true);
            script.theAudioClip = (AudioClip)EditorGUILayout.ObjectField("Audio Clip in Project Folder", script.theAudioClip, typeof(AudioClip), true);


        }
        else if (script.myType == XRF_UPenn_InteractionController.InteractionType.MoveFromTo)
        {
            script.thingToMove = (GameObject)EditorGUILayout.ObjectField("Object to Move to a new location on click", script.thingToMove, typeof(GameObject), true);
            script.transformPositionToMoveTo = (Transform)EditorGUILayout.ObjectField("Location to move to", script.transformPositionToMoveTo, typeof(Transform), true);


        }
        else if (script.myType == XRF_UPenn_InteractionController.InteractionType.CallEvent)
        {
            this.serializedObject.Update();
            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("EventToCall"), true);
            this.serializedObject.ApplyModifiedProperties();


        }
        else if (script.myType == XRF_UPenn_InteractionController.InteractionType.MakeObjectChild)
        {
            script.thingToChange = (GameObject)EditorGUILayout.ObjectField("child object", script.thingToChange, typeof(GameObject), true);
            script.newParentObject = (GameObject)EditorGUILayout.ObjectField("new parent object", script.newParentObject, typeof(GameObject), true);

        }
    }
}