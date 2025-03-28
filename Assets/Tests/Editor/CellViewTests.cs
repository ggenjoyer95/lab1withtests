using NUnit.Framework;
using FluentAssertions;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class CellViewTests
{
    [Test]
    public void CV_ForceAwk_Destroy_Ref()
    {
        var go = new GameObject("CV_Ref");
        var cv = go.AddComponent<CellView>();
        var mAwk = typeof(CellView).GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic);
        mAwk.Should().NotBeNull("Awk must exist in CV");
        mAwk.Invoke(cv, null);
        var rt = go.GetComponent<RectTransform>();
        rt.Should().NotBeNull("Awk should set rectTr");
        var txtO = new GameObject("Txt");
        txtO.transform.SetParent(go.transform);
        var txt = txtO.AddComponent<Text>();
        var fld = typeof(CellView).GetField("valueText", BindingFlags.NonPublic | BindingFlags.Instance);
        fld.SetValue(cv, txt);
        var cell = new Cell(new Vector2Int(0, 0), 1);
        var mInit = typeof(CellView).GetMethod("Init", BindingFlags.Instance | BindingFlags.Public);
        mInit.Should().NotBeNull("Init must exist in CV");
        mInit.Invoke(cv, new object[] { cell, null });
        var mDest = typeof(CellView).GetMethod("OnDestroy", BindingFlags.Instance | BindingFlags.NonPublic);
        mDest.Should().NotBeNull("OnDest must exist in CV");
        mDest.Invoke(cv, null);
        Object.DestroyImmediate(go);
    }

    [Test]
    public void CV_Norm_NoRef()
    {
        var go = new GameObject("CV_Norm");
        var cv = go.AddComponent<CellView>();
        var txtO = new GameObject("Txt");
        txtO.transform.SetParent(go.transform);
        var txt = txtO.AddComponent<Text>();
        var fld = typeof(CellView).GetField("valueText", BindingFlags.NonPublic | BindingFlags.Instance);
        fld.SetValue(cv, txt);
        var cell = new Cell(new Vector2Int(0, 0), 1);
        cv.Init(cell, null);
        Object.DestroyImmediate(go);
    }
}
