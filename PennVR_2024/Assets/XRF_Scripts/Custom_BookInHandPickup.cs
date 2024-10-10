using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom_BookInHandPickup : MonoBehaviour
{
    public GameObject[] objectsToPickUp;
    public GameObject[] objectsInBook;

    public Material offMaterial;
    public Material onMaterial;

    public GameObject youWinText;
    private bool gameComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        youWinText.SetActive(false);
        for(int i = 0; i < objectsInBook.Length;i++)
        {
            objectsInBook[i].GetComponent<MeshRenderer>().material = offMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameComplete)
        {
            int pickupTotal = objectsToPickUp.Length;
            int pickupCounter = 0;
            for (int i = 0; i < objectsToPickUp.Length; i++)
            {
                if (objectsToPickUp[i].activeSelf == true)
                {
                    //if active in the scene, make the book object offMaterial
                    objectsInBook[i].GetComponent<MeshRenderer>().material = offMaterial;
                }
                else
                {
                    pickupCounter++;
                    objectsInBook[i].GetComponent<MeshRenderer>().material = onMaterial;
                }
            }
            if(pickupCounter == pickupTotal)
            {
                gameComplete = true;
            }
        }
        else
        {
            youWinText.SetActive(true);
        }

    }
}
