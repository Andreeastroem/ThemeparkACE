using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour
{

    public GameObject Cloud;    //What object
    public GameObject Cloud2;
    public GameObject Cloud3;
    public GameObject Cloud4;
    private GameObject[] go = new GameObject[4];      //New object

    public float Delta;
    private Vector3 NewPosition = new Vector3();
    private Direction dir;
    
    private enum Direction
    {
        POSX,
        NEGX,
        POSZ,
        NEGZ
    }

    float Velocity = 0;
    // Use this for initialization
    void Start()
    {
        if(transform.forward.x > 0)
        {
            dir = Direction.POSX;

            NewPosition.Set(1, 0f, 0f);
        }
        else if(transform.forward.x < 0)
        {
            dir = Direction.NEGX;

            NewPosition.Set(-1, 0f, 0f);
        }
        else if(transform.forward.z > 0)
        {
            dir = Direction.POSZ;

            NewPosition.Set(0f, 0f, 1f);
        }
        else if (transform.forward.z > 0)
        {
            dir = Direction.NEGZ;

            NewPosition.Set(0f, 0f, -1f);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider Other)
    {
        Velocity = Other.transform.parent.gameObject.rigidbody.velocity.magnitude;

        if (Other.gameObject.layer.Equals(9))
        {
            go[0] = Instantiate(Cloud, transform.position, this.transform.rotation) as GameObject;

            if (Velocity > 20 && Velocity < 30)
            {

                go[1] = Instantiate(Cloud2, (NewPosition * Delta) + transform.position, this.transform.rotation) as GameObject;
            }
            if (Velocity > 30 && Velocity < 40)
            {
                go[1] = Instantiate(Cloud2, (NewPosition * Delta) + transform.position, this.transform.rotation) as GameObject;
                go[2] = Instantiate(Cloud3, (NewPosition * Delta * 2) + transform.position, this.transform.rotation) as GameObject;
            }
            if (Velocity > 40)
            {
                go[1] = Instantiate(Cloud2, (NewPosition * Delta) + transform.position, this.transform.rotation) as GameObject;
                go[2] = Instantiate(Cloud3, (NewPosition * Delta * 2) + transform.position, this.transform.rotation) as GameObject;
                go[3] = Instantiate(Cloud4, (NewPosition * Delta * 3) + transform.position, this.transform.rotation) as GameObject;
            }

            Destroy(this.gameObject);
        }
    }
}