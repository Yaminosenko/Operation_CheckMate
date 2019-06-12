using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_move : MonoBehaviour {

    public float speed;
    [SerializeField] float fireRate;
    [SerializeField] GameObject _muzzleFX;
    [SerializeField] GameObject _hitFX;
    bool _hit = false;

    void Start() {
        /*if (_muzzleFX != null) {
            var muzzleVFX = Instantiate (_muzzleFX, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
        }*/
    }

    private void OnEnable()
    {
        GameObject muzzleFXClone = Instantiate(_muzzleFX, transform.position, transform.rotation);
        Destroy(muzzleFXClone, 1f);

        
    }

    void Update() {
        //if (speed != 0) {
            transform.position += transform.forward * (speed * Time.deltaTime);


      
        /*} else {
            Debug.Log("No Speed");
        }*/
    }

    //    void OnCollisionEnter(Collision co) {
    //        if (_hit == false)
    //        {
    //                Debug.Log(co.gameObject);
    //            if(co.transform.gameObject.tag == "player")
    //            {
    //;                GetComponentInChildren<Rigidbody>().Sleep();
    //                GetComponentInChildren<BoxCollider>().enabled = false;
    //                speed = 0;


    //                GameObject hitFXClone = Instantiate(_hitFX, transform.position, transform.rotation);
    //                Destroy(hitFXClone, 1f);
    //                Destroy(gameObject, 1f);
    //                _hit = true;
    //            }

    //        }
    //    }
    private void OnTriggerEnter(Collider other)
    {
        if (_hit == false)
        {
            Debug.Log(other.gameObject);
            Debug.Log(other.gameObject.tag);
            if (other.gameObject.tag == "Player")
            {
                GetComponentInChildren<Rigidbody>().Sleep();
                GetComponentInChildren<BoxCollider>().enabled = false;
                speed = 0;


                GameObject hitFXClone = Instantiate(_hitFX, transform.position, transform.rotation);
                Destroy(hitFXClone, 1f);
                Destroy(gameObject, 1f);
                _hit = true;
            }

        }
    }

    private void Explosion()
    {

    }
}
