using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Countdown : MonoBehaviour {

    public float SecondsToWait;
    private bool AlreadyDone = false;

    //Sprites
    public Sprite[] CountdownSequence;
    public int CurrentSprite = 0;

    private float OriginalYPosition;
    public bool SwooshIn;
    public bool PlayEffect;

    //Parent controll transform
    private Transform ControllParent;

    //fading
    private SpriteRenderer m_SpriteRenderer;
    private Color newColour = new Color();
    public float FadingTime;

    //Smashing Out
    private Vector3 OriginalScale;
    public Vector3 StartScale;
    public float ScaleTime;
    private Vector3 TargetScale = new Vector3();
    private Vector3 TempVector3 = new Vector3();

    //falling
    private float TargetY;
    private Vector3 TargetPosition;
    public float FallTime;
    public float FallLength;

	// Use this for initialization
	void Start () {
        ControllParent = transform.parent;
        if (ControllParent == null)
        {
            return;
        }

        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        if (m_SpriteRenderer == null)
        {
            return;
        }

        m_SpriteRenderer.sprite = CountdownSequence[CurrentSprite];
        CurrentSprite++;

        newColour = m_SpriteRenderer.color;
        newColour.a = 0f;
        m_SpriteRenderer.color = newColour;

        //Scale
        OriginalScale = transform.localScale;
        transform.localScale = StartScale;

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
            if(SwooshIn)
            {
                //fade in
                newColour.a = Mathf.Lerp(newColour.a, 1, Time.deltaTime / FadingTime);
                m_SpriteRenderer.color = newColour;

                //Scaling down
                TempVector3 = Vector3.Lerp(transform.localScale, TargetScale, Time.deltaTime / ScaleTime);
                transform.localScale = TempVector3;

                //Check if done
                if (newColour.a > 0.98f)
                {
                    if (transform.localScale.x < (OriginalScale.x + 0.1))
                    {
                        if(!AlreadyDone)
                        {
                            StartCoroutine("Swoosh");
                            AlreadyDone = true;
                        }
                        
                    }
                }
            }
            else
            {
                //Fade out
                newColour.a = Mathf.Lerp(newColour.a, 0, Time.deltaTime / FadingTime);
                m_SpriteRenderer.color = newColour;


                //Scale down
                TempVector3 = Vector3.Lerp(transform.localScale, TargetScale, Time.deltaTime / ScaleTime);
                transform.localScale = TempVector3;

                if(newColour.a < 0.1f)
                {
                    if(transform.localScale.x < (TargetScale.x + 0.1f))
                    {
                        PlayEffect = false;
                    }
                }
            }
            
        }
        else
        {
            //RESET
            SwooshIn = true;
            AlreadyDone = false;
            if(CurrentSprite < (CountdownSequence.Length))
            {
                m_SpriteRenderer.sprite = CountdownSequence[CurrentSprite];
                CurrentSprite++;
                transform.position = ControllParent.position;
                transform.localScale = StartScale;
                PlayEffect = true;
                
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
	}

    //Coroutines
    private IEnumerator Swoosh()
    {
        yield return new WaitForSeconds(SecondsToWait);

        SwooshIn = false;
    }
}
