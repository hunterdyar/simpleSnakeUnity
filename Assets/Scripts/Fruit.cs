using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public ContactFilter2D filter2D;
    private BoxCollider2D boxCol;
    public Vector2Int levelSize;
    // Start is called before the first frame update
    void Start()
    {
        if(levelSize == Vector2Int.zero){
            Debug.LogError("Will cause infinite loop");
        }
        boxCol = GetComponent<BoxCollider2D>();
        MoveToRandomSpot();
    }
    void MoveToRandomSpot(){
        transform.position = GetRandomSpot();
        Collider2D[] results = new Collider2D[1];
        boxCol.OverlapCollider(filter2D,results);
        if(results[0] != null){
            Debug.Log("picked occupied spot, "+transform.position);
            //MoveToRandomSpot();
        }
    }
    Vector3 GetRandomSpot(){
        int randomX = Random.Range(-(int)levelSize.x/2,(int)levelSize.x/2);
        int randomY = Random.Range(-(int)levelSize.y/2,(int)levelSize.y/2);
        return new Vector3((int)randomX,(int)randomY,0);
    }
}
