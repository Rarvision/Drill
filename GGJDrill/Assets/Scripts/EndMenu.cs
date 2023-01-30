using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndMenu : MonoBehaviour
{
	public void Quit()
	{
		Application.Quit();
	}

    public void ReStartGame()
    {
        SceneManager.LoadScene(0);
    }
}
