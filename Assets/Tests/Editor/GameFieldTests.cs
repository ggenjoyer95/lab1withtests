using NUnit.Framework;
using FluentAssertions;
using UnityEngine;
using UnityEngine.TestTools;
using System.Reflection;

public class GameFieldTests
{
    [Test]
    public void StUpNoSpace()
    {
        var gfO = new GameObject("GFObj");
        var gf = gfO.AddComponent<GameField>();
        var mSt = typeof(GameField).GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic);
        mSt.Invoke(gf, null);
        var mUp = typeof(GameField).GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic);
        mUp.Invoke(gf, null);
        Object.DestroyImmediate(gfO);
    }

    [Test]
    public void CrCellPfNull()
    {
        var gfO = new GameObject("GFObj");
        var gf = gfO.AddComponent<GameField>();
        var mSt = typeof(GameField).GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic);
        mSt.Invoke(gf, null);
        gf.GetType().GetField("cellViewPrefab", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).SetValue(gf, null);
        LogAssert.Expect(LogType.Error, "[GameField] cellViewPrefab is null!");
        gf.CreateCell();
        var cvCount = gfO.GetComponentsInChildren<CellView>().Length;
        cvCount.Should().Be(0);
        Object.DestroyImmediate(gfO);
    }

    [Test]
    public void CrCellNoView()
    {
        var gfO = new GameObject("GFObj");
        var gf = gfO.AddComponent<GameField>();
        var mSt = typeof(GameField).GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic);
        mSt.Invoke(gf, null);
        var noVGo = new GameObject("NoViewPf");
        gf.GetType().GetField("cellViewPrefab", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).SetValue(gf, noVGo);
        LogAssert.Expect(LogType.Error, "[GameField] Missing CellView script.");
        gf.CreateCell();
        var cvCount = gfO.GetComponentsInChildren<CellView>().Length;
        cvCount.Should().Be(0);
        Object.DestroyImmediate(gfO);
        Object.DestroyImmediate(noVGo);
    }

    [Test]
    public void CrCellRandAbove()
    {
        var gfO = new GameObject("GFObj");
        var gf = gfO.AddComponent<GameField>();
        var mSt = typeof(GameField).GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic);
        mSt.Invoke(gf, null);
        var realPf = new GameObject("RealPf");
        realPf.AddComponent<CellView>();
        gf.GetType().GetField("cellViewPrefab", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).SetValue(gf, realPf);
        for (int i = 0; i < 20; i++)
        {
            gf.CreateCell();
        }
        Object.DestroyImmediate(gfO);
        Object.DestroyImmediate(realPf);
    }
}
