using UnityEngine;
using System.Collections;

public class LeftArm : MonoBehaviour {

    Player PlayerScript = null;
    private Vector3 TempPosition = new Vector3();

	// Use this for initialization
	void Start () {
        PlayerScript = transform.parent.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        //early exit
        if (PlayerScript == null)
        {
            return;
        }

        if (PlayerScript.DirX)
        {
            TempPosition.Set(transform.parent.position.x, 1 - PlayerScript.LeftHand.y, transform.parent.position.z - PlayerScript.LeftHand.x);
            transform.position = TempPosition;
        }
        else
        {
            TempPosition.Set(transform.parent.position.x - PlayerScript.LeftHand.x, 1 - PlayerScript.LeftHand.y, transform.parent.position.z);
            transform.position = TempPosition;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 18)
        {
            Debug.Log("Har vi en trigger event? LEFTARM");
            if(PlayerScript.UsingKinect)
            {
                PlayerScript.SetRotation(true);
            }
        }
    }
}
