using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public Dictionary<string, bool> keyList = new Dictionary <string, bool>();

    // Use this for initialization
    void Start ()
    {
        keyList.Add("playerHasGreenKey", false);
        keyList.Add("playerHasRedKey", false);
        keyList.Add("playerHasBlueKey", false);
    }
	
}
