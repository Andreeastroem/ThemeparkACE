using UnityEngine;
using System.Collections;

public enum PlayerDirection
{
    NORTH,
    SOUTH,
    EAST,
    WEST
};

public class Player : MonoBehaviour {

    //Temporary variables
    private Vector2 TempVector2 = new Vector2();
    private bool m_HasCollided = false;
    private float AchievedPoint = 0f;
    public int CollisionObject = -1;


    //Obstacles and Powerups
    public float CoffeePowerUp;
    public float BlueObstacleSlowdown;
    public float RedObstacleSlowdown;
    public int GreenPoints;
    public int BluePoints;
    public int RedPoints;

    private bool SHIELD;        //SHIELD
    public float ShieldCooldown;

    //Highscore points
    public int Highscore;

    //Win and lose
    public bool WinningStatus = false;
    public bool DoneWithSession;
    public bool StartMoving;
    public float SecondsUntilStart;

    //Animations
    public Animator anim;

    //

    public float MaxSpeed;

    public Vector3 direction;

    private bool Rotate;
    private bool RotateLeft;

    private bool CanRotate;

    //Movement
    public float Velocity = 0.0f;
    private float ForwardVelocity;
    private float SidewaysVelocity;
    private float JoystickForce;
    public float speed;
    public float TurnSpeed;
    public float Friction;
    private float VelocityBeforeTurning;
    protected bool m_HasControl = false;

    public float SideMovementScale;
    public bool DirX = false;

    public bool LEANING;

    //Walls
    bool hit0, hit1;
    RaycastHit hitinfo0, hitinfo1;
    private Vector3 mid;

    //Kinect
    public bool UsingKinect;
    public GameObject Skeletor;
    KinectPointController KPC;
    private float DeltaMovement = 0.0f;
    public float Scale = 1.0f;
    private Vector3 DeltaPos;

    public Vector2 LeftHand = new Vector2(0f, 0f), RightHand = new Vector2(0f, 0f);     //Hand
    public float HandMovementScale;                                                     //Movement
    private float HalfTheCollider;
    public float Threshold;
    private GameObject[] Arms = new GameObject[2];



    //Sideways
    private float PreviousKinectPosition;
    public float LeanThreshold;
    
    //Debug
    private Vector3 Debugvector = new Vector3();

	// Use this for initialization
	void Start () 
    {
        //DeltaPos = new Vector3(SW.bonePos[0, 11].x, 0.0f, SW.bonePos[0, 11].z);

        DontDestroyOnLoad(this.gameObject);

        KPC = Skeletor.GetComponent<KinectPointController>();

        GameObject go = GameObject.Find("MainCharacterCode");

        Threshold = go.GetComponent<BoxCollider>().bounds.size.x / 2;
        HalfTheCollider = go.GetComponent<BoxCollider>().bounds.size.x / 2;

        Rotate = false;
        RotateLeft = false;
        CanRotate = true;


        Arms[0] = transform.FindChild("RightArm").gameObject;
        Arms[1] = transform.FindChild("LeftArm").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_HasCollided = false;
        AchievedPoint = 0f;
        //Animations
        anim.SetFloat("Velocity", rigidbody.velocity.magnitude);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Application.loadedLevelName == "Score")
        {
            Destroy(this.gameObject);
        }

