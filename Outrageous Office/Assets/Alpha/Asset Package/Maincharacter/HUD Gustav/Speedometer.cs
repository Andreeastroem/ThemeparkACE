using UnityEngine;
using System.Collections;


public class Speedometer : MonoBehaviour {

    public Texture2D scoreletters;


    private float maxSpeed = 20;
    public float curSpeed = 0;

    public GUISkin UI_Skin = null;

    public Texture2D foreGround;
    public Texture2D backGround;
    public Texture2D letters;

    public float speedoMeterWidth;
    float speedoMeterLength;

    private Player getPlayerScript;

    private float originalWidth = 1920;
    private float originalHeight = 1080;
    private Vector3 scale;

    void Start(){
        getPlayerScript = GetComponent<Player>();
        maxSpeed = getPlayerScript.MaxSpeed * 2;
    }

    void Update(){
       adjustCurrentSpeed(0);
    }

    void OnGUI(){
        scale.x = Screen.width / originalWidth;
        scale.y = Screen.height / originalHeight;
        scale.z = 1;

        var theMatrix = GUI.matrix;

        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

        if(getPlayerScript.StartMoving)
        {
            if (!getPlayerScript.DoneWithSession)
            {
                if (UI_Skin != null)
                {
                    GUI.skin = UI_Skin;
                }
                GUI.BeginGroup(new Rect(20, 23, speedoMeterLength, 300));
                GUI.Box(new Rect(62, 90, 494, 74), backGround);
                GUI.EndGroup();
                GUI.Box(new Rect(10, 10, 600, 300), foreGround);
                GUI.Box(new Rect(5, 5, 270, 158), letters);

                //Score
                GUI.color = Color.blue;
                GUI.Label(new Rect(1650, 120, 200, 200), getPlayerScript.Highscore.ToString());
                GUI.Box(new Rect(1683, 10, 209, 136), scoreletters);

                //Debug

            }
        }

        GUI.matrix = theMatrix;
    }

    public void adjustCurrentSpeed(float adj){
        
        adj = this.rigidbody.velocity.magnitude;

        curSpeed = adj;

        if (curSpeed < 0){
            curSpeed = 0;
        }

        if (curSpeed > maxSpeed){
            curSpeed = maxSpeed;
        }

        if (maxSpeed < 1){
            maxSpeed = 1;
        }
        speedoMeterLength = speedoMeterWidth * (curSpeed / (float)maxSpeed) + 10;
    }
}


