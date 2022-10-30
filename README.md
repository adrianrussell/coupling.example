# Coupling Demo Repository

Demo repo that contains an example of the impact of low and high coupling on a codebase.  This can be used to demonstrate the different impact that design decisions can have on the maintainability and extensibility of a codebase when there is high and low coupling.

The example used to explain this is an API that accepts weather measurements for various different regions.  Each specific change is identified by an identifier CHG-X.  For commits to the codebase, low coupling commits are marked as LOW-CHG-X and high coupling changes are marked as HIGH-CHG-X.

In this example the following development activities have happened to date;

1. The service was initially developed to accept rain data in mm of rainfall per hour for a single rain station (CHG-1)
2. The service was then enhanced to accept temperature readings at a point in time for a single temperature station (CHG-2)
3. The service was then enhanced to accept wind direction and strength measurements for multiple windsocks based on the name of the windsock location (CHG-3)
4. Subsequent to the initial implementation of the three instrument types additional clients also wanted to submit data for temperature and wind direction using latitude and longitude coordinates (CHG-4)

Over time the use of the data that the service accepted grew substantially, which resulted in more measurement services submitting readings for temperature and wind.  The submission of more rain data was deemed as a nice to have but no one was willing to pay for changes to allow additional location data to be accepted for the rain service.

Once the usefulness of the service had been established it was thought that it would be good to start collecting data for another region (CHG-6).  This was a particularly wet region so the first service that they wanted to start using was the rainfall service.  This region collected rainfall data in CM per 24 hours and used numbered map grids to locate the weather stations (CHG-7).  Once the rainfall service was in use, it was realised that both temperature and wind direction would also be useful measurements to collect, but only temperature could be implemented at this point time (CHG-8).  These needed to be collected using the same map grid coordinate system as the rainfall collection for this region.

A new standardised coordinate system is being implemented across the globe, submitters to the service will have 12 months to change over to the new WGS84 coordinate system.  During this time submitters to the service can use both coordinate systems. (CHG-9)

At the same time, a third region is being introduced that needs to record rain data and new sensor, a Pyranometer for measuring solar radiation.(CHG-10).
