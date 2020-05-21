using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueBoxController : MonoBehaviour
{
    [TextArea(1,5)]
    [SerializeField] string[] DialogueTexts;
    int i =0 ;
    



    private void OnEnable()
    {
        StartCoroutine(textKeyboard(DialogueTexts[i]));
        
    }
    IEnumerator textKeyboard(string text)
    {
        FindObjectOfType<MainMenuControllerScript>().GetComponent<AudioSource>().Play();
        GetComponent<Text>().text = "";
        for (int i = 0; i < text.Length; i++)
        {
            GetComponent<Text>().text = GetComponent<Text>().text + text[i];
            yield return new WaitForSeconds(0.04f);
        }
        i++;
        FindObjectOfType<MainMenuControllerScript>().GetComponent<AudioSource>().Stop();
        if (i == DialogueTexts.Length)
        {
            yield return new WaitForSeconds(0.5f);
            UnityEngine.SceneManagement.SceneManager.LoadScene("City1");
        }
        StopCoroutine("textKeyboard");
        
    }
    
}
