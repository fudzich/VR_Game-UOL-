using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSounds : MonoBehaviour
{
    public static BackGroundSounds bgsounds;

    private void Awake(){
        if(bgsounds != null){
            Destroy(gameObject);
        }
        else{
            bgsounds = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
