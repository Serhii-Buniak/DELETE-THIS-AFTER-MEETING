using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddDbContext<DryCleaningContext>();

services.AddControllers().AddNewtonsoftJson();

services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();
