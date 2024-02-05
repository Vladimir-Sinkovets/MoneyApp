using Microsoft.AspNetCore.Mvc;
using MoneyApp.Infrastructure.Interfaces.DataAccess;
using MoneyApp.UseCases.Handlers.Records.Commands.AddRecord;

namespace MoneyApp.WebApi.Controllers
{
#if DEBUG
    public class TestDataBase : Controller
    {
        private readonly IDbContext _dbContext;

        public TestDataBase(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("test/GetDb")]
        public async Task<IActionResult> GetDb()
        {
            var records = HtmlRecords();

            var users = UsersHtml();

            var html = records + "<hr>" + users;

            return Content(html, "text/html");
        }

        [HttpDelete]
        [Route("test/DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var user = _dbContext.Users.First(u => u.Id == userId);

            _dbContext.Users.Remove(user);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }



        private string HtmlRecords()
        {
            var records = _dbContext.Records.ToList();

            var htmlPage = "<!DOCTYPE html><html><body>";

            htmlPage += "<table>";

            htmlPage += $"<tr><th>record.Id</th><th>record.Created</th><th>record.UserId</th><th>record.Text</th><th>record.Change</th></tr>";

            foreach (var record in records)
            {
                htmlPage += "<tr>";

                htmlPage += $"<td>{record.Id}</td><td>{record.Created}</td><td>{record.UserId}</td><td>{record.Text}</td><td>{record.Change}</td>";

                htmlPage += "</tr>";
            }

            htmlPage += "</table>";

            htmlPage += "</body></html>";
            return htmlPage;
        }
        private string UsersHtml()
        {
            var users = _dbContext.Users.ToList();

            var htmlPage = "<!DOCTYPE html><html><body>";

            htmlPage += "<table>";

            htmlPage += $"<tr><th>user.Id</th><th>user.UserName</th><th>user.Email</th><th>user.Role</th><th>user.Password</th></tr>";

            foreach (var user in users)
            {
                htmlPage += "<tr>";

                htmlPage += $"<td>{user.Id}</td><td>{user.UserName}</td><td>{user.Email}</td><td>{user.Role}</td><td>{user.Password}</td>";

                htmlPage += "</tr>";
            }

            htmlPage += "</table>";

            htmlPage += "</body></html>";
            return htmlPage;
        }
    }
#endif
}
