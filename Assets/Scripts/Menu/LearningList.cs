using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningList : MonoBehaviour
{
    
    public GameObject elementRow;
    public GameObject list;

    public void AddRow(){
        GameObject clone = Instantiate(elementRow, list.transform, true);
        clone.SetActive(true);
    }

    public void DeleteThisRow(){
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Destroy(button.transform.parent.gameObject);
    }
}
