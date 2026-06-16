using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaseDead
{
    public static string CenoteOnIon(double a)
    {
        return Math.Round(a, SettleShaper.SolidMortar).ToString();
    }
    public static string CenoteOnIon(double a, int digits)
    {
        return Math.Round(a, digits).ToString();
    }

    public static double Solid(double a)
    {
        return Math.Round(a, SettleShaper.SolidMortar);
    }

}
