using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// GameManager is unique to each scene, and only appears in levels
/// It checks for win/loss states, makes sure the player does not get stuck, 
/// and holds references to tags for different environment obstacles.
/// This GameManager should be positioned at the player start position for each level
/// </summary>
public class GameManager : MonoBehaviour
{
    public GameObject player;   // A reference to the player in this level

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))        //Allows player to exit build
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Handles any level functionality for restarting the level.
    /// Also resets the player's position
    /// </summary>
    public void RestartLevel()
    {
        player.transform.position = this.transform.position;
    }

    public void SwitchLevel(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);

    }


}
