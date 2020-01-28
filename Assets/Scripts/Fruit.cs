using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private BoxCollider2D boxCol;
    public Vector2Int levelSize;
    void Awake()
    {//Getting references in Awake is a good practice.
        boxCol = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        if(levelSize == Vector2Int.zero){
            Debug.LogError("Level size is not set. This will cause an infinite loop and crash the game.");//it will also crash the unity inspector.
        }
        MoveToRandomSpot();
    }
    void MoveToRandomSpot()
    {
        transform.position = GetRandomSpot();
        ContactFilter2D filter2D = new ContactFilter2D();//We could use this to say "only objects on this layer" or whatever with this.
        Collider2D[] results = new Collider2D[2];//While we aren't using the results array, we still need it for the function to run.
        if(boxCol.OverlapCollider(filter2D,results) > 0){//Collider2D.OverlapCollider returns an integer with the number of objects it is overlapping.
                //Debug.Log("in occupied spot, "+transform.position);
                MoveToRandomSpot();//This function calls itself.
                //a function is allowed to call itself, its just a danger of creating an infinite loop.
                //This is called a "recursive" function.
                //Basically, I keep picking random spots until one doesnt overlap.
                //If the snake survives to the maximum size, then the game will crash, as I have no backup exit condition for this infinite loop.
                //Good luck with that tho.
                //Since its just picking a randomly over and over again, the game might stutter as it tries to find a spot to move to.
                //it could end up picking the same spot over and over again, too. 
        }
    }
    Vector3 GetRandomSpot()
    {
        int randomX = Random.Range(-(int)levelSize.x/2,(int)levelSize.x/2);
        int randomY = Random.Range(-(int)levelSize.y/2,(int)levelSize.y/2);
        return new Vector3((int)randomX,(int)randomY,0);
    }
}
