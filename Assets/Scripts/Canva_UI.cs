using UnityEngine;

public class Canva_UI : MonoBehaviour
{
    public GameObject UI_;
    public bool _isUI;

    private void Update()
    {
    }

    public void Canva()
    {
        Debug.Log("123");
        UI_.SetActive(true);
    }
}