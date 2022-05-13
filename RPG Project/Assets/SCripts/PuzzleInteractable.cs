using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInteractable : Interactable
{
    [SerializeField] GameObject wall;
    [SerializeField] private float speed;
    [SerializeField] private Transform targetPos;
    [SerializeField] bool moveWallFlag;
    [SerializeField] int time;
    // Start is called before the first frame update
    void Start()
    {
        moveWallFlag = false;
    }

    public override void Interact()
    {
        base.Interact();


        moveWallFlag = true;


    }

    private void FixedUpdate()
    {
        if (moveWallFlag == true) {
            time++;
            wall.transform.position = Vector3.MoveTowards(wall.transform.position, targetPos.position, speed);
            if (time > 90) {

                moveWallFlag = false;
            }
        }
    }
}
