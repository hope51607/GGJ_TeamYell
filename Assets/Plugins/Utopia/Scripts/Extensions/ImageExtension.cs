using UnityEngine;
using UnityEngine.UI;

public static partial class ExtensionMethods
{
    public static void SetAlpha(this Image image, float alpha)
    {
        if (image == null)
        {
            return;
        }
        var color = image.color;
        image.color = new Color(color.r, color.g, color.b, alpha);
    }
}
