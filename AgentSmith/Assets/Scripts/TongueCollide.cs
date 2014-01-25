using UnityEngine;
using System.Collections;

public class TongueCollide : MonoBehaviour {

	public GameObject whipOwner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider datCollision){
		if(datCollision.collider.tag == "Player"){
			whipOwner = this.transform.root.gameObject;
		}
	}

}
