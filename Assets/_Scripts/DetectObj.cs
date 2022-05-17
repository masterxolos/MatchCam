using System.Collections;
using System.Collections.Generic;
using Tabtale.TTPlugins;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DetectObj : MonoBehaviour
{
    [SerializeField] private string camTag;
    [SerializeField] private float rayLength = 500f;
    [SerializeField] private Camera _camera;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private GameObject cameraOverlay;
    [SerializeField] private GameObject heartImage;
    [SerializeField] private int waitingTime = 2;
    private bool firstTime = true;

    [SerializeField] private RotateCamera rotateCameraScript;
    [SerializeField] private GameObject WomenCOntrollers;
    [SerializeField] private GameObject acilacakObje;
    [SerializeField] private float sure;
    
    private void Awake()
    {

          // Initialize CLIK Plugin
          TTPCore.Setup();
          // Your code here
    }

    private void FixedUpdate()
    {
        Ray ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height /2, 0));
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.transform.CompareTag(camTag))
            {
                Debug.Log("hitted to " + camTag);
                GirlFound();
            }
        }
    }

    private void GirlFound()
    {
        if (firstTime == true)
        {
          heartImage.SetActive(true); 
          gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
          StartCoroutine(WaitForHeart());
          firstTime = false;    
        }

    }

    private IEnumerator WaitForHeart()
    {
        yield return new WaitForSeconds(waitingTime);
        cameraOverlay.SetActive(false);
        _camera.GetComponent<Camera>().enabled = true;
        _mainCamera.GetComponent<Camera>().enabled = false;
        _camera.GetComponent<Camera>().targetTexture = null;

        _camera.GetComponent<Camera>().fieldOfView = 60;
        _camera.GetComponent<Transform>().position = new Vector3(15.67f, 1.48f, 25.8f);
        _camera.GetComponent<Transform>().rotation = Quaternion.Euler(5.1f, 268f, 0);
        WomenCOntrollers.SetActive(true);
        heartImage.SetActive(false);
        rotateCameraScript.enabled = false;
        yield return new WaitForSeconds(sure);
        acilacakObje.SetActive(true);
        acilacakObje.GetComponent<SpriteRenderer>().DoFade(100, sure);
    }
}