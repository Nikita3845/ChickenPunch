using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resume : MonoBehaviour
{
    public GameObject panel;
    public void Resume()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
}
