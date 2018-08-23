using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class FlappyGame : MonoBehaviour {

    public static FlappyGame main;
    public GameObject pipePrefab;

    public GameObject menu;
    public Text scoreNowText,scoreBestText;
    int _scoreNow,_scoreBest;
    public static int score;
    bool inGame;

    const string FLAP_KEY = "Fl4ppyK3y";

	void Awake () {
        main = this;
        _scoreBest = 0;
        if(PlayerPrefs.HasKey(FLAP_KEY))
        {
            setBest( PlayerPrefs.GetInt( FLAP_KEY ) );
        }
		InvokeRepeating ( "pipeMake", 2f, 1.5f );
	}

    void setBest(int best, bool save = false)
    {
        if( best < _scoreBest ) return;
        _scoreBest = best;
        if(save)PlayerPrefs.SetInt( FLAP_KEY, _scoreBest );
        scoreBestText.text = "Best:" + _scoreBest.ToString();
    }

    void Update()
    {
        scoreNowText.text = _scoreNow.ToString();
    }

    void pipeMake()
	{
        if(inGame)
        {
            GameObject pipe = Instantiate (pipePrefab,transform);
            pipe.GetComponent<Pipe>().setup( new Vector3( 20, Random.value * 4 - 1, 0 ), -Vector3.right );
        }
	}

    public void toggle(bool active)
    {
        if(active)
        {
            _scoreNow = score = 0;
        }
        else
        {
            setBest( _scoreNow, true );
            score = 0;
        }
        inGame = active;
        menu.SetActive( !active );
    }

    public void pass()
    {
        if( inGame )
        {
            _scoreNow++;
            score = _scoreNow;
            if(_scoreNow > _scoreBest)
            {
                setBest( _scoreNow );
            }
        }
    }

    public static bool isIdle()
    {
        return !main.inGame;
    }
}
