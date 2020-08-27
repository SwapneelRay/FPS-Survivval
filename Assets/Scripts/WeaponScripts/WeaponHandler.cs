using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}
public enum WeaponFireType
{
    SINGLEFIRE,
    MULTIPLEFIRE
}
public enum WeaponBulletType
{
    BULLET,
    ARROW,
    SPEAR,
    NONE
}
public class WeaponHandler : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    public WeaponAim weapon_aim;
    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private AudioSource shootSound, reload_Sound;

    public WeaponFireType FireType;

    public WeaponBulletType BulletType;

    public GameObject attak_Point;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void ShootAnimation()
    {
        anim.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }

    public void Aim(bool canAim)
    {
        anim.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }

    public void Turn_On_MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    public void Turn_Off_MuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }
    void Play_ShootSound()
    {
        shootSound.Play();
    }

    void Play_ReloadSound()
    {
        reload_Sound.Play();
    }

    void Turn_On_AttackPoint()
    {
        attak_Point.SetActive(true);
    }
    void Turn_Off_AttackPoint()
    {
        if (attak_Point.activeInHierarchy)
        {
            attak_Point.SetActive(false);
        }

    }

}
