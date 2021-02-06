using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;

    private SpawnManager spawnManager;
    private GameManager gameManager;

    private Touch touch;
    private float startPosTouchX;
    private float positionPlayerY = -3.8f;


    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        gameManager = GameObject.Find("GameManger").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && GameManager.IsGame)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began)
                    startPosTouchX = touch.position.x;

                if (touch.phase == TouchPhase.Ended)
                    if (startPosTouchX == touch.position.x)
                    {
                        if (gameManager.ScoreShell > 0)
                        {
                            spawnManager.SpawnTract(player.transform.position);
                            gameManager.ScoreShell--;
                        }
                        return;
                    }

                if (touch.phase == TouchPhase.Moved && i == 0)
                {              
                       Vector2 positionTouch = Camera.main.ScreenToWorldPoint(touch.position);
                       player.transform.position = new Vector2(positionTouch.x, positionPlayerY);
                }
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        gameManager.RemoveLife(1);
    }

}
