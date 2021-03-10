using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethods
{
	//http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
	public static void Shuffle<T>(this IList<T> myList)
	{
		System.Random rng = new System.Random();
		int n = myList.Count;
		while (n>1)
		{
			n--;
			int k = rng.Next(n+1);
			T temp = myList[k];
			myList[k] = myList[n];
			myList[n] = temp;
		}
	}

	public static void SetBackground(this Camera cam, Color top, Color bottom)
	{
		cam.GetComponent<GradientBackground>().bottomColor = bottom;
		cam.GetComponent<GradientBackground>().topColor = top;
	}

	public static Color SetAlpha(this Color col, float alpha)
	{
		return new Color(col.r,col.g,col.b,alpha);
	}
}
