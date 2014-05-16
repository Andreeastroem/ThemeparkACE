using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    //Rotate effect
    public enum RotationDirection
    {
        LEFT,
        RIGHT,
        ROTATIONDIRECTIONSIZE
    }
    public RotationDirection Direction;
    private Vector3 rotationdirection = new Vector3();
    private float minangle;
    private float maxangle;
    private float currentangle = 0f;
    public float RotationDegree;
    public float RotationPerSec;

	// Use this for initialization
	void Start () {
        //Rotation
        minangle = -RotationDegree;
        maxangle = RotationDegree;

        if (Mathf.Abs(Vector3.right.x) > 0)
        {
            rotationdirection.Set(0, 0, 1);
        }
        else
        {
            rotationdirection.Set(1, 0, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        //Rotation
        if (Direction == RotationDirection.RIGHT)
        {
            transform.Rotate(rotationdirection, Time.deltaTime * RotationPerSec);
            currentangle += Time.deltaTime * RotationPerSec;
            if (currentangle > maxangle)
                Direction = RotationDirection.LEFT;
        }
        else if (Direction == RotationDirection.LEFT)
        {
            transform.Rotate(rotationdirection, Time.deltaTime * -RotationPerSec);
            currentangle -= Time.deltaTime * RotationPerSec;
            if (currentangle < minangle)
                Direction = RotationDirection.RIGHT;
        }
	}
}
