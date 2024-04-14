using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Controller : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public HandMovements movementNumber;

    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HandTracking.instance.returnState(movementNumber))
        {
            theSR.sprite = pressedImage;
        }
        
        if (!HandTracking.instance.returnState(movementNumber))
        {
            theSR.sprite = defaultImage;
        }

    }
}
