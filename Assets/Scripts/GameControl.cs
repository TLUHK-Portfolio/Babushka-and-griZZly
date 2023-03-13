using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameControl : MonoBehaviour {

    [SerializeField] public GameObject Live1;
    [SerializeField] public GameObject Live2;
    [SerializeField] public GameObject Live3;
    // Start is called before the first frame update
    void Start() {
        Live1.gameObject.SetActive(true);
        Live2.gameObject.SetActive(true);
        Live3.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update() {
        /*switch (Ball.Lives) {
            case 3:
                Live1.gameObject.SetActive(true);
                Live2.gameObject.SetActive(true);
                Live3.gameObject.SetActive(true);
                break;
            case 2: 
                Live1.gameObject.SetActive(true);
                Live2.gameObject.SetActive(true);
                Live3.gameObject.SetActive(false);
                break;
            case 1:
                Live1.gameObject.SetActive(true);
                Live2.gameObject.SetActive(false);
                Live3.gameObject.SetActive(false);
                break;
            case 0:
                Live1.gameObject.SetActive(false);
                Live2.gameObject.SetActive(false);
                Live3.gameObject.SetActive(false);
                break;
        }*/
    }
}