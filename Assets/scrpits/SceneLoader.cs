using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene1()
    {
        SceneManager.LoadScene("level 1");
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene("level 2");
    }

    public void LoadScene3()
    {
        SceneManager.LoadScene("level 3");
    }

    public void LoadScene4()
    {
        SceneManager.LoadScene("tittle");
    }
}
