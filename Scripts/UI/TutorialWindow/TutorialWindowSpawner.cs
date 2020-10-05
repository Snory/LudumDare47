using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialWindowSpawner : Singleton<TutorialWindowSpawner>
{

    public TutorialWindow TutorialWindowPrefab;

    public TutorialWindow SpawnTutorialWindow(Tutor tutor)
    {
        TutorialWindow newWindow = Instantiate(TutorialWindowPrefab);
        newWindow.transform.SetParent(this.transform, false);
        newWindow.transform.position = tutor.transform.position;
        newWindow.transform.localPosition = new Vector3(tutor.OffsetWindowFromTutor.x, tutor.OffsetWindowFromTutor.y, 0);
        newWindow.SpawnText(tutor.GetCurrentAdvise());
        return newWindow;
    }
}
