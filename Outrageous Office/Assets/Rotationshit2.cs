using UnityEngine;
using System.Collections;

public class Rotationshit2 : MonoBehaviour {

    private int currentrotation;
    private bool direction = false;
    Vector3 RotationDirection;

	// Use this for initialization
	void Start () {
	
        if(Mathf.Abs(Vector3.right.x) > 0)
        {
            RotationDirection.Set(0, 0, 1);
        }
        else
        {
            RotationDirection.Set(1, 0, 0);
        }

	}
	
	// Update is called once per frame
	void Update () {
        if (direction)
        {
            transform.Rotate(RotationDirection, 5);
            currentrotation += 5;
            if (currentrotation > 20)
            {
                direction = false;
            }
        }
        else
        {
            transform.Rotate(RotationDirection, -5);
            currentrotation -= 5;
            if (currentrotation < -20)
            {
                direction = true;
            }
        }
	}
}
