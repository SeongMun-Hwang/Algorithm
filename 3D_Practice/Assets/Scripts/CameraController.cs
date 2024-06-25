using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera playerCamera;
    public RenderTexture renderTexture;
    public Renderer quadRenderer;

    public float scrollPower = 1.0f;
    private bool cameraPower = false;

    //shader
    private Material grayscaleMaterial;

    void Start()
    {
        renderTexture = new RenderTexture(1024, 768, 16); // ������ �ػ󵵷� RenderTexture�� ����
        playerCamera.targetTexture = renderTexture;
        quadRenderer.material.mainTexture = renderTexture;
        quadRenderer.enabled = false;

        grayscaleMaterial = new Material(Shader.Find("Custom/SimpleGrayscale"));     
    }

    void Update()
    {
        //camera zoom in/out
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
        {
            playerCamera.fieldOfView += scrollPower;
        }
        else if (scroll < 0)
        {
            playerCamera.fieldOfView -= scrollPower;
        }
        //camera on/off
        if (Input.GetMouseButtonDown(1))
        {
            if (cameraPower)
            {
                cameraPower = false;
                quadRenderer.enabled = false;
            }
            else
            {
                cameraPower = true;
                quadRenderer.enabled = true;
            }
        }
        //Take Photo
        if (Input.GetMouseButtonDown(0) && cameraPower)
        {
            StartCoroutine(TakePhoto());
        }
        //black & white mode
        if (Input.GetKeyDown(KeyCode.F1))
        {
            quadRenderer.material = grayscaleMaterial;
            quadRenderer.material.mainTexture = renderTexture;
            quadRenderer.enabled = false;
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
