using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtTableChargeChange : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Image image;
    [SerializeField] List<Sprite> sprites;

    void Start()
    {
        //Fetch the Dropdown GameObject
        dropdown = GetComponent<TMP_Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(dropdown);
        });

    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(TMP_Dropdown change)
    {
        image.sprite = sprites[change.value];
        image.SetNativeSize();
    }
}
