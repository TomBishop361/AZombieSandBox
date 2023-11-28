using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class GunClass
{
    public int Damage = 30;
    public float RateOfFire = 1f;
    public int Ammo = 0;
    public AudioClip Sound = null;
    public bool Automatic = false;
    public float ReloadTime = 0;
    [HideInInspector] public int currentAmmo = 0;   

}
   
