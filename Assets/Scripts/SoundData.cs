using System;
using System.Collections.Generic;
using UnityEngine;

// by nt.Dev93
namespace ntDev
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObjects/SoundData")]
    public class SoundData : ScriptableObject
    {
        public AudioClip SoundClick;
        public AudioClip MusicBg;
        public List<AudioClip> Attack = new List<AudioClip>();
        public List<AudioClip> EnemyDie;
        public List<AudioClip> EnemyGetHit;
        public AudioClip LevelComplete;
        public AudioClip Lose;
        public AudioClip LuckyRoll;
        public AudioClip PinkSmoke;
        public AudioClip PlayerGetHit;
        public AudioClip OpenChest;
        public AudioClip Drop;
        public AudioClip OpenGift;
        public AudioClip Merge;
        public AudioClip MergeMax;
        public AudioClip Pick;
        public AudioClip UnlockSkill;


     

    }
}