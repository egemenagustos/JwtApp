namespace Onion.JwpApp.Application.Dtos
{
    public class CheckUserRequestDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public bool IsExist { get; set; }
    }
}
