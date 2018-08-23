using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour {

    public GameObject sun;
    public Vector2 angle;
    float alpha;
    public float impress = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        alpha = Mathf.Lerp( alpha, FlappyGame.score / impress, 0.01f );
        Vector3 sunEuler = sun.transform.localEulerAngles;
        sunEuler.x = Mathf.Lerp( angle.x, angle.y, alpha );
        sun.transform.localEulerAngles = sunEuler;
	}
}
