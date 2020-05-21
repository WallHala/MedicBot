using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MainMenuControllerScript : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] GameObject IntroPanel;
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject CreditsPanel;
    [SerializeField] AudioClip KeyboardTapping;
    #endregion

    #region Public Methods
    public void PlayButtonFunction()
    {
        if (PlayerPrefs.GetInt("Intro") != 1)
        {
            GetComponent<PlayableDirector>().enabled = true;
            IntroPanel.SetActive(true);
            PlayerPrefs.SetInt("Intro", 1);
            GetComponent<AudioSource>().clip = KeyboardTapping;
            
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("City1");
        }
        
        
        
        
    }

    public void OptionsButtonFunction()
    {
        OptionsPanel.SetActive(!OptionsPanel.activeSelf);
    }

    public void CreditsPanelFunction()
    {
        CreditsPanel.SetActive(!CreditsPanel.activeSelf);
    }

    public void QuitButtonFunction()
    {
        Application.Quit();
    }

    

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
    #endregion
}
