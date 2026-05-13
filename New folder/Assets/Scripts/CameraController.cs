using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       void LateUpdate()
{
   void LateUpdate()
{
    // Cilj je uvijek: trenutna pozicija igrača + početni razmak
    Vector3 targetPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);
    
    // Direktno postavi poziciju bez Lerpa za test, da vidiš radi li
    transform.position = targetPosition; 
}
}
    }
}
