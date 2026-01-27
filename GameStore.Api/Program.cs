var builder = WebApplication.CreateBuilder(args);
//this is the application builder which helps to configure the application

//below secttion is to configure http request pipeline
var app = builder.Build();//instance of the application
//this is basically the host of the application 

app.MapGet("/", () => "Hello World!");

app.Run();

//this files defines the code to bootstrap the application 