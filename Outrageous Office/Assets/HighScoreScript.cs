using UnityEngine;
using System.Collections;

public class HighScoreScript : MonoBehaviour {
        
    public GameObject GC;
    public int[] ScoreList;
    private GameObject[] field = new GameObject[1];
    public GameObject text3d;
    private Vector3 tempVector = new Vector3();
    public float ydelta;

    //ACCESSING WORLDSCRIPT

	void Start () {
	    GC = GameObject.Find("WorldObject");
        ScoreList = new int[10];
        for (int i = 0; i < ScoreList.Length; i++)
        {
                ScoreList[i] = GC.GetComponent<WorldScript>().Scores[i];
        }

        Instatiatealltheobjects();
	}
	

	void Update () {
     /*   for (int i = 0; i < ScoreList.Length; i++)
        {
          // HighScoreList = (AudioSource)gameObject.AddComponent("AudioSource");
           this.GetComponent<TextMesh>().text = i.ToString() + "." + " " + ScoreList[i].ToString();
        }
       */
    }
         
    void Instatiatealltheobjects()
    {
        for (int i = 0; i < GC.GetComponent<WorldScript>().numScores; i++)
        {
            tempVector.Set(transform.position.x, transform.position.y + ydelta, transform.position.z);

            field[i] = Instantiate(text3d, tempVector, transform.rotation) as GameObject;
            field[i].GetComponent<TextMesh>().text = (i + 1).ToString() + ". " + ScoreList[i].ToString();

            transform.position = tempVector;
        }
    }



}

 //Alla objekten
    
   
    

   

