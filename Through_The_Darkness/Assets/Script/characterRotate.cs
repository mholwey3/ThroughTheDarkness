using UnityEngine;
using System.Collections;

public class characterRotate : MonoBehaviour {

	public GameObject frog;
	
	
	
	private Rect FpsRect ;
	private string frpString;
	
	private GameObject instanceObj;
	public GameObject[] gameObjArray=new GameObject[15];
	public AnimationClip[] AniList  = new AnimationClip[15];
	
	float minimum = 2.0f;
	float maximum = 50.0f;
	float touchNum = 0f;
	string touchDirection ="forward"; 
	private GameObject Wooden_doll;
	
	// Use this for initialization
	void Start () {
		
		//frog.animation["dragon_03_ani01"].blendMode=AnimationBlendMode.Blend;
		//frog.animation["dragon_03_ani02"].blendMode=AnimationBlendMode.Blend;
		//Debug.Log(frog.GetComponent("dragon_03_ani01"));
		
		//Instantiate(gameObjArray[0], gameObjArray[0].transform.position, gameObjArray[0].transform.rotation);
	}
	
 void OnGUI() {
	  if (GUI.Button(new Rect(20, 20, 70, 40),"Idle")){
		 frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Idle");
	  }
	    if (GUI.Button(new Rect(90, 20, 70, 40),"Walk")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Walk");
	  }
	     if (GUI.Button(new Rect(160, 20, 70, 40),"Attack")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("Attack");
	  } 
		if (GUI.Button(new Rect(230, 20, 70, 40),"Damage")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("Damage");
	  }  
		if (GUI.Button(new Rect(300, 20, 70, 40),"Knock_back")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("Knock_back");
	  } 
		if (GUI.Button(new Rect(370, 20, 70, 40),"Damage_sky")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("Damage_sky");
	  } 
		if (GUI.Button(new Rect(440, 20, 70, 40),"Dead")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("Dead");
	  } 
		if (GUI.Button(new Rect(510, 20, 70, 40),"Stun")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Loop;
		  	frog.GetComponent<Animation>().CrossFade("Stun");
	  }		
			if (GUI.Button(new Rect(580, 20, 70, 40),"Skill")){
		  frog.GetComponent<Animation>().wrapMode= WrapMode.Once;
		  	frog.GetComponent<Animation>().CrossFade("Skill");
	  } 
//		if (GUI.Button(new Rect(20, 65, 70, 40),"Down")){
//		  frog.animation.wrapMode= WrapMode.Once;
//		  	frog.animation.CrossFade("Down");
//	  } 
//		if (GUI.Button(new Rect(90, 65, 70, 40),"Up")){
//		  frog.animation.wrapMode= WrapMode.Once;
//		  	frog.animation.CrossFade("Up");
//	  } 
//		if (GUI.Button(new Rect(160, 65, 70, 40),"Stun")){
//		  frog.animation.wrapMode= WrapMode.Loop;
//		  	frog.animation.CrossFade("Stun");
//	  }
//	    if (GUI.Button(new Rect(230, 65, 70, 40),"Dead")){
//		  frog.animation.wrapMode= WrapMode.Once;
//		  	frog.animation.CrossFade("Dead");
//	  }	
//	    if (GUI.Button(new Rect(300, 65, 70, 40),"")){
//		  frog.animation.wrapMode= WrapMode.Once;
//		  	frog.animation.CrossFade("");
//	  }
//	    if (GUI.Button(new Rect(370, 65, 70, 40),"")){
//		  frog.animation.wrapMode= WrapMode.Once;
//		  	frog.animation.CrossFade("");			
//	  } 

//				if (GUI.Button(new Rect(580, 470, 120, 40),"Ver ")){
//		  frog.animation.wrapMode= WrapMode.Loop;
//		  	frog.animation.CrossFade("Idle");
//	  }
 }
	
	// Update is called once per frame
	void Update () {
		
		//if(Input.GetMouseButtonDown(0)){
		
			//touchNum++;
			//touchDirection="forward";
		 // transform.position = new Vector3(0, 0,Mathf.Lerp(minimum, maximum, Time.time));
			//Debug.Log("touchNum=="+touchNum);
		//}
		/*
		if(touchDirection=="forward"){
			if(Input.touchCount>){
				touchDirection="back";
			}
		}
	*/
		 
		//transform.position = Vector3(Mathf.Lerp(minimum, maximum, Time.time), 0, 0);
	if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
		//frog.transform.Rotate(Vector3.up * Time.deltaTime*30);
	}
	
}
