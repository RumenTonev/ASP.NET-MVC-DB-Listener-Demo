# ASP.NET-MVC-DB-Listener-Demo
ASP.NET MVC DB Listener Demo
This is a database listener demo.It works locally, but Azure deployment is not possible, due to the lack of SQL Service Broker support. 
    Other way is to use polling, but this is a heavy workaround and is not the point of the demo. Possible and probably future successfull solution by using VM anda host both
    application and DB on the same one or in virtual network. 
    Demo: On the "machines" page in aside nav there are all available VM for monitoring for example if we monitoring SCOM instalation.
    Every one of them has link to its own page, where there are shown two values-CPU and Memory in this particular case.
    RandomGeneratot console app radomly updates these values for randomly chosen available VM-thus simulates "real-life" case.
