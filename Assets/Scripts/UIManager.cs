using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    GameObject mortarObject;
    GameObject mortar;
    Transform spawnTransform;
    TMP_InputField horizontalScope;
    TMP_InputField verticalScope;
    TMP_Dropdown chargeDropdown;

    int[] speed = { 261, 216, 179, 135, 76 };
    int currentSpeed;
    int horizontalValue;
    int verticalValue;



    void Start()
    {

        mortarObject = GameObject.Find("MortarObject");
        mortar = GameObject.Find("Mortar");
        spawnTransform = mortar.transform.GetChild(0);
        //Debug.Log(spawnTransform.eulerAngles);
        //Debug.Log(spawnTransform.localEulerAngles);

        //find object by name on scene and get its input component
        //add listener onvaluechange
        horizontalScope = GameObject.Find("HorizontalScope").GetComponent<TMP_InputField>();
        horizontalValue = int.Parse(horizontalScope.text);
        horizontalScope.onEndEdit.AddListener(delegate
        {
            HorizontalChange(horizontalScope);
        });
       
        verticalScope = GameObject.Find("VerticalScope").GetComponent<TMP_InputField>();
        verticalValue = int.Parse(verticalScope.text);
        verticalScope.onEndEdit.AddListener(delegate
        {
            VerticalChange(verticalScope);
        });

        chargeDropdown = GameObject.Find("ChargeType").GetComponent<TMP_Dropdown>();
        currentSpeed = speed[chargeDropdown.value];
        Debug.Log(currentSpeed);
        chargeDropdown.onValueChanged.AddListener(delegate
        {
            ChargeChange(chargeDropdown);
        });




    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HorizontalChange(TMP_InputField change) 
    {

        horizontalValue = int.Parse(change.text);
        if (horizontalValue > 6000) 
        {
            horizontalValue = 6000;
            change.text = "6000";
        }
        if (horizontalValue < 0)
        {
            horizontalValue = 0;
            change.text = "0";
        }
        Debug.Log(horizontalValue);
        mortarObject.transform.localEulerAngles = new Vector3(0, horizontalValue * 0.06f, 0);
    }

    void VerticalChange(TMP_InputField change)
    {
        int previousVerticalValue = verticalValue;
        float angleDifference;
        verticalValue = int.Parse(change.text);
        if (verticalValue > 1000)
        {
            verticalValue = 1000;
            change.text = "1000";
        }
        if (verticalValue < 300)
        {
            verticalValue = 300;
            change.text = "300";
        }
        Debug.Log(verticalValue);
        angleDifference = (previousVerticalValue - verticalValue) * 0.06f;
        mortar.transform.Rotate(0, -angleDifference, 0);
        Debug.Log(spawnTransform.eulerAngles);
    }

    void ChargeChange(TMP_Dropdown change) 
    {
        currentSpeed = speed[change.value];
        Debug.Log(currentSpeed);
    }
}
