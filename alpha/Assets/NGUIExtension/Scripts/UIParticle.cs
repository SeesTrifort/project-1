using UnityEngine;
using System.Collections;
using UnityEditor;

/// (ハン)
/// 
/// NGUIではないMaterialをNGUIのDepthで調整できるようにするコンポーネント.
/// 同じMaterialを使うオブジェクトが２つある場合はDepthがどっちかになってしまうので、別Materialにする必要がある.
/// UIPanelの管理下におくことで、Paticleのスケールとかが変わる可能性があるので最初からコンポーネントを追加してから作業したほうがいいかと..
///

public class UIParticle : UIWidget
{

	[SerializeField] Material[] materials;

	[SerializeField] Renderer __renderer;

	public Renderer _renderer {
		get {
			if (__renderer == null) {
				__renderer = GetComponent<Renderer>();
			}
			return __renderer;
		}
	}
	 
	public override Material material {
		get {
			// 再生,停止が切り替わる時に呼ばれる.

			if (!CheckMaterials()) {
				// 使っているMaterialを全部取得して、RenderQueueを変える新しいクローンMaterialを作る.
				materials = new Material[_renderer.sharedMaterials.Length];
				for (int i = 0; i < _renderer.sharedMaterials.Length; i++) {
					materials [i] = new Material (_renderer.sharedMaterials [i]);
					materials [i].name = materials [i].name + "(Clone)";
				}
			}else {
				// MaterialをクローンしたMaterialにする.
				if (_renderer.sharedMaterials != materials) {
					_renderer.sharedMaterials = materials;
				}

			}
			return materials == null ? null : materials [0];
		}
	}

	bool CheckMaterials(){
		if (materials == null || materials.Length == 0) return false;

		foreach(Material mat in materials){
			if (mat == null) return false;
		}

		return true;
	}


	public override Shader shader {
		get {
			// 再生,停止が切り替わる時に呼ばれる.
			return materials == null ? null : materials[0].shader;
		}
	}

	protected override void OnUpdate ()
	{
		// 再生中には毎フレーム、再生中じゃなくてもGameObjectが選択されたら呼ばれる.

		base.OnUpdate ();

		if (CheckMaterials() && drawCall != null) {
			int renderQueue = drawCall.finalRenderQueue;
			for (int i = 0; i < materials.Length; i++) {
				if (materials[i].renderQueue != renderQueue) materials[i].renderQueue = renderQueue;
			}
		}
	}

	public override void OnFill (BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		// UIPanelが描かれる時に呼ばれるもの(？) ←　よくわからない.
		// http://www.tasharen.com/ngui/docs/class_u_i_widget.html#a95cd9b912861a5b5f08621d660e02ff0
		// UISprite や UILabel, UITexture だとwidgetの大きさから取得して描かれる領域vertsを作るが、
		// Particleは領域がよくわからなくて、一応10000にしておいた. 後で負荷がかかるようだったら調整が必要かもしれない.

		verts.Add (new Vector3 (0f, 0f));
		verts.Add (new Vector3 (0f, 0f));
		verts.Add (new Vector3 (0f, 0f));
		verts.Add (new Vector3 (0f, 0f));
		 
		uvs.Add (new Vector2 (0f, 0f));
		uvs.Add (new Vector2 (0f, 1f));
		uvs.Add (new Vector2 (1f, 1f));
		uvs.Add (new Vector2 (1f, 0f));
		
		cols.Add (color);
		cols.Add (color);
		cols.Add (color);
		cols.Add (color);
	}

	void OnDestroy(){
		for (int i = 0; i < materials.Length; i++) {
			DestroyImmediate(materials[i]);
			materials[i] = null;
		}
		materials = null;
	}
}