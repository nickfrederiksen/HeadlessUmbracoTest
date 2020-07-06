# HeadlessUmbracoTest

This repo contains the source code for my blog post about making a local install of Umbraco, headless: <https://ndesoft.dk/2020/07/03/making-umbraco-headless/>

To run the application, simply build and run the HeadlessUmbracoTest.App project.
Please note, that I'm using a setup we have in my company, where the web.config is build from a template, in this case web.template.config. So, after installing Umbraco you need to copy the connection string and ConfigurationStatus values into the template. Otherwise the install process will be restarted when rebuilding the solution.

If you have any idea how to do CORS better or anything, please let me know.