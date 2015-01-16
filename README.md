**DI Autoregistration for MVC**

This solution implements automatic class and interface registration in a IoC container for dependency injection in a MVC test website.

The dependency injection library has been made teaching purposes and they're missing some features you might actually need in a "real world" library.

The project has multi IoC framework engine support, by implementing the interface 'IDependencyInjectionEngine'.

**Engines supported**

From scratch there are three engines avialable: [Autofac](http://autofac.org), [nInject](http://www.ninject.org/) and [Unity](https://unity.codeplex.com/). Feel free to add more if you need them.

**External Assembly support**

Sometimes you don't want to add references in your projects to some or any external libraries. With regular       dependency injection implementations, you need to have all the libraries in References in order to be able to register them in your IoC container. This library allows you to use a filter to load .dll or .exe files in your        app directory and use them in your dependency injection.

**Automatic controller registration**

This library has been made for MVC5 (or lower if you change the target packages used). It will register all the "Controller" derived classes in your project automatically. If you need to make this library to work with non-MVC solutions, you'd only need to add support for 'Resolve'.

**Thank You!**

Enjoy it and check [my blog](http://joanvilarino.info), there are articles explaining this an much other things on application development.