using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMenuSpawner : Singleton<CircleMenuSpawner>
{

    public CircleMenu CircleMenuPrefab;
    private Transform _mainCameraTransform;

    private void Start()
    {
        _mainCameraTransform = Camera.main.transform;
    }


    public CircleMenu SpawnLooperMenu(Looper loop, bool showAll)
    {
        CircleMenu newMenu = Instantiate(CircleMenuPrefab);
        newMenu.transform.SetParent(this.transform, false);

        if(loop.transform.position.y > _mainCameraTransform.transform.position.y)
        {
            newMenu.transform.position = new Vector3(loop.transform.position.x, loop.transform.position.y, loop.transform.position.z);

        }
        else
        {
            newMenu.transform.position = new Vector3(loop.transform.position.x, loop.transform.position.y +0.5f, loop.transform.position.z);
        }



        if (!showAll)
        {
            newMenu.SpawnLooperButtonsActive(loop);
        } else {
            newMenu.SpawnLooperButtonsAll(loop);
        }

        return newMenu;
    }
}
