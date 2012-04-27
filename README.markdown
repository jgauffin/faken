Fake implementation of HttpContextBase
=================================================================================================

For terse and expressive tests without the tedious setup of a mocking framework. Tedious because
the HttpContextBase object graph is deep and rightfully riddled with NotImplementedException.

Can't I just roll my own?
-----------
Yes you can, and you'll have plenty of company.

 1. [MvcContrib](http://mvccontrib.codeplex.com/)
 2. [Oxite](http://oxite.codeplex.com/)
 3. [MVCFakes](http://stephenwalther.com/blog/archive/2008/07/01/asp-net-mvc-tip-12-faking-the-controller-context.aspx)
 4. [ASP.NET MVC Session at Mix08, TDD and MvcMockHelpers](http://www.hanselman.com/blog/ASPNETMVCSessionAtMix08TDDAndMvcMockHelpers.aspx)

Each of these projects had the same idea yet they all have subtly different interfaces. NuGet is about unifying  community fragmentation and making it easy to share and reuse code. This project intentionally has no dependencies on Asp.Net MVC or any mocking frameworks.
 
Examples:
-----------

QueryString:

```c#
[Test]
public void Url_query_string_should_add_to_query_string_collection()
{
	var url = new Uri("http://google.com?q=awesome&p=1");

	var request = new FakeHttpRequest(url);

	Assert.That(request.QueryString["q"], Is.EqualTo("awesome"));
	Assert.That(request.QueryString["p"], Is.EqualTo("1"));
}

[Test]
public void Can_access_query_string_values_by_default_indexer()
{
	var request = new FakeHttpRequest();

	request.QueryString.Add("id", "3");

	Assert.That(request["id"], Is.EqualTo("3"));
}
```

Form:

```c#
[Test]
public void Can_access_form_values_by_default_indexer()
{
	var request = new FakeHttpRequest();

	request.Form.Add("color", "blue");

	Assert.That(request["color"], Is.EqualTo("blue"));
}
```

Session:

```c#
[Test]
public void Should_add_values_to_session()
{
	var session = new FakeHttpSessionState { { "color", "red" } };

	Assert.That(session["color"], Is.EqualTo("red"));
}
```

User:

```
[Test]
public void User_should_not_be_authenticated()
{
	var principal = new FakeHttpContext().User;

	Assert.That(principal.Identity.IsAuthenticated, Is.False);
}

[Test]
public void User_can_be_authenticated_with_convenience_method()
{
	var context = new FakeHttpContext().Authenticate();

	Assert.That(context.User.Identity.IsAuthenticated, Is.True);
}
```

