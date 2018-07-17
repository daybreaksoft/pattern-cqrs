namespace AspNetCore.EF.Sample.Query.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int Point { get; set; }

        public int[] Roles { get; set; }
    }

    public class UserListItemViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int Point { get; set; }
    }
}
