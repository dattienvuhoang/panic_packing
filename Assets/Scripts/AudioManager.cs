using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public AudioSource musicSource;
   public AudioSource soundSource;
   public AudioClip musicClip, soundClip;
   public static AudioManager instance;

   private void Awake()
   {
      instance = this;
   }

   private void Start()
   {
      if (GameManager.instance.isMusic)
      {
         PlayMusic();   
      }       
      else
      {
         StopMusic();
      }
   }

   public void PlayMusic()
   {
      Debug.Log("Playing Music");
      musicSource.clip = musicClip;
      musicSource.Play();
   }

   public void StopMusic()
   {
      musicSource.Stop();
   }

   public void PlaySound()
   {
      Debug.Log("Playing Sound");
      soundSource.PlayOneShot(soundClip);
   }

   public void StopSound()
   {
      soundSource.Stop();
   }
}
