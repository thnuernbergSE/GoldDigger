using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmithButtons : MonoBehaviour
{ 
	// Use this for initialization
  public void UpgradesClick()
  {
    var smithPopup = GameObject.Find("SmithPopup");
    smithPopup.transform.GetChild(0).gameObject.SetActive(true);
    smithPopup.transform.GetChild(2).gameObject.SetActive(false);
  }

  public void SellItemsClick()
  {
    var smithPopup = GameObject.Find("SmithPopup");
    smithPopup.transform.GetChild(1).gameObject.SetActive(true);
    smithPopup.transform.GetChild(2).gameObject.SetActive(false);
  }

  public void CloseSmithUI()
  {
    var smithPopup = GameObject.Find("SmithPopup");
    smithPopup.SetActive(false);

    smithPopup.transform.GetChild(0).gameObject.SetActive(false);
    smithPopup.transform.GetChild(1).gameObject.SetActive(false);
    smithPopup.transform.GetChild(2).gameObject.SetActive(true);
  }
}
