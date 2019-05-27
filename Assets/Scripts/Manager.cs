using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Manager : MonoBehaviour
{
    public VideoPlayer vp;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void OnGUI()
    {
        int xCenter = (Screen.width / 2);
        int yCenter = (Screen.height / 2);
        int width = 100;
        int height = 60;

        GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("button"));
        fontSize.fontSize = 32;

        Scene scene = SceneManager.GetActiveScene();

        if (GUI.Button(new Rect(10 * 4 + width * 3, 10, width, height), "Quit", fontSize))
        {
            if (vp != null) vp.Stop();
            Application.Quit();
        }
    }
}
