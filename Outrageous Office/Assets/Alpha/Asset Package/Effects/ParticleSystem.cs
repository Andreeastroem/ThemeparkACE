using UnityEngine;
using System.Collections;

public class ParticleSystem : MonoBehaviour {

    //private ParticleSystem m_ParticleSystem;
    public float lifeTime;

    private int length;
    private Particle[] m_particle = new Particle[2];

	// Use this for initialization
	void Start () {
        rigidbody.velocity = GameObject.Find("MainCharacter").rigidbody.velocity;
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(this.gameObject, lifeTime);

        //particleSystem.GetParticles();
	}
}
