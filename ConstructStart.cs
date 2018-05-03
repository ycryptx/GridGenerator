using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructStart : MonoBehaviour {

	//have an array here
	public GameObject[] array;
	public Vector3 pos;
//	private int x;		//apply collider on every second object
	private bool checkDelete;	//checks we're not on first object to delete based on collider
	public Quaternion rot;


	// Use this for initialization
	void Start () {
		//array = new string[] {"A380","basketball","engineer","bb8", "buildings", "Canape", "church", "Cigarrete", "eb_house_plant_02","Fantasy yatch","ghetto","Jasubot-Pro01","microwave","Pizza","sheep","spino","computer","suv","TARDIS-FIGR_mkIII_station","Treadmill FBX","Tree","uh60","wood_house"};
		array = Resources.LoadAll<GameObject>("");
		pos = new Vector3(0, 0, 0);
		rot = new Quaternion (0, 0, 0, 0);
//		x = 0;
//		spawnRandom ();
//		spawnRandom ();

		for (int i = array.Length - 1; i > 0; i--)
		{
			int swapIndex = Random.Range(0,i + 1);
			spawnRandom (swapIndex);
			GameObject tmp = array[i];
			array[i] = array[swapIndex];
			array[swapIndex] = tmp;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//if viewer is within the Collider zone, call spawnRandom

	} 

	void spawnRandom (int rand) {
		Random.InitState(System.DateTime.Now.Millisecond);
//		int randomIndex = Random.Range(0,array.Length);
//		Debug.Log (rand);
//		Debug.Log (array[rand]);

		pos.z = pos.z + Random.Range (100, 200);
		pos.x = pos.x + Random.Range (-200, 200);
		GameObject obj1 = (GameObject)Instantiate(array[rand], pos, rot);
		SphereCollider sc = obj1.AddComponent<SphereCollider>() as SphereCollider;
		sc.center = Vector3.zero;
		sc.radius = 10;
		sc.isTrigger = true;

	

//		if (x % 2 == 0) {
//			x = x + 1;
//			SphereCollider sc = obj1.AddComponent<SphereCollider>() as SphereCollider;
//			sc.center = Vector3.zero;
//			sc.radius = 3;
//			sc.isTrigger = true;
//		}




	}

//	void OnCollisionEnter(Collision col){
//		if (Random.Range (0, 2) % 2 == 0) {
////			Kino.AnalogGlitch glitch = new Kino.AnalogGlitch. (Camera.main);
//			Kino.AnalogGlitch glitch = UnityStandardAssets.Characters.FirstPerson.FirstPersonController.
//				gameObject.AddComponent<Kino.AnalogGlitch>() as Kino.AnalogGlitch;
//
//		} 
//
//	}




}

