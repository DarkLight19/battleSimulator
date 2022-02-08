using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitScript : MonoBehaviour
{
    public GameObject CirclePrefab;
    public GameObject storage;
    public GameObject popUpOptions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Input from user (Through input fields
    public int N;
    public float AD;
    public float HP;
    public float Spd;
    public float Def;
    public float AR;

    //these are set by mainScipt/Input from user:
    public float size = 1f;
    public Color prefabColor = new Color(255,255,255);
    //these are set by the BoxCreation Script:
    public GameObject square;
    public Vector3 firstCordinate;
    public Vector3 secondCordinate;


    public void createTroops()
    {
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        float xLength = Math.Abs(cam.ScreenToWorldPoint(firstCordinate).x - cam.ScreenToWorldPoint(secondCordinate).x);
        float yLength = Math.Abs(cam.ScreenToWorldPoint(firstCordinate).y - cam.ScreenToWorldPoint(secondCordinate).y);

        #region prímválasztás
        //elso 100 prím szám
        List<int> Primes = new List<int>() { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541 };
        List<int> Nprimleosztas = new List<int>();
        while (N != 1)
        {
            foreach (var item in Primes)
            {
                if (N % item == 0)
                {
                    N = N / item;
                    Nprimleosztas.Add(item);
                    break;
                }
            }
        }

        List<List<int>> combos = GetAllCombos(Nprimleosztas);

        List<double> megoldasKetto = bestOption(combos, xLength / yLength, Nprimleosztas);
        #endregion
        
        float xSteps = Math.Abs(xLength / (float)megoldasKetto[0]);
        float ySteps = Math.Abs(yLength / (float)megoldasKetto[1]);

        //Debug.Log(megoldasKetto[0] + " " + megoldasKetto[1]);
        //Debug.Log(xLength + " " + xSteps + "\t" + yLength + " " + ySteps);

        Vector3 temp = square.transform.position;
        float xActPos = temp.x - xLength/2 + xSteps / 2;
        float yActPos = temp.y + yLength/2 - ySteps / 2;

        for (int i = 0; i < megoldasKetto[1]; i++) //y
        {
            for (int j = 0; j < megoldasKetto[0]; j++) //x
            {
                CirclePrefab.transform.GetComponent<SpriteRenderer>().sortingOrder = 11;
                CirclePrefab.transform.localScale = new Vector3(size / 10, size / 10, 0);
                CirclePrefab.GetComponent<SpriteRenderer>().color = prefabColor;
                CirclePrefab.transform.position = new Vector3(xActPos + xSteps * j, yActPos - ySteps * i, 0);
                //------
                CirclePrefab.GetComponent<unitPrefabScrit>().AD = AD;
                CirclePrefab.GetComponent<unitPrefabScrit>().HP = HP;
                CirclePrefab.GetComponent<unitPrefabScrit>().Spd = Spd;
                CirclePrefab.GetComponent<unitPrefabScrit>().Def = Def;
                CirclePrefab.GetComponent<unitPrefabScrit>().AR = AR;

                Instantiate(CirclePrefab, storage.transform, true);
            }
        }
    }
    public static List<List<T>> GetAllCombos<T>(List<T> list)
    {
        int comboCount = (int)Math.Pow(2, list.Count) - 1;
        List<List<T>> result = new List<List<T>>();
        for (int i = 1; i < comboCount + 1; i++)
        {
            // make each combo here
            result.Add(new List<T>());
            for (int j = 0; j < list.Count; j++)
            {
                if ((i >> j) % 2 != 0)
                    result.Last().Add(list[j]);
            }
        }
        return result;
    }
    public static List<double> bestOption(List<List<int>> combos, double arany, List<int> eredeti)
    {
        List<double> bestoption = new List<double>() { 0, 0, int.MaxValue };
        foreach (var item in combos)
        {
            int szamEGY = 1;
            int szamKetto = 1;
            List<int> kipotolt = parlistacsinalo(eredeti, item);
            foreach (var i in item)
            {
                szamEGY = szamEGY * i;
            }

            foreach (var i in kipotolt)
            {
                szamKetto = szamKetto * i;
            }

            if (Math.Abs((double)szamEGY / szamKetto - arany) < bestoption[2])
                bestoption = new List<double>() { szamEGY, szamKetto, Math.Abs((double)szamEGY / szamKetto - arany) };
        }
        return bestoption;
    }
    public static List<int> parlistacsinalo(List<int> eredeti, List<int> megvanmar)
    {
        List<int> solution = new List<int>(eredeti);

        for (int i = 0; i < megvanmar.Count; i++)
        {
            for (int j = 0; j < solution.Count; j++)
            {
                if (megvanmar[i] == solution[j])
                {
                    solution.RemoveAt(j);
                    break;
                }
            }
        }

        return solution;
    }

    public void doneButtonPressed()
    {
        GameObject inputs = GameObject.Find("InputHolder");
        foreach (Transform item in inputs.transform)
        {
            //Debug.Log(item.transform.GetChild(0).GetChild(item.transform.GetChild(0).childCount - 1).GetComponent<TMP_Text>() + " " + item.transform.GetChild(0));
            if (item.gameObject.name == "NInput")
                N = int.Parse(item.GetComponent<TMP_InputField>().text.ToString());
            if (item.gameObject.name == "ADInput")
                AD = float.Parse(item.GetComponent<TMP_InputField>().text.ToString());
            if (item.gameObject.name == "HPInput")
                HP = float.Parse(item.GetComponent<TMP_InputField>().text.ToString());
            if (item.gameObject.name == "SpdInput")
                Spd = float.Parse(item.GetComponent<TMP_InputField>().text.ToString());
            if (item.gameObject.name == "DefInput")
                Def = float.Parse(item.GetComponent<TMP_InputField>().text.ToString());
            if (item.gameObject.name == "ARInput")
                AR = float.Parse(item.GetComponent<TMP_InputField>().text.ToString());
        }
        createTroops();
        popUpOptions.gameObject.SetActive(false);
    }

    public void StartSimulation()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<unitPrefabScrit>().startSimulation = true;
        }
    }
}
