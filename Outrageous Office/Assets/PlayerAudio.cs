using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource wheelSoundS;
    public AudioClip wheelSound;
    private AudioSource turnSoundS;
    public AudioClip turnSound;

    void Start()
    {
        wheelSoundS = (AudioSource)gameObject.AddComponent("AudioSource");
        wheelSoundS.clip = wheelSound;
        wheelSoundS.loop = true;
        wheelSoundS.pitch = 1;
        wheelSoundS.Play();
        turnSoundS = (AudioSource)gameObject.AddComponent("AudioSource");
        turnSoundS.clip = turnSound;
        turnSoundS.pitch = 1;
   //     audio.loop = true;
   //     audio.clip = ChairWheel;
  //      audio.Play();
    }

    void Update(){
        float Velocity = this.rigidbody.velocity.magnitude;
        wheelSoundS.pitch = Velocity * 1 / 15;
    }

    void OnTriggerEnter(Collider Other)
    {
            if (Other.tag.Equals("LeftTurn"))
            {
                turnSoundS.PlayOneShot(turnSound);


            }
            else if (Other.tag.Equals("RightTurn"))
            {
                turnSoundS.PlayOneShot(turnSound);

            }
        }
}