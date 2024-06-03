using KZHDotNetCore.Shared;

namespace KZHDotNetCore.WinFormsApp.SqlInjection
{
    public partial class Form1 : Form
    {
        private readonly DapperService _dapperService;

        public Form1()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // string query = $"select * from tbl_user where email = '{txtEmail.Text.Trim()}' and password = '{txtPassword.Text.Trim()}'";
            string query = $"select * from tbl_user where email = @Email and password = @Password";
            var model = _dapperService.QueryFirstOrDefault<UserModel>(query, new
            {
                Email = txtEmail.Text.Trim(),
                Password = txtPassword.Text.Trim(),
            });
            if(model is null)
            {
                MessageBox.Show(@"User does not exist.");
                return;
            }
            MessageBox.Show(@"Is admin : " +  model.Email);
        }
    }

    public class UserModel
    {
        public string? Email { get; set; }
        public bool IsAdmin { get; set; }        
    }
}
