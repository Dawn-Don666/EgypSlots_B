using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medal : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("ToastText")]    public Text MedalPoet;

    

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);

        MedalPoet.text = uiFormParams.ToString();
        StartCoroutine(nameof(PickCaputMedal));
    }

    private IEnumerator PickCaputMedal()
    {
        yield return new WaitForSeconds(2);
        CaputUIEach(GetType().Name);
    }

}
