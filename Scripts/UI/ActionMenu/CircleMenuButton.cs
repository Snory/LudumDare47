using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CircleMenuButton : MonoBehaviour
{

    public Image Icon;
    public Button Button;
    public CircleMenu Menu { get; set; }
    public Action action;


    public void SetButton()
    {
        Menu.RaiseButtonSelected(action);
    }



    





}
