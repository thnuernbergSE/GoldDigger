using UnityEngine;

public class MerchantButtons : MonoBehaviour
{
  public void CloseMerchantUI()
  {
    var merchantPopup = GameObject.Find("MerchantPopup");

    merchantPopup.SetActive(false);

    merchantPopup.transform.GetChild(0).gameObject.SetActive(false);
  }
}
