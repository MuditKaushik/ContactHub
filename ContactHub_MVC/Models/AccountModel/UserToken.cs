namespace ContactHub_MVC.Models.AccountModel
{
    public class UserToken
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public string Expires_In { get; set; }
        public string Authorize => $"{Token_Type} {Access_Token}";
    }
}