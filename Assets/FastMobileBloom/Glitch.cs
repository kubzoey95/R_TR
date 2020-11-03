using UnityEngine;

public class Glitch : MonoBehaviour
{
	public Material mat = null;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, mat);
	}
}
