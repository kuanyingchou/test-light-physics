//2014.3.6  ken  initial version

using UnityEngine;
using System.Collections;

public class Util {
    public static Vector3 reflect(
            Vector3 from, Vector3 hit, Vector3 normal) {
        //throw new SystemException("not implemented yet!");
        return hit+Vector3.Reflect(hit-from, hit+normal); 
    }
    public static Vector3 refract(
            Vector3 from, Vector3 hit, Vector3 normal, 
            float n1, float n2) {
        float theta1 = Vector3.Angle(from - hit, normal) * Mathf.Deg2Rad;
        float theta2 = Mathf.Asin((n1 * Mathf.Sin(theta1)) / n2); //>>>
        return Vector3.RotateTowards(
                (Vector3.zero - normal), 
                (hit - from), 
                theta2,
                1);
    }
    public static bool Similar(float a, float b, float tolerance) {
        return Mathf.Abs(a - b) < tolerance;
    }
    public static bool Similar(Vector3 a, Vector3 b, float tolerance) {
        if(!Similar(a.x, b.x, tolerance) ||
           !Similar(a.y, b.y, tolerance) || 
           !Similar(a.z, b.z, tolerance)) return false;
        else return true;
    }
}
