using UnityEngine;
using UnityEditor;
using System;

public class AnimeStudioPostProcessor : AssetPostprocessor
{
	private bool fIsAnimeStudioModel = false;

	void OnPreprocessModel()
	{
		fIsAnimeStudioModel = false;
		// resampleRotations only became part of Unity as of version 5.3.
		// If you're using an older version of Unity, comment out the following block of code.
		// Set resampleRotations to false to fix the "bouncy" handling of constant interpolation keyframes.
		try
		{
			var importer = assetImporter as ModelImporter;
			importer.resampleRotations = false;
		}
		catch
		{
		}
	}

	void OnPostprocessGameObjectWithUserProperties(GameObject g, string[] names, System.Object[] values)
	{
		// Only operate on FBX files
		if (assetPath.IndexOf(".fbx") == -1)
		{
			return;
		}

		for (int i = 0; i < names.Length; i++)
		{
			if (names[i] == "ASP_FBX")
			{
				fIsAnimeStudioModel = true; // at least some part of this comes from Anime Studio
				break;
			}
		}
	}

	void OnPostprocessModel(GameObject g)
	{
		// Only operate on FBX files
		if (assetPath.IndexOf(".fbx") == -1)
		{
			return;
		}

		if (!fIsAnimeStudioModel)
		{
			//Debug.Log("*** Not Moho ***");
			return;
		}

		Shader shader = Shader.Find("Sprites/Default");
		if (shader == null)
			return;

		Renderer[] renderers = g.GetComponentsInChildren<Renderer>();
		int straightRenderOrder = shader.renderQueue;
		foreach (Renderer r in renderers)
		{
			int renderOrder = straightRenderOrder;
			if (r.name.Contains("|"))
			{
				string[] stringSeparators = new string[] {"|"};
				string[] parts = r.name.Split(stringSeparators, StringSplitOptions.None);
				int j;
				if (Int32.TryParse(parts[parts.Length - 1], out j))
					renderOrder += j;
			}
			r.sharedMaterial.shader = shader; // apply an unlit shader
			r.sharedMaterial.renderQueue = renderOrder; // set a fixed render order
			straightRenderOrder++;
		}
	}
}
