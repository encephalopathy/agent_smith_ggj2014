using UnityEngine;
using System.Collections;

public class WhipCollide : MonoBehaviour {

	public GameObject whipOwner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider datCollision){
		if(datCollision.collider.tag == "Convertable"){
			whipOwner = this.transform.root.gameObject;
			Debug.Log(whipOwner);
		}
	}

}
