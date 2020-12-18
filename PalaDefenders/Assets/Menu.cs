using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public int buttonWidth;
    public int buttonHeight;
    private int origin_x;
    private int origin_y;
    private float x;
    private float y;

    // Start is called before the first frame update
    void Start()
    {
        buttonWidth = 200;
        buttonHeight = 50;
        origin_x = Screen.width / 2 - buttonWidth / 2;
        origin_y = Screen.height / 2 - buttonHeight / 2;
    }

    private void Update()
    {
        x += Time.deltaTime * 10;
        y += Time.deltaTime * 10;
        transform.rotation = Quaternion.Euler(x, y, 0f);
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(origin_x, origin_y, buttonWidth, buttonHeight), "Scene 1"))
        {
            SceneManager.LoadScene(1);
        }
        if (GUI.Button(new Rect(origin_x, origin_y + buttonHeight +20, buttonWidth, buttonHeight), "Quit"))
        {
            UnityEditor.EditorApplication.isPlaying = false; 
            Application.Quit();
        }
    }

}
