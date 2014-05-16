using UnityEngine;
using System.Collections;

public class WalkingEffect : MonoBehaviour {

    public enum Direction
    {
        LEFT,
        RIGHT
    }

    //User input
    public float WalkLength;
    public float WalkLeftTime;
    public float WalkRightTime;
    public Direction direction;
    public bool AllowLeftWalk;
    public bool AllowRightWalk;

    //Selfmaintained variables
    private Vector3 TargetLeft;
    private Vector3 TargetRight;
    private float LeftSpeed;
    private float RightSpeed;
    private Vector3 TempPosition = new Vector3();
    

	// Use this for initialization
	void Start () 
    {
        TargetLeft.Set(transform.position.x - transform.right.x * (WalkLength / 2), 
            transform.position.y - transform.right.y * (WalkLength / 2), 
            transform.position.z - transform.right.z * (WalkLength / 2));

        TargetRight.Set(transform.position.x + transform.right.x * (WalkLength / 2),
            transform.position.y + transform.right.y * (WalkLength / 2),
            transform.position.z + transform.right.z * (WalkLength / 2));

        LeftSpeed = Vector3.Distance(transform.position, TargetLeft) / WalkLeftTime;
        RightSpeed = Vector3.Distance(transform.position, TargetRight) / WalkRightTime;
	}
	
	// Update is called once per frame
	void Update () {
        
        if(direction == Direction.LEFT)
        {
            if(AllowLeftWalk)
            {
                TempPosition.Set(transform.position.x - (transform.right.x * Time.deltaTime * LeftSpeed),
                    transform.position.y - (transform.right.y * Time.deltaTime * LeftSpeed),
                    transform.position.z - (transform.right.z * Time.deltaTime * LeftSpeed));

                transform.position = TempPosition;

                if (Vector3.Distance(transform.position, TargetLeft) < 0.1)
                {
                    direction = Direction.RIGHT;
                }
            }
        }
        else if (direction == Direction.RIGHT)
        {
            if(AllowRightWalk)
            {
                TempPosition.Set(transform.position.x + (transform.right.x * Time.deltaTime * RightSpeed),
                    transform.position.y + (transform.right.y * Time.deltaTime * RightSpeed),
                    transform.position.z + (transform.right.z * Time.deltaTime * RightSpeed));

                transform.position = TempPosition;

                if (Vector3.Distance(transform.position, TargetRight) < 0.1)
                {
                    direction = Direction.LEFT;
                }
            }
        }
	}
}