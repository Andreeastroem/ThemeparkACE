using UnityEngine;
using System.Collections;

public class Cloyd : MonoBehaviour
{

    public Animator anim;


    public GameObject ParticleSystem;

    public GameObject PlayerObj;

    float Velocity = 0;


    // Use this for initialization
    void Start()
    {

        PlayerObj = GameObject.Find("MainCharacter");
        anim = GetComponent<Animator>();
        Instantiate(ParticleSystem, new Vector3(transform.position.x, transform.position.y - renderer.bounds.size.y, transform.position.z), new Quaternion(0, 180, 180, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 1.5f);
    }
}
