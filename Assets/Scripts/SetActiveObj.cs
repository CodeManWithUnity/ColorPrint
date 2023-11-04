using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveObj : MonoBehaviour
{
    public void SetObjActive() 
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
    public void ResetTransform() 
    {
        this.gameObject.transform.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    }
}
