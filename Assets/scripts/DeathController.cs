using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour
{
    [SerializeField] private GameObject _deathPanel;
    private Healths _health;

    private void Start()
    {
        _deathPanel.SetActive(false);   
        _health = FindObjectOfType<Healths>();
        _health.DeathPlayer.AddListener(ShowScreen);
    }

    private void ShowScreen()
    {
        _deathPanel.SetActive(true);
    }

    public void ReStart()
    {
        SceneManager.LoadScene(1);
    }
}
