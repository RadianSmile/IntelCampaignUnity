using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipUnit : MonoBehaviour {

    // Use this for initialization

    public Sprite backImage;
    Sprite frontSprite;
    Image image; 
	void Start () {
        image = GetComponent<Image>();
        frontSprite = image.sprite;
        transform.localScale = Vector3.one;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    public void Reset (){
        flipped = false;
        transform.eulerAngles = Vector3.zero;
        backImage = null;
        if (frontSprite !=null)
        image.sprite = frontSprite;
    }
    public bool flipped = false;
    public void Flip (){
        
        if (flipped) return;
        if (FlipController.Instance.finished) return;
        flipped = true;
        LeanTween.rotateY(gameObject, 1080 + 180, .5f).setOnUpdate((float ang) =>
        {
            ang = transform.eulerAngles.y;
            if (ang % 360 > 90 && ang % 360 <= 270)
            {

                if (backImage != null)

                {
                    if (image.sprite.GetInstanceID() != backImage.GetInstanceID())
                        image.sprite = backImage;
                }
                else
                {
                    if (image.sprite.GetInstanceID() != FlipController.Instance.DefaultBackSprite.GetInstanceID())
                        image.sprite = FlipController.Instance.DefaultBackSprite;
                    
                }
            }
            else
            {
                if (image.sprite.GetInstanceID() != frontSprite.GetInstanceID())
                {
                    image.sprite = frontSprite;

                }
            }
        }).setOnComplete(() =>
        {
           
                
            FlipController.Instance.CheckFin();
        });
    }
    public LTDescr scaleUP(){
        return LeanTween.scale(gameObject, Vector3.one * 1.1f, .4f).setLoopPingPong(1);
    }

}
