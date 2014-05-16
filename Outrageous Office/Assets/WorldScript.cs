    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    
    

    /// <summary>
    /// High score manager.
    /// Local highScore manager for LeaderboardLength number of entries
    ///
    /// this is a singleton class. to access these functions, use WorldScript._instance object.
    /// eg: WorldScript._instance.SaveHighScore("meh",1232);
    /// No need to attach this to any game object, thought it would create errors attaching.
    /// </summary>

public class WorldScript : MonoBehaviour
{

    private GameObject PlayerScore;
    public int TempScore;
    public bool doneWithSession;
    private static WorldScript m_instance;
    private const int LeaderboardLength = 10;
    public int[] Scores = null;
    WebCamTexture CamTex;
    public string deviceName;
    public Texture2D datPhoto;
//    public Color32[] data;

    public static WorldScript _instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new GameObject("WorldScript").AddComponent<WorldScript>();
            }
            return m_instance;
        }

    }

    void Start()
    {
        PlayerScore = GameObject.Find("MainCharacter");

        
        doneWithSession = PlayerScore.GetComponent<Player>().DoneWithSession;
        TempScore = PlayerScore.GetComponent<Player>().Highscore;

        DontDestroyOnLoad(gameObject);

        CamTex = new WebCamTexture();
        WebCamDevice[] device = WebCamTexture.devices;
        if (0 < device.Length)
        {
            Debug.Log(device[0].name);
            deviceName = device[0].name;
            CamTex = new WebCamTexture(deviceName, 500, 500, 5);
            CamTex.Play();
//            data = new Color32[CamTex.width * CamTex.height]; 
        }
        datPhoto = new Texture2D(500, 500);
//        renderer.material.mainTexture = CamTex;
        TakeSnapshot(datPhoto);

    }

    void Awake()
    {
        
        if (m_instance == null)
        {
            m_instance = this;
        }
        else if (m_instance != this)
            Destroy(gameObject);

    }



            


        private int upper;
        public int numScores;
        public void CArray(int size)
        {
            Scores = new int[size];
            upper = size - 1;
            numScores = 0;

            for (int i = 0; i < Scores.Length; i++)
            {
                Scores[i] = 0;
            }
        }
        public void insert(int item)
        {
            Scores[numScores] = item;
            numScores++;
        }
        public void clear()
        {
            for (int i = 0; i <= upper; i++)
                Scores[i] = 0;
            numScores = 0;
        }

        public void insertionsort() { 
        int inner, temp; 

        for (int outer = 1; outer >= upper; outer++) {                            

        temp = Scores[outer]; 
        inner = outer; 
        while (inner < 0 && Scores[inner - 1] <= temp) {  
            Scores[inner] = Scores[inner-1]; 
            inner -= 1;
        } 

        Scores[inner] = temp; 
    } 

        }
    public int SaveCurrentScore(int tempScore)
    {
    if (PlayerScore.GetComponent<Player>().DoneWithSession){

        return PlayerScore.GetComponent<Player>().Highscore;
    }
    else
    {
        return 0;
    }
    
    }

    private string SavePath = "C:/Users/RumpNissen/Documents/Theme park start 3/Assets/Test"; //Change the path here!
    public int CaptureCounter = 0;

    public void TakeSnapshot(Texture2D SnapShot)
    {
        SnapShot = new Texture2D(CamTex.width, CamTex.height);
        SnapShot.SetPixels(CamTex.GetPixels());
        SnapShot.Apply();
        
        System.IO.File.WriteAllBytes(SavePath + CaptureCounter.ToString() + ".png", SnapShot.EncodeToPNG());
        ++CaptureCounter;
    }


    /*
    public void SaveCurrentScore()
    {
        int tempScore = PlayerScore.Highscore;
        int i = 1;
        while (i <= LeaderboardLength){

            if (tempScore > Scores[i])
            {
                for (int j = 0; j <= LeaderboardLength; j++)
                {
                    Scores[j - 1] = Scores[j];
                }
                    Scores[i] = tempScore;

            }
            if (Scores[i] != tempScore)
            {
                i++;
            }
        }
    }
    */

    /*
   public void SaveHighScore (string name, int score)
   {
       List<Scores> HighScores = new List<Scores> ();
     
       int i = 1;
       while (i<=LeaderboardLength && PlayerPrefs.HasKey("HighScore"+i+"score")) {
           Scores temp = new Scores ();
           temp.score = PlayerPrefs.GetInt ("HighScore" + i + "score");
          // temp.photo[i] = 
           HighScores.Add (temp);
           i++;
       }
       if (HighScores.Count == 0) {
           Scores _temp = new Scores ();
          // _temp.photo[i] = 
           _temp.score = score;
           HighScores.Add (_temp);
       } 
       else {
           for (i=1; i<=HighScores.Count && i<=LeaderboardLength; i++) {
               if (score > HighScores [i - 1].score) {
                   Scores _temp = new Scores ();
                 //  _temp.photo[i] = 
                   _temp.score = score;
                   HighScores.Insert (i - 1, _temp);
                   break;
               }
               if (i == HighScores.Count && i < LeaderboardLength) {
                   Scores _temp = new Scores ();
                //   _temp.photo[i] = name; score;
                   HighScores.Add (_temp);
                   break;
               }
           }
       }
     
   i = 1;
   while (i<=LeaderboardLength && i<=HighScores.Count) {
   //    PlayerPrefs.SetString ("HighScore" + i + "name", HighScores [i - 1].photo);
       PlayerPrefs.SetInt ("HighScore" + i + "score", HighScores [i - 1].score);
       i++;
   }
     
   }
     
   public List<Scores> GetHighScore ()
   {
       List<Scores> HighScores = new List<Scores> ();
     
       int i = 1;
       while (i<=LeaderboardLength && PlayerPrefs.HasKey("HighScore"+i+"score")) {
           Scores temp = new Scores ();
           temp.score = PlayerPrefs.GetInt ("HighScore" + i + "score");
          // temp.photo[i] = 
           HighScores.Add (temp);
           i++;
       }
     
       return HighScores;
   }
*/
    /*
        public void ClearLeaderBoard ()
        {
            //for(int i=0;i<HighScores.
            List<Scores> HighScores = GetHighScore();
     
            for(int i=1;i<=HighScores.Count;i++)
            {
                PlayerPrefs.DeleteKey("HighScore" + i + "name");
                PlayerPrefs.DeleteKey("HighScore" + i + "score");
            }
        }
           
  
        }
     */
    public class theScores
    {
        public int score;

        public Texture2D photo;

    }
    
    void OnGUI(){

        GUI.Box(new Rect(62, 90, datPhoto.width, datPhoto.height), datPhoto);
    }
    
}