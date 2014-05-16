using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour
{

    public Transform Player;
    public float Size = 15.0f;
    public float Smooth = 4.0f;
    public float Margin = 2.0f;
    public GameObject PlayerObject;

    // Use this for initialization
    void Start()
    {
        GetComponent<Camera>().orthographicSize = Size;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null) return;
        if (PlayerObject == null) return;

        /*
        float Hyp;
        float dX, dZ;

        dX = Mathf.Abs(Player.position.x - transform.position.x);
        dZ = Mathf.Abs(Player.position.z - transform.position.z);
        Hyp = Mathf.Sqrt(dX*dX + dZ*dZ);
         */
        
        transform.rotation = Player.rotation;
        

        float TargetZ, TargetX;

        if(transform.rotation.y < 90 && transform.rotation.y >= 0)
        {
            TargetX = Player.position.x;

            TargetZ = Mathf.Lerp(transform.position.z, Player.position.z - Margin, Smooth * Time.deltaTime);

            transform.position = new Vector3(TargetX, transform.position.y, TargetZ);
        }
        else if(transform.rotation.y <= 180 && transform.rotation.y > 90)
        {
            TargetZ = Player.position.z;
            TargetX = Mathf.Lerp(transform.position.x, Player.position.x - Margin, Smooth * Time.deltaTime);

            transform.position = new Vector3(TargetX, transform.position.y, TargetZ);
        }
        else if(transform.rotation.y <= 270 && transform.rotation.y > 180)
        {
            TargetX = Player.position.x;

            TargetZ = Mathf.Lerp(transform.position.z, Player.position.z + Margin, Smooth * Time.deltaTime);

            transform.position = new Vector3(TargetX, transform.position.y, TargetZ);
        }
        else if(transform.rotation.y <= 360 && transform.rotation.y > 270)
        {
            TargetZ = Player.position.z;

            TargetX = Mathf.Lerp(transform.position.x, Player.position.x + Margin, Smooth * Time.deltaTime);

            transform.position = new Vector3(TargetX, transform.position.y, TargetZ);
        }

        /*
        if(Mathf.Abs(PlayerObject.rigidbody.velocity.x) > 0)
        {
            bool X = (Mathf.Abs(transform.position.x - Player.position.x) > Margin);

            float TargetX = transform.position.x;

            if (X)
                TargetX = Mathf.Lerp(transform.position.x, Player.position.x, Smooth * Time.deltaTime);

            transform.position = new Vector3(TargetX, transform.position.y, transform.position.z);
        }
        else if (Mathf.Abs(PlayerObject.rigidbody.velocity.y) > 0)
        {
            bool Y = (Mathf.Abs(transform.position.y - Player.position.y) > Margin);

            float TargetY = transform.position.y;

            if (Y)
                TargetY = Mathf.Lerp(transform.position.y, Player.position.y, Smooth * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, TargetY, transform.position.y);
        }
        else if (Mathf.Abs(PlayerObject.rigidbody.velocity.z) > 0)
        {
            bool Z = (Mathf.Abs(transform.position.z - Player.position.z) > Margin);

            float TargetZ = transform.position.z;

            if (Z)
                TargetZ = Mathf.Lerp(transform.position.z, Player.position.z, Smooth * Time.deltaTime);

            transform.position = new Vector3(transform.position.x, transform.position.y, TargetZ);
        }
         */
    }
}
