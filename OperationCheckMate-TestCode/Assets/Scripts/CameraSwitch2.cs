using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch2 : MonoBehaviour
{

    public GameObject cameraOne;
    public GameObject cameraTwo;

    public AudioListener cameraOneAudioLis;
   public AudioListener cameraTwoAudioLis;

    // Use this for initialization
    void Start()
    {

        //Get Camera Listeners
        //cameraOneAudioLis = cameraOne.GetComponent<AudioListener>();
        cameraTwoAudioLis = cameraTwo.GetComponent<AudioListener>();

        //Camera Position Set
        //cameraPositionChange(PlayerPrefs.GetInt("CameraPosition"));
    }

    // Update is called once per frame
    void Update()
    {
        //Change Camera Keyboard
        switchCamera();
    }

    //UI JoyStick Method
    //public void cameraPositonM()
    //{
    //    cameraChangeCounter();
    //}

    //Change Camera Keyboard
    void switchCamera()
    {
        //if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        //{
        //    cameraChangeCounter();
        //}
    }

    //Camera Counter
    public void cameraChangeCounter()
    {
        cameraOneAudioLis = cameraOne.GetComponent<AudioListener>();
        cameraTwoAudioLis = cameraTwo.GetComponent<AudioListener>();
        //int cameraPositionCounter = PlayerPrefs.GetInt("CameraPosition");
        //cameraPositionCounter++;
        cameraPositionChange();
    }

    //Camera change Logic
    void cameraPositionChange()
    {
        cameraOne.SetActive(true);
        cameraOneAudioLis.enabled = true;

        cameraTwoAudioLis.enabled = false;
        cameraTwo.SetActive(false);
    }

    public void ResetCamera()
    {
        if (cameraTwo != null)
        {
            cameraTwo.SetActive(true);
            cameraTwoAudioLis.enabled = true;
        }


        //cameraOneAudioLis.enabled = false;
        //cameraOne.SetActive(false);
    }
}