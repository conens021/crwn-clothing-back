namespace CrwnClothing.BLL.Models
{
    public class VerifyCodeTemplate : TemplateModelBase
    {
        public string Username { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string ConfirmationLink { get; set; } = string.Empty;
    }
}
