using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject shellPrefab;
    GameObject mortarObject;
    GameObject mortar;
    Transform spawnTransform;
    TMP_InputField horizontalScope;
    TMP_InputField verticalScope;
    TMP_Dropdown chargeDropdown;
    Button shotButton;

    int[] speed = { 261, 216, 179, 135, 76 };
    int currentSpeed;
    int horizontalValue;
    int verticalValue;

    //GameObject newShell;

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

        shotButton = GameObject.Find("ShotButton").GetComponent<Button>();
        shotButton.onClick.AddListener(delegate
        {
            Shot();
        });

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(newShell.GetComponent<Rigidbody>().velocity);
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

    void Shot() 
    {
        //GameObject newShell = Instantiate(shellPrefab, spawnTransform.position+ new Vector3(1,1,1), Quaternion.identity);
        GameObject newShell = Instantiate(shellPrefab, spawnTransform.position, Quaternion.identity);
        //Physics.IgnoreCollision(newShell.GetComponent<CapsuleCollider>(), mortar.GetComponent<Collider>());

        //not working propperly, too much velocity
        //maybe drag(air resistance) will help, will try after relative force
        //velocity actually as stated in table
        //newShell.GetComponent<Rigidbody>().velocity =spawnTransform.up* currentSpeed;

        //using force
        //too low force, i dunno why
        //actually divides by mass, otherwise will be equal to velocity
        //newShell.GetComponent<Rigidbody>().AddForce(spawnTransform.up * currentSpeed,ForceMode.Impulse);

        //as well as default force mode - too low speed, just falls out 
        //newShell.GetComponent<Rigidbody>().AddForce(spawnTransform.up * currentSpeed, ForceMode.Acceleration);

        //nop, too low force and starting speed not right
        //newShell.GetComponent<Rigidbody>().AddRelativeForce(spawnTransform.up * currentSpeed, ForceMode.Impulse);

        //too high speed
        //newShell.GetComponent<Rigidbody>().AddRelativeForce(spawnTransform.up * currentSpeed, ForceMode.VelocityChange);

        //trying different drag with velocity
        //20 too much
        //5 too much
        //1 too much
        //0.5 too low
        //0.75 close, too much
        //0.7 close, too low
        //0.725 is enough
        //all above is true just for main charge and 333 vertical scope)0))
        //yep, doesn't work with anything other
        newShell.GetComponent<Rigidbody>().velocity = spawnTransform.up * currentSpeed;
        mortar.GetComponent<AudioSource>().Play();
    }
}
