using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{

    public static GameManger Instance { get; private set; }
    public AudioSource audioSource;

    private GameObject lastSelected;
    [Header("Audio Settings")]
    public AudioSource audioSourceButton;
    public AudioClip clickSound;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

           
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f; // 确保时间正常流动
        audioSource.Stop();
        audioSource.Play();
    }

  
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        // 关键点：每次都通过 EventSystem.current 获取当前场景的事件系统
        if (EventSystem.current == null) return;

        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;

        if (currentSelected != null && currentSelected != lastSelected)
        {
            // 检查点击的是否是按钮
            Button btn = currentSelected.GetComponentInParent<Button>();
            if (btn != null)
            {
                PlayClickSound();
            }

            // 更新最后选中的物体，防止一帧内或长按时重复播放
            lastSelected = currentSelected;
        }

        // 如果用户点击了空白区域，清除记录，以便下次点击同一个按钮也能触发
        if (currentSelected == null)
        {
            lastSelected = null;
        }
    }

    private void PlayClickSound()
    {
        if (audioSourceButton && clickSound)
        {
            audioSourceButton.PlayOneShot(clickSound);
        }
    }

}
