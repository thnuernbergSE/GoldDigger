using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwitching : MonoBehaviour
{

    private int selectedItem = 0;
    // Use this for initialization
    void Start()
    {
        SelectItem();
    }

    // Update is called once per frame
    void Update()
    {
        HandleItemSwitch();
    }

    void SelectItem()
    {
        int i = 0;
        foreach (Transform item in transform)
        {
            if (i == selectedItem)
            {
                if (i == selectedItem)
                {
                    item.gameObject.SetActive(true);
                }
                else
                {
                    item.gameObject.SetActive(false);
                }
                i++;
            }
        }
    }

    void HandleItemSwitch()
    {
        int previousSelectedItem = selectedItem;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedItem >= transform.childCount - 1)
            {
                selectedItem = 0;
            }
            else
            {
                selectedItem++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedItem <= 0)
            {
                selectedItem = transform.childCount - 1;
            }
            else
            {
                selectedItem--;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedItem = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedItem = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 3)
        {
            selectedItem = 2;
        }

        if (previousSelectedItem != selectedItem)
        {
            SelectItem();
        }
    }
}