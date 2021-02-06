using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTract : MonoBehaviour
{
    private float speed = 15;
    private static float maxPosFly = 6f;

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime,Space.World);

        if(transform.position.y > maxPosFly) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBird"))
        {
            GameManager.StartKilledBirdEffect(collision.gameObject.transform.position);
            Destroy(collision.gameObject);
            Destroy(gameObject);
            GameManager.CountKilledBirds++;
        }
    }

}
