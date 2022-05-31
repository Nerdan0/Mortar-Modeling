using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetController : MonoBehaviour
{

    TMP_Text targetPositionText;
    TMP_Text azimuthText;
    TMP_Text distanceText;
    GameObject azimuthObject;
    int minRange = 100;
    int maxRange = 3901;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        azimuthObject = GameObject.Find("AzimuthObject");

        targetPositionText = GameObject.Find("TargetPosition").GetComponent<TMP_Text>();
        azimuthText = GameObject.Find("Azimuth").GetComponent<TMP_Text>();
        distanceText = GameObject.Find("Distance").GetComponent<TMP_Text>();
        distance = VectorDistance(transform.position.x, transform.position.z);

        azimuthObject.transform.LookAt(transform);

        azimuthText.text = "Азимут до цілі: " + Mathf.Round(azimuthObject.transform.localEulerAngles.y);
        distanceText.text = "Дистанція до цілі: " + Mathf.Round(distance) +" м";
        targetPositionText.text = "Положення цілі:\nx = " + transform.position.x + "; z = " + transform.position.z + ";";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomPosition() 
    {
        int randX = RandomPoint();
        int randZ = RandomPoint();
        distance = VectorDistance(randX, randZ);

        Debug.Log(distance);

        while (distance > maxRange) 
        {   
            
            randZ = RandomPoint();
            distance = VectorDistance(randX, randZ);
            Debug.Log(distance);
        }

        transform.position = new Vector3(randX, transform.position.y, randZ);
        targetPositionText.text = "Положення цілі:\nx = " + transform.position.x + "; z = " + transform.position.z + ";";
        distanceText.text = "Дистанція до цілі: " + Mathf.Round(distance) + " м";
        azimuthObject.transform.LookAt(transform);
        azimuthText.text = "Азимут до цілі: " + Mathf.Round(azimuthObject.transform.localEulerAngles.y);

        GameObject[] craters = GameObject.FindGameObjectsWithTag("Crater");
        foreach (GameObject crater in craters)
        {
            Destroy(crater);
        }

    }

    int RandomPoint() 
    {
        return Random.Range(minRange, maxRange) * (Random.Range(0, 2) * 2 - 1);
    }

    float VectorDistance(float randX, float randZ) 
    {
        return Mathf.Sqrt(randX * randX + randZ * randZ);
    }
}
