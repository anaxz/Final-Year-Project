using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	// 0 -> main screen
	// 1 -> play screen

	public GameObject LoadScreen;

	public void LoadByIndex(int index) {
		if (index.Equals(1) || index.Equals(0)) LoadScreen.SetActive(true); // if play pressed, display load screen
		SceneManager.LoadScene(index); // load a scene by it's index - see via build settings
	}
}
