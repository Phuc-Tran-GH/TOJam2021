using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	/// <summary>
	/// A dictionary that holds every singleton instance.
	/// </summary>
	private static readonly Dictionary<Type, MonoBehaviour> singletonDict = new Dictionary<Type, MonoBehaviour>();

	/// <summary>
	/// The public accessor for the singleton instance which finds the instance and places it in the singleton dictionary.
	/// </summary>
	public static T instance => GetSingletonInstance(typeof(T)) as T;

	/// <summary>
	/// Finds the singleton in the scene, or creates one if one is not found. Afterwards, the singleton will be pulled from the singleton dictionary.
	/// </summary>
	private static object GetSingletonInstance(Type type)
	{
		// Check cache for existing instance
		if (singletonDict.ContainsKey(type))
		{
			return singletonDict[type] as T;
		}

		// Search for existing singleton in scene
		MonoBehaviour newInstance = (T) FindObjectOfType(typeof(T));

		// Create new instance if one doesn't already exist
		if (newInstance == null)
		{
			var singletonObject = new GameObject();
			newInstance = singletonObject.AddComponent<T>();
			singletonObject.name = $"{typeof(T)} (Singleton)";
		}

		// Make instance persistent
		DontDestroyOnLoad(newInstance);

		// Add instance to dictionary
		singletonDict[type] = newInstance;

		return newInstance;
	}
}