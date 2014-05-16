using UnityEngine;
using System.Collections;

public class ScaleEffect : MonoBehaviour {

    //Scale effect

    /*
     * Scale effect is the amount the object will be scaled. 1 is original size, 2 is half the scale and 0.5 is twice the size
     */
    private Vector3 OriginalScale;
    public float NewScaleEffect;
    public float ScaleTime;
    public float ScaleBackTime;
    private Vector3 TargetScale;
    private float Scaled;
    private bool ScaleBack;

	// Use this for initialization
	void Start () {
        //Scaling
        OriginalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        //Scaling
        Scaled = 0f;
        if (!ScaleBack)
        {
            TargetScale.Set(Mathf.Lerp(transform.localScale.x, OriginalScale.x / NewScaleEffect, Time.deltaTime / ScaleTime),
                Mathf.Lerp(transform.localScale.y, OriginalScale.y / NewScaleEffect, Time.deltaTime / ScaleTime),
                Mathf.Lerp(transform.localScale.z, OriginalScale.z / NewScaleEffect, Time.deltaTime / ScaleTime));

            transform.localScale = TargetScale;

            Scaled += Mathf.Abs(transform.localScale.x - (OriginalScale.x / NewScaleEffect));
            Scaled += Mathf.Abs(transform.localScale.y - (OriginalScale.y / NewScaleEffect));
            Scaled += Mathf.Abs(transform.localScale.z - (OriginalScale.z / NewScaleEffect));

            if (Scaled < 0.2)
            {
                ScaleBack = true;
            }
        }
        else
        {
            TargetScale.Set(Mathf.Lerp(transform.localScale.x, OriginalScale.x, Time.deltaTime / ScaleBackTime),
                Mathf.Lerp(transform.localScale.y, OriginalScale.y, Time.deltaTime / ScaleBackTime),
                Mathf.Lerp(transform.localScale.z, OriginalScale.z, Time.deltaTime / ScaleBackTime));
            transform.localScale = TargetScale;

            Scaled += Mathf.Abs(transform.localScale.x - (OriginalScale.x));
            Scaled += Mathf.Abs(transform.localScale.y - (OriginalScale.y));
            Scaled += Mathf.Abs(transform.localScale.z - (OriginalScale.z));

            if (Scaled < 0.2)
            {
                ScaleBack = false;
            }
        }
	}
}
