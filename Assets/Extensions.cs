using System.Collections;
using System.Collections.Generic;
using System ; 
using UnityEngine;

public static class Extensions{

    public static IEnumerator WaitSecondsThenDo (this MonoBehaviour mono ,float time , Action task ){

//      return mono.StartCoroutine(()=>{
            
            Debug.Log (mono.enabled);
            yield return new WaitForSeconds (time);
            
            task ();
//      });


    }



    public static Color Color  (int r , int g , int b ){
        return new UnityEngine.Color (r / 255f, g / 255f, b / 255f);
    }

    private static System.Random rng = new System.Random () ;

    public static void Shuffle<T>(this List<T> list)  
    {  
        
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  

    }


}
