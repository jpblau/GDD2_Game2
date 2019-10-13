using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIManager is persistant throughout all scenes in the game
/// Holds Canvas + button references
/// Enables/Disables UI as needed
/// </summary>
public class UIManager : MonoBehaviour
{
    public Canvas winCanvas;

    // Start is called before the first frame update
    void Start()
    {
        winCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
