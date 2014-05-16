using UnityEngine;
using System.Collections;

public class HUDTurns : MonoBehaviour {

    public float TurnWarningLength;

    public Animator LeftAnim;
    public Animator RightAnim;

    LayerMask TurnMask;

    bool Hit0 = false;

    RaycastHit HitInfo0;

	// Use this for initialization
	void Start () {
        TurnMask = 1 << 22 | 1 << 21 | 1 << 20;
	}
	
	// Update is called once per frame
	void Update () {

        if(RightAnim == null || LeftAnim == null)
        {
            return;
        }

        Hit0 = Physics.Raycast(transform.position, transform.forward, out HitInfo0, TurnWarningLength, TurnMask);

        if(Hit0)
        {
            switch(HitInfo0.collider.gameObject.layer)
            {
                case 20:
                    RightAnim.SetBool("Turn", true);
                    break;
                case 21:
                    LeftAnim.SetBool("Turn", true);
                    break;
                case 22:
                    LeftAnim.SetBool("Turn", true);
                    RightAnim.SetBool("Turn", true);
                    break;
            }
        }
        else
        {
            RightAnim.SetBool("Turn", false);
            LeftAnim.SetBool("Turn", false);
        }
	}
}
