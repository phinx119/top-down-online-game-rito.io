using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Image buttonImage;

    public void SetButtonImage(Sprite newImage)
    {
        buttonImage.sprite = newImage;
    }
}
