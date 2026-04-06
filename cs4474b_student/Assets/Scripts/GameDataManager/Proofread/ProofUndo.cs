using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//helper class
public class Edit
{
    public Edit(ProofWord sender, string updated, string previous)
    {
        this.sender = sender;
        this.updated = updated;
        this.previous = previous;
    }
    public ProofWord sender;
    public string updated;
    public string previous;
}
public class ProofUndo : MonoBehaviour
{
    public Stack<Edit> undo_history;
    public Stack<Edit> redo_history;

    void Start()
    {
        undo_history = new Stack<Edit>();
        redo_history = new Stack<Edit>();
    }

    // word objects will record their changes by calling this function
    public void RecordEdit(ProofWord sender, string updated, string previous)
    {
        Edit Change = new Edit(sender, updated, previous);
        undo_history.Push(Change);
        redo_history.Clear();
    }

    public void UndoEdit()
    {
        if(undo_history.Count() < 1)
        {
            return;
        }
        Edit Change = undo_history.Pop();
        Change.sender.Remote_Update(Change.previous); //remote undo change in word
        redo_history.Push(Change);
    }

    public void RedoEdit()
    {
        if(redo_history.Count() < 1)
        {
            return;
        }
        Edit Change = redo_history.Pop();
        Change.sender.Remote_Update(Change.updated); // remote redo change in word
        undo_history.Push(Change);
    }
}