using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLevel1 : MonoBehaviour
{
    public void transition()
    {
        SceneManager.LoadScene(1);
    }
}
