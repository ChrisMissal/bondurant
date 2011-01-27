bondurant
========

### bondurant is an asp.net mvc library that aims to make application events work well with javascript integrations

Here's an example of an application event that occurs. This event should display some JavaScript on the next page rendered:

    public ActionResult Index()
    {
        integrationService.HomePageViewed();
        return View();
    }

The idea above is that calling HomePageViewed() *(a method we've defined in our app)* will do output some predefined JavaScript.

For a couple simple examples, pull down the code and run the TestSite to see how it works. There are two URLs that will trigger events:

    /
    /home/backtohome
