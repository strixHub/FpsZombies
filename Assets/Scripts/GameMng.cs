using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMng : MonoBehaviour
{
    // Start is called before the first frame update
    public static Dictionary <string, List<string[]>> data;
    public GameObject cloneObj;    
    
    void Start()
    {
        data = new Dictionary<string, List<string[]>>();
        SaveManager.LoadData(cloneObj);
    }

}
