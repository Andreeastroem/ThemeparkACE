/*
 * KinectModelController.cs - Moves every 'bone' given to match
 * 				the position of the corresponding bone given by
 * 				the kinect. Useful for viewing the point tracking
 * 				in 3D.
 * 
 * 		Developed by Peter Kinney -- 6/30/2011
 * 
 */

using UnityEngine;
using System;
using System.Collections;

public class KinectPointController : MonoBehaviour {
	
    //Andrés ändringar
    public bool Ready = false;
    private float MiddlePos = 0.0f;
    public float Delta = 0.0f;
    public Vector2 LeftHand = new Vector2(0f, 0f), RightHand = new Vector2(0f, 0f);
    private Vector2 Hand;

    public bool ReachingHand;
    public float ReachOutPercent;
    private float ArmLength;
    private bool CheckArm = true;
    //slut på Andrés ändringar


	//Assignments for a bitmask to control which bones to look at and which to ignore
	public enum BoneMask
	{
		None = 0x0,
		Hip_Center = 0x1,
		Spine = 0x2,
		Shoulder_Center = 0x4,
		Head = 0x8,
		Shoulder_Left = 0x10,
		Elbow_Left = 0x20,
		Wrist_Left = 0x40,
		Hand_Left = 0x80,
		Shoulder_Right = 0x100,
		Elbow_Right = 0x200,
		Wrist_Right = 0x400,
		Hand_Right = 0x800,
		Hip_Left = 0x1000,
		Knee_Left = 0x2000,
		Ankle_Left = 0x4000,
		Foot_Left = 0x8000,
		Hip_Right = 0x10000,
		Knee_Right = 0x20000,
		Ankle_Right = 0x40000,
		Foot_Right = 0x80000,
		All = 0xFFFFF,
		Torso = 0x10000F, //the leading bit is used to force the ordering in the editor
		Left_Arm = 0x1000F0,
		Right_Arm = 0x100F00,
		Left_Leg = 0x10F000,
		Right_Leg = 0x1F0000,
		R_Arm_Chest = Right_Arm | Spine,
		No_Feet = All & ~(Foot_Left | Foot_Right),
		UpperBody = Shoulder_Center | Head|Shoulder_Left | Elbow_Left | Wrist_Left | Hand_Left|
		Shoulder_Right | Elbow_Right | Wrist_Right | Hand_Right
		
	}
	
	public SkeletonWrapper sw;
	
	public GameObject Hip_Center;
	public GameObject Spine;
	public GameObject Shoulder_Center;
	public GameObject Head;
	public GameObject Shoulder_Left;
	public GameObject Elbow_Left;
	public GameObject Wrist_Left;
	public GameObject Hand_Left;
	public GameObject Shoulder_Right;
	public GameObject Elbow_Right;
	public GameObject Wrist_Right;
	public GameObject Hand_Right;
	public GameObject Hip_Left;
	public GameObject Knee_Left;
	public GameObject Ankle_Left;
	public GameObject Foot_Left;
	public GameObject Hip_Right;
	public GameObject Knee_Right;
	public GameObject Ankle_Right;
	public GameObject Foot_Right;
	
	private GameObject[] _bones; //internal handle for the bones of the model
	//private Vector4[] _bonePos; //internal handle for the bone positions from the kinect
	
	public int player;
	public BoneMask Mask = BoneMask.All;
	
	public float scale = 1.0f;
	
