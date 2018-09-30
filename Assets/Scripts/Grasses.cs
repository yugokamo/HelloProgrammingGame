using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grasses : MonoBehaviour {
    [SerializeField] private GameObject grassPrefab;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 100; i++) {
            GameObject grass = Instantiate(grassPrefab) as GameObject;
            grass.transform.SetParent(transform, false);
            grass.transform.position += 2.56f * i * Vector3.right;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
