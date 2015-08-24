# Introduction #
In order to better trace commits in source control back to [issues](http://code.google.com/p/testoriented/issues/list), some properties have been added to folders.  Together, they implement the [Subversion integration with an issue tracker](http://www.fogcreek.com/FogBugz/docs/40/Articles/SourceControl/TortoiseSVN.html) as described in the FogBugz documentation.


# How to use #
If you are using [TortoiseSVN](http://tortoisesvn.tigris.org/), then you'll get an extra input text box when trying to commit, prompting you for an issue number.

If not, you can instead add the string **Issue:**, followed by a space and the issue number for which the commit is related to the change log.