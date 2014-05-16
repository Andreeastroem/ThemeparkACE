using UnityEngine;
using System.Collections;

public class FeedbackCamera : MonoBehaviour {

    public GameObject PlayerObject;
    private Player PlayerScript;

    public Sprite[] Sprites;

    private Transform c_FeedbackText;
    private SpriteRenderer c_FeedbackText_SpriteRenderer;

    private FeedbackEffect EffectScript;

	// Use this for initialization
	void Start () {
        c_FeedbackText = transform.parent.FindChild("Feedback Text");
        c_FeedbackText_SpriteRenderer = c_FeedbackText.GetComponent<SpriteRenderer>();

        c_FeedbackText_SpriteRenderer.sprite = null;
        
        PlayerScript = PlayerObject.GetComponent<Player>();

        EffectScript = c_FeedbackText.GetComponent<FeedbackEffect>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(PlayerScript.HasCollided())
        {
            EffectScript.SetEffect(true);

            Debug.Log("POINTS: " + PlayerScript.AchievedPoints());

            if(PlayerScript.AchievedPoints() <= 10)
            {
                c_FeedbackText_SpriteRenderer.sprite = Sprites[0];
                Debug.Log("0");
            }
            else if (PlayerScript.AchievedPoints() <= 100 && PlayerScript.AchievedPoints() > 10)
            {
                c_FeedbackText_SpriteRenderer.sprite = Sprites[1];
                Debug.Log("1");
            }
            else if (PlayerScript.AchievedPoints() <= 1000 && PlayerScript.AchievedPoints() > 100)
            {
                c_FeedbackText_SpriteRenderer.sprite = Sprites[2];
                Debug.Log("2");
            }
            else if(PlayerScript.AchievedPoints() > 1000 && PlayerScript.AchievedPoints() <= 2500)
            {
                c_FeedbackText_SpriteRenderer.sprite = Sprites[3];
                Debug.Log("3");
            }
            else if (PlayerScript.AchievedPoints() > 2500)
            {
                c_FeedbackText_SpriteRenderer.sprite = Sprites[4];
                Debug.Log("4 " + c_FeedbackText_SpriteRenderer.sprite.ToString());
            }
        }
	}
}
