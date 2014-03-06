//2014.3.6  ken  initial version

using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System;

public class UnitTest {

    [Test]
    public void TestFixedPoint() {
        Vector3 from,  //where the light comes from
                hit,   //where the light hits the mirror
                normal,//the normal vector of the mirror 
                to;    //where the light goes after reflection, 
                       //the length of 'to' should be eaual to 
                       //the length of (hit - from).

        from= new Vector3(1, 0, 0); //the light comes from the east
        normal = new Vector3(1, 0, 0); //the mirror is at (0, 0, 0), facing east
        hit = new Vector3(0, 0, 0); //the light hits the mirror
        to = Util.reflect(from, hit, normal); //the mirror reflects the light
        Assert.AreEqual(to, from);  //the light should go to (1, 0, 0);

        from= new Vector3(-1, 0, 0);
        hit = new Vector3(0, 0, 0);
        normal = new Vector3(-1, 0, 0);
        to = Util.reflect(from, hit, normal);
        Assert.AreEqual(to, from);
        
        from= new Vector3(0, 1, 0);
        hit = new Vector3(0, 0, 0);
        normal = new Vector3(0, 1, 0);
        to = Util.reflect(from, hit, normal);
        Assert.AreEqual(to, from);

        from= new Vector3(0, -1, 0);
        hit = new Vector3(0, 0, 0);
        normal = new Vector3(0, -1, 0);
        to = Util.reflect(from, hit, normal);
        Assert.AreEqual(to, from);
    }

    [Test]
    public void TestRotation() {
        Vector3 from, hit, normal, to;
        normal = new Vector3(0, 1, 0);
        hit = new Vector3(0, 0, 0);
        for(int deg=0; deg<720; deg++) {
            float x = Mathf.Cos(deg * Mathf.Deg2Rad); 
            float y = Mathf.Sin(deg * Mathf.Deg2Rad);
            from= new Vector3(x, y, 0);
            to = Util.reflect(from, hit, normal);
            Assert.AreEqual(to, new Vector3(-x, y, 0));
        }
    }

    [Test]
    public void TestRefraction() {
        Vector3 from, normal, hit, to;
        float fromIndex = 1;
        float toIndex = 1;
        from = new Vector3(1, 0, 0);
        hit = new Vector3(0, 0, 0);
        normal = new Vector3(1, 0, 0);
        to = Util.refract(from, hit, normal, fromIndex, toIndex);
        Assert.AreEqual((hit - from).normalized + hit, to);

        
        normal = new Vector3(0, 1, 0);
        fromIndex = 1.000293f; //air
        toIndex = 1.333f; //water
        for(int deg=0; deg<180; deg++) {
            float x = Mathf.Cos(deg * Mathf.Deg2Rad); 
            float y = Mathf.Sin(deg * Mathf.Deg2Rad);
            from= new Vector3(x, y, 0);
            float theta1 = ((deg < 90)?(90 - deg):(deg - 90)) * Mathf.Deg2Rad;
            float theta2 = Mathf.Asin((fromIndex * Mathf.Sin(theta1)) / toIndex);
            to = Util.refract(from, hit, normal, fromIndex, toIndex); 
            Assert.IsTrue(
                    Util.Similar(
                        Vector3.Angle((Vector3.zero - normal), to) * 
                                Mathf.Deg2Rad, 
                        theta2,
                        0.0001f
                    )
            );
        }
    }

    [Test]
    public void TestSimilar() {
        Assert.IsTrue(Util.Similar(0, 0, 0.0000001f));
        Assert.IsTrue(Util.Similar(1, 1, 0.0000001f));
        for(int i=-100; i<100; i++) {
            Assert.IsTrue(Util.Similar(i, i, 0.0000001f));
        }

        float tolerance = .1f;
        for(int i=-100; i<100; i++) {
            float a = i;
            float b = a + tolerance * .9999f;
            Assert.IsTrue(Util.Similar(a, b, tolerance));
            b = a + tolerance * 1.0001f;
            Assert.IsFalse(Util.Similar(a, b, tolerance));
        }
    }

}

