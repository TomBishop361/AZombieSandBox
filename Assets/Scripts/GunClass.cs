using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunClass
{
    public int Damage;
    public float RateOfFire;
    public float Ammo;
    public AudioClip Sound;
    public bool Automatic;
    public float ReloadTime;
    public float currentAmmo;


    public GunClass(int Dmg,float rof, AudioClip sound, int ammo, bool automatic, float reloadtime, float currentammo)
    {
        Damage = Dmg;
        RateOfFire = rof;
        Sound = sound;
        Ammo = ammo;
        Automatic = automatic;
        ReloadTime = reloadtime;
        currentAmmo = currentammo;
    }



    //Constructor
    public GunClass(){
        Damage = 30;
        RateOfFire = 1f;
        Sound = null;
        Automatic = false;
        Ammo = 15;
    }


}
   
