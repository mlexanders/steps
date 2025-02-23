namespace Steps.Client;

public static class ClientData
{
    public const string AppName = "Ступени мастерства";

    public static string PageTittle(string pageName) => $"{AppName} - {pageName}";
}