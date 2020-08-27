using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerattack : MonoBehaviour
{
    private WeaponManager weapon_manager;

    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage = 20f;

    private Animator zoomCameraAnim;
    private bool Zoomed;
    private GameObject crosshair;
    private Camera mainCam;

    private bool is_Aiming;

    [SerializeField]
    private GameObject Arrow_Prefabs, Spear_Prefabs;
    [SerializeField]
    private Transform arrow_Bow_Start_position;

    private void Awake()
    {
        weapon_manager = GetComponent<WeaponManager>();
        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);

        mainCam = Camera.main;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         weaponShoot();

        

        ZoomInAndOut();
    }
   
    void weaponShoot()
    {
        if(weapon_manager.GetCurrentSelectedWeapon().FireType==WeaponFireType.MULTIPLEFIRE)
        {
            if(Input.GetMouseButton(0) && Time.time >nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f/fireRate;
                weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();
                BulletFired();
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (weapon_manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();
                }
                if (weapon_manager.GetCurrentSelectedWeapon().BulletType == WeaponBulletType.BULLET)
                {
                    weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();

                    BulletFired();

                }
                else
                {
                    if(is_Aiming)
                    {
                        weapon_manager.GetCurrentSelectedWeapon().ShootAnimation();

                        if (weapon_manager.GetCurrentSelectedWeapon().BulletType == WeaponBulletType.ARROW)
                        {
                            ThrowArrrowOrSpear(true);

                        }
                        else if(weapon_manager.GetCurrentSelectedWeapon().BulletType == WeaponBulletType.SPEAR)
                        {
                            ThrowArrrowOrSpear(false);
                        }

                    }
                }
            }
        }
    }
    void ZoomInAndOut()
    {
        if(weapon_manager.GetCurrentSelectedWeapon().weapon_aim==WeaponAim.AIM)
        {
            if(Input.GetMouseButtonDown(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                crosshair.SetActive(false);

              
            }
            if(Input.GetMouseButtonUp(1))
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                crosshair.SetActive(true);
            }
        }

        if(weapon_manager.GetCurrentSelectedWeapon().weapon_aim==WeaponAim.SELF_AIM)
        {
            if(Input.GetMouseButtonDown(1))
            {
                weapon_manager.GetCurrentSelectedWeapon().Aim(true);
                is_Aiming = true;


            }
            if(Input.GetMouseButtonUp(1))
            {
                weapon_manager.GetCurrentSelectedWeapon().Aim(false);
                is_Aiming = false;


            }


        }

    }
    void ThrowArrrowOrSpear(bool throwArrow)
    {
        if(throwArrow)
        {
            GameObject arrow = Instantiate(Arrow_Prefabs);
            arrow.transform.position = arrow_Bow_Start_position.position;
            arrow.GetComponent<ArrowBowScript>().Launch(mainCam);
        }
        else
        {
            GameObject spear = Instantiate(Spear_Prefabs);
            spear.transform.position = arrow_Bow_Start_position.position;
            spear.GetComponent<ArrowBowScript>().Launch(mainCam);
        }
    }


    void BulletFired()
    {
        RaycastHit hit;
        if(Physics.Raycast(mainCam.transform.position,mainCam.transform.forward, out hit))
        {
            print("We hit"+ hit.transform.gameObject.name);
            if(hit.transform.tag == Tags.ENEMY_TAG)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
        }
    }
    
}
