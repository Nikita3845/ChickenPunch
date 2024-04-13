using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeScene : MonoBehaviour
{
    public void transition_resume()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
