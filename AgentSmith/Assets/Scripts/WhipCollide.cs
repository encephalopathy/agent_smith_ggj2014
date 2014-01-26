using UnityEngine;
using System.Collections;

public class WhipCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

 	void OnTriggerEnter(Collider datCollision){
		if(datCollision.collider.tag == "Convertable"){
			Mesh whipOwnerMesh = transform.root.GetComponent<MeshFilter>().mesh;
			MeshFilter collidedObjectMeshFilter = datCollision.GetComponent<MeshFilter>();

			if (collidedObjectMeshFilter.mesh != whipOwnerMesh) {
				collidedObjectMeshFilter.mesh = whipOwnerMesh;
				datCollision.name= transform.root.name;

			}
		}
	}
}
