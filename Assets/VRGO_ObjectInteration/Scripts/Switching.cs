using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switching : MonoBehaviour {

	public GameObject obj1;
	public GameObject obj2;

	public void OBJ_1()
	{
		obj2.SetActive (false);
		obj1.SetActive (true);
	}
	public void OBJ_2()
	{
		obj1.SetActive (false);
		obj2.SetActive (true);
	}
}
