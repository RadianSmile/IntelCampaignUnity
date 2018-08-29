using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class FlipController : MonoBehaviour
{


    // Use this for initialization

    public Transform Zone;
    public Sprite DefaultBackSprite;
    public List<Sprite> backSprites;
    public Dictionary<string, FlipUnit> flips;

    public Transform ActiveContainer;

    bool init = false;
    public static FlipController Instance;

    public float ScreenToPixel;

    private void Awake()
    {
        Instance = this;
        ScreenToPixel = 1920 / Camera.main.pixelWidth;

        flips = new Dictionary<string, FlipUnit>();

        foreach (var t in Zone.GetComponentsInChildren<FlipUnit>())
        {
            flips.Add(t.name, t);
        }

        init = true;
    }

    void Start()
    {


        StartGame();
    }

    public void StartGame()
    {
        finished = false;
        backSprites.Shuffle();
        List<int> indexes = Enumerable.Range(0, 9).ToList();
        indexes.Shuffle();            
        var flipss = flips.Values.ToList();

        for (var i = 0; i < flipss.Count; i++)
        {
            flipss[i].Reset();

        }
        for (var i = 0; i < backSprites.Count; i++){
            Debug.Log("indexes" + indexes.Count + " " + i);
            var ii = indexes[i];
            flipss[ii].backImage = backSprites[i];
        }



    }
    // Update is called once per frame
    void Update()
    {

    }

    public bool finished = false;
    public bool CheckFin()
    {
        int count = 0;
        foreach (var p in flips.Values.ToList())
        {
            if(p.flipped && p.backImage !=null){
                count++; 
            }
        }
        if (!finished && count == backSprites.Count){
            finished = true;
            CountDownTimer.Instance.Stop();
            var seq = LeanTween.sequence();

            foreach (var p in flips.Values.ToList())
            {
                if (p.flipped && p.backImage != null)
                {
                    //LeanTween.cancel(p.gameObject);
                    p.scaleUP();
                }
            }
            seq.append(3f);
            seq.append(() =>
            {
                FlipGameController.Instance.SwitchState(2);
            });

            return true;    
        }else{
            return false;
        }


    }
}
