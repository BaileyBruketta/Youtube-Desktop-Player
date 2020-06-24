I wanted a way to listen to music on youtube without having to look at the entire browser window and youtube sidebar. I still wanted to have an album cover in my peripheral vision.
I usually drag the borders of the browser to make a small box, but this still produces too much white space on the screen and has uneccessary icons and fields. 

Currently, this application produces a small window with a black backrgound, an embedded youtube player, and a text entry box with a reload button. 

Placing a link to a video into the text box and clicking reload reloads the embedded player with whatever video the link led to. 

Architecture is as follows:

The application runs in C#. At runtime, the Main() function of the MainWindow executes a Bat file, startServer.Bat.
The Bat file executes a python script, pyServer.py.
pyServer creates a localhost simple HTTP server. This is necessary as embedded youtube videos need to play from an HTTP server to work.
The localhost server created here points to index.html, which by contains the styling for the window's contents and the embedded player. 
Within C#'s MainWindow.XAML is the creation of a Browser object, made possible by "CefSharp," a library for loading browsers in C# applications. 
Functionality within the MainWindow class allows for new songs to be linked in and the window to be reloaded. 
Because of how my personal environment was set up, a copy of index.html, startserver.bat, pyServer.py, enum.py, http(python library) are stored in bin/x86/debug

A final build has not been created yet; pre-packaged builds are a possibility.


