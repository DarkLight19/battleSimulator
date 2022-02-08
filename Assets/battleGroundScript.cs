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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log((battleGroundImagePlace.texture as Texture2D).GetPixel((int)Input.mousePosition.x, (int)Input.mousePosition.y) + " " + battleGroundImagePlace.texture.height + " " + battleGroundImagePlace.texture.width + " :::::: " + +battleGroundImagePlace.GetComponent<RawImage>().rectTransform.rect.height + " " + +battleGroundImagePlace.GetComponent<RawImage>().rectTransform.rect.width);
            //Debug.Log((battleGroundImagePlace.texture as Texture2D).GetPixel(0, 0) + " " + (battleGroundImagePlace.texture as Texture2D).GetPixel(1280, 720));
        }
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
            #endregion
        }
    }
}
