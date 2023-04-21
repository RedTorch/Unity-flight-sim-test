using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen_Buttons : MonoBehaviour
{
    public void Quit() {
        Application.Quit();
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
