using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class litterController : MonoBehaviour
{
    private static float maxPosFall = -7f;
    private void Update()
    {
        if (transform.position.y < maxPosFall)
            Destroy(gameObject);
    }
}
