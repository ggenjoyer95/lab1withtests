using NUnit.Framework;
using FluentAssertions;
using UnityEngine;

public class CellTests
{
    [Test]
    public void Cell_Ctor_SetsPosAndVal()
    {
        var c = new Cell(new Vector2Int(1,2), 3);
        c.Position.Should().Be(new Vector2Int(1,2));
        c.Value.Should().Be(3);
    }

    [Test]
    public void Cell_SetValue_FiresEvent()
    {
        var c = new Cell(new Vector2Int(0,0), 1);
        bool fired = false;
        c.OnValueChanged += (cell, val) => { fired = true; };
        c.Value = 2;
        fired.Should().BeTrue();
        c.Value.Should().Be(2);
    }

    [Test]
    public void Cell_SetValue_Same_NoEvent()
    {
        var c = new Cell(new Vector2Int(0,0), 1);
        bool fired = false;
        c.OnValueChanged += (cell, val) => { fired = true; };
        c.Value = 1;
        fired.Should().BeFalse();
    }

    [Test]
    public void Cell_SetPos_FiresEvent()
    {
        var c = new Cell(new Vector2Int(0,0), 1);
        bool fired = false;
        c.OnPositionChanged += (cell, pos) => { fired = true; };
        c.Position = new Vector2Int(3,4);
        fired.Should().BeTrue();
        c.Position.Should().Be(new Vector2Int(3,4));
    }

    [Test]
    public void Cell_SetPos_Same_NoEvent()
    {
        var c = new Cell(new Vector2Int(1,1), 1);
        bool fired = false;
        c.OnPositionChanged += (cell, pos) => { fired = true; };
        c.Position = new Vector2Int(1,1);
        fired.Should().BeFalse();
    }
}
