using UnityEngine;
using UnityEngine.UI;

public class AUImage : Image
{
    private Material grayMaterial = null;
    private bool mIsGray = false;
    public bool IsGray
    {
        get => mIsGray;
        set
        {
            if (mIsGray == value) return;
            mIsGray = value;
            if (Application.isPlaying)
            {
                UpdateGray();
            }
        }
    }
    
    private void UpdateGray()
    {
        if (material == null) return;
        if (grayMaterial == null)
        {
            grayMaterial = new Material(Shader.Find("UI/DefaultExtra"));
            if (grayMaterial != null)
            {
                material = grayMaterial;
            }
        }
        grayMaterial?.SetFloat("_GrayScale", mIsGray ? 1f : 0f);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        grayMaterial = null;
    }
}