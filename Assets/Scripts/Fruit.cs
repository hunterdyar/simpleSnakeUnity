using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private BoxCollider2D boxCol;
    public Vector2Int levelSize;
    void Awake(){
                boxCol = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        if(levelSize == Vector2Int.zero){
            Debug.LogError("Will cause infinite loop");
        }
        MoveToRandomSpot();
    }
    void MoveToRandomSpot(){
        transform.position = GetRandomSpot();
        ContactFilter2D filter2D = new ContactFilter2D();
        Collider2D[] results = new Collider2D[5];
        if(boxCol.OverlapCollider(filter2D,results) > 0){
                Debug.Log("in occupied spot, "+transform.position);
                MoveToRandomSpot();
        }
    }
    Vector3 GetRandomSpot(){
        int randomX = Random.Range(-(int)levelSize.x/2,(int)levelSize.x/2);
        int randomY = Random.Range(-(int)levelSize.y/2,(int)levelSize.y/2);
        return new Vector3((int)randomX,(int)randomY,0);
    }
}
