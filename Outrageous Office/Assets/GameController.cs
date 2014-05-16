using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public int numSelectors = 12;

    private Player Playerscript = null;

    private GameObject BlueObstacles = null;

    private int i = 1;

    public GameObject selector;

    //public int Worldscore;

    public bool DoneWithSession;

    public static int Worldscore;

    public GUISkin UI_Skin = null;

    public Texture2D scoreletters;

    private GameObject MainCharacter = null;

	// Use this for initialization


    // Score sceen
    bool scorerenderer = false;

    public int[] Crashedobjects;

    private int B = 0;

    private GameObject[] field = new GameObject[50]; //Alla objekten
    public GameObject BLUE = null;
    public GameObject RED = null;
    public GameObject GREEN = null;
    private Vector3 tempVector = new Vector3();
    public float delta = 1;
    private float sumdelta = 0f;
    public float z_DELTA = 0.1f;
    public int arbitrartnummer = 5;

    //loadout of objects
    public GameObject[] ObjectTypes = new GameObject[20];

    void Start()
    {
        Crashedobjects = new int[i];



        loadprefabs();

        DontDestroyOnLoad(this.gameObject);

        if (Application.loadedLevelName == "Worldloader")
        {
            Application.LoadLevel("Menu");
        }
        Debug.Log(gos[0]);
    }
    
    IEnumerator LoadMenu()
    {
        Debug.Log("We are in your loading");
        AsyncOperation async = Application.LoadLevelAsync("Menu");
        yield return async;
        Debug.Log("All your loading belong to us");
    }
    IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(2.0f);
        Application.LoadLevel("Alpha");

    }
    IEnumerator LoadScore()
    {
        yield return new WaitForSeconds(1.0f);
        Application.LoadLevel("Score");
    }

    void Destroyedobjectsrenderer()
    {
        for (int R = 0; Crashedobjects.Length >= R; R++)
        {
            B = Crashedobjects[R];

            BLUE = ObjectTypes[B];

            
            if (BLUE != ObjectTypes[0])
            {
                field[R] = Instantiate(BLUE, transform.position, transform.rotation) as GameObject; //Skapar ett objekt av typen BLUE på positionen transform.position, med dess rotation transform.rotation
            }
            if (R % arbitrartnummer == 0)
            {
                tempVector.Set(transform.position.x - sumdelta, transform.position.y + delta, transform.position.z + z_DELTA);
                sumdelta = 0f;
            }
            else
            {
                tempVector.Set(transform.position.x + delta, transform.position.y, transform.position.z + z_DELTA);
                sumdelta += delta;
            }

            transform.position = tempVector;
            
        }
    }


	// Update is called once per frame
	void Update () 
    {
        if(Application.loadedLevelName == "Alpha")
        {
            if(MainCharacter == null)
            {
                MainCharacter = GameObject.Find("MainCharacter");
                Playerscript = MainCharacter.GetComponent<Player>();

                Playerscript.CollisionObject = -1;
            }
        }

        if (Application.loadedLevelName == "Menu" && Input.GetKeyDown("space"))
        {
            StartCoroutine(LoadGame());
        }

        if (Application.loadedLevelName == "Score" && Input.GetKeyDown("u"))
        {
            StartCoroutine(LoadMenu());
        }

        if (Application.loadedLevelName == "Score" && scorerenderer == false)
        {
            Destroyedobjectsrenderer();

            scorerenderer = true;
        }


        if (Application.loadedLevelName == "Alpha")
        {
            //GameObject MainCharacter = GameObject.Find("MainCharacter");
            //Player Playerscript = MainCharacter.GetComponent<Player>();

            if (Playerscript.DoneWithSession == true)
            {
                StartCoroutine(LoadScore());

                Worldscore = Playerscript.Highscore;

                //Debug.Log(Worldscore);

                /*
                if (UI_Skin != null)
                {

                    GUI.skin = UI_Skin;

                }
                
                for (int i = 0; i < numSelectors; i++)
                {

                    GameObject go = Instantiate(selector, new Vector3((float)i, 1, 0), Quaternion.identity) as GameObject;
                    go.transform.localScale = Vector3.one;
                    Blueobjects[i] = go;
                }
                */
                //Score
                //GUI.color = Color.blue;
                //GUI.Label(new Rect(1300, 120, 200, 200), Worldscore.ToString());
                //GUI.Box(new Rect(1333, 10, 209, 136), scoreletters);
                Playerscript.DoneWithSession = false;
            }
        }
        
        // ID counter blue obj
        if (MainCharacter != null)
        {
            if (Playerscript.CollisionObject == 0)
            {
                Crashedobjects[i] = 0;
                i++;
                Debug.Log("Kruk");
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 1)
            {
                Crashedobjects[i] = 1;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 2)
            {
                Crashedobjects[i] = 2;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 3)
            {
                Crashedobjects[i] = 3;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 4)
            {
                Crashedobjects[i] = 4;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 5)
            {
                Crashedobjects[i] = 5;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 6)
            {
                Crashedobjects[i] = 6;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 7)
            {
                Crashedobjects[i] = 7;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 8)
            {
                Crashedobjects[i] = 8;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 9)
            {
                Crashedobjects[i] = 9;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 10)
            {
                Crashedobjects[i] = 10;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 11)
            {
                Crashedobjects[i] = 11;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 12)
            {
                Crashedobjects[i] = 12;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 13)
            {
                Crashedobjects[i] = 13;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 14)
            {
                Crashedobjects[i] = 14;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 15)
            {
                Crashedobjects[i] = 15;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 16)
            {
                Crashedobjects[i] = 16;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 17)
            {
                Crashedobjects[i] = 17;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 18)
            {
                Crashedobjects[i] = 18;
                i++;
                Playerscript.CollisionObject = -1;
            }
            else if (Playerscript.CollisionObject == 19)
            {
                Crashedobjects[i] = 19;
                i++;
                Playerscript.CollisionObject = -1;
            }

        }


    }
    void OnGUI()
    {
        //GUILayout.Label("Score: " + Worldscore);
        //GUI.Label(new Rect(10, 10, 200, 30), "score:" + Worldscore);
        //GUI.Box(new Rect(10, 10, 209, 30), scoreletters);
    }

    GameObject[] gos = new GameObject[50];

    void loadprefabs()
    {
        for (int i = 0; i < 13; i++ )
        {
            Debug.Log("LOADPREFABS: " + i);
            switch(i)
            {
                case 2:
                    gos[i] = Instantiate(ObjectTypes[1]) as GameObject;
                    break;
                case 3:
                    gos[i] = Instantiate(ObjectTypes[2]) as GameObject;
                    break;
                case 4:
                    gos[i] = Instantiate(ObjectTypes[3]) as GameObject;
                    break;
                case 5:
                    gos[i] = Instantiate(ObjectTypes[4]) as GameObject;
                    break;
                case 6:
                    gos[i] = Instantiate(ObjectTypes[5]) as GameObject;
                    break;
                case 7:
                    gos[i] = Instantiate(ObjectTypes[6]) as GameObject;
                    break;
                case 8:
                    gos[i] = Instantiate(ObjectTypes[7]) as GameObject;
                    break;
                case 9:
                    gos[i] = Instantiate(ObjectTypes[8]) as GameObject;
                    break;
                case 10:
                    gos[i] = Instantiate(ObjectTypes[9]) as GameObject;
                    break;
                case 11:
                    gos[i] = Instantiate(ObjectTypes[10]) as GameObject;
                    break;
                case 12:
                    gos[i] = Instantiate(ObjectTypes[11]) as GameObject;
                    break;
            }
        }
    }
}