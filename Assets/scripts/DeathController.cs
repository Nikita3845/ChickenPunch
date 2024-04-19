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
        Time.timeScale = 1;
    }

    private void ShowScreen()
    {
        Time.timeScale = 0;
        _deathPanel.SetActive(true);
    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
