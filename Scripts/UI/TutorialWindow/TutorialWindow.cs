using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialWindow : MonoBehaviour
{

    public TutorialText AdviseTextPrefab;

    // Start is called before the first frame update
    public void SpawnText(Advise advise)
    {
        TutorialText text = Instantiate(AdviseTextPrefab);
        text.transform.SetParent(this.transform, false);
        text.transform.position = this.transform.position;
        text.transform.localPosition = new Vector3(0, 20, 0);
        text.GetComponent<Text>().text = advise.AdviseText;
    }


}
