using UnityEngine;
using System.Collections;

public class DemoUtil : MonoBehaviour {
    public Vector3 from = new Vector3(1, 0, 0), hit;
    public float normalAngle;
    public float indexA = 1;
    public float indexB = 1.333f;

    public void Update() {
        Vector3 normal = new Vector3(
                Mathf.Cos(normalAngle * Mathf.Deg2Rad), 
                Mathf.Sin(normalAngle * Mathf.Deg2Rad), 0);
        //hit = new Vector3(0, 0, 0);
        Vector3 reflection = Util.reflect(from, hit, normal);
        Vector3 refraction = Util.refract(from, hit, normal, indexA, indexB);
        Debug.DrawRay(from, hit - from, Color.red);
        Debug.DrawRay(hit, reflection - hit, Color.green);
        Debug.DrawRay(hit, refraction - hit, Color.blue);
    }
}
