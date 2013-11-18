Multiple-ASPNET-Identity-Contexts
=================================

A console sandbox application that uses class libraries to manage 2 (or more) separate ASPNET.Identity DataContexts across multiple databases.


Instructions:
=============

Update the DataContext(s) to use your SQL Server conection string(s). (Each context can be on a seperate DB).

Adjust properties on derived IdentityUser classe(s) to your needs

Run the ConsoleApplication to test/build tables and seed identity zone(s).


Other Details:
==============

Each identity class library respresents one set of identity users. These users, and their assciated tables, are seperate from others and utilize their own roles. This is a clean way to split off user groups outside of just roles. For example: PlatformUser(s) can access a certain application that can be used to manage AccountUser(s). AccountUser(s) use a seperate client application which is not aware of PlatformUsers or PlatfomrmUserRoles.

Each DataContext specifies unique table names for the IdentityTables within that class library.

Each identity class library has a management class which has been modified to allow for EmailAddresses as UserNames.

