using UnityEngine;
using System.Collections;

public class Presentation : MonoBehaviour {
	private bool left 	= true;
	private bool rotated= false;
	private bool move	= false;
	
	public float speed 	= 0.35f;
	public int maxX		= 16;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x<maxX && left){
			transform.Translate(Vector3.left * (Time.deltaTime*speed));
		}
		else if(!rotated){
			left = false;
			transform.Rotate(0, 180, 0, Space.World);
			rotated = true;
		}
		else if(transform.position.x>0){
			transform.Translate(Vector3.left * (Time.deltaTime*speed));
		}
		else if(!move){
			transform.Rotate(0, -90, 0, Space.World);
			move = true;
		}    
	}
}
