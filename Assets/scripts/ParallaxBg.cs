using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBg : MonoBehaviour
{
    [SerializeField] Transform followTriger;
    [SerializeField, Range(0f, 1f)] float parallaxStrength = 0.1f;
    [SerializeField] bool disableParallaxY;
    Vector3 targetPosition;

    void Start()
    {
        if (!followTriger)
            followTriger = Camera.main.transform;

        targetPosition = followTriger.position;
    }

    // Update is called once per frame
    void Update()
    {
        var delta = followTriger.position - targetPosition;

        if(disableParallaxY)
            delta.y = 0f;

        targetPosition = followTriger.position;

        transform.position += delta * parallaxStrength;
    }
}
