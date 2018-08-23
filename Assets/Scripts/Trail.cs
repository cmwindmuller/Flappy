using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {

    TrailRenderer tr;
    Vector3[] pos;
    public float spread;

    void Start () {
        tr = GetComponent<TrailRenderer>();

    }
	
	void Update ()
    {
        pos = new Vector3[tr.positionCount];
        tr.GetPositions( pos );
        for(int i=0;i<pos.Length;i++ )
        {
            float a = ( 1 - ( i / ( float ) pos.Length ) );
            pos[i].x = transform.position.x + a * spread;
        }
        tr.SetPositions( pos );
	}
}
