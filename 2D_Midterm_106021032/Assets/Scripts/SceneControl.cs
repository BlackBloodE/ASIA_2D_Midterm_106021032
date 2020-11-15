using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 場景控制:切換場景、離開遊戲
/// </summary>
public class SceneControl : MonoBehaviour
{
    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// 載入場景
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene("GameScene1");
    }

    /// <summary>
    /// 延遲
    /// </summary>
    public void DelayLoadScene()
    {
        Invoke("LoadScene", 0.5f);//延遲執行
    }
}
