using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float _offset;
    [SerializeField] private float _offsetSmoothing;
    private Vector3 playerPosition;

    void Start()
    {
        if (_player == null)
            _player = GameObject.FindWithTag("Player");
    }

    void LateUpdate()
    {
        if (_player == null)
            return;


        playerPosition = new Vector3(_player.transform.position.x, transform.position.y, transform.position.z);

        if (_player.transform.localScale.x > 0f)
        {
            playerPosition = new Vector3(playerPosition.x + _offset, playerPosition.y, playerPosition.z);
        }
        else
        {
            playerPosition = new Vector3(playerPosition.x - _offset, playerPosition.y, playerPosition.z);
        }

        transform.position = Vector3.Lerp(transform.position, playerPosition, _offsetSmoothing * Time.deltaTime);
    }
}
