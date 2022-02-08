using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxCreationScript : MonoBehaviour
{
    public bool BoxCreationIsAlowed = false;
    public Sprite squareSprite;
    public Color ChoosenColor;

    public bool firstFrame = true;
    public bool inOperation = false;
    public Vector2 firstCordinate;
    public Vector2 secondCordinate;

    public GameObject Canvas;
    public GameObject panelObj;

    private Vector3 scaleFirst;
    private Vector3 scaleSecond;
    private Vector3 scaleSecondRegi;

    public GameObject circleMaker;

    public GameObject popUpOptions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BoxCreationIsAlowed)
        {
            if (Input.GetMouseButtonDown(0) == true || Input.GetMouseButton(0) == true)
            {
                if (firstFrame == true)
                {
                    firstCordinate = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    scaleFirst = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(firstCordinate.x, firstCordinate.y, 0));
                    scaleSecondRegi = scaleFirst;
                    inOperation = true;
                    firstFrame = false;
                    panelObj = createBox();
                }
                if(inOperation == true)
                {
                    secondCordinate = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    scaleSecond = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(secondCordinate.x, secondCordinate.y, 0));
                    //Debug.Log(new Vector3((scaleSecondRegi.x - scaleSecond.x) / 2, (scaleSecondRegi.y - scaleSecond.y) / 2, 0));
                    panelObj.transform.localScale += new Vector3((scaleSecondRegi.x - scaleSecond.x), (scaleSecondRegi.y - scaleSecond.y), 0);
                    panelObj.transform.position -= new Vector3((scaleSecondRegi.x - scaleSecond.x)/2, (scaleSecondRegi.y - scaleSecond.y)/2, 0);
                    scaleSecondRegi = scaleSecond;
                }
            }
            if (inOperation == true && Input.GetMouseButtonUp(0))
            {
                inOperation = false;
                firstFrame = true;
                Color original = panelObj.GetComponent<SpriteRenderer>().color;
                panelObj.GetComponent<SpriteRenderer>().color = new Color(original.r,original.g,original.b,255);
                panelObj.transform.SetParent(Canvas.transform, false);
                //createBoxBrahBrah();
                /*
                Vector2 temp = new Vector2(Mathf.Abs(scaleFirst.x - scaleSecond.x), Mathf.Abs(scaleFirst.y - scaleSecond.y));
                circleMaker.GetComponent<UnitScript>().creatTroops(0.2f, GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(firstCordinate), panelObj, temp.x, temp.y, 100, 0);
                */
                circleMaker.GetComponent<UnitScript>().square = panelObj;
                circleMaker.GetComponent<UnitScript>().firstCordinate = firstCordinate;
                circleMaker.GetComponent<UnitScript>().secondCordinate = secondCordinate;
                //circleMaker.GetComponent<UnitScript>().createTroops(600);
                popUpOptions.gameObject.SetActive(true);
                BoxCreationIsAlowed = false;
            }
        }
    }

    public GameObject createBox()
    {
        //Debug.Log(firstCordinate);
        //Debug.Log(secondCordinate);
        GameObject panel = new GameObject("Square");
        panel.AddComponent<SpriteRenderer>();
        panel.GetComponent<SpriteRenderer>().color = ChoosenColor;
        panel.GetComponent<SpriteRenderer>().sortingOrder = 10;
        panel.GetComponent<SpriteRenderer>().sprite = squareSprite;
        Vector3 scaleFirst = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenToWorldPoint(new Vector3(firstCordinate.x, firstCordinate.y, 0));
        panel.transform.position = new Vector3(scaleFirst.x, scaleFirst.y, 0);
        panel.transform.localScale = new Vector3(0,0,0);
        return panel;
    }

    public void AllowBoxCreation()
    {
        BoxCreationIsAlowed = true;
    }
}
