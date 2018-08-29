using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipGameController : MonoBehaviour {

	// Use this for initialization

    public enum State {
        S0 = 0 , S1 = 1 ,S2 =2, S3 = 3 
    }

    public static FlipGameController Instance;
    public Sprite DefaultBackSprite; 
    public Step[] steps;
	void Start () {
        Instance = this;

        steps = GetComponentsInChildren<Step>();

        CountDownTimer.Instance.OnTimeup = null;
        CountDownTimer.Instance.OnTimeup += Fail;

        foreach (var ss in steps)
        {
            ss.gameObject.SetActive(false);
        }

        SwitchState(State.S0);
	}

    public void SwitchState(int i )
    {
        SwitchState((State)i);
    }

    void SwitchState (State s){
        foreach(var ss in steps){
            if(ss.name.Contains("Step" + ((int)s).ToString()) ) {
                ss.gameObject.SetActive(true);
            }else{
                ss.gameObject.SetActive(false);
            }
        }
        switch(s){
            
            case State.S0 :
                
                break;

            case State.S1:
                CountDownTimer.Instance.StartCount();
                FlipController.Instance.StartGame();
                break;
            case State.S2:
            case State.S3:
                var ssss = LeanTween.sequence();
                ssss.append(2f);
                ssss.append(() => {
                    SwitchState(State.S0);
                });
                break;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Fail (){
        SwitchState(State.S3);
    }
}
