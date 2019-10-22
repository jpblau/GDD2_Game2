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

    // Start is called before the first frame update
    void Start()
    {
        winCanvas.gameObject.SetActive(false);
        cameraScript = Camera.main.GetComponent<CameraFollow>();
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
}
