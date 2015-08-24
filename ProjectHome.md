The benefits of [Test-Driven Development (TDD)](http://en.wikipedia.org/wiki/Test_driven_development) can be appealing to many seeking to reduce the amount of time spent on a software project, but not all projects can implement the TDD methodology fully. A technique similar to TDD -- test-oriented development and debugging -- is implemented as an [Eclipse](http://www.eclipse.org/) plug-in to support this pragmatic approach so that it can help write high quality software.

# Screenshots #
## Invoking the 'Generate test(s)' action ##
You invoke the context menu on or inside the method you wish to write a test for, and then select `JUnit`/`Generate test(s)`.
![http://testoriented.googlecode.com/svn/wiki/GenerateTestsFromUnformat.png](http://testoriented.googlecode.com/svn/wiki/GenerateTestsFromUnformat.png)
## Result of invoking test generation ##
TODD will find the corresponding test class, open it in an editor and generate a test method for you.  All you have to do then is fill in a few 'blanks' and run your new test, which you can do by invoking the `Debug JUnit test(s)` or `Run JUnit test(s)` actions in the same menu!
![http://testoriented.googlecode.com/svn/wiki/SmarterTemplates.png](http://testoriented.googlecode.com/svn/wiki/SmarterTemplates.png)