using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationBehaviour : MonoBehaviour
{
    bool isNGame = false;
    private void Awake() {
        
    }
    // Start is called before the first frame update
    public void NavigateTo(string scene) {
        if (SceneManager.GetActiveScene().name == scene) {
            return;
        }
        SceneManager.LoadSceneAsync(scene);
    }
    public void ReturnHome() {
        if (PlayerPrefs.GetString("Tag") == "Narr") {//Blarp/Game
            SceneManager.LoadSceneAsync("Narrative_Home");
            return;
        }
        SceneManager.LoadSceneAsync("BLAP_Home");
    }
    public void ReturnToPrevious() {
        //Loade previous Scene if Feature is implemented
        Debug.Log("ReturnToPrevious : NYI");
    }
}