        if (DoneWithSession)
        {
            rigidbody.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            if (StartMoving)
            {
                if (Rotate)
                {
                    if (DirX)
                    {
                        DirX = false;
                    }
                    else
                    {
                        DirX = true;
                    }

                    VelocityBeforeTurning = rigidbody.velocity.magnitude;

                    if (RotateLeft)
                        transform.Rotate(new Vector3(0, -90, 0));
                    else
                        transform.Rotate(new Vector3(0, 90, 0));

                    //rigidbody.velocity.Set(rigidbody.transform.forward.x * TurnSpeed, rigidbody.transform.forward.y * TurnSpeed, rigidbody.transform.forward.z * TurnSpeed);
                    rigidbody.velocity = rigidbody.transform.forward * VelocityBeforeTurning;

                    Rotate = false;
                }
            }

            Movement();
            
        }
	}

    void Movement()
    {
        //Move player
        if(UsingKinect)
        {
            if(Skeletor != null)
            {
                DeltaMovement = -KPC.Delta;
            
                HandMovement();
                
                /** */
                if(LEANING)
                {
                    Leaning();
                }
                else
                {
                    Positioning();
                }

                
            }
        }
        //A-D keys and controller
        else
        {
            //Xbox 360 controller
            JoystickForce = Input.GetAxis("Horizontal");
            //Movement from left to right on the screen
            if(Mathf.Abs(JoystickForce) > 0)
            {
                rigidbody.AddForce(rigidbody.transform.right * TurnSpeed * JoystickForce);

                if(JoystickForce > 0.2)
                {
                    anim.SetBool("TurnRight", true);
                }
                if(JoystickForce < -0.2)
                {
                    anim.SetBool("TurnLeft", true);
                }
            }
            else
            {
                anim.SetBool("TurnRight", false);
                anim.SetBool("TurnLeft", false);
            }
        }

        if(!WinningStatus)
        {
            CheckLose();
        }
        

        //Increasing the movement forward
        if(rigidbody.velocity.magnitude < MaxSpeed && !WinningStatus)
        {
            if (UsingKinect)
            {
                Debug.Log(Skeletor.GetComponent<KinectPointController>().Ready.ToString());
                Debug.Log("CONTROL: " + m_HasControl.ToString());

                if (Skeletor.GetComponent<KinectPointController>().Ready && m_HasControl)
                {
                    rigidbody.AddForce(rigidbody.transform.forward * speed * Time.deltaTime);
                }
            }
            else
                rigidbody.AddForce(rigidbody.transform.forward * speed * Time.deltaTime);
        }
        else if(WinningStatus)
        {
            rigidbody.velocity /= 1.05f;
        }
    }

    void HandMovement()
    {
        LeftHand = KPC.LeftHand;
        LeftHand.Set(LeftHand.x * HandMovementScale, LeftHand.y * HandMovementScale);

        RightHand = KPC.RightHand;
        RightHand.Set(RightHand.x * HandMovementScale, RightHand.y * HandMovementScale);
    }

    void Leaning()
    {
        
        rigidbody.transform.position += new Vector3(
            rigidbody.transform.right.x * DeltaMovement,
            rigidbody.transform.right.y * DeltaMovement,
            rigidbody.transform.right.z * DeltaMovement);         //Leaning solution

        if(DeltaMovement > LeanThreshold)
        {
            anim.SetBool("TurnRight", true);
        }
        else if(DeltaMovement < -LeanThreshold)
        {
            anim.SetBool("TurnLeft", true);
        }
        else
        {
            anim.SetBool("TurnLeft", false);
            anim.SetBool("TurnRight", false);
        }

        //Händer

        ReachingOutLeftHand();
        ReachingOutRightHand();
             
    }

    void Positioning()
    {
        DeltaMovement *= SideMovementScale;

        if (m_HasControl)
        {
            if (!DirX)
            {
                rigidbody.transform.position = new Vector3(mid.x + DeltaMovement * rigidbody.transform.right.x, rigidbody.transform.position.y, rigidbody.transform.position.z);
            }
            else if (DirX)
            {
                rigidbody.transform.position = new Vector3(rigidbody.transform.position.x, rigidbody.transform.position.y, mid.z + DeltaMovement * rigidbody.transform.right.z);
            }

            
            if ((PreviousKinectPosition - DeltaMovement) > LeanThreshold)
            {
                anim.SetBool("TurnLeft", true);
            }
            else if ((PreviousKinectPosition - DeltaMovement) < -LeanThreshold)
            {
                anim.SetBool("TurnRight", true);
            }
            else
            {
                anim.SetBool("TurnLeft", false);
                anim.SetBool("TurnRight", false);
            }

            //Händer

            ReachingOutLeftHand();
            ReachingOutRightHand();

            PreviousKinectPosition = DeltaMovement;
        }
    }

    void ReachingOutLeftHand()
    {
        if(DirX)
        {

            Debug.Log("LEFTHAND: " + (LeftHand.x));
            if ((Vector3.Distance(transform.position, Arms[1].transform.position)) > Threshold)
            {
                anim.SetBool("LeftArm", true);
            }
            else
            {
                anim.SetBool("LeftArm", false);
            }
        }
        else
        {
            Debug.Log("LEFTHAND: " + (LeftHand.x - transform.position.z));
            if ((Vector3.Distance(transform.position, Arms[1].transform.position)) > Threshold)
            {
                anim.SetBool("LeftArm", true);
            }
            else
            {
                anim.SetBool("LeftArm", false);
            }
        }
        
    }

    void ReachingOutRightHand()
    {
        if(DirX)
        {
            if ((Vector3.Distance(transform.position, Arms[0].transform.position)) > Threshold)
            {
                anim.SetBool("RightArm", true);
            }
            else
            {
                anim.SetBool("RightArm", false);
            }
        }
        else
        {
            if ((Vector3.Distance(transform.position, Arms[0].transform.position)) > Threshold)
            {
                anim.SetBool("RightArm", true);
            }
            else
            {
                anim.SetBool("RightArm", false);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {

    }
    void OnCollisionStay(Collision other)
    {
    }

    void OnTriggerEnter(Collider Other)
    {
        if(!UsingKinect)
        {
            if(CanRotate)
            {
                if (Other.tag.Equals("LeftTurn"))
                {
                    Rotate = true;
                    RotateLeft = true;
                    CanRotate = false;

                    StartCoroutine("RotateDelay");
                }
                else if (Other.tag.Equals("RightTurn"))
                {
                    Rotate = true;
                    RotateLeft = false;
                    CanRotate = false;

                    StartCoroutine("RotateDelay");
                } 
            }
        }
        if (Other.gameObject.layer != 18 && Other.gameObject.layer != 19 && Other.gameObject.layer != 20 && Other.gameObject.layer != 21 && Other.gameObject.layer != 15 && Other.gameObject.layer != 0)
        {
            m_HasCollided = true;
            Debug.Log("Layer: " + Other.gameObject.layer);
            CollisionObject = Other.gameObject.GetComponent<Obstacles>().ID;
        }
       

        // 10-13 = Obstacles + powerups
        if (Other.gameObject.layer == 10)
        {
            rigidbody.AddForce(-rigidbody.transform.forward * BlueObstacleSlowdown);
            Highscore += GreenPoints * (int)rigidbody.velocity.magnitude;
            AchievedPoint = GreenPoints * (int)rigidbody.velocity.magnitude;

        }
        else if (Other.gameObject.layer == 11)
        {
            rigidbody.AddForce(-rigidbody.transform.forward * RedObstacleSlowdown);
            Highscore += BluePoints * (int)rigidbody.velocity.magnitude;
            AchievedPoint = BluePoints * (int)rigidbody.velocity.magnitude;

        }
        else if (Other.gameObject.layer == 12)
        {
            rigidbody.AddForce(-rigidbody.transform.forward * RedObstacleSlowdown);
            Highscore += RedPoints * (int)rigidbody.velocity.magnitude;
            AchievedPoint = RedPoints * (int)rigidbody.velocity.magnitude;

        }
            // 15-20
        else if (Other.gameObject.layer == 15)
        {
            WinningStatus = true;
            StartCoroutine("WinDelay");
        }
        else if (Other.gameObject.layer == 16)      //SHIELD powerup
        {
            SHIELD = true;

        }
        else if (Other.gameObject.layer == 20)
        {
        }
        else if (Other.gameObject.layer == 21)
        {
        }
    }

    //Coroutines
    private IEnumerator RotateDelay()
    {
        yield return new WaitForSeconds(1f);
        CanRotate = true;
    }
    private IEnumerator WinDelay()
    {
        yield return new WaitForSeconds(2.0f);
        

        Win();
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(SecondsUntilStart);

        StartMoving = true;
    }


    private IEnumerator CooldownShield()
    {
        yield return new WaitForSeconds(ShieldCooldown);

        SHIELD = false;
    }
    //  End of Coroutines

    public void SetRotation(bool Left)
    {
        //Rotate = true;
        //RotateLeft = Left;

        //StartCoroutine("RotateDelay");
    }

    void CheckLose()
    {
        if(rigidbody.transform.forward.x > 0)
        {
            if(rigidbody.velocity.x < 0)
            {
                Lose();
            }
        }
        else if(rigidbody.transform.forward.x < 0)
        {
            if (rigidbody.velocity.x > 0)
            {
                Lose();
            }
        }
        else if (rigidbody.transform.forward.z > 0)
        {
            if (rigidbody.velocity.z < 0)
            {
                Lose();
            }
        }
        else if(rigidbody.transform.forward.z < 0)
        {
            if (rigidbody.velocity.z > 0)
            {
                Lose();
            }
        }
    }

    void Win()
    {
        //Application.LoadLevel("Menu");
        DoneWithSession = true;
    }

    void Lose()
    {
        //Application.LoadLevel("Menu");
        DoneWithSession = true;
        WinningStatus = false;
    }
    
    void OnDrawGizmos()
    {
        hit0 = Physics.Raycast(transform.position, transform.right, out hitinfo0, 20.0f, 1 << LayerMask.NameToLayer("Wall"));
        hit1 = Physics.Raycast(transform.position, -transform.right, out hitinfo1, 20.0f, 1 << LayerMask.NameToLayer("Wall"));

        if (hit0 && hit1)
        {
            m_HasControl = true;
            mid = (hitinfo0.point + hitinfo1.point) / 2;

            //Händer

            if(DirX)
            {
                Debugvector.Set(transform.position.x, transform.position.y, (transform.position.z - Threshold));
                Gizmos.DrawCube(Debugvector, new Vector3(0.1f, 0.1f, 0.1f));
                Debugvector.Set(transform.position.x, transform.position.y, (transform.position.z + Threshold));
                Gizmos.DrawCube(Debugvector, new Vector3(0.1f, 0.1f, 0.1f));
            }
            else
            {
                Debugvector.Set(transform.position.x - Threshold, transform.position.y, transform.position.z);
                Gizmos.DrawCube(Debugvector, new Vector3(0.1f, 0.1f, 0.1f));
                Debugvector.Set(transform.position.x + Threshold, transform.position.y, transform.position.z);
                Gizmos.DrawCube(Debugvector, new Vector3(0.1f, 0.1f, 0.1f));
            }
            
        }
        else
        {
            m_HasControl = false;
        }
    }

    public bool HasCollided()
    {
        return m_HasCollided;
    }

    public float AchievedPoints()
    {
        return AchievedPoint;
    }

    public int GetCollisionObject()
    {
        return CollisionObject;
    }
}
