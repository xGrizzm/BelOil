namespace webapi.core.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int Barrels {  get; set; }

        public List<FieldEntity> Fields { get; set; } = new();

        public UserEntity(int id, string name, string login, string password) : base(id)
        {
            Name = name;
            Login = login;
            Password = password;
        }
    }
}
