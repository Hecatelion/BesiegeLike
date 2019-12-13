using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactorBrick : MonoBehaviour
{
	void Start() { }
    
    void Update()
    {
        
    }

	public void SetDirection(Vector3 _dir)
	{
		transform.forward = _dir;
	}
}
