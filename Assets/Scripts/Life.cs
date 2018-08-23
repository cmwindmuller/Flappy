using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

    public float life;
    float death;
    
	void Awake () {
        OnLife();
        Invoke( "OnDeath", life );
	}

    void OnLife ()
    {
        death = Time.time + life;
    }

    void OnDeath()
    {
        Destroy( this.gameObject );
    }

    public float getLife()
    {
        return Mathf.Clamp01( Mathf.Abs( death - Time.time ) / life );
    }
}
