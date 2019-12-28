using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// always emits eletric power
public class CoreBrick : MonoBehaviour
{
    void Start()
    {
		gameObject.AddComponent<Emitter>().isEmittingPower = true;
    }
}
