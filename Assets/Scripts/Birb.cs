using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MoveInfo
{
    public float x,y,jump;
}
public class Birb : MonoBehaviour
{
    public static float ForwardSpeed;
    public MoveInfo gameInfo;
    public MoveInfo[] rules;

    public GameObject deathPrefab;
    Vector3 startPosition;
    bool isDead;

    public GameObject body,wing,tail,eye;
    public Vector3 spanAngle;
    Vector2 baseAngle;
    Quaternion randomEye;

    void Start ()
    {
        baseAngle.x = wing.transform.localEulerAngles.x;
        baseAngle.y = tail.transform.localEulerAngles.z;
        startPosition = transform.localPosition;
        death();
        InvokeRepeating( "wanderEye", 0, 0.25f );
        gameInfo = rules[Mathf.FloorToInt( rules.Length * Random.value )];
    }

    bool activeBtn()
    {
        return Input.GetButtonDown( "Jump" ) || Input.GetButtonDown( "Fire1" );
    }

    void wanderEye()
    {
        randomEye = Quaternion.RotateTowards( Quaternion.identity, Random.rotationUniform, spanAngle.z );
    }
	
	void Update ()
    {
        Physics.gravity = Vector3.down * gameInfo.y;
        ForwardSpeed = gameInfo.x;
        Vector3 wingEuler = wing.transform.localEulerAngles;
        wingEuler.x = Mathf.Lerp( wingEuler.x, baseAngle.x + Mathf.Sin(Time.time * 7) * 10, 0.1f );
        if( !isDead && activeBtn() )
        {
            if( FlappyGame.isIdle() )
            {
                FlappyGame.main.toggle( true );
                rebirth( true );
            }
            wingEuler.x = baseAngle.x + spanAngle.x;
            GetComponent<Rigidbody>().velocity = Vector3.up * gameInfo.jump;
            GetComponent<AudioSource>().Play();
            GetComponent<ParticleSystem>().Play();
        }
        wing.transform.localEulerAngles = wingEuler;
        tail.transform.localEulerAngles = new Vector3( 0, 0, baseAngle.y + Mathf.Sin( Time.time * 6.28f * 3 ) * spanAngle.y );
        eye.transform.localRotation = Quaternion.Slerp( eye.transform.localRotation, randomEye, Time.deltaTime );
        body.transform.right = Vector3.Lerp( body.transform.right, new Vector3( gameInfo.x, GetComponent<Rigidbody>().velocity.y, 0 ), 0.1f );
    }

    void rebirth()
    {
        rebirth( false );
    }

    void rebirth(bool sudo=false)
    {
        if( sudo )
        {
            GetComponent<Collider>().enabled = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }
        isDead = false;
        toggleRender( true );
        transform.position = startPosition;
        body.transform.localEulerAngles = Vector3.zero;
    }

    void death(bool sudo=false)
    {
        if( sudo )
        {
            toggleRender( false );
            GameObject fx = Instantiate( deathPrefab );
            fx.transform.position = transform.position;
            isDead = true;
            Invoke( "rebirth", 5 );
        }
        FlappyGame.main.toggle( false );
        body.transform.localEulerAngles = Vector3.zero;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void toggleRender( bool visible )
    {
        body.SetActive( visible );
        GetComponent<TrailRenderer>().enabled = visible;
    }

    private void OnCollisionEnter( Collision collision )
    {
        death( true );
    }
}
