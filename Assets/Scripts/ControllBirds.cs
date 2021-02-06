using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllBirds : MonoBehaviour
{
    public GameObject litter;

    public static float speed = 4.5f;
    public static float MinTimeSpawnLit { set; get; } = 0.5f;
    public static float MaxTimeSpawnLit { set; get; } = 1f;

    public short directionWayX;

    private static float directionWayY = 1f;
    private static float rangeMoveY = 0.4f;

    private float positionBird = 1.3f;
    private float startPositionY;
    private float endPos;

    private void Start()
    {
        startPositionY = transform.position.y;
        endPos = -transform.position.x;

        StartCoroutine(SpawnLitter());
    }

    void Update()
    {
        transform.Translate(new Vector2(directionWayX, 0) * speed * Time.deltaTime,
                                                                          Space.World);

        transform.Translate(new Vector2(0, directionWayY)  * Time.deltaTime,
                                                                          Space.World);
        if (transform.position.y > startPositionY + rangeMoveY)
            directionWayY = -directionWayY;

        else if(transform.position.y < startPositionY - rangeMoveY)
            directionWayY = -directionWayY;


        if (transform.position.x < endPos && name == "RightBird(Clone)")
            Destroy(gameObject);

        else if (transform.position.x > endPos && name == "LeftBird(Clone)")
            Destroy(gameObject);
    }
     IEnumerator SpawnLitter() 
     {
        while (true) 
        {
            if(GameManager.IsGame)
            Instantiate(litter, new Vector2(transform.position.x,transform.position.y - positionBird)
                                            , Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(MinTimeSpawnLit, MaxTimeSpawnLit));
        }     
     }

    static  public void UpdateTimeSpawn(float complexity) 
    {
        if (complexity >= GameManager.maxComplexity)
        {
            MaxTimeSpawnLit = 0.7f;
        }
        else if(complexity == GameManager.startComplicity) 
        {
            MaxTimeSpawnLit = 1f;
        }
    }
}
