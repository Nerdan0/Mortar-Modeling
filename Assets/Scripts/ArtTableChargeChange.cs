using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtTableChargeChange : MonoBehaviour
{
    [SerializeField] TMP_Dropdown m_Dropdown;
    [SerializeField] Image image;
    [SerializeField] List<Sprite> sprites;

    void Start()
    {
        //Fetch the Dropdown GameObject
        m_Dropdown = GetComponent<TMP_Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        m_Dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(m_Dropdown);
        });

    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(TMP_Dropdown change)
    {
        image.sprite = sprites[change.value];
        image.SetNativeSize();
    }
}
