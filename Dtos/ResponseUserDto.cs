namespace Software2_API.Dtos
{
    public class ResponseUserDto
    {
        public int Id_User { get; set; }
        public int Id_Role { get; set; }
        public string Role { get; set; } = string.Empty;
        public int Id_State { get; set; }
        public string State { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
