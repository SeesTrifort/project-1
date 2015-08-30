using UnityEngine;
using System.Collections;

public class MyPageController : SeesProject {

	public void LoadScene(GameObject go){
		Application.LoadLevel(go.name);
	}

	protected override void Awake (){
		base.Awake();
	}

	public override void DataLoaded (){

	}
}
