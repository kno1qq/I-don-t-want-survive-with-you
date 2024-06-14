using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public AudioClip bgm;
    public AudioClip seDamage;
    public AudioClip seAttack;
    public AudioClip seWalk;   
    public AudioClip seBossHit;
    public AudioClip seBossAttack;

    [SerializeField]List<AudioSource> audios=new List<AudioSource>();

    private void Awake()
    {
        for(int i=0;i<7;i++) 
        {
            var audio = this.gameObject.AddComponent<AudioSource>();
            audios.Add(audio);
        }
    }


    public void Play(int index,string name,bool isloop)
    {
        var clip=GetAudioClip(name);
        if(clip!=null){
            var audio=audios[index];
            audio.clip = clip;
            audio.loop = isloop;
            audio.Play();
        }
    }

    AudioClip GetAudioClip (string name)
    {
        switch(name){
            case "bgm":
                return bgm;

            case "seDamage":
                return seDamage;

            case "seAttack":
                return seAttack;        

            case "seBossHit":
                return seBossHit;

            case "seBossAttack":
                return seBossAttack;

            case "seWalk":
                return seWalk;
        }
        return null;
       
    }
}
