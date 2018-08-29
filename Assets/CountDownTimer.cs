using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour {

    // Use this for initialization
    public Image main;
    public List<Sprite> nums;
    public static CountDownTimer Instance; 
    float passedTime;
    float totalTime = 10 ;
    bool started = false;
    private void Awake()
    {
        Instance = this; 
    }
    void Start () {
        
        //StartCount();
	}
    public delegate void Timeup ();
    public Timeup OnTimeup; 

    public void StartCount (){
        started = true; 
        passedTime = 0;
    }
    public void Stop (){
        started = false;
    }

    // Update is called once per frame
    void Update () {
        if (started)
        {
            passedTime += Time.deltaTime;
            var remain = totalTime - passedTime;
            if (remain <= 0)
            {
                if (OnTimeup != null)
                {
                    OnTimeup();
                    started = false;
                }

            }
            else
            {
                UpdateImage(remain);
            }


        }

           

	}
    void UpdateImage (float remain){

        int index = Mathf.FloorToInt(remain);

        main.sprite = nums[index];

    }

}
