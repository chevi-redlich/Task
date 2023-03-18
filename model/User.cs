namespace Tasks;

public class User {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Password { get; set; }

    public bool isAdmin { get; set; }

    public static explicit operator User(bool v)
    {
        throw new NotImplementedException();
    }
    public bool Equals(User user) {
        return
        (this.Id==user.Id&&
        this.Name==user.Name&&
        this.Password==user.Password&&
        this.isAdmin==user.isAdmin);
    }
}