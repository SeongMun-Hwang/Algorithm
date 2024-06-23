using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera playerCamera;
    public RenderTexture renderTexture;
    public Renderer quadRenderer;

    public float scrollPower = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        renderTexture = new RenderTexture(1024, 768, 16); // ������ �ػ󵵷� RenderTexture�� ����
        playerCamera.targetTexture = renderTexture;
        quadRenderer.material.mainTexture = renderTexture;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(TakePhoto());
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
        {
            playerCamera.fieldOfView += scrollPower;
        }
        else if (scroll < 0)
        {
            playerCamera.fieldOfView -= scrollPower;
        }
    }
    IEnumerator TakePhoto()
    {
        yield return new WaitForEndOfFrame(); // ���� ������ �������� �����⸦ ��ٸ�

        RenderTexture.active = renderTexture; // RenderTexture�� Ȱ��ȭ
        Texture2D screenImage = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        screenImage.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        screenImage.Apply();
        RenderTexture.active = null; // RenderTexture Ȱ��ȭ ����

        byte[] imageBytes = screenImage.EncodeToPNG(); // �̹����� PNG �������� ���ڵ�
        string filename = "Screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        System.IO.File.WriteAllBytes(filename, imageBytes); // ���Ϸ� ����
        Debug.Log("Screenshot saved to " + filename);

        Destroy(screenImage); // ��� �� Texture2D ��ü ����
    }
}
