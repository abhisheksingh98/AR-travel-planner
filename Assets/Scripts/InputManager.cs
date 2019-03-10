using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using GoogleARCore.Examples.HelloAR;
using GoogleARCore.Examples.Common;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {
	
	public GameObject sphereGO;
	public GameObject food1;
	public GameObject food2;
	public GameObject food3;
	public GameObject food4;

	public GameObject video360prefab;

	private bool spawned = false;

	private int currentStage = 0;
	private int foodref = 0 ;
	private Vector3 allpos;

	public Text uitext;

	private GameObject currGameObject;

	void Start(){
				uitext.enabled = true;
				uitext.text = "Lets book the hotel First . ";
				Invoke("uitextdisabler", 2.5f);
	}

	

	// Update is called once per frame
	void Update () {

		foreach(var t in Input.touches){

			if(t.phase != TouchPhase.Began)
				continue;

			var ray = Camera.main.ScreenPointToRay(t.position);
		//	if(Input.GetMouseButtonDown(0)){	
		//	var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hitInfo;
			if(Physics.Raycast(ray , out hitInfo) && spawned == false)
			{
				allpos = hitInfo.point;
				spawned = true;
				var go  = GameObject.Instantiate(sphereGO , hitInfo.point , Quaternion.identity);
				go.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
				currGameObject = go;
				OnTogglePlanes();
			}



		}
	}
	public void OnTogglePlanes() {
        foreach (GameObject plane in GameObject.FindGameObjectsWithTag ("pla")) {
            MeshRenderer r = plane.GetComponent<MeshRenderer>();
			plane.GetComponent<DetectedPlaneVisualizer>().enabled = false;
            r.enabled = false;
        }
		GameObject planeRest = GameObject.FindGameObjectWithTag ("pm");
		planeRest.GetComponent<PlaneVisualizationManager>().enabled = false ;

		GameObject pointC = GameObject.FindGameObjectWithTag ("point");
		MeshRenderer r1 = pointC.GetComponent<MeshRenderer>();
		r1.enabled = false;
	}

	public void prev(){

	}

	public void next(){

		if(currentStage == 1 ){
			
			if(foodref == 0 ){
				var go  = GameObject.Instantiate( food1 , allpos , Quaternion.identity);
				currGameObject = go;
				uitext.enabled = true;
				uitext.text = "Burger !!!";
				Invoke("uitextdisabler", 2.5f);

			}
			if(foodref == 1){
				Destroy(currGameObject);
				var go  = GameObject.Instantiate( food2 , allpos , Quaternion.identity);
				currGameObject = go;
				uitext.enabled = true;
				uitext.text = "Fries !!!";
				Invoke("uitextdisabler", 2.5f);
			
			}
			if(foodref == 2){
				Destroy(currGameObject);
				var go  = GameObject.Instantiate( food3 , allpos , Quaternion.identity);
				currGameObject = go;
				uitext.enabled = true;
				uitext.text = "HotDoG !!!";
				Invoke("uitextdisabler", 2.5f);
			}
			if(foodref == 3){
				Destroy(currGameObject);
				var go  = GameObject.Instantiate( food4 , allpos , Quaternion.identity);
				currGameObject = go;
				uitext.enabled = true;
				uitext.text = "Pizza (I just love it :D )  !!!";
				Invoke("uitextdisabler", 2.5f);
			}
			
			foodref = foodref + 1; 
		}

		if(currentStage == 2){
			

		}

	}

	public void cont(){
		
		currentStage = currentStage + 1;

			if(currentStage == 0)
			{
				next();

			}
			if(currentStage == 1)
			{
				Destroy(currGameObject);
				uitext.enabled = true;
				uitext.text = "Lets Choose your Yum food !!!!!!";
				Invoke("uitextdisabler", 2.5f);
			}
			if(currentStage == 2)
			{
				Destroy(currGameObject);
				uitext.enabled = true;
				uitext.text = "And most important let us see where you are gonna Visit !!!! ";
				Invoke("uitextdisabler", 2.5f);

				Destroy(currGameObject);
				var go  = GameObject.Instantiate( video360prefab , allpos , Quaternion.identity);
			}


		
	}

	private void uitextdisabler()
	{
		uitext.enabled = false;
	}
}