using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sparkleSelfDeactivate : MonoBehaviour {

	
    public void DeactivateSelf()
    {
        gameObject.SetActive(false);
    }
}
