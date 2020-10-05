using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public delegate void ActionSelectedDelegate(Action selectedAction);

public class CircleMenu : MonoBehaviour
{

    public CircleMenuButton ButtonPrefab;
    public event ActionSelectedDelegate SelectecActionEvent;

    [SerializeField]
    private float _buttonDistance = 50f;
    // Start is called before the first frame update
    public void SpawnLooperButtonsAll(Looper looper)
    {
        List<Action> listOfActions = looper.GetListOfActions();
        for (int i = 0; i < listOfActions.Count; i++)
        {
            Action looperAction = listOfActions[i];
            CircleMenuButton newButton = Instantiate(ButtonPrefab);
            newButton.transform.SetParent(transform, false);
            float theta =  (Mathf.PI *2/ listOfActions.Count) * i;
            float xPos = Mathf.Sin(theta);
            float yPos = Mathf.Cos(theta);
            newButton.transform.localPosition = new Vector3(xPos, yPos, 0) * _buttonDistance;


            var colors = newButton.Button.colors;
            colors.normalColor = looperAction.Normal;
            colors.selectedColor = looperAction.Selected;
            colors.highlightedColor = looperAction.Highlighted;
            colors.pressedColor = looperAction.Pressed;
            newButton.Button.colors = colors;
            newButton.Icon.sprite = looperAction.IsActionActive() ? looperAction.ActiveIcon : looperAction.InActiveIcon;
            newButton.Menu = this;
            newButton.action = looperAction;
        }
    }

    public void SpawnLooperButtonsActive(Looper looper)
    {

        List<Action> activeActions = looper.GetListOfActiveActions();

        


        for (int i = 0; i < activeActions.Count; i++)
        {

            Action looperAction = activeActions[i];
           
                
            CircleMenuButton newButton = Instantiate(ButtonPrefab);
            newButton.transform.SetParent(transform, false);
            float theta = (Mathf.PI * 2 / activeActions.Count) * i;
            float xPos = Mathf.Sin(theta);
            float yPos = Mathf.Cos(theta);
            newButton.transform.localPosition = new Vector3(xPos, yPos, 0) * _buttonDistance;


            var colors = newButton.Button.colors;
            colors.normalColor = looperAction.Normal;
            colors.selectedColor = looperAction.Selected;
            colors.highlightedColor = looperAction.Highlighted;
            colors.pressedColor = looperAction.Pressed;
            newButton.Button.colors = colors;
            newButton.Icon.sprite = looperAction.IsActionActive() ? looperAction.ActiveIcon : looperAction.InActiveIcon;
            newButton.Menu = this;
            newButton.action = looperAction;
        }
    }

    public void RaiseButtonSelected(Action action)
    {
        if (SelectecActionEvent != null)
        {
            SelectecActionEvent.Invoke(action);
        }
    }

}
