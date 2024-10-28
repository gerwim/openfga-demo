using ConsoleApp;
using Spectre.Console;

while (true)
{
    var user = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Check if user has can_edit permissions on user:gerwim?")
            .AddChoices("user:baas", "user:karin"));
    
    AnsiConsole.Clear();

    var authService = new AuthorizationService();

    var hasAccess = await authService.Check(user, "user:gerwim", "can_edit");
    AnsiConsole.WriteLine(hasAccess
        ? $"{Emoji.Known.CheckMarkButton}  {user} can edit user:gerwim"
        : $"{Emoji.Known.CrossMark}  {user} can NOT edit user:gerwim");
}