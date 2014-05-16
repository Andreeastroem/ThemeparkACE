using UnityEngine;
using System.Collections;

public class rotationshit : MonoBehaviour {

    private int currentrotation;
    private bool direction = true;
    public Transform ContainerParent;

	// Use this for initialization
	void Start () {
	    //ContainerParent = transform.parent.Find("Container");
	}
	
	// Update is called once per frame
	void Update () {
        

        if(direction)
        {
            transform.Rotate(ContainerParent.transform.right, 5);
            currentrotation += 5;
            if(currentrotation > 20)
            {
                direction = false;
            }
        }
        else
        {
            transform.Rotate(ContainerParent.transform.right, -5);
            currentrotation -= 5;
            if(currentrotation < -20)
            {
                direction = true;
            }
        }
        
	}
}
