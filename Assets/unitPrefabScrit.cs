using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitPrefabScrit : MonoBehaviour
{
    public bool startSimulation = false;

    public Color color;
    public float AD;
    public float HP;
    public float Spd;
    public float Def;
    public float AR;

    //enemy:
    List<Transform> enemys = new List<Transform>();
    float distance = float.MaxValue;
    GameObject closestTarget;

    // Start is called before the first frame update
    void Start()
    {
        TimeAlapsed = 1f;
        GameObject UnitmakerObj = GameObject.Find("UnitMakerPanel");
        foreach (Transform item in UnitmakerObj.transform)
        {
            if(item.gameObject.GetComponent<unitPrefabScrit>().color != color)
            {
                enemys.Add(item);
                float tempDistance = (item.position - this.transform.position).magnitude;
                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    closestTarget = item.gameObject;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float TimeAlapsed;
    private void FixedUpdate()
    {
        TimeAlapsed += Time.deltaTime;
        if (startSimulation)
        {
            //retargeting
            /*
            if (TimeAlapsed > 1f)
            {
                retarget();
                TimeAlapsed = 0f;
            }*/
            Vector3 fromPlayerTarget = (closestTarget.transform.position - this.transform.position);
            //this.GetComponent<Rigidbody2D>().velocity = fromPlayerTarget / fromPlayerTarget.magnitude * Spd;
            this.transform.position += fromPlayerTarget / fromPlayerTarget.magnitude * Spd/100f;
        }
    }

    public void retarget()
    {
        distance = float.MaxValue;
        foreach (var item in enemys)
        {
            float tempdistance = (item.position - this.transform.position).magnitude;
            if(tempdistance<distance)
            {
                distance = tempdistance;
                closestTarget = item.gameObject;
            }
        }
        this.GetComponent<Rigidbody2D>().velocity = (closestTarget.transform.position - this.transform.position) / (closestTarget.transform.position - this.transform.position).magnitude * Spd;
    }
}
