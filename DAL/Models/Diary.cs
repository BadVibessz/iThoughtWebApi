namespace DAL;

public class Diary
{
    public int Id { get; set; } // todo: while using database REMOVE SETTER!
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Password { get; set; } // todo: разобраться с хешированием
    public DateTime DateOfCreation { get; } = DateTime.Now;

    // private
    private List<Note> _notes = new();

    public Diary(string name, string? desc = null, string? pass = null)
    {
        this.Name = name;
        this.Description = desc;
        this.Password = pass;
    }

    public void AddNote(Note note)
    {
        _notes.Add(note);
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

        var isDeleted = _notes.Remove(note);
        note.Diary = null;

        return isDeleted;
    }

    private Note? GetNote(int noteId)
        => _notes.Find(n => n.Id == noteId);

    public List<Note> GetNotes() => new(_notes);
}