using System;
using System.Collections.Generic;
using Extensions;
using NUnit.Framework;

public class TestListExtensions
{
    [Test]
    public void AddBeforeOf()
    {
        var list = new List<int> {0, 1, 2, 3, 4, 5, 6};
        var item = 7;

        //add 7 in front of 3
        list.AddBeforeOf(3, item);

        var posBefore3 = list.IndexOf(3) - 1;
        Assert.AreEqual(list[posBefore3], item);
    }

    [Test]
    public void AddBeforeOfNotContainedItem_IndexOutOfBounds()
    {
        var list = new List<int> {0, 1, 2, 3, 4, 5, 6};
        var item = 7;
        var notContainedItem = 9;

        var posBefore3 = list.IndexOf(3) - 1;
        Assert.False(list.Contains(notContainedItem));

        //add 7 in front of 9
        void addNotContained()
        {
            list.AddBeforeOf(notContainedItem, item);
        }

        Assert.Throws<ArgumentOutOfRangeException>(addNotContained);
    }

    [Test]
    public void AddAfterOf()
    {
        var list = new List<int> {0, 1, 2, 3, 4, 5, 6};
        var item = 7;

        //add 7 after of 3
        list.AddAfterOf(3, item);

        var posBefore3 = list.IndexOf(3) + 1;
        Assert.AreEqual(list[posBefore3], item);
    }

    [Test]
    public void AddAfterOfLast()
    {
        var list = new List<int> {0, 1, 2, 3, 4, 5, 6};
        var item = 7;
        var present = 6;

        //add 7 after of 3
        list.AddAfterOf(present, item);

        var position = list.IndexOf(present) + 1;
        Assert.AreEqual(list[position], item);
    }

    [Test]
    public void GetRandomItemFromEmptyList_OutOfBounds()
    {
        var list = new List<int>();

        void GetRandom()
        {
            list.RandomItem();
        }

        Assert.Throws<IndexOutOfRangeException>(GetRandom);
    }
}