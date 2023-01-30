namespace DAL;

public class Note
{
    public int Id { get; set; } // todo: while using database REMOVE SETTER!

    public string Text { get; set; } // todo: ENCRYPT!!! text should not be exposed even if database is compromised

    //todo: the solution https://security.stackexchange.com/questions/157422/store-encrypted-user-data-in-database
    public string DateOfCreation { get; set; } //= DateTime.Now.ToString("f");
    public Diary? Diary { get; set; } // remove nullable?

    // todo: password?
    public Note()
    {
        
    }
    public Note(string text, Diary? diary = null)
    {
        Text = text;
        if (diary is not null) Diary = diary;
        this.DateOfCreation = DateTime.Now.ToString("f");
    }
}