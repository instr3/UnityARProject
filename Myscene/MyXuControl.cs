using UnityEngine;
using System.Collections;

public class MyXuControl : MonoBehaviour {
	private Animator _animator;
	private AnimatorStateInfo _currentStateInfo;
	private AnimatorStateInfo _preStateInfo;

	public float waitTime = 3f;
	public bool isRandom = true;

	public GameObject camera;
	public GameObject arObject;

	public AnimationClip[] _FaveClips;
	public string[] _FaceMotionName;

	public AudioClip[] _ChanVoice;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
		_currentStateInfo = _animator.GetCurrentAnimatorStateInfo (0);
		_preStateInfo = _currentStateInfo;
		StartCoroutine (RandomChangeMotion ());
		_FaveClips = Resources.LoadAll<AnimationClip> ("FaceMotion");
		_FaceMotionName = new string[_FaveClips.Length];
		_ChanVoice = Resources.LoadAll<AudioClip> ("ChanVoice");
		for (int i = 0; i < _FaveClips.Length; i++) {
			_FaceMotionName [i] = _FaveClips [i].name;
		}
			
	}
	private void ChangeFace(){
		_animator.SetLayerWeight(1,1);
		int index = Random.Range(0,_FaveClips.Length);
		_animator.CrossFade(_FaceMotionName[index],0);
		AudioSource audio = GetComponent<AudioSource> ();
		if (audio.isPlaying) {
			audio.Stop ();
		}
		audio.clip = _ChanVoice [index];
		audio.Play ();
	}	
	// Update is called once per frame
	void Update () {

		/*Vector3 m;
		Vector3 n;
		if (camera != null && arObject != null) {
			m = camera.transform.position;
			n = arObject.transform.position;
			print (Vector3.Distance (m, n));
		}*/
		RaycastHit hitInfo;
		if (Input.GetMouseButtonDown (0)) {
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo, Mathf.Infinity)) {
				if (hitInfo.collider.tag == "face") {
					ChangeFace ();
				}
			}
		}
		if (_animator.GetBool ("Next")) {
			_currentStateInfo = _animator.GetCurrentAnimatorStateInfo (0);
			if (_currentStateInfo.fullPathHash != _preStateInfo.fullPathHash) {
				_animator.SetBool ("Next", false);
				_preStateInfo = _currentStateInfo;
			}
		}
	}

	IEnumerator RandomChangeMotion()
	{
		while (true) {
			if (isRandom) {
				_animator.SetBool ("Next", true);
			}
			yield return new WaitForSeconds (waitTime);
		}
	}
		

}
