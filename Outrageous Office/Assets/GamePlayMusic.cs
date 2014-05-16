using UnityEngine;
using System.Collections;

public class GamePlayMusic : MonoBehaviour {

    private AudioSource MusicSource;
    public AudioClip HetsPunkMP3;
    public AudioClip HetsPunkMP3_noIntro;
    public Player WinLose;

    private Countdown CountdownScript;
    private bool StartSound = true;

	void Start () {
        MusicSource = (AudioSource)gameObject.AddComponent("AudioSource");

        CountdownScript = GetComponentInChildren<Countdown>();
	}
	
    

	void Update () {

        if(StartSound && CountdownScript.CurrentSprite == 2)
        {
            MusicSource.clip = HetsPunkMP3;
            MusicSource.volume = 0.2f;
            MusicSource.Play();
            StartSound = false;
        }

        if (MusicSource.clip == HetsPunkMP3 && !MusicSource.isPlaying)
        {
            Debug.Log(CountdownScript.CurrentSprite);
            MusicSource.clip = HetsPunkMP3_noIntro;
            MusicSource.loop = true;
            MusicSource.Play();
        }

        if (WinLose.WinningStatus == true)
        {
            MusicSource.volume -= 0.1f * Time.deltaTime;
        }
	}

}
