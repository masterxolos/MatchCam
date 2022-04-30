using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;


    [SerializeField] private Camera cam;
    private GameObject selectedGameObject = null;


    private void Start()
    {
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(mousePos);
            if(Physics.Raycast(ray, out hit, 10000f, layerMask))
            {
                selectedGameObject = hit.transform.gameObject;
            }

        }
        if(Input.GetMouseButtonUp(0))
        {
            selectedGameObject = null;
        }

        if(selectedGameObject != null)
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
            selectedGameObject.transform.position = new Vector3(worldPos.x, worldPos.y, selectedGameObject.transform.position.z);

        }

    }
}
