using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBrick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		GetComponent<ConductiveBrick>().isConducting = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
