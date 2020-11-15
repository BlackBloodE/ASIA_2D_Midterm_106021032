using UnityEngine;

public class LearnLerp : MonoBehaviour
{
    public int A = 0;
    public int B = 10;

    public float C = 0;
    public int D = 100;

    private void Start()
    {
        float r = Mathf.Lerp(A, B, 0.7f);
    }

    private void Update()
    {
        C = Mathf.Lerp(C, D, 0.5f * Time.deltaTime);

        print(C);
    }
}
