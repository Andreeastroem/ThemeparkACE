using UnityEngine;
using System.Collections;

public class FeedbackEffect : MonoBehaviour {

    public bool SwooshIn;
    public bool PlayEffect;
    private float OriginalYPosition;

    //Parent controll transform
    private Transform ControllParent;

    //Rotation


    //fading
    private SpriteRenderer m_SpriteRenderer;
    private Color newColour = new Color();
    public float FadingTime;

    //SmashingIn
    private Vector3 OriginalScale;
    public Vector3 NewScale;
    public float ScaleTime;


    //falling
    private float TargetY;
    private Vector3 TargetPosition;
    public float FallTime;
    public float FallLength;

	// Use this for initialization
	void Start () {

        ControllParent = transform.parent.FindChild("CameraHUD");
        if(ControllParent == null)
        {
            return;
        }

        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        if (m_SpriteRenderer == null)
        {
            return;
        }
        newColour = m_SpriteRenderer.color;
        newColour.a = 0f;
        m_SpriteRenderer.color = newColour;

        //Rotation


        //Scale
        OriginalScale = transform.localScale;
        transform.localScale = NewScale;

        //falling
        TargetY = transform.position.y - FallLength;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_SpriteRenderer == null)
        {
            return;
        }

        if(PlayEffect)
        {
            if (SwooshIn)
            {
                //fading
                newColour.a = Mathf.Lerp(newColour.a, 1, Time.deltaTime / FadingTime);
                m_SpriteRenderer.color = newColour;

                //Rotation


                //Scaling
                NewScale.Set(Mathf.Lerp(NewScale.x, OriginalScale.x, Time.deltaTime / ScaleTime),
                    Mathf.Lerp(NewScale.y, OriginalScale.y, Time.deltaTime / ScaleTime),
                    Mathf.Lerp(NewScale.z, OriginalScale.z, Time.deltaTime / ScaleTime));
                transform.localScale = NewScale;

                //Check if done
                if (newColour.a > 0.98f)
                {
                    
                    if (transform.localScale.x > (OriginalScale.x - 0.02))
                    {
                        SwooshIn = false;
                    }
                    
                }
            }
            else
            {
                TargetPosition.Set(transform.position.x, Mathf.Lerp(transform.position.y, TargetY,  Time.deltaTime / FallTime), transform.position.z);
                transform.position = TargetPosition;

                newColour.a = Mathf.Lerp(newColour.a, 0, Time.deltaTime / FadingTime);
                m_SpriteRenderer.color = newColour;

                if (Mathf.Abs(transform.position.y - TargetY) < 0.1f)
                {
                    if (m_SpriteRenderer.color.a < 0.05f)
                    {
                        m_SpriteRenderer.sprite = null;
                    }
                }
            }
        }
	}

    public void SetEffect(bool state)
    {
        PlayEffect = state;

        //Reset
        newColour = m_SpriteRenderer.color;
        newColour.a = 0f;
        m_SpriteRenderer.color = newColour;


        //Rotation


        //Scaling
        NewScale.Set(0f, 0f, 0f);
        transform.localScale = NewScale;
        
        TargetPosition.Set(transform.position.x, ControllParent.position.y, transform.position.z);
        transform.position = TargetPosition;

        TargetY = transform.position.y - FallLength;

        SwooshIn = true;
    }
}
