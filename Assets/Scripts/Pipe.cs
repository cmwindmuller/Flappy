using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {

    public Vector3 movement;
    public GameObject shade;
    float completeX = -2;
    float deathX = -30;
    bool used;

    public void setup(Vector3 localP, Vector3 direction)
    {
        transform.localPosition = localP;
        movement = direction;
    }

    Vector3 adjust(Vector3 position, float height)
    {
        Vector3 p = position;
        p.y = height;
        return p;
    }

	// Update is called once per frame
	void Update () {
        transform.localPosition += movement * Birb.ForwardSpeed * Time.deltaTime;
        if( transform.localPosition.x < deathX )
        {
            kill();
        }
        else if( transform.localPosition.x < completeX && !used )
        {
            used = true;
            FlappyGame.main.pass();
        }
	}

    public void kill()
    {
        Destroy( this.gameObject );
    }
}
