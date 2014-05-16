using UnityEngine;
using System.Collections;

public class SnapShotScript : MonoBehaviour {

    public GameObject GC;
    public Texture2D TempShot;
    public Sprite TempSprite;

	void Start () {
       TempShot = new Texture2D(500, 500);
       GC = GameObject.Find("WorldObject");
       TempShot = GC.GetComponent<GameController>().tempShot;
       //TempSprite = Sprite.Create(TempShot, new Rect(0, 0, TempShot.width, TempShot.height), new Vector2(0.5f, 0.5f));
      // this.GetComponent<SpriteRenderer>().sprite = TempSprite;
     //  GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
      // go.renderer.material.mainTexture = Resources.Load("TempShot", typeof(Texture2D));
	}
	

	void Update () {
	
	}

    void OnGUI() {
 //       GUI.Box(new Rect(62, 90, TempShot.width, TempShot.height), TempShot);
        
    }
}
