using System.Collections;
using System.Drawing;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using UnityEngine.UI;

public class battleGroundScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool colorPicker = false;
    public RawImage colorPickerPanel;>
    // Update is called once per frame
    void FixedUpdate()
    {
        if (colorPicker)
        {
            if (Input.GetMouseButtonDown(0))
            {
                islandFinder(Input.mousePosition.x, Input.mousePosition.y);
                tobBeFoundColor = (battleGroundImagePlace.texture as Texture2D).GetPixel((int)Input.mousePosition.x, (int)Input.mousePosition.y);
                //Debug.Log((battleGroundImagePlace.texture as Texture2D).GetPixel((int)Input.mousePosition.x, (int)Input.mousePosition.y) + " " + battleGroundImagePlace.texture.height + " " + battleGroundImagePlace.texture.width + " :::::: " + +battleGroundImagePlace.GetComponent<RawImage>().rectTransform.rect.height + " " + +battleGroundImagePlace.GetComponent<RawImage>().rectTransform.rect.width);
                //Debug.Log((battleGroundImagePlace.texture as Texture2D).GetPixel(0, 0) + " " + (battleGroundImagePlace.texture as Texture2D).GetPixel(1280, 720));
            }

            setRawImage.color = (battleGroundImagePlace.texture as Texture2D).GetPixel((int)Input.mousePosition.x, (int)Input.mousePosition.y);
        }
    }
    public Color32 toBeFoundColor;
    private void islandFinder(List<(int,int)> foundCords, int mousePosX, int mousePosY)
    {
        Texture2D imagecopy = image;
        for (int i = 0; i < imagecopy.height; i++) //if i remember correctly thats how we do it
            for (int j = 0; j < imagecopy.width; j++)
                if(imagecopy.Getpixel(j,i) == toBeFoundColor)//found an island
                {
                    //needed to make a rectangle
                    /*
                           (1,1)
                      (0,0)
                     */
                    //setting them to the exact opposite:
                    int topMostCord = 720;
                    int downMostCord = 0;
                    int leftMostCord = 1280;
                    int rightMostCord = 0;
                    foreach (var item in islandCollector(new List<(int, int)>() { (j, i) }, j, i))
                    {

                    }
                }
    }

    private List<(int,int)> islandCollector(List<(int,int)> foundCords, int startX, int startY)
    {
        //check for all 8 adjacent pixels:
        List<(int, int)> tempSolution = new List<(int, int)>();
        if (startX > 0)
        {
            if (image.GetPixel(startX - 1, startY) == toBeFoundColor)
                if(!foundCords.Contains((startX - 1, startY)))
                    foundCords.Add((startX - 1, startY));
            if (startY < 720)
                if (image.GetPixel(startX - 1, startY + 1) == toBeFoundColor)
                    if (!foundCords.Contains((startX - 1, startY + 1)))
                        foundCords.Add((startX - 1, startY + 1));
            if (startY > 0)
                if (image.GetPixel(startX - 1, startY - 1) == toBeFoundColor)
                    if (!foundCords.Contains((startX - 1, startY - 1)))
                        foundCords.Add((startX - 1, startY - 1));
        }
        if (startX < 1280)
        {
            if (image.GetPixel(startX + 1, startY) == toBeFoundColor)
                if (!foundCords.Contains((startX + 1, startY)))
                    foundCords.Add((startX + 1, startY));
            if (startY < 720)
                if (image.GetPixel(startX + 1, startY + 1) == toBeFoundColor)
                    if (!foundCords.Contains((startX + 1, startY + 1)))
                        foundCords.Add((startX + 1, startY + 1));
            if (startY > 0)
                if (image.GetPixel(startX + 1, startY - 1) == toBeFoundColor)
                    if (!foundCords.Contains((startX + 1, startY - 1)))
                        foundCords.Add((startX + 1, startY - 1));
        }
        if (startY > 0)
            if (image.GetPixel(startX, startY - 1) == toBeFoundColor)
                if (!foundCords.Contains((startX, startY - 1)))
                    foundCords.Add((startX, startY - 1));
        if (startY < 720)
            if (image.GetPixel(startX, startY + 1) == toBeFoundColor)
                if (!foundCords.Contains((startX, startY + 1)))
                    foundCords.Add((startX, startY + 1));

        //adding found pixelCords to the list:
        foreach (var item in tempSolution)
            if (!foundCords.Contains(item))
                foundCords.Add(item);

        //Sending for those pixels as well:
        foreach (var item in tempSolution)
            foreach (var i in islandCollector(foundCords, item.Item1, item.ite2))
                if (!foundCords.Contains(i)) //collecting the new cordinates
                    foundCords.Add(i);

        return foundCords; //not very efficient, but its not recursive
    }

    public string path;
    public RawImage battleGroundImagePlace;
    public Texture2D image;

    public void ChooseBattleGround()
    {
        path = EditorUtility.OpenFilePanel("Válassz ki egy képet!", "", "*"); //getting the path
        StartCoroutine(setRawImage());
    }

    IEnumerator setRawImage()
    {
        bool masodikmegoldas = true;
        #region tuti mûködik
        if (!masodikmegoldas)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture("file:///" + path); //connection string
            yield return www.SendWebRequest(); //waiting till we get the connection

            if (www.isNetworkError || www.isHttpError) { }
                //Debug.Log(www.error);
            else
            {
                battleGroundImagePlace.color = new Color32(255, 255, 255, 255);
                battleGroundImagePlace.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            }
        }
        #endregion

        #region SaveAsImage
        else
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture("file:///" + path); //connection string
            yield return www.SendWebRequest(); //waiting till we get the connection

            if (www.isNetworkError || www.isHttpError) { }
                //Debug.Log(www.error);
            else
            {
                battleGroundImagePlace.color = new Color32(255, 255, 255, 255);
                image = ((DownloadHandlerTexture)www.downloadHandler).texture;
            }


            //Debug.Log("Texture size: " + image.height + " " + image.width);
            /*
            image.width = 720;
            image.height = 1280;*/
            battleGroundImagePlace.texture = image;
            //Debug.Log("Raw Image size: " + battleGroundImagePlace.texture.width + " " + battleGroundImagePlace.texture.height + " ");
        }
        #endregion
    }
}
