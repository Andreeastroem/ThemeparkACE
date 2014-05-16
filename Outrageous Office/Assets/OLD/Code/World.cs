using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

    public GameObject Road;

    public GameObject[] RoadParts = new GameObject[20];
    public Transform Player;
    public GameObject[] Backgrounds = new GameObject[2];
    public GameObject[] Houses = new GameObject[8];

    public float PlayerZ;
    private float lastPlayerZ;
    private float CurrentPlayerZ;
    private float segmentsize;
    private float BackgroundDistance;
    private float DistanceTravelled;

	// Use this for initialization
    void Start()
    {
        DistanceTravelled = 0;

        BackgroundDistance = Mathf.Abs(Player.position.z - Backgrounds[0].transform.position.z);
        lastPlayerZ = Player.position.z;
        CurrentPlayerZ = lastPlayerZ;
        segmentsize = RoadParts[0].collider.bounds.size.z;

        for (int i = 1; i < RoadParts.Length; i++)
        {
            RoadParts[i] = Instantiate(RoadParts[i - 1], new Vector3(RoadParts[i - 1].transform.position.x, RoadParts[i - 1].transform.position.y, RoadParts[i - 1].transform.position.z - segmentsize), RoadParts[i - 1].transform.rotation) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float LastPlayerZ = CurrentPlayerZ;
        CurrentPlayerZ = Player.position.z;
        float playerZdiff = lastPlayerZ - CurrentPlayerZ;

        float playerZDiff = LastPlayerZ - CurrentPlayerZ;
        DistanceTravelled += playerZDiff;

        if(playerZdiff > segmentsize)
        {
            for(int i = 0; i < RoadParts.Length; i++)
            {
                float TargetZ = RoadParts[i].transform.position.z - playerZdiff;
                
                RoadParts[i].transform.position = new Vector3 (RoadParts[i].transform.position.x, RoadParts[i].transform.position.y, TargetZ);
            }
            lastPlayerZ = CurrentPlayerZ;
        }

        for (int i = 0; i < Backgrounds.Length; i++)
        {
            float targetZ = Player.position.z - BackgroundDistance;
            Backgrounds[i].transform.position = new Vector3(Backgrounds[i].transform.position.x, Backgrounds[i].transform.position.y, targetZ);
        }
        
        if(DistanceTravelled > BackgroundDistance)
        {
            for (int i = 0; i < Houses.Length; i++)
            {
                float targetZ = Houses[i].transform.position.z - DistanceTravelled;
                Houses[i].transform.position = new Vector3(Houses[i].transform.position.x, Houses[i].transform.position.y, targetZ);
            }
            DistanceTravelled = 0.0f;
        }
	}
}
