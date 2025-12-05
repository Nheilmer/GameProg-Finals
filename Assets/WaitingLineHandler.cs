using System.Collections.Generic;
using UnityEngine;

public class WaitingLineHandler : MonoBehaviour
{
    [SerializeField] private List<PathHandler> waitingLine = new List<PathHandler>();
    [SerializeField] private List<Transform> linePositions; // 14 positions in order
    [SerializeField] private int maxCapacity = 14;

    // Adds an NPC to the line, returns true if added
    public bool TryAddToLine(PathHandler npc)
    {
        if (waitingLine.Count >= maxCapacity)
            return false;

        waitingLine.Add(npc);
        UpdatePositions();
        return true;
    }

    // Removes an NPC from the line
    public void RemoveFromLine(PathHandler npc)
    {
        if (waitingLine.Contains(npc))
        {
            waitingLine.Remove(npc);
            UpdatePositions();
        }
    }

    // Returns the first NPC in line, or null if empty
    public PathHandler PeekFirstInLine()
    {
        if (waitingLine.Count == 0)
            return null;

        return waitingLine[0];
    }

    // Move NPCs forward in the line after someone leaves
    private void UpdatePositions()
    {
        int i = 0;
        foreach (var npc in waitingLine)
        {
            if (i < linePositions.Count)
                npc.MoveToLinePosition(linePositions[i]);
            i++;
        }
    }

    // Returns current line count
    public int GetLineCount()
    {
        return waitingLine.Count;
    }

    public List<PathHandler> getPayers() {
        return waitingLine;
    }
}
