using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject End;

    [SerializeField]
    private Button EndBTN;

    void Awake()
    {
        EndBTN.onClick.AddListener(EndGame);
    }

    private void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    public void SetActive(bool isActive)
    {
        End.SetActive(isActive);
    }
}