	// Use this for initialization
	void Start () {
		//store bones in a list for easier access
		_bones = new GameObject[(int)Kinect.NuiSkeletonPositionIndex.Count] {Hip_Center, Spine, Shoulder_Center, Head,
			Shoulder_Left, Elbow_Left, Wrist_Left, Hand_Left,
			Shoulder_Right, Elbow_Right, Wrist_Right, Hand_Right,
			Hip_Left, Knee_Left, Ankle_Left, Foot_Left,
			Hip_Right, Knee_Right, Ankle_Right, Foot_Right};
		//_bonePos = new Vector4[(int)BoneIndex.Num_Bones
	}
	/*
	// Update is called once per frame
	void Update () {
		if(player == -1)
			return;
		//update all of the bones positions
		if (sw.pollSkeleton())
		{
			for( int ii = 0; ii < (int)Kinect.NuiSkeletonPositionIndex.Count; ii++) {
				//_bonePos[ii] = sw.getBonePos(ii);
				if( ((uint)Mask & (uint)(1 << ii) ) > 0 ){
					//_bones[ii].transform.localPosition = sw.bonePos[player,ii];
					_bones[ii].transform.localPosition = new Vector3(
						sw.bonePos[player,ii].x * scale,
						sw.bonePos[player,ii].y * scale,
						sw.bonePos[player,ii].z * scale);
				}
			}
		}
	}
    */

    //Andrés ändringar

    // Update is called once per frame
    void Update()
    {
        if (player == -1)
            return;

        if(Ready)
        {
            Delta = MiddlePos - sw.bonePos[0, 2].x;

            Hand.Set(sw.bonePos[0, 2].x - sw.bonePos[0, 7].x, sw.bonePos[0, 2].y - sw.bonePos[0, 7].y);
            LeftHand = Hand;
            Hand.Set(sw.bonePos[0, 2].x - sw.bonePos[0, 11].x, sw.bonePos[0, 2].y - sw.bonePos[0, 11].y);
            RightHand = Hand;
        }
        else
        {
            CheckHandGesture();
            if(Ready)
            {
                MiddlePos = sw.bonePos[0, 2].x;
            }
        }

        //update all of the bones positions
        if (sw.pollSkeleton())
        {
            for (int ii = 0; ii < (int)Kinect.NuiSkeletonPositionIndex.Count; ii++)
            {
                //_bonePos[ii] = sw.getBonePos(ii);
                if (((uint)Mask & (uint)(1 << ii)) > 0)
                {
                    //_bones[ii].transform.localPosition = sw.bonePos[player,ii];
                    _bones[ii].transform.localPosition = new Vector3(
                        sw.bonePos[player, ii].x * scale,
                        sw.bonePos[player, ii].y * scale,
                        sw.bonePos[player, ii].z * scale);
                }
            }
        }
    }

    void CheckHandGesture()
    {
        if(sw.bonePos[0, 7].y > sw.bonePos[0, 2].y || sw.bonePos[0, 11].y > sw.bonePos[0, 2].y)
        {
            CheckArmLength();
            Ready = true;
        }
    }

    void CheckArmLength()
    {
        if(CheckArm)
        {
            ArmLength = 0f;
            //Calculate the length of the arm

            ArmLength += Vector3.Distance(sw.bonePos[0, 7], sw.bonePos[0, 5]);
            ArmLength += Vector3.Distance(sw.bonePos[0, 5], sw.bonePos[0, 4]);
            
            if(ArmLength > 0.15f)
            {
                CheckArm = false;
            }
            else
            {
                ArmLength = 0f;

                ArmLength += Vector3.Distance(sw.bonePos[0, 11], sw.bonePos[0, 9]);
                ArmLength += Vector3.Distance(sw.bonePos[0, 9], sw.bonePos[0, 8]);

                Debug.Log("Right arm: " + ArmLength);

                if(ArmLength > 0.15f)
                {
                    CheckArm = false;
                }
            }
        }
    }

    public bool ReachingOutRightHand()
    {
        if(CheckArm)
        {
            return false;
        }
        if (Vector3.Distance(sw.bonePos[0, 11], sw.bonePos[0, 8]) > (ArmLength / ReachOutPercent))
        {
            return true;
        }
        else
        {
          return false;  
        }
    }

    public bool ReachingOutLeftHand()
    {
        if (CheckArm)
        {
            return false;
        }
        if (Vector3.Distance(sw.bonePos[0, 7], sw.bonePos[0, 4]) > (ArmLength / ReachOutPercent))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
