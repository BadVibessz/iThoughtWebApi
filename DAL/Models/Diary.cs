using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL;

public class Diary
{
    public int Id { get; set; } // todo: while using database REMOVE SETTER!
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Password { get; set; } // todo: разобраться с хешированием

    [Column] public string DateOfCreation { get; set; } // = DateTime.Now.ToString("f"); // todo: not mapping

    //public List<Note> Notes { get; set; } = new(); // todo: safe setter?
    public ICollection<Note> Notes { get; set; } = new Collection<Note>(); // todo: safe setter?

    public Diary()
    {
    }

    public Diary(Diary other)
    {
        Name = other.Name;
        Description = other.Description;
        Password = other.Password;
        //Notes = new(other.Notes);
        Notes = other.Notes; // todo: copy
    }

    public Diary(string name, string? desc = null, string? pass = null)
    {
        this.Name = name;
        this.Description = desc;
        this.Password = pass;
        this.DateOfCreation = DateTime.Now.ToString("f");
    }

    public void AddNote(Note note)
    {
        Notes.Add(note);
        note.Diary = this;
    }

    public bool UpdateNote(int noteId, string newText)
    {
        var note = GetNote(noteId);
        if (note is null) return false;

        note.Text = newText;
        return true;
    }

    public bool DeleteNote(int noteId)
    {
        var note = GetNote(noteId);
        if (note is null) return false;

        var isDeleted = Notes.Remove(note);
        note.Diary = null;

        return isDeleted;
    }

    private Note? GetNote(int noteId)
        => Notes.FirstOrDefault(n => n.Id == noteId);

    public List<Note> GetNotes() => new(Notes);
}