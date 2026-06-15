using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wharf : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("ToastText")]    public Text WharfCrew;

    

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);

        WharfCrew.text = uiFormParams.ToString();
        StartCoroutine(nameof(YaleTowerWharf));
    }

    private IEnumerator YaleTowerWharf()
    {
        yield return new WaitForSeconds(2);
        TowerUIAkin(GetType().Name);
    }

}
