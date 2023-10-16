using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    [SerializeField] //assing particles to this
    private ParticleSystem particles;

    private Vector3 mousePos;

    // Start is called before the first frame update
    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // click left mouse button to turn particles on
        // and place themm at mouse pos
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            particles.Play();
            particles.transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);
        }
    }


}
