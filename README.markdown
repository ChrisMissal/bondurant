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

### Clients

Clients are essentially "subscribers" that are injected as your IntegrationService deems necessary. You can include your own code in these or use them to programmitically add scripts like GoogleAnalytics, ClickTale or any other third party client-side integrations.

### Prerequisites

Some scripts may have prerequisites that need to be inserted on a page so they are functional. If multiple clients have overlapping prerequisites they will not be automatically included twice.

Consider the following:

	<script src="/scripts/jquery-1.4.1.min.js"></script>
	<script src="/scripts/messaging.js"></script>
	<script type="text/javascript">
	$(function() {queueMessage('test message');}
	function doSomething() {}
	</script>

The MessageClient from the test site is a JQueryClient *(so jQuery is loaded automatically)* but also requires the messaging file. If a second client is also a JQueryClient, it will not include the script tag for jQuery twice, but will recognize it and avoid including it on the page for the second client.
