using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Custom_TextOverTime : MonoBehaviour
{
    public string theMessage;
    private int theCurrentCharacter;
    public float timeInterval = 0.1f;
    public TMP_Text theTextBox;

    // Start is called before the first frame update
    void Start()
    {
        theTextBox.text = "";
        StartCoroutine(oneCharAtATime());
    }

    public IEnumerator oneCharAtATime()
    {
        yield return new WaitForSeconds(timeInterval);
        theCurrentCharacter++;
        Debug.Log("Hello! " + theCurrentCharacter);
        //take the message, and grab the first "theCurrentCharacter" chacters

        string theString = theMessage.Substring(0, theCurrentCharacter);

        theTextBox.text = theString;

        StartCoroutine(oneCharAtATime());
    }

}
