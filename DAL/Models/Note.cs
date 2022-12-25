namespace DAL;

public class Note
{
    public int Id { get; set; } // todo: while using database REMOVE SETTER!

    public string Text { get; set; } // todo: ENCRYPT!!! text should not be exposed even if database is compromised

    //todo: the solution https://security.stackexchange.com/questions/157422/store-encrypted-user-data-in-database
    public DateTime DateOfCreation { get; } = DateTime.Now;
    public Diary? Diary { get; set; }

    public Note(string text, Diary? diary = null)
    {
        this.Text = text;
        if (diary is not null) this.Diary = diary;
    }
}