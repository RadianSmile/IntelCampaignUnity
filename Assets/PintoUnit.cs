﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
public class PintoUnit : MonoBehaviour {

	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public PintoZone arrivedZone;
    //public string arrivedNum = "";
    Vector3 originPos;
    public void BeginDrag (BaseEventData baseEvent){
        originPos = transform.position; 
        transform.parent = PintoController.Instance.ActiveContainer; 
    }

    public void Drag (BaseEventData baseEvent)
    {
        var p = (PointerEventData)baseEvent;
        transform.position = p.position;
        Debug.Log(p.position);
        Debug.Log(Camera.main.pixelWidth);
        Debug.Log(Camera.main);
    }

    public void EndDrag(BaseEventData baseEvent)
    {
        bool arrived = false;
        
        transform.parent = PintoController.Instance.StartZone.transform;
        var targetZones = PintoController.Instance.pintoZones.Values.ToList();

        var min = 20000f;
        var minIndex = -1;
        for (var i = 0; i < targetZones.Count; i++)
        {
            var targetPos = targetZones[i].transform.position;
            var v = Vector3.Distance(transform.position, targetPos);

            Debug.LogWarning("transform.position" + transform.position);

            v *= PintoController.Instance.ScreenToPixel;
            Debug.LogWarning("VV" + v);

            if (v < 141f && v < min)
            {
                min = v;
                minIndex = i;
            }
        }
        if (minIndex != -1)
        {
            var i = minIndex;
            var targetPos = targetZones[i].transform.position;

            arrived = true;

            if (targetZones[i].ocuppiedPinto != null && targetZones[i].ocuppiedPinto.GetInstanceID() != this.GetInstanceID())
            {
                var originPinto = targetZones[i].ocuppiedPinto;
                var originPintoTargetZone = arrivedZone;

                originPinto.arrivedZone = arrivedZone;
                originPinto.transform.position = originPos;

                if (arrivedZone != null)
                    originPintoTargetZone.ocuppiedPinto = originPinto;
            }
            else{
                if (arrivedZone != null){
                    arrivedZone.ocuppiedPinto = null;
                }
            }

            transform.position = targetPos;
            targetZones[i].ocuppiedPinto = this;
            arrivedZone = targetZones[i];


        }
        if (!arrived){
            transform.position = originPos;    
        }else{

            if (PintoController.Instance.CheckFin()){
                
                CountDownTimer.Instance.Stop();
                var seq = LeanTween.sequence();
                seq.append(2f);
                seq.append(() =>
                {
                    GameController.Instance.SwitchState(2);
                });

            }else{
                
            }
        }

    }
}
