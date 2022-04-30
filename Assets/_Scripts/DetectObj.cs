using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private void Awake()
    {
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
        _camera.GetComponent<Transform>().position = new Vector3(9.49f, 1.6f, 31.64f);
        _camera.GetComponent<Transform>().rotation = Quaternion.Euler(5.1f, 266f, 0);
        heartImage.SetActive(false);
        rotateCameraScript.enabled = false;

    }
}