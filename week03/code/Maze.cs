using System;
using System.Collections.Generic;

public class Maze
{
    private readonly Dictionary<(int, int), bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<(int, int), bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    public void MoveLeft()
    {
        var pos = (_currX, _currY);

        if (!_mazeMap.ContainsKey(pos) || !_mazeMap[pos][0])
        {
            throw new InvalidOperationException("Can't go that way!");
        }

        _currX -= 1;
    }

    public void MoveRight()
    {
        var pos = (_currX, _currY);

        if (!_mazeMap.ContainsKey(pos) || !_mazeMap[pos][1])
        {
            throw new InvalidOperationException("Can't go that way!");
        }

        _currX += 1;
    }

    public void MoveUp()
    {
        var pos = (_currX, _currY);

        if (!_mazeMap.ContainsKey(pos) || !_mazeMap[pos][2])
        {
            throw new InvalidOperationException("Can't go that way!");
        }

        _currY -= 1;
    }

    public void MoveDown()
    {
        var pos = (_currX, _currY);

        if (!_mazeMap.ContainsKey(pos) || !_mazeMap[pos][3])
        {
            throw new InvalidOperationException("Can't go that way!");
        }

        _currY += 1;
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}
