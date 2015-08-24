# User Interface #

It would be nice to have the editor's context menu be more similar to that illustrated in the following  screenshot of [TestDriven.NET](http://www.testdriven.net/):

![![](http://www.testdriven.net/images/content_vs1-small.png)](http://www.testdriven.net/images/content_vs1.png)

...as TODD's functionality is currently:
  1. a sub-menu (partially fixed by [issue 1](http://code.google.com/p/testoriented/issues/detail?id=1))
  1. ...that's nowhere near the top
  1. ...and is not attainable through keyboard shortcuts (partially fixed by [issue 1](http://code.google.com/p/testoriented/issues/detail?id=1))
...as illustrated in the following screenshot:

![http://testoriented.googlecode.com/svn/wiki/TODDsubmenu.png](http://testoriented.googlecode.com/svn/wiki/TODDsubmenu.png)

## Raw Ideas taken from the paper ##

More appropriate user-interface triggers could also be researched, such as exposing TODD’s test-generation functionality through the Eclipse JDT quick-assist extensions, since the test generation is indeed sensitive to context. Such quick-assist functionality may also be able to detect if tests have already been written for the method currently under consideration – thus allowing users to navigate to the tests – as well as determining what new tests would need to be written to verify recently-performed functionality changes or additions.

It may even be possible to integrate the capabilities of Testar in determining the set of unit tests to re-run as a result of changing some of the source code.



