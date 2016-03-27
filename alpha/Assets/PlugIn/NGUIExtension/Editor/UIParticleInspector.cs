using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CanEditMultipleObjects]
[CustomEditor(typeof(UIParticle), true)]
public class UIParticleInspector : UIWidgetInspector
{
	UIParticle particle;
	
	protected override void OnEnable ()
	{
		base.OnEnable();
		particle = target as UIParticle;
	}
	
	protected override bool ShouldDrawProperties ()
	{

		if (!CheckMaterials()){
			EditorGUILayout.HelpBox("・ParticleにMaterialが設定されていません。", MessageType.Error);
		}else if (!CheckMaterialsName()){
			EditorGUILayout.HelpBox("・Materialの生成に失敗した可能性があります。Depthが適用されない場合、コンポーネントを外してからもう一回入れなおしてください。", MessageType.Error);
		}else if (!CheckMaterialUsed()){
			EditorGUILayout.HelpBox("・複数のParticleで同じMaterialを使っている可能性があります。同じParticleを違うDepthにしたい場合は、違うMaterialを使用してください。", MessageType.Warning);
		}

		return true;
	}

	bool CheckMaterials(){
		foreach(Material mat in particle._renderer.sharedMaterials){
			if (mat == null) return false;
		}
		return true;
	}

	bool CheckMaterialsName(){
		foreach(Material mat in particle._renderer.sharedMaterials){
			if (!mat.name.Contains("(Clone)")) return false;
		}
		return true;
	}

	bool CheckMaterialUsed(){
		UIParticle[] allParticles = GameObject.FindObjectsOfType<UIParticle>();
		if (allParticles.Length <= 1) return true;
		else{
			foreach(Material mat in particle._renderer.sharedMaterials){
				foreach(UIParticle otherParticle in allParticles){
					if (otherParticle == particle) continue;
					foreach(Material otherMat in otherParticle._renderer.sharedMaterials){
						if (otherMat == mat) return false;
					}
				}
			}
		}
		return true;
	}
}
