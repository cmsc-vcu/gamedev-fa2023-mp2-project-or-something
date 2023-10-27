using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    // Start is called before the first frame update
    public void Begin(InputAction.CallbackContext context)
    {
        string curr_scene = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(curr_scene);
        SceneManager.LoadScene("Game");
    }

    public void OnClick()
    {
        string curr_scene = SceneManager.GetActiveScene().name;
        SceneManager.UnloadSceneAsync(curr_scene);
        SceneManager.LoadScene("Game");
    }
}
