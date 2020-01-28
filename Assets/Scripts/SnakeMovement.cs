using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public List<Transform> tailPieces;
    public List<Vector3> moveHistory;

    public GameObject fruitPrefab;
    Vector2 currentDirection;
    BoxCollider2D boxCol;

    //timer used to move ever x seconds
    float timer;
    public float moveInterval; //as this decreases, the game goes faster.

    int turn;
    void Awake(){
        boxCol = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        moveHistory = new List<Vector3>();
        turn = 0;
        timer = 0;
        currentDirection = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {      
        CheckInput();
        timer = timer+Time.deltaTime;
        if(timer >= moveInterval){
            Move();
            timer = 0;
        }
    }
    void Move(){
        moveHistory.Add(transform.position);
        transform.position = transform.position+new Vector3(currentDirection.x,currentDirection.y,0);
        turn++;
        if(turn%10 == 0){//if the remainder turn divided by 10 is zero. IE if turn is a multiple of 10.
            moveInterval = moveInterval - moveInterval*0.1f;//10% faster!
        }
        if(CheckOverlap() != null){
            Transform overlapping = CheckOverlap();
            if(overlapping.tag == "Tail"){
                Debug.Log("You Lose");
                Death();
            }else if(overlapping.tag == "Fruit"){
                Debug.Log("New Tail Piece");
                overlapping.tag = "Tail";
                tailPieces.Add(overlapping);

                DropNewFruit();
            }
        }
        //Move The Tail Pieces
        for(int i = 0;i<tailPieces.Count;i++)
        {
            tailPieces[i].position = moveHistory[moveHistory.Count - 1 - i]; 
        }
    }
    void CheckInput(){
        if(Input.GetKeyDown(KeyCode.DownArrow) && currentDirection != Vector2.up){
            currentDirection = Vector2.down;
        }else if(Input.GetKeyDown(KeyCode.UpArrow) && currentDirection != Vector2.down){
            currentDirection = Vector2.up;
        }else if(Input.GetKeyDown(KeyCode.LeftArrow) && currentDirection != Vector2.right){
            currentDirection = Vector2.left;
        }else if(Input.GetKeyDown(KeyCode.RightArrow) && currentDirection != Vector2.left){
            currentDirection = Vector2.right;
        }
    }
    //Returns the transform of what we are overlapping.
    Transform CheckOverlap(){
        ContactFilter2D filter2D = new ContactFilter2D();

        Collider2D[] results = new Collider2D[1];
        boxCol.OverlapCollider(filter2D,results);
        if(results[0] != null){
            return results[0].transform;
        }else{
            return null;
        }
    }
    //Creates a new fruit. Does not move it into position.
    void DropNewFruit(){
        GameObject.Instantiate(fruitPrefab,Vector3.zero,Quaternion.identity);
    }
    void Death(){
        moveInterval = Mathf.Infinity;
    }
}
