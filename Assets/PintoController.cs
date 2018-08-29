using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class PintoController : MonoBehaviour
{


    // Use this for initialization

    public Transform StartZone;
    public Transform TargetZone;

    public Dictionary<string, Vector3> startZonePonits;
    public Dictionary<string, PintoZone> pintoZones;
    public Dictionary<string, PintoUnit> pintos;

    public Transform ActiveContainer;

    bool init = false;
    public static PintoController Instance;

    public float ScreenToPixel;

    private void Awake()
    {
        Instance = this;
        ScreenToPixel = 1920 / Screen.width;


        //startZonePonits;
        pintos = new Dictionary<string, PintoUnit>();
        startZonePonits = new Dictionary<string, Vector3>();
        pintoZones = new Dictionary<string, PintoZone>();

        foreach (var t in StartZone.GetComponentsInChildren<PintoUnit>())
        {
            pintos.Add(t.name, t);
            t.arrivedZone = null;
            startZonePonits.Add(t.name, t.transform.position);
        }
        foreach (var t in TargetZone.GetComponentsInChildren<PintoZone>())
        {
            t.ocuppiedPinto = null;
            pintoZones.Add(t.name, t);
        }
        init = true;
    }

    void Start()
    {


        StartGame();
    }

    public void StartGame()
    {

        foreach (var t in pintos.Values)
        {

            t.arrivedZone = null;

        }
        foreach (var t in pintoZones.Values)
        {
            t.ocuppiedPinto = null;

        }

        System.Random rng = new System.Random();

        var thePintos = pintos.Values.ToList();
        var positions = startZonePonits.Values.ToList();
        positions.Shuffle();

        //Debug.Log(randomPintoPosition.Count + " " + startZonePonits.Count);

        for (var i = 0; i < positions.Count; i++)
        {
            //var randP = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 1);
            Debug.Log("randomPintoPosition[i].position" + positions[i]);
            thePintos[i].transform.position = positions[i]; //+ randP ;

        }

    }
    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckFin()
    {
        foreach (var p in pintoZones.Values)
        {
            if (p.ocuppiedPinto == null)
            {
                return false;
            }
            if (p.name != p.ocuppiedPinto.name)
            {
                return false;
            }
        }
        return true;
    }
}

