# Hangfire.Unity4


This project is based on the [Hangfire.Unity](https://github.com/phenixdotnet/Hangfire.Unity) project with 2 chnages

- *Hangfire.Unity* works fine for Unity v5+ but due to namespace changes you can't use it for Unity v4. This package (Hangfire.Unity4) works with old namespace of unity which is ` Microsoft.Practices.Unity`
- This package also have a helper class for lifetime management called  `PerRequestOrHierarchicalLifeTimeManager` which if you don't have `HttpContext` available, it will offer `HierarchicalLifetimeManager` instead of `PerRequestLifetimeManager`
you can use this to in `RegisterTypes` method to avoid *Resolve* exception

```csharp
public static void RegisterTypes(IUnityContainer container)
{
    container.RegisterType<IMyService, MyService>(new PerRequestOrHierarchicalLifeTimeManager());
}
```

[Hangfire](http://hangfire.io) background job activator based on 
[Unity](https://github.com/unitycontainer/unity) IoC Container. It allows you to use instance
methods of classes that define parametrized constructors:



```csharp
public class EmailService
{
	private DbContext _context;
    private IEmailSender _sender;
	
	public EmailService(DbContext context, IEmailSender sender)
	{
		_context = context;
		_sender = sender;
	}
	
	public void Send(int userId, string message)
	{
		var user = _context.Users.Get(userId);
		_sender.Send(user.Email, message);
	}
}	

// Somewhere in the code
BackgroundJob.Enqueue<EmailService>(x => x.Send(1, "Hello, world!"));
```

Improve the testability of your jobs without static factories!

Installation
--------------

Hangfire.Unity4 is available as a NuGet Package. Type the following
command into NuGet Package Manager Console window to install it:

```
Install-Package Hangfire.Unity4
```

Usage
------

The package provides an extension method for [OWIN bootstrapper](http://docs.hangfire.io/en/latest/users-guide/getting-started/owin-bootstrapper.html):

```csharp
app.UseHangfire(config =>
{
    var container = new UnityContainer();
	// Setup your unity container
	
	// Use it with Hangfire
    config.UseUnityActivator(container);
});
```

In order to use the library outside of web application, set the static `JobActivator.Current` property:

```csharp
var container = new UnityContainer();
JobActivator.Current = new UnityJobActivator(container);
```
