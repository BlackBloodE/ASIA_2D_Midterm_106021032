using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("追蹤目標")]
    public Transform target;
    [Header("追蹤速度")]
    public float speed = 5;
    [Header("攝影機限制")]
    public Vector2 limitY = new Vector2(-2.5f, 5);

    /// <summary>
    /// 攝影機追蹤
    /// </summary>
    private void Track()
    {
        Vector3 a = transform.position;  //攝影機
        Vector3 b = target.position;  //目標
        b.z = -10;

        a = Vector3.Lerp(a, b, Time.deltaTime * speed);

        a.y = Mathf.Clamp(a.y, limitY.x, limitY.y);
        

        transform.position = a;
    }

    //Update先執行
    //LateUpdate後執行，建議攝影機或追蹤行為
    private void LateUpdate()
    {
        Track();
    }
}
