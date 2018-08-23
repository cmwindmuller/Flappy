using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour {

    public GameObject[] flora;
    public int count;
    public float gap,bounds;
    GameObject lastTree;
    GameObject[] _flora;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(lastTree.transform.position.x > gap)
        {
            lastTree = Instantiate( flora[0] );
            lastTree.transform.position = Vector3.zero;
        }
	}
}
