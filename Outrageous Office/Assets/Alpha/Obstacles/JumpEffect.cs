using UnityEngine;
using System.Collections;

public class JumpEffect : MonoBehaviour {

    //Jumping effect
    public float JumpHeight;
    public float FallTime;
    public float JumpTime;
    private float OriginalHeight;
    private float TargetHeight;
    private bool Falling = false;
    private Vector3 TemporaryPosition;

	// Use this for initialization
	void Start () {
        //Jumping
        OriginalHeight = transform.position.y;
        TargetHeight = OriginalHeight + JumpHeight;
        TemporaryPosition = transform.parent.position;
	}
	
	// Update is called once per frame
	void Update () {
        //Jumping
        if (Falling)
        {
            TemporaryPosition.Set(transform.parent.position.x, Mathf.Lerp(transform.position.y, OriginalHeight, Time.deltaTime / FallTime), transform.parent.position.z);
            transform.position = TemporaryPosition;

            if (Mathf.Abs(transform.position.y - OriginalHeight) < 0.1f)
            {
                Falling = false;
            }
        }
        else
        {
            TemporaryPosition.Set(transform.parent.position.x, Mathf.Lerp(transform.position.y, TargetHeight, Time.deltaTime / JumpTime), transform.parent.position.z);
            transform.position = TemporaryPosition;

            if (Mathf.Abs(transform.position.y - TargetHeight) < 0.1f)
            {
                Falling = true;
            }
        }
	}
}
