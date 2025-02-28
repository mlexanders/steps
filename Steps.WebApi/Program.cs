using Steps.Utils.AppDefinition;


//
// Expression<Func<ClubViewModel, bool>> expr = ((club) => club.Name == "123" && club.Id != Guid.Empty);
// string query = FilterExpressionBuilder.ExpressionToQueryString(expr);
// Console.WriteLine(query); 
// // Выведет: {"param":"Id","op":"eq","val":"123"}
//
//
// var restoredExpression = FilterExpressionBuilder.QueryStringToExpression(query);
// var compiled = restoredExpression.Compile();
// // bool result = compiled(new ClubViewModel { Id = "123" }); 
// Console.WriteLine(result); // true

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefinitions(builder, typeof(Program));

var app = builder.Build();

app.UseDefinitions(typeof(Program));

app.Run();






