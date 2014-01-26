using UnityEngine;
using System.Collections;

public class AnimateFloor : MonoBehaviour {

	public float scrollSpeed = 0.1f;

	void Start () {
		StartCoroutine(DoTextureScroll());
		StartCoroutine(DoBumpScroll());
	}

	private IEnumerator DoTextureScroll() {
		while(true) {
			float offset = Time.time * scrollSpeed;
			Vector2 vct = new Vector2(offset,offset/2);
			renderer.material.SetTextureOffset("_MainTex", vct);
			yield return new WaitForSeconds (0.02f);
			}
	}

	private IEnumerator DoBumpScroll() {
		while(true) {
			float offset = Time.time * scrollSpeed * 0.1f;
			Vector2 vct = new Vector2(-offset,offset/2);
			renderer.material.SetTextureOffset("_BumpMap", vct);

			float bmp_x = Mathf.Sin(vct.x);
			float bmp_y = Mathf.Sin(vct.y);
			Vector2 bmp_vct = new Vector2(bmp_x,bmp_y);
			renderer.material.SetTextureScale ("_MainTex", bmp_vct);
			yield return new WaitForSeconds (0.02f);
		}
	}


/*
	void Update () {
		float offset = Time.time * scrollSpeed;
		Vector2 vct = new Vector2(offset,0);
		renderer.material.SetTextureOffset("_MainTex", vct);
	}
*/
}
