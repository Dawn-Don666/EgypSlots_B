using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalFinnish : MonoYoungster<MedalFinnish>
{
    public void WithMedal(string info)
    {
        UIFinnish.RatRuminate().WithUIOnset(nameof(Medal), info);
    }
}
