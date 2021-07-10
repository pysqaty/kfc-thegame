using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class CollectionsUtils
    {
		// Helper for O(n) IList<T> shuffling, based on Fisher–Yates Shuffle
		public static void Shuffle<T>(IList<T> array)  
		{    
			for (int n = array.Count - 1; n > 0; n--) 
			{
				int k = UnityEngine.Random.Range(0, n);
				T value = array[k];
				array[k] = array[n];  
				array[n] = value;  
			}
		}
    }
}
