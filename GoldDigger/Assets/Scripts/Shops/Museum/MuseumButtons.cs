using UnityEngine;

public class MuseumButtons : MonoBehaviour
{
  public void CloseMuseumUI()
  {
    var museumUI = GameObject.Find("MuseumPopup");
    museumUI.SetActive(false);
  }
}
