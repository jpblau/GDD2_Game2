using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// UIManager is persistant throughout all scenes in the game
/// Holds Canvas + button references
/// Enables/Disables UI as needed
/// </summary>
public class UIManager : MonoBehaviour
{
    public Canvas winCanvas;
    private CameraFollow cameraScript;

    public List<AudioClip> buttonPressSounds;   //A list of all our button press sounds
    private AudioSource UIAudioSource; // The UI's audio source

    // Start is called before the first frame update
    void Start()
    {
        winCanvas.gameObject.SetActive(false);
        cameraScript = Camera.main.GetComponent<CameraFollow>();
        UIAudioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShakeCamera(float dur, float mag)
    {
        cameraScript.StartCoroutine(cameraScript.Shake(dur, mag));
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void Sound_RockButtonPressed()
    {
        UIAudioSource.clip = buttonPressSounds[0];
        UIAudioSource.Play();
    }

    public void Sound_SlimeButtonPressed()
    {
        UIAudioSource.clip = buttonPressSounds[1];
        UIAudioSource.Play();
    }

    public void Sound_BalloonButtonPressed()
    {
        UIAudioSource.clip = buttonPressSounds[2];
        UIAudioSource.Play();
    }
}
